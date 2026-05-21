using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SignalLost.Core;

namespace SignalLost.Save
{
    [Serializable]
    public class PersistentProfile
    {
        public List<string> unlockedScannerUpgradeIds = new();
        public List<string> collectedAudioLogIds = new();
        public float masterVolume = 1f;
        public float lookSensitivity = 0.12f;
        public bool subtitlesOn = true;
    }

    [Serializable]
    public class MissionSave
    {
        public string missionId;
        public int deaths;
        public List<string> triangulatedNodeIds = new();
        public string lastSafeRoomId;
    }

    public class SaveSystem : MonoBehaviour, IService
    {
        public PersistentProfile Profile { get; private set; } = new();
        public MissionSave CurrentMissionSave { get; private set; } = new();

        public void Register() => Services.Register(this);

        private string ProfilePath  => Path.Combine(Application.persistentDataPath, "profile.json");
        private string MissionPath  => Path.Combine(Application.persistentDataPath, "mission.json");

        private void Awake()
        {
            LoadProfile();
            LoadMission();
        }

        public void LoadProfile()
        {
            if (File.Exists(ProfilePath))
                Profile = JsonUtility.FromJson<PersistentProfile>(File.ReadAllText(ProfilePath)) ?? new();
            else Profile = new();
        }

        public void SaveProfile()
        {
            File.WriteAllText(ProfilePath, JsonUtility.ToJson(Profile, true));
            Debug.Log($"[SaveSystem] Profile saved → {ProfilePath}");
        }

        public void LoadMission()
        {
            if (File.Exists(MissionPath))
                CurrentMissionSave = JsonUtility.FromJson<MissionSave>(File.ReadAllText(MissionPath)) ?? new();
            else CurrentMissionSave = new();
        }

        public void SaveMission()
        {
            File.WriteAllText(MissionPath, JsonUtility.ToJson(CurrentMissionSave, true));
            Debug.Log($"[SaveSystem] Mission saved → {MissionPath}");
        }

        public void ClearMission()
        {
            CurrentMissionSave = new();
            if (File.Exists(MissionPath)) File.Delete(MissionPath);
        }
    }
}
