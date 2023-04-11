using System;
using Godot;

public interface IEntity<T, ParentType> : IInit<T>, IInitialized<T>, IParent<ParentType>
{}