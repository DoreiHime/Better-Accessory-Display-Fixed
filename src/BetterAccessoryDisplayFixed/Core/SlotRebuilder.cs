using System;
using System.Runtime.CompilerServices;
using Duckov.UI;
using Duckov.Utilities;
using ItemStatsSystem;
using ItemStatsSystem.Items;
using UnityEngine;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Clean re-implementation of the original mod's 6-slot visuals.
    ///
    /// Original behaviour (decompiled v1.1, Workshop 3596581923):
    ///  * every gun ItemDisplay shows exactly 6 indicators in the order
    ///      Scope, Muzzle, Grip, Stock, Tec, Mag;
    ///  * a slot that exists on the weapon is bound to the real Slot;
    ///  * a slot the weapon does not have is bound to a synthetic Slot("TempSlot");
    ///  * occupied slots: contentIndicator Image -> white;
    ///  * synthetic slots: contentIndicator activated and coloured gray (v1.1);
    ///  * real empty slots keep the game's native look (indicator hidden);
    ///  * uninspected weapons keep the slot container hidden (v1.1 fix).
    ///
    /// Differences (performance only, visuals unchanged):
    ///  * the indicator pool is cleared/rebuilt ONCE per ItemDisplay.Setup instead of on every
    ///    Refresh (the original did ReleaseAll + 6x pool.Get on every single Refresh);
    ///  * reflection members are cached statically (see Access) instead of being looked up on
    ///    every refresh;
    ///  * no Debug.Log / LogWarning calls at all in the hot path.
    /// </summary>
    internal static class SlotRebuilder
    {
        // The original mod checks item.Tags.Contains("Gun"); keep the same tag string.
        private const string Constants_GunTag = "Gun";

        /// <summary>Slot keys, in visual order. Same order as the original mod.</summary>
        internal static readonly string[] GunSlots = { "Scope", "Muzzle", "Grip", "Stock", "Tec", "Mag" };

        private static readonly Slot[] TempSlots = CreateTempSlots();
        private static readonly ConditionalWeakTable<ItemDisplay, DisplayState> States =
            new ConditionalWeakTable<ItemDisplay, DisplayState>();

        private static bool _poolErrorLogged;
        private static bool _rebuildErrorLogged;
        private static bool _setupErrorLogged;
        private static bool _refreshErrorLogged;

        private static Slot[] CreateTempSlots()
        {
            Slot[] slots = new Slot[GunSlots.Length];
            for (int i = 0; i < slots.Length; i++) slots[i] = new Slot("TempSlot");
            return slots;
        }

        private static DisplayState GetState(ItemDisplay display)
        {
            DisplayState state;
            if (States.TryGetValue(display, out state)) return state;
            state = new DisplayState();
            try { States.Add(display, state); }
            catch { States.TryGetValue(display, out state); }
            return state;
        }

        // ------------------------------------------------------------------ hooks

        /// <summary>ItemDisplay.Setup prefix: a new setup generation starts (used to detect re-setup).</summary>
        internal static void OnSetupBegin(ItemDisplay display)
        {
            if (display == null) return;
            try
            {
                GetState(display).SetupGeneration++;
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _setupErrorLogged, "Setup prefix failed: " + e);
            }
        }

        /// <summary>ItemDisplay.Setup postfix: build the 6 fixed slots right after the game's setup.</summary>
        internal static void OnSetupEnd(ItemDisplay display)
        {
            if (display == null) return;
            try
            {
                Item target = display.Target;
                if (target == null) return;
                DisplayState state = GetState(display);
                if (!IsGun(state, target)) return;

                bool alreadyBuilt = state.Built
                                    && ReferenceEquals(state.BuiltTarget, target)
                                    && state.RebuiltGeneration == state.SetupGeneration;
                if (!alreadyBuilt) Rebuild(display, state, target);
                EnsureContainer(display, state, target);
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _setupErrorLogged, "Setup postfix failed: " + e);
            }
        }

        /// <summary>
        /// ItemDisplay.Refresh postfix: cheap guard. Rebuilds only when the display was never
        /// built for the current target (e.g. it existed before this mod was activated), otherwise
        /// it just keeps the container visible. This is the hot path and must stay allocation-free.
        /// </summary>
        internal static void OnRefresh(ItemDisplay display)
        {
            if (display == null) return;
            try
            {
                Item target = display.Target;
                if (target == null) return;
                DisplayState state = GetState(display);
                if (!IsGun(state, target)) return;

                if (!state.Built || !ReferenceEquals(state.BuiltTarget, target) ||
                    state.RebuiltGeneration != state.SetupGeneration)
                {
                    Rebuild(display, state, target);
                }
                EnsureContainer(display, state, target);
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _refreshErrorLogged, "Refresh postfix failed: " + e);
            }
        }

        // ------------------------------------------------------------- internals

        private static bool IsGun(DisplayState state, Item item)
        {
            if (item == null) return false;
            if (state.GunKnown && ReferenceEquals(state.IsGunFor, item)) return state.IsGun;

            bool isGun = false;
            try { isGun = item.Tags != null && item.Tags.Contains(Constants_GunTag); }
            catch { isGun = false; }

            state.IsGunFor = item;
            state.IsGun = isGun;
            state.GunKnown = true;
            return isGun;
        }

        private static void Rebuild(ItemDisplay display, DisplayState state, Item target)
        {
            try
            {
                if (!Access.MembersAvailable) return;

                PrefabPool<SlotIndicator> pool = state.Pool;
                if (pool == null)
                {
                    pool = Access.GetPool(display);
                    state.Pool = pool;
                }
                if (pool == null)
                {
                    // Avoid retrying (and re-throwing) on every refresh; the log is emitted once.
                    Log.ErrorOnce(ref _poolErrorLogged,
                        "SlotIndicatorPool is unavailable; the 6-slot display stays inactive for this item.");
                    state.BuiltTarget = target;
                    state.Built = true;
                    state.RebuiltGeneration = state.SetupGeneration;
                    return;
                }

                if (!state.ContainerResolved)
                {
                    state.Container = Access.GetSlotContainer(display);
                    state.ContainerResolved = true;
                }

                // Same as the original: release the game's/previous indicators, then create exactly 6.
                pool.ReleaseAll();

                SlotCollection slots = null;
                try { slots = target.Slots; }
                catch (Exception e) { Log.Debug("Reading target.Slots failed: " + e.Message); }

                for (int i = 0; i < GunSlots.Length; i++)
                {
                    Slot slot = FindSlot(slots, GunSlots[i]);
                    if (slot == null) slot = TempSlots[i];

                    SlotIndicator indicator = null;
                    try { indicator = pool.Get(); }
                    catch (Exception e) { Log.Debug("Pool.Get failed: " + e.Message); continue; }
                    if (indicator == null) continue;

                    try { indicator.Setup(slot); }
                    catch (Exception e) { Log.Debug("SlotIndicator.Setup failed: " + e.Message); }
                }

                state.BuiltTarget = target;
                state.Built = true;
                state.RebuiltGeneration = state.SetupGeneration;
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _rebuildErrorLogged, "Rebuilding the fixed slots failed: " + e);
            }
        }

        private static Slot FindSlot(SlotCollection slots, string key)
        {
            if (slots == null || string.IsNullOrEmpty(key)) return null;
            try
            {
                // SlotCollection has a string indexer (dictionary based, built lazily by the game).
                Slot slot = slots[key];
                return slot;
            }
            catch (Exception e)
            {
                Log.Debug("Slot lookup for '" + key + "' failed: " + e.Message);
                return null;
            }
        }

        private static void EnsureContainer(ItemDisplay display, DisplayState state, Item target)
        {
            try
            {
                if (!state.ContainerResolved)
                {
                    state.Container = Access.GetSlotContainer(display);
                    state.ContainerResolved = true;
                }
                GameObject container = state.Container;
                if (container == null) return;
                // v1.1 behaviour: never force the container visible for an unsearched weapon.
                if (!container.activeSelf && !target.NeedInspection) container.SetActive(true);
            }
            catch (Exception e)
            {
                Log.Debug("Ensuring the slot container failed: " + e.Message);
            }
        }
    }
}
