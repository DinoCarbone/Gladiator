using UnityEngine;

namespace Utils
{
    public class DrawSphereGizmo : MonoBehaviour
    {
        [SerializeField] private float radius = 1f;
        [SerializeField] private Color color = Color.red;

        void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}