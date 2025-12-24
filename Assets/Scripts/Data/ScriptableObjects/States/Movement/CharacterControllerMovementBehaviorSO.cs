using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "CharacterControllerMovement", 
    menuName = "ScriptableObjects/States/Movement/CharacterControllerMovement")]
    public class CharacterControllerMovementBehaviorSO : BehaviorSO<CharacterControllerMovementState>
    {
        [SerializeField, Tooltip("Movement speed used by the CharacterController.")]
        private float moveSpeed = 5f;
        
        /// <summary>
        /// Создаёт конфигурируемое состояние движения на основе найденного компонента <see cref="CharacterController"/> в контекстах.
        /// </summary>
        /// <param name="contexts">Список объектов-контекстов, среди которых ищется необходимый компонент.</param>
        /// <returns>Экземпляр <see cref="IState"/> для данного поведения.</returns>
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

        /// <summary>Возвращает базовый тип поведения, с которым совместимо это ScriptableObject.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseMovement);
        }
    }
}