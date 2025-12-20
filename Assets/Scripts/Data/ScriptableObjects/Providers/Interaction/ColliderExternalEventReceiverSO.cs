using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "ColliderExternalEventReceiver",
      menuName = "ScriptableObjects/Providers/Interactions/ColliderExternalEventReceiver")]
    public class ColliderExternalEventReceiverSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            Collider controller = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out controller)) 
                break;
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
    }
}