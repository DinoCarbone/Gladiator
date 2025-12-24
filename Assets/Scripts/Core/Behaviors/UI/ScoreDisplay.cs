using System;
using Core.Providers;
using TMPro;
using Utils;

namespace Core.Behaviors.UI
{
    /// <summary>
    /// Отвечает за отображение счёта в UI (TextMeshProUGUI).
    /// </summary>
    public class ScoreDisplay : IDisposable
    {
        private readonly IScoreProvider scoreProvider;
        private readonly TextMeshProUGUI text;
        private const string AddedString = "Score: ";

        /// <summary>
        /// Создаёт отображение счёта и подписывается на обновления провайдера.
        /// </summary>
        /// <param name="scoreProvider">Провайдер счёта.</param>
        /// <param name="text">Текстовый компонент для вывода значения.</param>
        public ScoreDisplay(IScoreProvider scoreProvider, TextMeshProUGUI text)
        {
            this.scoreProvider = Extensions.AssignWithNullCheck(scoreProvider);
            this.text = Extensions.AssignWithNullCheck(text);
            UpdateScoreDisplay(this.scoreProvider.Score);
            Subscribe();
        }

        /// <summary>Подписывается на событие изменения счёта.</summary>
        private void Subscribe()
        {
            scoreProvider.OnScoreChanged += UpdateScoreDisplay;
        }

        /// <summary>Отписывается от события изменения счёта.</summary>
        private void Unsubscribe()
        {
            scoreProvider.OnScoreChanged -= UpdateScoreDisplay;
        }

        /// <summary>Обновляет текстовое представление счёта.</summary>
        /// <param name="score">Новое значение счёта.</param>
        private void UpdateScoreDisplay(int score)
        {
            text.text = $"{AddedString}{score}";
        }

        /// <summary>Отписывается и освобождает ресурсы.</summary>
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}