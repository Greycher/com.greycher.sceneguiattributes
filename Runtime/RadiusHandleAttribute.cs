using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class RadiusHandleAttribute : SceneGUIFieldAttributeWithColorProperty
    {
        protected override Color DefaultColor => Color.yellow;

        public RadiusHandleAttribute()
        {
            ToggleWithGizmos = true;
        }
    }
}