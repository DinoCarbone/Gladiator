using System;
using Core.Behaviors.UI;
using Core.Providers;
using Data.Dto;
using UnityEngine;
using UnityEngine.UI;

namespace Data.ScriptableObjects.Providers.UI
{
    [CreateAssetMenu(fileName = "HealthEntity",
      menuName = "ScriptableObjects/Providers/UI/HealthEntity")]
    public class HealthEntityUiSO : BaseProviderSO
    {
        /// <summary>Создаёт визуализацию здоровья (`HealthViewUpdater`) на основе найденного `Image`.</summary>
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Image barImage = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                barImage = dependencies[0] as Image ?? (dependencies[0] as GameObject)?.GetComponent<Image>();
            }
            if(barImage == null)
                throw new Exception("HealthEntityUI: barImage is empty");

            IValueDisplay valueDisplay = new ImageValueDisplay(barImage);
            return new HealthViewUpdater(valueDisplay);
        }

        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Health Bar Image",
                    typeName = "UnityEngine.UI.Image, UnityEngine.UI",
                    optional = false
                }
            };
        }
    }
}