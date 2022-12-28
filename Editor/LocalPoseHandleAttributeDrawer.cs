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
            var tr = monoBehaviour.transform;
            Handles.matrix = Matrix4x4.TRS(tr.position, tr.rotation, Vector3.one);
            base.InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute);
            Handles.matrix = oldMatrix;
        }
    }
}