using System.Collections.Generic;
using Core.Providers;
using Core.Providers.Input;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Input
{
    [CreateAssetMenu(fileName = "InputAxisMovement", 
    menuName = "ScriptableObjects/Providers/Input/InputAxisMovementProvider")]
    public class InputAxisMovementProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new InputAxisMovementProvider();
        }
    }
}