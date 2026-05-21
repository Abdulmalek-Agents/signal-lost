using System;
using System.Collections.Generic;
using UnityEngine;

namespace SignalLost.Core
{
    /// <summary>
    /// Lightweight static service registry. Replaces ad-hoc singletons.
    /// </summary>
    public static class Services
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service) where T : class
        {
            var t = typeof(T);
            if (_services.ContainsKey(t))
            {
                Debug.LogWarning($"[Services] {t.Name} already registered — overwriting.");
            }
            _services[t] = service;
        }

        public static T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var s)) return (T)s;
            Debug.LogError($"[Services] Service {typeof(T).Name} not registered. Did Bootstrap run?");
            return null;
        }

        public static bool TryGet<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out var s)) { service = (T)s; return true; }
            service = null;
            return false;
        }

        public static void Clear() => _services.Clear();
    }
}
