using System;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputAxisService
    {
        event Action<Vector2> OnAxisChanged; 
    }
}