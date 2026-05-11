using UnityEngine;

namespace Tests.EditMode
{
    public static class Create
    {
        public static Transform Transform(Vector3 position, Quaternion rotation)
        {
            var go = new GameObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go.transform;
        }
    }
}