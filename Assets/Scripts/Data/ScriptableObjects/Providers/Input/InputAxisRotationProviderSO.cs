using System.Collections.Generic;
using Core.Providers;
using Core.Providers.Input;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Input
{
    [CreateAssetMenu(fileName = "InputAxisRotationProvider", 
    menuName = "ScriptableObjects/Providers/Input/InputAxisRotationProvider")]
    public class InputAxisRotationProviderSO : BaseProviderSO
    {
        [SerializeField, Tooltip("Rotation input threshold to consider as movement.")]
        private float rotationThreshold = 0.1f;
        public override IProvider CreateProvider(List<GameObject> _)
        {
            /// <summary>Создаёт провайдер вращения оси с указанным порогом.</summary>
            return new InputAxisRotationProvider(rotationThreshold);
        }
    }
}