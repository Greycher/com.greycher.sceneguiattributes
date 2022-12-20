using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class LocalPositionHandleAttributeDrawer : GenericPositionHandleAttributeDrawer<LocalPositionHandleAttribute>
    {
        protected override Vector3 GetPosition(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            return monoBehaviour.transform.TransformPoint((Vector3)fieldInfo.GetValue(monoBehaviour));
        }
        
        protected override void SetPosition(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, Vector3 position)
        { 
            fieldInfo.SetValue(monoBehaviour, monoBehaviour.transform.InverseTransformPoint(position));
        }
    }
}