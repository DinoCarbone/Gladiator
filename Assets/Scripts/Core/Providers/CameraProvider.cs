using UnityEngine;

namespace Core.Providers
{
    public class CameraProvider : ICameraProvider
    {
        private readonly Camera mainCamera;
        /// <summary>Инициализирует провайдера с указанной камерой.</summary>
        /// <param name="mainCamera">Главная камера сцены.</param>
        public CameraProvider(Camera mainCamera)
        {
            this.mainCamera = mainCamera;
        }

        /// <summary>Трансформ главной камеры.</summary>
        public Transform CameraTransform => mainCamera.transform;

        /// <summary>Ссылка на главный <see cref="Camera"/>.</summary>
        public Camera MainCamera => mainCamera;
    }
}