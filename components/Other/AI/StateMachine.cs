using Godot;
using System;

public class StateMachine : Component<Node, StateMachine._Props, None>
{
    public class _Props{
        public Node InitialState;
    }

    [Export]
    public NodePath InitialStatePath{get; set;} = null;
    public Node InitialState{get; set;} = null;

    public State CurrentState{get; private set;} = null;

    private Timer _Timer{get; set;} = null;
    protected override void _Init(_Props props)
    {
        InitialState = props.InitialState ?? InitialState ?? GetNodeOrNull(InitialStatePath);

        CurrentState = InitialState as State;

        _ExtractState();

        _Timer = GetNode<Timer>("Timer");

        _Timer.Start(_CalculateDuration());

        _Timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    private float _CalculateDuration(){
        return CurrentState.Duration + (float)GD.RandRange(0, CurrentState.DurationVariance);
    }

    private void OnTimerTimeout()
    {
        // removes the past state's children from the parent of the state machine
        // and adds them to the current state
        _CompressState();
        // changes the current state to the next state
        _ChangeState();
        // extracts the current state's children and adds them to the parent of the state machine
        _ExtractState();

        // starts the next state, with the specified duration
        // duration = state.duration + random(0, state.durationVariance)
        _Timer.Start(_CalculateDuration());
    }

    private void _CompressState(){
        var children = Parent.GetTree().GetNodesInGroup(CurrentState.GetStateGroupName()).ToArray<Node>();

        CurrentState.AddChildrenProcesses(children);
    }

    private void _ExtractState(){
        foreach(Node child in CurrentState.ChildrenProcesses){
            // grandparent of the states, parent of the state machine
            Parent.CallDeferred("add_child", child);
        }
    }

    private void _ChangeState(){
        CurrentState = CurrentState.NextState as State;
    }
}
