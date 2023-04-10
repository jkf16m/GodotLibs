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
        var destructible = GetParentOrNull<IDestructible<Node>>();
        if(destructible == null){
            throw new Exception($"The parent node {GetParent().Name} does not implement the IDestructible interface");
        }
        var destroyedNodeRemainings = destructible.Destroy();
        Destroyed?.Invoke(destroyedNodeRemainings);
    }
}
