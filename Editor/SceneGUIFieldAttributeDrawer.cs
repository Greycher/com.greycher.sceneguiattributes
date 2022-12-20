using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class SceneGUIFieldAttributeDrawer
    {
        public void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, SceneGUIFieldAttribute attribute)
        {
            if (ShouldDraw(fieldInfo))
            {
                Undo.RecordObject(monoBehaviour, attribute.GetType().Name);
                InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute);
            }
        }

        protected abstract void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, SceneGUIFieldAttribute attribute);
        protected abstract bool ShouldDraw(FieldInfo fieldInfo);
    }
}