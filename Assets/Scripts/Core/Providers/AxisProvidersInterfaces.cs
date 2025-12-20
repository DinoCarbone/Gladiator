using System;
using UnityEngine;

namespace Core.Providers
{
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
}