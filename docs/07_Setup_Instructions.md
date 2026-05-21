# 🛠 Setup Instructions — Signal Lost

> *From `git clone` to pressing Play.* If you follow these steps in order, the only manual work is buying assets and dragging prefabs into the wiring slots described in section **§5**.

---

## §1 — Prerequisites

| Tool | Version |
|---|---|
| Unity Hub | latest |
| Unity Editor | **2022.3.40f1 LTS** (the project pins this version) |
| Git LFS | recommended for large binary commits later |
| A Unity ID with payment method | for Asset Store purchases |

## §2 — Clone & Open

```bash
git clone https://github.com/Abdulmalek-Agents/signal-lost.git
cd signal-lost
```

In Unity Hub → **Open → Add project from disk → select `signal-lost/unity-project/`.**

First open will take 3–8 min to compile. You will see warnings about missing Asset Store packages — that's normal until §3.

## §3 — Buy & Import Assets

Refer to [`04_Asset_List.md`](04_Asset_List.md). Buy & import:

1. **Synty POLYGON Sci-Fi Space** → places content under `Assets/Synty/POLYGON_SciFiSpace/`
2. **FPS Controller** of choice → `Assets/ThirdParty/FPSController/`
3. **Sci-Fi Industrial SFX Pack** → `Assets/ThirdParty/SFX_SciFi/`
4. Mixamo animations downloaded as FBX → `Assets/Mixamo/`

After importing, append these lines to your local `.gitignore` (NOT committed — Asset Store EULA forbids redistribution):

```
/unity-project/Assets/Synty/
/unity-project/Assets/Mixamo/
/unity-project/Assets/ThirdParty/
```

## §4 — Install Required Packages (already declared in `manifest.json`)

Open **Window → Package Manager** and verify these resolve:

- Universal Render Pipeline
- Input System
- Cinemachine
- Timeline
- Addressables
- TextMesh Pro
- Test Framework

If anything fails, restart Unity once.

## §5 — Wiring the Prefabs (one-time)

> Every script is already written. You only drag references in the Inspector.

### 5.1 — Create Master Prefabs

In `Assets/_Project/Prefabs/`, create (right-click → Prefab):

| Prefab name | Built from |
|---|---|
| `Player_FPS` | Your imported FPS controller rig + attach `PlayerController.cs`, `ScannerSystem.cs`, `InteractionSystem.cs` |
| `Carrier` | Synty humanoid + NavMeshAgent + attach `CarrierAI.cs` |
| `AnomalyNode` | Empty GameObject + collider trigger + attach `AnomalyNode.cs` |
| `SafeRoom` | Trigger volume + attach `SafeRoom.cs` |
| `AudioLogPickup` | Small mesh + attach `AudioLogPickup.cs` |
| `BootstrapManagers` | Empty + attach `Bootstrap.cs` + child `AudioManager`, `SaveSystem`, `UIController` |

### 5.2 — Create ScriptableObject Instances

In `Assets/_Project/Data/`:

- Right-click → Create → SignalLost → `MissionData` → name it `MissionData_Mission01`
- Right-click → Create → SignalLost → `CarrierBehaviourProfile` → name it `CarrierProfile_Default`
- Right-click → Create → SignalLost → `DifficultyProfile` → name it `Difficulty_Normal`
- Create `AnomalyNodeData` instances ×3 (Node01, Node02, Node03)
- Create `AudioLogData` instances ×7
- Create event channels: `OnAnomalyTriangulated`, `OnPlayerDied`, `OnSafeRoomEntered`, `OnMissionComplete`, `OnCarrierStateChanged`

Fill the inspector fields per the GDD (`02_GDD.md` §5 & §6).

### 5.3 — Build the Scene

Open `Assets/_Project/Scenes/Mission01_Kestrel.unity` (greybox provided — replace blockouts with Synty modular pieces from `Assets/Synty/POLYGON_SciFiSpace/Prefabs/`).

- Drop `BootstrapManagers` prefab.
- Drop `Player_FPS` at the `PlayerSpawn` marker.
- Place 1 × `Carrier` along the patrol path waypoints (`Carrier_Patrol_Waypoints`).
- Place 3 × `AnomalyNode` at the markers; assign each its `AnomalyNodeData`.
- Place 3 × `SafeRoom` triggers.
- Place 7 × `AudioLogPickup`s; assign each `AudioLogData`.

### 5.4 — NavMesh

**Window → AI → Navigation → Bake.** This generates the Carrier's walkable surface.

### 5.5 — Lighting

**Window → Rendering → Lighting → Generate Lighting.** Mixed mode is preset.

## §6 — Press Play

Press Play on **`Mission01_Kestrel`**. You should be able to walk, hold RMB to aim the scanner, click to ping, triangulate Anomaly Node 1, and trigger Audio Log 2 auto-play.

If anything is silent or broken, check the Console — every script writes a clear startup log line.

## §7 — Build a PC EXE

1. **File → Build Settings → PC, Mac & Linux Standalone** (Windows x64).
2. Add scenes in this order:
   1. `00_Bootstrap`
   2. `01_MainMenu`
   3. `02_Hub_PlayerShip`
   4. `Mission01_Kestrel`
3. **Player Settings → Other Settings → Graphics APIs → Direct3D11**.
4. **Quality:** URP-Performant.
5. Build → output to `/Builds/SignalLost_PC/`.

## §8 — Troubleshooting

| Symptom | Fix |
|---|---|
| "ScannerSystem missing reference" | Did you assign the SO event channels in §5.2? |
| Carrier never moves | NavMesh not baked (§5.4) |
| Black screen on play | Lighting not baked (§5.5) or URP asset not assigned in Graphics settings |
| Magenta materials | Synty materials not converted to URP — `Window → Rendering → Render Pipeline Converter` |
| No audio | Verify `AudioManager` is in the Bootstrap prefab and AudioMixer asset is assigned |

---

If you hit a wall, every script logs its setup state on `Awake()` — read the Console first.
