using System.Collections.Generic;
using UnityEngine;
using SignalLost.Core;
using SignalLost.Data;
using SignalLost.Player;

namespace SignalLost.Scanner
{
    /// <summary>
    /// The core mechanic. Aims a directional cone, emits a ping, classifies hits, and feeds
    /// awareness into the CarrierAI via event channels. Pure C# logic; rendering is in ScannerVisualizer.
    /// </summary>
    public class ScannerSystem : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private InputReader input;
        [SerializeField] private Transform aimOrigin;          // usually the camera transform
        [SerializeField] private DifficultyProfile difficulty;
        [SerializeField] private LayerMask scanMask = ~0;

        [Header("Base Parameters (modified by upgrades)")]
        [SerializeField] private float baseRange = 25f;
        [SerializeField] private float baseConeDegrees = 60f;

        [Header("Events Out")]
        [SerializeField] private Vector3EventChannel onPingFired;
        [SerializeField] private FloatEventChannel onAwarenessChanged;
        [SerializeField] private StringEventChannel onAnomalyDetected;

        public float CurrentRange { get; private set; }
        public float CurrentConeDegrees { get; private set; }
        public float Awareness { get; private set; }  // 0..1

        private readonly List<ScannerUpgradeData> _equippedUpgrades = new();

        private void Awake()
        {
            RecomputeFromUpgrades();
        }

        private void OnEnable()
        {
            if (input != null) input.OnPingPressed += FirePing;
        }
        private void OnDisable()
        {
            if (input != null) input.OnPingPressed -= FirePing;
        }

        public void EquipUpgrade(ScannerUpgradeData u)
        {
            if (u == null || _equippedUpgrades.Contains(u)) return;
            _equippedUpgrades.Add(u);
            RecomputeFromUpgrades();
        }

        private void RecomputeFromUpgrades()
        {
            float r = baseRange, c = baseConeDegrees;
            foreach (var u in _equippedUpgrades)
            {
                r += u.rangeBonus;
                c += u.coneDegreesBonus;
            }
            CurrentRange = r;
            CurrentConeDegrees = c;
        }

        private void Update()
        {
            // Awareness decays when not actively pinging
            if (difficulty != null && Awareness > 0f)
            {
                Awareness = Mathf.Max(0f, Awareness - difficulty.awarenessDecayPerSec * Time.deltaTime);
                onAwarenessChanged?.Raise(Awareness);
            }
        }

        private void FirePing()
        {
            if (input == null || !input.ScannerAimHeld) return;
            if (aimOrigin == null) { Debug.LogWarning("[ScannerSystem] No aimOrigin set."); return; }

            // Awareness up
            if (difficulty != null)
            {
                float cost = difficulty.pingAwarenessCost;
                foreach (var u in _equippedUpgrades) cost = Mathf.Max(0f, cost - u.awarenessReduction);
                Awareness = Mathf.Clamp01(Awareness + cost);
                onAwarenessChanged?.Raise(Awareness);
            }

            // Broadcast ping origin (for CarrierAI to register a noise event)
            onPingFired?.Raise(aimOrigin.position);

            // Overlap a cone forward
            var origin = aimOrigin.position;
            var fwd = aimOrigin.forward;
            float halfAngleCos = Mathf.Cos(CurrentConeDegrees * 0.5f * Mathf.Deg2Rad);

            var hits = Physics.OverlapSphere(origin, CurrentRange, scanMask, QueryTriggerInteraction.Collide);
            foreach (var h in hits)
            {
                var toTarget = (h.bounds.center - origin).normalized;
                if (Vector3.Dot(toTarget, fwd) < halfAngleCos) continue;

                // Anomaly?
                if (h.TryGetComponent(out IScannable s))
                {
                    s.OnScanned(origin, fwd);
                    if (s is AI.AnomalyNode an) onAnomalyDetected?.Raise(an.NodeId);
                }
            }
        }
    }

    public interface IScannable
    {
        void OnScanned(Vector3 origin, Vector3 direction);
    }
}
