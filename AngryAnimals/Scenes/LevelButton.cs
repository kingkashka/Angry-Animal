using Godot;
using System;
using System.Reflection.Metadata;

public partial class LevelButton : TextureButton
{
	[Export] private int levelNumber { get; set; }
	[Export] private Label levelLabel;
	[Export] private Label scoreLabel;
	[Export] TextureButton levelButton;
	[Export] CursorShape cursorShape;
	public override void _Ready()
	{
		MouseEntered += onButtonHover;
		MouseExited += onButtonExit;
		Pressed += onButtonPressed;
		levelLabel.Text = levelNumber.ToString();
		scoreLabel.Text = ScoreManager.GetLevelBestScore(levelNumber).ToString();
	}

	public override void _ExitTree()
	{
		MouseEntered -= onButtonHover;
		MouseExited -= onButtonExit;
		Pressed -= onButtonPressed;
	}
    private void onButtonPressed()
    {
     ScoreManager.SetLevelSelected(levelNumber);
	 GetTree().ChangeSceneToFile($"res://Scenes/Level{levelNumber}.tscn");
    }

    private void onButtonHover()
	{
		GD.Print("Mouse entered button");
		levelButton.Scale = new Vector2(1.1f, 1.1f);
	
		
	}
	private void onButtonExit()
    {
		GD.Print("Mouse exited button");
        levelButton.Scale = new Vector2(1.0f, 1.0f);
    }
}
