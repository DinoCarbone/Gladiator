using System.Collections.Generic;
using Core.Providers;
using UnityEngine;

namespace Data.ScriptableObjects.Providers
{
    public abstract class BaseProviderSO : ScriptableObject
    {
        /// <summary>
        /// Создаёт провайдер на основе конфигурации ScriptableObject и переданных контекстов.
        /// </summary>
        /// <param name="contexts">Список GameObject-контекстов (например, для поиска компонентов).</param>
        /// <returns>Созданный `IProvider`.</returns>
        public abstract IProvider CreateProvider(List<GameObject> contexts);
    }
}