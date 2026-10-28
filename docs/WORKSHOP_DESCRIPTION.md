[h1]Better Accessory Display · Fixed[/h1]
[b]Independent fixed version of Better Accessory Display: same six slots, minus unstoppable log spam. Original stays installed and enabled.[/b]

[h2]◆ The problem[/h2]
[list][*]DEBUG logging is hard-coded and cannot be disabled: ~48 lines per slot refresh.
[*]3 h 23 min: 484,156 lines / 34.75 MB (85.68% of log lines, 89.19% of size) — huge log, constant writes, hard diagnosis.[/list]

[h2]◆ What's fixed[/h2]
[list][*]0 spam lines while playing: no repeated Player.log writes.
[*]Rebuilt only when needed instead of all six every Refresh; reflection cached.
[*]Same visuals: six slots, white dot = installed, gray dot = missing, uninspected hidden.[/list]

[h2]◆ Requirements[/h2]
[list][*]HarmonyLoadMod (3589088839): the Harmony runtime.
[*]Original BetterAccessoryDisplay (3596581923): keep enabled; this mod takes over its display.
[*]No bundled files; load after both.[/list]

[h2]◆ Changelog[/h2]
[list][*]1.0.0 — first release: independent implementation, same visuals, no log spam.[/list]

[h2]◆ Credits and licence[/h2]
[list][*]Original: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Fixed version: Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Independent fix; no original files; credits/downloads stay with the original author.[/list]
