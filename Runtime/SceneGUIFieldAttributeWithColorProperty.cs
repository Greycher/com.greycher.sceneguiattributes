using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGUIFieldAttributeWithColorProperty : SceneGUIFieldAttribute
    {
        /// <summary>
        /// See <a href="https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html">ColorUtility.TryParseHtmlString</a>
        /// </summary>
        public string HTMLColorCode { get; set; }
        
        public Color GetColor()
        {
            if (ColorUtility.TryParseHtmlString(HTMLColorCode, out Color color))
            {
                return color;
            }

            return DefaultColor;
        }
        
        protected virtual Color DefaultColor => Color.white;
    }
}