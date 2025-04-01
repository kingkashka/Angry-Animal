using Godot;
using System;
using System.Xml.Serialization;

public partial class Cup : StaticBody2D
{
	public const string CUP_GROUP = "cup";
	[Export] AnimationPlayer cupAnimation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cupAnimation.AnimationFinished += CupAnimationFinished;
		SignalManager.Instance.OnCupDestroyed += CupDie;
	}
	public override void _EnterTree()
	{
		AddToGroup(CUP_GROUP);
	}
	public override void _ExitTree()
	{
		SignalManager.Instance.OnCupDestroyed -= CupDie;
		cupAnimation.AnimationFinished -= CupAnimationFinished;
	}
	public override void _PhysicsProcess(double delta)
	{
		// GD.Print("Cup Physics Process");
	}
    public void CupAnimationFinished(StringName animName)
    {
		SignalManager.EmitOnCupDied();
		QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
	public void CupDie()
	{
		GD.Print($"Cup {Name} is dying");
		cupAnimation.Play("Vanish");
	}

}
