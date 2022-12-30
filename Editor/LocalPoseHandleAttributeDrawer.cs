using SceneGUIAttributes.Runtime;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class LocalPoseHandleAttributeDrawer : GenericPoseHandleAttributeDrawer<LocalPoseHandleAttribute>
    {
        protected override Pose PoseHandle(Pose localPose, MonoBehaviour monoBehaviour)
        {
            var tr = monoBehaviour.transform;
            var pose = new Pose()
            {
                position = tr.TransformPoint(localPose.position),
                rotation = localPose.rotation * tr.rotation
            };
            pose = base.PoseHandle(pose, monoBehaviour);
            localPose.position = tr.InverseTransformPoint(pose.position);
            localPose.rotation = pose.rotation * Quaternion.Inverse(tr.rotation);
            return localPose;
        }
    }
}