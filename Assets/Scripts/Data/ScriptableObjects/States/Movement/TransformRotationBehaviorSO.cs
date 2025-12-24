using System;
using System.Collections.Generic;
using Core.Behaviors.States.Movement;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Movement
{
    [CreateAssetMenu(fileName = "TransformRotationBehavior", menuName = "ScriptableObjects/States/Movement/TransformRotationBehavior")]
    public class TransformRotationBehaviorSO : BehaviorSO<TransformRotation>
    {
        [SerializeField] private float speed = 10f;
        /// <summary>
        /// Создаёт состояние поворота трансформа по найденному Transform в контекстах.
        /// </summary>
        /// <param name="contexts">Список объектов-контекстов для поиска Transform.</param>
        /// <returns>Экземпляр состояния поворота.</returns>
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            Transform rootTransform = null;

            foreach (GameObject context in contexts)
            {
                if (context.TryGetComponent(out rootTransform)) 
                break;
            }
            if(rootTransform == null)
            throw new Exception($"TransformRotationBehaviorSO: Не удалось найти компонент  Transform в контекстах для создания состояния {nameof(TransformRotation)}.");

            return new TransformRotation(GetIncompatibleTypes(), rootTransform, speed);
        }

        /// <summary>Возвращает базовый тип поведения (вращение), совместимый с данным SO.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseRotation);
        }
    }
}