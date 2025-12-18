using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors
{
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
          public abstract IState CreateConfigState(List<GameObject> contexts);
     }
}
