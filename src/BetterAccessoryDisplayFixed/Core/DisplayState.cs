using Duckov.UI;
using Duckov.Utilities;
using ItemStatsSystem;
using UnityEngine;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Per-ItemDisplay cache. ItemDisplay instances are pooled by the game, so this state survives
    /// re-use and is thrown away together with the Unity object (ConditionalWeakTable keyed by the
    /// ItemDisplay reference; no leaks, no per-frame dictionary scans).
    /// </summary>
    internal sealed class DisplayState
    {
        // "is this a gun" cache (ItemDisplay.Refresh is called a lot, for every item display)
        public Item IsGunFor;
        public bool IsGun;
        public bool GunKnown;

        // rebuild bookkeeping
        public Item BuiltTarget;
        public bool Built;
        public int SetupGeneration;
        public int RebuiltGeneration;

        // cached game members (resolved through cached reflection members, see Access)
        public PrefabPool<SlotIndicator> Pool;
        public GameObject Container;
        public bool ContainerResolved;
    }
}
