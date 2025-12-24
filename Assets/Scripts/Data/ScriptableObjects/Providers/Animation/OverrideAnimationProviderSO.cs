using System.Collections.Generic;
using Data.Serialization;
using UnityEngine;
using Utils;

namespace Data.ScriptableObjects.Providers.Animation
{
    [CreateAssetMenu(fileName = "OverrideAnimationProvider", 
    menuName = "ScriptableObjects/Providers/Animations/OverrideAnimationProvider")]
    public class OverrideAnimationProviderSO : AnimationProviderSOBase
    {
        [SerializeField, Tooltip("Base animation provider to override.")]
        private AnimationProviderSOBase animationProviderSO;

        [SerializeField, Tooltip("List of animation clip overrides for specific states.")]
        private List<AnimationSerializeClipData> clipDatas;
        /// <summary>
        /// Возвращает список состояний анимации с заменёнными клипами из `clipDatas`.
        /// Если для состояния не найден override, используется базовая конфигурация.
        /// </summary>
        public override List<AnimationStateTypeData> GetAnimationStateTypeDatas()
        {
            // Получаем базовые состояния из оригинального провайдера
            List<AnimationStateTypeData> baseStates = animationProviderSO.GetAnimationStateTypeDatas();
            List<AnimationStateTypeData> result = new List<AnimationStateTypeData>();

            foreach (var clipData in clipDatas)
            {
                if (clipData.animationStateSO == null)
                {
                    Debug.LogError("AnimationStateSO is null in clipDatas");
                    continue;
                }

                bool found = false;

                for (int i = 0; i < baseStates.Count; i++)
                {
                    AnimationStateTypeData baseState = baseStates[i];

                    if (baseState.StateName == clipData.animationStateSO.StateName)
                    {
                        found = true;
                        Dictionary<string, float> overrideBlendTimes =
                         Extensions.GetOverrideBlendTimes(clipData.overrideBlendTimeDatas) ?? baseState.OverrideBlendTimes;

                        var overriddenState = new AnimationStateTypeData(
                            clipData.animationStateSO.StateName,
                            clipData.clip,
                            baseState.BehaviorType,
                            clipData.baseBlendTime,
                            overrideBlendTimes
                        );

                        result.Add(overriddenState);
                        break;
                    }
                }

                if (!found)
                {
                    Debug.LogError($"AnimationStateSO with StateName '{clipData.animationStateSO.StateName}' not found in base animation provider states");
                }
            }

            foreach (var baseState in baseStates)
            {
                bool isOverridden = false;

                foreach (var clipData in clipDatas)
                {
                    if (clipData.animationStateSO != null &&
                        clipData.animationStateSO.StateName == baseState.StateName)
                    {
                        isOverridden = true;
                        break;
                    }
                }

                if (!isOverridden)
                {
                    result.Add(baseState);
                }
            }

            return result;
        }
    }
}