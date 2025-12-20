using UnityEngine;

namespace Core.Providers
{
    public class CameraProvider : ICameraProvider
    {
        private readonly Camera mainCamera;
        public CameraProvider(Camera mainCamera)
        {
            this.mainCamera = mainCamera;
        }
        public Transform CameraTransform => mainCamera.transform;
        public Camera MainCamera => mainCamera;
    }
}