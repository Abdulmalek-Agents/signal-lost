using UnityEngine;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Scanner Upgrade", fileName = "Upgrade_")]
    public class ScannerUpgradeData : ScriptableObject
    {
        public string upgradeId;
        public string displayName;
        [TextArea] public string description;

        public float rangeBonus;
        public float coneDegreesBonus;
        public float awarenessReduction;
        public int unlocksAtMission = 1;
    }
}
