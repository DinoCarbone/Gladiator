using System;
using System.Collections.Generic;
using Core.Behaviors.Animations;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
     [CreateAssetMenu(fileName = "AnimationProviderSO", menuName = "ScriptableObjects/Providers/AnimationProviderSO")]
    public class AnimationProviderSO : BaseProviderSO
    {
        public AnimationClip idle;
        public AnimationClip move;

        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            Animator animator = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out animator)) 
                break;
            }
            if(animator == null)
            throw new Exception($"AnimationProviderSO: Не удалось найти компонент  Animator в контекстах для создания провайдера {nameof(ExampleAnimationProvider)}.");

            return new ExampleAnimationProvider(animator, idle, move);
        }
    }
}