using System;
using Core.Providers;
using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Маркер интерфейса данных, описывающих убиваемый объект.
    /// </summary>
    public interface IKillableData{}

    /// <summary>
    /// Данные специфичные для игрока (маркерный интерфейс).
    /// </summary>
    public interface IPlayerKillableData : IKillableData{}

    /// <summary>
    /// Данные убиваемого врага, включают стоимость очков и ссылку на игровой объект.
    /// </summary>
    public interface IEnemyKillableData : IKillableData, IScoreCostData
    {
        /// <summary>Ссылка на основной GameObject сущности врага.</summary>
        public GameObject CoreGameObject { get; }
    }

    /// <summary>
    /// Сервис регистрации смертей сущностей.
    /// </summary>
    public interface IDeathService
    {
        /// <summary>Регистрирует смерть по переданным данным.</summary>
        /// <param name="killable">Данные убиваемого объекта.</param>
        void RegisterDeath(IKillableData killable);
    }

    /// <summary>
    /// Нотификатор, оповещающий о смерти врага.
    /// </summary>
    public interface IEnemyDeathNotifier
    {
        /// <summary>Событие, вызываемое при смерти врага.</summary>
        event Action<IEnemyKillableData> OnEnemyDied;
    }

    /// <summary>
    /// Нотификатор, оповещающий о гибели игрока.
    /// </summary>
    public interface IPlayerDeathNotifier
    {
        /// <summary>Событие, вызываемое при смерти игрока.</summary>
        event Action<IPlayerKillableData> OnPlaerDied;
    }

    /// <summary>
    /// Менеджер спавна/деспавна врагов.
    /// </summary>
    public interface IEnemySpawner
    {
        /// <summary>Создаёт сущность врага.</summary>
        void Spawn();

        /// <summary>Удаляет сущность врага.</summary>
        /// <param name="gameObject">GameObject, подлежащий удалению.</param>
        void Despawn(GameObject gameObject);
    }

    /// <summary>
    /// Добавляет очки в систему подсчёта.
    /// </summary>
    public interface IScoreAdder
    {
        /// <summary>Добавляет очки на основе переданных данных стоимости.</summary>
        /// <param name="scoreCostData">Данные с информацией о цене в очках.</param>
        void AddScore(IScoreCostData scoreCostData);
    }

    /// <summary>
    /// Фабрика создания врагов.
    /// </summary>
    public interface IEnemyFactory
    {
        /// <summary>Создаёт и возвращает GameObject сущности.</summary>
        public GameObject Create();
    }
}