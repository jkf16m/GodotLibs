using System;
using System.Collections.Generic;
using Godot;

public class PackedEntity<ClassType> : PackedEntity<ClassType, ClassType>
where ClassType : Node{}

public class PackedEntity<ClassType, ImplementedInterface>
: PackedScene
where ClassType : Node, ImplementedInterface
{
    public ClassType Create(){
        var creation = Instance() as ClassType;

        return creation;
    }
    public ClassType Create(Action<ImplementedInterface> Initializer)
    {
        var creation = Create();

        Initializer(creation);

        return creation;
    }

    public T CreateGodotNode<T>(Action<T> value) where T : Node
    {
        var creation = Instance() as T;

        value(creation);

        return creation;
    }
}