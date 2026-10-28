using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Duckov.Modding;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Makes sure the Harmony runtime (0Harmony.dll) is loaded before any HarmonyLib symbol is
    /// touched by the JIT.
    ///
    /// The mod has a compile-time reference to HarmonyLib (that is why HarmonyLoadMod is declared
    /// as a required item), but it must not depend on the exact activation order: a brand new
    /// subscriber may load this mod before HarmonyLoadMod. Therefore we proactively load
    /// 0Harmony.dll from the required mods' folders (HarmonyLoadMod, BetterAccessoryDisplay) or
    /// from any other installed mod folder that ships it.
    ///
    /// IMPORTANT: this class intentionally uses NO HarmonyLib types. Keep it that way, otherwise
    /// the JIT could try to resolve HarmonyLib before this loader had a chance to load it.
    /// </summary>
    public static class HarmonyLoader
    {
        private static bool _loaded;
        private static string _source = string.Empty;
        private static bool _loadErrorLogged;

        public static bool Loaded => _loaded;

        /// <summary>Where the Harmony runtime was found (diagnostics only; not logged by default).</summary>
        public static string Source => _source;

        /// <summary>True once HarmonyLib.Harmony is resolvable. Idempotent and cheap after success.</summary>
        public static bool EnsureLoaded()
        {
            if (_loaded) return true;

            try
            {
                if (FindHarmonyType() != null)
                {
                    _loaded = true;
                    _source = "already loaded";
                    return true;
                }

                List<string> candidates = CandidatePaths();
                for (int i = 0; i < candidates.Count; i++)
                {
                    string candidate = candidates[i];
                    if (string.IsNullOrEmpty(candidate) || !File.Exists(candidate)) continue;
                    try
                    {
                        Assembly asm = Assembly.LoadFrom(candidate);
                        if (asm != null && asm.GetType("HarmonyLib.Harmony", false) != null)
                        {
                            _loaded = true;
                            _source = candidate;
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug("Assembly.LoadFrom failed for '" + candidate + "': " + e.Message);
                    }

                    // Another mod may have loaded Harmony between our two checks.
                    if (FindHarmonyType() != null)
                    {
                        _loaded = true;
                        _source = "loaded by another mod";
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WarnOnce(ref _loadErrorLogged, "Harmony runtime detection failed: " + e.Message);
            }

            return false;
        }

        private static Type FindHarmonyType()
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                for (int i = 0; i < assemblies.Length; i++)
                {
                    try
                    {
                        Type type = assemblies[i].GetType("HarmonyLib.Harmony", false);
                        if (type != null) return type;
                    }
                    catch { /* dynamic / broken assemblies */ }
                }
            }
            catch { }
            return null;
        }

        /// <summary>Candidate 0Harmony.dll locations, required loaders first.</summary>
        private static List<string> CandidatePaths()
        {
            List<string> result = new List<string>();

            // 1) mods from the game's mod list (HarmonyLoadMod first, then the original mod)
            try
            {
                List<ModInfo> infos = ModManager.modInfos;
                if (infos != null)
                {
                    AddIfExists(result, infos, ModRuntime.HarmonyModName);
                    AddIfExists(result, infos, ModRuntime.OriginalModName);
                }
            }
            catch (Exception e) { Log.Debug("Scanning mod list for 0Harmony failed: " + e.Message); }

            // 2) local mods folder
            try
            {
                string local = ModManager.DefaultModFolderPath;
                if (!string.IsNullOrEmpty(local) && Directory.Exists(local))
                {
                    string[] dirs = Directory.GetDirectories(local);
                    for (int i = 0; i < dirs.Length; i++) AddDll(result, dirs[i]);
                }
            }
            catch (Exception e) { Log.Debug("Scanning local mods for 0Harmony failed: " + e.Message); }

            // 3) workshop content folder
            try
            {
                string root = OriginalModGuard.GuessWorkshopContentPath();
                if (!string.IsNullOrEmpty(root) && Directory.Exists(root))
                {
                    string[] dirs = Directory.GetDirectories(root);
                    for (int i = 0; i < dirs.Length; i++) AddDll(result, dirs[i]);
                }
            }
            catch (Exception e) { Log.Debug("Scanning Workshop content for 0Harmony failed: " + e.Message); }

            return result;
        }

        private static void AddIfExists(List<string> result, List<ModInfo> infos, string modName)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                ModInfo info = infos[i];
                if (info.name == null || !string.Equals(info.name.Trim(), modName, StringComparison.OrdinalIgnoreCase)) continue;
                AddDll(result, info.path);
            }
        }

        private static void AddDll(List<string> result, string folder)
        {
            try
            {
                if (string.IsNullOrEmpty(folder)) return;
                string dll = Path.Combine(folder, "0Harmony.dll");
                if (File.Exists(dll) && !result.Contains(dll)) result.Add(dll);
            }
            catch { }
        }
    }
}
