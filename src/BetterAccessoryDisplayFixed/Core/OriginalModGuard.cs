using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Duckov.Modding;
using UnityEngine;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Hard dependency check + invisible neutralisation of the original mod.
    ///
    /// Hard constraint (user requirement): the original BetterAccessoryDisplay must stay
    /// installed AND enabled in the in-game mod list, without any popup and without touching
    /// its Workshop folder. So instead of disabling it we only remove ITS Harmony patches
    /// (<see cref="HarmonyBridge.NeutraliseOriginal"/>), which turns its refresh handler and all
    /// of its Debug.Log output into a no-op. The clean implementation in this mod then produces
    /// the exact same visuals.
    /// </summary>
    public static class OriginalModGuard
    {
        private static bool _missingLogged;

        // ------------------------------------------------------------ dependency

        /// <summary>True when the original mod is installed (enabled or not).</summary>
        public static bool IsOriginalModInstalled()
        {
            ModInfo info;
            return TryFindOriginalMod(out info);
        }

        /// <summary>Finds the original mod's info in the game's mod list, or null.</summary>
        public static bool TryFindOriginalMod(out ModInfo found)
        {
            found = default(ModInfo);
            try
            {
                List<ModInfo> infos = ModManager.modInfos;
                if (infos != null)
                {
                    for (int i = 0; i < infos.Count; i++)
                    {
                        ModInfo info = infos[i];
                        if (IsOriginalName(info.name) || info.publishedFileId == ModRuntime.OriginalModPublishedFileId)
                        {
                            found = info;
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Debug("Reading the mod list failed: " + e.Message);
            }

            // Fallback: scan the two places mods can live. This keeps the hard dependency working
            // even if the mod list was not populated yet when this mod starts.
            string folder = FindFolderWithModName(ModManager.DefaultModFolderPath, ModRuntime.OriginalModName);
            if (folder == null) folder = FindFolderWithModName(GuessWorkshopContentPath(), ModRuntime.OriginalModName);
            if (folder == null) return false;

            found = new ModInfo { path = folder, name = ModRuntime.OriginalModName, publishedFileId = ModRuntime.OriginalModPublishedFileId };
            return true;
        }

        private static bool IsOriginalName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return string.Equals(name.Trim(), ModRuntime.OriginalModName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Finds a mod folder under <paramref name="root"/> whose info.ini name matches.</summary>
        public static string FindFolderWithModName(string root, string modName)
        {
            try
            {
                if (string.IsNullOrEmpty(root) || !Directory.Exists(root)) return null;
                string[] dirs = Directory.GetDirectories(root);
                for (int i = 0; i < dirs.Length; i++)
                {
                    string ini = Path.Combine(dirs[i], "info.ini");
                    if (!File.Exists(ini)) continue;
                    if (InfoIniHasName(ini, modName)) return dirs[i];
                }
            }
            catch (Exception e)
            {
                Log.Debug("Mod folder scan failed: " + e.Message);
            }
            return null;
        }

        private static bool InfoIniHasName(string iniPath, string modName)
        {
            try
            {
                string[] lines = File.ReadAllLines(iniPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line == null) continue;
                    int eq = line.IndexOf('=');
                    if (eq < 1) continue;
                    string key = line.Substring(0, eq).Trim();
                    if (!string.Equals(key, "name", StringComparison.OrdinalIgnoreCase)) continue;
                    return string.Equals(line.Substring(eq + 1).Trim(), modName, StringComparison.OrdinalIgnoreCase);
                }
            }
            catch { }
            return false;
        }

        // ------------------------------------------------------------- game paths

        /// <summary>
        /// &lt;steamapps&gt;\workshop\content\3167020 derived from Application.dataPath.
        /// </summary>
        public static string GuessWorkshopContentPath()
        {
            try
            {
                // <steamapps>\common\<game>\Duckov_Data -> <steamapps>\workshop\content\3167020
                DirectoryInfo data = new DirectoryInfo(Application.dataPath);
                DirectoryInfo game = data.Parent;
                DirectoryInfo common = game?.Parent;
                DirectoryInfo steamapps = common?.Parent;
                if (steamapps == null) return null;
                return Path.Combine(steamapps.FullName, "workshop", "content", "3167020");
            }
            catch { return null; }
        }

        // ----------------------------------------------------------- report helpers

        /// <summary>One friendly line when the hard dependency is missing (no popup, no crash).</summary>
        public static void ReportMissingDependency()
        {
            if (_missingLogged) return;
            _missingLogged = true;
            Log.Warn("Hard dependency missing: the original mod '" + ModRuntime.OriginalModName +
                     "' (3596581923) is not installed. This fixed version stays completely inactive; " +
                     "the game itself is unaffected. Subscribe to the original mod and restart the game.");
        }
    }
}
