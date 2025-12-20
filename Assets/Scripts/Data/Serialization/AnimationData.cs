using System;
using System.Collections.Generic;
using Core.Services.States;
using Data.ScriptableObjects.Animatios;
using Data.ScriptableObjects.Behaviors;
using UnityEngine;

namespace Data.Serialization
{
    public abstract class AnimationSerializeDataBase
    {
        public float baseBlendTime;
        public AnimationStateSO animationStateSO;
        public List<OverrideBlendTimeData> overrideBlendTimeDatas;
    }
    [Serializable]
    public class AnimationSerializeClipData : AnimationSerializeDataBase
    {
        public AnimationClip clip;
    }
    [Serializable]
    public class AnimationSerializeTypeData : AnimationSerializeDataBase
    {
        public BaseBehaviorTypeSO behaviorTypeSO;
    }
    [Serializable]
    public class OverrideBlendTimeData
    {
         public AnimationStateSO animationStateSO;
         public float overrideBlendTime;
    }
    
    public abstract class AnimationStateBase
    {
        public readonly float BlendTime;
        public readonly string StateName;
        public readonly AnimationClip Clip; 
        public readonly Dictionary<string, float> OverrideBlendTimes;
        public AnimationStateBase(string stateName, AnimationClip clip, float blendTime, Dictionary<string, float> overrideBlendTimes = null)
        {
            BlendTime = blendTime;
            StateName = stateName;
            Clip = clip;
            OverrideBlendTimes = overrideBlendTimes;
        }
        public AnimationStateBase( AnimationStateBase animationState)
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
        public readonly Type BehaviorType;
    }
    public class AnimationStateEnterData : AnimationStateBase
    {
        public AnimationStateEnterData(AnimationStateBase animationState, IEnterable enterState) :
            base(animationState)
        {
            EnterState = enterState;
        }
        public readonly IEnterable EnterState;
    }
}