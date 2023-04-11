using System;
using Godot;
using whitecat.v2;

public interface IHealthAPI{
    string[] Groups{get;}
    NodePath HealthPath{get;}
    Health Health{get;}
}
public class HealthAPI : whitecat.v2.Component<Area2D, IHealthAPI>{
    
    [Export]
    public string[] Groups{get; set;}
    [Export]
    public NodePath HealthPath{get; set;}
    public Health Health{get; set;}

    protected override void _Init(IHealthAPI props){
        Parent.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    private void OnAreaEntered(Area2D area){
        foreach(var group in Groups){
            if(area.IsInGroup(group)){
                var damage = area.GetNodeOrNull<Damage>("Damage");
                Health.TakeDamage(damage?.Value ?? 1);
            }
        }
    }


}