using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class PositionHandleAttribute : SceneGuiAttributeWithOptinalLabel
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var position = GetPosition(monoBehaviour, fieldInfo);
            position = Handles.PositionHandle(position, Quaternion.identity);
            SetPosition(monoBehaviour, fieldInfo, position);
            
            DrawLabelIfEnabled(position, fieldInfo.Name);
        }

        protected override bool SuitabilityCheck(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3))
            {
                Debug.LogError($"{nameof(PositionHandleAttribute)} should only be used on {nameof(Vector3)} typed fields.");
                return false;
            }
            
            return Tools.current is Tool.Move or Tool.Transform;
        }

        protected virtual Vector3 GetPosition(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            return (Vector3)fieldInfo.GetValue(monoBehaviour);
        }
        
        protected virtual void SetPosition(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, Vector3 position)
        {
            fieldInfo.SetValue(monoBehaviour, position);
        }
    }
}