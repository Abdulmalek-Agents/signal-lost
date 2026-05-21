using System.Collections.Generic;
using UnityEngine;
using SignalLost.Core;
using SignalLost.Data;

namespace SignalLost.Mission
{
    /// <summary>
    /// Authoritative mission driver. Reads MissionData, tracks anomalies triangulated,
    /// raises win/fail events. Scales to N missions by swapping MissionData.
    /// </summary>
    public class MissionManager : MonoBehaviour, IService
    {
        [SerializeField] private MissionData currentMission;

        [Header("Events In")]
        [SerializeField] private StringEventChannel onAnomalyTriangulated;
        [SerializeField] private VoidEventChannel onPlayerCaught;

        [Header("Events Out")]
        [SerializeField] private VoidEventChannel onMissionComplete;
        [SerializeField] private VoidEventChannel onMissionFailed;
        [SerializeField] private FloatEventChannel onMissionProgress; // 0..1

        public int AnomaliesTriangulated { get; private set; }
        public int DeathCount { get; private set; }
        public MissionData CurrentMission => currentMission;

        public void Register() => Services.Register(this);

        private readonly HashSet<string> _triangulated = new();

        private void OnEnable()
        {
            if (onAnomalyTriangulated != null) onAnomalyTriangulated.OnRaised += HandleAnomaly;
            if (onPlayerCaught != null) onPlayerCaught.OnRaised += HandleDeath;
        }
        private void OnDisable()
        {
            if (onAnomalyTriangulated != null) onAnomalyTriangulated.OnRaised -= HandleAnomaly;
            if (onPlayerCaught != null) onPlayerCaught.OnRaised -= HandleDeath;
        }

        private void HandleAnomaly(string nodeId)
        {
            if (!_triangulated.Add(nodeId)) return;
            AnomaliesTriangulated = _triangulated.Count;
            float p = currentMission != null && currentMission.anomalyCount > 0
                ? (float)AnomaliesTriangulated / currentMission.anomalyCount
                : 0f;
            onMissionProgress?.Raise(p);
            Debug.Log($"[MissionManager] Anomaly progress {AnomaliesTriangulated}/{currentMission.anomalyCount}");
            if (currentMission != null && AnomaliesTriangulated >= currentMission.anomalyCount)
            {
                Debug.Log("[MissionManager] All anomalies triangulated — broadcast unlocked.");
            }
        }

        private void HandleDeath()
        {
            DeathCount++;
            Debug.Log($"[MissionManager] Death {DeathCount}/{currentMission.maxDeaths}");
            if (currentMission != null && DeathCount >= currentMission.maxDeaths)
            {
                onMissionFailed?.Raise();
            }
        }

        public void CompleteMission()
        {
            onMissionComplete?.Raise();
            Debug.Log("[MissionManager] Mission complete.");
        }
    }
}
