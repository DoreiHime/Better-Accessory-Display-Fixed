# 更好的配件展示 · 修复版

[English](README.md) | [简体中文](README.zh-CN.md)

《逃离鸭科夫》（Steam appid 3167020，游戏 2.3.30）原版 mod **「更好的配件展示 / Better Accessory Display」**
的**独立修复版**。保留原版「所有武器强制显示 6 个配件槽」的视觉效果，去掉它写死的调试日志刷屏：
原版每次槽位刷新会打约 48 行 `Debug.Log`（无开关），本版启动后只打 **1 行**启动日志，刷新时 0 行。

> 原版 mod **保持安装并保持启用**。本修复版只在运行时摘除原版的 Harmony 补丁，然后用干净代码
> 重新实现同样的视觉；不会复制、修改或打包原版的任何文件，原版的署名与工坊下载量都归原作者。

## 特性

- **与原版 v1.1 相同的视觉**——每把武器强制显示 6 个配件槽，顺序为 `Scope / Muzzle / Grip / Stock / Tec / Mag`；
  已装配件的槽显示**白色**指示点，武器没有的槽显示**灰色**，真实空槽保持游戏原生表现，
  未搜索武器不强制显示槽位。
- **0 刷屏**——作者本机实测：原版一次 3 小时 23 分会话产生 **743,761 行 / 55.2 MB** 日志
  （占全部行数 87.33%、体积 90.56%）；修复后 72 个 mod 全开的 32 分钟会话里，原版只剩
  **6 行一次性启动/退出日志（3 启动 + 3 退出）**，运行期 **0 行**；本修复版只有 **1 行**启动日志。
  整份日志从 851,701 行 / 58.13 MB 降到 21,215 行 / 1.12 MB（同一环境）。
- **更轻的热路径**——`ItemDisplay.slotIndicatorContainer`、`ItemDisplay.SlotIndicatorPool`、
  `SlotIndicator.contentIndicator` 三个反射成员只解析一次并静态缓存。
- **按需重建**——槽位指示器只在真正需要时重建（每个 `ItemDisplay.Setup` 一次），不像原版每次 `Refresh` 都清空重建。
- **失效安全**——若未来游戏更新改掉内部 UI 成员，本 mod 会保持不生效并只输出 1 行警告，不会崩溃、不会刷屏。

## 前置要求（两个都是工坊「必需物品」）

| 必需物品 | 工坊 ID | 说明 |
|---|---|---|
| [HarmonyLoadMod](https://steamcommunity.com/sharedfiles/filedetails/?id=3589088839) | 3589088839 | 提供 Harmony 运行时（`0Harmony.dll`） |
| [BetterAccessoryDisplay（更好的配件展示）](https://steamcommunity.com/sharedfiles/filedetails/?id=3596581923) | 3596581923 | 原版；必须保持**安装并启用** |

两个前置都不包含在本仓库或发布包内。加载顺序：`HarmonyLoadMod → BetterAccessoryDisplay → BetterAccessoryDisplayFixed`。
新订阅的 mod 默认排在最后，一般无需手动操作。若未安装原版，本 mod 保持完全不激活并只输出 1 行友好提示（不弹窗、不崩溃）。

## 安装

1. 在 Steam 创意工坊订阅本 mod 以及两个必需物品；
2. 在游戏 Mods 列表里把三者都启用（原版必须保持启用）；
3. 重启游戏，本修复版排在两个前置之后即可。

Steam Workshop: <https://steamcommunity.com/sharedfiles/filedetails/?id=3828601227>

## 构建

需要 .NET SDK 8/9/10、已安装的《逃离鸭科夫》，以及已安装的 HarmonyLoadMod 或原版（它们自带 `0Harmony.dll`）。

```powershell
# 构建 + 校验（0Harmony 仅是编译期引用，不会复制到输出）
.\build-badfixed.ps1

# 构建 + 作为本地 mod 安装（Duckov_Data\Mods\BetterAccessoryDisplayFixed）
.\build-badfixed.ps1 -DeployLocal

# 显式指定路径
.\build-badfixed.ps1 -GamePath "D:\Steam\steamapps\common\Escape from Duckov" -HarmonyDll "C:\...\0Harmony.dll"
```

`verify-badfixed.ps1` 会检查入口类型与继承、程序集引用（仅游戏/Unity/System + 编译期 `0Harmony`），
并确认发布包里没有前置 DLL 或原版文件。

## 项目结构

```
build-badfixed.ps1        构建 / 打包 / 校验
deploy-badfixed.ps1       安装为本地 mod（或替换工坊副本，带备份）
verify-badfixed.ps1       对产物 DLL 与发布包做静态校验
src/BetterAccessoryDisplayFixed/
  BetterAccessoryDisplayFixed.csproj
  info.ini                工坊元数据（name/displayName/description/version/tags）
  ModBehaviour.cs         入口：依赖检查 → Harmony 运行时 → 自己的补丁 → 中和原版
  Core/                   反射缓存、Harmony 桥、原版守卫、槽位重建/着色逻辑
  Patches/                三个 Harmony 补丁（ItemDisplay.Setup/Refresh、SlotIndicator.Refresh）
docs/RELEASE_NOTES.md     发布说明（中文 + 英文摘要）
docs/TEST_REPORT.md       构建/静态/离线 + 实机测试报告（含实测数字）
docs/WORKSHOP_DESCRIPTION.md      英文工坊描述
docs/workshop-localizations.md    10 语种工坊标题/描述
docs/workshop-page-preview.png    工坊页面版式预览图
assets/preview.png        工坊封面（原版封面 + 修复版角标）
LICENSE                   MIT
NOTICE.md                 原版署名与许可
```

> 发布说明/测试报告里 `work/...` 开头的路径是作者本地测试工作区的归档路径，不随本仓库发布。

## 测试

- .NET SDK 10.0.302，`Release` 构建 0 warning / 0 error；静态校验通过。
- 实机（2026-10-28）两轮：首轮 32 分 14 秒（作者 72 个 mod 全开）+ 10 分 52 秒重启验证轮。
- 结果：原版仍启用且被正确中和，**运行期 0 行刷屏**，本修复版仅 1 行启动日志，无本 mod 相关报错，
  视觉与原版 v1.1 一致（详见 `docs/TEST_REPORT.md`）。

## 致谢与许可

- 原版 **「更好的配件展示 / Better Accessory Display」**作者：**我是火柴人 / chardexn** —
  <https://github.com/Chardexn/BetterAccessoryDisplay>，MIT 许可。
- 本修复版是**独立重实现，不是原作者发布**，不包含任何原版文件；原版的署名与工坊下载量归原作者所有。
- 封面图在原版工坊封面基础上加了「修复版 / FIXED」角标（依修复版作者要求），底图版权归原作者。
- 修复版作者：**Dorei Hime**，同样以 MIT 许可发布（见 `LICENSE` 与 `NOTICE.md`）。
