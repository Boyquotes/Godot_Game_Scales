using Godot;

using System;
using System.Threading;

public partial class Scales : Sprite2D
{
	[Export]
	private Sprite2D _cupLight;
	[Export]
	private Sprite2D _cupNight;

	public Action TriggerEnter;

	public float Weight { get; set; }

	private void OnTriggerEnter(Area2D area) => TriggerEnter?.Invoke();

	public override void _Process(double delta)
	{
		UpdateFullWeight((float)delta);
	}

	public void UpdateFullWeight(float delta)
	{
		UpdateWeightLerp(_cupLight, -Weight, delta);
		UpdateWeightLerp(_cupNight, Weight, delta);
		if(Mathf.Abs(Weight) >64)
			GetTree().ReloadCurrentScene();

	}

	private void UpdateWeightLerp(Sprite2D cup, float weight, float delta) => cup.Position = cup.Position.Lerp(new Vector2(cup.Position.X, weight), delta);

}




