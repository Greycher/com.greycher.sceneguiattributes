using System;
using System.Collections.Generic;
using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    [InitializeOnLoad]
    public static class SceneGUIAttributeRunner
    {
        private static readonly Dictionary<Type, SceneGUIFieldAttributeDrawer> _drawerMap = new Dictionary<Type, SceneGUIFieldAttributeDrawer>();
        
        static SceneGUIAttributeRunner()
        {
            AssemblyReloadEvents.afterAssemblyReload -= MapAttributeToDrawers;
            AssemblyReloadEvents.afterAssemblyReload += MapAttributeToDrawers;
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;
        }

        private static void MapAttributeToDrawers()
        {
            _drawerMap.Clear();
            var attributeCollection = TypeCache.GetTypesDerivedFrom<SceneGUIFieldAttribute>();
            foreach (var attribute in attributeCollection)
            {
                //If the attribute is abstract
                //It can not be used unless derived
                if (attribute.IsAbstract)
                {
                    continue;
                }
                
                var genericType = typeof(GenericSceneGUIFieldAttributeDrawer<>).MakeGenericType(attribute);
                var collection = TypeCache.GetTypesDerivedFrom(genericType);
                
                if (collection.Count == 0)
                {
                    Debug.LogWarning($"No {nameof(SceneGUIFieldAttributeDrawer)} exists for {attribute.Name}!");
                    continue;
                }
                
                if (collection.Count > 1)
                {
                    throw new Exception($"There are multiple {nameof(SceneGUIFieldAttributeDrawer)} for {attribute.Name}!");
                }
                
                _drawerMap.Add(attribute, Activator.CreateInstance(collection[0]) as SceneGUIFieldAttributeDrawer);
            }
        }

        private static void DuringSceneGUI(SceneView sceneView)
        {
            if (Selection.count > 1)
            {
                return;
            }
            
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
                    foreach (var attribute in fieldInfo.GetCustomAttributes(typeof(SceneGUIFieldAttribute)))
                    {
                        if (!(attribute is SceneGUIFieldAttribute sceneGuiAttribute))
                        {
                            continue;
                        }

                        if (sceneGuiAttribute.ToggleWithGizmos && !sceneView.drawGizmos)
                        {
                            continue;
                        }
                        
                        if (_drawerMap.TryGetValue(sceneGuiAttribute.GetType(), out SceneGUIFieldAttributeDrawer drawer))
                        {
                            drawer.DuringSceneGui(monoBehaviour, fieldInfo, sceneGuiAttribute);
                        }
                    }
                }
            }
        }
    }
}
