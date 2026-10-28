# Better Accessory Display (Fixed)

[English](README.md) | [简体中文](README.zh-CN.md)

An **independent fixed version** of the abandoned *Better Accessory Display* mod for
**Escape from Duckov** (Steam appid 3167020, game 2.3.30).

It keeps the original mod's six-slot attachment display and throws away its debug logging:
the original writes ~48 `Debug.Log` lines on **every** slot refresh (hard-coded, no switch),
this version writes exactly **one startup line** and nothing per refresh.

> The original mod stays installed **and enabled**. This mod removes only its Harmony patches
> at runtime and re-implements the same visuals with clean code. The original's files are never
> copied, modified or bundled — its credit and Workshop downloads stay with its author.

## Highlights

- **Same visuals as the original v1.1** — every gun shows six attachment slots in the order
  `Scope / Muzzle / Grip / Stock / Tec / Mag`; slots with an attachment get a **white** indicator,
  slots the weapon does not have are shown **gray**, real empty slots keep the game's native look,
  and uninspected weapons keep their slots hidden.
- **Zero log spam** — measured on the author's install: the original produced **743,761 lines /
  55.2 MB** in a 3 h 23 min session (87.33 % of all lines, 90.56 % of the log size). After the fix,
  there are **0 log lines per refresh**, plus 1 startup line from this mod.
  The whole log went from 851,701 lines / 58.13 MB to 21,215 lines / 1.12 MB (same install).
- **Cheaper hot path** — the `ItemDisplay.slotIndicatorContainer`, `ItemDisplay.SlotIndicatorPool`
  and `SlotIndicator.contentIndicator` reflection members are resolved once and cached statically.
- **On-demand rebuild** — the slot indicators are rebuilt only when actually needed
  (once per `ItemDisplay.Setup`), not on every `Refresh` like the original.
- **Fail-safe** — if the game changes its internal UI members in a future patch, the mod stays
  inactive and prints a single warning instead of crashing or spamming.

## Requirements (both are Workshop "Required items")

| Required item | Workshop ID | Notes |
|---|---|---|
| [HarmonyLoadMod](https://steamcommunity.com/sharedfiles/filedetails/?id=3589088839) | 3589088839 | Provides the Harmony runtime (`0Harmony.dll`) |
| [BetterAccessoryDisplay](https://steamcommunity.com/sharedfiles/filedetails/?id=3596581923) | 3596581923 | The original mod; must stay **installed and enabled** |

Neither prerequisite is bundled in this repository or in the released mod package.
Load order: `HarmonyLoadMod → BetterAccessoryDisplay → BetterAccessoryDisplayFixed`.
A freshly installed mod loads last by default, so usually there is nothing to do.

If the original mod is missing, the fixed version stays completely inactive and prints one
friendly warning (no popup, no crash).

## Install

1. Subscribe to the mod on the Steam Workshop and to both required items.
2. Enable all three in the game's **Mods** list (the original must stay enabled).
3. Restart the game. Load this fixed version after the two required items.

Steam Workshop: <https://steamcommunity.com/sharedfiles/filedetails/?id=3828601227>

## Build

Requirements: .NET SDK 8/9/10, an installed copy of Escape from Duckov, and an installed
HarmonyLoadMod or BetterAccessoryDisplay (which ships `0Harmony.dll`).

```powershell
# build + verify (the 0Harmony reference is compile-time only, never copied to the output)
.\build-badfixed.ps1

# build + install as a local mod (Duckov_Data\Mods\BetterAccessoryDisplayFixed)
.\build-badfixed.ps1 -DeployLocal

# explicit paths
.\build-badfixed.ps1 -GamePath "D:\Steam\steamapps\common\Escape from Duckov" -HarmonyDll "C:\...\0Harmony.dll"
```

`verify-badfixed.ps1` checks the entry point, its base type, that only game/Unity/System
assemblies plus the compile-time `0Harmony` reference are used, and that no prerequisite DLL
or original-mod file ended up in the package.

## Project layout

```
build-badfixed.ps1        build / package / verify
deploy-badfixed.ps1       install as a local mod (or replace a Workshop copy, with backup)
verify-badfixed.ps1       static verification of the produced DLL and package
src/BetterAccessoryDisplayFixed/
  BetterAccessoryDisplayFixed.csproj
  info.ini                Workshop metadata (name/displayName/description/version/tags)
  ModBehaviour.cs         entry point: dependency check → Harmony runtime → own patches → neutralise original
  Core/                   reflection cache, Harmony bridge, original-mod guard, slot rebuild/colour logic
  Patches/                the three Harmony patches (ItemDisplay.Setup/Refresh, SlotIndicator.Refresh)
docs/RELEASE_NOTES.md     release notes (CN + EN summary)
docs/TEST_REPORT.md       build/static/offline + in-game test report with measured numbers
docs/WORKSHOP_DESCRIPTION.md       English Workshop description
docs/workshop-localizations.md     10-language Workshop titles/descriptions
docs/workshop-page-preview.png     mock-up of the Workshop page (layout preview)
assets/preview.png        Workshop cover (original cover + "fixed" badge)
LICENSE                   MIT
NOTICE.md                 original mod attribution and licence
```

## Testing

- Built with .NET SDK 10.0.302, `Release`, 0 warnings / 0 errors; static verification passed.
- Verified in-game on game 2.3.30: a full gameplay session plus a restart check, and a final
  check of the Workshop copy after publishing.
- Result: the original mod stays enabled and is neutralised at runtime, **0 per-refresh log lines**,
  this fix logs 1 startup line and raises no errors; visuals match the original v1.1
  (see `docs/TEST_REPORT.md`).

## Credits & licence

- Original mod **“更好的配件展示 / Better Accessory Display”** by **我是火柴人 / chardexn** —
  <https://github.com/Chardexn/BetterAccessoryDisplay>, MIT licensed.
- This fixed version is an **independent re-implementation**, not a release by the original author.
  It contains none of the original files. All credit and Workshop download counts for the original
  belong to its author.
- The cover image is the original Workshop cover with a “修复版 / FIXED” badge added, per the
  fixed version author's request; credit for the base image goes to the original author.
- Fixed version by **Dorei Hime**, released under the MIT licence (see `LICENSE` and `NOTICE.md`).
