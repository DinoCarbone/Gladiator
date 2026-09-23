using System;
using System.Collections.Generic;
using Core.Behaviors.Animations;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Animation
{
    public abstract class AnimationProviderSOBase : BaseProviderSO
    {
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

        public abstract List<AnimationStateTypeData> GetAnimationStateTypeDatas();
    }
}