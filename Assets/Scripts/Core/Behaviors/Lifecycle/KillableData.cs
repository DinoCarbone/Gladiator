using Core.Providers;
using UnityEngine;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class PlayerKillableData : IPlayerKillableData{}
    public class EnemyKillableData : IEnemyKillableData
    {
        /// <summary>Ссылка на внутренний GameObject сущности (ядро).</summary>
        private readonly GameObject coreGameObject;

        /// <summary>Ядро сущности.</summary>
        public GameObject CoreGameObject => coreGameObject;

        /// <summary>Стоимость/ценность сущности (используется при начислении очков и т.д.).</summary>
        private readonly int cost;

        /// <summary>Стоимость сущности.</summary>
        public int Cost => cost;

        /// <summary>Создаёт данные для убиваемой сущности-врага.</summary>
        /// <param name="coreGameObject">GameObject ядра сущности.</param>
        /// <param name="cost">Стоимость сущности (целое неотрицательное значение).</param>
        public EnemyKillableData(GameObject coreGameObject, int cost)
        {
            this.cost = Extensions.AssignWithZeroCheck(cost);
            this.coreGameObject = Extensions.AssignWithNullCheck(coreGameObject);
        }
    }
}