using System;
using System.Collections.Generic;
using Godot;


/**
<summary>
Adds a health value to the node.
This is a component.

A component is a class that extends the Component class and implements the _Init method.
Internally is coupled with the Node class, so every single component will always be a simple Node.

The difference is, the parent of the component can be of any other derived type of Node.
</summary>
**/
public class Health : Component<Node, Health._Props, Health._Events>
{
    public class _Props
    {
        public int? MaxHealth;
        public int? CurrentHealth;
        public _Events Events;
    }
    public class _Events
    {
        public Action OnDeath;
        public Action<int> TakeDamage;
    }

    [Export]
    public int MaxHealth {get; private set;}
    [Export]
    public int CurrentHealth {get; private set;}

    protected override void _Init(_Props props)
    {
        MaxHealth = props.MaxHealth ?? MaxHealth;
        CurrentHealth = props.CurrentHealth ?? CurrentHealth;
        Events = props.Events;
    }



    // METHODS
    public void ChangeMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Events.OnDeath?.Invoke();
        }
        Events.TakeDamage?.Invoke(damage);
    }
    public void TakeDamage(Damage damage)
    {
        TakeDamage(damage.Value);
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}