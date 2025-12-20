using System.Collections.Generic;
using Data.ScriptableObjects.Animatios;
using Data.Serialization;
using UnityEngine;
using Utils;

namespace Data.ScriptableObjects.Providers.Animation
{
     [CreateAssetMenu(fileName = "AnimationProvider",
      menuName = "ScriptableObjects/Providers/Animations/AnimationProvider")]
    public class AnimationProviderSO : AnimationProviderSOBase
    {
        [SerializeField] private List<AnimationSerializeTypeData> animationSerializeTypeDatas;
        public override List<AnimationStateTypeData> GetAnimationStateTypeDatas()
        {
            List<AnimationStateTypeData> stateTypeDatas = new List<AnimationStateTypeData>();
            foreach (AnimationSerializeTypeData data in animationSerializeTypeDatas)
            {
                if(data.animationStateSO == null || string.IsNullOrEmpty(data.animationStateSO.StateName))
                {
                    Debug.LogError("AnimationStateSO.StateName is null or empty");
                    continue;
                }
                else if (data.behaviorTypeSO == null)
                {
                    Debug.LogError("BehaviorTypeSO is null");
                    continue;
                }
                
                stateTypeDatas.Add(new AnimationStateTypeData(data.animationStateSO.StateName, null,
                 data.behaviorTypeSO.GetBaseBehaviorType(), data.baseBlendTime,
                  Extensions.GetOverrideBlendTimes(data.overrideBlendTimeDatas)));
            }
            return stateTypeDatas;
        }

        internal AnimationStateSO GetAnimationStateSO(string stateName)
        {
            foreach (AnimationSerializeTypeData data in animationSerializeTypeDatas)
            {
                if (data.animationStateSO != null && data.animationStateSO.StateName == stateName)
                {
                    return data.animationStateSO;
                }
            }
            return null;
        }
    }
}