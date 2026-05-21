# 🎨 Visual Identity — Signal Lost

> **The visual brief.** This document exists so anyone who joins the team (artist, contractor, marketer) knows exactly what the game must look like — and what it must NOT look like. Locked by Creative Director after USP review.

---

## The One-Sentence Brief

> *"Iron Lung's claustrophobia, Mouthwashing's ugliness, Crow Country's PS1 reverence — running on a low-poly station that almost lets you forget you're in a horror game until the scanner pings the wrong shape."*

---

## Visual Pillars

### Pillar 1 — PS1/PSX aesthetic
- **Vertex jitter** on all geometry (Aubergine PSX Effects shader).
- **Affine texture mapping** (no perspective correction — the wobbly, warping look).
- **Dithered framebuffer** (16-bit colour reduction).
- **Pixelated render resolution** (render at 320×240 or 480×360, upscale to native).
- **CRT scanlines + slight curvature** on the final composite layer.

### Pillar 2 — Light is currency
- Lighting is **mostly dark**. The player-carried flashlight + scanner ping is the only readable light source 80% of the time.
- Baked GI for stations; real-time lights only on the player's flashlight and 1 emergency point per Safe Room.
- **No bloom-everywhere look.** This is dread, not Cyberpunk.

### Pillar 3 — Diegetic UI
- HUD elements feel like they're on a CRT visor — green wireframe, occasional signal interference glitch.
- Audio log text appears as if on an old terminal (monospace font + flicker).
- No floating waypoints. No minimap. No quest markers.

### Pillar 4 — Palette discipline
Each mission has a strict **3-colour palette** so screenshots read distinctly.

| Mission | Location | Palette |
|---|---|---|
| 1 | Kestrel-7 relay | **Cold blue-grey + sickly green (scan UI) + emergency red** (only when Carrier is in LOS) |
| 2 | Cold Front (ice rig) | Cold whites + ice blue + warning amber |
| 3 | Containment (bio lab) | Sterile white + bio-orange + black |
| 4 | Empty Vessel (colony ship) | Deep navy + sodium yellow + sickly green |
| 5 | The Origin | Inverted/unstable; every other palette flickers in |

---

## Reference Films / Games

- *Iron Lung* — Markiplier-era dread, claustrophobia, audio-driven horror
- *Mouthwashing* — intimacy + ugliness + no clean monsters
- *Crow Country* — PS1 mechanical reverence, scan-the-environment loop
- *Soma* — operator's last log = your survival manual
- *Alien* (1979) — sound design, slow reveal, hallway dread

## Anti-References (DO NOT LOOK LIKE THESE)

- ❌ **Lethal Company** — saturated low-poly cartoon. We are **NOT** this.
- ❌ **Content Warning / R.E.P.O.** — same Synty look. We are **NOT** this.
- ❌ **Cyberpunk 2077 / generic neon sci-fi** — bloom-heavy, busy. We are **NOT** this.
- ❌ **Generic asset-flip Steam horror** — overlit, busy props, Mixamo zombies. We are **NOT** this.

---

## Specific Technical Targets

| Surface | Treatment |
|---|---|
| All Synty meshes | Aubergine PSX vertex-jitter shader applied (configurable jitter strength per material) |
| Carrier character | **NOT Synty.** HauntedPS1 base mesh or commission. Strong silhouette. Slight texture corruption / glitch shader on top. |
| Player hands | Mixamo, with **affine texture mapping** so they read as PS1 |
| Lights | Cool blue baked + warm orange emergency strips + green CRT glow for scanner |
| Particles | Low-resolution sprite-based only; no GPU-fancy effects |
| UI | TextMesh Pro with bitmap fonts; monospace terminal aesthetic |
| Post-FX volume | Vignette (heavy), Film Grain (heavy), Colour Grading (cold + desaturated), Chromatic Aberration (subtle), CRT scanlines (composite layer) |

---

## Lighting Recipe (per scene)

```
1  × Baked Directional (very low intensity — emergency power only)
3-5 × Emergency strip lights (Real-time, baked-shadow, range 4m, cold-blue or sickly-green per palette)
1  × Real-time spotlight on player flashlight
0  × Other real-time lights
Volumetric Fog: density 0.04, height falloff, palette-tinted
Light Probes: dense in interaction zones only
```

## What "Done" Looks Like

**Side-by-side stranger test:**
1. Take a Mission 1 corridor screenshot.
2. Show it next to a Lethal Company corridor screenshot.
3. A non-gamer must be able to tell within **3 seconds** that they are different games in different genres.
4. If they can't → Visual Identity has failed. Iterate the PSX strength, character silhouette, or lighting palette.

This is the bar. The Creative Director will hold the bar.

---

## Status

✅ Locked by Creative Director. Any deviation requires a Critic Board review.
