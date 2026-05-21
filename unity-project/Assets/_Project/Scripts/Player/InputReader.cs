using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace SignalLost.Player
{
    /// <summary>
    /// Lightweight Input System wrapper. Exposes events the PlayerController subscribes to.
    /// Create a default Input Actions asset and bind in the Inspector.
    /// </summary>
    public class InputReader : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool SprintHeld { get; private set; }
        public bool CrouchHeld { get; private set; }
        public bool ScannerAimHeld { get; private set; }

        public event Action OnPingPressed;
        public event Action OnInteractPressed;
        public event Action OnFlashlightToggled;
        public event Action OnPausePressed;

        [SerializeField] private InputActionAsset actions;

        private InputAction _move, _look, _sprint, _crouch, _aim, _ping, _interact, _flashlight, _pause;

        private void Awake()
        {
            if (actions == null)
            {
                Debug.LogError("[InputReader] No InputActionAsset assigned.");
                return;
            }

            var map = actions.FindActionMap("Player", true);
            _move = map.FindAction("Move");
            _look = map.FindAction("Look");
            _sprint = map.FindAction("Sprint");
            _crouch = map.FindAction("Crouch");
            _aim = map.FindAction("ScannerAim");
            _ping = map.FindAction("ScannerPing");
            _interact = map.FindAction("Interact");
            _flashlight = map.FindAction("Flashlight");
            _pause = map.FindAction("Pause");
        }

        private void OnEnable()
        {
            actions.Enable();
            _ping.performed += OnPing;
            _interact.performed += OnInteract;
            _flashlight.performed += OnFlashlight;
            _pause.performed += OnPause;
        }

        private void OnDisable()
        {
            _ping.performed -= OnPing;
            _interact.performed -= OnInteract;
            _flashlight.performed -= OnFlashlight;
            _pause.performed -= OnPause;
            actions.Disable();
        }

        private void Update()
        {
            MoveInput = _move.ReadValue<Vector2>();
            LookInput = _look.ReadValue<Vector2>();
            SprintHeld = _sprint.IsPressed();
            CrouchHeld = _crouch.IsPressed();
            ScannerAimHeld = _aim.IsPressed();
        }

        private void OnPing(InputAction.CallbackContext _)        => OnPingPressed?.Invoke();
        private void OnInteract(InputAction.CallbackContext _)    => OnInteractPressed?.Invoke();
        private void OnFlashlight(InputAction.CallbackContext _)  => OnFlashlightToggled?.Invoke();
        private void OnPause(InputAction.CallbackContext _)       => OnPausePressed?.Invoke();
    }
}
