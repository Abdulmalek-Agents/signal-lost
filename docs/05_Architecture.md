# 👨‍💻 Architecture — Signal Lost

> Designed by the Senior Unity Developer.
> Designed to make Mission 1 ship *and* to make Missions 2–5 require **only data, art, and level layout** — zero new systems.

---

## 1. Architectural Pillars

1. **Data-driven** — Every tunable lives in a ScriptableObject. Code reads data; data is authored in the Inspector.
2. **Event-driven** — Systems never call each other directly. They publish to `ScriptableObject Event Channels`. Subscribers wire themselves up in the Editor.
3. **Service-located** — A lightweight `ServiceLocator` static registry replaces singletons. Easier to mock & swap.
4. **Async-loaded** — Missions are loaded via **Addressables**, not direct scene refs.
5. **Pooled** — Carrier VFX, audio one-shots, and UI ping rings come from `ObjectPool<T>` to keep GC flat.
6. **URP + Mobile-friendly** — Single-pass forward, baked + mixed lighting, ASTC textures.

---

## 2. Top-Level Layer Diagram

```
┌──────────────────────────────────────────────────────────────────┐
│                         APP / BOOT                               │
│  Bootstrap → ServiceLocator → SaveSystem → MainMenu              │
└──────────────────────────────────────────────────────────────────┘
            │
            ▼
┌──────────────────────────────────────────────────────────────────┐
│                       MISSION LAYER                              │
│  MissionManager (loads MissionData SO, drives win/fail)          │
│  CheckpointSystem · ObjectiveSystem · DialogueRunner             │
└──────────────────────────────────────────────────────────────────┘
            │
            ▼
┌──────────────────────────────────────────────────────────────────┐
│                       GAMEPLAY LAYER                             │
│  PlayerController · ScannerSystem · InteractionSystem            │
│  CarrierAI · AnomalyNode · SafeRoom · AudioLogPickup             │
└──────────────────────────────────────────────────────────────────┘
            │
            ▼
┌──────────────────────────────────────────────────────────────────┐
│                       INFRA / UTIL                               │
│  EventChannels (SO) · ObjectPool · AudioManager · UIController   │
│  InputReader (Input System) · SaveSystem · AddressablesLoader    │
└──────────────────────────────────────────────────────────────────┘
```

## 3. ScriptableObject Data Layer

| SO | Purpose | Fields |
|---|---|---|
| `MissionData` | One per mission | id, displayName, sceneAddressable, anomalyCount, difficultyProfile, audioLogIds[] |
| `CarrierBehaviourProfile` | One per Carrier variant | dormantSpeed, alertedSpeed, huntingSpeed, awarenessDecay, sightCone |
| `AnomalyNodeData` | One per node | nodeId, requiredAngle, linkedAudioLogId |
| `ScannerUpgradeData` | One per upgrade | name, rangeBonus, coneBonus, awarenessReduction, unlockMission |
| `AudioLogData` | One per log | id, title, clipReference (Addressable), transcript |
| `DifficultyProfile` | Re-used by Mission 2-5 | global scanner cost, carrier awareness curve |
| `VoidEventChannel`, `FloatEventChannel`, etc. | Event channels | — |

## 4. Event Channels

Decouples publishers from subscribers. Example:

- `OnAnomalyTriangulated` (`StringEventChannel`) — fires nodeId.
  - Subscribed: MissionManager (progress), CarrierAI (state escalation), AudioLogPlayer (auto-play), UIController (HUD update).

This means a new Mission 2 mechanic (e.g. "cold meter") subscribes to existing events *without* touching `CarrierAI` or `MissionManager` source.

## 5. Service Locator

```csharp
public static class Services {
    public static void Register<T>(T service);
    public static T Get<T>();
}
```

Registered services: `SaveSystem`, `AudioManager`, `InputReader`, `AddressablesLoader`, `UIController`, `MissionManager`.

## 6. Save System

- Three layers:
  - **PersistentProfile** (cross-mission): scanner upgrades unlocked, audio logs collected, settings.
  - **MissionSave** (per-mission checkpoint): scene state, anomalies triangulated, last safe room.
  - **Volatile** (runtime only): Carrier state.
- Format: JSON via `JsonUtility`, stored in `Application.persistentDataPath`.
- Save triggers: enter Safe Room, mission complete, manual via pause menu.

## 7. Addressables

- Each mission scene is an Addressable group: `Mission01_Kestrel.unity`, `Mission02_IceRig.unity`, etc.
- Audio logs are Addressable AudioClips loaded on demand.
- Main Menu and Hub scene are in the boot bundle.

## 8. Performance Budgets (Mobile-grade)

| Budget | Target |
|---|---|
| Frame time | 16.6 ms (60 fps) on Pixel 6 / iPhone 11 |
| Draw calls | < 150 |
| Tris on screen | < 250k |
| Texture memory | < 256 MB |
| Static batching | All non-animated geo |
| GPU instancing | On for repeated props |
| Lighting | Mixed (baked GI + 1 realtime directional + Light Probes) |
| Occlusion culling | Enabled per scene |
| Texture compression | ASTC 6×6 mobile, BC7 PC |
| Audio | Vorbis, streaming for clips > 200 KB |

## 9. Folder Layout (in Unity)

```
Assets/
└── _Project/
    ├── Scripts/
    │   ├── Core/             (Bootstrap, ServiceLocator, EventChannels)
    │   ├── Player/           (PlayerController, InputReader)
    │   ├── Scanner/          (ScannerSystem, ScannerUI, PingPool)
    │   ├── AI/               (CarrierAI, CarrierStateMachine)
    │   ├── Mission/          (MissionManager, CheckpointSystem, ObjectiveSystem)
    │   ├── Data/             (ScriptableObject definitions)
    │   ├── UI/               (HUDController, MenuController)
    │   ├── Audio/            (AudioManager, AudioLogPlayer)
    │   └── Save/             (SaveSystem, ProfileData)
    ├── Scenes/
    │   ├── 00_Bootstrap.unity
    │   ├── 01_MainMenu.unity
    │   ├── 02_Hub_PlayerShip.unity
    │   └── Mission01_Kestrel.unity
    ├── Prefabs/
    ├── Data/                 (ScriptableObject instances)
    ├── Materials/
    ├── Animations/
    └── Audio/
```

## 10. Test Strategy (lightweight)

- **Unit:** `ScannerSystem`, `CarrierStateMachine`, `SaveSystem` — pure C# tests in `Tests/Editor/`.
- **Play-mode smoke:** Boot → Hub → Mission 1 → Anomaly 1 triangulate → Mission Complete.
- **Performance:** Unity Profiler capture once per milestone; assert < 16.6 ms on mid Android.

## 11. CI (recommended, post-Mission-1)

- GitHub Actions + GameCI for headless Unity builds.
- Auto-build on `develop` push.

## 12. Critic Board Review

✅ Approved. Architecture demonstrably supports Mission 2-5 with **data-only** additions.
