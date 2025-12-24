using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Core.Behaviors.Lifecycle
{
    /// <summary>
    /// Обрабатывает событие гибели игрока и перезагружает сцену (Game Over).
    /// </summary>
    public class GameOverHandler
    {
        private readonly IPlayerDeathNotifier playerDeathNotifier;

        public GameOverHandler(IPlayerDeathNotifier playerDeathNotifier)
        {
            this.playerDeathNotifier = Extensions.AssignWithNullCheck(playerDeathNotifier);
            Subscribe();
        }

        private void Subscribe()
        {
            playerDeathNotifier.OnPlaerDied += OnLooseRecive;
        }

        private void Unsubscribe()
        {
            playerDeathNotifier.OnPlaerDied -= OnLooseRecive;
        }

        private void OnLooseRecive(IPlayerKillableData data)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>Отписывается от событий и освобождает ресурсы.</summary>
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}