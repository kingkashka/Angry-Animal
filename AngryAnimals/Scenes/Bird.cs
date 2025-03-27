using Godot;
using System;

public partial class Bird : RigidBody2D
{
	public enum AnimalState { READY, DRAG, RELEASE }
	[Export] private Label label;
	[Export] private Sprite2D arrow;
	[Export] private VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;
	[Export] private AudioStreamPlayer2D stretchSound;
	[Export] private AudioStreamPlayer2D launchSound;
	[Export] private AudioStreamPlayer2D kickSound;

	private AnimalState state = AnimalState.READY;
	private float arrowScaleX = 0.0f;
	private Vector2 start = Vector2.Zero;
	Vector2 draggedVector = new Vector2(0, 0);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
		InitializeVariables();
	}

	public override void _Process(double delta)
	{
			UpdateDebugLabel();
	}
	private void UpdateDebugLabel()
	{
		label.Text = $"{state} SL{Sleeping}";
	}
	private void ConnectSignals()
	{
		visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
		SleepingStateChanged += OnSleepingStateChanged;
		InputEvent += OnInputEvent;
	}
	private void InitializeVariables()
	{
		start = Position;
		arrowScaleX = arrow.Scale.X;
		arrow.Hide();
	}
	private void OnSleepingStateChanged()
	{

	}



	private void OnScreenExited()
	{
		GD.Print("Bird off screen");
		// QueueFree();
	}


	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		InputEventMouseButton ev = @event as InputEventMouseButton;
		if (ev.ButtonMask == MouseButtonMask.Left)
		{
			Position = GetGlobalMousePosition();
			HandleDragging();
		}
	}

	private void HandleDragging()
	{

	}

	private void OnTimeout()
	{
		ApplyCentralImpulse(new Vector2(50, -400));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
