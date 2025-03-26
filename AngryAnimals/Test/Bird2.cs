using Godot;
using System;

public partial class Bird2 : RigidBody2D
{
	[Export] private Label label;
	[Export] private Timer timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Timeout += OnTimeout;
		InputEvent += OnInputEvent;
	}

    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        // GD.Print(@event.ToString());
		if(@event is InputEventMouseButton)
		{
			InputEventMouseButton ev = @event as InputEventMouseButton;
			if(ev.ButtonMask == MouseButtonMask.Left)
			{
				Position = GetGlobalMousePosition();
			}
		}
    }


    private void OnTimeout()
    {
        ApplyCentralImpulse(new Vector2(50, -350));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		label.Text = $"{Sleeping}";
	}
}
