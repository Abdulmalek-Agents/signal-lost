using System;
using UnityEngine;

namespace SignalLost.Core
{
    /// <summary>
    /// ScriptableObject event channels. Decouple publishers and subscribers.
    /// Create instances in Assets/_Project/Data/Events/ and wire in Inspector.
    /// </summary>

    [CreateAssetMenu(menuName = "SignalLost/Events/Void", fileName = "VoidEvent_")]
    public class VoidEventChannel : ScriptableObject
    {
        public event Action OnRaised;
        public void Raise() => OnRaised?.Invoke();
    }

    [CreateAssetMenu(menuName = "SignalLost/Events/String", fileName = "StringEvent_")]
    public class StringEventChannel : ScriptableObject
    {
        public event Action<string> OnRaised;
        public void Raise(string value) => OnRaised?.Invoke(value);
    }

    [CreateAssetMenu(menuName = "SignalLost/Events/Float", fileName = "FloatEvent_")]
    public class FloatEventChannel : ScriptableObject
    {
        public event Action<float> OnRaised;
        public void Raise(float value) => OnRaised?.Invoke(value);
    }

    [CreateAssetMenu(menuName = "SignalLost/Events/Vector3", fileName = "Vec3Event_")]
    public class Vector3EventChannel : ScriptableObject
    {
        public event Action<Vector3> OnRaised;
        public void Raise(Vector3 value) => OnRaised?.Invoke(value);
    }
}
