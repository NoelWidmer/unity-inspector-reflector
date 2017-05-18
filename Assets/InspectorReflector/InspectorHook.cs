using UnityEditor;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class InspectorHook : Editor
    {
        public override void OnInspectorGUI()
        {
            object obj = target;

            if(obj == null)
                return;

            InspectorDrawer drawer = new InspectorDrawer();

            if(drawer.ShouldReflectInspector(obj))
            {
                drawer.ReflectInspector(obj);
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}