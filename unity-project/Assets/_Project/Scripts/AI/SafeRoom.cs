using UnityEngine;
using SignalLost.Core;

namespace SignalLost.AI
{
    /// <summary>
    /// Trigger zone: when player enters, awareness resets, checkpoint saves, optional VO plays.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SafeRoom : MonoBehaviour
    {
        [SerializeField] private string safeRoomId;
        [SerializeField] private VoidEventChannel onSafeRoomEntered;
        [SerializeField] private FloatEventChannel resetAwarenessChannel;

        private void Reset()
        {
            var c = GetComponent<Collider>();
            if (c != null) c.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            resetAwarenessChannel?.Raise(0f);
            onSafeRoomEntered?.Raise();
            Debug.Log($"[SafeRoom:{safeRoomId}] Player entered.");
        }
    }
}
