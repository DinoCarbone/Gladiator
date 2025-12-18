using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    [CreateAssetMenu(fileName = "AnimationStateSO", menuName = "ScriptableObjects/Animatios/AnimationStateSO")]
    public class AnimationStateSO : ScriptableObject
    {
        [SerializeField] private string stateName;
        public string StateName => stateName;
    }
}