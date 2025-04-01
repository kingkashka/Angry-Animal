using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }
	[Signal] public delegate void OnBirdDiedEventHandler();
	[Signal] public delegate void OnCupDestroyedEventHandler();
	[Signal] public delegate void OnLevelCompleteEventHandler();
	[Signal] public delegate void OnAttemptMadeEventHandler();
	[Signal] public delegate void OnAttemptUpdatedEventHandler(int number);
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void EmitOnBirdDied()
	{
		Instance.EmitSignal(SignalName.OnBirdDied);
	}
	public static void EmitOnCupDied()
	{
		Instance.EmitSignal(SignalName.OnCupDestroyed);
	}
	public static void EmitOnLevelComplete()
	{
		Instance.EmitSignal(SignalName.OnLevelComplete);
	}
	public static void EmitOnAttemptMade()
	{
		Instance.EmitSignal(SignalName.OnAttemptMade);
	}
	public static void EmitOnAttemptUpdated(int number )
	{
		Instance.EmitSignal(SignalName.OnAttemptUpdated, number);
	}
}
