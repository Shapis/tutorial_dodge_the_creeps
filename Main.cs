using Godot;
using System;

public partial class Main : Node
{
    // Don't forget to rebuild the project so the editor knows about the new export variable.

#pragma warning disable 649
    // We assign this in the editor, so we don't need the warning about not being assigned.
    [Export] private PackedScene MobScene;
#pragma warning restore 649

    public int Score;

    public override void _Ready()
    {
        GD.Randomize();
    }

    public void _on_Player_hit()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    public void NewGame()
    {
        Score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        // player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
    }
}
