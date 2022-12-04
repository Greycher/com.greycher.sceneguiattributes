using System.Reflection;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGuiAttribute : PropertyAttribute
    {
        public void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            if (SuitabilityCheck(fieldInfo))
            {
                InternalDuringSceneGui(monoBehaviour, fieldInfo);
            }
        }

        protected abstract void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo);
        protected abstract bool SuitabilityCheck(FieldInfo fieldInfo);
    }
}