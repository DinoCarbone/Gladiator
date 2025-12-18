using System.Collections.Generic;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    [CreateAssetMenu(fileName = "InputAxisMovement", menuName = "ScriptableObjects/Providers/InputAxisMovementProvider")]
    public class InputAxisMovementProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new InputAxisMovementProvider();
        }
    }
}