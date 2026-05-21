using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Mission Data", fileName = "MissionData_")]
    public class MissionData : ScriptableObject
    {
        [Header("Identity")]
        public string missionId;
        public string displayName;
        [TextArea] public string flavorText;

        [Header("Scene")]
        public AssetReference sceneReference;

        [Header("Gameplay")]
        public int anomalyCount = 3;
        public int audioLogCount = 7;
        public DifficultyProfile difficulty;
        public CarrierBehaviourProfile carrierProfile;

        [Header("Content")]
        public List<AnomalyNodeData> anomalies = new();
        public List<AudioLogData> audioLogs = new();

        [Header("Win/Lose")]
        public int maxDeaths = 3;
    }
}
