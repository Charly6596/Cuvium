using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Cuvium.Attributes;
using Cuvium.Core;

namespace Cuvium.EditorExtension
{
#if UNITY_EDITOR
    [CustomEditor(typeof(Object), true)]
    public class ButtonEditor : Editor
    {
        private MethodAttributeInfo<ButtonAttribute>[] _methods;
        void OnEnable()
        {
            _methods = GetMethodsWithButton();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            for(var i = 0; i < _methods.Length; i++)
            {
                var method = _methods[i];
                var mInfo = method.MethodInfo;
                var aInfo = method.AttributeInfo;
                var label = aInfo.IsLabelDefined ? aInfo.Label : mInfo.Name;
                if(GUILayout.Button(label))
                {
                    method.MethodInfo.Invoke(target, null);
                }
            }
        }

        private MethodAttributeInfo<ButtonAttribute>[] GetMethodsWithButton()
        {
            return target
                .GetType()
                .GetMethodsWithAttribute<ButtonAttribute>();
        }
    }
    #endif
}

