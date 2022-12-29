# Unity Scene GUI Attributes

A framework for Unity that enables scene GUI functionality over attributes. Scene GUI is drawn for the selected game object similar to the position handle on the Transform component.

![Radius Handle Preview](/Resources~/RadiusHandleAttributePreview.png)

Built-in Attributes
+ PositionHandle (UnityEngine.Vector3, IList<UnityEngine.Vector3>)
+ LocalPositionHandle (UnityEngine.Vector3, IList<UnityEngine.Vector3>)
+ PoseHandle (UnityEngine.Pose, IList<UnityEngine.Pose>)
+ LocalPoseHandle (UnityEngine.Pose, IList<UnityEngine.Pose>)
+ RadiusHandle (float)

Restrictions
+ Only works on fields
+ Only works when single game object selected
+ Only tested on Unity 2020.3 and 2021.3

## Installing

This framework is compatible with Unity's package manager. You can add it through the package manager by 'add package from git URL' option. For more information check [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

## Using

Use the desired attribute as any other attribute. Make sure to use the attributes on the proper types.

```
[RadiusHandle] private float radius = 5;
```

## Extending

Just like [Property Drawer](https://docs.unity3d.com/ScriptReference/PropertyDrawer.html), there needs to be an attribute class and a drawer class. 

The attribute class should be derived from [SceneGUIFieldAttribute](/Runtime/SceneGUIFieldAttribute.cs).

```
public class RadiusHandleAttribute : SceneGUIFieldAttribute { }
```

The drawer class should be derived from [GenericSceneGUIFieldAttributeDrawer](/Editor/GenericSceneGUIFieldAttributeDrawer.cs). Make sure that created drawer class is under an editor folder.

```
public class RadiusHandleFieldAttributeDrawer : GenericSceneGUIFieldAttributeDrawer<RadiusHandleAttribute>
{
    protected override bool ShouldDraw(FieldInfo fieldInfo)
    {
        if (fieldInfo.FieldType != typeof(float))
        {
            Debug.LogError($"{nameof(RadiusHandleAttribute)} should only be used on float typed fields!");
            return false;
        }

        return true;
    }

    protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, RadiusHandleAttribute attribute)
    {
        var radius = (float)fieldInfo.GetValue(monoBehaviour);
        var oldColor = Handles.color;
        Handles.color = attribute.GetColor();
        radius = Handles.RadiusHandle(Quaternion.identity, monoBehaviour.transform.position, radius);
        Handles.color = oldColor;
        fieldInfo.SetValue(monoBehaviour, radius);
    }
}
```

## Details

You can reach settings through Window>Scene GUI Attributes>Settings
