using Godot;
using System;

public partial class Level : Node2D
{
	[Export] Marker2D spawnPoint;
	[Export] PackedScene birdScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.OnBirdDied += OnBirdDied;
		OnBirdDied();
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnBirdDied -= OnBirdDied;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnBirdDied()
	{ 
		Bird newBird = birdScene.Instantiate<Bird>();
		newBird.Position = spawnPoint.Position;
		AddChild(newBird);
	}
}
