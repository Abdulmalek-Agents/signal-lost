using UnityEngine;
using SignalLost.Core;
using SignalLost.Data;
using SignalLost.Scanner;

namespace SignalLost.AI
{
    /// <summary>
    /// Triangulation target. Requires two scans from sufficiently different angles to lock.
    /// Place in scene, assign AnomalyNodeData and OnTriangulated event channel.
    /// </summary>
    public class AnomalyNode : MonoBehaviour, IScannable
    {
        [SerializeField] private AnomalyNodeData data;
        [SerializeField] private StringEventChannel onTriangulated; // raises nodeId
        [SerializeField] private GameObject revealedVfx;

        public string NodeId => data != null ? data.nodeId : name;
        public bool IsTriangulated { get; private set; }

        private Vector3 _firstScanDir;
        private bool _hasFirstScan;

        public void OnScanned(Vector3 origin, Vector3 direction)
        {
            if (IsTriangulated || data == null) return;

            if (!_hasFirstScan)
            {
                _firstScanDir = direction;
                _hasFirstScan = true;
                Debug.Log($"[AnomalyNode:{NodeId}] First scan registered.");
                return;
            }

            float angle = Vector3.Angle(_firstScanDir, direction);
            if (angle >= data.requiredAngleDegrees)
            {
                IsTriangulated = true;
                if (revealedVfx != null) revealedVfx.SetActive(true);
                onTriangulated?.Raise(data.nodeId);
                Debug.Log($"[AnomalyNode:{NodeId}] Triangulated at {angle:0}°.");
            }
        }
    }
}
