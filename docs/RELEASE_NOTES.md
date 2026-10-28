# Release notes — Better Accessory Display (Fixed) v1.0.0

An independent fixed version of **Better Accessory Display** for
**Escape from Duckov** (Steam appid 3167020, game 2.3.30).

## What this is

The original **Better Accessory Display** forces all weapons to show their six attachment
slots. The feature is useful, but its debug output is hard-coded on: every slot refresh writes
about 48 lines to `Player.log`. One measured 3 h 23 min session produced **484,156 lines /
34.75 MB — about 85.68 % of all log lines and 89.19 % of the log size**.

This version keeps the same six-slot visuals and removes that logging churn:

- **0 log lines while playing** — no more repeated writes to `Player.log`;
- slot indicators are **rebuilt only when needed** (not on every refresh);
- reflection members are resolved **once and cached**;
- visuals are unchanged: six slots (white dot = attachment installed, gray dot = slot the weapon
  does not have), uninspected weapons still show no slots.

## Requirements

| Required item | Workshop ID |
|---|---|
| HarmonyLoadMod (Harmony runtime) | [3589088839](https://steamcommunity.com/sharedfiles/filedetails/?id=3589088839) |
| BetterAccessoryDisplay (the original mod) | [3596581923](https://steamcommunity.com/sharedfiles/filedetails/?id=3596581923) |

The original must stay installed **and enabled**; this fix takes over its display logic at
runtime. Neither prerequisite is bundled in this repository or in the released mod package.
Load this mod after both required mods.

## Compatibility

- Game 2.3.30 (verified in-game).
- Everything happens at runtime: no game files, assets or save data are modified.
- Disabling the fix inside the game requires a game restart to restore the original behaviour.
- No frame-rate gain is claimed — this release only removes the logging churn.

## Links

- Steam Workshop: <https://steamcommunity.com/sharedfiles/filedetails/?id=3828601227>
- Original mod: <https://github.com/Chardexn/BetterAccessoryDisplay> (MIT, by 我是火柴人 / chardexn)
- This fixed version: <https://github.com/DoreiHime/Better-Accessory-Display-Fixed> (MIT)

---

## 中文摘要

「更好的配件展示」的独立修复版：保留原版 6 个配件槽的显示效果，去掉写死的调试日志刷屏
（原版一次 3 小时 23 分钟会话实测 484,156 行 / 34.75 MB，约占日志 85.68% 行数、89.19% 体积）。
修复版运行期 0 行刷屏、槽位按需重建、反射成员缓存一次；视觉与原版 v1.1 一致。

前置：HarmonyLoadMod（3589088839）+ 原版 BetterAccessoryDisplay（3596581923，必须保持启用）；
本 mod 不含前置文件，加载顺序排在两者之后。MIT 许可；独立修复版，非原作者发布。
