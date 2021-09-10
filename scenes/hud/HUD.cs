using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Timer>("MessageTimer").Connect("timeout",this,"onMessageTimeout");    
        GetNode<Button>("StartButton").Connect("pressed",this,"onStartButtonPressed");
    }

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    public void onMessageTimeout()
    {
            GetNode<Label>("Message").Hide();
    }

    async public void ShowGameOver()
    {
    ShowMessage("Game Over");

    var messageTimer = GetNode<Timer>("MessageTimer");
    await ToSignal(messageTimer, "timeout");

    var message = GetNode<Label>("Message");
    message.Text = "Dodge the\nCreeps!";
    message.Show();

    await ToSignal(GetTree().CreateTimer(1), "timeout");
    GetNode<Button>("StartButton").Show();
    }

    public void onStartButtonPressed()
    {
         GetNode<Button>("StartButton").Hide();
         EmitSignal("StartGame");
    }
    public void UpdateScore(int score)
    {
    GetNode<Label>("ScoreLabel").Text = score.ToString();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
