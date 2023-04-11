using System;

public interface IInit<T>
{
    void Init(Action<T> props);
}

public interface IInitialized<T>
{
    event Action<T> Initialized;
}