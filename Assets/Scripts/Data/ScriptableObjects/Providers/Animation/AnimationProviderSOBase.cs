using System;
using System.Collections.Generic;
using Core.Behaviors.Animations;
using Core.Providers;
using Data.Serialization;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Animation
{
    /// <summary>
    /// Базовый ScriptableObject для создания провайдеров анимации: ищет Animator в контекстах и создаёт `AnimationTransitionNotifier, если его нет на объекте`.
    /// </summary>
    public abstract class AnimationProviderSOBase : BaseProviderSO
    {
        /// <summary>Создаёт провайдер анимации на основе первого найденного Animator в контекстах.</summary>
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            Animator animator = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out animator))
                    break;
            }
            if (animator == null)
                throw new Exception($"AnimationProviderSO: Не удалось найти компонент  Animator в контекстах для создания провайдера {nameof(AnimationTransitionHandler)}.");

            return new AnimationTransitionNotifier(animator, GetAnimationStateTypeDatas());
        }

        /// <summary>Возвращает список метаданных состояний анимации для создания обработчика переходов.</summary>
        public abstract List<AnimationStateTypeData> GetAnimationStateTypeDatas();
    }
}