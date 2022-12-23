using SceneGUIAttributes.Runtime;
using UnityEditor;

namespace SceneGUIAttributes.Editor
{
    public class MenuItems
    {
        private const string MenuParent = "Window/Scene GUI Attributes/";
        
        [MenuItem(MenuParent + "Settings")]
        public static void SelectSetting()
        {
            var settings = Settings.Instance;
            Selection.SetActiveObjectWithContext(settings, settings);
        }
    }
}