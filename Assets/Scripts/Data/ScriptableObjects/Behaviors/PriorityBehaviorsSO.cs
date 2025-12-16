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

        public IReadOnlyList<Type> GetPriorityTypes()
        {
            return behaviorPriorities.Select(b => b.GetBehaviorType()).ToList();
        }
    }
}