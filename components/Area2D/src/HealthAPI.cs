using System;
using Godot;

public class HealthAPI : Component<Area2D, HealthAPI._Props, None>{
    public class _Props{
        public string[] Groups;
    }

    [Export]
    public string[] Groups{get; set;}
    [Export]
    public NodePath HealthPath{get; set;}
    public Health Health{get; set;}

    protected override void _Init(HealthAPI._Props props){
        Groups = props.Groups ?? Groups;       
        Health = GetNodeOrNullOnce<Health>(HealthPath) ?? Health;

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