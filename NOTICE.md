# NOTICE — third-party attribution

## Original mod

**更好的配件展示 / Better Accessory Display**

- Author: **我是火柴人 / chardexn**
- Workshop: <https://steamcommunity.com/sharedfiles/filedetails/?id=3596581923>
- Source: <https://github.com/Chardexn/BetterAccessoryDisplay>
- Licence: MIT License, Copyright (c) 2025 Kirk (see below)

This repository is an **independent re-implementation** of the behaviour of that mod, made
because the original hard-codes its debug logging and rebuilds all slot indicators on every
refresh. It is **not** a release by the original author. The original author keeps all credit,
all Workshop downloads and every right in their own work.

**No original files are included in this repository or in the released mod package**
(no original DLL, no original `preview.png`, no original source files were copied).

The released `assets/preview.png` is the original Workshop cover image with a
“修复版 / FIXED” badge added, at the request of the fixed version's author; credit for the base
image belongs to the original author.

## Original MIT licence

```
MIT License

Copyright (c) 2025 Kirk

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Prerequisites (not included in this repository or in the mod package)

- **HarmonyLoadMod** (Workshop 3589088839) — provides the Harmony runtime `0Harmony.dll`.
  The Harmony runtime is referenced at compile time only and is never bundled.
- **BetterAccessoryDisplay** (Workshop 3596581923) — the original mod; required and must stay
  installed and enabled.
