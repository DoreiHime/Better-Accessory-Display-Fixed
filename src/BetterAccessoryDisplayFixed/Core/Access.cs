using System;
using System.Reflection;
using Duckov.UI;
using Duckov.Utilities;
using UnityEngine;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Cached access to the few private members the original mod used to look up on EVERY refresh
    /// (requirement b: reflection members are resolved once and cached statically).
    ///
    /// Game version 2.3.30 (Escape from Duckov):
    ///   ItemDisplay.slotIndicatorContainer  private GameObject
    ///   ItemDisplay.SlotIndicatorPool       private PrefabPool&lt;SlotIndicator&gt; (lazy property)
    ///   SlotIndicator.contentIndicator      private GameObject
    /// Everything else (Target, Slots, Tags, NeedInspection, ...) is public and used directly.
    /// </summary>
    internal static class Access
    {
        private const BindingFlags InstanceAny = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static readonly FieldInfo ItemDisplaySlotContainerField =
            typeof(ItemDisplay).GetField("slotIndicatorContainer", InstanceAny);

        private static readonly PropertyInfo ItemDisplayPoolProperty =
            typeof(ItemDisplay).GetProperty("SlotIndicatorPool", InstanceAny);

        private static readonly FieldInfo SlotIndicatorContentField =
            typeof(SlotIndicator).GetField("contentIndicator", InstanceAny);

        private static bool _missingLogged;

        /// <summary>True when every cached member was found (otherwise the mod stays passive).</summary>
        internal static bool MembersAvailable
        {
            get
            {
                bool ok = ItemDisplaySlotContainerField != null && ItemDisplayPoolProperty != null &&
                          SlotIndicatorContentField != null;
                if (!ok)
                {
                    Log.WarnOnce(ref _missingLogged,
                        "Game UI members not found (game update changed ItemDisplay/SlotIndicator?); " +
                        "the fixed slot display stays inactive.");
                }
                return ok;
            }
        }

        internal static GameObject GetSlotContainer(ItemDisplay display)
        {
            if (display == null || ItemDisplaySlotContainerField == null) return null;
            try { return ItemDisplaySlotContainerField.GetValue(display) as GameObject; }
            catch (Exception e) { Log.Debug("Reading slotIndicatorContainer failed: " + e.Message); return null; }
        }

        internal static PrefabPool<SlotIndicator> GetPool(ItemDisplay display)
        {
            if (display == null || ItemDisplayPoolProperty == null) return null;
            try { return ItemDisplayPoolProperty.GetValue(display) as PrefabPool<SlotIndicator>; }
            catch (Exception e) { Log.Debug("Reading SlotIndicatorPool failed: " + e.Message); return null; }
        }

        internal static GameObject GetContentIndicator(SlotIndicator indicator)
        {
            if (indicator == null || SlotIndicatorContentField == null) return null;
            try { return SlotIndicatorContentField.GetValue(indicator) as GameObject; }
            catch (Exception e) { Log.Debug("Reading SlotIndicator.contentIndicator failed: " + e.Message); return null; }
        }
    }
}
