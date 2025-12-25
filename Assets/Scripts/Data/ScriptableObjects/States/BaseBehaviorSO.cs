using Core.Services.States;

namespace Data.ScriptableObjects.States
{
     /// <summary>
     /// Базовый ScriptableObject для описания поведения (создаёт конфигурационное состояние по контекстам).
     /// </summary>
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
              /// <summary>Создаёт конкретную конфигурацию состояния для данного SO.</summary>
              /// <param name="dependencies">Набор зависимостей, переданных из инспектора в том порядке, как объявлено в `contextRequirements`.</param>
              public abstract IState CreateConfigState(params object[] dependencies);
     }
}
