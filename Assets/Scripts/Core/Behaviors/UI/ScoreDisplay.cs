using System;
using Core.Providers;
using TMPro;
using Utils;

namespace Core.Behaviors.UI
{
    public class ScoreDisplay : IDisposable
    {
        private readonly IScoreProvider scoreProvider;
        private readonly TextMeshProUGUI text;
        private const string AddedString = "Score: ";
        public ScoreDisplay(IScoreProvider scoreProvider, TextMeshProUGUI text)
        {
            this.scoreProvider = Extensions.AssignWithNullCheck(scoreProvider);
            this.text = Extensions.AssignWithNullCheck(text);
            UpdateScoreDisplay(scoreProvider.Score);
            Subscribe();
        }
        private void Subscribe()
        {
            scoreProvider.OnScoreChanged += UpdateScoreDisplay;
        }
        private void Unsubscribe()
        {
            scoreProvider.OnScoreChanged -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int score)
        {
            text.text = $"{AddedString}{score}";
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}