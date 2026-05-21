using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SignalLost.Data
{
    [CreateAssetMenu(menuName = "SignalLost/Audio Log Data", fileName = "AudioLog_")]
    public class AudioLogData : ScriptableObject
    {
        public string logId;
        public string title;
        public AssetReferenceT<AudioClip> clipReference;
        [TextArea(4, 10)] public string transcript;
        public bool isStoryCritical;
    }
}
