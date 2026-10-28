# In-game verification — Better Accessory Display (Fixed) v1.0.0

Concise public summary. Detailed logs and the full internal test report stay in the author's
local test workspace and are not published.

## Environment

- Game: **Escape from Duckov 2.3.30** (Steam appid 3167020, Unity 2022.3.62f2), Windows, Simplified Chinese.
- Build: `BetterAccessoryDisplayFixed.dll` v1.0.0 (23,552 bytes, SHA-256
  `EDABDD0BC91F72CC422D506ED780752D878C54BF0FA4729A6EE3BF262E88BC13`).
- Required mods: HarmonyLoadMod (3589088839) + original BetterAccessoryDisplay (3596581923),
  both installed and enabled; the fix neutralises the original's Harmony patches at startup.

## Sessions

| Session | Length | Result |
|---|---|---|
| Main test | 32 min 14 s | Full gameplay: stash/inventory, shop, looting, scene changes. |
| Restart check | 10 min 52 s | Restart with the same load order; verified the fix neutralises the original again. |
| Post-publish check | ~1 min | Workshop copy downloaded by Steam, loaded from the Workshop folder after removing the local test copy. |

## Log metrics

| Metric | Before the fix (measured baseline) | With the fix (main test session) |
|---|---|---|
| Lines written by the original mod | 743,761 lines / 55.2 MB (87.33 % of lines, 90.56 % of log size) | **0 lines per refresh** |
| Lines written by this fix | — | **1 startup line** |
| Total `Player.log` | 851,701 lines / 58.13 MB | **21,215 lines / 1.12 MB** |
| Errors caused by this fix | — | **0** |

## Visual checks

- Guns in the inventory, equipment slots, stash, shop and in-raid screens show six attachment dots:
  white = attachment installed, gray = slot the weapon does not have, dark = real empty slot.
- Non-gun items keep the game's native indicators.
- Behaviour is unchanged across scene changes and game restarts, and matches the original v1.1.

## Other notes

- Remaining exceptions in the log come from other installed mods (pre-existing; the same
  signatures appear in logs from before this mod was installed) and are not caused by this fix.
- Not covered in this release: uninstalling the original mod, disabling this fix in-game, and
  plug/unplug A/B screenshots. These do not affect the core result above.
