using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void HitEventHandler();
    public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
    public Vector2 ScreenSize;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D _Wolf_Animated;
    private AnimatedSprite2D _Fox_Animated;
    private ProgressBar _vita;
    private CollisionShape2D _Bite;
    private CollisionShape2D _bodyWolf;
    private CollisionShape2D _bodyFox;
    private CollisionShape2D _Hit;
    public override void _Ready()
    {
        _Wolf_Animated = GetNode<AnimatedSprite2D>("Wolf");
        _Fox_Animated = GetNode<AnimatedSprite2D>("Fox");
        _vita = GetNode<ProgressBar>("Barra_vida/Vita");
        _Bite = GetNode<CollisionShape2D>("bite");
        _bodyWolf = GetNode<CollisionShape2D>("body_Wolf");
        _bodyFox = GetNode<CollisionShape2D>("body_Fox");
        _Hit = GetTree().Root.GetNode<CollisionShape2D>("Node2D/Bear/hit");
    }

    public override void _PhysicsProcess(double delta)
	{
        bool OnAttack = false;
        bool OnMove;
        bool OnJump = false;
        bool OnAction = false;
        bool Died = false;
        int Vita = 100;

        if (_Hit.Position >= Position)
        {
            Vita -= 10;
        }

        _vita.Value = Vita;
        if (Vita == 0)
        {
            Died = true;
        }

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

        if (OnAttack | OnMove | OnJump | Died)
        {
            OnAction = true;
        }
        else
        {
            OnAction = false;
        }

        if (Input.IsKeyPressed(Key.Q))
        {
            _Wolf_Animated.Visible = true;
            _Fox_Animated.Visible = false;
            _bodyWolf.Visible = true;
            _bodyFox.Visible = false;
            _bodyWolf.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
            _bodyFox.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

        }
        if (Input.IsKeyPressed(Key.E))
        {
            _Wolf_Animated.Visible = false;
            _Fox_Animated.Visible = true;
            _bodyWolf.Visible = false;
            _bodyFox.Visible = true;
            _bodyWolf.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            _bodyFox.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
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
                _Bite.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
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
                _Bite.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
            }

            if (Died)
            {
                _Wolf_Animated.Play("die");
                _Fox_Animated.Play("die");
            }
        }
        else
        {
            _Wolf_Animated.Play("idle");
            _Fox_Animated.Play("idle");
            _Bite.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }

        Velocity = velocity;
		MoveAndSlide();
	}
}
