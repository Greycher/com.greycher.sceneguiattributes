using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGUIFieldAttributeWithOptinalLabelDrawer : SceneGUIFieldAttribute
    {
        public bool DrawLabel { get; set; } = true;
        public string Text { get; set; }
        public int FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.red;
        public Vector3 UnitSpacing { get; set; } = new Vector2(0, -0.03f);
    }
}