using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class CharacterControllerMovementState : BaseAxisMovement
    {
        protected float speed = 5f;
        protected CharacterController controller;

        /// <summary>
        /// Конструктор состояния движения на основе <see cref="CharacterController"/>.
        /// </summary>
        /// <param name="controller">Контроллер персонажа, используемый для перемещения.</param>
        /// <param name="incompatibleStates">Список несовместимых типов состояний.</param>
        /// <param name="startSpeed">Начальная скорость движения.</param>
        public CharacterControllerMovementState(CharacterController controller, List<Type> incompatibleStates, float startSpeed = 5f) : base(incompatibleStates)
        {
            speed = startSpeed;
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }

        /// <summary>
        /// Обрабатывает движение персонажа по заданной оси.
        /// </summary>
        /// <param name="axis">Вектор входного осевого управления.</param>
        protected override void OnMove(Vector2 axis)
        {
            // Движение вперед/назад относительно вращения
            Vector3 move = controller.transform.forward * axis.y +
                           controller.transform.right * axis.x;

            move.y = 0; // Убираем вертикальную составляющую

            if (move.magnitude > 0.1f)
            {
                move.Normalize();
                controller.Move(move * speed * Time.deltaTime);
            }
        }
    }
}