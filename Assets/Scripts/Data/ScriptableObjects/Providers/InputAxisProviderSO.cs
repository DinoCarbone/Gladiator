using System.Collections.Generic;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    [CreateAssetMenu(fileName = "InputAxisProvider", menuName = "ScriptableObjects/Providers/InputAxisProvider")]
    public class InputAxisProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new InputAxisProvider();
        }
    }
}