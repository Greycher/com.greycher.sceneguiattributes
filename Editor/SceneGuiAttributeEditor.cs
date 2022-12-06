using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    [InitializeOnLoad]
    public class SceneGuiAttributeEditor
    {
        static SceneGuiAttributeEditor()
        {
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;
        }

        private static void DuringSceneGUI(SceneView sceneview)
        {
            var go = Selection.activeGameObject;
            if (!go || !go.scene.isLoaded)
            {
                return;
            }
            
            var monoBehaviours = go.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                var fieldInfos = monoBehaviour.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fieldInfo in fieldInfos)
                {
                    foreach (var attribute in fieldInfo.GetCustomAttributes(typeof(SceneGuiAttribute)))
                    {
                        (attribute as SceneGuiAttribute).DuringSceneGui(monoBehaviour, fieldInfo);
                    }
                }
            }
        }
    }
}
