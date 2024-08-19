using Godot;
using System;

public partial class Menu : Control
{
    private Control _continue_menu;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _continue_menu = GetNode<Control>("Continue_Menu");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_new_game_pressed()
    {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://levels/Level_Select.tscn"));
    }

    public void _on_continue_pressed()
    {
        _continue_menu.Visible = true;
    }
    public void _on_exit_pressed()
    {
        GetTree().Quit();
    }
}
