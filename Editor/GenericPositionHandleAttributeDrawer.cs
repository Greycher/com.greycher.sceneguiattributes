using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class GenericPositionHandleAttributeDrawer<T> : SceneGUIFieldAttributeWithOptinalLabelDrawer<T> where T : PositionHandleAttribute
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, T attribute)
        {
            var position = GetPosition(monoBehaviour, fieldInfo);
            position = Handles.PositionHandle(position, Quaternion.identity);
            SetPosition(monoBehaviour, fieldInfo, position);
            DrawLabelIfEnabled(position, fieldInfo.Name, attribute);
        }

        protected override bool ShouldDraw(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3))
            {
                Debug.LogError($"{nameof(PositionHandleAttribute)} should only be used on {nameof(Vector3)} typed fields.");
                return false;
            }
            
            return Tools.current == Tool.Move || Tools.current == Tool.Transform;
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