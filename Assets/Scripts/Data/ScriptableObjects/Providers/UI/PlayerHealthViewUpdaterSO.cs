using Core.Behaviors.UI;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.UI
{
    [CreateAssetMenu(fileName = "PlayerHealthViewUpdater",
      menuName = "ScriptableObjects/Providers/UI/PlayerHealthViewUpdater")]
    public class PlayerHealthViewUpdaterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            /// <summary>Создаёт провайдер отображения здоровья игрока.</summary>
            return new PlayerHealthViewUpdater();
        }
    }
}