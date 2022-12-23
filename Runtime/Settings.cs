using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class Settings : ScriptableObject
    {
        [Tooltip("Should " + nameof(SceneGUIAttributes) + " visibility change with gizmo toggle on the scene view.")] 
        public bool DefaultToggleWithGizmos;
        [Tooltip("Should draw " + nameof(SceneGUIAttributes) + " for properties." +
                 " This may slowdown the editor depending on the monobehaviour count on single game object" +
                 " and the property count in it.")] 
        public bool IncludeProperties = true;

        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<Settings>(nameof(Settings));
                }

                return _instance;
            }
        }
    }
}