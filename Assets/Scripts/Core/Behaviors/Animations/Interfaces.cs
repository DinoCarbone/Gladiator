using System;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using UnityEngine;

namespace Core.Behaviors.Animations
{
    public interface IAnimationEventsNotifier
    {
        event Action<AnimationEventSO> OnAnimationEvent;
    }
    public interface IAnimationPlayService
    {
        string GetCurrentAnimationName(int layer = 0);
        void Play(string stateName, AnimationClip clip, float blendTime = 0.2f);
    }
    public interface IAnimationEndNotifier
    {
        void AddNotifiable(AnimationStateEnterData notifiableData);
    }
    public interface IAnimationEventsNotifierFactory
    {
        IAnimationEventsNotifier Create(Animator animator);
    }
    public interface IAnimationPlayServiceFactory
    {
        IAnimationPlayService Create(Animator animator);
    }
    public interface IAnimationEndNotifierFactory
    {
        IAnimationEndNotifier Create(Animator animator);
    }
}