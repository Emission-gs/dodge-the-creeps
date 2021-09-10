using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
    public int MinSpeed = 150;
    [Export]
    public int MaxSpeed = 250;
    private static Random _random = new Random();
    private VisibilityNotifier2D visibilityNotifier2D;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimatedSprite animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        var mobTypes = animSprite.Frames.GetAnimationNames();
        animSprite.Animation = mobTypes[_random.Next(0,mobTypes.Length)];
        visibilityNotifier2D = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
        visibilityNotifier2D.Connect("screen_exited",this,"OnVisibilityNotifier2DScreenExited"); 
    }

    public void OnVisibilityNotifier2DScreenExited(){
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
