using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors
{
    /// <summary>
    /// Приоретет состояний. Чем выше - тем больше.
    /// </summary>
    [CreateAssetMenu(fileName = "PriorityBehaviors", menuName = "ScriptableObjects/PriorityBehaviors")]
    public class PriorityBehaviorsSO : ScriptableObject
    {
        [SerializeField] private List<BaseBehaviorSO> behaviorPriorities;

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