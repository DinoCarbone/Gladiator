using UnityEngine;

namespace Utils
{
    public class DrawSphereGizmo : MonoBehaviour
    {
        [SerializeField, Tooltip("Radius of the debug sphere gizmo.")]
        private float radius = 1f;

        [SerializeField, Tooltip("Color of the debug sphere gizmo.")]
        private Color color = Color.red;
        /// <summary>
        /// Рисует в сцене отладочную окружность заданного радиуса и цвета.
        /// </summary>
        void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}