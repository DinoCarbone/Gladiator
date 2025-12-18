using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviors.States.Movement
{
    public class CharacterControllerMovementState : BaseAxisMovement
    {
        protected float speed = 5f;
        protected CharacterController controller;

        public CharacterControllerMovementState(CharacterController controller, List<Type> incompatibleStates, float startSpeed = 5f) : base(incompatibleStates)
        {
            speed = startSpeed;
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }
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