using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class GenericPositionHandleAttributeDrawer<T> : SceneGUIFieldAttributeWithOptinalLabelDrawer<T> where T : PositionHandleAttribute
    {
        protected override bool ShouldDraw(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3) && !typeof(IList<Vector3>).IsAssignableFrom(fieldInfo.FieldType))
            {
                Debug.LogError($"{nameof(PositionHandleAttribute)} should only be used on {nameof(Vector3)} typed fields.");
                return false;
            }
            
            return Tools.current == Tool.Move || Tools.current == Tool.Transform;
        }
        
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, T attribute)
        {
            if (fieldInfo.FieldType == typeof(Vector3))
            {
                var pos = (Vector3)fieldInfo.GetValue(monoBehaviour);
                pos = Handles.PositionHandle(pos, Quaternion.identity);
                fieldInfo.SetValue(monoBehaviour, pos);
                
                DrawLabelIfEnabled(pos, fieldInfo.Name, attribute);
            }
            else
            {
                var list = (IList<Vector3>)fieldInfo.GetValue(monoBehaviour);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = Handles.PositionHandle(list[i], Quaternion.identity);
                    DrawLabelIfEnabled(list[i], fieldInfo.Name, attribute, "", $" {i}");
                }
                fieldInfo.SetValue(monoBehaviour, list);
            }
        }
    }
}