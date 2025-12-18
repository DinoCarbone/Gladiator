using Data.Serialization;
using UnityEngine;
using Zenject;

namespace Core.Providers
{
    public class ExampleProvider : IProvider
    {
        [Inject]
        private void Construct(StateListData states)
        {
            foreach (var state in states.States)
            {
                Debug.Log(state.GetType().Name);
            }
        }
    }
}