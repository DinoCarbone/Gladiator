using System;
using UnityEngine;

namespace Core.Providers
{
    public interface IAxisMovementProvider : IProvider
    {
        Vector2 Axis { get; }
        public event Action<Vector2> OnAxisChanged;
    }
    public interface IAxisRotationProvider : IProvider
    {
        Quaternion Rotation { get; }
        public event Action<Quaternion> OnAxisRotation;
    }
}