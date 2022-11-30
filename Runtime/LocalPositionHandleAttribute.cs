using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class LocalPositionHandleAttribute : PositionHandleAttribute
    {
        public override void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var oldMatrix = Handles.matrix;
            Handles.matrix = monoBehaviour.transform.localToWorldMatrix;
            {
                base.DuringSceneGui(monoBehaviour, fieldInfo);
            }
            Handles.matrix = oldMatrix;
        }
    }
}