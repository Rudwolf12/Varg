using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D _Wolf_Animated;
    private AnimatedSprite2D _Fox_Animated;
    private ProgressBar _vita;

    public override void _Ready()
    {
        _Wolf_Animated = GetNode<AnimatedSprite2D>("Wolf");
        _Fox_Animated = GetNode<AnimatedSprite2D>("Fox");
        _vita = GetNode<ProgressBar>("Vita");
    }

    public override void _PhysicsProcess(double delta)
	{
        bool OnAttack = false;
        bool OnMove = false;
        bool OnJump = false;
        bool OnAction = false;

        if (Input.IsActionPressed("ui_attack"))
        {
            OnAttack = true;
        }

        if (Velocity.X != 0)
        {
            OnMove = true;
        }
        else
        {
            OnMove = false;
        }

        if (Velocity.Y != 0)
        {
            OnJump = true;
        }

        if (OnAttack | OnMove | OnJump)
        {
            OnAction = true;
        }
        else
        {
            OnAttack = false;
        }

        if (Input.IsKeyPressed(Key.Q))
        {
            _Wolf_Animated.Visible = true;
            _Fox_Animated.Visible = false;

        }
        if (Input.IsKeyPressed(Key.E))
        {
            _Wolf_Animated.Visible = false;
            _Fox_Animated.Visible = true;
        }

        Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;
			

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
            velocity.X = direction.X * Speed;

            if (direction.X < 0)
			{
                _Wolf_Animated.FlipH = true;
                _Fox_Animated.FlipH = true;
            }
			else if (direction.X > 0)
			{
                _Wolf_Animated.FlipH = false;
                _Fox_Animated.FlipH = false;
            }
		}
		else
		{
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }
        

        if (OnAction == true)
        {
            if (OnAttack && !OnMove && !OnJump)
            {
                _Wolf_Animated.Play("bite");
                _Fox_Animated.Play("bite");
            }

            if (OnMove && !OnJump)
            {
                _Wolf_Animated.Play("run");
                _Fox_Animated.Play("run");
            }

            if (OnJump && !OnAttack)
            {
                _Wolf_Animated.Play("jump");
                _Fox_Animated.Play("jump");
            }
            else if (OnAttack && OnJump)
            {
                _Wolf_Animated.Play("jumping_bite");
            }
        }
        else
        {
            _Wolf_Animated.Play("idle");
            _Fox_Animated.Play("idle");
        }

        Velocity = velocity;
		MoveAndSlide();
	}

}
