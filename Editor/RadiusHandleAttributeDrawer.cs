using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class RadiusHandleFieldAttributeDrawer : GenericSceneGUIFieldAttributeDrawer<RadiusHandleAttribute>
    {
        protected override bool ShouldDraw(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(float))
            {
                Debug.LogError($"{nameof(RadiusHandleAttribute)} should only be used on float typed fields!");
                return false;
            }

            return true;
        }

        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, RadiusHandleAttribute attribute)
        {
            var radius = (float)fieldInfo.GetValue(monoBehaviour);
            var oldColor = Handles.color;
            Handles.color = attribute.Color;
            radius = Handles.RadiusHandle(Quaternion.identity, monoBehaviour.transform.position, radius);
            Handles.color = oldColor;
            fieldInfo.SetValue(monoBehaviour, radius);
        }
    }
}