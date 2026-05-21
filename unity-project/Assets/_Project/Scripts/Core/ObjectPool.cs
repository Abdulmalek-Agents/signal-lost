using System.Collections.Generic;
using UnityEngine;

namespace SignalLost.Core
{
    /// <summary>
    /// Minimal generic component pool. Use for VFX ping rings, audio one-shots, etc.
    /// </summary>
    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _stack = new();

        public ObjectPool(T prefab, int prewarm = 0, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < prewarm; i++) _stack.Push(CreateNew());
        }

        private T CreateNew()
        {
            var t = Object.Instantiate(_prefab, _parent);
            t.gameObject.SetActive(false);
            return t;
        }

        public T Rent()
        {
            var t = _stack.Count > 0 ? _stack.Pop() : CreateNew();
            t.gameObject.SetActive(true);
            return t;
        }

        public void Return(T t)
        {
            t.gameObject.SetActive(false);
            _stack.Push(t);
        }
    }
}
