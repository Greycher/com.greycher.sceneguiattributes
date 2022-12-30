using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class LocalPositionHandleAttributeDrawer : GenericPositionHandleAttributeDrawer<LocalPositionHandleAttribute>
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, LocalPositionHandleAttribute attribute)
        {
            var oldMatrix = Handles.matrix;
            Handles.matrix = monoBehaviour.transform.localToWorldMatrix;
            base.InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute);
            Handles.matrix = oldMatrix;
        }
    }
}