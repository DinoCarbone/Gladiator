using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "CharacterControllerMovement",
    menuName = "ScriptableObjects/States/Movement/CharacterControllerMovement")]
    public class CharacterControllerMovementBehaviorSO : BehaviorSO<CharacterControllerMovementState>
    {
        [SerializeField, Tooltip("Movement speed used by the CharacterController.")]
        private float moveSpeed = 5f;

        public override IState CreateConfigState(params object[] dependencies)
        {
            CharacterController controller = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                controller = dependencies[0] as CharacterController ?? (dependencies[0] as GameObject)?.GetComponent<CharacterController>();
            }
            if(controller == null)
            throw new Exception($"CharacterControllerMovementBehaviorSO: CharacterController is empty");

            return new CharacterControllerMovementState(controller ,GetIncompatibleTypes(), moveSpeed);
        }
        public override ContextRequirement[] GetContextRequirements()
        {
            return
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "CharacterController",
                    typeName = "UnityEngine.CharacterController, UnityEngine",
                    optional = false
                }
            };
        }

        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseMovement);
        }
    }
}