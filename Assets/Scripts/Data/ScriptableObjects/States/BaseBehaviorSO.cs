using Core.Services.States;

namespace Data.ScriptableObjects.States
{
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
              public abstract IState CreateConfigState(params object[] dependencies);
     }
}
