using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class InspectorHook : Editor
    {
        private static int? _ptr;
        private static TransientData _transientData;


        public override void OnInspectorGUI()
        {
            object obj = target;

            if(obj == null)
                return;

            if(_ptr == null)
            {
                _ptr = obj.GetHashCode();
                _transientData = new TransientData();
            }
            else if(_ptr != obj.GetHashCode())
            {
                _transientData = new TransientData();
            }

            InspectorDrawer drawer = new InspectorDrawer();

            if(drawer.ShouldReflectInspector(obj))
            {
                drawer.ReflectInspector(obj, _transientData);
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}