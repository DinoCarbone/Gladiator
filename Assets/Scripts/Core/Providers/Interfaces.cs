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
    public interface IDamageProvider : IProvider
    {
        event Action<int> OnTakeDamage;
    }
    public interface IDeathProvider : IProvider
    {
        event Action OnDie;
    }
    public interface ICameraProvider
    {
        Transform CameraTransform { get; }
        Camera MainCamera { get; }
    }
    public interface IPlayerSceneProvider
    {
        Transform Transform { get; }
    }
    public interface IPlayerCameraPoint
    {
        Transform PointToLoockCamera { get; }
    }
    public interface IScoreProvider
    {
        int Score { get; }
        event Action<int> OnScoreChanged;
    }
    public interface IScoreCostData
    {
        int Cost { get; }
    }
}