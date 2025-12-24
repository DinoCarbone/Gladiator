using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zenject;

namespace Core.Services.DI
{
    /// <summary>
    /// Сервис гибридной инъекции зависимостей: Zenject + взаимные ссылки внутри набора объектов.
    /// </summary>
    public interface IHybridInjectService
    {
        /// <summary>
        /// Выполняет инъекцию зависимостей во все элементы списка.
        /// Использует Zenject <see cref="DiContainer"/> и разрешает зависимости внутри переданного набора объектов.
        /// </summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="objects">Список объектов для инъекции.</param>
        public void InjectAll<T>(List<T> objects);
    }
    public class HybridInjectService : IHybridInjectService
    {
        readonly DiContainer container;

        /// <summary>
        /// Создаёт сервис инъекций на основе предоставленного <see cref="DiContainer"/>.
        /// </summary>
        /// <param name="container">Zenject DiContainer.</param>
        public HybridInjectService(DiContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Прокидывает зависимости во все объекты списка: сначала пытается разрешить зависимости через контейнер,
        /// затем ищет соответствующие объекты в пуле переданного списка.
        /// </summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="objects">Список объектов.</param>
        public void InjectAll<T>(List<T> objects)
        {
            var pool = objects.Cast<object>().ToList();

            foreach (var obj in objects)
                InjectInto(obj, pool);
        }

        /// <summary>
        /// Выполняет инъекцию в конкретный объект: ищет методы с атрибутом <see cref="InjectAttribute"/> и вызывает их с разрешёнными аргументами.
        /// Попытка разрешения параметров идёт через контейнер, затем через пул объектов; при неудаче бросается исключение.
        /// </summary>
        /// <param name="target">Целевой объект для инъекции.</param>
        /// <param name="pool">Пул объектов для разрешения зависимостей внутри набора.</param>
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