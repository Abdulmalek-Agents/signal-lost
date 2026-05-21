# Changelog

All notable changes to **Signal Lost** are documented here. Follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and [SemVer](https://semver.org/).

## [Unreleased]

### Changed
- **Visual strategy pivoted to USP-aware hybrid** (post Critic Board re-review).
  - `docs/04_Asset_List.md`: dropped Synty Sci-Fi City entirely; added Aubergine PSX Effects (~$15) and HauntedPS1 community packs (~$10) as the USP differentiator. New budget: ~$140 (was ~$150).
  - Rationale: pure-Synty plan would have looked like Lethal Company / Content Warning / R.E.P.O. PSX hybrid plan places the game visually in the Mouthwashing / Crow Country / Iron Lung lane.
- `README.md`: updated budget figure, added Visual Identity doc link, added "Visual lane" tagline.

### Added
- `docs/08_Visual_Identity.md`: NEW. Visual brief, pillars, references, anti-references, technical targets, lighting recipe, "done" stranger test. Locked by Creative Director.
- Repository bootstrap.
- All original design docs (Creative Vision, GDD, Mission 1, Asset List, Architecture, Scalability, Setup).
- Unity 2022.3.40f1 LTS project skeleton (URP).
- Core systems C#: Bootstrap, ServiceLocator, MissionManager, ScannerSystem, CarrierAI, AnomalyNode, SafeRoom, PlayerController, SaveSystem, AudioManager, EventChannels.
- ScriptableObject definitions: MissionData, ScannerUpgradeData, AnomalyNodeData, CarrierBehaviourProfile, AudioLogData, DifficultyProfile.
- Asset folder structure under `Assets/_Project/`.
- Standard Unity .gitignore.

## [v0.1-mission1-skeleton] — TBD

First taggable checkpoint once assets are imported and Mission 1 scene is wired and playable end-to-end.
