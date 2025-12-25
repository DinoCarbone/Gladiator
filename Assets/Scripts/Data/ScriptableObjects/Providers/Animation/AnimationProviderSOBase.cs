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
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Animator animator = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                animator = dependencies[0] as Animator ?? (dependencies[0] as GameObject)?.GetComponent<Animator>();
            }
            if (animator == null)
                throw new Exception($"AnimationProviderSO: Animator is empty.");

            return new AnimationTransitionNotifier(animator, GetAnimationStateTypeDatas());
        }

        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Animator",
                    typeName = "UnityEngine.Animator, UnityEngine",
                    optional = false
                }
            };
        }

        /// <summary>Возвращает список метаданных состояний анимации для создания обработчика переходов.</summary>
        public abstract List<AnimationStateTypeData> GetAnimationStateTypeDatas();
    }
}