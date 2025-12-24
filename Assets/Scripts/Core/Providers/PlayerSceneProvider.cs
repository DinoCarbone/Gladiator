using UnityEngine;
using Utils;

namespace Core.Providers
{
    /// <summary>
    /// Провайдер сцены игрока — хранит ссылку на трансформ игрока и точку камеры.
    /// Используется при создании объектов и при расчётах направления взгляда.
    /// </summary>
    public class PlayerSceneProvider : MonoBehaviour, IPlayerSceneProvider, IPlayerCameraPoint
    {
        [SerializeField, Tooltip("Transform used as the camera look target.")]
        private Transform pointToLoockCamera;

        /// <summary>Трансформ объекта, на котором находится провайдер (игрок).</summary>
        public Transform Transform => transform;

        /// <summary>Точка на объекте, используемая как опорная точка камеры.</summary>
        public Transform PointToLoockCamera => pointToLoockCamera;
    }
}