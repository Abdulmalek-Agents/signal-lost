# 📡 Signal Lost

> *"The signal isn't coming from outside. It's coming from her."*

A short-form first-person sci-fi exploration-horror built in **Unity 2022 LTS / URP**. Single-player. PC-first, mobile-portable. **Visual lane: Mouthwashing / Crow Country / Iron Lung.**

**Status:** Pre-production — Mission 1 vertical-slice skeleton.

---

## Concept (one-line)

You are a junior signal-tech sent to fix a routine fault on a derelict deep-space relay. Your **directional scanner** is the only thing keeping you alive — but every scan pulls the **Carrier** closer.

## Core Verb Loop

**Scan → Triangulate → Move → Hide → Repeat**

## Vertical Slice — Mission 1

| | |
|---|---|
| Location | Relay Station *Kestrel-7* |
| Length | 30–45 min |
| Win | Triangulate 3 anomaly nodes → broadcast override → reach airlock |
| Lose | Carrier reaches player (3 respawns via Safe Rooms) |

Mission 1 ships with **all systems** required for Missions 2–5. Data-driven via ScriptableObjects.

---

## 📁 Documents

| File | Purpose |
|---|---|
| [`docs/01_Creative_Vision.md`](docs/01_Creative_Vision.md) | Creative Director's pitch |
| [`docs/02_GDD.md`](docs/02_GDD.md) | Full Game Design Document |
| [`docs/03_Mission1_Design.md`](docs/03_Mission1_Design.md) | Mission 1 scene-by-scene breakdown |
| [`docs/04_Asset_List.md`](docs/04_Asset_List.md) | Unity Asset Store shopping list (~$140) |
| [`docs/05_Architecture.md`](docs/05_Architecture.md) | Code architecture & scalability |
| [`docs/06_Mission_Scalability_Matrix.md`](docs/06_Mission_Scalability_Matrix.md) | 5-mission roadmap |
| [`docs/07_Setup_Instructions.md`](docs/07_Setup_Instructions.md) | Clone → buy assets → press Play |
| [`docs/08_Visual_Identity.md`](docs/08_Visual_Identity.md) | **Visual brief & USP differentiation (PSX hybrid)** |

---

## 🛠 Setup (TL;DR)

1. **Clone:** `git clone https://github.com/Abdulmalek-Agents/signal-lost.git`
2. **Open `unity-project/`** in Unity Hub. Unity will install version **2022.3.40f1 LTS**.
3. **Buy & import assets** (see [`docs/04_Asset_List.md`](docs/04_Asset_List.md)).
4. Follow [`docs/07_Setup_Instructions.md`](docs/07_Setup_Instructions.md) to wire prefabs.
5. Press Play on `Assets/_Project/Scenes/Mission01_Kestrel.unity`.

All C# is written and committed. Your only manual work is buying assets, dragging prefabs into the wiring slots described in `07_Setup_Instructions.md`, and pressing Play.

---

## 🧱 Tech Stack

- Unity 2022.3.40f1 LTS
- URP 14.x (mobile-grade)
- TextMesh Pro, Cinemachine, Timeline, Input System (new)
- ScriptableObject-driven data layer
- Object pooling for VFX/audio
- Addressables for mission loading
- **PSX/PS1 post-FX pipeline** (Aubergine PSX Effects) for visual USP

## 📜 Licence

Code: MIT. Asset Store assets remain under their original licences (commercial-use confirmed for every entry on the asset list).
