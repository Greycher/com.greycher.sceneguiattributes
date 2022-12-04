using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class PoseHandleAttribute : SceneGuiAttributeWithOptinalLabel
    {
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var pose = GetPose(monoBehaviour, fieldInfo);

            if (Tools.current is Tool.Move or Tool.Transform)
            {
                var rot = Tools.pivotRotation == PivotRotation.Global ? Quaternion.identity : pose.rotation;
                pose.position = Handles.PositionHandle(pose.position, rot);
            }

            if (Tools.current is Tool.Rotate or Tool.Transform)
            {
                if (Tools.pivotRotation == PivotRotation.Global)
                {
                    pose.rotation *= Handles.RotationHandle(Quaternion.identity, pose.position);
                }
                else
                {
                    pose.rotation = Handles.RotationHandle(pose.rotation, pose.position);
                }
            }
            
            SetPose(monoBehaviour, fieldInfo, pose);
            
            DrawLabelIfEnabled(pose.position, fieldInfo.Name);
        }

        protected override bool SuitabilityCheck(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Pose))
            {
                Debug.LogError($"{nameof(PoseHandleAttribute)} should only be used on {nameof(Pose)} typed fields.");
                return false;
            }
            
            return Tools.current is Tool.Move or Tool.Rotate or Tool.Transform;
        }
        
        protected virtual Pose GetPose(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var pose = (Pose)fieldInfo.GetValue(monoBehaviour);
            pose.rotation.Normalize();
            return pose;
        }
        
        protected virtual void SetPose(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, Pose pose)
        {
            fieldInfo.SetValue(monoBehaviour, pose);
        }
    }
}