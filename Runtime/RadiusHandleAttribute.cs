using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class RadiusHandleAttribute : SceneGUIFieldAttribute
    {
        /// <summary>
        /// See <see href = "ColorUtility.TryParseHtmlString" />.
        /// </summary>
        public string HTMLColorCode { get; set; }
        
        public Color Color { get; private set; }
        
        public RadiusHandleAttribute()
        {
            ToggleWithGizmos = true;
            if (ColorUtility.TryParseHtmlString(HTMLColorCode, out Color color))
            {
                Color = color;
            }
            else
            {
                Color = Color.red; 
            }
        }
    }
}