# 🗺️ Game Design Document — Signal Lost

**Version:** 1.0 (approved by Critic Board)
**Author:** Game & Level Designer
**Status:** Locked for Mission 1 implementation

---

## 1. Game Summary

- **Genre:** First-person psychological exploration horror
- **Players:** 1 (solo)
- **Mission length:** 30–45 min (Mission 1)
- **Platform:** PC primary (Steam), mobile-portable via touch controls
- **Engine:** Unity 2022.3.40f1 LTS, URP
- **Save model:** Per-mission, plus persistent scanner upgrades

## 2. Core Loop

```
Scan ─▶ Triangulate Anomaly Node
  ▲           │
  │           ▼
 Hide ◀──── Carrier reacts to scan
```

Tertiary loops:
- **Audio Log collection** (optional, story depth)
- **Scanner upgrade hunting** (persistent across missions)

## 3. Player Verbs

| Verb | Input (KBM) | Input (Gamepad) |
|---|---|---|
| Move | WASD | Left stick |
| Look | Mouse | Right stick |
| Sprint | Shift | L3 |
| Crouch | Ctrl | B |
| Flashlight toggle | F | Y |
| Scanner aim | RMB (hold) | LT (hold) |
| Scanner ping | LMB (while aiming) | RT |
| Interact | E | A |
| Pause | Esc | Start |

## 4. The Scanner (the central mechanic)

- **Directional cone**, 60° spread, range 25 m base.
- **Reveals:** Anomaly Nodes (gold), Audio Logs (blue), Safe Rooms (green), Carrier (red, only when scan touches it).
- **Cost:** Every ping increments the **Carrier's awareness meter** within range. Higher awareness = faster Carrier movement, shorter calm phase.
- **Upgrade slots:** Range, Cone Width, Awareness Decay Rate, Frequency Band (unlocks Mission 2+ targets).

## 5. The Carrier (antagonist)

| State | Behaviour |
|---|---|
| **Dormant** | Wanders patrol path. Slow. Cannot detect player without a scan ping or noise. |
| **Alerted** | Moves toward last scan ping. Searches 20 s, then returns to Dormant if no further pings. |
| **Hunting** | Direct line to player. Heard breathing audio. Faster. Triggers when awareness ≥ threshold OR line-of-sight. |
| **Lost** | Player broke LOS + entered Safe Room. Returns to Dormant after 30 s. |

Authored via `CarrierBehaviourProfile` ScriptableObject — Mission 2+ Carriers tweak speeds, awareness curves, and add new transitions (e.g. teleport, multi-instance).

## 6. Anomaly Nodes

Each Mission 1 node:
1. **Detect** — appear faintly on scan from distance.
2. **Triangulate** — player must scan from 2 distinct angles (≥ 30° apart) to lock position.
3. **Interact** — physical interact reveals audio log + advances story flag.

Three nodes = required to broadcast override signal at the Comms Hub.

## 7. Safe Rooms

- Player-revealed via scan.
- Entering halts Carrier awareness, restores breathing, auto-saves checkpoint.
- Mission 1: 3 Safe Rooms (start airlock, mid corridor, comms hub antechamber).

## 8. Audio Logs

Optional collectibles. 7 in Mission 1, each a 30–60 s monologue from the previous operator. Found by scanning blue-flagged hotspots.

## 9. Failure & Respawn

- 3 respawns. Each death respawns at the last entered Safe Room.
- On 3rd death → Mission Failed → restart from last manual checkpoint.

## 10. Difficulty Tuning (data-driven)

`MissionData.difficulty` ScriptableObject exposes:
- Carrier base speed, awareness decay, sight cone
- Scanner range, awareness cost per ping
- Anomaly count, node visibility falloff

Mission 2+ subclasses override these without code changes.

## 11. UI & Feedback

- **HUD:** Minimal. Flashlight icon, scanner battery, Carrier awareness arc (only visible when ≥ 30%).
- **Scan overlay:** CRT-style ping rings on world geometry.
- **Audio logs:** Subtitled, pausable, replayable from Logs menu.

## 12. Persistence

| Data | Scope |
|---|---|
| Mission progress | Per-mission save |
| Scanner upgrades | Persistent profile |
| Audio logs collected | Persistent profile |
| Settings (volume, sensitivity, subtitles) | Persistent |

## 13. Accessibility (MVP)

- Subtitles on by default.
- Mouse sensitivity slider.
- Aim-assist toggle for scanner.
- Hold-to-toggle option for sprint/crouch.
- Reduced motion option (disables head bob, screen shake).

## 14. Risks & Mitigations

| Risk | Mitigation |
|---|---|
| Players ignore scanner, soft-lock | Tutorial gate at airlock requires 1 scan |
| Carrier feels random/unfair | Awareness meter is *visible* once ≥ 30%, telegraphed |
| 45 min feels short | Mission 2 unlocks immediately; replay incentive via audio logs |
| Story too oblique | Mid-mission required log makes the arc legible |

## 15. Critic Board Sign-Off

✅ Approved with Notes addressed in Round 3. Cleared for implementation.
