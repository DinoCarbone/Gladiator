using System;
using Data.ScriptableObjects.Animatios;
using UnityEngine;

namespace Core.Behaviors.Animations
{
    [DisallowMultipleComponent]
    public class AnimationEventsObserverBehavior : MonoBehaviour, IAnimationEventsNotifier
    {
        /// <summary>Событие, вызываемое при возникновении анимационного события.</summary>
        public event Action<AnimationEventSO> OnAnimationEvent;

        /// <summary>Вызывает событие анимации для подписчиков.</summary>
        public void NotifyAnimationEnded(AnimationEventSO animationEvent)
        {
            OnAnimationEvent?.Invoke(animationEvent);
        }
    }
}