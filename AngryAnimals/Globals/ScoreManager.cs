using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	const string scoreFilePath = "user://animals.save";
	List<LevelScore> levelScores = new List<LevelScore>();
	const int defaultScore = 1000;
	[Export] int levelSelected;
	public override void _Ready()
	{
		Instance = this;
		LoadScores();
	}

	public override void _ExitTree()
	{
		SaveScores();
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
	// Get best score for level
	public static LevelScore GetLevelScore(int level)
	{
		return Instance.levelScores.FirstOrDefault(x => x.LevelNumber == level);
	}
	public static int GetLevelBestScore(int levelNumber)
	{
		LevelScore levelScore = GetLevelScore(levelNumber);
		if (levelScore != null)
		{
			return levelScore.BestScore;
		}
		else
		{
			return defaultScore;
		}
	}

	// Set score for level
	public static void SetScoreforLevel(int levelNumber, int score)
	{
		LevelScore levelScore = GetLevelScore(levelNumber);
		if (levelScore != null)
		{
			if(score > levelScore.BestScore)
			{
				levelScore.BestScore = score;
				levelScore.DateSet = DateTime.Now;
			}
			else
			{
				Instance.levelScores.Add(new LevelScore(levelNumber, score));
			}
		}
	}

	//Save score to file
	private void SaveScores()
	{
		using var file = FileAccess.Open(scoreFilePath, FileAccess.ModeFlags.Write);
		if(file != null)
		{
			string jsonStr = JsonConvert.SerializeObject(levelScores);
			file.StoreString(jsonStr);
		}
	}

	//Load score from file
	private void LoadScores()
	{
		using var file = FileAccess.Open(scoreFilePath, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			string jsonStr = file.GetAsText();
			if(!string.IsNullOrEmpty(jsonStr))
			{
				levelScores = JsonConvert.DeserializeObject<List<LevelScore>>(jsonStr);
			}
		}
	}
}
