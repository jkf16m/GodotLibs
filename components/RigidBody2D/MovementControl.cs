using System;
using Godot;

public class MovementControl : Component<RigidBody2D, MovementControl._Props, None>{
    public class _Props{
        public string UpAction;
        public string DownAction;
        public string LeftAction;
        public string RightAction;
        public string JumpAction;
        public bool? CanJump;
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
    public bool CanJump{get; set;} = true;
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
        CanJump = props.CanJump ?? CanJump;
    }




    // should be called in _IntegrateForces
    public void IntegrateForces(RigidBody2D body, Physics2DDirectBodyState state){
        if(CanJump)
            _ApplyJump(body, state);
        _ApplyMovement(body, state);
        _ApplyDeceleration(body, state);
        _ApplyLimitSpeed(body, state);
        //_ApplyLimitSpeed(body, state);
    }









    private void _ApplyMovement(RigidBody2D body, Physics2DDirectBodyState state)
    {

        var direction = Vector2.Zero;
        var desacceleration = Vector2.Zero;
        if (Input.IsActionPressed(LeftAction))
        {
            direction.x -= 1;
        }
        if (Input.IsActionPressed(RightAction))
        {
            direction.x += 1;
        }
        if (Input.IsActionPressed(UpAction))
        {
            direction.y -= 1;
        }
        if (Input.IsActionPressed(DownAction))
        {
            direction.y += 1;
        }
        body.AppliedForce = (direction.Normalized() * Speed);
        //desacceleration = _Decelerate(state, desacceleration);

    }

    private Vector2 _Decelerate(Physics2DDirectBodyState state, Vector2 desacceleration)
    {
        if (
                    !Input.IsActionPressed(LeftAction) &&
                    !Input.IsActionPressed(RightAction) &&
                    !Input.IsActionPressed(UpAction) &&
                    !Input.IsActionPressed(DownAction)
                )
        {
            desacceleration = -state.LinearVelocity.Normalized() * Desacceleration;
        }

        return desacceleration;
    }

    private void _ApplyLimitSpeed(RigidBody2D body, Physics2DDirectBodyState state){
        if(state.LinearVelocity.Length() > MaxSpeed){
            body.LinearVelocity = state.LinearVelocity.Normalized() * MaxSpeed;
        }
    }





    private void _ApplyJump(RigidBody2D body, Physics2DDirectBodyState state){
        if(Input.IsActionJustPressed(JumpAction)){
            var velocity = state.LinearVelocity;
            velocity.y = -JumpForce;
            body.LinearVelocity = velocity;
        }
    }





    private void _ApplyDeceleration(RigidBody2D body, Physics2DDirectBodyState state){
        if (
                    !Input.IsActionPressed(LeftAction) &&
                    !Input.IsActionPressed(RightAction) &&
                    !Input.IsActionPressed(UpAction) &&
                    !Input.IsActionPressed(DownAction)
                )
        {
            if(body.LinearVelocity.Length() < 5f){
                body.LinearVelocity = Vector2.Zero;
                return;
            }
            var desacceleration = -state.LinearVelocity.Normalized() * Desacceleration;
            body.AppliedForce += desacceleration;
        }
    }
}