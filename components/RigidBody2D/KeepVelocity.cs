using Godot;
using System;

public class KeepVelocity : Node, IComponent<RigidBody2D>
{
    public RigidBody2D Parent { get; set; }
    [Export]
    public float Speed { get; set; } = 0.1f;
    [Export]
    public bool CanSurpassSpeed { get; set; } = false;
    public override void _Ready()
    {
        Parent = GetParent<RigidBody2D>();

        Parent.LinearDamp = 0;
    }

    public void KeepSpeed(RigidBody2D body, Physics2DDirectBodyState state){
        var velocity = state.LinearVelocity;

        if(velocity.Length() > Speed && !CanSurpassSpeed){
            velocity = velocity.Normalized() * Speed;
        }

        velocity = velocity.Normalized() * Speed;

        body.LinearVelocity = velocity;
    }


}
