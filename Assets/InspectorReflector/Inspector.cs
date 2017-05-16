using System;
using System.Collections.Generic;
using UnityEditor;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class Inspector : Editor
    {
        private static HashSet<string> _namespaces2Ignore = new HashSet<string>
        {
            "UnityEditor"
        };


        public override void OnInspectorGUI()
        {
            Type targetType = target.GetType();

            if(ShouldDrawDefaultInspector(target, targetType))
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawReflectedInspector(target, targetType);
            }
        }



        private static bool ShouldDrawDefaultInspector(object obj, Type type)
        {
            string ns = type.Namespace;
            return _namespaces2Ignore.Contains(type.Namespace);
        }



        private static void DrawReflectedInspector(object obj, Type type)
        {
        }
    }
}