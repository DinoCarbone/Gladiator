using System;
using System.Collections.Generic;

namespace Core.Services.States
{
    /// <summary>
    /// Маркерный интерфейс для всех состояний.
    /// </summary>
    public interface IState { }

    /// <summary>
    /// Состояние, требующее обновления каждый кадр.
    /// </summary>
    public interface IUpdateState : IState
    {
        /// <summary>Выполняет логику обновления состояния.</summary>
        void Update();
    }

    /// <summary>Интерфейс для состояния, из которого можно выйти.</summary>
    public interface IExitable : IState
    {
        /// <summary>Возвращает, можно ли выйти из состояния.</summary>
        bool CanExit { get; }

        /// <summary>Событие, вызываемое при выходе из состояния.</summary>
        event Action OnExit;
    }

    /// <summary>Интерфейс для состояния, в которое можно войти.</summary>
    public interface IEnterable : IState
    {
        /// <summary>Возвращает, можно ли войти в состояние.</summary>
        bool CanEnter { get; }

        /// <summary>Событие, вызываемое при входе в состояние.</summary>
        event Action OnEnter;
    }

    /// <summary>Состояние с явным методом входа.</summary>
    public interface IEnterState : IEnterable
    {
        /// <summary>Выполняет логику при входе в состояние.</summary>
        void Enter();
    }

    /// <summary>Состояние с явным методом выхода.</summary>
    public interface IExitState : IExitable
    {
        /// <summary>Выполняет логику при выходе из состояния.</summary>
        void Exit();
    }

    /// <summary>Интерфейс для актора, инициирующего выход из состояния.</summary>
    public interface IExitActivator : IState
    {
        /// <summary>Активирует условие выхода для связанного состояния.</summary>
        void ActivateExit();
    }

    /// <summary>Состояние, которое объявляет несовместимые типы состояний.</summary>
    public interface IIncompatibleStates : IState
    {
        /// <summary>Список типов состояний, несовместимых с данным состоянием.</summary>
        IReadOnlyList<Type> IncompatibleStates { get; }
    }

    /// <summary>Делегат для действия над состоянием.</summary>
    /// <param name="state">Состояние, над которым производится действие.</param>
    public delegate void StateActionDelegate(IState state);

    /// <summary>Делегат для проверки условия состояния.</summary>
    /// <param name="state">Проверяемое состояние.</param>
    /// <returns>True если условие выполнено.</returns>
    public delegate bool StateConditionDelegate(IState state);

    /// <summary>Делегат для получения списка несовместимых типов для состояния.</summary>
    /// <param name="state">Исходное состояние.</param>
    /// <returns>Список несовместимых типов.</returns>
    public delegate IReadOnlyList<Type> StateIncompatibleDelegate(IState state);
}