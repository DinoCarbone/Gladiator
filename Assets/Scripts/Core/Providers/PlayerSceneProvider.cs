using UnityEngine;
using Utils;

namespace Core.Providers
{
    public class PlayerSceneProvider : MonoBehaviour, IPlayerSceneProvider, IPlayerCameraPoint
    {
        [SerializeField, Tooltip("Transform used as the camera look target.")]
        private Transform pointToLoockCamera;

        public Transform Transform => transform;

        public Transform PointToLoockCamera => pointToLoockCamera;
    }
}