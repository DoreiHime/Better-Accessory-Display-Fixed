using System;
using System.Runtime.CompilerServices;
using Duckov.UI;
using ItemStatsSystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Clean re-implementation of the original mod's SlotIndicator colour logic.
    ///
    /// Original behaviour (decompiled v1.1, Workshop 3596581923):
    ///  * slot has content       -> contentIndicator Image = white;
    ///  * synthetic "TempSlot"   -> contentIndicator active + Image = gray (v1.1 changed red -> gray);
    ///  * real slot without item -> do nothing (the game keeps the indicator hidden);
    ///  * Target == null         -> do nothing (the original spammed a warning here, 172k lines).
    ///
    /// The Image component is resolved once per SlotIndicator and cached (the original called
    /// GetComponent&lt;Image&gt; plus typeof(...).GetField on every refresh).
    /// </summary>
    internal static class SlotColors
    {
        private static readonly Color OccupiedColor = Color.white;
        private static readonly Color ExtraSlotColor = Color.gray;

        private static readonly ConditionalWeakTable<SlotIndicator, ContentImageCache> ImageCache =
            new ConditionalWeakTable<SlotIndicator, ContentImageCache>();

        private static bool _contentErrorLogged;
        private static bool _colorErrorLogged;

        internal static void OnRefresh(SlotIndicator indicator)
        {
            if (indicator == null) return;
            try
            {
                Slot slot = indicator.Target;
                if (slot == null) return;

                GameObject contentIndicator = Access.GetContentIndicator(indicator);
                if (contentIndicator == null) return;

                if (slot.Content != null)
                {
                    Image image = GetImage(indicator, contentIndicator);
                    if (image != null) image.color = OccupiedColor;
                }
                else if (string.Equals(slot.Key, "TempSlot", StringComparison.Ordinal))
                {
                    if (!contentIndicator.activeSelf) contentIndicator.SetActive(true);
                    Image image = GetImage(indicator, contentIndicator);
                    if (image != null) image.color = ExtraSlotColor;
                }
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _colorErrorLogged, "Slot colouring failed: " + e);
            }
        }

        private static Image GetImage(SlotIndicator indicator, GameObject contentIndicator)
        {
            ContentImageCache cache;
            if (!ImageCache.TryGetValue(indicator, out cache))
            {
                cache = new ContentImageCache();
                try { ImageCache.Add(indicator, cache); }
                catch { ImageCache.TryGetValue(indicator, out cache); }
            }
            if (cache == null) return null;
            if (cache.Resolved) return cache.Image;

            try { cache.Image = contentIndicator.GetComponent<Image>(); }
            catch (Exception e) { Log.ErrorOnce(ref _contentErrorLogged, "Reading the content indicator image failed: " + e); }
            cache.Resolved = true;
            return cache.Image;
        }

        private sealed class ContentImageCache
        {
            public bool Resolved;
            public Image Image;
        }
    }
}
