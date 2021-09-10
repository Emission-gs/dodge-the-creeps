using Godot;
using System;

public class Player : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public int Speed = 400;

    [Signal]
    public delegate void Hit();
    private Vector2 _screenSize;
    private Vector2 _target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Connect("body_entered",this,"OnBodyEntered");
    }
    
    public void OnBodyEntered(PhysicsBody2D body)
    {
    Hide(); // Player disappears after being hit.
    EmitSignal("Hit");
    GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void Start(Vector2 pos)
    {
    Position = pos;
    _target = pos;
    Show();
    GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
    var velocity = new Vector2();

    var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    if (Position.DistanceTo(_target) > 10)
    {
        velocity = _target - Position;
    }
    if(velocity.Length() > 0)
    {
        velocity = velocity.Normalized() * Speed;
        animatedSprite.Play();
    }
    else {
        animatedSprite.Stop();
    }

    Position += velocity * delta;
    Position = new Vector2(
    x: Mathf.Clamp(Position.x, 0, _screenSize.x),
    y: Mathf.Clamp(Position.y, 0, _screenSize.y)
    );
 
    // Directional Animation Sprite
    if(velocity.x!=0)
    {
        animatedSprite.Animation = "walk";
        animatedSprite.FlipV = false;
        animatedSprite.FlipH = velocity.x < 0;
    }
    if(velocity.y != 0)
    {
        animatedSprite.Animation="up";
        animatedSprite.FlipV = velocity.y > 0;
        animatedSprite.FlipH = false;
    }
 }

  
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventScreenTouch eventMouseButton && eventMouseButton.Pressed)
        {
            _target = (@event as InputEventScreenTouch).Position;
        }
    }


}
