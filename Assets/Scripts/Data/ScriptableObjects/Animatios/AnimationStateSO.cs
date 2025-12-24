using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    /// <summary>
    /// ScriptableObject, представляющий имя состояния анимации (Animator state name).
    /// </summary>
    [CreateAssetMenu(fileName = "AnimationState", menuName = "ScriptableObjects/Animatios/AnimationState")]
    public class AnimationStateSO : ScriptableObject
    {
        [SerializeField, Tooltip("Animator state name.")]
        private string stateName;

        /// <summary>Имя состояния в Animator.</summary>
        public string StateName => stateName;
    }
}