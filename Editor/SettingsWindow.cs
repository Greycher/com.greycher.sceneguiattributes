using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private const string _defaultToggleWithGizmosKey = "default_toggle_with_gizmos";
        private const string _defaultToggleWithGizmosName = "Default Toggle With Gizmos";
        private const string _tooltip = "Should " + nameof(SceneGUIAttributes) + " visibility change with gizmo toggle on the scene view.";

        private static bool DefaultToggleWithGizmos
        {
            get => EditorPrefs.GetBool(_defaultToggleWithGizmosKey, false);
            set
            {
                EditorPrefs.SetBool(_defaultToggleWithGizmosKey, value);
                Settings.DefaultToggleWithGizmos = value;
            }
        }
        
        void OnGUI()
        {
            var content = new GUIContent(_defaultToggleWithGizmosName, _tooltip);
            DefaultToggleWithGizmos = EditorGUILayout.Toggle(content, DefaultToggleWithGizmos);
        }
        
        [InitializeOnLoad]
        public static class SettingsInitializer
        { 
            static SettingsInitializer()
            {
                Settings.DefaultToggleWithGizmos = DefaultToggleWithGizmos;
            }
        }
    }
}