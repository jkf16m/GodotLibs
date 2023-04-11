using Godot;
using System;

public class ApplyInitialImpulseTowardsMouse : Component<RigidBody2D, ApplyInitialImpulseTowardsMouse._Props, None>
{
    public class _Props{
        public float? ImpulseForce {get; set;}
        public float? DegreesOffset {get; set;}
    }

    [Export]
    public float ImpulseForce {get; set;} = 0;
    [Export]
    public float DegreesOffset {get; set;} = 0;
    protected override void _Init(_Props props){
        ImpulseForce = props.ImpulseForce ?? ImpulseForce;
        DegreesOffset = props.DegreesOffset ?? DegreesOffset;

        _ApplyImpulse(Parent);
    }

    private void _ApplyImpulse(RigidBody2D body){
        var mousePosition = GetViewport().GetMousePosition();

        var direction = body.GlobalPosition.DirectionTo(mousePosition).Rotated(Mathf.Deg2Rad(DegreesOffset));

        body.ApplyCentralImpulse(direction * ImpulseForce);
    }


    
}
