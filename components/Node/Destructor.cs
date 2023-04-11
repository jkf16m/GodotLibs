using System;
using Godot;

public class Destructor : Component<Node, Destructor._Props, Destructor._Events>{
    public class _Props{
        public Node DestructionRemainings {get; set;} = null;
        public Action<Node> Destroyed {get; set;} = null;
    }
    public class _Events{
        public Action<Node> Destroyed;
    }

    [Export]
    public NodePath DestructionRemainingsPath {get; set;} = "";

    public Node DestructionRemainings {get; private set;} = null;

    protected override void _Init(_Props props)
    {
        DestructionRemainings = GetNodeOrNull(DestructionRemainingsPath) ?? props.DestructionRemainings;
        Events.Destroyed = props.Destroyed;


    }


    public Node Destroy(){
        var remainings = DestructionRemainings;

        Parent.QueueFree();

        Events.Destroyed?.Invoke(remainings);

        return remainings;
    }
    
}