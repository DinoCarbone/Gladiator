using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    public abstract class BaseProviderSO : ScriptableObject
    {
        public abstract IProvider CreateProvider();
    }
}