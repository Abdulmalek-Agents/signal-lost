# 🛰 Mission 1 — *Kestrel-7*

> Scene-by-scene breakdown of the vertical slice.

---

## Location

**Relay Station Kestrel-7.** A T-shaped orbital relay. Three wings: Comms (north), Crew Quarters (east), Engineering (west). All converge on the central **Atrium**.

## Layout (top-down, ASCII)

```
                  [ COMMS HUB ]
                       │
                  [ Safe Room C ]
                       │
                  [ N. Corridor ]
                       │
[ ENG. BAY ]── [  ATRIUM  ] ──[ CREW QUARTERS ]
       │           │                  │
       │      [ Safe Room A          [ Safe Room B ]
       │       (Airlock) ]            │
       │                              │
   [ Reactor ]                 [ Captain's Bunk ]
```

## Pacing Curve (intended emotional intensity, 0–10)

```
10 │                                            ╱╲
 9 │                                           ╱  ╲
 8 │                              ╱╲          ╱    ╲
 7 │                             ╱  ╲ ╱╲     ╱      ╲
 6 │                ╱╲          ╱    V  ╲   ╱        ╲
 5 │               ╱  ╲   ╱╲   ╱           ╲╱          ╲
 4 │        ╱╲   ╱    ╲ ╱  ╲ ╱
 3 │  ────╱   ╲ ╱      V
 2 │ ╱
 1 │╱
   └─────────────────────────────────────────────────────
    0  5  10 15 20 25 30 35 40 45  (min)
```

---

## Scene Breakdown

### Scene 0 — Pre-Mission Hub (Locker)
- Player Ship interior. Choose loadout (scanner upgrades equipped).
- Mission select console → confirm "Kestrel-7."
- 1 cinematic Timeline: undock animation, brief intro VO.

### Scene 1 — Airlock Arrival (Safe Room A)
- Tutorial gating. Required scan reveals door panel.
- HUD prompts surface (flashlight, scan, interact).
- First **Audio Log #1** (operator's first day, light tone — irony).

### Scene 2 — Atrium
- First open space. Carrier patrol audible from a far corridor (not visible).
- Two paths open: Crew Quarters (E) or Engineering (W).
- Either path leads to **Anomaly Node 1**.

### Scene 3 — Anomaly Node 1 (Crew Quarters Captain's Bunk)
- Triangulation tutorial: prompt shows angle requirement.
- Reward: Audio Log #2 — operator describes "hearing herself" on the comms.
- Trigger: Carrier transitions Dormant → Alerted on first triangulation.

### Scene 4 — Mid-Game Squeeze (N. Corridor)
- Narrow corridor. Carrier patrols. Player must time scans.
- **Safe Room C** is past the corridor (forward checkpoint).

### Scene 5 — Anomaly Node 2 (Comms Hub antechamber)
- Visually elevated stakes. Scan UI begins to glitch.
- Audio Log #3 — operator describes locking herself in Reactor.

### Scene 6 — Engineering Detour (optional)
- Locked door to Reactor. Code is in Audio Log #4 (Engineering side rooms).
- Optional but unlocks **Scanner Upgrade: Awareness Decay +25%** as a pickup.

### Scene 7 — Anomaly Node 3 (Reactor floor)
- Reactor environmental hazard: heat warning, red light, Carrier appears in scripted scare (not yet hostile in this scene).
- Audio Log #5 (the truth) plays during triangulation.

### Scene 8 — Comms Hub Broadcast
- Player returns to Comms Hub. Interact with broadcast console.
- Carrier transitions to Hunting (scripted).
- 30 s broadcast — player must hold position while Carrier closes.

### Scene 9 — Airlock Chase (Win Condition)
- Linear sprint back to Safe Room A.
- Lights flicker, doors slam (Timeline-driven set pieces).
- Reach airlock → cinematic ejection → Mission Complete screen.

---

## Triggers & Save Points

| Trigger ID | Type | Action |
|---|---|---|
| `T_Airlock_Open` | Volume | Disable tutorial, save checkpoint |
| `T_Atrium_Enter` | Volume | Start Carrier patrol |
| `T_Node1_Triangulated` | Event | Carrier → Alerted; unlock Comms door |
| `T_Node2_Triangulated` | Event | Carrier patrol path expands |
| `T_Node3_Triangulated` | Event | Reactor scripted scare |
| `T_Broadcast_Start` | Interact | Carrier → Hunting |
| `T_Broadcast_End` | Timer | Open airlock |
| `T_Airlock_Reached` | Volume | Mission Complete |

## Collectibles

| ID | Type | Location | Notes |
|---|---|---|---|
| AL_01 | Audio Log | Safe Room A | Tutorial pickup |
| AL_02 | Audio Log | Captain's Bunk | Auto-played on Node 1 |
| AL_03 | Audio Log | Comms antechamber | Auto-played on Node 2 |
| AL_04 | Audio Log | Engineering side room | Required for Reactor code |
| AL_05 | Audio Log | Reactor floor | Auto-played on Node 3 |
| AL_06 | Audio Log | Crew lounge | Optional |
| AL_07 | Audio Log | Atrium hidden vent | Optional |
| UP_01 | Scanner Upgrade | Engineering pickup | Awareness Decay +25% |

## Estimated Build Hours (post asset import)

| Task | Hours |
|---|---|
| Greybox layout | 8 |
| Asset dressing | 12 |
| AI patrol path setup | 4 |
| Triggers + Timeline | 6 |
| Audio logs + VO integration | 4 |
| Lighting + post-FX | 4 |
| Polish + bug pass | 10 |
| **Total** | **~48 hrs** |
