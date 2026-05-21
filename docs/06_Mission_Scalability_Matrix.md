# 📈 Mission Scalability Matrix — Signal Lost

Mission 1 ships with **all systems**. Missions 2–5 require **no new code** — only:
- New `MissionData` ScriptableObject
- New `CarrierBehaviourProfile` (tuning only)
- New Addressable scene
- New audio logs
- (Optional) one new ScriptableObject sub-mechanic per mission

| # | Title | Location | New Twist | Code Changes | Asset Reuse |
|---|---|---|---|---|---|
| 1 | **Kestrel-7** | Deep-space relay | Base scanner + Carrier | — | 100% Synty Sci-Fi Space |
| 2 | **Cold Front** | Ice mining rig | Cold meter (drains scanner battery faster) | Add 1 `ColdZoneVolume` script | Sci-Fi Space + a free Ice particle pack |
| 3 | **Containment** | Bio-research lab | Two Carrier variants simultaneously | None — just two `CarrierBehaviourProfile` instances | Sci-Fi Space + Synty Office reskin |
| 4 | **Empty Vessel** | Derelict colony ship | Zero-G zones (scan deflects) | Add 1 `ZeroGravityVolume` (physics override) | Reuse + minor Synty kits |
| 5 | **The Origin** | Source of the signal | Boss Carrier; multi-stage finale | None — multi-instance Carrier + Timeline | Reuse |

## Authoring Per-Mission Without Code

1. Right-click → `Create → SignalLost → MissionData`.
2. Fill in `MissionData_Mission02`: scene Addressable, anomaly count, difficulty, audio logs.
3. Duplicate `CarrierBehaviourProfile_Default` → adjust speeds / awareness curves.
4. Build Addressable bundle.
5. Add mission entry to `MissionDatabase` SO.
6. Mission appears in the Hub mission select console.

## Estimated Per-Mission Effort

| Task | Hours |
|---|---|
| Greybox new scene | 6 |
| Asset dress | 8 |
| Configure SOs | 1 |
| AI patrol paths | 2 |
| Triggers / Timeline | 3 |
| Audio logs + VO | 3 |
| Polish | 5 |
| **Total** | **~28 hrs/mission after #1** |

Mission 1 takes ~48 hrs because every system is built. Missions 2–5 amortise to ~28 hrs each.
