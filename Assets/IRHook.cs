using System.Collections.Generic;
using UnityEditor;

namespace InspectorReflector.Implementation
{
    /// <summary>
    ///     This class provides the editor integration that allows the IR to draw its own visuals.
    /// </summary>
    [CustomEditor(typeof(object), true)]
    public class IRHook : Editor
    {
        private static object _lastTarget;
        private static Dictionary<string, bool> _foldoutData;



        public override void OnInspectorGUI()
        {
            IRDrawer drawer = new IRDrawer();

            if(drawer.SupportsType(target.GetType()))
            {
                if(target == null)
                {
                    _lastTarget = null;
                    _foldoutData = null;
                }
                else
                {
                    if(_lastTarget == null)
                    {
                        _lastTarget = target;
                    }
                    else if(_lastTarget.Equals(target) == false)
                    {
                        _lastTarget = target;
                        _foldoutData = null;
                    }
                    
                    if(_foldoutData == null)
                        _foldoutData = drawer.DrawInspector(target);
                    else
                        drawer.DrawInspector(target, _foldoutData);
                }
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}