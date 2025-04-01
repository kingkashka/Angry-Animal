using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private Label levelLabel;
	[Export] private Label attemptsLabel;
	[Export] private VBoxContainer levelCompleteContainer;
	[Export] private AudioStreamPlayer2D levelCompleteSound;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		levelCompleteContainer.Hide();
		SignalManager.Instance.OnLevelComplete += ShowOnLevelComplete;
		SignalManager.Instance.OnAttemptUpdated += UpdateAttemptUpdated;
		levelLabel.Text = $"Level: {ScoreManager.GetLevelSelected()}";

	}

	private void UpdateAttemptUpdated(int number)
	{
		attemptsLabel.Text = $"Attempts: {number}";
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnLevelComplete -= ShowOnLevelComplete;
		SignalManager.Instance.OnAttemptUpdated -= UpdateAttemptUpdated;
	}
	public override void _Process(double delta)
	{
		OnLevelCompleteButtonPressed();
	}
	private void ShowOnLevelComplete()
	{
		levelCompleteContainer.Show();
		levelCompleteSound.Play();
	}

	private void OnLevelCompleteButtonPressed()
	{
		if (Input.IsKeyPressed(Key.Space) && levelCompleteContainer.Visible)
		{
			GameManager.LoadMain();
		}

	}
}
