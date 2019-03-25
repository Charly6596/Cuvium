using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace Cuvium.Attributes
{
    public static class MethodInfoExtensions
    {
        public static MethodAttributeInfo<T>[] GetMethodsWithAttribute<T>(this Type type) where T : PropertyAttribute
        {
            return type
                .GetMethods()
                .Where(m => m.GetCustomAttribute<T>() != null)
                .Select(m => new MethodAttributeInfo<T>(m, m.GetCustomAttribute<T>()))
                .ToArray();
        }
    }
}

