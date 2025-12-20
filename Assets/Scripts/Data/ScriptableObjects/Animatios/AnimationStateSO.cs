using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    [CreateAssetMenu(fileName = "AnimationState", menuName = "ScriptableObjects/Animatios/AnimationState")]
    public class AnimationStateSO : ScriptableObject
    {
        [SerializeField] private string stateName;
        public string StateName => stateName;
    }
}