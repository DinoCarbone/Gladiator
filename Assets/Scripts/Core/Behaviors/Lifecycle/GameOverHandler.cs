using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Core.Behaviors.Lifecycle
{
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

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}