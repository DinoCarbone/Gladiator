using System;
using System.Collections.Generic;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.Behaviors
{
     public abstract class BaseBehaviorSO : ScriptableObject
     {
          public abstract IState CreateConfigState(List<GameObject> contexts);

          public abstract Type GetBaseBehaviorType();
     }
}
