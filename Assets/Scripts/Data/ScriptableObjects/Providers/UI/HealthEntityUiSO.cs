using System;
using System.Collections.Generic;
using Core.Behaviors.UI;
using Core.Providers;
using UnityEngine;
using UnityEngine.UI;

namespace Data.ScriptableObjects.Providers.UI
{
    [CreateAssetMenu(fileName = "HealthEntity",
      menuName = "ScriptableObjects/Providers/UI/HealthEntity")]
    public class HealthEntityUiSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            Image barImage = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out barImage)) 
                break;
            }
            if(barImage == null)
            throw new Exception("HealthEntityUI: barImage is empty");
            IValueDisplay valueDisplay = new ImageValueDisplay(barImage);
            return new HealthViewUpdater(valueDisplay);
        }
    }
}