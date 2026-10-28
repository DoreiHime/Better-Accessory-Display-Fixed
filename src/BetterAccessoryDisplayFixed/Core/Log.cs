using System;
using UnityEngine;

namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>
    /// Logging helper. The whole point of this mod is a quiet Player.log, therefore:
    ///  * exactly one Info line on a successful start;
    ///  * one Warn line when a hard dependency is missing;
    ///  * Error lines only for real failures (logged once, never per frame / per refresh);
    ///  * Debug() is compiled behind a hard-off switch (always false in shipped builds).
    /// </summary>
    public static class Log
    {
        private const string Prefix = "[BetterAccessoryDisplayFixed] ";

        /// <summary>Always false in shipped builds; change only for a local debugging build.</summary>
        public static readonly bool Verbose = false;

        public static void Info(string message)
        {
            UnityEngine.Debug.Log(Prefix + message);
        }

        public static void Warn(string message)
        {
            UnityEngine.Debug.LogWarning(Prefix + message);
        }

        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(Prefix + message);
        }

        public static void Error(string message, Exception exception)
        {
            UnityEngine.Debug.LogError(Prefix + message + "\n" + exception);
        }

        public static void Debug(string message)
        {
            if (Verbose) UnityEngine.Debug.Log(Prefix + message);
        }

        /// <summary>Logs a warning only the first time the given flag is false, then stays silent.</summary>
        public static void WarnOnce(ref bool flag, string message)
        {
            if (flag) return;
            flag = true;
            UnityEngine.Debug.LogWarning(Prefix + message);
        }

        /// <summary>Logs an error only the first time the given flag is false, then stays silent.</summary>
        public static void ErrorOnce(ref bool flag, string message)
        {
            if (flag) return;
            flag = true;
            UnityEngine.Debug.LogError(Prefix + message);
        }
    }
}
