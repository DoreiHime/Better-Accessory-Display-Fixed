# 更好的配件展示 · 修复版 / Better Accessory Display (Fixed) - deploy helper
#
# Usage:
#   .\deploy-badfixed.ps1 -Mode Local                       # install as a local mod (Duckov_Data\Mods\BetterAccessoryDisplayFixed)
#   .\deploy-badfixed.ps1 -Mode Workshop -WorkshopId <id>   # replace the Workshop copy (backup kept)
#   .\deploy-badfixed.ps1 -Mode Restore -WorkshopId <id> -Backup <dir>
#
# Local installs are the recommended way to test before the Workshop item exists. The local copy
# has priority over the Workshop copy only if it is listed first in the in-game mod order; the
# same internal name must never be enabled twice (the game silently skips the duplicate).

param(
    [ValidateSet("Workshop", "Local", "Restore")][string]$Mode = "Local",
    [string]$GamePath = "",
    [string]$WorkshopId = "",
    [string]$Backup = "",
    [string]$Source = ""
)

$ErrorActionPreference = "Stop"
$RepoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$ModName = "BetterAccessoryDisplayFixed"

function Resolve-GamePath {
    if ($GamePath -ne "") { if (-not (Test-Path $GamePath)) { throw "Game path not found: $GamePath" }; return $GamePath }
    if ($env:DUCKOV_GAME -and (Test-Path $env:DUCKOV_GAME)) { return $env:DUCKOV_GAME }
    $candidates = @(
        "C:\Program Files (x86)\Steam\steamapps\common\Escape from Duckov",
        "C:\Program Files\Steam\steamapps\common\Escape from Duckov",
        "D:\Steam\steamapps\common\Escape from Duckov",
        "D:\SteamLibrary\steamapps\common\Escape from Duckov"
    )
    foreach ($c in $candidates) { if (Test-Path $c) { return $c } }
    throw "Escape from Duckov not found. Pass -GamePath."
}

function Assert-GameClosed {
    $proc = Get-Process -Name Duckov -ErrorAction SilentlyContinue
    if ($proc) { throw "Duckov.exe is running. Close the game before deploying (deploy writes into the game folder)." }
}

function Resolve-WorkshopDir {
    param([string]$game, [string]$id)
    if ($id -eq "") { throw "-WorkshopId is required for Workshop mode." }
    $steamApps = Split-Path -Parent (Split-Path -Parent $game)
    $candidate = Join-Path $steamApps "workshop\content\3167020\$id"
    if (Test-Path $candidate) { return $candidate }
    throw "Workshop content folder not found for item $id ($candidate)."
}

$game = Resolve-GamePath

if ($Mode -eq "Restore") {
    if ($Backup -eq "" -or -not (Test-Path $Backup)) { throw "Pass -Backup <backup folder>" }
    Assert-GameClosed
    $target = Resolve-WorkshopDir $game $WorkshopId
    Get-ChildItem $Backup -File | ForEach-Object {
        Copy-Item $_.FullName (Join-Path $target $_.Name) -Force
        Write-Host "restored $($_.Name)"
    }
    Write-Host "Restore done." -ForegroundColor Green
    exit 0
}

if ($Source -eq "") { $Source = Join-Path $RepoRoot "dist\$ModName" }
if (-not (Test-Path (Join-Path $Source "$ModName.dll"))) {
    throw "No build found in $Source. Run .\build-badfixed.ps1 first."
}

if ($Mode -eq "Workshop") {
    Assert-GameClosed
    $target = Resolve-WorkshopDir $game $WorkshopId
    $stamp = Get-Date -Format "yyyyMMdd-HHmmss"
    $backupDir = Join-Path $RepoRoot "backups\badfixed-workshop-$stamp"
    New-Item -ItemType Directory -Force -Path $backupDir | Out-Null
    foreach ($name in @("$ModName.dll", "info.ini", "preview.png", "LICENSE.txt")) {
        $old = Join-Path $target $name
        if (Test-Path $old) { Copy-Item $old (Join-Path $backupDir $name) }
    }
    Copy-Item (Join-Path $Source "$ModName.dll") (Join-Path $target "$ModName.dll") -Force
    Copy-Item (Join-Path $Source "info.ini") (Join-Path $target "info.ini") -Force
    foreach ($name in @("preview.png", "LICENSE.txt")) {
        $file = Join-Path $Source $name
        if (Test-Path $file) { Copy-Item $file (Join-Path $target $name) -Force }
    }
    Write-Host "Deployed to Workshop: $target" -ForegroundColor Green
    Write-Host "Backup: $backupDir"
} else {
    Assert-GameClosed
    $target = Join-Path $game "Duckov_Data\Mods\$ModName"
    New-Item -ItemType Directory -Force -Path $target | Out-Null
    Copy-Item (Join-Path $Source "$ModName.dll") (Join-Path $target "$ModName.dll") -Force
    Copy-Item (Join-Path $Source "info.ini") (Join-Path $target "info.ini") -Force
    foreach ($name in @("preview.png", "LICENSE.txt")) {
        $file = Join-Path $Source $name
        if (Test-Path $file) { Copy-Item $file (Join-Path $target $name) -Force }
    }
    Write-Host "Installed local mod: $target" -ForegroundColor Green
    Write-Host "Enable '$ModName' once in the in-game Mods list (it may be off by default for newly installed mods)."
}
