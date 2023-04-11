using System;
using Godot;

public class ApplyInitialImpulse : Component<RigidBody2D, ApplyInitialImpulse._Props, None>{
    public class _Props{
        public Vector2? ImpulseForce;
        public float? ImpulseTorque;
    }

    [Export]
    public Vector2 ImpulseForce{get; set;}
    [Export]
    public float ImpulseTorque{get; set;}

    protected override void _Init(ApplyInitialImpulse._Props props){
        ImpulseForce = props.ImpulseForce ?? ImpulseForce;
        ImpulseTorque = props.ImpulseTorque ?? ImpulseTorque;

        _ApplyImpulses(Parent);
    }


    private void _ApplyImpulses(RigidBody2D body){
        body.ApplyCentralImpulse(ImpulseForce);
        body.ApplyTorqueImpulse(ImpulseTorque);
    }

}