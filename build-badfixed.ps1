# 更好的配件展示 · 修复版 / Better Accessory Display (Fixed) - build script
#
# Usage:
#   .\build-badfixed.ps1                  # build only (dist\BetterAccessoryDisplayFixed)
#   .\build-badfixed.ps1 -DeployLocal     # + install as a local mod (Duckov_Data\Mods\BetterAccessoryDisplayFixed)
#   .\build-badfixed.ps1 -DeployWorkshop -WorkshopId 1234567890
#   .\build-badfixed.ps1 -GamePath "D:\Steam\...\Escape from Duckov"
#   .\build-badfixed.ps1 -HarmonyDll "...\0Harmony.dll"   # override the compile-time Harmony reference
#
# Requires: .NET SDK 8/9/10, an installed copy of Escape from Duckov, and an installed
# HarmonyLoadMod (3589088839) or BetterAccessoryDisplay (3596581923) that ships 0Harmony.dll.
# 0Harmony.dll is a compile-time reference only; it is never copied into the package.

param(
    [string]$Configuration = "Release",
    [string]$GamePath = "",
    [string]$HarmonyDll = "",
    [switch]$DeployLocal,
    [switch]$DeployWorkshop,
    [string]$WorkshopId = ""
)

$ErrorActionPreference = "Stop"
$RepoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectDir = Join-Path $RepoRoot "src\BetterAccessoryDisplayFixed"
$ProjectFile = Join-Path $ProjectDir "BetterAccessoryDisplayFixed.csproj"
$DistDir = Join-Path $RepoRoot "dist\BetterAccessoryDisplayFixed"
$ModName = "BetterAccessoryDisplayFixed"

function Write-Step($message) {
    Write-Host ""
    Write-Host "==> $message" -ForegroundColor Cyan
}

function Resolve-GamePath {
    if ($GamePath -ne "") {
        if (-not (Test-Path $GamePath)) { throw "Game path not found: $GamePath" }
        return $GamePath
    }
    $envGame = $env:DUCKOV_GAME
    if ($envGame -and (Test-Path $envGame)) { return $envGame }
    $candidates = @(
        "C:\Program Files (x86)\Steam\steamapps\common\Escape from Duckov",
        "C:\Program Files\Steam\steamapps\common\Escape from Duckov",
        "D:\Steam\steamapps\common\Escape from Duckov",
        "D:\SteamLibrary\steamapps\common\Escape from Duckov",
        "E:\SteamLibrary\steamapps\common\Escape from Duckov"
    )
    foreach ($c in $candidates) { if (Test-Path $c) { return $c } }
    throw "Escape from Duckov not found. Pass -GamePath or set DUCKOV_GAME."
}

function Resolve-HarmonyDll($game) {
    if ($HarmonyDll -ne "") {
        if (-not (Test-Path $HarmonyDll)) { throw "0Harmony.dll not found: $HarmonyDll" }
        return (Resolve-Path $HarmonyDll).Path
    }
    if ($env:HARMONY_DLL -and (Test-Path $env:HARMONY_DLL)) { return (Resolve-Path $env:HARMONY_DLL).Path }

    $steamApps = Split-Path -Parent (Split-Path -Parent $game)
    $candidates = @(
        (Join-Path $steamApps "workshop\content\3167020\3589088839\0Harmony.dll"), # HarmonyLoadMod (required)
        (Join-Path $steamApps "workshop\content\3167020\3596581923\0Harmony.dll"), # original mod (required)
        (Join-Path $game "Duckov_Data\Mods\HarmonyLoadMod\0Harmony.dll")
    )
    foreach ($c in $candidates) { if (Test-Path $c) { return $c } }

    # last resort: any installed mod folder that ships 0Harmony.dll
    $workshopRoot = Join-Path $steamApps "workshop\content\3167020"
    if (Test-Path $workshopRoot) {
        $hit = Get-ChildItem $workshopRoot -Directory | ForEach-Object { Join-Path $_.FullName "0Harmony.dll" } |
               Where-Object { Test-Path $_ } | Select-Object -First 1
        if ($hit) { return $hit }
    }
    throw "0Harmony.dll not found. Install HarmonyLoadMod (3589088839) / BetterAccessoryDisplay (3596581923), or pass -HarmonyDll."
}

$game = Resolve-GamePath
$managed = Join-Path $game "Duckov_Data\Managed"
if (-not (Test-Path (Join-Path $managed "TeamSoda.Duckov.Core.dll"))) {
    throw "Game assemblies not found under: $managed"
}
$harmony = Resolve-HarmonyDll $game

Write-Step "Building $Configuration (Harmony reference: $harmony)"
& dotnet build $ProjectFile -c $Configuration -p:GameManagedDir="$managed" -p:HarmonyDll="$harmony" --nologo
if ($LASTEXITCODE -ne 0) { throw "dotnet build failed (exit $LASTEXITCODE)" }

$dll = Join-Path $ProjectDir "bin\$Configuration\$ModName.dll"
if (-not (Test-Path $dll)) { throw "Build output not found: $dll" }

Write-Step "Assembling dist package"
if (Test-Path $DistDir) { Remove-Item $DistDir -Recurse -Force }
New-Item -ItemType Directory -Force -Path $DistDir | Out-Null
Copy-Item $dll (Join-Path $DistDir "$ModName.dll")
Copy-Item (Join-Path $ProjectDir "info.ini") (Join-Path $DistDir "info.ini")
$preview = Join-Path $RepoRoot "assets\BetterAccessoryDisplayFixed-preview.png"
if (-not (Test-Path $preview)) { $preview = Join-Path $RepoRoot "assets\preview.png" }  # GitHub repo layout
if (Test-Path $preview) { Copy-Item $preview (Join-Path $DistDir "preview.png") }
$license = Join-Path $RepoRoot "assets\BetterAccessoryDisplayFixed-LICENSE.txt"
if (-not (Test-Path $license)) { $license = Join-Path $RepoRoot "NOTICE.md" }             # GitHub repo layout
if (Test-Path $license) { Copy-Item $license (Join-Path $DistDir "LICENSE.txt") }

Write-Step "Verifying build"
& (Join-Path $RepoRoot "verify-badfixed.ps1") -Dll (Join-Path $DistDir "$ModName.dll") -PackageDir $DistDir
if ($LASTEXITCODE -ne 0) { throw "verify-badfixed.ps1 failed" }

Write-Host ""
Write-Host "Package ready: $DistDir" -ForegroundColor Green
Get-ChildItem $DistDir | Select-Object Name, Length | Format-Table -AutoSize

if ($DeployLocal) {
    & (Join-Path $RepoRoot "deploy-badfixed.ps1") -GamePath $game -Mode Local -Source $DistDir
}
if ($DeployWorkshop) {
    if ($WorkshopId -eq "") { throw "-WorkshopId is required with -DeployWorkshop (id of the new Workshop item)" }
    & (Join-Path $RepoRoot "deploy-badfixed.ps1") -GamePath $game -Mode Workshop -WorkshopId $WorkshopId -Source $DistDir
}
