# Workshop 文案（10 语种）— Better Accessory Display (Fixed) v1.0.0

> 游戏支持的 10 种语言。上传工具（`work/uploader-new/SteamLocalize`）按「单独一行 `locale tag: 语言` + 随后的代码围栏」解析；围栏内第一段 [h1] 会作为工坊标题单独提取，其余全部作为描述。
> 用法：`SteamLocalize <publishedFileId> docs/BetterAccessoryDisplayFixed-workshop-localizations.md`
> 排版约定：标题后不留空行；[list] 内不留空行；块内只使用 [h1][h2][list][*][b][i][hr]，不支持 [center]。

## English (en)

locale tag: en

```
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

[h2]◆ What's fixed · What you get[/h2]
[list][*]Log spam is gone: the original wrote about 48 debug lines on every slot refresh — one measured 3 h 23 min session reached 484,156 lines / 34.75 MB, 85.68% of all log lines and 89.19% of the log size. This version writes 0 lines while playing, and the original itself now only contributes its 6 one-time lines (3 at startup + 3 at shutdown). Browsing the backpack, storage or shops no longer keeps writing to disk, so Player.log stays small and much easier to diagnose.
[*]Lighter refreshes: the original cleared and rebuilt all six slot indicators on every Refresh. This version rebuilds only when it actually has to, and reflection members are looked up once at startup and cached — the repeated work is gone.
[*]Same visuals: every weapon still shows six attachment slots (white dot = attachment installed, gray dot = a slot the weapon does not have), and uninspected weapons still show no slots — identical to the original v1.1.
[*]A safety net: if a future game update changes the internal members, this mod simply stays inactive and logs one warning — no spam, no crash.[/list]

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
```

## 简体中文 (zh-CN)

locale tag: zh-CN

```
[h1]更好的配件展示 · 修复版[/h1]
[b]原版「更好的配件展示」的独立修复版：保留同样的 6 槽视觉效果，去掉日志刷屏。原版保持安装并启用。[/b]

原版「更好的配件展示」（Better Accessory Display，工坊 3596581923）会把游戏里所有武器强制显示为 6 个配件槽，方便对比和更换配件。功能本身很好用，问题出在它的调试日志：日志是写死的、关不掉，每次槽位刷新都会往 Player.log 里写约 48 行。实测过一次 3 小时 23 分钟的会话：共 484,156 行 / 34.75 MB，占全部日志行数的 85.68%、体积的 89.19%，峰值约 216 行/秒；其中 172,327 行来自「SlotIndicator.Target 为 null」的无效路径，是纯粹没人需要的噪音。

本 mod 保留完全一样的视觉效果，用干净的代码重新实现。它是独立修复版，不是原作者发布的。

[h2]◆ 前置要求（两个都是必需物品）[/h2]
[list][*]HarmonyLoadMod（3589088839）：提供 Harmony 运行时（0Harmony.dll）。
[*]原版 BetterAccessoryDisplay（3596581923）：必须保持安装并启用。
[*]两个前置都不包含在本 mod 里；原版的文件、署名和下载量都归原作者。
[*]原版必须保持启用：本 mod 会在启动时移除它的 Harmony 补丁，再由自己接管画面。你在 Mods 列表里看到原版仍然是启用的，这是刻意的。[/list]

[h2]◆ 加载顺序[/h2]
[list][*]请让本 mod 排在 HarmonyLoadMod 和原版 BetterAccessoryDisplay 之后。新装的 mod 默认排在最后，通常不需要手动调整。
[*]如果手动排序，把两个前置放在本 mod 上面即可。（本 mod 需要时也会自己加载 0Harmony.dll，作为兜底。）[/list]

[h2]◆ 修复了什么 · 你能得到什么[/h2]
[list][*]日志刷屏归零：原版每次槽位刷新都写约 48 行调试日志——实测一次 3 小时 23 分钟的会话共 484,156 行 / 34.75 MB，占全部日志行数的 85.68%、体积的 89.19%。本版运行期 0 行，原版自身也只剩 3 行启动 + 3 行退出共 6 行一次性日志；反复翻背包、仓库、商店不再持续写盘，Player.log 更小，排查问题也轻松多了。
[*]刷新更轻：原版每次 Refresh 都清空并重建全部 6 个槽位；本版只在真正需要时重建一次，反射成员也改为启动时查找一次并缓存，减少重复开销。
[*]视觉不变：所有武器仍显示 6 个配件槽（白点 = 已装，灰点 = 武器没有的槽），未搜索武器不强制显示——与原版 v1.1 一致。
[*]有兜底：将来游戏更新改掉内部成员时，本 mod 会保持不生效并只打 1 行警告，不刷屏、不崩溃。[/list]

[h2]◆ 兼容性[/h2]
[list][*]游戏版本：逃离鸭科夫 2.3.30；其他版本未测试。
[*]只在运行时生效：不改游戏文件、资源或存档。
[*]2026-10-28 在作者本人 72 mod 全开环境下实测 32 分钟：本修复版仅 1 行启动日志、原版逐刷新日志 0 条、无本 mod 相关报错。该环境同时包含 FixSlotDisplay、PluggableIndicator、ItemLevelAndSearchSoundMod、CustomItemLevelValue；未单独做 A/B 的共存项以实际表现为准。
[*]可以安全禁用或卸载：本 mod 只会移除自己的补丁。原版的状态见下方「已知限制」。[/list]

[h2]◆ 已知限制[/h2]
[list][*]原版保持启用时，它自己的一次性启动/退出日志共 6 行（3 行启动 + 3 行退出）仍会出现在 Player.log；除此之外的输出已经被静默。
[*]在游戏内禁用本 mod 后，原版的补丁不会自动恢复；需要重启游戏才能回到原版行为。
[*]本 mod 只影响界面显示，不改动任何游戏数值或玩法。[/list]

[h2]◆ 更新日志[/h2]
[list][*]1.0.0 — 首个公开版本：独立干净实现，保留 6 槽视觉；调试日志关闭（仅 1 行启动日志）；反射成员静态缓存；槽位按需重建。[/list]

[h2]◆ 致谢与许可[/h2]
[list][*]原版「更好的配件展示 / Better Accessory Display」作者：我是火柴人 / chardexn —— https://github.com/Chardexn/BetterAccessoryDisplay
[*]原版以 MIT 许可发布。本 mod 是独立修复版：非原作者发布，不包含任何原版文件；原版的署名与下载量归原作者所有。
[*]修复版作者：Dorei Hime，同样以 MIT 许可发布。[/list]
```

## 繁體中文 (zh-TW)

locale tag: zh-TW

```
[h1]更好的配件展示 · 修正版[/h1]
[b]原版「更好的配件展示」的獨立修正版：保留同樣的 6 槽視覺效果，去掉日誌刷屏。原版保持安裝並啟用。[/b]

原版「更好的配件展示」（Better Accessory Display，工坊 3596581923）會把遊戲裡所有武器強制顯示為 6 個配件槽，方便對比和更換配件。功能本身很好用，問題出在它的除錯日誌：日誌是寫死的、關不掉，每次槽位重新整理都會往 Player.log 裡寫約 48 行。實測過一次 3 小時 23 分鐘的工作階段：共 484,156 行 / 34.75 MB，佔全部日誌行數的 85.68%、體積的 89.19%，峰值約 216 行/秒；其中 172,327 行來自「SlotIndicator.Target 為 null」的無效路徑，是純粹沒人需要的雜訊。

本模組保留完全一樣的視覺效果，用乾淨的程式碼重新實作。它是獨立修正版，不是原作者發布的。

[h2]◆ 前置要求（兩個都是必要項目）[/h2]
[list][*]HarmonyLoadMod（3589088839）：提供 Harmony 執行環境（0Harmony.dll）。
[*]原版 BetterAccessoryDisplay（3596581923）：必須保持安裝並啟用。
[*]兩個前置都不包含在本模組裡；原版的檔案、署名和下載量都歸原作者。
[*]原版必須保持啟用：本模組會在啟動時移除它的 Harmony 補丁，再由自己接管畫面。你在 Mods 列表裡看到原版仍然是啟用的，這是刻意的。[/list]

[h2]◆ 載入順序[/h2]
[list][*]請讓本模組排在 HarmonyLoadMod 和原版 BetterAccessoryDisplay 之後。新裝的模組預設排在最後，通常不需要手動調整。
[*]如果手動排序，把兩個前置放在本模組上面即可。（本模組需要時也會自己載入 0Harmony.dll，作為保險。）[/list]

[h2]◆ 修正了什麼 · 你能得到什麼[/h2]
[list][*]日誌刷屏歸零：原版每次槽位重新整理都寫約 48 行除錯日誌——實測一次 3 小時 23 分鐘的工作階段共 484,156 行 / 34.75 MB，佔全部日誌行數的 85.68%、體積的 89.19%。本版執行期 0 行，原版自身也只剩 3 行啟動 + 3 行退出共 6 行一次性日誌；反覆翻背包、倉庫、商店不再持續寫入磁碟，Player.log 更小，排查問題也輕鬆多了。
[*]重新整理更輕：原版每次 Refresh 都清空並重建全部 6 個槽位；本版只在真正需要時重建一次，反射成員也改為啟動時查找一次並快取，減少重複開銷。
[*]視覺不變：所有武器仍顯示 6 個配件槽（白點 = 已裝，灰點 = 武器沒有的槽），未搜索武器不強制顯示——與原版 v1.1 一致。
[*]安全防護：將來遊戲更新改掉內部成員時，本模組會保持不生效並只打 1 行警告，不刷屏、不崩潰。[/list]

[h2]◆ 相容性[/h2]
[list][*]遊戲版本：逃離鴨科夫 2.3.30；其他版本未測試。
[*]只在執行時生效：不改遊戲檔案、資源或存檔。
[*]2026-10-28 在作者本人 72 mod 全開環境下實測 32 分鐘：本修正版僅 1 行啟動日誌、原版逐重新整理日誌 0 條、無本模組相關報錯。該環境同時包含 FixSlotDisplay、PluggableIndicator、ItemLevelAndSearchSoundMod、CustomItemLevelValue；未單獨做 A/B 的共存項目以實際表現為準。
[*]可以安全停用或移除：本模組只會移除自己的補丁。原版的狀態見下方「已知限制」。[/list]

[h2]◆ 已知限制[/h2]
[list][*]原版保持啟用時，它自己的一次性啟動/退出日誌共 6 行（3 行啟動 + 3 行退出）仍會出現在 Player.log；除此之外的輸出已經被靜音。
[*]在遊戲內停用本模組後，原版的補丁不會自動恢復；需要重新啟動遊戲才能回到原版行為。
[*]本模組只影響介面顯示，不改動任何遊戲數值或玩法。[/list]

[h2]◆ 更新日誌[/h2]
[list][*]1.0.0 — 首個公開版本：獨立乾淨實作，保留 6 槽視覺；除錯日誌關閉（僅 1 行啟動日誌）；反射成員靜態快取；槽位按需重建。[/list]

[h2]◆ 致謝與授權[/h2]
[list][*]原版「更好的配件展示 / Better Accessory Display」作者：我是火柴人 / chardexn —— https://github.com/Chardexn/BetterAccessoryDisplay
[*]原版以 MIT 授權發布。本模組是獨立修正版：非原作者發布，不包含任何原版檔案；原版的署名與下載量歸原作者所有。
[*]修正版作者：Dorei Hime，同樣以 MIT 授權發布。[/list]
```

## 日本語 (ja)

locale tag: ja

```
[h1]武器アタッチメント表示 · 修正版[/h1]
[b]オリジナル「Better Accessory Display」の独立修正版です。6スロットの見た目はそのままに、ログの大量出力を取り除きました。オリジナルMODはインストールしたまま有効にしておいてください。[/b]

オリジナル「Better Accessory Display」（更好的配件展示、Workshop 3596581923）は、Escape from Duckov のすべての武器に6つのアタッチメントスロットを強制的に表示するMODです。機能自体は便利ですが、問題はデバッグログにあります。ログはハードコードで常に有効になっており、スロットが更新されるたびに約48行を Player.log に書き込みます。実測した3時間23分のセッションでは 484,156 行 / 34.75 MB、全ログ行の 85.68%、サイズの 89.19% を占め、ピーク時は毎秒約216行に達しました。そのうち 172,327 行は「SlotIndicator.Target が null」という無効な経路によるもので、誰にも必要のないノイズでした。

本MODは見た目を完全に同じまま、クリーンなコードで実装し直したものです。原作者によるリリースではなく、独立した修正版です。

[h2]◆ 必要なMOD（どちらも必須アイテム）[/h2]
[list][*]HarmonyLoadMod（3589088839）：Harmony ランタイム（0Harmony.dll）を提供します。
[*]オリジナルの BetterAccessoryDisplay（3596581923）：インストールしたまま、有効にしておく必要があります。
[*]どちらも本MODには同梱されません。オリジナルのファイル・クレジット・ダウンロード数は原作者に帰属します。
[*]オリジナルは有効のままにしてください。本MODは起動時にオリジナルの Harmony パッチを外し、自分で表示を引き継ぎます。MOD一覧でオリジナルが有効に見えるのは意図的なものです。[/list]

[h2]◆ 読み込み順[/h2]
[list][*]本MODは HarmonyLoadMod とオリジナルの BetterAccessoryDisplay より後に読み込まれるようにしてください。新しくインストールしたMODは既定で最後に読み込まれるため、通常は何もする必要はありません。
[*]手動で並べ替える場合は、2つの必須MODを本MODより上に置いてください。（必要に応じて本MOD自身が 0Harmony.dll を読み込むフォールバックもあります。）[/list]

[h2]◆ 修正されたこと · 得られるメリット[/h2]
[list][*]ログの大量出力はゼロに：オリジナルはスロット更新のたびに約48行のデバッグログを書き出していました。実測した3時間23分のセッションでは 484,156 行 / 34.75 MB、全ログ行の 85.68%、サイズの 89.19% を占めていました。本MODはプレイ中の出力が0行で、オリジナル自身も起動3行＋終了3行の合計6行（一度きり）だけになります。バックパックや倉庫、ショップを行き来しても書き込みが続かないので、Player.log は小さく、問題の切り分けもずっと楽になります。
[*]更新処理が軽く：オリジナルは Refresh のたびに6つのスロット表示をすべて破棄して作り直していました。本MODは本当に必要なときだけ再構築し、リフレクションで取得するメンバーも起動時に一度だけ解決してキャッシュするため、無駄な処理が減ります。
[*]見た目はそのまま：すべての武器に6つのアタッチメントスロットが表示され（白い点＝装着済み、灰色の点＝武器にないスロット）、未捜索の武器には表示されません。オリジナル v1.1 と同じです。
[*]万一の備え：将来のゲーム更新で内部メンバーが変わった場合は、本MODは無効のまま警告を1行だけ出します。連続出力もクラッシュもありません。[/list]

[h2]◆ 互換性[/h2]
[list][*]ゲームバージョン：Escape from Duckov 2.3.30。他のバージョンでは未テストです。
[*]実行時にのみ動作します。ゲームファイル・アセット・セーブは変更しません。
[*]2026-10-28、作者本人の環境で72個のMODをすべて有効にした状態で32分間実機テストしました。本MODの起動ログは1行のみ、オリジナルの逐次更新ログは0行、本MOD関連のエラーもありませんでした。この環境には FixSlotDisplay、PluggableIndicator、ItemLevelAndSearchSoundMod、CustomItemLevelValue も含まれています。個別にA/BテストしていないMODとの共存については、実際の動作を基準にしています。
[*]無効化・削除しても安全です。本MODは自分のパッチだけを外します。オリジナル側の状態は下の「既知の制限」を参照してください。[/list]

[h2]◆ 既知の制限[/h2]
[list][*]オリジナルを有効にしている間は、オリジナル自身の起動／終了ログ（合計6行：起動3行＋終了3行）が Player.log に残ります。それ以外の出力は止まります。
[*]ゲーム内で本MODを無効化しても、オリジナルのパッチは自動では戻りません。オリジナルの挙動に戻すにはゲームを再起動してください。
[*]本MODは表示だけに影響し、ゲームの数値や進行には触れません。[/list]

[h2]◆ 更新履歴[/h2]
[list][*]1.0.0 — 初回公開版：独立したクリーンな実装、6スロットの見た目を維持、デバッグログをオフ（起動ログは1行）、リフレクションメンバーの静的キャッシュ、スロットは必要なときだけ再構築。[/list]

[h2]◆ クレジットとライセンス[/h2]
[list][*]オリジナル「更好的配件展示 / Better Accessory Display」作者：我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]オリジナルは MIT ライセンスです。本MODは独立した修正版であり、原作者による公開ではありません。オリジナルのファイルは一切含みません。オリジナルのクレジットとダウンロード数は原作者に帰属します。
[*]修正版作者：Dorei Hime。MIT ライセンスで公開します。[/list]
```

## 한국어 (ko)

locale tag: ko

```
[h1]무기 부품 표시 · 수정판[/h1]
[b]원본 Better Accessory Display의 독립 수정판입니다. 6개 슬롯 표시는 그대로 유지하면서 로그 폭주만 제거했습니다. 원본 모드는 설치한 채로 활성화해 두세요.[/b]

원본 「Better Accessory Display」(更好的配件展示, Workshop 3596581923)는 Escape from Duckov의 모든 무기에 6개의 부착물 슬롯을 강제로 표시해 주는 모드입니다. 기능 자체는 편리하지만 문제는 디버그 로그에 있습니다. 로그가 하드코딩으로 항상 켜져 있어서 슬롯이 갱신될 때마다 약 48줄을 Player.log에 기록합니다. 실제로 측정한 3시간 23분 세션에서 484,156줄 / 34.75 MB를 기록했는데, 이는 전체 로그 줄의 85.68%, 용량의 89.19%에 해당하며 최대 초당 약 216줄까지 올라갔습니다. 그중 172,327줄은 "SlotIndicator.Target이 null"인 무효 경로에서 나온, 아무에게도 필요 없는 노이즈였습니다.

이 모드는 화면은 똑같이 유지하면서 코드를 깨끗하게 다시 구현했습니다. 원작자가 배포한 것이 아닌 독립 수정판입니다.

[h2]◆ 필수 모드 (둘 다 필수 아이템)[/h2]
[list][*]HarmonyLoadMod(3589088839): Harmony 런타임(0Harmony.dll)을 제공합니다.
[*]원본 BetterAccessoryDisplay(3596581923): 설치한 채로 활성화해야 합니다.
[*]두 필수 모드 모두 이 모드에 포함되어 있지 않습니다. 원본의 파일, 크레딧, 다운로드 수는 원작자에게 있습니다.
[*]원본은 활성 상태로 두세요. 이 모드는 시작할 때 원본의 Harmony 패치를 제거하고 직접 화면을 담당합니다. 모드 목록에서 원본이 활성화된 것으로 보이는 것은 의도된 동작입니다.[/list]

[h2]◆ 로드 순서[/h2]
[list][*]이 모드는 HarmonyLoadMod와 원본 BetterAccessoryDisplay보다 뒤에 로드되어야 합니다. 새로 설치한 모드는 기본적으로 맨 마지막에 로드되므로 보통은 따로 할 일이 없습니다.
[*]수동으로 정렬한다면 두 필수 모드를 이 모드 위에 두세요. (필요하면 이 모드가 0Harmony.dll을 직접 불러오는 안전장치도 있습니다.)[/list]

[h2]◆ 수정된 것 · 얻을 수 있는 것[/h2]
[list][*]로그 폭주 제로: 원본은 슬롯이 갱신될 때마다 약 48줄의 디버그 로그를 기록했습니다. 실제로 측정한 3시간 23분 세션에서 484,156줄 / 34.75 MB로 전체 로그 줄의 85.68%, 용량의 89.19%를 차지했습니다. 이 버전은 플레이 중 0줄을 기록하며, 원본 자체도 시작 3줄 + 종료 3줄, 총 6줄의 일회성 로그만 남습니다. 가방·창고·상점을 오가도 계속 디스크에 쓰지 않으니 Player.log가 작아지고 문제를 찾기도 훨씬 쉬워집니다.
[*]갱신 부담 감소: 원본은 Refresh마다 6개 슬롯 표시를 모두 지우고 다시 만들었습니다. 이 버전은 정말 필요할 때만 다시 만들고, 리플렉션 멤버도 시작할 때 한 번 찾아 캐시해 두어 반복 작업이 줄어듭니다.
[*]화면은 그대로: 모든 무기에 6개의 부착물 슬롯이 표시되고(흰 점 = 장착됨, 회색 점 = 무기에 없는 슬롯), 아직 조사하지 않은 무기에는 표시되지 않습니다. 원본 v1.1과 동일합니다.
[*]안전장치: 나중에 게임 업데이트로 내부 멤버가 바뀌면 이 모드는 비활성 상태로 경고 1줄만 남깁니다. 폭주도, 튕김도 없습니다.[/list]

[h2]◆ 호환성[/h2]
[list][*]게임 버전: Escape from Duckov 2.3.30. 다른 버전은 테스트하지 않았습니다.
[*]실행 중에만 동작합니다. 게임 파일, 에셋, 세이브를 변경하지 않습니다.
[*]2026-10-28, 작성자 본인의 환경에서 모드 72개를 모두 활성화한 상태로 32분간 실기 테스트했습니다. 이 수정판의 시작 로그는 1줄뿐이었고, 원본의 갱신별 로그는 0줄이었으며, 이 모드 관련 오류도 없었습니다. 이 환경에는 FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod, CustomItemLevelValue도 함께 포함되어 있습니다. 개별 A/B 테스트를 하지 않은 모드와의 공존은 실제 동작을 기준으로 합니다.
[*]비활성화하거나 삭제해도 안전합니다. 이 모드는 자기 패치만 제거합니다. 원본 상태는 아래 '알려진 제한'을 참고하세요.[/list]

[h2]◆ 알려진 제한[/h2]
[list][*]원본이 활성화되어 있는 동안에는 원본 자체의 일회성 시작·종료 로그(총 6줄: 시작 3줄 + 종료 3줄)가 Player.log에 남습니다. 그 외 출력은 조용해집니다.
[*]게임 안에서 이 모드를 비활성화해도 원본 패치는 자동으로 돌아오지 않습니다. 원본 동작으로 돌아가려면 게임을 재시작하세요.
[*]이 모드는 표시에만 영향을 주며 게임 수치나 진행에는 관여하지 않습니다.[/list]

[h2]◆ 변경 이력[/h2]
[list][*]1.0.0 — 첫 공개 버전: 독립적인 깔끔한 구현, 6개 슬롯 표시 유지, 디버그 로그 끔(시작 로그 1줄), 리플렉션 멤버 정적 캐시, 필요한 때만 슬롯 재구성.[/list]

[h2]◆ 크레딧 및 라이선스[/h2]
[list][*]원본 「更好的配件展示 / Better Accessory Display」 제작: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]원본은 MIT 라이선스입니다. 이 모드는 독립 수정판으로, 원작자가 배포한 것이 아니며 원본 파일을 포함하지 않습니다. 원본의 크레딧과 다운로드 수는 원작자에게 있습니다.
[*]수정판 제작: Dorei Hime. MIT 라이선스로 배포합니다.[/list]
```

## Français (fr)

locale tag: fr

```
[h1]Affichage des accessoires d'arme · Version corrigée[/h1]
[b]Version corrigée indépendante du mod original Better Accessory Display : les mêmes 6 emplacements, sans le spam de logs. L'original reste installé et activé.[/b]

Le mod original « Better Accessory Display » (更好的配件展示, Workshop 3596581923) force toutes les armes d'Escape from Duckov à afficher les six emplacements d'accessoires, ce qui simplifie la comparaison et l'échange de pièces. La fonction est utile ; le problème vient de ses logs de debug, codés en dur et impossibles à désactiver : chaque rafraîchissement d'emplacement écrit environ 48 lignes dans Player.log. Sur une session mesurée de 3 h 23, il a produit 484 156 lignes / 34,75 Mo, soit 85,68 % des lignes et 89,19 % du volume du journal, avec des pointes d'environ 216 lignes par seconde. 172 327 de ces lignes venaient d'un chemin mort (« SlotIndicator.Target is null ») et ne servaient à rien.

Ce mod conserve exactement le même rendu, mais avec un code propre. C'est une correction indépendante, pas une publication de l'auteur original.

[h2]◆ Prérequis (deux objets requis)[/h2]
[list][*]HarmonyLoadMod (3589088839) : fournit le runtime Harmony (0Harmony.dll).
[*]Le mod original BetterAccessoryDisplay (3596581923) : doit rester installé et activé.
[*]Aucun des deux n'est inclus ici. Les fichiers, le crédit et le nombre de téléchargements de l'original restent à son auteur.
[*]L'original doit rester activé : ce mod retire ses patches Harmony au démarrage et reprend lui-même l'affichage. Voir l'original encore activé dans la liste des mods est voulu.[/list]

[h2]◆ Ordre de chargement[/h2]
[list][*]Chargez ce mod après HarmonyLoadMod et après l'original BetterAccessoryDisplay. Un mod fraîchement installé se charge en dernier par défaut : en général, rien à faire.
[*]Si vous triez à la main, gardez les deux prérequis au-dessus de ce mod. (Par sécurité, ce mod charge aussi 0Harmony.dll lui-même si nécessaire.)[/list]

[h2]◆ Ce qui est corrigé · Ce que vous y gagnez[/h2]
[list][*]Plus aucun spam de logs : l'original écrivait environ 48 lignes de debug à chaque rafraîchissement d'emplacement — sur une session mesurée de 3 h 23, cela faisait 484 156 lignes / 34,75 Mo, soit 85,68 % des lignes et 89,19 % du volume du journal. Cette version n'écrit aucune ligne pendant le jeu, et l'original lui-même n'apporte plus que ses 6 lignes ponctuelles (3 au démarrage + 3 à la fermeture). Naviguer dans le sac, le stockage ou les boutiques n'écrit plus en continu sur le disque : Player.log reste petit et bien plus facile à diagnostiquer.
[*]Rafraîchissements plus légers : l'original vidait et reconstruisait les six indicateurs d'emplacement à chaque Refresh. Cette version ne reconstruit que lorsque c'est vraiment nécessaire, et les membres obtenus par réflexion sont résolus une fois au démarrage puis mis en cache, ce qui supprime le travail répété.
[*]Visuel inchangé : toutes les armes affichent toujours six emplacements d'accessoires (point blanc = accessoire installé, point gris = emplacement absent de l'arme), et les armes non inspectées n'affichent rien — identique à l'original v1.1.
[*]Filet de sécurité : si une future mise à jour du jeu modifie les membres internes, le mod reste inactif et n'affiche qu'un seul avertissement — pas de spam, pas de crash.[/list]

[h2]◆ Compatibilité[/h2]
[list][*]Version du jeu : Escape from Duckov 2.3.30. Autres versions non testées.
[*]Tout se passe à l'exécution : aucun fichier, asset ou sauvegarde modifié.
[*]Test réel le 2026-10-28 dans l'environnement de l'auteur avec les 72 mods activés, pendant 32 minutes : cette version corrigée n'a produit qu'une seule ligne au démarrage, l'original 0 ligne par rafraîchissement, et aucune erreur liée à ce mod. Cet environnement incluait aussi FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod et CustomItemLevelValue ; pour les mods non testés en A/B séparément, c'est le comportement réel qui fait foi.
[*]Désactivation ou suppression sans risque : ce mod retire uniquement ses propres patches. Voir « Limites connues » ci-dessous pour l'état de l'original.[/list]

[h2]◆ Limites connues[/h2]
[list][*]Tant que l'original est activé, ses 6 lignes ponctuelles (3 au démarrage, 3 à la fermeture) restent dans Player.log ; le reste de sa sortie est réduit au silence.
[*]Désactiver ce mod en jeu ne restaure pas les patches de l'original ; redémarrez le jeu pour retrouver son comportement.
[*]Ce mod n'affecte que l'affichage : aucune valeur ni progression de jeu n'est modifiée.[/list]

[h2]◆ Historique[/h2]
[list][*]1.0.0 — première version publique : implémentation propre et indépendante, visuel 6 emplacements conservé, logs de debug désactivés (une seule ligne au démarrage), membres de réflexion mis en cache, emplacements reconstruits uniquement si nécessaire.[/list]

[h2]◆ Crédits et licence[/h2]
[list][*]Mod original « 更好的配件展示 / Better Accessory Display » par 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]L'original est publié sous licence MIT. Cette version corrigée est indépendante : elle n'est pas publiée par l'auteur original et ne contient aucun fichier de l'original. Le crédit et les téléchargements de l'original reviennent à son auteur.
[*]Version corrigée par Dorei Hime, publiée sous licence MIT.[/list]
```

## Deutsch (de)

locale tag: de

```
[h1]Waffenzubehör-Anzeige · Korrigierte Version[/h1]
[b]Unabhängig korrigierte Version des Originalmods Better Accessory Display: gleiche 6-Slot-Anzeige, ohne Log-Spam. Das Original bleibt installiert und aktiviert.[/b]

Der Originalmod „Better Accessory Display“ (更好的配件展示, Workshop 3596581923) zwingt alle Waffen in Escape from Duckov, alle sechs Zubehör-Slots anzuzeigen – praktisch beim Vergleichen und Tauschen von Anbauteilen. Die Funktion ist gut, das Problem sind die Debug-Logs: Sie sind fest eingebaut und lassen sich nicht abschalten, und bei jedem Slot-Refresh werden etwa 48 Zeilen in Player.log geschrieben. In einer gemessenen Sitzung von 3 Std. 23 Min. waren es 484.156 Zeilen / 34,75 MB – 85,68 % aller Logzeilen und 89,19 % des Logvolumens, mit Spitzen von rund 216 Zeilen pro Sekunde. 172.327 dieser Zeilen stammten aus einem toten Pfad („SlotIndicator.Target is null“) und waren reiner Ballast.

Dieser Mod behält das exakt gleiche Aussehen, verwendet aber sauberen Code. Es ist eine unabhängige Korrektur, keine Veröffentlichung des Originalautors.

[h2]◆ Voraussetzungen (beide sind erforderliche Gegenstände)[/h2]
[list][*]HarmonyLoadMod (3589088839): liefert die Harmony-Laufzeit (0Harmony.dll).
[*]Der Originalmod BetterAccessoryDisplay (3596581923): muss installiert und aktiviert bleiben.
[*]Beide sind hier nicht enthalten. Dateien, Nennung und Downloadzahl des Originals bleiben beim Originalautor.
[*]Das Original muss aktiviert bleiben: Dieser Mod entfernt beim Start dessen Harmony-Patches und übernimmt die Anzeige selbst. Dass das Original in der Mod-Liste weiter aktiviert erscheint, ist beabsichtigt.[/list]

[h2]◆ Ladereihenfolge[/h2]
[list][*]Dieser Mod muss nach HarmonyLoadMod und nach dem Original BetterAccessoryDisplay geladen werden. Neu installierte Mods werden standardmäßig zuletzt geladen – meist ist nichts zu tun.
[*]Bei manueller Sortierung beide Voraussetzungen über diesem Mod lassen. (Zur Sicherheit lädt dieser Mod bei Bedarf auch selbst 0Harmony.dll.)[/list]

[h2]◆ Was behoben ist · Was Sie davon haben[/h2]
[list][*]Kein Log-Spam mehr: Das Original schrieb bei jedem Slot-Refresh etwa 48 Debug-Zeilen — in einer gemessenen Sitzung von 3 Std. 23 Min. waren das 484.156 Zeilen / 34,75 MB, also 85,68 % aller Logzeilen und 89,19 % des Logvolumens. Diese Version schreibt im Spiel 0 Zeilen, und das Original selbst liefert nur noch seine 6 einmaligen Zeilen (3 beim Start + 3 beim Beenden). Wer Rucksack, Lager oder Läden durchsucht, löst keine fortlaufenden Schreibvorgänge mehr aus: Player.log bleibt klein und lässt sich viel leichter auswerten.
[*]Leichtere Refreshes: Das Original leerte und erzeugte bei jedem Refresh alle sechs Slot-Indikatoren neu. Diese Version baut sie nur noch auf, wenn es wirklich nötig ist, und Reflexions-Member werden beim Start einmal ermittelt und zwischengespeichert — die wiederholte Arbeit entfällt.
[*]Optik unverändert: Alle Waffen zeigen weiterhin sechs Zubehör-Slots (weißer Punkt = Zubehör montiert, grauer Punkt = Slot, den die Waffe nicht hat), und noch nicht untersuchte Waffen zeigen keine Slots — identisch zum Original v1.1.
[*]Absicherung: Falls ein künftiges Spielupdate die internen Member ändert, bleibt der Mod inaktiv und schreibt nur eine Warnzeile — kein Spam, kein Absturz.[/list]

[h2]◆ Kompatibilität[/h2]
[list][*]Spielversion: Escape from Duckov 2.3.30. Andere Versionen ungetestet.
[*]Alles läuft zur Laufzeit: keine Spiel-Dateien, Assets oder Spielstände werden verändert.
[*]Praxistest am 2026-10-28 in der eigenen Umgebung des Autors mit allen 72 Mods aktiv, 32 Minuten lang: Diese korrigierte Version schrieb nur eine Startzeile, das Original 0 Zeilen pro Refresh, und es gab keine Fehler dieses Mods. Die Umgebung enthielt auch FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod und CustomItemLevelValue; bei Mods, die nicht einzeln im A/B-Test geprüft wurden, gilt das tatsächliche Verhalten.
[*]Deaktivieren oder Entfernen ist unbedenklich: Der Mod entfernt nur seine eigenen Patches. Zum Zustand des Originals siehe „Bekannte Einschränkungen“.[/list]

[h2]◆ Bekannte Einschränkungen[/h2]
[list][*]Solange das Original aktiviert ist, bleiben dessen 6 einmaligen Zeilen (3 beim Start, 3 beim Beenden) in Player.log; der Rest seiner Ausgabe ist stumm.
[*]Wird dieser Mod im Spiel deaktiviert, werden die Patches des Originals nicht automatisch wiederhergestellt; dafür das Spiel neu starten.
[*]Dieser Mod betrifft nur die Anzeige; Spielwerte und Fortschritt bleiben unverändert.[/list]

[h2]◆ Änderungsverlauf[/h2]
[list][*]1.0.0 — erste öffentliche Version: unabhängige, saubere Implementierung; 6-Slot-Optik beibehalten; Debug-Logs aus (eine Startzeile); Reflexions-Member zwischengespeichert; Slots nur bei Bedarf neu aufgebaut.[/list]

[h2]◆ Danksagung und Lizenz[/h2]
[list][*]Originalmod „更好的配件展示 / Better Accessory Display“ von 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]Das Original steht unter MIT-Lizenz. Diese korrigierte Version ist unabhängig: nicht vom Originalautor veröffentlicht und ohne Dateien des Originals. Nennung und Downloadzahlen des Originals gehören seinem Autor.
[*]Korrigierte Version von Dorei Hime, veröffentlicht unter der MIT-Lizenz.[/list]
```

## Español (es)

locale tag: es

```
[h1]Visualización de accesorios de arma · Versión corregida[/h1]
[b]Versión corregida independiente del mod original Better Accessory Display: los mismos 6 espacios, sin exceso de registros. El original permanece instalado y activado.[/b]

El mod original «Better Accessory Display» (更好的配件展示, Workshop 3596581923) obliga a todas las armas de Escape from Duckov a mostrar los seis espacios de accesorios, lo que facilita comparar e intercambiar piezas. La función es útil; el problema son sus registros de depuración: están fijados en el código y no se pueden desactivar, y cada refresco de espacios escribe unas 48 líneas en Player.log. En una sesión medida de 3 h 23 min generó 484.156 líneas / 34,75 MB: el 85,68 % de las líneas y el 89,19 % del tamaño del registro, con picos de unas 216 líneas por segundo. 172.327 de esas líneas venían de una ruta muerta («SlotIndicator.Target is null») y eran ruido puro.

Este mod mantiene exactamente el mismo aspecto, pero con código limpio. Es una corrección independiente, no una publicación del autor original.

[h2]◆ Requisitos (ambos son objetos requeridos)[/h2]
[list][*]HarmonyLoadMod (3589088839): proporciona el runtime de Harmony (0Harmony.dll).
[*]El mod original BetterAccessoryDisplay (3596581923): debe seguir instalado y activado.
[*]Ninguno está incluido aquí. Los archivos, el crédito y las descargas del original pertenecen a su autor.
[*]El original debe seguir activado: este mod retira sus parches de Harmony al iniciar y asume la visualización. Ver el original todavía activado en la lista de mods es intencionado.[/list]

[h2]◆ Orden de carga[/h2]
[list][*]Carga este mod después de HarmonyLoadMod y del original BetterAccessoryDisplay. Un mod recién instalado se carga al final por defecto, así que normalmente no hay que hacer nada.
[*]Si ordenas manualmente, deja los dos requisitos por encima de este mod. (Como red de seguridad, este mod también carga 0Harmony.dll por sí mismo si hace falta.)[/list]

[h2]◆ Qué se corrige · Qué ganas[/h2]
[list][*]Se acabó el spam de registros: el original escribía unas 48 líneas de depuración en cada refresco de espacio — en una sesión medida de 3 h 23 min eran 484.156 líneas / 34,75 MB, el 85,68 % de las líneas y el 89,19 % del tamaño del registro. Esta versión escribe 0 líneas durante el juego, y el original solo aporta ya sus 6 líneas puntuales (3 de inicio + 3 de cierre). Revisar la mochila, el almacén o las tiendas ya no genera escrituras continuas en disco: Player.log queda pequeño y es mucho más fácil diagnosticar.
[*]Refrescos más ligeros: el original vaciaba y reconstruía los seis indicadores de espacio en cada Refresh. Esta versión solo reconstruye cuando hace falta de verdad, y los miembros obtenidos por reflexión se resuelven una vez al iniciar y se guardan en caché, eliminando el trabajo repetido.
[*]Aspecto sin cambios: todas las armas siguen mostrando seis espacios de accesorios (punto blanco = accesorio instalado, punto gris = espacio que el arma no tiene), y las armas no inspeccionadas no muestran nada — igual que el original v1.1.
[*]Red de seguridad: si una futura actualización del juego cambia los miembros internos, el mod queda inactivo y solo escribe un aviso — sin spam ni cuelgues.[/list]

[h2]◆ Compatibilidad[/h2]
[list][*]Versión del juego: Escape from Duckov 2.3.30. Otras versiones sin probar.
[*]Todo ocurre en tiempo de ejecución: no se modifican archivos, recursos ni partidas.
[*]Prueba real el 2026-10-28 en el entorno del autor con los 72 mods activados, durante 32 minutos: esta versión corregida solo registró una línea al iniciar, el original 0 líneas por refresco y ningún error relacionado con este mod. El entorno también incluía FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod y CustomItemLevelValue; para los mods que no se probaron en A/B por separado, se toma como referencia el comportamiento real.
[*]Se puede desactivar o eliminar sin riesgo: solo retira sus propios parches. Para el estado del original, consulta «Limitaciones conocidas».[/list]

[h2]◆ Limitaciones conocidas[/h2]
[list][*]Mientras el original esté activado, sus 6 líneas puntuales (3 de inicio y 3 de cierre) seguirán en Player.log; el resto de su salida queda silenciado.
[*]Desactivar este mod dentro del juego no restaura los parches del original; reinicia el juego para recuperar su comportamiento.
[*]Este mod solo afecta a la visualización; no cambia valores ni progreso del juego.[/list]

[h2]◆ Historial de cambios[/h2]
[list][*]1.0.0 — primera versión pública: implementación limpia e independiente; se mantiene el aspecto de 6 espacios; registros de depuración desactivados (una sola línea al iniciar); miembros de reflexión en caché; espacios reconstruidos solo cuando hace falta.[/list]

[h2]◆ Créditos y licencia[/h2]
[list][*]Mod original «更好的配件展示 / Better Accessory Display» de 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]El original tiene licencia MIT. Esta versión corregida es independiente: no la publica el autor original y no contiene archivos del original. El crédito y las descargas del original son de su autor.
[*]Versión corregida por Dorei Hime, publicada bajo licencia MIT.[/list]
```

## Português (pt)

locale tag: pt

```
[h1]Exibição de acessórios de arma · Versão corrigida[/h1]
[b]Versão corrigida independente do mod original Better Accessory Display: os mesmos 6 espaços, sem o excesso de logs. O original permanece instalado e ativado.[/b]

O mod original «Better Accessory Display» (更好的配件展示, Workshop 3596581923) força todas as armas de Escape from Duckov a mostrar os seis espaços de acessórios, o que facilita comparar e trocar peças. A função é útil; o problema são os logs de depuração: ficam fixos no código, não dá para desligar, e a cada atualização dos espaços são gravadas cerca de 48 linhas no Player.log. Numa sessão medida de 3 h 23 min foram 484.156 linhas / 34,75 MB: 85,68% de todas as linhas e 89,19% do tamanho do log, com picos de cerca de 216 linhas por segundo. 172.327 dessas linhas vinham de um caminho morto («SlotIndicator.Target is null») e eram puro ruído.

Este mod mantém exatamente a mesma aparência, mas com código limpo. É uma correção independente, não uma publicação do autor original.

[h2]◆ Requisitos (os dois são itens necessários)[/h2]
[list][*]HarmonyLoadMod (3589088839): fornece o runtime do Harmony (0Harmony.dll).
[*]O mod original BetterAccessoryDisplay (3596581923): precisa continuar instalado e ativado.
[*]Nenhum dos dois vem incluído aqui. Os arquivos, os créditos e o número de downloads do original pertencem ao autor dele.
[*]O original precisa continuar ativado: este mod remove os patches de Harmony dele ao iniciar e assume a exibição. Ver o original ainda ativado na lista de mods é intencional.[/list]

[h2]◆ Ordem de carregamento[/h2]
[list][*]Carregue este mod depois do HarmonyLoadMod e do original BetterAccessoryDisplay. Um mod recém-instalado carrega por último por padrão, então normalmente não há nada a fazer.
[*]Se você ordenar manualmente, mantenha os dois requisitos acima deste mod. (Como segurança, este mod também carrega o 0Harmony.dll sozinho quando necessário.)[/list]

[h2]◆ O que foi corrigido · O que você ganha[/h2]
[list][*]Fim do excesso de logs: o original gravava cerca de 48 linhas de depuração a cada atualização dos espaços — numa sessão medida de 3 h 23 min, eram 484.156 linhas / 34,75 MB, 85,68% das linhas e 89,19% do tamanho do log. Esta versão grava 0 linhas durante o jogo, e o original passa a contribuir apenas com as suas 6 linhas pontuais (3 na inicialização + 3 no encerramento). Abrir a mochila, o armazém ou as lojas não gera mais gravações contínuas em disco: o Player.log fica pequeno e bem mais fácil de diagnosticar.
[*]Atualizações mais leves: o original liberava e recriava os seis indicadores de espaço a cada Refresh. Esta versão só recria quando é realmente necessário, e os membros obtidos por reflexão são resolvidos uma vez na inicialização e ficam em cache, eliminando trabalho repetido.
[*]Visual igual: todas as armas continuam mostrando seis espaços de acessórios (ponto branco = acessório instalado, ponto cinza = espaço que a arma não tem), e armas ainda não inspecionadas não mostram nada — igual ao original v1.1.
[*]Proteção: se uma futura atualização do jogo mudar os membros internos, o mod fica inativo e registra apenas um aviso — sem excesso de logs e sem travamentos.[/list]

[h2]◆ Compatibilidade[/h2]
[list][*]Versão do jogo: Escape from Duckov 2.3.30. Outras versões não testadas.
[*]Tudo acontece em tempo de execução: nenhum arquivo, recurso ou save é alterado.
[*]Teste real em 2026-10-28 no ambiente do autor com os 72 mods ativados, por 32 minutos: esta versão corrigida registrou apenas uma linha na inicialização, o original 0 linhas por atualização e nenhum erro relacionado a este mod. O ambiente também incluía FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod e CustomItemLevelValue; para os mods que não foram testados em A/B separadamente, o comportamento real é a referência.
[*]Pode ser desativado ou removido sem risco: ele remove apenas os próprios patches. Para o estado do original, veja «Limitações conhecidas».[/list]

[h2]◆ Limitações conhecidas[/h2]
[list][*]Enquanto o original estiver ativado, as 6 linhas pontuais dele (3 na inicialização e 3 no encerramento) continuam no Player.log; o resto da saída fica silencioso.
[*]Desativar este mod dentro do jogo não restaura os patches do original; reinicie o jogo para voltar ao comportamento original.
[*]Este mod afeta apenas a exibição; não altera valores nem progresso do jogo.[/list]

[h2]◆ Histórico de mudanças[/h2]
[list][*]1.0.0 — primeira versão pública: implementação limpa e independente; visual de 6 espaços mantido; logs de depuração desligados (uma única linha na inicialização); membros de reflexão em cache; espaços recriados só quando necessário.[/list]

[h2]◆ Créditos e licença[/h2]
[list][*]Mod original «更好的配件展示 / Better Accessory Display», de 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay
[*]O original é distribuído sob licença MIT. Esta versão corrigida é independente: não é publicada pelo autor original e não contém arquivos do original. Os créditos e downloads do original pertencem ao autor dele.
[*]Versão corrigida por Dorei Hime, publicada sob licença MIT.[/list]
```

## Русский (ru)

locale tag: ru

```
[h1]Отображение аксессуаров оружия · Исправленная версия[/h1]
[b]Независимая исправленная версия оригинального мода Better Accessory Display: те же 6 слотов, но без потока логов. Оригинал остаётся установленным и включённым.[/b]

Оригинал «Better Accessory Display» (更好的配件展示, Workshop 3596581923) принудительно показывает у всего оружия в Escape from Duckov все шесть слотов аксессуаров. Проблема — отладочные логи: они жёстко включены, и при каждом обновлении слотов в Player.log пишутся ~48 строк. За сессию 3 ч 23 мин это дало 484 156 строк / 34,75 МБ: 85,68 % всех строк и 89,19 % объёма лога. Эта версия сохраняет внешний вид, но реализована чистым кодом; это независимое исправление, а не публикация автора оригинала.

[h2]◆ Требования (оба — обязательные предметы)[/h2]
[list][*]HarmonyLoadMod (3589088839) — среда Harmony (0Harmony.dll).
[*]Оригинальный BetterAccessoryDisplay (3596581923) — должен оставаться установленным и включённым.
[*]Ничего из этого не входит в мод; файлы, упоминание и загрузки оригинала остаются у его автора.
[*]Оригинал должен быть включён: мод снимает его патчи Harmony при запуске и сам отвечает за отображение. Включённый оригинал в списке модов — это задумано.[/list]

[h2]◆ Порядок загрузки[/h2]
[list][*]Загружайте мод после HarmonyLoadMod и оригинального BetterAccessoryDisplay. Новые моды по умолчанию загружаются последними, так что обычно ничего делать не нужно.
[*]При ручной сортировке оставьте оба обязательных мода выше. (При необходимости мод сам загружает 0Harmony.dll.)[/list]

[h2]◆ Что исправлено · Что вы получаете[/h2]
[list][*]Поток логов исчез: 48 строк на каждое обновление и 484 156 строк / 34,75 МБ за сессию 3 ч 23 мин (85,68 % строк, 89,19 % объёма) — теперь 0 строк во время игры, а у оригинала остаются только 6 одноразовых строк (3 при запуске + 3 при выходе). Рюкзак, склад и магазины больше не пишут на диск постоянно — Player.log меньше и проще для диагностики.
[*]Обновления легче: оригинал при каждом Refresh очищал и создавал заново все 6 индикаторов слотов — теперь пересоздание только при необходимости, а члены рефлексии определяются один раз при запуске и кешируются.
[*]Вид прежний: 6 слотов у всего оружия (белая точка — аксессуар установлен, серая — слота у оружия нет), у неосмотренных единиц слоты не показываются; как в оригинале v1.1.
[*]Подстраховка: если обновление игры изменит внутренние члены, мод останется неактивным и выведет одно предупреждение — без спама и падений.[/list]

[h2]◆ Совместимость[/h2]
[list][*]Игра: Escape from Duckov 2.3.30 (проверено); другие версии не тестировались.
[*]Только во время выполнения: файлы, ресурсы и сохранения не изменяются.
[*]Тест 2026-10-28 в окружении автора со всеми 72 модами, 32 минуты: 1 строка запуска, 0 строк на обновление, ошибок нет. В окружении также были FixSlotDisplay, PluggableIndicator, ItemLevelAndSearchSoundMod и CustomItemLevelValue; для модов без отдельного A/B-теста ориентир — фактическое поведение.
[*]Отключать и удалять безопасно: мод снимает только свои патчи. Состояние оригинала — в «Известных ограничениях».[/list]

[h2]◆ Известные ограничения[/h2]
[list][*]При включённом оригинале его 6 одноразовых строк (3 при запуске + 3 при выходе) остаются в Player.log; остальной вывод заглушен.
[*]Если отключить мод в игре, патчи оригинала сами не вернутся — перезапустите игру.
[*]Мод влияет только на отображение, игровые значения не меняются.[/list]

[h2]◆ История изменений[/h2]
[list][*]1.0.0 — первый публичный выпуск: чистая независимая реализация, прежний вид 6 слотов, отладочные логи выключены (1 строка запуска), кеш рефлексии, пересоздание слотов по необходимости.[/list]

[h2]◆ Благодарности и лицензия[/h2]
[list][*]Оригинал «更好的配件展示 / Better Accessory Display» — 我是火柴人 / chardexn: https://github.com/Chardexn/BetterAccessoryDisplay
[*]Оригинал под лицензией MIT. Это независимое исправление: не публикация автора оригинала, файлов оригинала здесь нет; упоминание и загрузки принадлежат автору.
[*]Исправленная версия: Dorei Hime, лицензия MIT.[/list]
```
