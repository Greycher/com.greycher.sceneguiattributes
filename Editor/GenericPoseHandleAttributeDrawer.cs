using System.Collections.Generic;
using System.Reflection;
using SceneGUIAttributes.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneGUIAttributes.Editor
{
    public class GenericPoseHandleAttributeDrawer<T> : SceneGUIFieldAttributeWithOptinalLabelDrawer<T> where T : PoseHandleAttribute
    {
        protected override bool ShouldDraw(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Pose) && !typeof(IList<Pose>).IsAssignableFrom(fieldInfo.FieldType))
            {
                Debug.LogError($"{GetType().Name} should be used on either {nameof(Pose)} or {nameof(IList<Pose>)} typed fields.");
                return false;
            }
            
            return Tools.current == Tool.Move || Tools.current == Tool.Rotate || Tools.current == Tool.Transform;
        }
        
        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, T attribute)
        {
            if (fieldInfo.FieldType == typeof(Pose))
            {
                var pose = (Pose)fieldInfo.GetValue(monoBehaviour);
                pose = PoseHandle(pose, monoBehaviour);
                fieldInfo.SetValue(monoBehaviour, pose);
                DrawLabelIfEnabled(pose.position, monoBehaviour, fieldInfo.Name, attribute);
            }
            else
            {
                var list = (IList<Pose>)fieldInfo.GetValue(monoBehaviour);
                for (int i = 0; i < list.Count; i++)
                {
                    var pose = list[i];
                    pose = PoseHandle(pose, monoBehaviour);
                    DrawLabelIfEnabled(pose.position, monoBehaviour, fieldInfo.Name, attribute, "", $" {i}");
                    list[i] = pose;
                }
                fieldInfo.SetValue(monoBehaviour, list);
            }
        }

        protected virtual Pose PoseHandle(Pose pose, MonoBehaviour monoBehaviour)
        {
            //ensure rotation correction
            pose.rotation = pose.rotation.normalized;
            
            if (Tools.current == Tool.Move || Tools.current == Tool.Transform)
            {
                var rot = Tools.pivotRotation == PivotRotation.Global ? Quaternion.identity : pose.rotation.normalized;
                pose.position = Handles.PositionHandle(pose.position, rot);
            }
        
            if (Tools.current == Tool.Rotate || Tools.current == Tool.Transform)
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

            return pose;
        }
    }
}