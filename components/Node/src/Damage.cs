using System;
using Godot;

public class Damage : Component<Node, Damage._Props, None>
{
    public class _Props
    {
        public int? Damage;
    }



    [Export]
    public int Value{get; private set;}

    protected override void _Init(Damage._Props props)
    {
        Value = props.Damage ?? Value;
    }
}
