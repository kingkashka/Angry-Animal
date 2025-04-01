using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }
	
	private PackedScene MainScene = GD.Load<PackedScene>("res://Scenes/MainMenu.tscn");
	public override void _Ready()
	{
		Instance = this;
	}

	public static void LoadMain()
	{
		Instance.GetTree().ChangeSceneToPacked(Instance.MainScene);
	}
}
