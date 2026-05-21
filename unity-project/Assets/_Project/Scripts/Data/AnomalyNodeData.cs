using UnityEngine;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Anomaly Node Data", fileName = "Anomaly_")]
    public class AnomalyNodeData : ScriptableObject
    {
        public string nodeId;
        [Tooltip("Minimum angle between two scan directions required to lock-in (degrees).")]
        [Range(10f, 180f)] public float requiredAngleDegrees = 30f;
        public AudioLogData linkedAudioLog;
        [TextArea] public string nodeFlavor;
    }
}
