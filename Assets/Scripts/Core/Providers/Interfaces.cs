using System;
using UnityEngine;

namespace Core.Providers
{
    /// <summary>
    /// Базовый маркерный интерфейс для провайдеров игровых данных и ввода.
    /// </summary>
    public interface IProvider {}

    /// <summary>
    /// Провайдер осевого перемещения (например, для управления движением персонажа).
    /// </summary>
    public interface IAxisMovementProvider : IProvider
    {
        /// <summary>Флаг — нужно ли обрабатывать текущее значение оси.</summary>
        bool IsHandle { get; }

        /// <summary>Текущее значение осей ввода.</summary>
        Vector2 Axis { get; }
    }

    /// <summary>
    /// Провайдер поворота, возвращающий целевую ротацию.
    /// </summary>
    public interface IAxisRotationProvider : IProvider
    {
        /// <summary>Запрашиваемая ротация.</summary>
        Quaternion Rotation { get; }
    }

    /// <summary>Провайдер сигнала атаки (булев флаг).</summary>
    public interface IAttackProvider : IProvider
    {
        /// <summary>Истина, если инициирована атака.</summary>
        bool IsAttack { get; }
    }

    /// <summary>Провайдер, излучающий событие нанесения урона.</summary>
    public interface IDamageProvider : IProvider
    {
        /// <summary>Событие — получен урон с указанной величиной.</summary>
        event Action<int> OnTakeDamage;
    }

    /// <summary>Провайдер, сигнализирующий об уничтожении/смерти сущности.</summary>
    public interface IDeathProvider : IProvider
    {
        /// <summary>Событие — сущность умерла.</summary>
        event Action OnDie;
    }

    /// <summary>Провайдер, предоставляющий данные по камере сцены.</summary>
    public interface ICameraProvider
    {
        /// <summary>Трансформ камеры.</summary>
        Transform CameraTransform { get; }

        /// <summary>Ссылка на главный `Camera` сцены.</summary>
        Camera MainCamera { get; }
    }

    /// <summary>Провайдер сцены игрока, дающий доступ к Transform игрока.</summary>
    public interface IPlayerSceneProvider
    {
        /// <summary>Трансформ игрока в сцене.</summary>
        Transform Transform { get; }
    }

    /// <summary>Точка в сцене, на которую следует смотреть (камера игрока).</summary>
    public interface IPlayerCameraPoint
    {
        /// <summary>Точка для наведения камеры/слежения.</summary>
        Transform PointToLoockCamera { get; }
    }

    /// <summary>Провайдер очков/скора игры.</summary>
    public interface IScoreProvider
    {
        /// <summary>Текущее количество очков.</summary>
        int Score { get; }

        /// <summary>Событие — изменилось значение очков.</summary>
        event Action<int> OnScoreChanged;
    }

    /// <summary>Данные стоимости, используемые для расчёта изменения очков.</summary>
    public interface IScoreCostData
    {
        /// <summary>Стоимость/вес для начисления очков.</summary>
        int Cost { get; }
    }
}