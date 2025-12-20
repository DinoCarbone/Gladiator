using System.Collections.Generic;
using Core.Providers;
using Core.Providers.Input;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Input
{
    [CreateAssetMenu(fileName = "InputAttackProvider", 
    menuName = "ScriptableObjects/Providers/Input/InputAttackProvider")]
    public class InputAttackProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            return new InputAttackProvider();
        }
    }
}