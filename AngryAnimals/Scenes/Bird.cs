using Godot;
using System;

public partial class Bird : RigidBody2D
{
	[Export] private Timer timer;
	[Export] private Label label;
	[Export] private VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;
	[Export] private AudioStreamPlayer2D stretchSound;
	[Export] private AudioStreamPlayer2D launchSound;
	[Export] private AudioStreamPlayer2D kickSound;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Timeout += OnTimeout;
		InputEvent += OnInputEvent;
	}

    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        InputEventMouseButton ev = @event as InputEventMouseButton;
		if(ev.ButtonMask == MouseButtonMask.Left)
		{
			Position = GetGlobalMousePosition();
		}
    }


    private void OnTimeout()
    {
        ApplyCentralImpulse(new Vector2(50, -400));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		
	}
}
