using System;
using System.Collections.Generic;
using Core.Services.States;
using Data.ScriptableObjects.Providers;
using Data.ScriptableObjects.States;
using UnityEngine;

namespace Data.Dto
{
    [Serializable]
    public class EntityDataPart
    {
        /// <summary>ScriptableObject, описывающий поведение сущности.</summary>
        public BaseBehaviorSO behaviorSO;

        /// <summary>Контексты (GameObject), необходимые для создания состояния/поведения.</summary>
        public List<GameObject> contexts;

        /// <summary>Флаг, указывающий, является ли это состояние состоянием по умолчанию.</summary>
        public bool isDefaultState = false;
    }
    [Serializable]
    public class ProviderDataPart
    {
        /// <summary>ScriptableObject провайдера, используемый для создания провайдера в сущности.</summary>
        public BaseProviderSO providerSO;

        /// <summary>Контексты, необходимые для создания провайдера.</summary>
        public List<GameObject> contexts;
    }
    public class StateListData
    {
        /// <summary>Набор состояний, созданных для сущности.</summary>
        public readonly IReadOnlyList<IState> States;

        /// <summary>Инициализирует список состояний на основе переданной коллекции.</summary>
        public StateListData(List<IState> states)
        {
            States = new List<IState>(states);
        }
    }
    public class AllEntityData
    {
        /// <summary>Список всех частей данных сущностей (поведения и провайдеры).</summary>
        public readonly IReadOnlyList<object> EntityData;

        /// <summary>Создаёт копию списка данных сущностей.</summary>
        public AllEntityData(IReadOnlyList<object> entityData)
        {
            EntityData = new List<object>(entityData);
        }
    }
    public struct ContextRequirement
    {
        public string displayName;
        public string typeName;
        public bool optional;
    }
}