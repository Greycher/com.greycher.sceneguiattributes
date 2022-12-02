using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public abstract class SceneGuiAttributeWithOptinalLabel : SceneGuiAttribute
    {
        public bool DrawLabel { get; set; } = true;
        public string Text { get; set; }
        public int FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.red;
        public Vector3 UnitSpacing { get; set; } = new Vector2(0, -0.03f);
        
        protected void DrawLabelIfEnabled(Vector3 position, string fiedlName)
        {
            if (!DrawLabel)
            {
                return;
            }
            
            var cameraTr = SceneView.currentDrawingSceneView.camera.transform;
            var d = (cameraTr.position - position).magnitude;
            var spacing = cameraTr.rotation * UnitSpacing * d;
            var textPos = position + spacing;
                
            if (String.IsNullOrEmpty(Text))
            {
                Text = ObjectNames.NicifyVariableName(fiedlName);
            }
                
            var labelSkin = new GUIStyle(GUI.skin.label);
            labelSkin.fontSize = FontSize;
            labelSkin.normal.textColor = TextColor;
            labelSkin.alignment = TextAnchor.MiddleCenter;

            Handles.Label(textPos, Text, labelSkin);
        }
    }
}