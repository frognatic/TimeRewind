using UnityEngine;

namespace Utilities
{
    public static class TransformExtensions
    {
        public static void DestroyImmediateAllChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
