using System;
using System.Collections.Generic;
using Data.ScriptableObjects.Behaviors;
using Data.ScriptableObjects.Providers;
using UnityEngine;

namespace Data.Serialization
{
    [Serializable]
    public class EntityDataPart
    {
        public BaseBehaviorSO behaviorSO;
        public List<GameObject> contexts;
        public bool isDefaultState = false;
    }
    [Serializable]
    public class ProviderDataPart
    {
        public BaseProviderSO providerSO;
        public List<GameObject> contexts;
    }
}