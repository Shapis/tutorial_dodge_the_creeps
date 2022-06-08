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

    public void _on_player_hit()
    {
        GameOver();
    }

    public void GameOver()
    {
        GetNode<AudioStreamPlayer>("Music").Stop();
        GetNode<AudioStreamPlayer>("DeathSound").Play();
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
    }


    public void NewGame()
    {
        GetNode<AudioStreamPlayer>("Music").Play();
        // Note that for calling Godot-provided methods with strings,
        // we have to use the original Godot snake_case name.
        GetTree().CallGroup("mobs", "queue_free");

        Score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();

        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(Score);
        hud.ShowMessage("Get Ready!");
    }



    public void _on_MobTimer_timeout()
    {
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        var mob = (Mob)MobScene.Instantiate();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        mob.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);
    }

    public void _on_ScoreTimer_timeout()
    {
        Score++;
        GetNode<HUD>("HUD").UpdateScore(Score);
    }

    public void _on_StartTimer_timeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
        GD.Print("abc");
    }

    public void _on_HUD_start_game()
    {
        NewGame();
    }

}
