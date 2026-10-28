[h1]Better Accessory Display · Fixed[/h1]
[b]An independent fixed version of the original Better Accessory Display: the same six-slot visuals, without the log spam. The original mod stays installed and enabled.[/b]

The original "Better Accessory Display" (更好的配件展示, Workshop 3596581923) forces every weapon in Escape from Duckov to show all six attachment slots, which makes comparing and swapping attachments much easier. The feature itself works — the problem is its debug output: it is hard-coded on, and every slot refresh writes about 48 lines to Player.log. One measured 3 h 23 min session produced 484,156 lines / 34.75 MB — 85.68% of all log lines and 89.19% of the log size — peaking at roughly 216 lines per second. 172,327 of those lines came from a dead path ("SlotIndicator.Target is null"): noise nobody needs.

This mod keeps exactly the same look and re-implements the feature with clean code. It is an independent fix, not a release by the original author.

[h2]◆ Requirements (both are required items)[/h2]
[list][*]HarmonyLoadMod (3589088839) — provides the Harmony runtime (0Harmony.dll).
[*]The original BetterAccessoryDisplay (3596581923) — must stay installed and enabled.
[*]Neither is bundled with this mod. The files, credit and download count of the original stay with its author.
[*]The original must stay enabled: this mod removes its Harmony patches at startup and takes over the visuals itself. Seeing the original still enabled in the Mods list is intentional.[/list]

[h2]◆ Load order[/h2]
[list][*]Load this mod after HarmonyLoadMod and after the original BetterAccessoryDisplay. A freshly installed mod loads last by default, so usually there is nothing to do.
[*]If you sort mods manually, keep both required mods above this one. (As a safety net, this mod also loads 0Harmony.dll by itself when needed.)[/list]

[h2]◆ What you see[/h2]
[list][*]Every weapon shows all six attachment slots: Scope / Muzzle / Grip / Stock / Tec / Mag.
[*]A slot with an attachment installed shows a white content indicator.
[*]Slots the weapon does not have are still shown, marked gray.
[*]Weapons you have not inspected yet keep their slots hidden, same as the original's current behaviour.
[*]No settings and no extra UI: subscribe, enable, restart the game and you are done.[/list]

[h2]◆ What this fix changes[/h2]
[list][*]Logging is hard-off: one startup line from this mod, nothing per refresh. (The original's own 6 one-time lines — 3 at startup, 3 at shutdown — remain because it stays enabled; they cannot be removed from this side.)
[*]Reflection members that the original looked up on every refresh are resolved once and cached statically.
[*]Slot indicators are rebuilt only when they actually need to be, instead of releasing and recreating all six on every Refresh.
[*]If a future game update changes the internal members, the mod stays inactive and prints a single warning — no spam, no crash.[/list]

[h2]◆ Compatibility[/h2]
[list][*]Game version: Escape from Duckov 2.3.30. Other versions are untested.
[*]Everything happens at runtime: no game files, assets or save data are modified.
[*]Tested on 2026-10-28 in the author's own setup with all 72 mods enabled, 32-minute session: this fixed version logged only 1 startup line, the original mod produced 0 per-refresh lines, and no errors came from this mod. That run also covered coexistence with FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod and CustomItemLevelValue; for mods not A/B-tested individually, the actual in-game results are the reference.
[*]Safe to disable or remove: this mod only removes its own patches. See "Known limitations" for the original's state.[/list]

[h2]◆ Known limitations[/h2]
[list][*]While the original is enabled, its own 6 one-time lines (3 at startup, 3 at shutdown) still appear in Player.log; everything else from it is silenced.
[*]Disabling this mod inside the game does not restore the original's patches automatically; restart the game to get the original behaviour back.
[*]This mod only affects the display and changes no gameplay values.[/list]

[h2]◆ Changelog[/h2]
[list][*]1.0.0 — first public release: independent clean implementation; 6-slot visuals kept; debug logging off (one startup line); reflection members cached; slots rebuilt only when needed.[/list]

[h2]◆ Credits and licence[/h2]
[list][*]Original mod "更好的配件展示 / Better Accessory Display" by 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]The original is released under the MIT licence. This fixed version is independent: it is not published by the original author and contains none of the original files. All credit and download counts for the original belong to its author.
[*]Fixed version by Dorei Hime, released under the MIT licence.[/list]
