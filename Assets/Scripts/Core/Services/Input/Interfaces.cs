using System;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IMovementInput
    {
        bool IsHandle { get; }
        Vector2 Axis { get; }
        event Action<Vector2> OnMovementAxisChanged; 
    }
    public interface IMouseLookInput
    {
        event Action<Vector2> OnLookAxisChanged; 
    }
    public interface IAttackInput
    {
        bool IsAttack { get; }
        event Action OnFirePressed;
        event Action OnFireReleased;
    }
}