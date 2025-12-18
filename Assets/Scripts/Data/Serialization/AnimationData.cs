using System;
using Core.Services.States;
using Data.ScriptableObjects.Animatios;
using Data.ScriptableObjects.Behaviors;
using UnityEngine;

namespace Data.Serialization
{
    public abstract class AnimationSerializeDataBase
    {
        public float blendTime;
        public AnimationStateSO animationStateSO;
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
    
    public abstract class AnimationStateBase
    {
        public readonly float BlendTime;
        public readonly string StateName;
        public readonly AnimationClip Clip; 
        public AnimationStateBase(string stateName, AnimationClip clip, float blendTime)
        {
            BlendTime = blendTime;
            StateName = stateName;
            Clip = clip;
        }
        public AnimationStateBase( AnimationStateBase animationState)
        {
            BlendTime = animationState.BlendTime;
            StateName = animationState.StateName;
            Clip = animationState.Clip;
        }
    }
    public class AnimationStateTypeData : AnimationStateBase
    {
        public AnimationStateTypeData(string stateName, AnimationClip clip, Type behaviorType, float blendTime) :
            base(stateName, clip, blendTime)
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