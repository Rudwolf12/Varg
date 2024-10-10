using Godot;
using System;

public partial class Menu : Control
{
    private Control _continue_menu;
    private Control _configuration;
    private Control _help;
    private Control _credits;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _continue_menu = GetNode<Control>("Continue_Menu");
        _configuration = GetNode<Control>("Configuracion");
        _help = GetNode<Control>("Ayuda");
        _credits = GetNode<Control>("Creditos");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Escape))
        {
            _continue_menu.Visible = false;
            _configuration.Visible = false;
            _help.Visible = false;
            _credits.Visible = false;
        }
    }

    public void _on_new_game_pressed()
    {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://levels/Level_Select.tscn"));
    }

    public void _on_continue_pressed()
    {
        _continue_menu.Visible = true;
    }
    public void _on_options_pressed()
    {
        _configuration.Visible = true;
    }
    public void _on_help_pressed()
	{
        _help.Visible = true;
	}
    public void _on_credits_pressed()
    {
        _credits.Visible = true;    
    }
    public void _on_exit_pressed()
    {
        GetTree().Quit();
    }
}
