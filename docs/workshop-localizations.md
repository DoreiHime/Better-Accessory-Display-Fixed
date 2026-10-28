# Workshop 文案（10 语种）— Better Accessory Display (Fixed) v1.0.0

> 游戏支持的 10 种语言。上传工具（`work/uploader-new/SteamLocalize`）按「单独一行 `locale tag: 语言` + 随后的代码围栏」解析；围栏内第一段 [h1] 会作为工坊标题单独提取，其余全部作为描述。
> 用法：`SteamLocalize <publishedFileId> docs/BetterAccessoryDisplayFixed-workshop-localizations.md`
> 排版约定：标题后不留空行；[list] 内不留空行；块内只使用 [h1][h2][list][*][b][i][hr]，不支持 [center]。

## English (en)

locale tag: en

```
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
```

## 简体中文 (zh-CN)

locale tag: zh-CN

```
[h1]更好的配件展示 · 修复版[/h1]
[b]这是原版「更好的配件展示 / Better Accessory Display」的独立修复版：保留同样的 6 个配件槽显示，去掉原版关不掉的日志刷屏。原版保持安装并启用。[/b]

[h2]◆ 原版的问题[/h2]
[list][*]调试日志写死在代码里、无法关闭，每次槽位刷新都会往 Player.log 写约 48 行。
[*]一次 3 小时 23 分钟的会话实测产生 484,156 行 / 34.75 MB（占整份日志 85.68% 行数、89.19% 体积）；后果是日志巨大、持续写盘，反复翻背包 / 仓库 / 商店时更严重，排查问题非常困难。[/list]

[h2]◆ 修复版做了什么[/h2]
[list][*]运行期 0 行刷屏：不再反复写 Player.log。
[*]不再每次 Refresh 都清空重建 6 个槽位，只在真正需要时重建；反射成员改为启动时查找一次并缓存。
[*]视觉保持一致：所有武器仍显示 6 个配件槽（白点 = 已安装，灰点 = 武器没有的槽），未搜索武器不强制显示。[/list]

[h2]◆ 前置要求（两个都是必需物品）[/h2]
[list][*]HarmonyLoadMod（3589088839）：提供 Harmony 运行时。
[*]原版 BetterAccessoryDisplay（3596581923）：必须保持安装并启用；本 mod 启动后会接管它的显示逻辑。
[*]本 mod 不包含这两个 mod 的文件；加载顺序请排在两者之后。[/list]

[h2]◆ 更新日志[/h2]
[list][*]1.0.0 — 首个公开版本：独立实现，保留 6 槽视觉，去掉日志刷屏与重复重建。[/list]

[h2]◆ 致谢与许可[/h2]
[list][*]原版作者：我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay（MIT 许可）
[*]本修复版作者：Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed（MIT 许可）
[*]独立修复版，非原作者发布，不包含原版文件；原版的署名与下载量归原作者。[/list]
```

## 繁體中文 (zh-TW)

locale tag: zh-TW

```
[h1]更好的配件展示 · 修正版[/h1]
[b]這是原版「更好的配件展示 / Better Accessory Display」的獨立修正版：保留同樣的 6 個配件槽顯示，去掉原版關不掉的日誌刷屏。原版保持安裝並啟用。[/b]

[h2]◆ 原版的問題[/h2]
[list][*]除錯日誌寫死在程式碼裡、無法關閉，每次槽位重新整理都會往 Player.log 寫約 48 行。
[*]一次 3 小時 23 分鐘的工作階段實測產生 484,156 行 / 34.75 MB（佔整份日誌 85.68% 行數、89.19% 體積）；結果是日誌龐大、持續寫入磁碟，反覆翻背包 / 倉庫 / 商店時更嚴重，排查問題非常困難。[/list]

[h2]◆ 修正版做了什麼[/h2]
[list][*]執行期 0 行刷屏：不再反覆寫 Player.log。
[*]不再每次 Refresh 都清空重建 6 個槽位，只在真正需要時重建；反射成員改為啟動時查找一次並快取。
[*]視覺保持一致：所有武器仍顯示 6 個配件槽（白點 = 已安裝，灰點 = 武器沒有的槽），未搜索武器不強制顯示。[/list]

[h2]◆ 前置要求（兩個都是必要項目）[/h2]
[list][*]HarmonyLoadMod（3589088839）：提供 Harmony 執行環境。
[*]原版 BetterAccessoryDisplay（3596581923）：必須保持安裝並啟用；本模組啟動後會接管它的顯示邏輯。
[*]本模組不包含這兩個模組的檔案；載入順序請排在兩者之後。[/list]

[h2]◆ 更新日誌[/h2]
[list][*]1.0.0 — 首個公開版本：獨立實作，保留 6 槽視覺，去掉日誌刷屏與重複重建。[/list]

[h2]◆ 致謝與授權[/h2]
[list][*]原版作者：我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay（MIT 授權）
[*]本修正版作者：Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed（MIT 授權）
[*]獨立修正版，非原作者發布，不包含原版檔案；原版的署名與下載量歸原作者。[/list]
```

## 日本語 (ja)

locale tag: ja

```
[h1]武器アタッチメント表示 · 修正版[/h1]
[b]オリジナル「Better Accessory Display」の独立修正版です。6つのアタッチメントスロット表示はそのままに、オフにできないログの大量出力を取り除きました。オリジナルMODはインストールしたまま有効にしてください。[/b]

[h2]◆ オリジナルの問題[/h2]
[list][*]デバッグログがコードに直書きされていて無効にできず、スロット更新のたびに約48行を Player.log に書き込みます。
[*]実測した3時間23分のセッションでは 484,156 行 / 34.75 MB（全体の 85.68% の行数、89.19% の容量）。ログが巨大になり書き込みが続くため、問題の切り分けも難しくなります。[/list]

[h2]◆ 修正版の内容[/h2]
[list][*]プレイ中の出力は0行：Player.log への繰り返し書き込みがなくなります。
[*]Refresh のたびに6つのスロットをすべて破棄して作り直すことはなくなり、本当に必要なときだけ再構築。リフレクションのメンバーも起動時に一度だけ解決してキャッシュします。
[*]見た目はそのまま：すべての武器に6つのスロットを表示（白い点＝装着済み、灰色の点＝武器にないスロット）、未捜索の武器には表示しません。[/list]

[h2]◆ 必要なMOD（どちらも必須アイテム）[/h2]
[list][*]HarmonyLoadMod（3589088839）：Harmony ランタイムを提供します。
[*]オリジナルの BetterAccessoryDisplay（3596581923）：インストールしたまま有効にしてください。本MODは起動後にその表示処理を引き継ぎます。
[*]本MODにこの2つのファイルは含まれません。読み込み順はこの2つより後にしてください。[/list]

[h2]◆ 更新履歴[/h2]
[list][*]1.0.0 — 初回公開版：独立した実装、6スロットの見た目はそのまま、ログ出力と重複した再構築を削除。[/list]

[h2]◆ クレジットとライセンス[/h2]
[list][*]オリジナル作者：我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay（MIT）
[*]修正版作者：Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed（MIT）
[*]独立した修正版であり、原作者による公開ではありません。オリジナルのファイルは含みません。クレジットとダウンロード数は原作者に帰属します。[/list]
```

## 한국어 (ko)

locale tag: ko

```
[h1]무기 부품 표시 · 수정판[/h1]
[b]원본 「Better Accessory Display」의 독립 수정판입니다. 6개 부착물 슬롯 표시는 그대로 두고, 끌 수 없는 로그 폭주를 없앴습니다. 원본 모드는 설치한 채로 활성화해 두세요.[/b]

[h2]◆ 원본의 문제[/h2]
[list][*]디버그 로그가 코드에 하드코딩되어 꺼지지 않으며, 슬롯이 갱신될 때마다 약 48줄을 Player.log에 기록합니다.
[*]실제로 측정한 3시간 23분 세션에서 484,156줄 / 34.75 MB(전체 로그의 85.68% 줄, 89.19% 용량). 로그가 거대해지고 디스크 쓰기가 계속되어 문제를 찾기 어려워집니다.[/list]

[h2]◆ 수정판이 한 일[/h2]
[list][*]플레이 중 출력 0줄: Player.log에 반복해서 쓰지 않습니다.
[*]Refresh마다 6개 슬롯을 모두 지우고 다시 만들지 않고, 정말 필요할 때만 재구성합니다. 리플렉션 멤버도 시작할 때 한 번 찾아 캐시합니다.
[*]화면은 그대로: 모든 무기에 6개 슬롯이 표시되고(흰 점 = 장착됨, 회색 점 = 무기에 없는 슬롯), 아직 조사하지 않은 무기에는 표시하지 않습니다.[/list]

[h2]◆ 필수 모드 (둘 다 필수 아이템)[/h2]
[list][*]HarmonyLoadMod(3589088839): Harmony 런타임을 제공합니다.
[*]원본 BetterAccessoryDisplay(3596581923): 설치한 채로 활성화해야 합니다. 이 모드는 시작 후 원본의 표시 로직을 이어받습니다.
[*]이 두 모드의 파일은 포함되어 있지 않습니다. 로드 순서는 두 모드 뒤에 두세요.[/list]

[h2]◆ 변경 이력[/h2]
[list][*]1.0.0 — 첫 공개 버전: 독립 구현, 6슬롯 표시 유지, 로그 폭주와 중복 재구성 제거.[/list]

[h2]◆ 크레딧 및 라이선스[/h2]
[list][*]원본 제작: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]수정판 제작: Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]독립 수정판으로 원작자가 배포한 것이 아니며 원본 파일을 포함하지 않습니다. 원본의 크레딧과 다운로드 수는 원작자에게 있습니다.[/list]
```

## Français (fr)

locale tag: fr

```
[h1]Affichage des accessoires d'arme · Version corrigée[/h1]
[b]Version corrigée indépendante du mod original « Better Accessory Display » : le même affichage de 6 emplacements, sans le spam de logs impossible à désactiver. L'original reste installé et activé.[/b]

[h2]◆ Le problème de l'original[/h2]
[list][*]Les logs de debug sont codés en dur et ne peuvent pas être désactivés : chaque rafraîchissement d'emplacement écrit environ 48 lignes dans Player.log.
[*]Session mesurée de 3 h 23 : 484 156 lignes / 34,75 Mo (85,68 % des lignes, 89,19 % du volume). Journal énorme et écritures continues : diagnostic très difficile.[/list]

[h2]◆ Ce que fait cette version[/h2]
[list][*]0 ligne de spam en jeu : plus d'écritures répétées dans Player.log.
[*]Plus de vidage/reconstruction des 6 emplacements à chaque Refresh : seulement si nécessaire, et les membres de réflexion sont résolus une fois au démarrage puis mis en cache.
[*]Visuel inchangé : toutes les armes affichent 6 emplacements (point blanc = installé, point gris = absent), les armes non inspectées n'affichent rien.[/list]

[h2]◆ Prérequis (deux objets requis)[/h2]
[list][*]HarmonyLoadMod (3589088839) : fournit le runtime Harmony.
[*]L'original BetterAccessoryDisplay (3596581923) : doit rester installé et activé ; cette version reprend son affichage au démarrage.
[*]Aucun des deux n'est inclus ; chargez ce mod après eux.[/list]

[h2]◆ Historique[/h2]
[list][*]1.0.0 — première version : implémentation indépendante, visuel 6 emplacements conservé, plus de spam de logs ni de reconstructions répétées.[/list]

[h2]◆ Crédits et licence[/h2]
[list][*]Auteur de l'original : 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Version corrigée par Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Version corrigée indépendante, non publiée par l'auteur original, sans fichiers de l'original ; crédit et téléchargements à son auteur.[/list]
```

## Deutsch (de)

locale tag: de

```
[h1]Waffenzubehör-Anzeige · Korrigierte Version[/h1]
[b]Unabhängig korrigierte Version des Originalmods „Better Accessory Display“: gleiche Anzeige mit 6 Slots, ohne den nicht abschaltbaren Log-Spam. Das Original bleibt installiert und aktiviert.[/b]

[h2]◆ Das Problem des Originals[/h2]
[list][*]Die Debug-Logs sind fest im Code und lassen sich nicht abschalten: Jeder Slot-Refresh schreibt etwa 48 Zeilen in Player.log.
[*]Gemessene Sitzung von 3 Std. 23 Min.: 484.156 Zeilen / 34,75 MB (85,68 % aller Logzeilen, 89,19 % des Volumens). Riesiges Log und laufende Schreibzugriffe – echte Probleme lassen sich kaum eingrenzen.[/list]

[h2]◆ Was diese Version macht[/h2]
[list][*]0 Zeilen Spam im Spiel: keine wiederholten Schreibzugriffe auf Player.log mehr.
[*]Kein Leeren und Neuerzeugen aller 6 Slots bei jedem Refresh mehr: Neuaufbau nur bei Bedarf, und Reflexions-Member werden beim Start einmal ermittelt und zwischengespeichert.
[*]Optik unverändert: Alle Waffen zeigen weiterhin 6 Slots (weißer Punkt = montiert, grauer Punkt = Slot fehlt), noch nicht untersuchte Waffen zeigen keine.[/list]

[h2]◆ Voraussetzungen (beide erforderlich)[/h2]
[list][*]HarmonyLoadMod (3589088839): liefert die Harmony-Laufzeit.
[*]Der Originalmod BetterAccessoryDisplay (3596581923): muss installiert und aktiviert bleiben; dieser Mod übernimmt beim Start seine Anzeige-Logik.
[*]Beide sind nicht enthalten; lade diesen Mod nach ihnen.[/list]

[h2]◆ Änderungsverlauf[/h2]
[list][*]1.0.0 — erste öffentliche Version: unabhängige Implementierung, 6-Slot-Optik erhalten, kein Log-Spam und keine wiederholten Neuaufbauten.[/list]

[h2]◆ Danksagung und Lizenz[/h2]
[list][*]Originalautor: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Korrigierte Version von Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Unabhängige korrigierte Version, nicht vom Originalautor veröffentlicht, ohne Dateien des Originals; Nennung und Downloadzahlen des Originals verbleiben beim Originalautor.[/list]
```

## Español (es)

locale tag: es

```
[h1]Visualización de accesorios de arma · Versión corregida[/h1]
[b]Versión corregida independiente del mod original «Better Accessory Display»: la misma visualización de 6 espacios, sin el spam de registros que no se puede desactivar. El original sigue instalado y activado.[/b]

[h2]◆ El problema del original[/h2]
[list][*]Los registros de depuración están fijados en el código y no se pueden desactivar: cada refresco de espacio escribe unas 48 líneas en Player.log.
[*]Sesión medida de 3 h 23 min: 484.156 líneas / 34,75 MB (el 85,68 % de las líneas, el 89,19 % del tamaño). Registro enorme y escrituras continuas: diagnóstico muy difícil.[/list]

[h2]◆ Qué hace esta versión[/h2]
[list][*]0 líneas de spam durante el juego: ya no escribe repetidamente en Player.log.
[*]Deja de vaciar y reconstruir los 6 espacios en cada Refresh: solo se reconstruyen cuando hace falta, y los miembros de reflexión se resuelven una vez al iniciar y se guardan en caché.
[*]Aspecto sin cambios: todas las armas siguen mostrando 6 espacios (punto blanco = instalado, punto gris = falta), y las armas no inspeccionadas no muestran ninguno.[/list]

[h2]◆ Requisitos (ambos obligatorios)[/h2]
[list][*]HarmonyLoadMod (3589088839): proporciona el runtime de Harmony.
[*]El original BetterAccessoryDisplay (3596581923): debe seguir instalado y activado; esta versión asume su visualización al iniciarse.
[*]Ninguno está incluido; carga este mod después de ambos.[/list]

[h2]◆ Historial de cambios[/h2]
[list][*]1.0.0 — primera versión: implementación independiente, misma visualización de 6 espacios, sin spam de registros ni reconstrucciones repetidas.[/list]

[h2]◆ Créditos y licencia[/h2]
[list][*]Autor original: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Versión corregida por Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Versión corregida independiente, no publicada por el autor original y sin archivos del original; el crédito y las descargas del original son de su autor.[/list]
```

## Português (pt)

locale tag: pt

```
[h1]Exibição de acessórios de arma · Versão corrigida[/h1]
[b]Versão corrigida independente do mod original «Better Accessory Display»: a mesma exibição de 6 espaços, sem o excesso de logs que não dá para desligar. O original continua instalado e ativado.[/b]

[h2]◆ O problema do original[/h2]
[list][*]Os logs de depuração são fixos no código e não podem ser desativados: cada atualização dos espaços grava cerca de 48 linhas no Player.log.
[*]Sessão medida de 3 h 23 min: 484.156 linhas / 34,75 MB (85,68% das linhas, 89,19% do tamanho). Log enorme e gravações contínuas: diagnóstico muito difícil.[/list]

[h2]◆ O que esta versão faz[/h2]
[list][*]0 linhas de spam durante o jogo: não grava mais repetidamente no Player.log.
[*]Deixa de limpar e recriar os 6 espaços a cada Refresh: recria só quando necessário, e os membros de reflexão são resolvidos uma vez na inicialização e ficam em cache.
[*]Visual inalterado: todas as armas continuam mostrando 6 espaços (ponto branco = instalado, ponto cinza = ausente), e armas não inspecionadas não mostram nenhum.[/list]

[h2]◆ Requisitos (os dois são obrigatórios)[/h2]
[list][*]HarmonyLoadMod (3589088839): fornece o runtime do Harmony.
[*]O original BetterAccessoryDisplay (3596581923): deve continuar instalado e ativado; esta versão assume a exibição dele ao iniciar.
[*]Nenhum dos dois vem incluído; carregue este mod depois deles.[/list]

[h2]◆ Histórico de mudanças[/h2]
[list][*]1.0.0 — primeira versão: implementação independente, visual de 6 espaços mantido, sem excesso de logs nem recriações repetidas.[/list]

[h2]◆ Créditos e licença[/h2]
[list][*]Autor original: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Versão corrigida por Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Versão corrigida independente, não publicada pelo autor original e sem arquivos do original; os créditos e downloads do original pertencem ao autor dele.[/list]
```

## Русский (ru)

locale tag: ru

```
[h1]Отображение аксессуаров оружия · Исправленная версия[/h1]
[b]Независимая исправленная версия оригинального «Better Accessory Display»: тот же показ 6 слотов, но без потока логов, который нельзя отключить. Оригинал остаётся установленным и включённым.[/b]

[h2]◆ Проблема оригинала[/h2]
[list][*]Отладочные логи жёстко прописаны в коде и не отключаются: при каждом обновлении слотов в Player.log пишутся ~48 строк.
[*]За сессию 3 ч 23 мин — 484 156 строк / 34,75 МБ (85,68 % всех строк, 89,19 % объёма). Огромный лог и постоянная запись на диск — настоящие проблемы очень трудно найти.[/list]

[h2]◆ Что делает исправленная версия[/h2]
[list][*]0 строк спама во время игры: больше нет повторяющейся записи в Player.log.
[*]Больше нет очистки и пересоздания всех 6 слотов при каждом Refresh: пересоздание только при необходимости, а члены рефлексии определяются один раз при запуске и кешируются.
[*]Внешний вид прежний: у всего оружия по-прежнему 6 слотов (белая точка — установлен, серая — слота нет), у неосмотренных единиц слоты не показываются.[/list]

[h2]◆ Требования (оба обязательны)[/h2]
[list][*]HarmonyLoadMod (3589088839): среда Harmony.
[*]Оригинальный BetterAccessoryDisplay (3596581923): должен оставаться установленным и включённым; эта версия при запуске берёт его логику отображения на себя.
[*]Файлы обоих в мод не входят; загружайте его после них.[/list]

[h2]◆ История изменений[/h2]
[list][*]1.0.0 — первый публичный выпуск: независимая реализация, прежний вид 6 слотов, без потока логов и повторных пересозданий.[/list]

[h2]◆ Благодарности и лицензия[/h2]
[list][*]Автор оригинала: 我是火柴人 / chardexn — https://github.com/Chardexn/BetterAccessoryDisplay (MIT)
[*]Исправленная версия: Dorei Hime — https://github.com/DoreiHime/Better-Accessory-Display-Fixed (MIT)
[*]Независимое исправление, выпущено не автором оригинала, без файлов оригинала; упоминание и загрузки оригинала остаются у его автора.[/list]
```
