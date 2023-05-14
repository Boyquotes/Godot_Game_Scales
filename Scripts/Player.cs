using Godot;

using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 60.0f;
	public const float JumpVelocity = -200.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		//Ars();
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y = /*gravity * */ -Position.Y * 20f * (float)delta - 12;

		// Handle Jump.
		//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		//velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y += direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Vector2 lerpVelocity = Velocity.Lerp(velocity, (float)delta);
		Velocity = lerpVelocity;
		Rotation = lerpVelocity.X / 140f;
		MoveAndSlide();
	}


}
