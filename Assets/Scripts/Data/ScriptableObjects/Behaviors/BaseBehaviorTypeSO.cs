using System;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors
{
    public abstract class BaseBehaviorTypeSO : ScriptableObject
    {
        public abstract Type GetBaseBehaviorType();
    }
}