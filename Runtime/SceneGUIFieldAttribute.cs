using System;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class SceneGUIFieldAttribute : PropertyAttribute
    {
        public bool ToggleWithGizmos { get; set; }

        protected SceneGUIFieldAttribute()
        {
            ToggleWithGizmos = Settings.Instance.DefaultToggleWithGizmos;
        }
    }
}