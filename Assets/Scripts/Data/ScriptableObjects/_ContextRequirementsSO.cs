using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects
{
    public abstract class ContextRequirementsSO : ScriptableObject
    {
        /// <summary>
        /// Требования контекстов, которые необходимы этому SO. Переопределяются при необходимости.
        /// </summary>
        public virtual ContextRequirement[] GetContextRequirements()
        {
            return null;
        }
    }
}