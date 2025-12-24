using System;
using System.Collections.Generic;
using Core.Services.States;
using Data.ScriptableObjects.Animatios;
using Data.ScriptableObjects.States;
using UnityEngine;

namespace Data.Serialization
{
    public abstract class AnimationSerializeDataBase
    {
        /// <summary>Базовое время перекрытия/плавного смешения для состояния анимации.</summary>
        public float baseBlendTime;

        /// <summary>Ссылка на ScriptableObject состояния анимации.</summary>
        public AnimationStateSO animationStateSO;

        /// <summary>Переопределённые времена смешения для конкретных клипов/состояний.</summary>
        public List<OverrideBlendTimeData> overrideBlendTimeDatas;
    }
    [Serializable]
    public class AnimationSerializeClipData : AnimationSerializeDataBase
    {
        /// <summary>Клип анимации, используемый для данного элемента сериализации.</summary>
        public AnimationClip clip;
    }
    [Serializable]
    public class AnimationSerializeTypeData : AnimationSerializeDataBase
    {
        /// <summary>ScriptableObject, указывающий на тип поведения, с которым связан набор анимаций.</summary>
        public BaseBehaviorTypeSO behaviorTypeSO;
    }
    [Serializable]
    public class OverrideBlendTimeData
    {
         /// <summary>Состояние анимации, для которого переопределяется время смешения.</summary>
         public AnimationStateSO animationStateSO;

         /// <summary>Переопределённое время смешения для указанного состояния.</summary>
         public float overrideBlendTime;
    }
    
    public abstract class AnimationStateBase
    {
        /// <summary>Время смешения для этого состояния анимации.</summary>
        public readonly float BlendTime;

        /// <summary>Имя состояния анимации.</summary>
        public readonly string StateName;

        /// <summary>Анимационный клип, соответствующий этому состоянию.</summary>
        public readonly AnimationClip Clip;

        /// <summary>Словарь переопределённых времен смешения по имени клипа/состояния.</summary>
        public readonly Dictionary<string, float> OverrideBlendTimes;

        /// <summary>Создаёт базовое представление состояния анимации.</summary>
        public AnimationStateBase(string stateName, AnimationClip clip, float blendTime, Dictionary<string, float> overrideBlendTimes = null)
        {
            BlendTime = blendTime;
            StateName = stateName;
            Clip = clip;
            OverrideBlendTimes = overrideBlendTimes;
        }

        /// <summary>Копирующий конструктор для клонирования состояния анимации.</summary>
        public AnimationStateBase(AnimationStateBase animationState)
        {
            BlendTime = animationState.BlendTime;
            StateName = animationState.StateName;
            Clip = animationState.Clip;
            OverrideBlendTimes = animationState.OverrideBlendTimes;
        }
    }
    public class AnimationStateTypeData : AnimationStateBase
    {
        public AnimationStateTypeData(string stateName, AnimationClip clip, Type behaviorType, float blendTime, Dictionary<string, float> overrideBlendTimes = null) :
            base(stateName, clip, blendTime, overrideBlendTimes)
        {
            BehaviorType = behaviorType;
        }
        /// <summary>Тип поведения, с которым связано это состояние анимации.</summary>
        public readonly Type BehaviorType;
    }
    public class AnimationStateEnterData : AnimationStateBase
    {
        public AnimationStateEnterData(AnimationStateBase animationState, IEnterable enterState) :
            base(animationState)
        {
            EnterState = enterState;
        }
        /// <summary>Ссылка на состояние, которое реализует вход (IEnterable).</summary>
        public readonly IEnterable EnterState;
    }
}