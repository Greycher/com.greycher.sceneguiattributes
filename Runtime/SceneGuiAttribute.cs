using System.Reflection;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGuiAttribute : PropertyAttribute
    {
        public abstract void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo);
    }
}