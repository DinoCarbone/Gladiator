using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Behaviors.States.Movement
{
    public class TransformRotation : BaseAxisRotation
    {
        private float rotationSpeed = 10;
        private Transform rootTransform;

        public TransformRotation(List<Type> incompatibleStates, Transform rootTransform,
         float rotationSpeed) : base(incompatibleStates)
        {
            this.rootTransform = Extensions.AssignWithNullCheck(rootTransform);
            this.rotationSpeed = rotationSpeed;
        }
        protected override void OnRotation(Quaternion targetRotation)
        {
            rootTransform.rotation = Quaternion.Slerp(
                rootTransform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}