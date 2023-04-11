using Godot;
using System;

public interface ICreator{
    Node2D Create<T>(Action<T> Initializer) where T : Node2D;
    T Create<T>() where T : Node2D;
    T Create<T>(T instance) where T : Node2D, IInit<T>;
    event Action<Node2D> Created;
}
/**
<summary>
    ClassType indicates a derived class of Node2D
</summary>
*/
public class Creator : whitecat.v2.Component<Node2D, ICreator>
{

    public event Action<Node2D> Created;

    [Export]
    public PackedEntity<Node2D> CreationScene {get; set;} = null;

    protected override void _Init(ICreator props)
    {
    }

    public T Create<T>() where T : Node2D{
        var creation = CreationScene.CreateGodotNode<T>((o)=>{
            o.Position = Parent.GlobalPosition;
        });
    
        Created?.Invoke(creation);

        return creation;
    }

    public I Create<T,I>(Action<I> initializer) where T : Node2D, I{
        var creation = Create<T>();

        initializer?.Invoke(creation);

        return creation;
    }

    public T Create<T>(Action<T> initializer) where T : Node2D{
        var creation = Create<T>();

        initializer?.Invoke(creation);

        return creation;
    }


    public Node2D Create(Action<Node2D> Initializer)
    {
        var creation = CreationScene.Create(Initializer);

        creation.Position = Parent.GlobalPosition;

        return creation;
    }

}
