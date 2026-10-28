# Better Accessory Display (Fixed) v1.0.0 — 发布说明 / Release Notes

> 状态：**发布前草案**。构建与实机验证完成后，请补齐第 6 节「验证状态」；实测数据见 [测试报告](BetterAccessoryDisplayFixed-TEST_REPORT.md)（目前为「待实测」骨架，不得当作已验证结论）。
>
> 相关文档：[10 语种工坊文案](BetterAccessoryDisplayFixed-workshop-localizations.md) · [English 默认描述](BetterAccessoryDisplayFixed-workshop-default.md) · [加载顺序参考](MOD_LOAD_ORDER.md)

## 0. 版本一览

| 项目 | 内容 |
|---|---|
| 显示名（暂定） | 更好的配件展示 · 修复版 \| Better Accessory Display (Fixed) |
| 工坊标题（en / zh-CN） | Better Accessory Display · Fixed / 更好的配件展示 · 修复版（其余 8 语种见工坊文案文件） |
| Steam 工坊 | [3828601227](https://steamcommunity.com/sharedfiles/filedetails/?id=3828601227)（2026-10-28 发布） |
| 内部名 / 工坊目录 | `BetterAccessoryDisplayFixed` |
| 版本 | **1.0.0**（首个公开版本） |
| 适配游戏 | Escape from Duckov **2.3.30**（Steam appid 3167020） |
| 发布账号 | Dorei Hime |
| 原版作者 | 我是火柴人 / chardexn — <https://github.com/Chardexn/BetterAccessoryDisplay> |
| 许可 | MIT（原版同为 MIT；本 mod 不包含原版文件） |
| 性质 | **独立修复版，非原作者发布** |
| Required items | HarmonyLoadMod `3589088839`、原版 BetterAccessoryDisplay `3596581923`（原版保持启用） |

## 1. 这个版本解决什么问题

原版「更好的配件展示」（Better Accessory Display，工坊 3596581923）会把所有武器强制显示为 6 个配件槽，功能本身正常。问题在调试日志：原版把 `DEBUG_MODE` 写死为 `true`，所有 `Debug.Log` 无条件输出，每次槽位刷新约 48 行、没有开关。

历史实测基线（72 个 mod 的一次会话，来源：`work/loadorder/log-perf.md`）：

- 一次 **3 小时 23 分** 的会话共 **484,156 行 / 34.75 MB**，占该次日志行数的 **85.68%**、体积的 **89.19%**；
- 其中 **172,327 行** 是「`SlotIndicator.Target 为 null`，跳过颜色设置」的无效路径，属于纯噪音；
- 峰值约 **216 行/秒**，共 2,795 个连续刷屏块，最大一块 6,949 行；
- 原版补丁逻辑本身不重（启动窗口排行 <0.13%），主要伤害是日志噪声、字符串分配与写盘。

更近的一次直接基线（2026-10-28 会话 `Player-prev.log`，本修复版尚未安装，同一 72 mod 环境）：日志共 **851,701 行 / 58.13 MB**，其中原版日志 **743,761 行 / 55.2 MB**（占行数 **87.33%**、体积 **90.56%**），8,652 个重建周期（平均 86 行/周期），其中 242,902 行是同类 null 无效路径；该会话里原版一次性启动/退出日志共 6 行（3 启动 + 3 退出）。这两组数据都只说明「日志量」问题，不构成帧率结论。

因此本版目标明确：**保留原版视觉，做到 0 刷屏**。只减少日志量，**不承诺帧率/性能提升**，也不把日志量差异写成性能结论。

## 2. v1.0.0 更新内容

### 2.1 视觉（与原版当前行为一致）

- 所有武器强制显示 6 个配件槽：**Scope / Muzzle / Grip / Stock / Tec / Mag**；
- 槽内已安装配件时，`contentIndicator` 设为**白色**；
- 武器本身没有的槽位强制显示，标记为**灰色**（红色是原版 v1.0 的旧行为；当前原版与本版均按灰色表述）；
- 未搜索（`NeedInspection`）的武器不强制显示槽位；
- 无设置项、无新增界面，装上即用。

### 2.2 日志

- 编译期调试开关写死关闭（`Verbose = false`），发布构建不输出任何逐刷新日志；
- 成功启动只有 **1 行** Info 日志；
- 硬依赖缺失、游戏内部成员不匹配等异常情况各只记 1 行 Warn / Error，**不按帧、不按刷新**输出；
- 原版自身的 **3 行启动 + 3 行退出（共 6 行）一次性日志**仍然存在（原版必须保持启用，本 mod 无法也不应从自己这一侧消除）。**因此不承诺「整份日志 0 行」，只承诺「0 刷屏、本 mod 仅 1 行启动日志」**。

### 2.3 实现方式

- **反射成员静态缓存**：`ItemDisplay.slotIndicatorContainer`、`ItemDisplay.SlotIndicatorPool`、`SlotIndicator.contentIndicator` 在类型初始化时解析一次并缓存，不再每次刷新反射查找；
- **槽位按需重建**：不再每次 `Refresh` 都 `ReleaseAll()` 清空并重建全部 6 个指示器，只在确实需要时重建；
- **与原版隔离**：启动时按原版 Harmony ID `"BetterAccessoryDisplay"` 执行 `UnpatchAll`，并额外按程序集兜底清扫；只摘除补丁，**不修改、不移动、不禁用原版的文件和工坊目录**，原版在 Mods 列表里保持「已启用」外观；
- **自身补丁独立管理**：本 mod 使用自己的 Harmony ID `"BetterAccessoryDisplayFixed"` 打补丁；禁用本 mod 时只移除自己的补丁；
- **Harmony 运行时**：编译期引用 `0Harmony.dll`（`Private=false`，不随包分发），运行时由 HarmonyLoadMod 提供；即使加载顺序不理想，本 mod 也会主动从必需 mod 目录加载 `0Harmony.dll` 兜底。

### 2.4 版本

- 1.0.0：首个公开版本（无历史版本迁移逻辑）。

## 3. 前置要求与加载顺序

### 3.1 必需物品（两个都要）

| 必需物品 | 工坊 id | 作用 | 状态 |
|---|---|---|---|
| HarmonyLoadMod | 3589088839 | 提供 Harmony 运行时 `0Harmony.dll` | 必须订阅并启用 |
| 原版 BetterAccessoryDisplay（更好的配件展示） | 3596581923 | 本 mod 修复的对象；运行时摘除其补丁后由本 mod 接管 | 必须订阅并**保持启用** |

- 两个前置都**不包含**在本 mod 发布包里；原版的文件、署名与下载量都归原作者；
- 原版在 Mods 列表里显示为「已启用」是刻意保留的，不要手动禁用；
- 若原版未安装：本 mod 保持不激活，仅输出 1 行提示，不弹窗、不崩溃（具体日志文本以实测为准）。

### 3.2 加载顺序

- 顺序要求：**HarmonyLoadMod → 原版 BetterAccessoryDisplay → 本 mod**；
- 新订阅的 mod 默认排在最后，一般无需手动操作；
- 手动排序时，把两个前置放在本 mod 上面；本 mod 自身也有主动加载 0Harmony 的兜底逻辑，但不建议依赖它。
- 参考：[MOD_LOAD_ORDER.md](MOD_LOAD_ORDER.md)。

## 4. 兼容性

| 项目 | 状态 |
|---|---|
| 游戏版本 2.3.30 | ✅ 实机验证（2026-10-28 两轮：首轮 32 分 14 秒 + 重启轮） |
| 原版仍启用 | ✅ 实机验证（保持启用，运行时摘除其补丁，文件未改） |
| FixSlotDisplay（3596579430） | ✅ 实机共存（72 mod 全开，无相关异常/视觉错乱；未单独 A/B） |
| PluggableIndicator（3641154103） | ✅ 实机共存（同上） |
| ItemLevelAndSearchSoundMod（3638170212 重制版；注意与 3588386576 同内部名） | ✅ 实机共存（同上） |
| CustomItemLevelValue（3638170364） | ✅ 实机共存（同上） |
| 其他游戏版本 | 未验证 |

- 所有改动都在运行时完成：不修改游戏文件、资源或存档；
- 上表「实机共存」指 72 个 mod 同时启用的整轮实测（见 §6.1 与测试报告）；未逐一做单 mod 关闭/开启的 A/B 对照。

## 5. 已知限制

1. **原版日志仍在**：原版保持启用，它自己的 3 行启动 + 3 行退出（共 6 行）一次性日志无法从本 mod 一侧消除；本 mod 只保证不再出现逐刷新刷屏。
2. **禁用后原版补丁不会自动恢复**：在游戏内禁用本 mod 时只会移除本 mod 自己的补丁，原版补丁不会自动重新挂上；需要**重启游戏**（或在禁用本 mod 后重开原版再重启）才能回到原版行为。
3. **游戏大更新后可能失效**：若未来游戏版本重命名了本 mod 依赖的内部成员，本 mod 会保持不生效并只输出 1 行警告，不会刷屏、不会崩溃。
4. **不承诺性能收益**：本版只减少日志量与重复刷新/反射开销；帧率是否变化不在承诺范围内，也不写成实测结论。
5. **同类 UI mod 叠加**：已在作者本人 72 mod 全开环境（FixSlotDisplay / PluggableIndicator / ItemLevelAndSearchSoundMod / CustomItemLevelValue 等同时启用）实机 32 分钟，无本 mod 相关异常与视觉错乱；未逐一做单 mod A/B，极限叠加仍建议实测。

## 6. 验证状态（发布前必须补齐）

- [x] 构建：`dotnet build` 0 error / 0 warning（2026-10-28，.NET SDK 10.0.302 Release；DLL 23,552 bytes）
- [x] 静态校验（入口类型 / 继承 / 程序集引用；`verify-badfixed.ps1` 通过，包内无前置 DLL）
- [x] 离线冒烟：游戏 2.3.30 反射成员、目标方法、`[HarmonyPatch]` 类均可被识别（详见测试报告 §1.2）
- [x] **实机测试**：2026-10-28 首轮 32 分 14 秒完整会话（72 mod 全开）+ 重启验证轮；结果见 §6.1 与[测试报告](BetterAccessoryDisplayFixed-TEST_REPORT.md)
- [ ] 工坊 10 语种文案人工校对（含标题、BBCode、数字与署名）
- [ ] 工坊页面核对：Required items 两个、预览图、改动说明、版本号
- [x] 确认发布包内不含原版文件与 `0Harmony.dll`

> 在上述项目完成前，本文件中的「已修复 / 已兼容」表述只能视为设计目标，不能对外宣称。

### 6.1 实机测试结果（2026-10-28）

| 指标 | 修复前基线 | 修复版实测（32.2 分钟） |
|---|---|---|
| `[更好的配件展示]` 行数 | 743,761 行 / 55.2 MB（87.33% 行 / 90.56% 体积） | **6 行**（3 启动 + 3 退出，运行期 0 行） |
| 其中逐刷新/无效路径日志 | 24 万+ 行 | **0 行** |
| 本 mod 日志 | — | **1 行**启动 Info |
| Player.log 总量 | 851,701 行 / 58.13 MB（203 分钟） | **21,215 行 / 1.12 MB**（每分钟 -84.3% 行 / -87.8% 体积） |
| 本 mod 相关异常 | — | **0** |

- 视觉：装备栏/仓库/商店/战局内物品格均为 6 个圆点；已装槽白点、武器没有的槽灰点、真实空槽黑点；非枪械物品不强制 6 点；进出场景与重启后正常（截图见测试报告 §4）。
- 共存：FixSlotDisplay / PluggableIndicator / ItemLevelAndSearchSoundMod / CustomItemLevelValue 等全部启用下无相关报错、无视觉错乱。
- 既有异常（非本 mod 引入）：其它 mod 的床交互 NRE、退出清理 NRE、`Index was out of range` 等，修复前日志中已有同签名，测试报告 §3.3 有归因。
- 未覆盖项：未搜索武器截图、逐次装/卸配件记录、游戏内禁用本 mod、缺少原版场景（T07/T10/T17/T18）。

## 7. 发布包内容（以最终 dist 产物为准）

| 文件 | 说明 |
|---|---|
| `BetterAccessoryDisplayFixed.dll` | 版本 1.0.0；23,552 bytes；SHA256 `EDABDD0BC91F72CC422D506ED780752D878C54BF0FA4729A6EE3BF262E88BC13` |
| `info.ini` | `version = 1.0.0`，`tags = Quality of Life,UI`；首次上传时由发布工具写入 `publishedFileId` |
| `preview.png` | 工坊预览图 512×512（原版封面 + 左上角「修复版 / FIXED」角标，232,684 bytes） |
| `LICENSE.txt` | MIT 许可 + 原版署名/来源说明 + 预览图来源说明（2,566 bytes） |

- 整个发布包 4 个文件 / 259,873 bytes；不包含：原版 mod 的任何文件、`0Harmony.dll`、本地调试符号。

## 8. 致谢与许可

- 原版「更好的配件展示 / Better Accessory Display」作者：**我是火柴人 / chardexn** — <https://github.com/Chardexn/BetterAccessoryDisplay>
- 原版以 **MIT** 许可发布。本 mod 是**独立修复版，非原作者发布**，**不包含任何原版文件**；原版的署名与下载量归原作者所有。
- 修复版作者 / 发布账号：**Dorei Hime**；本 mod 同样以 **MIT** 许可发布。

---

## English summary

**Better Accessory Display (Fixed) v1.0.0** is an independent fixed version of the original Better Accessory Display (Workshop 3596581923). It keeps the original's visuals — all weapons forced to show six attachment slots (Scope / Muzzle / Grip / Stock / Tec / Mag), white indicators for installed attachments, gray indicators for slots the weapon does not have, no forced slots on uninspected weapons — while removing the original's hard-coded debug logging: one measured 3 h 23 min session produced 484,156 log lines / 34.75 MB (85.68% of all lines, 89.19% of the log size), including 172,327 useless "SlotIndicator.Target is null" lines. This version logs a single startup line and nothing per refresh.

Requires HarmonyLoadMod (3589088839) and the original BetterAccessoryDisplay (3596581923), which must stay installed **and enabled**; the original is listed as a required item, its patches are removed at runtime, and none of its files are bundled here. Load this mod after both required mods (a fresh install loads last by default). Built for game 2.3.30. **Tested on 2026-10-28**: a 32-minute session with all 72 of the author's mods enabled (including FixSlotDisplay / PluggableIndicator / ItemLevelAndSearchSoundMod / CustomItemLevelValue) produced **6 one-time lines from the original mod (3 startup + 3 shutdown) and zero per-refresh lines**, one startup line from this fix, and no errors from this mod; the log was 21,215 lines / 1.12 MB vs 851,701 lines / 58.13 MB before the fix.

Known limits: the original's own 3 startup + 3 shutdown one-time log lines remain (it stays enabled); disabling this mod in-game does not restore the original's patches until the game is restarted; no frame-rate gain is claimed — only the log spam is removed.

Original mod by 我是火柴人 / chardexn (MIT): https://github.com/Chardexn/BetterAccessoryDisplay. This fixed version by Dorei Hime, also MIT, and is not published by the original author.
