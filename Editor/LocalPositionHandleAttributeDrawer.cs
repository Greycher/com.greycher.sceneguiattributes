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
            Handles.matrix = Matrix4x4.TRS(monoBehaviour.transform.position, Quaternion.identity, Vector3.one);
            base.InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute);
            Handles.matrix = oldMatrix;
        }
    }
}