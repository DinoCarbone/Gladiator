using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    [CreateAssetMenu(fileName = "InputAxisProvider", menuName = "ScriptableObjects/Providers/InputAxisProvider")]
    public class InputAxisProviderSO : BaseProviderSO
    {
        public override IProvider CreateProvider()
        {
            return new InputAxisProvider();
        }
    }
}