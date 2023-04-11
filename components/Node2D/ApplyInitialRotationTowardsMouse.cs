using System;
using Godot;

public class ApplyInitialRotationTowardsMouse: Component<Node2D, ApplyInitialRotationTowardsMouse._Props, None>
{
    public class _Props{
        public float? DegreesOffset { get; set; }
    }

    [Export]
    public float DegreesOffset { get; set; } = 0;
    protected override void _Init(_Props props)
    {
        DegreesOffset = props.DegreesOffset ?? DegreesOffset;

        var mousePosition = GetViewport().GetMousePosition();

        var direction = Parent.GlobalPosition.DirectionTo(mousePosition);

        Parent.Rotation = direction.Angle() + Mathf.Deg2Rad(DegreesOffset);
    }
}