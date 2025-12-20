using System;
using UnityEngine;

namespace Core.Providers
{
    public interface IProvider{}
    public interface IAxisMovementProvider : IProvider
    {
        bool IsHandle { get; }
        Vector2 Axis { get; }
    }
    public interface IAxisRotationProvider : IProvider
    {
        Quaternion Rotation { get; }
    }
    public interface IAttackProvider : IProvider
    {
        bool IsAttack { get; }
    }
    public interface ICameraProvider
    {
        Transform CameraTransform { get; }
        Camera MainCamera { get; }
    }
    public interface IDamageProvider : IProvider
    {
        event Action<int> OnTakeDamage;
    }
}