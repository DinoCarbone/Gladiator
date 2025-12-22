using Core.Providers;
using UnityEngine;
using Zenject;
using Utils;

namespace Core.Behaviors.UI
{
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