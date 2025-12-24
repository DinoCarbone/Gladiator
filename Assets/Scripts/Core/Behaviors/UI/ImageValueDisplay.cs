using UnityEngine.UI;
using UnityEngine;
using Utils;

namespace Core.Behaviors.UI
{
    /// <summary>
    /// Отображение числового значения через `UnityEngine.UI.Image.fillAmount`.
    /// </summary>
    public class ImageValueDisplay : IValueDisplay
    {
        private readonly Image fillImage;
        private int maxValue = 100;

        /// <summary>
        /// Создаёт инстанс отображения и настраивает полосу заполнения.
        /// </summary>
        /// <param name="fillImage">Image, используемый как полоска заполнения.</param>
        public ImageValueDisplay(Image fillImage)
        {
            this.fillImage = Extensions.AssignWithNullCheck(fillImage);
            this.fillImage.fillMethod = Image.FillMethod.Horizontal;
            this.fillImage.fillAmount = 1f;
        }

        /// <summary>
        /// Устанавливает максимальное значение для отображения.
        /// </summary>
        /// <param name="value">Новый максимум (должен быть > 0).</param>
        public void SetMaxValue(int value)
        {
            if (value <= 0) return;

            var currentFill = fillImage.fillAmount;
            var currentValue = Mathf.RoundToInt(currentFill * maxValue);

            maxValue = value;
            DisplayValue(currentValue);
        }

        /// <summary>
        /// Отображает текущее значение, переводя его в заполнение полосы.
        /// </summary>
        /// <param name="value">Значение для отображения.</param>
        public void DisplayValue(int value)
        {
            if (fillImage == null) return;

            var clampedValue = Mathf.Clamp(value, 0, maxValue);
            fillImage.fillAmount = (float)clampedValue / maxValue;
        }
    }
}