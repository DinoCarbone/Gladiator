using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
    /// <summary>
    /// Приоретет состояний. Чем выше - тем больше.
    /// </summary>
    [CreateAssetMenu(fileName = "PriorityStates", menuName = "ScriptableObjects/PriorityStates")]
    public class PriorityBehaviorsSO : ScriptableObject
    {
        [SerializeField, Tooltip("Ordered list of behavior ScriptableObjects by priority (higher index = higher priority).")]
        private List<BaseBehaviorSO> behaviorPriorities;

        public Dictionary<Type, int> GetPriorityTypes()
        {
            var priorityDict = new Dictionary<Type, int>();

            for (int i = 0; i < behaviorPriorities.Count; i++)
            {
                var behaviorType = behaviorPriorities[i].GetBaseBehaviorType();
                priorityDict[behaviorType] = i;
            }

            return priorityDict;
        }
    }
}