using Godot;
using System;


/**
<summary>
When this node is added to a node, it will destroy the parent node after the specified time.
The parent node must implement the IDestructible interface.
</summary>
*/
public class DestructionTimer : Timer
{
    public delegate void DestroyedDelegate<T>(T destroyedNodeRemainings);
    public event DestroyedDelegate<Node> Destroyed;
    public override void _Ready()
    {
        Connect("timeout", this, nameof(OnTimeout));
    }

    private void OnTimeout(){
        var parent = GetParent();
        var destructor = parent.GetNodeOrNull<Destructor>("Destructor");

        if(destructor == null)
            QueueFree();

        var remainings = destructor.Destroy();   

        Destroyed?.Invoke(remainings);

    }
}
