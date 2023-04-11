using Godot;
using System;

// you must call IntegrateForces in the parent to make this work
public class RotateTowardsVelocity : Component<RigidBody2D, RotateTowardsVelocity._Props, None>{

    private float _lastRotation = 0;
    public class _Props{
        public float? RotateWhenReachesSpeed;
    }

    [Export]
    public float RotateWhenReachesSpeed{get; set;} = 0;
    protected override void _Init(_Props props)
    {
        RotateWhenReachesSpeed = props.RotateWhenReachesSpeed ?? RotateWhenReachesSpeed;

        
    }

    public void IntegrateForces(RigidBody2D body, Physics2DDirectBodyState state){
        var linealVelocity = state.LinearVelocity;

        if(linealVelocity.Length() > RotateWhenReachesSpeed){
            var direction = linealVelocity.Normalized();

            body.Rotation = direction.Angle();
            _lastRotation = body.Rotation;
        }else{
            body.Rotation = _lastRotation;
        }
    }

}
