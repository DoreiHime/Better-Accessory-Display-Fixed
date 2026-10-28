namespace BetterAccessoryDisplayFixed.Core
{
    /// <summary>Stable identifiers shared by the mod, the guard, the docs and the publish tooling.</summary>
    public static class ModRuntime
    {
        /// <summary>Mod folder name and entry type prefix (must match info.ini "name").</summary>
        public const string ModName = "BetterAccessoryDisplayFixed";
        public const string ModVersion = "1.0.0";
        public const string Author = "Dorei_Hime";

        /// <summary>Harmony owner id used by this mod's own (clean) patches.</summary>
        public const string PatchHarmonyId = "BetterAccessoryDisplayFixed";

        /// <summary>Harmony owner id used by the original mod (see its ModBehaviour.OnEnable).</summary>
        public const string OriginalHarmonyId = "BetterAccessoryDisplay";

        /// <summary>The original mod this one is a fix for (hard dependency, must stay enabled).</summary>
        public const string OriginalModName = "BetterAccessoryDisplay";
        public const ulong OriginalModPublishedFileId = 3596581923UL;

        /// <summary>Harmony runtime loader; second hard dependency (ships 0Harmony.dll).</summary>
        public const string HarmonyModName = "HarmonyLoadMod";
        public const ulong HarmonyModPublishedFileId = 3589088839UL;
    }
}
