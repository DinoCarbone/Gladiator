using System;

namespace Data.ScriptableObjects.States
{
    public abstract class BaseBehaviorTypeSO : ContextRequirementsSO
    {
        public abstract Type GetBaseBehaviorType();
    }
}