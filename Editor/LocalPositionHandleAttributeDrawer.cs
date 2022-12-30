using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class LocalPositionHandleAttributeDrawer : GenericPositionHandleAttributeDrawer<LocalPositionHandleAttribute>
    {
        protected override Vector3 PositionHandle(Vector3 localPos, MonoBehaviour monoBehaviour)
        {
            var tr = monoBehaviour.transform;
            var pos = tr.TransformPoint(localPos);
            return tr.InverseTransformPoint(base.PositionHandle(pos, monoBehaviour));
        }

        protected override void DrawLabelIfEnabled(Vector3 localPos, MonoBehaviour monoBehaviour, string fieldName,
            SceneGUIFieldAttributeWithOptinalLabelDrawer attribute, string prefix = "", string postfix = "")
        {
            var pos = monoBehaviour.transform.TransformPoint(localPos);
            base.DrawLabelIfEnabled(pos, monoBehaviour, fieldName, attribute, prefix, postfix);
        }
    }
}