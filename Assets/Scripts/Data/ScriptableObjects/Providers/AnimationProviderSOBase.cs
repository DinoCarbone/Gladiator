using System;
using System.Collections.Generic;
using Core.Behaviors.Animations;
using Core.Providers;
using Data.Serialization;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    public abstract class AnimationProviderSOBase : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            Animator animator = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out animator))
                    break;
            }
            if (animator == null)
                throw new Exception($"AnimationProviderSO: Не удалось найти компонент  Animator в контекстах для создания провайдера {nameof(BaseAnimationProvider)}.");

            return new ObserverAnimatorProvider(animator, GetAnimationStateTypeDatas());
        }
        public abstract List<AnimationStateTypeData> GetAnimationStateTypeDatas();
    }
}