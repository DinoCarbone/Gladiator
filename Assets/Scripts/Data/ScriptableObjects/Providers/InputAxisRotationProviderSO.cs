using System.Collections.Generic;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    [CreateAssetMenu(fileName = "InputAxisRotationProvider", menuName = "ScriptableObjects/Providers/InputAxisRotationProvider")]
    public class InputAxisRotationProviderSO : BaseProviderSO
    {
        [SerializeField] private float rotationThreshold = 0.1f;
        public override IProvider CreateProvider(List<GameObject> _)
        {
            return new InputAxisRotationProvider(rotationThreshold);
        }
    }
}