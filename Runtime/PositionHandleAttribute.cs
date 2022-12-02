using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class PositionHandleAttribute : SceneGuiAttributeWithOptinalLabel
    {
        public override void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3))
            {
                Debug.LogError($"{nameof(PositionHandleAttribute)} should only be used on {nameof(Vector3)} typed fields.");
                return;
            }
            
            var position = (Vector3)fieldInfo.GetValue(monoBehaviour);
            position = Handles.PositionHandle(position, Quaternion.identity);
            fieldInfo.SetValue(monoBehaviour, position);
            
            DrawLabelIfEnabled(position, fieldInfo.Name);
        }
    }
}