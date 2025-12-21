using System;
using System.Collections.Generic;
using Core.Services.States;
using Data.ScriptableObjects.Providers;
using Data.ScriptableObjects.States;
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
    public class StateListData
    {
        public readonly IReadOnlyList<IState> States;
        public StateListData(List<IState> states)
        {
            States = new List<IState>(states);
        }
    }
    public class AllEntityData
    {
        public readonly IReadOnlyList<object> EntityData;
        public AllEntityData(IReadOnlyList<object> entityData)
        {
            EntityData = new List<object>(entityData);
        }
    }
}