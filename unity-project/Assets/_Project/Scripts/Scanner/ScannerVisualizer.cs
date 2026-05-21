using UnityEngine;

namespace SignalLost.Scanner
{
    /// <summary>
    /// Spawns a ping ring VFX at the scan origin every time a ping fires.
    /// Hook to ScannerSystem.onPingFired.
    /// </summary>
    public class ScannerVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject pingRingPrefab;
        [SerializeField] private float lifetime = 1.2f;

        public void OnPingFired(Vector3 worldPos)
        {
            if (pingRingPrefab == null) return;
            var go = Instantiate(pingRingPrefab, worldPos, Quaternion.identity);
            Destroy(go, lifetime);
        }
    }
}
