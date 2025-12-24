using UnityEngine;

namespace Data.ScriptableObjects.Animatios
{
    /// <summary>
    /// ScriptableObject, представляющий событие анимации (идентификатор события).
    /// </summary>
    [CreateAssetMenu(fileName = "AnimationEvent", menuName = "ScriptableObjects/Animatios/AnimationEvent")]
    public class AnimationEventSO : ScriptableObject
    {
        [SerializeField, Tooltip("Animation event identifier.")]
        private string eventName;

        /// <summary>Имя/идентификатор анимационного события.</summary>
        public string EventName => eventName;
    }
}