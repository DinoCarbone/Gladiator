using UnityEngine;
using Utils;

namespace Core.Providers
{
    public class PlayerSceneProvider : IPlayerSceneProvider
    {
        public PlayerSceneProvider(Transform transform)
        {
            this.transform = Extensions.AssignWithNullCheck(transform);
        }
        private readonly Transform transform;
        public Transform Transform => transform;
    }
}