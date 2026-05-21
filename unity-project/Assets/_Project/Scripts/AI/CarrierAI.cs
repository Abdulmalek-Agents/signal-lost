using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SignalLost.Core;
using SignalLost.Data;

namespace SignalLost.AI
{
    /// <summary>
    /// Carrier antagonist. State-machine driven, reacts to scanner pings and player presence.
    /// All behaviour tuned via CarrierBehaviourProfile SO.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class CarrierAI : MonoBehaviour
    {
        public enum State { Dormant, Alerted, Hunting, Lost }

        [Header("Profile")]
        [SerializeField] private CarrierBehaviourProfile profile;
        [SerializeField] private DifficultyProfile difficulty;

        [Header("Patrol")]
        [SerializeField] private Transform[] patrolWaypoints;

        [Header("Sensing")]
        [SerializeField] private Transform eyes;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private float playerDetectionDistance = 12f;

        [Header("Events In")]
        [SerializeField] private Vector3EventChannel onScannerPing;
        [SerializeField] private FloatEventChannel onAwarenessChanged;

        [Header("Events Out")]
        [SerializeField] private StringEventChannel onStateChanged;
        [SerializeField] private VoidEventChannel onPlayerCaught;

        private NavMeshAgent _agent;
        private State _state = State.Dormant;
        private int _waypointIndex;
        private float _searchTimer;
        private float _awareness;
        private Vector3 _lastKnownPlayerPos;
        private Transform _playerTransform;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            if (onScannerPing != null) onScannerPing.OnRaised += HandlePing;
            if (onAwarenessChanged != null) onAwarenessChanged.OnRaised += a => _awareness = a;
        }

        private void OnDisable()
        {
            if (onScannerPing != null) onScannerPing.OnRaised -= HandlePing;
        }

        private void Start()
        {
            if (profile == null)
            {
                Debug.LogError("[CarrierAI] CarrierBehaviourProfile is not assigned.");
                enabled = false;
                return;
            }
            ApplySpeed();
            GoToNextWaypoint();
        }

        private void Update()
        {
            if (profile == null) return;

            // Awareness-based escalation
            if (_state == State.Dormant && _awareness >= profile.alertThreshold) SetState(State.Alerted);
            if (_state == State.Alerted && _awareness >= profile.huntThreshold) SetState(State.Hunting);

            switch (_state)
            {
                case State.Dormant: TickDormant(); break;
                case State.Alerted: TickAlerted(); break;
                case State.Hunting: TickHunting(); break;
                case State.Lost:    TickLost();    break;
            }

            CheckLineOfSightToPlayer();
        }

        private void TickDormant()
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f) GoToNextWaypoint();
        }

        private void TickAlerted()
        {
            _searchTimer += Time.deltaTime;
            if (_searchTimer >= profile.searchDurationOnLastPing) SetState(State.Dormant);
        }

        private void TickHunting()
        {
            if (_playerTransform != null) _agent.SetDestination(_playerTransform.position);
        }

        private void TickLost()
        {
            _searchTimer += Time.deltaTime;
            if (_searchTimer >= profile.lostReturnToDormantSec) SetState(State.Dormant);
        }

        private void HandlePing(Vector3 worldPos)
        {
            _lastKnownPlayerPos = worldPos;
            if (_state != State.Hunting)
            {
                _agent.SetDestination(worldPos);
                if (_state == State.Dormant) SetState(State.Alerted);
                _searchTimer = 0f;
            }
        }

        private void CheckLineOfSightToPlayer()
        {
            if (eyes == null) return;
            var hits = Physics.OverlapSphere(eyes.position, playerDetectionDistance, playerMask);
            foreach (var h in hits)
            {
                var dir = (h.transform.position - eyes.position).normalized;
                float ang = Vector3.Angle(eyes.forward, dir);
                if (ang > profile.sightConeDegrees * 0.5f) continue;
                if (Physics.Raycast(eyes.position, dir, out var hit, profile.sightRange)
                    && hit.collider.gameObject == h.gameObject)
                {
                    _playerTransform = h.transform;
                    SetState(State.Hunting);
                    if (Vector3.Distance(eyes.position, h.transform.position) < 1.5f)
                    {
                        onPlayerCaught?.Raise();
                    }
                    return;
                }
            }
            if (_state == State.Hunting && _playerTransform == null)
            {
                SetState(State.Lost);
            }
        }

        private void GoToNextWaypoint()
        {
            if (patrolWaypoints == null || patrolWaypoints.Length == 0) return;
            _agent.SetDestination(patrolWaypoints[_waypointIndex].position);
            _waypointIndex = (_waypointIndex + 1) % patrolWaypoints.Length;
        }

        private void SetState(State s)
        {
            if (_state == s) return;
            _state = s;
            _searchTimer = 0f;
            ApplySpeed();
            onStateChanged?.Raise(s.ToString());
            Debug.Log($"[CarrierAI] State → {s}");
        }

        private void ApplySpeed()
        {
            if (profile == null) return;
            float mul = difficulty != null ? difficulty.globalCarrierSpeedMultiplier : 1f;
            _agent.speed = _state switch
            {
                State.Dormant => profile.dormantSpeed,
                State.Alerted => profile.alertedSpeed,
                State.Hunting => profile.huntingSpeed,
                _ => profile.dormantSpeed
            } * mul;
        }
    }
}
