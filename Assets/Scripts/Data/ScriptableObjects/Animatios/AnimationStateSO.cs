using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    [CreateAssetMenu(fileName = "AnimationState", menuName = "ScriptableObjects/Animatios/AnimationState")]
    public class AnimationStateSO : ScriptableObject
    {
        [SerializeField, Tooltip("Animator state name.")]
        private string stateName;

        public string StateName => stateName;
    }
}