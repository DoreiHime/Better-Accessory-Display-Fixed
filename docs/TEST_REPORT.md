# 测试报告 / Test Report — Better Accessory Display (Fixed) v1.0.0

> **状态：构建、静态校验、离线冒烟与游戏内实测均已完成（2026-10-28，两轮实机）。**
> 「基线」列来自修复前的同环境真实日志（`work/loadorder/log-perf.md` 的历史会话 + 修复前 `Player-prev.log` 的直接统计）；「修复版结果」列来自 2026-10-28 的两轮实机测试（首轮 32 分 14 秒完整会话 + 10 分 52 秒重启验证轮）。
> 测试目标：验证「视觉与原版一致 + 0 刷屏（本 mod 仅 1 行启动日志）」、前置与加载顺序、共存性、已知限制。

## 1. 测试环境

| 项目 | 值 | 状态 |
|---|---|---|
| 操作系统 | Windows 11（本机开发环境） | 已记录 |
| 游戏 | Escape from Duckov **2.3.30**（Steam appid 3167020，Unity 2022.3.62f2） | 实测确认 |
| 游戏语言 | 简体中文 | 实测 |
| 被测构建 | `BetterAccessoryDisplayFixed.dll` v1.0.0，23,552 bytes，SHA256 `EDABDD0BC91F72CC422D506ED780752D878C54BF0FA4729A6EE3BF262E88BC13` | 已构建（2026-10-28） |
| 日志路径 | `%USERPROFILE%\AppData\LocalLow\TeamSoda\Duckov\Player.log` | 已使用 |
| 测试日期 / 测试方式 | 2026-10-28 10:14–10:46（首轮，32 分 14 秒）+ 10:46:41–10:57:33（重启验证轮，10 分 52 秒） | 实机 |
| 基线来源 | ① 历史 3h23m 会话统计（`work/loadorder/log-perf.md`）；② 修复前 `Player-prev.log` 直接统计（见 §1.3） | 已实测（修复前） |

### 1.1 mod 清单（实测时状态：73 项优先级 / 72 个启用 mod 全开）

| # | 内部名 | 工坊 id | 启用 | 作用 |
|---|---|---|---|---|
| 0 | HarmonyLoadMod | 3589088839 | 是 | 必需：提供 0Harmony 2.4.1 |
| 60 | BetterAccessoryDisplay | 3596581923 | **是（保持启用）** | 必需：原版，本 mod 运行时摘除其补丁 |
| 61 | BetterAccessoryDisplayFixed | —（发布后填写） | 是 | 被测对象 |
| 53 | FixSlotDisplay | 3596579430 | 是 | 共存（同一套槽位显示） |
| 54 | PluggableIndicator | 3641154103 | 是 | 共存（可插槽高亮） |
| 58 | ItemLevelAndSearchSoundMod | 3638170212（重制版） | 是 | 共存（物品等级/搜索音效） |
| 59 | CustomItemLevelValue | 3638170364 | 是 | 共存（更丰富信息显示） |
| 62 | WhereAreMyItems | 3612302587 | 是 | 共存（物品位置/数量悬浮） |

完整 73 项顺序见 `work/loadorder/final_order.json` 与 `docs/MOD_LOAD_ORDER.md`（本轮实测时 `Mods.ES3` 与计划完全一致）。

### 1.2 构建、静态校验与离线冒烟（已完成，2026-10-28）

| 项目 | 结果 |
|---|---|
| 构建环境 | .NET SDK 10.0.302，`Release`，`dotnet build` **0 warning / 0 error**（游戏 2.3.30 Managed 程序集 + HarmonyLoadMod 的 0Harmony 2.4.1 编译期引用，`Private=false`） |
| 产物 | `dist\BetterAccessoryDisplayFixed\` 共 4 个文件 / 259,838 bytes：DLL 23,552 B、info.ini 1,036 B、LICENSE.txt 2,566 B、preview.png 232,684 B（512×512） |
| 静态校验 `verify-badfixed.ps1` | 通过：入口类型 `BetterAccessoryDisplayFixed.ModBehaviour` 存在且继承 `Duckov.Modding.ModBehaviour`；程序集引用仅游戏/Unity/System 程序集 + `0Harmony`（编译期，未随包）；包内不含 `0Harmony.dll` / 原版文件 |
| 离线冒烟（`work/badfixed-smoke`） | 通过（元数据级）：游戏 2.3.30 中 `ItemDisplay.slotIndicatorContainer`、`ItemDisplay.SlotIndicatorPool`、`SlotIndicator.contentIndicator` 三个反射成员均存在；`ItemDisplay.Setup/Refresh`、`SlotIndicator.Refresh` 三个目标方法存在；3 个 `[HarmonyPatch]` 类、4 个 patch 方法（1 prefix + 3 postfix）可被 Harmony 正确识别。真实 `PatchAll` 需游戏自带 Mono，已由实机测试覆盖。 |
| 加载顺序（已写入） | `Mods.ES3`：HarmonyLoadMod #0 → 原版 BetterAccessoryDisplay #60 → **BetterAccessoryDisplayFixed #61** → WhereAreMyItems #62；写后回读校验通过；`Global.json` 中 `ModActive_BetterAccessoryDisplayFixed = true` |
| 备份 | `Saves\Mods.ES3.leadbackup-20261028-090622`（改动前）、`Saves\Global.json.leadbackup-20261028-090601`（改动前）、`backups\badfixed-loadorder-20261028-090549\` |
| 发布工具预检 | `work\uploader-badfixed\SteamNewItemBadFixed.exe --check` 只读通过：Steam 用户 `Dorei Hime`、appid 3167020、2 个 Required items 解析正确；**未创建/未上传任何条目** |

### 1.3 「修复前」直接基线（修复版安装前，同一环境）

修复版安装前用户上一次完整会话日志（已归档统计，原始 JSON：`work/badfixed-tools/baseline-breakdown.json`）：

| 指标 | 数值 |
|---|---|
| 该日志总量 | 851,701 行 / 58.13 MB（60,956,309 bytes），会话 3h23m |
| `[更好的配件展示]` 行数 | **743,761 行（87.33%）/ 55,200,930 bytes（90.56%）** |
| 其中 `SlotIndicator.Target 为 null` 无效路径 | 242,902 行 |
| 其中 `SlotIndicatorRefresh ... Postfix` | 248,933 行 |
| 重建周期（`创建插槽` / 6） | 8,652 个周期 → 平均 **86.0 行/周期** |
| 原版一次性启动/退出日志（修复后仍会保留的部分） | **6 行 = 3 行启动**（`Loaded!!!`、`OnEnable`、`Harmony patches applied successfully`）**+ 3 行退出**（`OnDisable`、`Harmony patches removed successfully`、`OnDestroy`） |

## 2. 测试场景清单（实测结果）

| 编号 | 场景 | 预期结果（验收标准） | 结果 | 证据 |
|---|---|---|---|---|
| T01 | 启动与加载顺序 | HarmonyLoadMod → 原版 → 本 mod 依次加载，无报错 | **通过**（两轮启动：`Sorted mods` 中原版 #167 → 修复版 #168；激活日志 `ModActive_BetterAccessoryDisplay: True` 先于 `ModActive_BetterAccessoryDisplayFixed: True`） | `final-session.log` L2355-2364；第二轮 `Player.log` L150-180 |
| T02 | 启动日志 | 仅 1 行本 mod 日志，无逐刷新输出 | **通过**（两轮均恰 1 行） | 两轮 Player.log |
| T03 | 原版仍启用 | 原版保持「已启用」且被接管 | **通过**（Global.json 两个 `ModActive_*` 均为 True；原版 3 行启动日志出现；本 mod ready 行写明 neutralised；原版文件未改动，SHA256 前后一致） | `final-session.log` L2359-2363；`Global.json` |
| T04 | 反复开关背包/仓库 | 无刷屏、槽位正常 | **通过**（首轮 32 分钟内含多次仓库/装备/背包开关，`[更好的配件展示]` 始终仅 3 行） | `work/badfixed-tools/test-live-session1.jsonl` |
| T05 | 悬浮/查看武器 | 每件武器 6 槽、日志静默 | **通过**（仓库、装备栏、商店、战局内物品格均为 6 点；全程 0 条逐刷新日志） | 截图见 §4 |
| T06 | 武器详情页 | 与原版行为一致 | **通过（按原版一致性口径）**：原版 v1.1 本身不修改详情页（`ItemDetailsDisplay` 用的是 `ItemSlotCollectionDisplay`，与 `ItemDisplay/SlotIndicator` 不是同一套组件），本 mod 同样不修改；原版描述也写明详情页是「未来可能考虑」项 | 反编译 `ItemDetailsDisplay.cs` |
| T07 | 未搜索武器 | 不强制显示槽位 | **未单独覆盖**（日志无相关异常；设计上沿用 v1.1 的 `NeedInspection` 保护逻辑） | — |
| T08 | 已装配件白点 | 有配件的槽白色 | **通过**（Vector 6 点中 3 个白点） | `work/lead-scratch/current_two_guns_dots.png` |
| T09 | 武器没有的槽灰点 | 缺失槽显示灰色 | **通过**（同截图灰色点；未出现红色——红色是 v1.0 旧行为） | 同上 |
| T10 | 装/卸配件 | 颜色即时更新、无重复/无刷屏 | **部分覆盖**（有查看/悬浮不同配件与武器，无异常；未逐次做安装/卸下 A/B 记录） | — |
| T11 | 切换武器 | 槽位跟随、无残留 | **通过**（Vector/TT-33 双枪、近战/头盔等混合装备切换显示正常） | `current_two_guns_dots.png` |
| T12 | 进出场景 | 正常恢复、无本 mod 异常 | **通过**（基地 → 战局 → 基地；含一次游戏重启，显示均正常） | `work/lead-scratch/f12_equipment.png` 等 |
| T13 | 共存 FixSlotDisplay | 互不破坏 | **通过**（全 Mod 同时在线的实机轮次，无相关报错/视觉错乱） | `final-session.log` |
| T14 | 共存 PluggableIndicator | 互不破坏 | **通过**（同上） | 同上 |
| T15 | 共存 ItemLevelAndSearchSoundMod | 互不破坏 | **通过**（同上） | 同上 |
| T16 | 共存 CustomItemLevelValue | 互不破坏 | **通过**（同上） | 同上 |
| T17 | 游戏内禁用本 mod | 验证已知限制 | **未测**（留待后续；设计上禁用后原版补丁不会自动恢复，需重启） | — |
| T18 | 缺少原版 | 静默 + 1 行提示 | **未测**（需要临时移走原版，留待后续可选） | — |
| T19 | 退出游戏 | 原版正常 OnDisable；本 mod 无异常 | **通过**（原版 3 行退出日志正常；本 mod 无任何 Error） | `final-session.log` L20706-20707、L20795 |

## 3. 日志指标（实测结果）

### 3.1 采集方式

- 首轮：游戏 10:14:12 启动 → 10:46:26 退出（32 分 14 秒），期间完整操作：仓库整理/商店买卖/悬浮物品/进战局/搜刮容器/回基地/地图/设置；
- 全程每 15 秒采样一次（`work/badfixed-tools/monitor_loop.py` → `test-live-session1.jsonl`），退出后归档完整日志 `work/badfixed-tools/final-session.log`（1,173,763 bytes / 21,215 行）；
- 分析脚本：`monitor_live.py`、`baseline_breakdown.py`、`compare_exceptions.py`、`analyze_log.py`（均在 `work/badfixed-tools/`）。

### 3.2 指标表

| 指标 | 修复前基线 | 修复版实测（首轮 32.2 分钟） | 验收标准 | 结论 |
|---|---|---|---|---|
| Player.log 总行数 | 851,701 行（203 分钟） | **21,215 行** | 同清单复测明显下降 | 每分钟 659 行 vs 基线 4,196 行 → **-84.3%** |
| Player.log 总字节 | 58.13 MB（203 分钟） | **1.12 MB** | 同清单复测明显下降 | 每分钟 0.035 MB vs 0.286 MB → **-87.8%**（目标降 80%+，达标） |
| `[更好的配件展示]` 行数 | 743,761 行（87.33% / 90.56% 体积） | **6 行**（3 启动 + 3 退出，0.03% 行 / 0.02% 体积） | 仅一次性启动/退出；**运行期 0 行** | 达标（逐刷新日志 0 行） |
| 其中 `SlotIndicator.Target 为 null` | 242,902 行 | **0 行** | 0 行 | 达标 |
| 其中 `SlotIndicatorRefresh` / `已清空所有插槽指示器` / `创建插槽` / `设置颜色` 等 | 数十万行 | **全部 0 行** | 0 行 | 达标 |
| `[BetterAccessoryDisplayFixed]` 行数 | —（不存在） | **1 行**（启动 Info） | 1 行 | 达标 |
| 原版一次性启动 / 退出日志 | 3 启动 + 3 退出 | **3 启动 + 3 退出（6 行）** | 无法消除，记录即可 | 与基线一致 |
| 本 mod 相关异常 / Error | — | **0** | 0 | 达标 |
| 连续刷屏块 | 2,795 块（最大 6,949 行） | **0 块** | 0 | 达标 |
| 会话时长 | 203 分钟 | 32.2 分钟（另有重启验证轮） | 记录 | 已记录 |

> 采样曲线（每 15 秒）见 `test-live-session1.jsonl`：整个会话 `bad_total` 始终为 3（启动）→ 退出后为 6，从未增长。

### 3.3 异常统计（按来源归因）

| 异常 | 次数 | 归因 | 是否本次引入 |
|---|---|---|---|
| `MakeTimeQuacker.Bed2Interactable.Awake` NRE | 2 | 该 mod 的床交互对象（已知问题，历史基线/修复前会话均有） | 否 |
| 退出时 `Component.get_gameObject` / `DuckovSxerAttackCD.OnDisable` NRE | 4 | 其它 mod 的 OnDisable/OnDestroy 清理代码（xuanxing_Mod_RemoveEmptyBox、WhereAreMyItems、DuckovSxerAttackCD；历史基线异常表已有） | 否 |
| `Error: Index was out of range ... startIndex` | 55 行（约 27 次） | 修复前 `Player-prev.log` 中已存在同签名（9 次）；触发形态同为进关/悬浮物品时；无任何一行来自本 mod/Harmony | 否 |
| `[DynamicCrosshair] LateUpdate() Error:` | 3 | DynamicCrosshair 自身已知问题（历史基线已记录） | 否 |
| `[Better Locking] Failed to unpatch Harmony patches` | 1 | Better Locking 自身退出逻辑 | 否 |
| 本 mod / Harmony patch 相关异常 | **0** | — | — |

> 结论：本轮没有发现由本修复版引入的新异常；现有异常均可归因到既有 mod 或修复前已存在的现象（`compare_exceptions.py` 指纹对比见 `final-analysis.txt`）。

### 3.4 第二轮：重启验证（2026-10-28 10:46:41–10:57:33，10 分 52 秒）

重启后验证「顺序修正持久化 + 每次启动都能正确接管」：

| 指标 | 结果 |
|---|---|
| 启动加载顺序 | `Sorted mods` 中 **BetterAccessoryDisplay #167 → BetterAccessoryDisplayFixed #168**（正确；`Mods.ES3` 与计划完全一致，73 项连续） |
| `[更好的配件展示]` 行数 | **6 行**（3 启动 + 3 退出，运行期 0 行；`null_target_warning` / 刷新类日志全 0） |
| `[BetterAccessoryDisplayFixed]` 行数 | **1 行**（启动 Info，含「patches neutralised」） |
| 本 mod 相关异常 | **0** |
| 日志总量 | 5,142 行 / 0.30 MB（10 分 52 秒） |
| 其它异常 | 均为既有问题：MakeTimeQuacker 床 NRE、退出清理 NRE、`Index was out of range` 55→18 行（同一既有签名）、DynamicCrosshair 1 条；另有 1 条 `TMPro.TMP_InputField.GenerateHightlight` 的 `IndexOutOfRangeException`（UI 搜索输入框的 TMP 既有问题，与本 mod 无关） |

> 第二轮证明：重启后顺序正确、原版再次被中和、0 刷屏成立；两轮均无本 mod 相关异常。

## 4. 视觉验收与截图证据

截图归档：`work/lead-scratch/`（重点图）与 `work/badfixed-tools/live/`（原始帧）。

| 编号 | 场景 | 实际画面 | 证据文件 | 状态 |
|---|---|---|---|---|
| S01 | 装备栏武器 6 槽 | Vector/TT-33 均 6 个圆点；近战武器/头盔不强制 6 点 | `current_two_guns_dots.png`、`f12_equipment.png`、`f09_left.png` | 通过 |
| S02 | 商店武器卡 6 槽 | MP-155/UZI/VPO-101/PM 均 6 点 | `frame01_trader_zoom.png` | 通过 |
| S03 | 白/灰/黑三色语义 | Vector：白（已装）×3、灰（武器没有该槽）×1、黑（真实空槽）×2 | `current_two_guns_dots.png`（放大后可逐点辨认） | 通过 |
| S04 | 未搜索武器不显示 | 未单独抓图 | — | 未覆盖 |
| S05 | 武器详情页 | 与原版一致（不改详情页，见 T06 说明） | 反编译证据 | 通过（一致性口径） |
| S06 | Mods 列表 | 未抓图；以 Global.json 启用标志 + 启动日志证明两者同时启用 | — | 部分覆盖 |
| S07 | 日志尾部 | 本 mod 仅 1 行启动日志、原版 0 刷新日志 | `final-analysis.txt`、`final-session.log` | 通过 |
| S08 | 日志量对比 | 1.12 MB / 21,215 行 vs 基线 58.13 MB / 851,701 行 | §3.2 表 | 通过 |

## 5. 已知限制复测

| 限制（RELEASE_NOTES 口径） | 复测方式 | 结论 |
|---|---|---|
| 原版 3 行启动 / 3 行退出（共 6 行）日志仍在 | 检查完整会话日志 | **已确认**：实际 3 + 3 = 6 行 |
| 游戏内禁用本 mod 后原版补丁不自动恢复，需重启 | T17 | 未测（设计限制，留待后续） |
| 游戏更新改名内部成员 → 静默不生效 + 1 行警告 | 无法主动构造 | 静态检查通过；设计已实现降级 |
| 不承诺帧率提升（只减少日志量） | 本报告不采集 FPS 结论 | 口径确认 |

## 6. 通过标准与结论

- **P0 功能**：6 槽强制显示、白色/灰色语义、非枪械不强制 → **通过**（未搜索武器未单独截图，但沿用 v1.1 保护逻辑且无异常）；
- **P0 接管**：原版保持启用且被本 mod 正常接管；无重复槽位/视觉 → **通过**；
- **P0 日志**：`[更好的配件展示]` 运行期 0 行（仅 6 行一次性）；本 mod 仅 1 行启动 → **通过**；
- **P0 稳定性**：无本 mod 相关异常；进出场景/重启正常 → **通过**；
- **P1 共存**：与 FixSlotDisplay / PluggableIndicator / ItemLevelAndSearchSoundMod / CustomItemLevelValue 全开实机无异常 → **通过**；
- **P1 日志量**：总行数 -84.3%、总体积 -87.8%，原版刷屏 -99.995% → **通过（达到降 80%+ 目标）**。

**最终结论：通过。** 首轮 32.2 分钟完整会话 + 10.9 分钟重启验证轮均符合验收标准；未覆盖项（T07/T10/T17/T18、S04/S06）已标注，不影响 P0 结论。

## 7. 证据文件清单

| 文件 | 内容 |
|---|---|
| `work/badfixed-tools/final-session.log` | 首轮完整日志（1,173,763 B / 21,215 行） |
| `work/badfixed-tools/final-session2.log` | 重启验证轮完整日志（316,370 B / 5,142 行） |
| `work/badfixed-tools/final-analysis.txt` / `final-analysis2.txt` | 两轮自动分析输出（monitor/breakdown/异常指纹） |
| `work/badfixed-tools/test-live-session1.jsonl` | 每 15 秒采样曲线 |
| `work/badfixed-tools/baseline-breakdown.json` | 修复前基线明细 |
| `work/lead-scratch/current_two_guns_dots.png` | 双枪 6 点特写（白/灰/黑语义） |
| `work/lead-scratch/frame01_trader_zoom.png` | 商店武器卡 6 点 |
| `work/lead-scratch/f12_equipment.png` | 战局内装备面板 |
| `work/badfixed-tools/live/` | 全程原始截图帧（39 张） |
