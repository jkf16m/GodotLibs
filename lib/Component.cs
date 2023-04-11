using Godot;
using System;

/**
<summary>
Abstraction for all components.

Every component has to inherit from this class and implement the _Init method.

The _Init method is called when the component is ready and it receives the props of the defined type.
</summary>
<typeparam name="ParentType">The type of the parent node.</typeparam>
<typeparam name="PropsType">The type of the props.</typeparam>
<typeparam name="EventsType">The type of the events.</typeparam>
**/
public abstract class Component<ParentType, PropsType, EventsType>
: Node, IComponent<ParentType, PropsType, EventsType> 
where ParentType : Node
where PropsType : new()
where EventsType : new()
{
    public ParentType Parent {get; protected set;}
    public EventsType Events {get; protected set;}
    /**
    <summary>
    Should be overrided.
    This method's purpose is to expose the props to the inspector.
    </summary>
    <param name="props">The props of the component.</param>
    **/
    protected abstract void _Init(PropsType props);
    public void Init(PropsType props)
    {
        Parent = Parent ?? GetParent<ParentType>();
        if(Events == null)
            Events = new EventsType();
        _Init(props);
    }
    public override void _Ready()
    {
        Init(new PropsType());
    }



    /**
    UTILITY METHODS
    **/

    /**
    <summary>
    Gets a node from the parent node.
    But also, it sets the path to null (it uses the path only once).
    </summary>
    <typeparam name="T">The type of the node.</typeparam>
    <param name="path">The path of the node.</param>
    <returns>The node or null if it doesn't exist.</returns>
    **/
    protected T GetNodeOrNullOnce<T>(NodePath path) where T : Node
    {
        if(path == null)
            return null;

        var result = GetNodeOrNull<T>(path);
        path = null;

        return result;
    }
}