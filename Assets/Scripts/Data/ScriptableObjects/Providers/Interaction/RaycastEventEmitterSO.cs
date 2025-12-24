using System;
using System.Collections.Generic;
using Core.Behaviors.Interaction;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "RaycastEventEmitter",
      menuName = "ScriptableObjects/Providers/Interactions/RaycastEventEmitter")]
    public class RaycastEventEmitterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(List<GameObject> contexts)
        {
            /// <summary>Создаёт `RaycastEventEmitter` для указанного источника (Transform) в контекстах.</summary>
            Transform transformObject = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out transformObject)) 
                break;
            }
            if(transformObject == null)
            throw new Exception($"RaycastEventEmitterSO: Transform is empty");

            return new RaycastEventEmitter(transformObject);
        }
    }
}