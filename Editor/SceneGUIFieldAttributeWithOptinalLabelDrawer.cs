using System;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class SceneGUIFieldAttributeWithOptinalLabelDrawer<T> : GenericSceneGUIFieldAttributeDrawer<T> where T : SceneGUIFieldAttributeWithOptinalLabelDrawer
    {
        protected void DrawLabelIfEnabled(Vector3 position, string fiedlName, SceneGUIFieldAttributeWithOptinalLabelDrawer attribute)
        {
            if (!attribute.DrawLabel)
            {
                return;
            }
            
            var cameraTr = SceneView.currentDrawingSceneView.camera.transform;
            var d = (cameraTr.position - position).magnitude;
            var spacing = cameraTr.rotation * attribute.UnitSpacing * d;
            var textPos = position + spacing;
                
            if (String.IsNullOrEmpty(attribute.Text))
            {
                attribute.Text = ObjectNames.NicifyVariableName(fiedlName);
            }
                
            var labelSkin = new GUIStyle(GUI.skin.label);
            labelSkin.fontSize = attribute.FontSize;
            labelSkin.normal.textColor = attribute.GetColor();
            labelSkin.alignment = TextAnchor.MiddleCenter;

            Handles.Label(textPos, attribute.Text, labelSkin);
        }
    }
}