using System;
using Godot;

public class AppllyInitialRotationTowardsMouse: Component<Node2D, None, None>
{
    protected override void _Init(None props)
    {
        var mousePosition = GetViewport().GetMousePosition();
        var direction = mousePosition - Parent.GlobalPosition;
        Parent.Rotation = direction.Angle();
    }
}