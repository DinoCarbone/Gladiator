using System;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using Data.Dto;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "TransformRotationBehavior", menuName = "ScriptableObjects/States/Movement/TransformRotationBehavior")]
    public class TransformRotationBehaviorSO : BehaviorSO<TransformRotation>
    {
        [SerializeField, Tooltip("Rotation speed applied to the transform.")]
        private float speed = 10f;
        /// <summary>
        /// Создаёт состояние поворота трансформа по найденному Transform в контекстах.
        /// </summary>
        /// <param name="contexts">Список объектов-контекстов для поиска Transform.</param>
        /// <returns>Экземпляр состояния поворота.</returns>
        public override IState CreateConfigState(params object[] dependencies)
        {
            Transform rootTransform = null;

            if (dependencies != null && dependencies.Length > 0)
            {
                rootTransform = dependencies[0] as Transform ?? (dependencies[0] as GameObject)?.GetComponent<Transform>();
            }
            if(rootTransform == null)
            throw new Exception($"TransformRotationBehaviorSO: Transform is empty.");

            return new TransformRotation(GetIncompatibleTypes(), rootTransform, speed);
        }

        /// <summary>Возвращает базовый тип поведения (вращение), совместимый с данным SO.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseRotation);
        }

        public override ContextRequirement[] GetContextRequirements()
        {
            return 
            new ContextRequirement[]
            {
                new ContextRequirement
                {
                    displayName = "Transform",
                    typeName = "UnityEngine.Transform, UnityEngine",
                    optional = false
                }
            };
        }
    }
}