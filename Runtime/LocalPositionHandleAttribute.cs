using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class LocalPositionHandleAttribute : SceneGuiAttributeWithOptinalLabel
    {
        public override void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3))
            {
                Debug.LogError($"{nameof(LocalPositionHandleAttribute)} should only be used on {nameof(Vector3)} typed fields.");
                return;
            }
            
            var localPosition = (Vector3)fieldInfo.GetValue(monoBehaviour);
            var transform = monoBehaviour.transform;
            var position = transform.TransformPoint(localPosition);
            position = Handles.PositionHandle(position, Quaternion.identity);
            localPosition = transform.InverseTransformPoint(position);
            fieldInfo.SetValue(monoBehaviour, localPosition);
            
            DrawLabelIfEnabled(position, fieldInfo.Name);
        }
    }
}