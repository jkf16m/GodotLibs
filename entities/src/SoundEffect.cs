using Godot;
using System;

public class SoundEffect : Node
{
    [Export]
    public PackedScene AudioStreamPlayerScene{get; private set;}
    public override void _Ready()
    {
    }

    public AudioStreamPlayer SpawnSoundEffect(AudioStream audioStream){
        AudioStreamPlayer audioStreamPlayer = (AudioStreamPlayer)AudioStreamPlayerScene.Instance();
        audioStreamPlayer.Stream = audioStream;
        audioStreamPlayer.Play();
        return audioStreamPlayer;
    }
}
