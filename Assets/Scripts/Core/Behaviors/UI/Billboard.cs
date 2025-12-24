using Core.Providers;
using UnityEngine;
using Zenject;
using Utils;

namespace Core.Behaviors.UI
{
    /// <summary>
    /// Простая Billboard-компонента: поворачивает объект в сторону камеры, предоставленной <see cref="ICameraProvider"/>.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        private Transform cameraTransform;
        
        [Inject]
        private void Construct(ICameraProvider cameraProvider)
        {
            cameraTransform = Extensions.AssignWithNullCheck(cameraProvider.CameraTransform);
        }

        private void LateUpdate()
        {
            transform.LookAt(cameraTransform);
        }
    }
}
