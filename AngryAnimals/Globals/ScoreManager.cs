using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	const int defaultScore = 1000;
	[Export] int levelSelected;
	public override void _Ready()
	{
		Instance = this;
	}

	
	
	public static int GetLevelSelected()
	{
		return Instance.levelSelected;
	}
	public static int SetLevelSelected(int level)
	{
		Instance.levelSelected = level;
		return Instance.levelSelected;
	}
	public static int GetLevelBestScore()
	{
		return defaultScore;
	}
}
