using System;
using BetterAccessoryDisplayFixed.Core;
using Duckov.UI;
using HarmonyLib;

namespace BetterAccessoryDisplayFixed.Patches
{
    /// <summary>Hooks the game's ItemDisplay.Setup: after the game itself created the native slot
    /// indicators, replace them with exactly six fixed ones (same moment the original mod used).</summary>
    [HarmonyPatch(typeof(ItemDisplay), "Setup")]
    internal static class ItemDisplaySetupPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ItemDisplay __instance)
        {
            try { SlotRebuilder.OnSetupBegin(__instance); }
            catch (Exception e) { Log.Debug("Setup prefix exception: " + e.Message); }
        }

        [HarmonyPostfix]
        private static void Postfix(ItemDisplay __instance)
        {
            try { SlotRebuilder.OnSetupEnd(__instance); }
            catch (Exception e) { Log.Debug("Setup postfix exception: " + e.Message); }
        }
    }

    /// <summary>Hooks ItemDisplay.Refresh: cheap guard (rebuild only when needed + keep the
    /// container visible). The original mod did a full ReleaseAll + 6x pool.Get on every refresh.</summary>
    [HarmonyPatch(typeof(ItemDisplay), "Refresh")]
    internal static class ItemDisplayRefreshPatch
    {
        [HarmonyPostfix]
        private static void Postfix(ItemDisplay __instance)
        {
            try { SlotRebuilder.OnRefresh(__instance); }
            catch (Exception e) { Log.Debug("Refresh postfix exception: " + e.Message); }
        }
    }

    /// <summary>Hooks SlotIndicator.Refresh: occupied slots white, synthetic slots gray, empty real
    /// slots untouched, Target == null silent (no more 172k "Target 为 null" warning lines).</summary>
    [HarmonyPatch(typeof(SlotIndicator), "Refresh")]
    internal static class SlotIndicatorRefreshPatch
    {
        [HarmonyPostfix]
        private static void Postfix(SlotIndicator __instance)
        {
            try { SlotColors.OnRefresh(__instance); }
            catch (Exception e) { Log.Debug("SlotIndicator postfix exception: " + e.Message); }
        }
    }
}
