using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class PositionHandleAttribute : SceneGuiAttribute
    {
        public bool DrawText { get; set; } = true;
        public string Text { get; set; }
        public int FontSize { get; set; } = 24;
        public Color TextColor { get; set; } = Color.red;
        public Vector3 UnitSpacing { get; set; } = new Vector2(0, -0.03f);

        public override void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Vector3))
            {
                Debug.LogError($"{GetType().Name} should only be used on {nameof(Vector3)} typed fields.");
                return;
            }
            
            var position = (Vector3)fieldInfo.GetValue(monoBehaviour);
            position = Handles.PositionHandle(position, Quaternion.identity);
            fieldInfo.SetValue(monoBehaviour, position);
            
            if (this.DrawText)
            {
                DrawText();
            }
            
            void DrawText()
            {
                var cameraTr = SceneView.currentDrawingSceneView.camera.transform;
                var d = (cameraTr.position - position).magnitude;
                var spacing = cameraTr.rotation * UnitSpacing * d;
                var textPos = position + spacing;
                
                if (String.IsNullOrEmpty(Text))
                {
                    Text = ObjectNames.NicifyVariableName(fieldInfo.Name);
                }
                
                var labelSkin = new GUIStyle(GUI.skin.label);
                labelSkin.fontSize = FontSize;
                labelSkin.normal.textColor = TextColor;
                labelSkin.alignment = TextAnchor.MiddleCenter;

                Handles.Label(textPos, Text, labelSkin);
            }
        }
    }
}