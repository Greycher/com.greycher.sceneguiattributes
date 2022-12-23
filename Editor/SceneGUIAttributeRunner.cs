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
        private static readonly Dictionary<Type, List<FieldInfo>> _fieldCache = new Dictionary<Type, List<FieldInfo>>();
        
        static SceneGUIAttributeRunner()
        {
            AssemblyReloadEvents.afterAssemblyReload -= AfterAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += AfterAssemblyReload;
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;
        }

        private static void AfterAssemblyReload()
        {
            MapAttributeToDrawers();
            CacheFields();
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
        
        private static void CacheFields()
        {
            _fieldCache.Clear();
            var collection = TypeCache.GetFieldsWithAttribute<SceneGUIFieldAttribute>();
            foreach (var info in collection)
            {
                if (!_fieldCache.TryGetValue(info.DeclaringType, out List<FieldInfo> list))
                {
                    list = new List<FieldInfo>();
                    _fieldCache.Add(info.DeclaringType, list);
                }
                
                list.Add(info);
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
                if (!_fieldCache.TryGetValue(monoBehaviour.GetType(), out List<FieldInfo> list))
                {
                    continue;
                }

                foreach (var fieldInfo in list)
                {
                    var attributes = fieldInfo.GetCustomAttributes<SceneGUIFieldAttribute>();
                    foreach (var attribute in attributes)
                    {
                        if (_drawerMap.TryGetValue(attribute.GetType(), out SceneGUIFieldAttributeDrawer drawer))
                        {
                            drawer.DuringSceneGui(monoBehaviour, fieldInfo, attribute);
                        }
                    }
                }
            }
        }
        
        [MenuItem("Temp/ListFileds")]
        public static void ListFileds()
        {
            foreach (var fieldInfo in TypeCache.GetFieldsWithAttribute<SceneGUIFieldAttribute>())
            {
                Debug.Log(fieldInfo.DeclaringType);
            }
        }
    }
}
