using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "ColliderExternalEventReceiver",
      menuName = "ScriptableObjects/Providers/Interactions/ColliderExternalEventReceiver")]
    public class ColliderExternalEventReceiverSO : BaseProviderSO
    {
        /// <summary>Создаёт или возвращает существующий `ColliderExternalEventReceiver` на Collider из контекстов.</summary>
        public override IProvider CreateProvider(params object[] dependencies)
        {
            Collider controller = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                controller = dependencies[0] as Collider ?? (dependencies[0] as GameObject)?.GetComponent<Collider>();
            }
            if(controller == null)
                throw new Exception("ColliderExternalEventReceiverSO: collider is empty");

            ColliderExternalEventReceiver colliderExternalEventReceiver;

            if(controller.TryGetComponent(out colliderExternalEventReceiver))
            {
                return colliderExternalEventReceiver;
            }
            
            colliderExternalEventReceiver = controller.gameObject.AddComponent<ColliderExternalEventReceiver>();
            return colliderExternalEventReceiver;
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Collider",
                    typeName = "UnityEngine.Collider, UnityEngine",
                    optional = false
                }
            };
        }
    }
}