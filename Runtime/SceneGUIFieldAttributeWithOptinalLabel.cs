using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGUIFieldAttributeWithOptinalLabelDrawer : SceneGUIFieldAttributeWithColorProperty
    {
        public bool DrawLabel { get; set; } = true;
        public string Text { get; set; }
        public int FontSize { get; set; } = 24;
        public float UnitSpacingX { get; set; } = 0;
        public float UnitSpacingY { get; set; } = -0.03f;
        
        protected override Color DefaultColor => Color.green;
        public Vector2 UnitSpacing => new Vector2(UnitSpacingX, UnitSpacingY);
    }
}