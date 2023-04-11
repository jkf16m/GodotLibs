using Godot;

public delegate void IntegratedForcesDelegate(RigidBody2D body, Physics2DDirectBodyState state);
public interface IIntegratedForces
{
    event IntegratedForcesDelegate IntegratedForces;
}

public delegate void ProcessedDelegate(float delta);
public interface IProcessed
{
    event ProcessedDelegate Processed;
}


public interface IRigidBody2DEvents<T>
: IIntegratedForces
where T : RigidBody2D
{
}