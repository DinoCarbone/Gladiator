using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States
{
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
          public abstract IState CreateConfigState(List<GameObject> contexts);
     }
}
