using Godot;
using System;

public partial class Pause_Menu : Control
{
    private Control _configuration;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _configuration = GetNode<Control>("Configuracion");
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
		_configuration.Visible = false;
    }

	public void _on_btn_regresar_pressed()
	{
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://UI/Menu.tscn"));
    }

	public void _on_btn_configuracion_pressed()
	{
		_configuration.Visible = true;
	}

    public void _on_btn_salir_pressed()
	{
        GetTree().Quit();
    }
}
