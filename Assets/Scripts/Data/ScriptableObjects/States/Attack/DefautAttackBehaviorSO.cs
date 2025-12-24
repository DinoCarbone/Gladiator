using System;
using System.Collections.Generic;
using Core.Behaviors.States.Attack;
using Core.Services.States;
using UnityEngine;

namespace Data.ScriptableObjects.States.Attack
{
    [CreateAssetMenu(fileName = "DefautAttack",
     menuName = "ScriptableObjects/States/Attack/DefautAttack")]
    public class DefautAttackBehaviorSO : BehaviorSO<DefautAttack>
    {
        public override IState CreateConfigState(List<GameObject> contexts)
        {
            return new DefautAttack(GetIncompatibleTypes());
        }
        /// <summary>Создаёт конфигурацию состояния атаки и возвращает базовый тип поведения.</summary>
        public override Type GetBaseBehaviorType()
        {
            return typeof(BaseAttack);
        }
    }
}