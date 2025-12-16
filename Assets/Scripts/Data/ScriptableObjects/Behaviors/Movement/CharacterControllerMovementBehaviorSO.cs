using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors.Movement
{
    [CreateAssetMenu(fileName = "CharacterControllerMovement", menuName = "ScriptableObjects/Behaviors/Movement/CharacterController")]
    public class CharacterControllerMovementBehaviorSO : BehaviorSO<CharacterControllerMovementState>
    {
        [SerializeField] private float moveSpeed = 5f;
        
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            CharacterController controller = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out controller)) 
                break;
            }
            if(controller == null)
            throw new Exception($"CharacterControllerMovementBehaviorSO: Не удалось найти компонент  CharacterController в контекстах для создания состояния {nameof(CharacterControllerMovementState)}.");

            return new CharacterControllerMovementState(controller ,GetIncompatibleTypes(), moveSpeed);
        }

        public override Type GetBehaviorType()
        {
            return typeof(CharacterControllerMovementState);
        }
    }
}