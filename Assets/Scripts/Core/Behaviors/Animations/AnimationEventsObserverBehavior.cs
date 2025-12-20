using System;
using Data.ScriptableObjects.Animatios;
using UnityEngine;

namespace Core.Behaviors.Animations
{
    [DisallowMultipleComponent]
    public class AnimationEventsObserverBehavior : MonoBehaviour, IAnimationEventsNotifier
    {
        public event Action<AnimationEventSO> OnAnimationEvent;
        
        public void NotifyAnimationEnded(AnimationEventSO animationEvent)
        {
            OnAnimationEvent?.Invoke(animationEvent);
        }
    }
}