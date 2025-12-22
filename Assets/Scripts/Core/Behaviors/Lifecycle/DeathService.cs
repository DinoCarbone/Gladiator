using UnityEngine;

namespace Core.Behaviors.Lifecycle
{
    public class MockDeathService : IDeathService
    {
        public void RegisterDeath(IKillableData killable)
        {
            Debug.Log("RegisterDeath by type: " + killable.GetType().Name);
        }
    }
}