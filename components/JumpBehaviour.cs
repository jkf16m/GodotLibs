using Godot;
using System;

public class JumpBehaviour : Node
{
    [Export]
    public NodePath RigidBody2DPath {get; private set;}
    [Export]
    public NodePath JumpRecoveryAreaPath {get; private set;}
    [Export]
    public float JumpForce {get; private set;} = 1000f;
    [Export]
    public bool CanJump {get; private set;} = true;

    private RigidBody2D _rigidBody2D;
    private Area2D _jumpRecoveryArea;
    public override void _Ready()
    {
        _rigidBody2D = GetNode<RigidBody2D>(RigidBody2DPath);

        _rigidBody2D.ContactMonitor = true;

        _rigidBody2D.Connect("body_entered", this, nameof(OnBodyEntered));



        _jumpRecoveryArea = GetNode<Area2D>(JumpRecoveryAreaPath);
        _jumpRecoveryArea.Connect("body_entered", this, nameof(OnBodyEntered));
        _jumpRecoveryArea.Connect("body_exited", this, nameof(OnBodyExited));
    }

    public void OnBodyEntered(Node body){
        if(body.IsInGroup("ground")){
            CanJump = true;
        }
    }

    public void OnBodyExited(Node body){
        if(body.IsInGroup("ground")){
            CanJump = false;
        }
    }



    public void Jump(){
        if(!CanJump) return;

        _rigidBody2D.ApplyCentralImpulse(new Vector2(0, -JumpForce));

        CanJump = false;
    }
}
