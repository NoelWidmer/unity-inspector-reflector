using UnityEditor;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class InspectorHook : Editor
    {
        private static object _lastTarget;


        public override void OnInspectorGUI()
        {
            object obj = target;

            if(obj == null)
            {
                _lastTarget = null;
                return;
            }

            if(_lastTarget == null)
            {
                _lastTarget = obj;
                // Set transient data to null.
            }
            else if(_lastTarget.Equals(obj) == false)
            {
                // Create new transient data.
            }

            InspectorDrawer drawer = new InspectorDrawer();

            if(drawer.ShouldReflectInspector(obj))
                drawer.ReflectInspector(obj);
            else
                DrawDefaultInspector();
        }
    }
}