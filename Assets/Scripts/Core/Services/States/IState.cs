using System;
using System.Collections.Generic;

namespace Core.Services.States
{
    public interface IState{}
    public interface IUpdateState : IState
    {
        void Update();
    }
    public interface IExitable : IState
    {
        bool CanExit { get; }
    }
    public interface IEnterable : IState
    {
        bool CanEnter { get; }
    }
    public interface IEnterState : IEnterable
    {
        void Enter();
    }
    public interface IExitState : IExitable
    {
        void Exit();
    }
    public interface IIncompatibleStates : IState
    {
        IReadOnlyList<Type> IncompatibleStates { get; }
    } 
}