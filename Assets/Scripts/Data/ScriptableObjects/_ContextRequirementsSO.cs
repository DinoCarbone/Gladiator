using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects
{
    public abstract class ContextRequirementsSO : ScriptableObject
    {
        public virtual ContextRequirement[] GetContextRequirements()
        {
            return null;
        }
    }
}