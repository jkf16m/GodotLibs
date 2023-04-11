using System;
using Godot;

namespace whitecat.v2{
    public abstract class Component<ParentType, ImplementedInterface>
    : Node, IEntity<Node, ParentType>
    where ParentType : Node
    {
        public ParentType Parent {get; protected set;}

        public event Action<Node> Initialized;

        public void Init(Action<Node> props)
        {
            Parent = (ParentType)GetParent();
            props?.Invoke(this);
            Initialized?.Invoke(this);
            
        }

        protected abstract void _Init(ImplementedInterface props);

        public override void _Ready()
        {
            Init(null);
        }
    }
}