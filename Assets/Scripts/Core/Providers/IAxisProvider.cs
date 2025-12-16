using System;
using UnityEngine;

namespace Core.Providers
{
    public interface IAxisProvider : IProvider
    {
        Vector2 Axis { get; }
        public event Action<Vector2> OnAxisChanged;
    }
}