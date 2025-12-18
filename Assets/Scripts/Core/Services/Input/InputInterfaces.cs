using System;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IMovementInput
    {
        event Action<Vector2> OnMovementAxisChanged; 
    }
    public interface IMouseLookInput
    {
        event Action<Vector2> OnLookAxisChanged; 
    }
}