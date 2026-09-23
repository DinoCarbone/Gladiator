using Core.Providers;

namespace Data.ScriptableObjects.Providers
{
    public abstract class BaseProviderSO : ContextRequirementsSO
    {
        public abstract IProvider CreateProvider(params object[] dependencies);
    }
}