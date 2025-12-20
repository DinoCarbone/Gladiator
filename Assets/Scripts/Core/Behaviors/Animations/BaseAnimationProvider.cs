using System;
using System.Collections.Generic;
using Data.Serialization;
using UnityEngine;
using Zenject;
using Utils;
using Core.Services.States;

namespace Core.Behaviors.Animations
{
    public class BaseAnimationProvider : Providers.IProvider, IDisposable
    {
        private List<AnimationStateTypeData> templateAnimationStates = new List<AnimationStateTypeData>();
        private readonly List<AnimationStateEnterData> animationStates = new List<AnimationStateEnterData>();
        private readonly BaseAnimatorService animatorService;
        private readonly Dictionary<AnimationStateEnterData, Action> enterHandlers = new();

        public BaseAnimationProvider(Animator animator, List<AnimationStateTypeData> templateAnimationStates)
        {
            animatorService = new BaseAnimatorService(animator);
            this.templateAnimationStates = Extensions.AssignWithNullCheck(templateAnimationStates);
        }

        [Inject]
        public void Construct(StateListData animationStates)
        {
            CreateAnimationStates(animationStates.States);
            Subscribe();
        }

        private void CreateAnimationStates(IReadOnlyList<IState> states)
        {
            animationStates.Clear();
            enterHandlers.Clear();

            foreach (var state in states)
            {
                if (state == null)
                {
                    Debug.LogError("State is null");
                    continue;
                }

                AnimationStateTypeData stateTypeData = Extensions.FindCompatibleBehaviorType(state, templateAnimationStates);
                if (stateTypeData != null)
                {
                    if (string.IsNullOrEmpty(stateTypeData.StateName))
                    {
                        Debug.LogError("StateName is null or empty");
                        continue;
                    }

                    if (state is IEnterable enterable)
                    {
                        AnimationStateEnterData animationEnterData = new AnimationStateEnterData(stateTypeData, enterable);
                        animationStates.Add(animationEnterData);
                    }
                    else Debug.LogError("State doesn't implement IEnterable");
                }
            }
        }

        private void Subscribe()
        {
            foreach (var state in animationStates)
            {
                Action enterHandler = () => OnEnterState(state);
                state.EnterState.OnEnter += enterHandler;
                enterHandlers[state] = enterHandler;
            }
        }

        private void Unsubscribe()
        {
            foreach (var kvp in enterHandlers)
            {
                AnimationStateEnterData state = kvp.Key;
                Action handler = kvp.Value;

                state.EnterState.OnEnter -= handler;
            }

            enterHandlers.Clear();
        }
        
        protected virtual void OnEnterState(AnimationStateEnterData enterData)
        {
            animatorService.Play(enterData.StateName, enterData.Clip, enterData.BlendTime);
        }

        public virtual void Dispose()
        {
            Unsubscribe();
            animationStates.Clear();
            templateAnimationStates.Clear();
            templateAnimationStates = null;
        }
    }
}