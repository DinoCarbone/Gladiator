using Core.Providers;
using Core.Providers.Input;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Input
{
    [CreateAssetMenu(fileName = "InputAxisMovement", 
    menuName = "ScriptableObjects/Providers/Input/InputAxisMovementProvider")]
    public class InputAxisMovementProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] _)
        {
            /// <summary>Создаёт провайдер для осевого движения (на основе `IMovementInput`).</summary>
            return new InputAxisMovementProvider();
        }
    }
}