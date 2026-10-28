# 更好的配件展示 · 修复版 / Better Accessory Display (Fixed) - build verification
#
# Unlike verify-meleecd.ps1 this mod DOES have one third-party compile-time reference:
#   0Harmony  (the Harmony runtime, provided at runtime by the required HarmonyLoadMod)
# Everything else must still be a game / Unity / netstandard assembly, and the package must
# NOT contain 0Harmony.dll, the original mod's files, or any other prerequisite DLL.

param(
    [Parameter(Mandatory = $true)][string]$Dll,
    [string]$PackageDir = ""
)

# Note: "Continue" (not "Stop") because ilspycmd writes an unrelated update notice to stderr;
# all real failures are collected explicitly and turned into a non-zero exit code below.
$ErrorActionPreference = "Continue"
$failures = New-Object System.Collections.Generic.List[string]

if (-not (Test-Path $Dll)) { throw "DLL not found: $Dll" }
$size = (Get-Item $Dll).Length
Write-Host "DLL: $Dll ($size bytes)"
if ($size -lt 8000) { $failures.Add("DLL looks too small ($size bytes)") }

# --- package contents: only our own files, no prerequisite DLLs -------------------------
if ($PackageDir -ne "") {
    if (-not (Test-Path $PackageDir)) { throw "PackageDir not found: $PackageDir" }
    $allowed = @("BetterAccessoryDisplayFixed.dll", "info.ini", "preview.png", "LICENSE.txt")
    foreach ($file in Get-ChildItem $PackageDir -File) {
        if ($allowed -notcontains $file.Name) {
            $failures.Add("Unexpected file in package: $($file.Name)")
        }
    }
    foreach ($required in @("BetterAccessoryDisplayFixed.dll", "info.ini")) {
        if (-not (Test-Path (Join-Path $PackageDir $required))) { $failures.Add("Missing package file: $required") }
    }
    foreach ($banned in @("0Harmony.dll", "HarmonyLib.dll", "BetterAccessoryDisplay.dll", "preview-original.png")) {
        if (Test-Path (Join-Path $PackageDir $banned)) { $failures.Add("Prerequisite/original file must not be packaged: $banned") }
    }
    $ini = Join-Path $PackageDir "info.ini"
    if (Test-Path $ini) {
        $iniText = Get-Content $ini -Raw
        if ($iniText -notmatch "(?m)^name\s*=\s*BetterAccessoryDisplayFixed\s*$") {
            $failures.Add("info.ini name is not BetterAccessoryDisplayFixed")
        }
        Write-Host "OK: info.ini name = BetterAccessoryDisplayFixed"
    }
    Write-Host "OK: package contains only the mod's own files (no prerequisites)"
}

# --- IL checks -------------------------------------------------------------------------
$ilspy = Join-Path $env:USERPROFILE ".dotnet\tools\ilspycmd.exe"
if (-not (Test-Path $ilspy)) {
    $cmd = Get-Command ilspycmd -ErrorAction SilentlyContinue
    if ($cmd) { $ilspy = $cmd.Source }
}
if (-not (Test-Path $ilspy)) {
    $failures.Add("ilspycmd not found; cannot verify types. Install with: dotnet tool install -g ilspycmd")
} else {
    $types = (& $ilspy -l c $Dll 2>&1 | Out-String)
    if ($types -notmatch "Class BetterAccessoryDisplayFixed\.ModBehaviour") {
        $failures.Add("Type BetterAccessoryDisplayFixed.ModBehaviour not found")
    } else {
        Write-Host "OK: BetterAccessoryDisplayFixed.ModBehaviour found"
    }

    $il = (& $ilspy --ilcode -t "BetterAccessoryDisplayFixed.ModBehaviour" $Dll 2>&1 | Out-String)
    if ($il -notmatch "extends \[TeamSoda\.Duckov\.Core\]Duckov\.Modding\.ModBehaviour") {
        $failures.Add("ModBehaviour does not extend Duckov.Modding.ModBehaviour")
    } else {
        Write-Host "OK: entry point extends Duckov.Modding.ModBehaviour"
    }

    # Assembly references: game / Unity / System / netstandard + the single allowed
    # third-party runtime reference 0Harmony (provided by the required HarmonyLoadMod).
    $refs = (& $ilspy --dump-table AssemblyRef $Dll 2>&1 | Out-String)
    $allowedExact = @("netstandard", "mscorlib", "System", "System.Core", "System.Runtime",
                      "TeamSoda.Duckov.Core", "TeamSoda.Duckov.Utilities", "ItemStatsSystem",
                      "SodaLocalization", "TeamSoda.MiniLocalizor", "Plugins", "0Harmony",
                      "Unity.TextMeshPro", "Unity.InputSystem", "UnityEngine.UI", "DynLib")
    $bad = New-Object System.Collections.Generic.List[string]
    $seen = New-Object System.Collections.Generic.List[string]
    foreach ($line in ($refs -split "`r?`n")) {
        $m = [regex]::Match($line, '^\d+\s+0x[0-9A-Fa-f]+\s+[\d.]+\s+\S+\s+\S+\s+(\S+)')
        if (-not $m.Success) { continue }
        $name = $m.Groups[1].Value
        if (-not $seen.Contains($name)) { $seen.Add($name) }
        $ok = ($allowedExact -contains $name) -or $name.StartsWith("UnityEngine.") -or $name.StartsWith("System.")
        if (-not $ok) { $bad.Add($name) }
    }
    if ($bad.Count -gt 0) {
        foreach ($b in $bad) { $failures.Add("Unexpected assembly reference: $b") }
    } elseif ($seen -contains "0Harmony") {
        Write-Host "OK: references game/Unity/System assemblies + 0Harmony (compile-time only, not packaged)"
    } else {
        Write-Host "OK: references only game / Unity / System assemblies"
    }
}

if ($failures.Count -gt 0) {
    Write-Host ""
    foreach ($f in $failures) { Write-Host "FAIL: $f" -ForegroundColor Red }
    exit 1
}
Write-Host ""
Write-Host "Verification passed." -ForegroundColor Green
exit 0
