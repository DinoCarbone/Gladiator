using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
     /// <summary>
     /// Базовый ScriptableObject для описания поведения (создаёт конфигурационное состояние по контекстам).
     /// </summary>
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
          /// <summary>Создаёт конкретную конфигурацию состояния для данного SO.</summary>
          public abstract IState CreateConfigState(List<GameObject> contexts);
     }
}
