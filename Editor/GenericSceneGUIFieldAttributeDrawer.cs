using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class GenericSceneGUIFieldAttributeDrawer<T> : SceneGUIFieldAttributeDrawer where T : SceneGUIFieldAttribute
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, SceneGUIFieldAttribute attribute)
        {
            InternalDuringSceneGui(monoBehaviour, fieldInfo, attribute as T);
        }

        protected abstract void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, T attribute);
    }
}