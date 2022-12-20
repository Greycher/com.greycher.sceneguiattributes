using System;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SceneGUIFieldAttribute : PropertyAttribute
    {
        public bool ToggleWithGizmos { get; set; }
    }
}