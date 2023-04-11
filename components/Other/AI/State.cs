using Godot;
using System;
using System.Collections.Generic;

/**
<summary>
A state is a component that can be added to a StateMachine.

The purpose of the state is to define a behaviour that will be executed for a certain amount of time.

This behaviour can be defined by adding other components to the state,
then thanks to the statemachine, these behaviours will be extracted from the state and then
added to the statemachine's parent.
</summary>
**/
public class State : Component<Node, State._Props, None>
{
    public class _Props{
        public float? Duration;
        public float? DurationVariance;
        public Node NextState;
    }

    [Export]
    public float Duration{get; set;} = 0;
    [Export]
    public float DurationVariance{get; set;} = 0;
    [Export]
    public NodePath NextStatePath{get; set;} = null;
    public Node NextState{get; set;} = null;

    

    public Node[] ChildrenProcesses{get; private set;} = null;
    protected override void _Init(_Props props)
    {
        Duration = props.Duration ?? Duration;
        DurationVariance = props.DurationVariance ?? DurationVariance;
        NextState = props.NextState ?? NextState ?? GetNodeOrNull(NextStatePath);

        ChildrenProcesses = DisableChildren();

        //_GroupAllChildrenWithName();
    }

    private Node[] DisableChildren(){
        List<Node> children = new List<Node>();

        foreach(Node child in GetChildren()){
            RemoveChild(child);

            children.Add(child);
        }

        return children.ToArray();
    }

    public void AddChildrenProcesses(Node[] children){
        ChildrenProcesses = children;
    }

    public string GetStateGroupName(){
        return $"__state_{Name}_{this.GetInstanceId()}";
    }

    private void _GroupAllChildrenWithName(){
        foreach(Node child in ChildrenProcesses){
            child.AddToGroup(GetStateGroupName());
        }
    }
}
