using System;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
    public abstract class BaseBehaviorTypeSO : ScriptableObject
    {
        public abstract Type GetBaseBehaviorType();
    }
}