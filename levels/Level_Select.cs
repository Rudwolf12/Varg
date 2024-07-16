using Godot;
using System;

public partial class Level_Select : Control
{
    // Called when the node enters the scene tree for the first time.
    private Label lblLevel;
    private Button btnExit;
    public override void _Ready()
	{
		lblLevel = GetNode<Label>("lblLevel");
        btnExit = GetNode<Button>("btnExit");
    }
    public int level_num = 0;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("ui_up"))
        {
            level_num++;
        }
        Console.WriteLine(level_num);
        if (Input.IsActionPressed("ui_down"))
        {
            level_num--;
        }

        if (level_num == 0)
        {
            lblLevel.Text = "0 -- Level Select";
        }
        if (level_num == 1)
        {
            lblLevel.Text = "1 -- Prueba";

        }
        if (Input.IsKeyPressed(Key.Enter))
            GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://levels/prueba.tscn"));
         
    }
    public void _on_btn_exit_pressed()
    {
        GetTree().Quit();
    }
}
