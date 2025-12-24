using System;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using UnityEngine;

namespace Core.Behaviors.Animations
{
    /// <summary>Отслеживает события анимаций и ретранслирует их подписчикам.</summary>
    public interface IAnimationEventsNotifier
    {
        /// <summary>Событие — произошёл анимационный callback, содержащее данные <see cref="AnimationEventSO"/>.</summary>
        event Action<AnimationEventSO> OnAnimationEvent;
    }

    /// <summary>Сервис воспроизведения анимаций (обёртка над <see cref="Animator"/>).</summary>
    public interface IAnimationPlayService
    {
        /// <summary>Возвращает имя текущего состояния на указанном слое.</summary>
        string GetCurrentAnimationName(int layer = 0);

        /// <summary>Проигрывает состояние/клип анимации с указанием времени смешения.</summary>
        void Play(string stateName, AnimationClip clip, float blendTime = 0.2f);
    }

    /// <summary>Нотификатор конца анимации, регистрирует состояние, которое должно быть оповещено при завершении.</summary>
    public interface IAnimationEndNotifier
    {
        /// <summary>Регистрирует структуру данных, которая будет уведомлена о входе/выходе состояния анимации.</summary>
        void AddNotifiable(AnimationStateEnterData notifiableData);
    }

    /// <summary>Фабрика для создания <see cref="IAnimationEventsNotifier"/>.</summary>
    public interface IAnimationEventsNotifierFactory
    {
        IAnimationEventsNotifier Create(Animator animator);
    }

    /// <summary>Фабрика для создания <see cref="IAnimationPlayService"/>.</summary>
    public interface IAnimationPlayServiceFactory
    {
        IAnimationPlayService Create(Animator animator);
    }

    /// <summary>Фабрика для создания <see cref="IAnimationEndNotifier"/>.</summary>
    public interface IAnimationEndNotifierFactory
    {
        IAnimationEndNotifier Create(Animator animator);
    }
}