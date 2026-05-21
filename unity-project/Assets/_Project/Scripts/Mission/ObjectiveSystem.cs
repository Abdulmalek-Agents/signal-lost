using System;
using System.Collections.Generic;
using UnityEngine;
using SignalLost.Core;

namespace SignalLost.Mission
{
    /// <summary>
    /// Minimal flag-based objective tracker. Hook objectives to event channels in Inspector.
    /// </summary>
    public class ObjectiveSystem : MonoBehaviour
    {
        [Serializable]
        public class Objective
        {
            public string id;
            public string description;
            public bool isComplete;
        }

        [SerializeField] private List<Objective> objectives = new();
        [SerializeField] private StringEventChannel onObjectiveCompleted;

        public IReadOnlyList<Objective> Objectives => objectives;

        public void Complete(string id)
        {
            var o = objectives.Find(x => x.id == id);
            if (o == null || o.isComplete) return;
            o.isComplete = true;
            onObjectiveCompleted?.Raise(id);
            Debug.Log($"[ObjectiveSystem] {id} complete: {o.description}");
        }
    }
}
