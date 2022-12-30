using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class LocalPoseHandleAttributeDrawer : GenericPoseHandleAttributeDrawer<LocalPoseHandleAttribute>
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, LocalPoseHandleAttribute attribute)
        {
            var oldMatrix = Handles.matrix;
            Handles.matrix = monoBehaviour.transform.localToWorldMatrix;
            base.InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute);
            Handles.matrix = oldMatrix;
        }
    }
}