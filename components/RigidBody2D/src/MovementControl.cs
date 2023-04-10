using System;
using Godot;

public class MovementControl : Component<RigidBody2D, MovementControl._Props, None>{
    public class _Props{
        public string UpAction;
        public string DownAction;
        public string LeftAction;
        public string RightAction;
        public string JumpAction;
        public float? MaxSpeed;
        public float? Speed;
        public float? JumpForce;
        public float? Desacceleration;
    }

    [Export]
    public string UpAction{get; set;} = "ui_up";
    [Export]
    public string DownAction{get; set;} = "ui_down";
    [Export]
    public string LeftAction{get; set;} = "ui_left";
    [Export]
    public string RightAction{get; set;} = "ui_right";
    [Export]
    public string JumpAction{get; set;} = "ui_accept";
    [Export]
    public float MaxSpeed{get; set;} = 100;
    [Export]
    public float Speed{get; set;} = 100;
    [Export]
    public float JumpForce{get; set;} = 100;
    [Export]
    public float Desacceleration{get; set;} = 0.5f;

    protected override void _Init(MovementControl._Props props){
        UpAction = props.UpAction ?? UpAction;
        DownAction = props.DownAction ?? DownAction;
        LeftAction = props.LeftAction ?? LeftAction;
        RightAction = props.RightAction ?? RightAction;
        JumpAction = props.JumpAction ?? JumpAction;
        Speed = props.Speed ?? Speed;
        MaxSpeed = props.MaxSpeed ?? MaxSpeed;
        JumpForce = props.JumpForce ?? JumpForce;
        Desacceleration = props.Desacceleration ?? Desacceleration;
    }




    // should be called in _IntegrateForces
    public void IntegrateForces(RigidBody2D body, Physics2DDirectBodyState state){
        _ApplyMovement(body, state);
        _ApplyJump(body, state);
        _ApplyDesacceleration(body, state);
    }









    private void _ApplyMovement(RigidBody2D body, Physics2DDirectBodyState state){
        var velocity = state.LinearVelocity;
        var direction = new Vector2();
        if(Input.IsActionPressed(LeftAction)){
            direction.x -= 1;
        }
        if(Input.IsActionPressed(RightAction)){
            direction.x += 1;
        }
        if(Input.IsActionPressed(UpAction)){
            direction.y -= 1;
        }
        if(Input.IsActionPressed(DownAction)){
            direction.y += 1;
        }
        direction = direction.Normalized();
        velocity.x = direction.x * Speed;
        velocity.y = direction.y * Speed;
        body.LinearVelocity = velocity;

        _LimitSpeed(body,state);
    }

    private void _LimitSpeed(RigidBody2D body, Physics2DDirectBodyState state){
        var velocity = state.LinearVelocity;
        if(velocity.Length() > MaxSpeed){
            velocity = velocity.Normalized() * MaxSpeed;
        }
        body.LinearVelocity = velocity;
    }





    private void _ApplyJump(RigidBody2D body, Physics2DDirectBodyState state){
        if(Input.IsActionJustPressed(JumpAction)){
            var velocity = state.LinearVelocity;
            velocity.y = -JumpForce;
            body.LinearVelocity = velocity;
        }
    }





    private void _ApplyDesacceleration(RigidBody2D body, Physics2DDirectBodyState state){
        var velocity = state.LinearVelocity;
        velocity.x *= Desacceleration;
        velocity.y *= Desacceleration;
        body.LinearVelocity = velocity;
    }
}