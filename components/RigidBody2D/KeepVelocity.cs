using Godot;
using System;

public class KeepVelocity : Component<RigidBody2D, KeepVelocity._Props, None>
{
    public class _Props{
        public float? Speed {get; set;} = 0.1f;
        public bool? CanSurpassSpeed {get; set;} = false;
    }


    [Export]
    public float Speed { get; set; } = 0.1f;
    [Export]
    public bool CanSurpassSpeed { get; set; } = false;

    protected override void _Init(_Props props)
    {
        Speed = props.Speed ?? Speed;
        CanSurpassSpeed = props.CanSurpassSpeed ?? CanSurpassSpeed;
    }
    public override void _Ready()
    {
        Parent = GetParent<RigidBody2D>();

        Parent.LinearDamp = 0;
    }

    public void IntegrateForces(RigidBody2D body, Physics2DDirectBodyState state){
        var velocity = state.LinearVelocity;

        if(velocity.Length() > Speed && !CanSurpassSpeed){
            velocity = velocity.Normalized() * Speed;
        }

        velocity = velocity.Normalized() * Speed;

        body.LinearVelocity = velocity;
    }


}
