using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class PoseHandleAttribute : SceneGuiAttributeWithOptinalLabel
    {
        public override void DuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Pose))
            {
                Debug.LogError($"{nameof(PoseHandleAttribute)} should only be used on {nameof(Pose)} typed fields.");
                return;
            }
            
            var pose = (Pose)fieldInfo.GetValue(monoBehaviour);
            pose.rotation.Normalize();
            if (Tools.pivotRotation == PivotRotation.Global)
            {
                pose.position = Handles.PositionHandle(pose.position, Quaternion.identity);
                pose.rotation *= Handles.RotationHandle(Quaternion.identity, pose.position);
            }
            else
            {
                pose.position = Handles.PositionHandle(pose.position, pose.rotation);
                pose.rotation = Handles.RotationHandle(pose.rotation, pose.position);
            }
            fieldInfo.SetValue(monoBehaviour, pose);
            
            Debug.Log(Tools.pivotRotation);
            
            DrawLabelIfEnabled(pose.position, fieldInfo.Name);
        }
    }
}