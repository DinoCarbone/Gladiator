using UnityEngine.UI;
using UnityEngine;
using Utils;

namespace Core.Behaviors.UI
{
    public class ImageValueDisplay : IValueDisplay
{
    private readonly Image fillImage;
    private int maxValue = 100;
    
    public ImageValueDisplay(Image fillImage)
    {
        this.fillImage = fillImage ?? throw new System.ArgumentNullException(nameof(fillImage));
        
        fillImage = Extensions.AssignWithNullCheck(fillImage);
        fillImage.fillMethod = Image.FillMethod.Horizontal;
        fillImage.fillAmount = 1f;
    }
    
    public void SetMaxValue(int value)
    {
        if (value <= 0) return;
        
        var currentFill = fillImage.fillAmount;
        var currentValue = Mathf.RoundToInt(currentFill * maxValue);
        
        maxValue = value;
        DisplayValue(currentValue);
    }
    
    public void DisplayValue(int value)
    {
        if (fillImage == null) return;
        
        var clampedValue = Mathf.Clamp(value, 0, maxValue);
        fillImage.fillAmount = (float)clampedValue / maxValue;
    }
}
}