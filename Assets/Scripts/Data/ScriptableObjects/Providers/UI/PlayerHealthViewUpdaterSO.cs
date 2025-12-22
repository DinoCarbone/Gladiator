using System.Collections.Generic;
using Core.Behaviors.UI;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.UI
{
    [CreateAssetMenu(fileName = "PlayerHealthViewUpdater",
      menuName = "ScriptableObjects/Providers/UI/PlayerHealthViewUpdater")]
    public class PlayerHealthViewUpdaterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new PlayerHealthViewUpdater();
        }
    }
}