using Godot;
using System;
using System.Reflection.Metadata;

public partial class Bird : RigidBody2D
{
	#region Variables
	public enum AnimalState { READY, DRAG, RELEASE }

	private static readonly Vector2 DragLimMax = new Vector2(0, 60);
	private static readonly Vector2 DragLimMin = new Vector2(-60, 0);

	private const float impulseMultiplier = 20.0f;
	private const float impulseMax = 1200.0f;

	[Export] private Label label;
	[Export] private Sprite2D arrow;
	[Export] private VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;
	[Export] private AudioStreamPlayer2D stretchSound;
	[Export] private AudioStreamPlayer2D launchSound;
	[Export] private AudioStreamPlayer2D kickSound;

	private AnimalState state = AnimalState.READY;
	private float arrowScaleX = 0.0f;
	private Vector2 start = Vector2.Zero;
	private Vector2 draggedStart = Vector2.Zero;
	private Vector2 draggedVector = Vector2.Zero;
	private Vector2 lastDraggedVector = Vector2.Zero;

	#endregion

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
		InitializeVariables();

	}

	public override void _PhysicsProcess(double delta)
	{
		UpdateDebugLabel();
		UpdateState();
	}
	private void UpdateDebugLabel()
	{
		label.Text = $"{state} SL{Sleeping}";
		label.Text += $"{draggedStart.X: F!}, {draggedStart.Y:F1}/n";
		label.Text += $"{draggedVector.X}, {draggedVector.Y}";
	}
	private void ConnectSignals()
	{
		visibleOnScreenNotifier2D.ScreenExited += BirdDied;
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



	private void BirdDied()
	{
		GD.Print("Bird off screen");
		QueueFree();
		SignalManager.EmitOnBirdDied();
	}


	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (state == AnimalState.READY && @event.IsActionPressed("drag"))
		{
			GD.Print("Dragged");
			ChangeState(AnimalState.DRAG);
		}
		// InputEventMouseButton ev = @event as InputEventMouseButton;

	}
	


	private void ChangeState(AnimalState newState)
	{
		state = newState;
		switch (state)
		{
			case AnimalState.DRAG:
				StartDragging();
				break;
			case AnimalState.RELEASE:
				StartRelease();
				break;
		}
	}
	private bool DetectRelease()
	{
		if (state == AnimalState.DRAG && Input.IsActionJustReleased("drag"))
		{
			ChangeState(AnimalState.RELEASE);
			return true;
		}
		return false;
	}
	private void UpdateArrowScale()
	{
		float impulseLength = CalculateImpulse().Length();
		float scalePercentage = impulseLength / impulseMax;
		arrow.Scale = new Vector2(
			(arrowScaleX * scalePercentage) + arrowScaleX,
			arrow.Scale.Y
		);
		arrow.Rotation = (start - Position).Angle();
	}
	private Vector2 CalculateImpulse()
	{
		return draggedVector * -impulseMultiplier;
	}
	private void StartRelease()
	{
		arrow.Hide();
		launchSound.Play();
		Freeze = false;
		ApplyCentralImpulse(CalculateImpulse());
	}
	#region HandleDrag Function
	private void ConstrainDragWithinLimits()
	{
		lastDraggedVector = draggedVector;
		draggedVector = draggedVector.Clamp(DragLimMin, DragLimMax);
		Position = start + draggedVector;
	}
	private void PlayStretchSound()
	{
		Vector2 diff = draggedVector - lastDraggedVector;
		if (diff.Length() > 0 && !stretchSound.Playing)
		{
			stretchSound.Play();
		}
	}
	private void UpdateDraggedVector()
	{
		draggedVector = GetGlobalMousePosition() - draggedStart;
	}
	private void StartDragging()
	{
		draggedStart = GetGlobalMousePosition();
		arrow.Show();
	}
	private void HandleDragging()
	{
		if (DetectRelease())
			return;

		UpdateDraggedVector();
		PlayStretchSound();
		ConstrainDragWithinLimits();
		UpdateArrowScale();
	}
	#endregion

	private void UpdateState()
	{
		switch (state)
		{
			case AnimalState.DRAG:
				HandleDragging();
				break;
			case AnimalState.RELEASE:
				// StartRelease();
				break;
		}
	}

}
