using Godot;
using System;

public partial class hud : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    public void _on_hud_start_game()
    {
        var hud = GetNode<hud>("HUD");
        hud.UpdateScore(2);
        hud.ShowMessage("Get Ready!");
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

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    public void _on_MessageTimer_timeout()
    {
        GetNode<Label>("Message").Hide();
    }

    public void _on_StartButton_pressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }
}
