using UnityEngine;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Carrier Behaviour Profile", fileName = "CarrierProfile_")]
    public class CarrierBehaviourProfile : ScriptableObject
    {
        [Header("Movement")]
        public float dormantSpeed = 1.5f;
        public float alertedSpeed = 2.5f;
        public float huntingSpeed = 4.0f;

        [Header("Perception")]
        [Range(0f, 1f)] public float alertThreshold = 0.3f;
        [Range(0f, 1f)] public float huntThreshold = 0.7f;
        public float sightConeDegrees = 60f;
        public float sightRange = 12f;
        public float searchDurationOnLastPing = 20f;
        public float lostReturnToDormantSec = 30f;
    }
}
