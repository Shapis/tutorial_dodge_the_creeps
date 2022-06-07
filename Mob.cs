using Godot;
using System;

public partial class Mob : RigidDynamicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    public override void _Ready()
    {
        var animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        animSprite.Playing = true;
        string[] mobTypes = animSprite.Frames.GetAnimationNames();
        animSprite.Animation = mobTypes[GD.Randi() % mobTypes.Length];
    }

    private void _on_VisibilityNotifier2D_screen_exited()
    {
        QueueFree();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
