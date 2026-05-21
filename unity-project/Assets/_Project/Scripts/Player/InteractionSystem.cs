using UnityEngine;
using SignalLost.AI;

namespace SignalLost.Player
{
    /// <summary>
    /// Sphere-cast forward from camera. Highlights interactables and forwards "Interact" input.
    /// </summary>
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private float range = 2.5f;
        [SerializeField] private float radius = 0.3f;
        [SerializeField] private LayerMask mask = ~0;

        public IInteractable Current { get; private set; }

        private void OnEnable()
        {
            if (input != null) input.OnInteractPressed += TryInteract;
        }
        private void OnDisable()
        {
            if (input != null) input.OnInteractPressed -= TryInteract;
        }

        private void Update()
        {
            Current = null;
            if (rayOrigin == null) return;
            if (Physics.SphereCast(rayOrigin.position, radius, rayOrigin.forward, out var hit, range, mask))
            {
                Current = hit.collider.GetComponentInParent<IInteractable>();
            }
        }

        private void TryInteract() => Current?.Interact();
    }
}
