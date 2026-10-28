using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// All HarmonyLib usage lives in this class (the only class that is JIT-compiled with a
    /// HarmonyLib dependency). <see cref="HarmonyLoader.EnsureLoaded"/> MUST have succeeded
    /// before any method here is called.
    ///
    /// Responsibilities:
    ///  * apply this mod's own clean patches (ItemDisplay.Setup/Refresh + SlotIndicator.Refresh);
    ///  * remove the original mod's patches by its Harmony id ("BetterAccessoryDisplay"),
    ///    with an extra assembly-based sweep in case the id ever changes;
    ///  * report whether the original's patches are still present;
    ///  * remove only this mod's patches when the mod is disabled.
    /// </summary>
    internal static class HarmonyBridge
    {
        private static Harmony _patchHarmony;
        private static Harmony _guardHarmony;
        private static bool _verifiedOnce;

        /// <summary>Applies this mod's patches. Returns false (and removes partial patches) on failure.</summary>
        internal static bool Install(out string error)
        {
            error = null;
            try
            {
                if (_patchHarmony == null) _patchHarmony = new Harmony(ModRuntime.PatchHarmonyId);
                if (_guardHarmony == null) _guardHarmony = new Harmony(ModRuntime.PatchHarmonyId + ".Guard");

                // A previous attempt (or a hot reload) may have left partial patches behind.
                RemoveOwnPatches();

                _patchHarmony.PatchAll(typeof(HarmonyBridge).Assembly);

                if (!PatchesOwnedBy(ModRuntime.PatchHarmonyId))
                {
                    error = "PatchAll applied no patch (game method signatures changed?)";
                    RemoveOwnPatches();
                    return false;
                }

                _verifiedOnce = true;
                return true;
            }
            catch (Exception e)
            {
                error = e.GetType().Name + ": " + e.Message;
                try { RemoveOwnPatches(); } catch { }
                return false;
            }
        }

        /// <summary>
        /// Removes every patch owned by the original mod. Returns true when no patch of the
        /// original remains afterwards.
        /// </summary>
        internal static bool NeutraliseOriginal(out string detail)
        {
            detail = null;
            try
            {
                if (_guardHarmony == null) _guardHarmony = new Harmony(ModRuntime.PatchHarmonyId + ".Guard");

                _guardHarmony.UnpatchAll(ModRuntime.OriginalHarmonyId);
                int swept = SweepOriginalPatchesByAssembly(_guardHarmony);

                bool stillPresent = PatchesOwnedBy(ModRuntime.OriginalHarmonyId);
                detail = "original patches removed" + (swept > 0 ? " (+" + swept + " by assembly sweep)" : string.Empty);
                return !stillPresent;
            }
            catch (Exception e)
            {
                detail = e.GetType().Name + ": " + e.Message;
                return false;
            }
        }

        /// <summary>True when any Harmony patch owned by the original mod id is still applied.</summary>
        internal static bool OriginalPatchesPresent()
        {
            try { return PatchesOwnedBy(ModRuntime.OriginalHarmonyId); }
            catch { return false; }
        }

        /// <summary>True when this mod's own patches are still applied.</summary>
        internal static bool OwnPatchesPresent()
        {
            try { return PatchesOwnedBy(ModRuntime.PatchHarmonyId); }
            catch { return false; }
        }

        /// <summary>Removes only this mod's own patches (used on disable / before a retry).</summary>
        internal static void RemoveOwnPatches()
        {
            try
            {
                if (_patchHarmony != null) _patchHarmony.UnpatchAll(ModRuntime.PatchHarmonyId);
            }
            catch (Exception e)
            {
                Log.Debug("Removing own patches failed: " + e.Message);
            }
        }

        internal static void Shutdown()
        {
            RemoveOwnPatches();
        }

        // --------------------------------------------------------------- internals

        private static bool PatchesOwnedBy(string ownerId)
        {
            if (string.IsNullOrEmpty(ownerId)) return false;
            foreach (MethodBase method in Harmony.GetAllPatchedMethods())
            {
                HarmonyLib.Patches info = Harmony.GetPatchInfo(method);
                if (info == null) continue;
                if (info.Owners != null && info.Owners.Contains(ownerId)) return true;
            }
            _ = _verifiedOnce; // keep the field meaningful for diagnostics builds
            return false;
        }

        /// <summary>
        /// Extra safety: unpatch anything whose patch method comes from the original mod's
        /// assembly, even if its Harmony owner id ever differs from "BetterAccessoryDisplay".
        /// </summary>
        private static int SweepOriginalPatchesByAssembly(Harmony harmony)
        {
            int removed = 0;
            try
            {
                List<MethodBase> methods = new List<MethodBase>(Harmony.GetAllPatchedMethods());
                for (int i = 0; i < methods.Count; i++)
                {
                    MethodBase method = methods[i];
                    HarmonyLib.Patches info;
                    try { info = Harmony.GetPatchInfo(method); }
                    catch { continue; }
                    if (info == null) continue;

                    removed += UnpatchMatching(harmony, method, info.Prefixes);
                    removed += UnpatchMatching(harmony, method, info.Postfixes);
                    removed += UnpatchMatching(harmony, method, info.Transpilers);
                    removed += UnpatchMatching(harmony, method, info.Finalizers);
                }
            }
            catch (Exception e)
            {
                Log.Debug("Assembly sweep for original patches failed: " + e.Message);
            }
            return removed;
        }

        private static int UnpatchMatching(Harmony harmony, MethodBase original, IList<Patch> patches)
        {
            if (patches == null || patches.Count == 0) return 0;
            int removed = 0;
            for (int i = 0; i < patches.Count; i++)
            {
                Patch patch = patches[i];
                if (patch == null || !IsOriginalPatch(patch)) continue;
                try
                {
                    MethodInfo patchMethod = patch.PatchMethod;
                    if (patchMethod == null) continue;
                    harmony.Unpatch(original, patchMethod);
                    removed++;
                }
                catch (Exception e)
                {
                    Log.Debug("Unpatching an original patch failed: " + e.Message);
                }
            }
            return removed;
        }

        private static bool IsOriginalPatch(Patch patch)
        {
            try
            {
                if (string.Equals(patch.owner, ModRuntime.OriginalHarmonyId, StringComparison.Ordinal)) return true;
                MethodInfo patchMethod = patch.PatchMethod;
                if (patchMethod == null) return false;
                Type declaring = patchMethod.DeclaringType;
                if (declaring == null) return false;
                Assembly asm = declaring.Assembly;
                if (asm == null) return false;
                string asmName = asm.GetName()?.Name;
                // Exact match only: this mod's assembly is "BetterAccessoryDisplayFixed".
                return string.Equals(asmName, ModRuntime.OriginalModName, StringComparison.Ordinal);
            }
            catch { return false; }
        }
    }
}
