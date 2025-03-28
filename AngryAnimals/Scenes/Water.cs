using Godot;
using System;

public partial class Water : Area2D
{
	[Export] AudioStreamPlayer2D splashSound;
	[Export] CollisionShape2D waterBoundary;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		
	}

    private void OnBodyEntered(Node2D body)
    {
        if(body is Bird)
		{
			GD.Print("Bird Splash");
			splashSound.Play();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
