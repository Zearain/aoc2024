using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public event Action<BaseDay> DayEntered;

	[Node("AnimatedSprite2D")]
	private AnimatedSprite2D _animatedSprite;

	public BaseDay? CurrentDay { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Check if the player is attempting to enter a door.
		if (CurrentDay != null && Input.IsActionJustPressed("interact"))
		{
			OnDayEntered(CurrentDay);
			// We can also return early here to prevent weird behavior during scene transitions.
			return;
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("move_left", "move_right");

		if (direction < 0f)
		{
			_animatedSprite.FlipH = true;
		}
		else if (direction > 0f)
		{
			_animatedSprite.FlipH = false;
		}

		if (direction == 0f)
		{
			_animatedSprite.Play("idle");
		}
		else
		{
			_animatedSprite.Play("run");
		}

		if (direction != 0f)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    private void OnDayEntered(BaseDay day)
	{
		DayEntered?.Invoke(day);
	}
}
