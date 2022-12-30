using System;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public abstract class SceneGUIFieldAttributeWithOptinalLabelDrawer<T> : GenericSceneGUIFieldAttributeDrawer<T> where T : SceneGUIFieldAttributeWithOptinalLabelDrawer
    {
        protected virtual void DrawLabelIfEnabled(Vector3 position, MonoBehaviour monoBehaviour, string fieldName, 
            SceneGUIFieldAttributeWithOptinalLabelDrawer attribute, string prefix = "", string postfix = "")
        {
            if (!attribute.DrawLabel)
            {
                return;
            }
            
            var cameraTr = SceneView.currentDrawingSceneView.camera.transform;
            var d = (cameraTr.position - position).magnitude;
            var spacing = cameraTr.rotation * attribute.UnitSpacing * d;
            var textPos = position + spacing;

            var text = String.IsNullOrEmpty(attribute.Text)
                ? ObjectNames.NicifyVariableName(fieldName)
                : attribute.Text;
            var content = new GUIContent($"{prefix}{text}{postfix}");

            var labelSkin = new GUIStyle(GUI.skin.label);
            labelSkin.fontSize = attribute.FontSize;
            labelSkin.normal.textColor = attribute.GetColor();
            labelSkin.alignment = TextAnchor.MiddleCenter;

            Handles.Label(textPos, content, labelSkin);
        }
    }
}