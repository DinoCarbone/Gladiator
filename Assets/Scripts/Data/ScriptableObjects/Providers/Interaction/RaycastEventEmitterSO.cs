using System;
using Core.Behaviors.Interaction;
using Core.Providers;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.Providers.Interaction
{
    [CreateAssetMenu(fileName = "RaycastEventEmitter",
      menuName = "ScriptableObjects/Providers/Interactions/RaycastEventEmitter")]
    public class RaycastEventEmitterSO : BaseProviderSO
    {
        public override IProvider CreateProvider(params object[] dependencies)
        {
            /// <summary>Создаёт `RaycastEventEmitter` для указанного источника (Transform) в контекстах.</summary>
            Transform transformObject = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                transformObject = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }
            if(transformObject == null)
            throw new Exception($"RaycastEventEmitterSO: Transform is empty");

            return new RaycastEventEmitter(transformObject);
        }
        
        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Source Transform",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}