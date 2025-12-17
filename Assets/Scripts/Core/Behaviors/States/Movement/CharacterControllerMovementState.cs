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
            Debug.Log("CharacterControllerMovementState Create");
            speed = startSpeed;
            this.controller = Utils.Extensions.AssignWithNullCheck(controller);
        }
        protected override void OnMove(Vector2 axis)
        {
            Vector3 move = new Vector3(axis.x, 0, axis.y);
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}