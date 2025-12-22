using System;
using Core.Providers;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    public class ScoreTracker : IScoreAdder, IScoreProvider, IDisposable
    {
        private readonly IEnemyDeathNotifier enemyDeathNotifier;
        public int Score { get; private set; } = 0;
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

        public void AddScore(IScoreCostData scoreCostData)
        {
            Score += scoreCostData.Cost;
            OnScoreChanged?.Invoke(Score);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}