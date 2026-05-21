using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using SignalLost.Core;
using SignalLost.Data;

namespace SignalLost.Audio
{
    /// <summary>
    /// Auto-plays audio logs when their id is raised on the channel.
    /// </summary>
    public class AudioLogPlayer : MonoBehaviour
    {
        [SerializeField] private StringEventChannel onAudioLogRequested;
        [SerializeField] private List<AudioLogData> registry = new();
        [SerializeField] private StringEventChannel onTranscriptShow;

        private void OnEnable()
        {
            if (onAudioLogRequested != null) onAudioLogRequested.OnRaised += Play;
        }
        private void OnDisable()
        {
            if (onAudioLogRequested != null) onAudioLogRequested.OnRaised -= Play;
        }

        private void Play(string logId)
        {
            var data = registry.Find(x => x.logId == logId);
            if (data == null) { Debug.LogWarning($"[AudioLogPlayer] No data for {logId}"); return; }
            if (!data.clipReference.RuntimeKeyIsValid())
            {
                Debug.LogWarning($"[AudioLogPlayer] Clip ref invalid for {logId}");
                return;
            }
            data.clipReference.LoadAssetAsync<AudioClip>().Completed += OnLoaded;
            onTranscriptShow?.Raise(data.transcript);

            void OnLoaded(AsyncOperationHandle<AudioClip> op)
            {
                if (op.Status != AsyncOperationStatus.Succeeded) return;
                Services.Get<AudioManager>()?.PlayVoice(op.Result);
            }
        }
    }
}
