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
		SignalManager.Instance.OnCupDestroyed += OnScoringPoints;
		OnBirdDied();
	}

    private void OnScoringPoints()
    {
		GD.Print("Scoring points");
    }


    public override void _ExitTree()
	{
		SignalManager.Instance.OnBirdDied -= OnBirdDied;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GoBackScene();
	}
	private void OnBirdDied()
	{ 
		Bird newBird = birdScene.Instantiate<Bird>();
		newBird.Position = spawnPoint.Position;
		AddChild(newBird);
	}
	private void GoBackScene()
	{
		if(Input.IsKeyPressed(Key.Backspace))
		{
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
		}
	}
}
