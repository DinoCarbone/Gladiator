using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    [CreateAssetMenu(fileName = "AnimationEvent", menuName = "ScriptableObjects/Animatios/AnimationEvent")]
    public class AnimationEventSO : ScriptableObject
    {
        [SerializeField] private string eventName;
        public string EventName => eventName;
    }
}