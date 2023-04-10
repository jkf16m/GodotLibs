using Godot;
using System;

public class ApplyInitialImpulseRigidBody2D : Node, IComponent<RigidBody2D>
{

    public RigidBody2D Parent { get; set; }

    [Export]
    public Vector2 Impulse { get; set; }
    [Export]
    public float Torque { get; set; }
    public override void _Ready()
    {
        Parent = GetParent<RigidBody2D>();

        Parent.ApplyCentralImpulse(Impulse);
        Parent.ApplyTorqueImpulse(Torque);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
