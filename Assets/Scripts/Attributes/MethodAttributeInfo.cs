using System.Reflection;
using UnityEngine;

namespace Cuvium.Attributes
{
    public class MethodAttributeInfo<T> where T : PropertyAttribute
    {
        public MethodInfo MethodInfo { get; set; }
        public T AttributeInfo { get; set; }

        public MethodAttributeInfo(MethodInfo mInfo, T aInfo)
        {
            MethodInfo = mInfo;
            AttributeInfo = aInfo;
        }
    }
}

