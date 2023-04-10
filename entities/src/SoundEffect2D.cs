using Godot;
using System;

public class SoundEffect2D : Node
{
    [Export]
    public PackedScene AudioStreamPlayerScene{get; private set;}
    public override void _Ready()
    {   
    }

    public AudioStreamPlayer2D SpawnSoundEffect(AudioStream audioStream, Vector2 position){
        AudioStreamPlayer2D audioStreamPlayer = (AudioStreamPlayer2D)AudioStreamPlayerScene.Instance();
        audioStreamPlayer.Stream = audioStream;
        audioStreamPlayer.Position = position;

        audioStreamPlayer.Connect("finished", this, nameof(OnAudioStreamPlayerFinished), new Godot.Collections.Array{audioStreamPlayer});
        audioStreamPlayer.Play();
        return audioStreamPlayer;
    }

    private void OnAudioStreamPlayerFinished(AudioStreamPlayer2D audioStreamPlayer){
        audioStreamPlayer.QueueFree();
    }

}
