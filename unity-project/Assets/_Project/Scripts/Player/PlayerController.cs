using UnityEngine;

namespace SignalLost.Player
{
    /// <summary>
    /// First-person character controller. Walk/sprint/crouch + mouselook + flashlight toggle.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 3.0f;
        [SerializeField] private float sprintSpeed = 5.5f;
        [SerializeField] private float crouchSpeed = 1.5f;
        [SerializeField] private float gravity = -19.62f;

        [Header("Look")]
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private float lookSensitivity = 0.12f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        [Header("Flashlight")]
        [SerializeField] private Light flashlight;

        [Header("Refs")]
        [SerializeField] private InputReader input;

        private CharacterController _cc;
        private float _pitch;
        private Vector3 _velocity;
        private bool _crouched;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            if (input != null) input.OnFlashlightToggled += ToggleFlashlight;
        }
        private void OnDisable()
        {
            if (input != null) input.OnFlashlightToggled -= ToggleFlashlight;
        }

        private void Update()
        {
            if (input == null) return;
            HandleLook();
            HandleMove();
        }

        private void HandleLook()
        {
            var l = input.LookInput;
            transform.Rotate(0f, l.x * lookSensitivity, 0f);
            _pitch -= l.y * lookSensitivity;
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
            if (cameraPivot != null) cameraPivot.localEulerAngles = new Vector3(_pitch, 0f, 0f);
        }

        private void HandleMove()
        {
            float speed = walkSpeed;
            _crouched = input.CrouchHeld;
            if (_crouched) speed = crouchSpeed;
            else if (input.SprintHeld) speed = sprintSpeed;

            var i = input.MoveInput;
            var dir = transform.right * i.x + transform.forward * i.y;
            _cc.Move(dir * speed * Time.deltaTime);

            if (_cc.isGrounded && _velocity.y < 0) _velocity.y = -2f;
            _velocity.y += gravity * Time.deltaTime;
            _cc.Move(_velocity * Time.deltaTime);
        }

        private void ToggleFlashlight()
        {
            if (flashlight != null) flashlight.enabled = !flashlight.enabled;
        }
    }
}
