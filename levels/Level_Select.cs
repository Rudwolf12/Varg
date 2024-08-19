using Godot;
using System;

public partial class Level_Select : Control
{
    private Label lblLevel;
    private Label lblInstruccion;
    private Button btnExit;
    public override void _Ready()
    {
        lblLevel = GetNode<Label>("lblLevel");
        lblInstruccion = GetNode<Label>("Instruccion");
        btnExit = GetNode<Button>("btnExit");
    }
    private int level_num = 0; 
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Random rand = new Random();
        int randomInRange = rand.Next(1, 100);

        if (Input.IsActionJustPressed("ui_up"))
        {
            level_num++;
        }
        if (Input.IsActionJustPressed("ui_down"))
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
        if (level_num == 2)
        {
            lblLevel.Text = "2 -- Multiplayer Prueba";

        }

        if (randomInRange == 25)
            lblInstruccion.Text = "Presiona Enter para continuar";

        if (randomInRange == 75)
            lblInstruccion.Text = "Presiona para continuar";

        if (Input.IsKeyPressed(Key.Enter) & lblLevel.Text == "1 -- Prueba")
            GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://levels/prueba.tscn"));
        if (Input.IsKeyPressed(Key.Enter) & lblLevel.Text == "2 -- Multiplayer Prueba")
            GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://levels/Multiplayer.tscn"));
    }
    public void _on_btn_exit_pressed()
    {
        GetTree().Quit();
    }
}
