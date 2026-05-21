using UnityEngine;
using SignalLost.Core;
using SignalLost.Data;

namespace SignalLost.AI
{
    /// <summary>
    /// Interactable in the world. On interact, raises an event with the AudioLog ID.
    /// </summary>
    public class AudioLogPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioLogData data;
        [SerializeField] private StringEventChannel onAudioLogPicked;
        [SerializeField] private GameObject visualOnPicked;

        public string InteractPrompt => "Pick up audio log";

        public void Interact()
        {
            if (data == null) return;
            onAudioLogPicked?.Raise(data.logId);
            if (visualOnPicked != null) visualOnPicked.SetActive(false);
            Debug.Log($"[AudioLogPickup] Picked {data.logId}");
        }
    }

    public interface IInteractable
    {
        string InteractPrompt { get; }
        void Interact();
    }
}
