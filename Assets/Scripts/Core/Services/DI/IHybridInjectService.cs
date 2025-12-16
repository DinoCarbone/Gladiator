using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zenject;

namespace Core.Services.DI
{
    public interface IHybridInjectService
    {
        public void InjectAll<T>(List<T> objects);
    }
    public class HybridInjectService : IHybridInjectService
    {
        readonly DiContainer container;

        public HybridInjectService(DiContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Прокидывает зависимости во все объекты списка:
        /// - сначала из DI
        /// - затем из этого же списка
        /// </summary>
        public void InjectAll<T>(List<T> objects)
        {
            var pool = objects.Cast<object>().ToList();

            foreach (var obj in objects)
                InjectInto(obj, pool);
        }

        void InjectInto(object target, List<object> pool)
        {
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetCustomAttribute<InjectAttribute>() != null);

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                var args = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;

                    if (container.HasBinding(paramType))
                    {
                        args[i] = container.Resolve(paramType);
                        continue;
                    }

                    var fromPool = pool.FirstOrDefault(o => paramType.IsInstanceOfType(o));
                    if (fromPool != null)
                    {
                        args[i] = fromPool;
                        continue;
                    }

                    throw new Exception(
                        $"Cannot resolve dependency {paramType.Name} for {target.GetType().Name}"
                    );
                }

                method.Invoke(target, args);
            }
        }
    }
}