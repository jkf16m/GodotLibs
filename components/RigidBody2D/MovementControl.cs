using Godot;
using System;

/**
In this class we define what to do when the player presses a key.
**/
public class MovementControl : Node, IComponent<RigidBody2D>{
    public RigidBody2D Parent{get; private set;}

    [Export]
    public float MaxSpeed = 100;
    [Export]
    public float Speed = 100;

    [Export]
    public bool CanJump = false;
    [Export]
    public Vector2 JumpForce = new Vector2(0, -100);

    [Export]
    public bool CanMove = true;
    [Export]
    public float Desacceleration = 100;
    [Export]
    public bool FixedXAxis = false;
    [Export]
    public bool FixedYAxis = false;



    [Export]
    public string UpAction = "ui_up";
    [Export]
    public string DownAction = "ui_down";
    [Export]
    public string LeftAction = "ui_left";
    [Export]
    public string RightAction = "ui_right";
    [Export]
    public string JumpAction = "ui_accept";

    public float FixedXAxisValue = 0;
    public float FixedYAxisValue = 0;
    public override void _Ready(){
        Parent = GetParentOrNull<RigidBody2D>();

        if(Parent == null){
            throw new Exception("Parent is not a RigidBody2D");
        }

        if(FixedXAxis){
            FixedXAxisValue = Parent.Position.x;
        }
        if(FixedYAxis){
            FixedYAxisValue = Parent.Position.y;
        }
        
    }


    /**
    <summary>
    This method should be called inside the _IntegrateForces method of the parent RigidBody2D.
    </summary>
    **/
    public void MoveRigidBody(RigidBody2D body, Physics2DDirectBodyState state){
        if(!CanMove){
            return;
        }       

        Vector2 direction = new Vector2(0,0);
        Vector2 desacceleration = new Vector2(0,0);
        if(Input.IsActionPressed(RightAction)){
            direction.x += 1;
        }
        if(Input.IsActionPressed(LeftAction)){
            direction.x -= 1;
        }
        if(Input.IsActionPressed(UpAction)){
            direction.y -= 1;
        }
        if(Input.IsActionPressed(DownAction)){
            direction.y += 1;
        }
        if(
            !Input.IsActionPressed(RightAction) &&
            !Input.IsActionPressed(LeftAction) &&
            !Input.IsActionPressed(UpAction) &&
            !Input.IsActionPressed(DownAction)
        ){
            desacceleration = -state.LinearVelocity.Normalized() * Desacceleration;
        }

        if(desacceleration.Length() != 0){
            body.AppliedForce = desacceleration;
        }else{
            body.AppliedForce = (direction.Normalized() * Speed);
        }


        if(CanJump && Input.IsActionJustPressed(JumpAction)){
            state.ApplyCentralImpulse(JumpForce);
        }

        if(state.LinearVelocity.Length() > MaxSpeed){
            state.LinearVelocity = state.LinearVelocity.Normalized() * MaxSpeed;
        }

        if(FixedXAxis){
            body.Position = new Vector2(FixedXAxisValue, body.Position.y);
        }

        if(FixedYAxis){
            body.Position = new Vector2(body.Position.x, FixedYAxisValue);
        }

    }
}