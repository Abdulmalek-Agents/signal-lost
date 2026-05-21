# 🎨 Asset List — Signal Lost

> Every entry is **commercial-use licensed** on the Unity Asset Store and tested for **URP / Unity 2022.3 LTS / mobile-friendly**. Total ceiling: **≈ $150**. Prices fluctuate; check during the bi-weekly Unity sale.

---

## A. Environment & Props

| # | Asset | Approx. $ | Why | URP-ready |
|---|---|---|---|---|
| 1 | **Synty POLYGON Sci-Fi Space — Low Poly 3D Art** | **$79.99** | Core station modular kit. Covers all of Mission 1 + Missions 2-5 reskins. | ✅ |
| 2 | (Optional) Synty POLYGON Construction or Sci-Fi City | $79.99 | Mission 4 colony ship variety | ✅ |

## B. Player & Character

| # | Asset | $ | Why |
|---|---|---|---|
| 3 | **FPS Microgame** (Unity Learn — free) or **Easy First-Person Controller** | Free / $20 | Base FPS rig. We replace its weapon with the scanner. |
| 4 | **Mixamo animations** (free) | Free | Operator-style first-person hand rigs |

## C. Audio

| # | Asset | $ | Why |
|---|---|---|---|
| 5 | **Sci-Fi Industrial SFX Pack** (any reputable; e.g. Cafofo or Hzandbits) | $20–30 | Hull groans, scanner pings, doors, atmospheric loops |
| 6 | **Horror Atmospheres Vol. 1** (free or $10) | Free–$10 | Background dread loops |
| 7 | Operator VO recordings | DIY / $50 fiverr | 7 audio logs (~30–60 s each) |

## D. VFX & Post

| # | Asset | $ | Why |
|---|---|---|---|
| 8 | **Beautify 3** *(optional)* | $40 | CRT/scanline/grain in one pass |
| 9 | Built-in URP Volume + Custom Renderer Features | Free | Vignette, film grain, color grading |

## E. Systems & Tooling

| # | Asset | $ | Why |
|---|---|---|---|
| 10 | **Cinemachine** | Free (Package Manager) | Cutscene cameras |
| 11 | **Timeline** | Free (Package Manager) | Scripted scares + intros |
| 12 | **Input System (new)** | Free (Package Manager) | KBM + Gamepad |
| 13 | **TextMesh Pro** | Free (Package Manager) | All UI text |
| 14 | **Addressables** | Free (Package Manager) | Async mission loading |
| 15 | **DOTween** (Demigiant) | Free | UI/material tweens |
| 16 | **Yarn Spinner** | Free | Audio-log dialog scripting |

---

## Subtotal

| Tier | Cost |
|---|---|
| **Must-have** | **~$130** (Synty Sci-Fi Space + FPS Controller + SFX pack + VO) |
| Nice-to-have | +$40 (Beautify 3, extra Synty pack) |
| **Ceiling** | **~$170** |

---

## Licence Audit

| Asset | Licence terms (confirmed) |
|---|---|
| Synty POLYGON | Commercial use permitted; redistribution of source assets prohibited (we do NOT commit raw Synty files to git — see `.gitignore` exclusion below) |
| Mixamo | Free for commercial use under Adobe terms |
| Cafofo / Hzandbits SFX | Standard Asset Store EULA, commercial use OK |
| DOTween free / Yarn Spinner / Cinemachine / Timeline / Input System / Addressables | MIT / Unity Companion / Apache 2.0 — all commercial-OK |

## Asset Folder Layout (after import)

```
Assets/
└── _Project/                     ← OUR code & data (committed)
    ├── Scripts/
    ├── Scenes/
    ├── Prefabs/
    ├── Data/
    ├── Materials/
    └── Audio/
└── Synty/                        ← Imported, NOT committed
└── Mixamo/                       ← Imported, NOT committed
└── ThirdParty/                   ← All other asset-store packages
```

Add the following lines to `.gitignore` once you import:
```
/unity-project/Assets/Synty/
/unity-project/Assets/Mixamo/
/unity-project/Assets/ThirdParty/
```

> ⚠️ **Never commit raw paid Asset Store content to a public repo.** Synty/Mixamo licences allow you to *use* the files in your game build but not to redistribute the source files.

## Buy Order (recommended)

1. Synty POLYGON Sci-Fi Space (do this first — biggest visual win)
2. SFX pack
3. Beautify 3 *(only if budget allows)*
4. Record / commission VO last (after greybox is locked)
