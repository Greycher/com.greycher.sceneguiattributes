using System.Reflection;
using UnityEngine;

namespace SceneGUIAttributes.Runtime
{
    public class LocalPoseHandle : PoseHandleAttribute
    {
        protected override Pose GetPose(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var localPose = (Pose)fieldInfo.GetValue(monoBehaviour);
            localPose.rotation.Normalize();
            var tr = monoBehaviour.transform;
            return new Pose
            {
                position = tr.TransformPoint(localPose.position),
                rotation = localPose.rotation * tr.rotation
            };
        }

        protected override void SetPose(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, Pose pose)
        {
            var tr = monoBehaviour.transform;
            fieldInfo.SetValue(monoBehaviour, new Pose
            {
                position = tr.InverseTransformPoint(pose.position),
                rotation = pose.rotation * Quaternion.Inverse(tr.rotation)
            });
        }
    }
}