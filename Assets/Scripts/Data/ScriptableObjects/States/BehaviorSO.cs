using System;
using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
    public abstract class BehaviorSO<T> : BaseBehaviorSO where T : IIncompatibleStates
    {
        [SerializeField, Tooltip("List of behavior ScriptableObjects that are incompatible with this behavior.")]
        private List<BaseBehaviorSO> incompatibleStates;

        protected List<Type> GetIncompatibleTypes()
        {
            /// <summary>Составляет список типов несовместимых поведений, указанных в `incompatibleStates`.</summary>
            List<Type> types = new List<Type>();
            foreach (var state in incompatibleStates)
            {
                Type type = state.GetBaseBehaviorType();
                if(type == null || types.Contains(type))
                    continue;
                types.Add(type);
            }
            return types;
        }
    }
}