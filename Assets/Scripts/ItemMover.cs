using Godot;
using System;

public partial class ItemMover : TextureButton
{
	private Vector2 PosAtGrab;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += () => AttachAndMove();
	}

	private void AttachAndMove()
	{
		PosAtGrab = this.GetViewport().GetMousePosition() - this.GlobalPosition;
	}
	public override void _Process(double delta)
	{
		if(this.ButtonPressed)
		{
			var offset = PosAtGrab;
			GlobalPosition = this.GetViewport().GetMousePosition() - offset;
		}
	}

}
