using System;
using System.Collections.Generic;
using Data.Serialization;
using UnityEngine;
using Zenject;
using Utils;
using Core.Services.States;

namespace Core.Behaviors.Animations
{
    public class AnimationTransitionHandler : Providers.IProvider, IDisposable
    {
        private List<AnimationStateTypeData> templateStates = new List<AnimationStateTypeData>();
        private readonly List<AnimationStateEnterData> states = new List<AnimationStateEnterData>();
        private IAnimationPlayService animatorService;
        private readonly Dictionary<AnimationStateEnterData, Action> enterHandlers = new();
        protected readonly Animator animator;

        public AnimationTransitionHandler(Animator animator, List<AnimationStateTypeData> templateAnimationStates)
        {
            this.animator = Extensions.AssignWithNullCheck(animator);
            this.templateStates = Extensions.AssignWithNullCheck(templateAnimationStates);
        }

        [Inject]
        public void Construct(StateListData animationStates, IAnimationPlayServiceFactory animationServicesFactory)
        {
            animatorService = Extensions.AssignWithNullCheck(animationServicesFactory.Create(animator));
            CreateAnimationStates(animationStates.States);
            Subscribe();
        }

        private void CreateAnimationStates(IReadOnlyList<IState> states)
        {
            this.states.Clear();
            enterHandlers.Clear();

            foreach (var state in states)
            {
                if (state == null)
                {
                    Debug.LogError("State is null");
                    continue;
                }

                AnimationStateTypeData stateTypeData = Extensions.FindCompatibleBehaviorType(state, templateStates);
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
                        this.states.Add(animationEnterData);
                    }
                    else Debug.LogError("State doesn't implement IEnterable");
                }
            }
        }

        protected virtual void Subscribe()
        {
            foreach (var state in states)
            {
                Action enterHandler = () => OnEnterState(state);
                state.EnterState.OnEnter += enterHandler;
                enterHandlers[state] = enterHandler;
            }
        }

        protected virtual void Unsubscribe()
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
            float blendTime = enterData.BlendTime;
            if(enterData.OverrideBlendTimes?.Count > 0)
            {
                string currentStateName = animatorService.GetCurrentAnimationName();
                if( !string.IsNullOrEmpty(currentStateName) && 
                enterData.OverrideBlendTimes.TryGetValue(currentStateName, out blendTime))
                Debug.Log($"Overriding blendTime for {enterData.StateName} to {blendTime}");
                else blendTime = enterData.BlendTime;
            }

            animatorService.Play(enterData.StateName, enterData.Clip, blendTime);
        }

        public virtual void Dispose()
        {
            Unsubscribe();
            states.Clear();
            templateStates.Clear();
            templateStates = null;
        }
    }
}