using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public static class MenuShortcuts
    {
        private const string _menuParent = "Window/Scene GUI Attributes/";
        

        [MenuItem(_menuParent + "Settings")]
        static void Init()
        {
            SettingsWindow window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow), false, ObjectNames.NicifyVariableName(nameof(SettingsWindow)));
            window.Show();
            window.minSize = new Vector2(300, 150);
        }
    }
}