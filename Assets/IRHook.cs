using System;
using System.Collections.Generic;
using UnityEditor;

namespace InspectorReflector.Implementation
{
    /// <summary>
    ///     This class provides the editor integration that allows the IR to draw its own visuals.
    /// </summary>
    [CustomEditor(/* inspectedType: */ typeof(object), /* editorForChildClasses: */ true)]
    public class IRHook : Editor
    {
        private static IRDrawer _drawer = new IRDrawer();

        private static object _lastDrawnObject = null;
        private static Dictionary<string, bool> _folderPathToIsExpanded = null;

        public override void OnInspectorGUI()
        {
            if(target == null)
            {
                throw new InvalidOperationException($"{nameof(target)} is null.");
            }

            if(_drawer.ShouldDrawObjectOfType(target.GetType()))
            {
                // The currently inspected object should be drawn using IR.
                if(_lastDrawnObject == null)
                {
                    // This is the very first object we draw.
                    _lastDrawnObject = target;
                }
                else if(ReferenceEquals(_lastDrawnObject, target) == false)
                {
                    // We have switched to a new object.
                    _lastDrawnObject = target;
                    _folderPathToIsExpanded = null;
                }
                else
                {
                    // We are drawing the same object.
                }

                if(_folderPathToIsExpanded == null)
                {
                    // We do not know anything about the foldout structure of the object.
                    var foldoutData = new Dictionary<string, bool>();
                    _drawer.Draw(target, foldoutData);
                    _folderPathToIsExpanded = foldoutData;
                }
                else
                {
                    // We use the same foldout structure of the previous draw cycle.
                    _drawer.Draw(target, _folderPathToIsExpanded);
                }
            }
            else
            {
                // The currently inspected object should be drawn using the default unity inspector.
                DrawDefaultInspector();
            }
        }
    }
}