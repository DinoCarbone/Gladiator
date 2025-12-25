using Core.Providers;

namespace Data.ScriptableObjects.Providers
{
    public abstract class BaseProviderSO : ContextRequirementsSO
    {
        /// <summary>
        /// Создаёт провайдер на основе конфигурации ScriptableObject и переданных контекстов.
        /// </summary>
        /// <param name="dependencies">Набор зависимостей, переданных из инспектора в том порядке, как объявлено в `contextRequirements`.
        /// Каждый элемент будет того типа, который объявлен в `ContextRequirement.typeName` (или `UnityEngine.Object`).</param>
        /// <returns>Созданный `IProvider`.</returns>
        public abstract IProvider CreateProvider(params object[] dependencies);
    }
}