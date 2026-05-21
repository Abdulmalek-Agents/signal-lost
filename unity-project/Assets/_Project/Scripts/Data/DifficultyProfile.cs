using UnityEngine;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Difficulty Profile", fileName = "Difficulty_")]
    public class DifficultyProfile : ScriptableObject
    {
        [Header("Scanner")]
        [Tooltip("Awareness cost per scanner ping (0-1).")]
        [Range(0f, 1f)] public float pingAwarenessCost = 0.15f;
        [Tooltip("Awareness decay per second when scanner idle (0-1).")]
        [Range(0f, 1f)] public float awarenessDecayPerSec = 0.05f;

        [Header("Carrier")]
        public float globalCarrierSpeedMultiplier = 1f;
        public float carrierSightConeMultiplier = 1f;
    }
}
