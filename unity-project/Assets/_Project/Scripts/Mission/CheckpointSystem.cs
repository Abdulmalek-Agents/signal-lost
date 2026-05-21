using UnityEngine;
using SignalLost.Core;

namespace SignalLost.Mission
{
    /// <summary>
    /// Maintains the last visited Safe Room as the respawn point.
    /// </summary>
    public class CheckpointSystem : MonoBehaviour, IService
    {
        [SerializeField] private VoidEventChannel onSafeRoomEntered;
        [SerializeField] private Transform defaultSpawn;

        public Vector3 LastCheckpoint { get; private set; }

        public void Register() => Services.Register(this);

        private Transform _activePlayer;

        public void SetPlayer(Transform t) => _activePlayer = t;

        private void Awake()
        {
            if (defaultSpawn != null) LastCheckpoint = defaultSpawn.position;
        }

        private void OnEnable()
        {
            if (onSafeRoomEntered != null) onSafeRoomEntered.OnRaised += RecordCheckpoint;
        }
        private void OnDisable()
        {
            if (onSafeRoomEntered != null) onSafeRoomEntered.OnRaised -= RecordCheckpoint;
        }

        private void RecordCheckpoint()
        {
            if (_activePlayer == null) return;
            LastCheckpoint = _activePlayer.position;
            Debug.Log($"[CheckpointSystem] Saved {LastCheckpoint}");
        }

        public void Respawn()
        {
            if (_activePlayer == null) return;
            _activePlayer.position = LastCheckpoint;
            Debug.Log($"[CheckpointSystem] Respawned at {LastCheckpoint}");
        }
    }
}
