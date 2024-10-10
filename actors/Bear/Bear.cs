using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
public partial class Bear : CharacterBody2D
{
    [Signal]
    public delegate void HitEventHandler();

    private CharacterBody2D _target;

    public const float Speed = 250.0f;
	public const float JumpVelocity = -400.0f;

    private AnimatedSprite2D _Bear_Animated;
    private CollisionShape2D _Hit;
    private CollisionShape2D _Hit_back;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        _target = GetTree().Root.GetNode<CharacterBody2D>("Node2D/Player");
        _Bear_Animated = GetNode<AnimatedSprite2D>("Bear_Animated");
        _Hit = GetNode<CollisionShape2D>("hit");
        _Hit_back = GetNode<CollisionShape2D>("hit_back");
    }

    public override void _PhysicsProcess(double delta)
    {
        float Vita = 100;
        bool OnAttack = false;
        bool OnAttack2 = false;
        bool OnMove = false;
        bool OnJump = false;
        bool OnAction = false;
        bool Died = false;
        
        

        if (Vita == 0)
        {
            Died = true;
        }
        else
        {
            Died = false;
        }
        
        if (_target != null && GlobalPosition.DistanceTo(_target.Position) != 0)
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

        if (OnAttack | OnMove | OnJump | Died | OnAttack2)
        {
            OnAction = true;
        }
        else
        {
            OnAction = false;
        }

        Vector2 velocity = Velocity;
        Vector2 scale = Scale;

        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;
        
        if (_target != null)
        {
            Vector2 direction = (_target.GlobalPosition - GlobalPosition).Normalized();
            int i = 0;
            while(direction.X != 0)
            {
                if (_target != null && GlobalPosition.DistanceTo(_target.Position) != 0)
                {
                    i = i + 1;
                    OnAttack = true;
                }      
                if (i == 3)
                {
                    OnAttack2 = true;
                }
            }
            if (direction.X < -0.99350154 && direction.X > -0.999628)
            {
                velocity.X = direction.X * Speed;
            }
            if (direction.X > 0.99362504 && direction.X < 0.9995912)
            {
                velocity.X = direction.X * Speed;
            }

            if (direction.X < 0)
			{
                _Bear_Animated.FlipH = true;
                
            }
			else if (direction.X > 0)
			{
                _Bear_Animated.FlipH = false;
            }
        }

        if (OnAction == true)
        {
            if (OnAttack && !OnMove && !OnJump)
            {
                _Bear_Animated.Play("bite");
                
            }
            else if (OnAttack2 && !OnMove && !OnJump)
            {
                _Bear_Animated.Play("hit");
            }

            if (OnMove && !OnJump)
            {
                _Bear_Animated.Play("run");
            }

            if (OnJump && !OnAttack)
            {
                _Bear_Animated.Play("jump");
            }

            if (Died)
            {
                _Bear_Animated.Play("die");
            }
        }
        else
        {
            _Bear_Animated.Play("idle");
            _Hit.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            _Hit_back.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }

        Scale = scale;
        Velocity = velocity;
		MoveAndSlide();
	}
    
}
