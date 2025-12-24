using System;
using Core.Providers;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Трекер очков: подписывается на события смерти врага и накапливает счёт.
    /// </summary>
    public class ScoreTracker : IScoreAdder, IScoreProvider, IDisposable
    {
        private readonly IEnemyDeathNotifier enemyDeathNotifier;

        /// <summary>Текущее количество очков.</summary>
        public int Score { get; private set; } = 0;

        /// <summary>Событие изменения счёта.</summary>
        public event Action<int> OnScoreChanged;

        public ScoreTracker(IEnemyDeathNotifier enemyDeathNotifier)
        {
            this.enemyDeathNotifier = Extensions.AssignWithNullCheck(enemyDeathNotifier);
            Subscribe();
        }

        private void Subscribe()
        {
            enemyDeathNotifier.OnEnemyDied += AddScore;
        }

        private void Unsubscribe()
        {
            enemyDeathNotifier.OnEnemyDied -= AddScore;
        }

        /// <summary>Добавляет очки по данным стоимости и уведомляет слушателей.</summary>
        public void AddScore(IScoreCostData scoreCostData)
        {
            Score += scoreCostData.Cost;
            OnScoreChanged?.Invoke(Score);
        }

        /// <summary>Отписывает обработчики.</summary>
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}