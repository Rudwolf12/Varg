using Godot;
using System;

public partial class Pause_Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	private CharacterBody2D _Player;
	public override void _Ready()
	{
		_Player = GetNode<CharacterBody2D>("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			Visible = true;
		}
	}
	public void _on_btn_reanudar_pressed()
	{
        Visible = false;
    }
	public void _on_btn_salir_pressed()
	{
        GetTree().Quit();
    }
}
