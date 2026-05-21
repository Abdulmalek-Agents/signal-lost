# 🎨 Asset List — Signal Lost

> **Updated for USP differentiation.** The pure-Synty plan would have made the game read as a Synty asset-flip (Lethal Company / Content Warning / R.E.P.O. all use the same pack). The new plan keeps Synty as the cheap modular skeleton but **adds a PS1/PSX post-FX layer** so the game reads as *Mouthwashing / Crow Country / Iron Lung*.
>
> See [`08_Visual_Identity.md`](08_Visual_Identity.md) for the full visual brief.
>
> Total ceiling: **~$140** (cheaper than the original $150 plan, because we drop Sci-Fi City entirely).

---

## Strategy

**Hybrid:** Synty modular skeleton + PSX shader differentiator + non-Synty Carrier character + heavy URP post-FX.

---

## A. Environment & Props (modular skeleton)

| # | Asset | $ | Why | URP |
|---|---|---|---|---|
| 1 | **Synty POLYGON Sci-Fi Space** | $80 | Modular station interior (walls/floors/ceilings/doors/lift). The skeleton, not the skin. | ✅ |

> ⚠️ We do **NOT** buy Synty Sci-Fi City. Wrong setting (urban streets, vehicles), would dilute the asset budget for content we never use.

## B. THE USP DIFFERENTIATOR — PSX Post-FX

| # | Asset | $ | Why |
|---|---|---|---|
| 2 | **Aubergine PSX Effects** (Unity Asset Store) | ~$15 | Vertex jitter, affine texture mapping, dithering, CRT scanlines. **This is the entire reason the game stops looking like Synty and starts looking like Mouthwashing / Crow Country.** Non-negotiable. |
| 3 | **HauntedPS1 community asset bundles** (itch.io: HauntedPS1 demo disc community packs) | Free–$10 | Hundreds of distinctively PS1-style props + a non-Synty base mesh for the Carrier. Verify per-pack licence (most are CC0 or commercial-OK with attribution). |

## C. Player & Character

| # | Asset | $ | Why |
|---|---|---|---|
| 4 | **FPS Microgame** (Unity Learn — free) or **Easy First-Person Controller** | Free–$20 | Base FPS rig |
| 5 | **Mixamo animations** | Free | Operator-style first-person hand rigs |

> ⚠️ The **Carrier** mesh comes from HauntedPS1 packs (or a small commission), **NOT from Synty's Cryo Female**. This is critical — the Carrier silhouette is what players will see in screenshots and reaction videos.

## D. Audio

| # | Asset | $ | Why |
|---|---|---|---|
| 6 | **Sci-Fi Industrial SFX Pack** (Cafofo or Hzandbits) | $20–30 | Hull groans, scanner pings, doors, atmospheric loops |
| 7 | **Horror Atmospheres Vol. 1** (free or paid) | Free–$10 | Background dread loops |
| 8 | Operator VO (DIY / Fiverr commission) | $10–50 | 7 audio logs (~30–60 s each) |

## E. VFX & Post

| # | Asset | $ | Why |
|---|---|---|---|
| 9 | Built-in URP Volume + custom Renderer Features | Free | Vignette, film grain, color grading — layered ON TOP of PSX shader |
| 10 | (Optional) **Beautify 3** | $40 | CRT/scanline composite on top of PSX shader |

## F. Systems & Tooling

| # | Asset | $ | Why |
|---|---|---|---|
| 11 | Cinemachine, Timeline, Input System, Addressables, TextMesh Pro | Free | Built-in |
| 12 | DOTween (Demigiant) | Free | UI/material tweens |
| 13 | Yarn Spinner | Free | Audio-log dialog scripting |

---

## Subtotal

| Tier | Cost |
|---|---|
| **Must-have** | **~$140** (Synty Space + PSX Effects + HauntedPS1 + SFX + VO) |
| Nice-to-have | +$40 (Beautify 3) |
| **Recommended cap** | **~$140–180** |

---

## Licence Audit

| Asset | Licence terms (confirmed) |
|---|---|
| Synty POLYGON | Commercial use permitted; redistribution of source assets prohibited — do NOT commit raw Synty files to git |
| Aubergine PSX Effects | Standard Asset Store EULA, commercial use OK |
| HauntedPS1 community packs | Varies per pack — most are CC0 or "free for commercial use with attribution." **Verify each pack individually before shipping.** |
| Mixamo | Free for commercial use under Adobe terms |
| Cafofo / Hzandbits SFX | Standard Asset Store EULA, commercial OK |
| DOTween free / Yarn Spinner / Cinemachine / Timeline / Input System / Addressables | MIT / Unity Companion / Apache 2.0 — all commercial-OK |

## Asset Folder Layout (after import)

```
Assets/
├── _Project/                ← OUR code & data (committed)
├── Synty/                   ← Imported, NOT committed
├── HauntedPS1/              ← Imported, NOT committed
├── ThirdParty/PSXEffects/   ← Imported, NOT committed
├── ThirdParty/Audio/        ← Imported, NOT committed
└── Mixamo/                  ← Imported, NOT committed
```

Append post-import to your local `.gitignore`:
```
/unity-project/Assets/Synty/
/unity-project/Assets/HauntedPS1/
/unity-project/Assets/ThirdParty/
/unity-project/Assets/Mixamo/
```

> ⚠️ Never commit raw paid Asset Store content to a public repo. Synty/Mixamo licences allow you to *use* the files in your game build but not to redistribute the source files.

## Buy Order (recommended)

1. **Synty POLYGON Sci-Fi Space** ($80) — biggest immediate visual win for greybox dressing
2. **Aubergine PSX Effects** ($15) — install second; **this is what makes the game NOT look like Synty**
3. **HauntedPS1 community packs** (~$10) — for the Carrier mesh + distinctive props
4. **SFX pack** ($25)
5. **VO** last (after greybox is locked)

## Why This Beats Pure-Synty

| | Pure-Synty plan (rejected) | Hybrid plan (current) |
|---|---|---|
| Cost | ~$150 | **~$140** |
| Looks like Lethal Company? | ⚠️ Yes (same pack) | ❌ No (PSX shader breaks the chain) |
| Looks like Mouthwashing / Crow Country? | ❌ No | ✅ Yes |
| Carrier silhouette distinct? | ⚠️ Synty Cryo Female is generic | ✅ HauntedPS1 mesh is unmistakeable |
| Mobile-port-able later? | ✅ | ✅ (PSX shader is mobile-friendly by design) |
