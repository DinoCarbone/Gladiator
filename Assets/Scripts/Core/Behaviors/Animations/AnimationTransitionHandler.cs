using System;
using System.Collections.Generic;
using Data.Dto;
using UnityEngine;
using Zenject;
using Utils;
using Core.Services.States;

namespace Core.Behaviors.Animations
{
    /// <summary>
    /// Обрабатывает переходы состояний и запускает соответствующие анимации через <see cref="IAnimationPlayService"/>.
    /// Подписывается на события входа состояний и проигрывает анимационные клипы с учётом blend time.
    /// </summary>
    public class AnimationTransitionHandler : Providers.IProvider, IDisposable
    {
        private List<AnimationStateTypeData> templateStates = new List<AnimationStateTypeData>();
        private readonly List<AnimationStateEnterData> states = new List<AnimationStateEnterData>();
        private IAnimationPlayService animatorService;
        private readonly Dictionary<AnimationStateEnterData, Action> enterHandlers = new();
        protected readonly Animator animator;

        /// <summary>
        /// Создаёт обработчик переходов анимаций для указанного <see cref="Animator"/>.
        /// </summary>
        /// <param name="animator">Animator, используемый для проигрывания клипов.</param>
        /// <param name="templateAnimationStates">Шаблоны состояний анимаций для сопоставления с поведениями.</param>
        public AnimationTransitionHandler(Animator animator, List<AnimationStateTypeData> templateAnimationStates)
        {
            this.animator = Extensions.AssignWithNullCheck(animator);
            this.templateStates = Extensions.AssignWithNullCheck(templateAnimationStates);
        }

        /// <summary>
        /// Инъекция зависимостей: создаёт сервис проигрывания анимаций и инициализирует состояния.
        /// </summary>
        /// <param name="animationStates">Данные состояний для создания подписок.</param>
        /// <param name="animationServicesFactory">Фабрика сервиса проигрывания анимаций.</param>
        [Inject]
        public void Construct(StateListData animationStates, IAnimationPlayServiceFactory animationServicesFactory)
        {
            animatorService = Extensions.AssignWithNullCheck(animationServicesFactory.Create(animator));
            CreateAnimationStates(animationStates.States);
            Subscribe();
        }

        /// <summary>
        /// Создаёт внутренние данные состояний и собирает обработчики входа для тех состояний, которые реализуют <see cref="IEnterable"/>.
        /// </summary>
        /// <param name="states">Список состояний для обработки.</param>
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

        /// <summary>
        /// Подписывается на события входа состояний, сохраняет делегаты для последующей отписки.
        /// </summary>
        protected virtual void Subscribe()
        {
            foreach (var state in states)
            {
                Action enterHandler = () => OnEnterState(state);
                state.EnterState.OnEnter += enterHandler;
                enterHandlers[state] = enterHandler;
            }
        }

        /// <summary>
        /// Отписывается от ранее зарегистрированных обработчиков входа состояний.
        /// </summary>
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
        
        /// <summary>
        /// Вызывается при входе состояния; вычисляет корректный <c>blendTime</c> и запускает анимацию.
        /// </summary>
        /// <param name="enterData">Данные состояния анимации.</param>
        protected virtual void OnEnterState(AnimationStateEnterData enterData)
        {
            float blendTime = enterData.BlendTime;
            if (enterData.OverrideBlendTimes?.Count > 0)
            {
                string currentStateName = animatorService.GetCurrentAnimationName();
                if (!string.IsNullOrEmpty(currentStateName) &&
                    enterData.OverrideBlendTimes.TryGetValue(currentStateName, out blendTime)) { }
                else blendTime = enterData.BlendTime;
            }

            animatorService.Play(enterData.StateName, enterData.Clip, blendTime);
        }

        /// <summary>
        /// Освобождает ресурсы — отписывается от событий и очищает внутренние коллекции.
        /// </summary>
        public virtual void Dispose()
        {
            Unsubscribe();
            states.Clear();
            templateStates.Clear();
            templateStates = null;
        }
    }
}