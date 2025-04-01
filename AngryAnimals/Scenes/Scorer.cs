using Godot;
using System;

public partial class Scorer : Node
{
	private int totalCups = 0;
	private int cupsHit = 0;
	public static int attempts = 0;
	public override void _Ready()
	{
		SignalManager.Instance.OnCupDestroyed += OnCupDestroyed;
		SignalManager.Instance.OnAttemptMade += OnAttemptMade;
		totalCups = GetTree().GetNodesInGroup(Cup.CUP_GROUP).Count;
		GD.Print("Total Cups: " + totalCups);
	}

    private void OnAttemptMade()
    {
        attempts++;
		SignalManager.EmitOnAttemptUpdated(attempts);
		GD.Print($"Attempt Made: {attempts}");
    }

    public override void _ExitTree()
	{
		SignalManager.Instance.OnCupDestroyed -= OnCupDestroyed;
		SignalManager.Instance.OnAttemptMade -= OnAttemptMade;
	}
	private void OnCupDestroyed()
	{
		cupsHit++;
		totalCups--;
		GD.Print(totalCups);
		if (totalCups == 0)
		{
			SignalManager.EmitOnLevelComplete();
			GD.Print("All Cups Destroyed!");
			ScoreManager.SetScoreforLevel(ScoreManager.GetLevelSelected(), attempts);
		}
		else
		{
			GD.Print("Cups Hit: " + cupsHit);
		}
	}

}
