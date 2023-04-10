using System;
using Godot;

public class RigidBody2DExtension
{
    public RigidBody2D Node{get; private set;}

    public RigidBody2DExtension(RigidBody2D node)
    {
        Node = node;
    }


}