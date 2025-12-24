using Core.Services;
using UnityEngine;

namespace Core.Behaviors.Animations
{
    /// <summary>
    /// Фабрика для создания <see cref="IAnimationPlayService"/>.
    /// </summary>
    public class AnimationPlayServiceFactory : IAnimationPlayServiceFactory
    {
        public IAnimationPlayService Create(Animator animator)
        {
            return new AnimationPlayService(animator);
        }
    }

    /// <summary>
    /// Фабрика для создания <see cref="IAnimationEndNotifier"/> с использованием <see cref="ITickableService"/>.
    /// </summary>
    public class AnimationEndNotifierFactory : IAnimationEndNotifierFactory
    {
        private readonly ITickableService tickableService;

        public AnimationEndNotifierFactory(ITickableService tickableService)
        {
            this.tickableService = tickableService;
        }

        public IAnimationEndNotifier Create(Animator animator)
        {
            return new AnimationEndNotifier(tickableService, animator);
        }
    }

    /// <summary>
    /// Фабрика для получения или создания <see cref="IAnimationEventsNotifier"/> на игровом объекте аниматора.
    /// </summary>
    public class AnimationEventsNotifierFactory : IAnimationEventsNotifierFactory
    {
        public IAnimationEventsNotifier Create(Animator animator)
        {
            AnimationEventsObserverBehavior animationEventsObserverBehavior;

            if (animator.TryGetComponent(out animationEventsObserverBehavior))
            {
                return animationEventsObserverBehavior;
            }

            animationEventsObserverBehavior = animator.gameObject.AddComponent<AnimationEventsObserverBehavior>();
            return animationEventsObserverBehavior;
        }
    }
}