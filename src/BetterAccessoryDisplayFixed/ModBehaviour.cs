using System;
using BetterAccessoryDisplayFixed.Core;
using Duckov.Modding;
using UnityEngine;

namespace BetterAccessoryDisplayFixed
{
    /// <summary>
    /// Entry point of "更好的配件展示 · 修复版" (Better Accessory Display - Fixed).
    ///
    /// Lifecycle:
    ///  1. hard dependency check (original BetterAccessoryDisplay installed) - otherwise inert;
    ///  2. make sure the Harmony runtime is loaded (HarmonyLoadMod is a required item, but we do
    ///     not depend on activation order; 0Harmony.dll is loaded from the required mods' folders);
    ///  3. apply this mod's own clean patches;
    ///  4. neutralise the original mod's patches (it stays enabled in the mod list, its refresh
    ///     handler and Debug.Log spam simply never run again);
    ///  5. watch the first seconds / reactivation events for the original mod re-patching itself.
    ///
    /// Normal startup logs exactly ONE line. No polling after the first ten seconds.
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private const float WatchdogInterval = 0.5f;
        private const int WatchdogTicks = 20; // 10 seconds of cheap retries at startup

        private bool _started;
        private bool _dependencySatisfied;
        private bool _patchesInstalled;
        private bool _neutralised;
        private bool _readyLogged;
        private bool _failedLogged;
        private bool _harmonyMissingLogged;
        private bool _neutraliseFailedLogged;
        private bool _subscribed;
        private float _nextWatchdog;
        private int _watchdogTicks;

        private void Awake()
        {
            // Load the Harmony runtime as early as possible: the methods below that reference
            // HarmonyLib must never be JIT-compiled before 0Harmony is loaded (HarmonyLoadMod is
            // a required item, but we do not rely on activation order).
            try { HarmonyLoader.EnsureLoaded(); }
            catch (Exception e) { Log.Debug("Harmony preload in Awake failed: " + e.Message); }
        }

        private void OnEnable()
        {
            if (_started) return;
            _started = true;
            // Same safety net as in Awake (in case Awake was skipped by an unusual load path).
            try { HarmonyLoader.EnsureLoaded(); }
            catch (Exception e) { Log.Debug("Harmony preload in OnEnable failed: " + e.Message); }
            TryActivate();
        }

        protected override void OnAfterSetup()
        {
            // ModManager calls Setup() right after AddComponent (OnEnable already ran), but call
            // again defensively in case Unity's enable order differs on some game versions.
            if (!_started) _started = true;
            if (!_patchesInstalled || !_neutralised) TryActivate();
        }

        private void Update()
        {
            if (!_started) return;
            float now = Time.unscaledTime;
            if (now < _nextWatchdog) return;
            _nextWatchdog = now + WatchdogInterval;
            if (_watchdogTicks >= WatchdogTicks) return;
            _watchdogTicks++;

            try
            {
                if (!_patchesInstalled || !_neutralised)
                {
                    TryActivate();
                    return;
                }

                // Safety net: if the original mod was re-activated (or re-patched) after us,
                // remove its patches again. Cheap; stops after WatchdogTicks.
                if (HarmonyBridge.OriginalPatchesPresent()) Neutralise();
            }
            catch (Exception e)
            {
                Log.Debug("Watchdog tick failed: " + e.Message);
            }
        }

        private void OnDisable()
        {
            if (!_started) return;
            _started = false;
            try { Unsubscribe(); } catch { }
            try { HarmonyBridge.Shutdown(); } catch { }
            // The original mod stays neutralised until the game restarts: we do not re-apply its
            // patches here because doing so during shutdown / mod-toggle is error-prone. Restart
            // the game (or toggle the original mod) to get the original behaviour back.
        }

        // -------------------------------------------------------------- activation

        private void TryActivate()
        {
            try
            {
                if (!_dependencySatisfied)
                {
                    if (!OriginalModGuard.IsOriginalModInstalled())
                    {
                        OriginalModGuard.ReportMissingDependency();
                        return;
                    }
                    _dependencySatisfied = true;
                }

                if (!Access.MembersAvailable) return;

                if (!_patchesInstalled)
                {
                    if (!HarmonyLoader.EnsureLoaded())
                    {
                        Log.WarnOnce(ref _harmonyMissingLogged,
                            "Harmony runtime (0Harmony.dll) not found. Install HarmonyLoadMod (3589088839) " +
                            "and restart the game; this fixed version stays inactive for now.");
                        return;
                    }

                    string installError;
                    if (!HarmonyBridge.Install(out installError))
                    {
                        Log.ErrorOnce(ref _failedLogged, "Applying the fixed display patches failed: " + installError);
                        return;
                    }
                    _patchesInstalled = true;
                }

                if (!_neutralised)
                {
                    Neutralise();
                    if (!_neutralised) return;
                }

                Subscribe();

                if (!_readyLogged)
                {
                    _readyLogged = true;
                    Log.Info("v" + ModRuntime.ModVersion + " ready (game " + Application.version +
                             ") - original " + ModRuntime.OriginalModName +
                             " patches neutralised; 6-slot display active.");
                }
            }
            catch (Exception e)
            {
                Log.ErrorOnce(ref _failedLogged, "Activation failed: " + e);
            }
        }

        private void Neutralise()
        {
            try
            {
                string detail;
                if (HarmonyBridge.NeutraliseOriginal(out detail))
                {
                    _neutralised = true;
                }
                else
                {
                    Log.WarnOnce(ref _neutraliseFailedLogged,
                        "Could not remove the original mod's Harmony patches (" + detail + "). " +
                        "The original may keep logging until the game is restarted.");
                }
            }
            catch (Exception e)
            {
                Log.WarnOnce(ref _neutraliseFailedLogged, "Neutralising the original mod failed: " + e.Message);
            }
        }

        // ---------------------------------------------------------------- events

        private void Subscribe()
        {
            if (_subscribed) return;
            ModManager.OnModActivated += OnModActivated;
            _subscribed = true;
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;
            ModManager.OnModActivated -= OnModActivated;
            _subscribed = false;
        }

        private void OnModActivated(ModInfo info, Duckov.Modding.ModBehaviour behaviour)
        {
            try
            {
                if (info.name == null) return;
                if (!string.Equals(info.name.Trim(), ModRuntime.OriginalModName, StringComparison.OrdinalIgnoreCase)) return;
                if (!_patchesInstalled) return;
                // The original was just (re-)activated and has applied its own patches in OnEnable.
                Neutralise();
            }
            catch (Exception e)
            {
                Log.Debug("OnModActivated handler failed: " + e.Message);
            }
        }
    }
}
