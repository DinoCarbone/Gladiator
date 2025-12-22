using UnityEngine;
using Utils;

namespace Core.Providers
{
    public class PlayerSceneProvider : MonoBehaviour, IPlayerSceneProvider, IPlayerCameraPoint
    {
        [SerializeField] private  Transform pointToLoockCamera;
        public Transform Transform => transform;
        public Transform PointToLoockCamera => pointToLoockCamera;
    }
}