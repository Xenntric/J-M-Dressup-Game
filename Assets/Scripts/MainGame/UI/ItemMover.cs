using Godot;
using Godot.NativeInterop;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

public partial class ItemMover : Entity
{
	private Vector2 PosOffset;
	[Export] private Control FolderContainer;
	private Vector2 oldMousePos;
    [Export] private Node ItemLayerNode;
	private bool mouseMoving;
	private Vector2	TextureSize;
	private bool grabbed;
	[Export] private Vector2	magnetPosition;

	public override void _Ready()
	{
		this.ProcessPriority = 1;
		this.TextureSize = this.TextureNormal.GetSize();
		GD.Print("ItemMover");

		this.ButtonDown += AttachAndMove;
	}

	private void AttachAndMove()
	{		
		PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;
		if(GetParent() != ItemLayerNode)
		{
			var menumidpoint = this.Position + Size/2;
			SetZIndex();
			SetSize(TextureSize);
			Position = menumidpoint - Size*.33f/2;

			Reparent(ItemLayerNode);

			PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;

			ToggleMode = true;
			ButtonPressed = true;
			grabbed = true;
			GD.Print(ButtonPressed);
		}
	}
	
	public override void _Process(double delta)
	{

	}
    public override void _Pressed()
    {
        base._Pressed();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if(@event.IsActionReleased("Grab") && grabbed)
		{
			grabbed = false;
			ToggleMode = false;
		}

		else if (@event is InputEventMouseMotion eventMouseMotion && ButtonPressed)
        {
			// GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
			GlobalPosition = eventMouseMotion.Position - PosOffset;
		}
    }
}
