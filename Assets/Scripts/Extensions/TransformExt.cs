using UnityEngine;

namespace Cuvium.Core.Extensions
{
    public static class TransformExt
    {
        public static bool HasComponent<T>(this Transform transform)
        {
            var component = transform.GetComponent<T>();
            return component != null;
        }

        public static bool HasComponent<T>(this GameObject obj)
        {
            var component = obj.GetComponent<T>();
            return component != null;
        }
    }
}

