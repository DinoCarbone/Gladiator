using System;
using UnityEngine;

namespace Core.Services.Input
{
    /// <summary>Провайдер оси движения.</summary>
    public interface IMovementInput
    {
        /// <summary>Признак того, что ввод сейчас обрабатывается.</summary>
        bool IsHandle { get; }

        /// <summary>Текущая ось движения.</summary>
        Vector2 Axis { get; }

        /// <summary>Событие: значение оси движения изменилось.</summary>
        event Action<Vector2> OnMovementAxisChanged;
    }

    /// <summary>Провайдер ввода мыши/прицела.</summary>
    public interface IMouseLookInput
    {
        /// <summary>Событие: направление обзора (ось) изменилось.</summary>
        event Action<Vector2> OnLookAxisChanged;
    }

    /// <summary>Провайдер для ввода атаки/стрельбы.</summary>
    public interface IAttackInput
    {
        /// <summary>Возвращает, была ли атака в текущем кадре.</summary>
        bool IsAttack { get; }

        /// <summary>Событие: кнопка огня нажата.</summary>
        event Action OnFirePressed;

        /// <summary>Событие: кнопка огня отпущена.</summary>
        event Action OnFireReleased;
    }
}