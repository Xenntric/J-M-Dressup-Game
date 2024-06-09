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
    [Export] public Node ItemLayerNode;
	private bool mouseMoving;
	private Vector2	TextureSize;
	private bool grabbed;
	
	public override void _EnterTree()
	{

	}

	public override void _Ready()
	{
		this.ProcessPriority = 1;
		this.TextureSize = this.TextureNormal.GetSize();
		GD.Print("ItemMover");

		this.ButtonDown += AttachAndMove;
	}

	private void AttachAndMove()
	{		
		var menusize = Size;
		
		PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;
		GD.Print(GlobalPosition);
		if(GetParent() != ItemLayerNode)
		{
			var menumidpoint = this.Position + Size/2;
			SetZIndex();
			if(TextureSize *.33f < menusize)
			{
				GD.Print("Smaller");
				Position = menumidpoint - TextureSize*.33f/2;	
			}
			else
			{
				GD.Print("Bigger");
				Position = menumidpoint - TextureSize*.33f/2;		
			}

			SetSize(TextureSize);
			Reparent(ItemLayerNode);

			PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;

			ToggleMode = true;
			ButtonPressed = true;
			grabbed = true;
			GD.Print(ButtonPressed);
		}
		
		// this.Position = PosOffset;
	}
	
	public override void _Process(double delta)
	{
		var currentMousePos = this.GetViewport().GetMousePosition();
		mouseMoving = oldMousePos != currentMousePos;
		oldMousePos = currentMousePos;
		if(ButtonPressed && mouseMoving)
		{
			GlobalPosition = currentMousePos - PosOffset;
			
			// GD.Print ("mousepos: " + currentMousePos + "\n" + "itempos: " + this.GlobalPosition);
			if(!FolderContainer.GetGlobalRect().HasPoint(currentMousePos))
			{

				 GD.Print("Out!");
				// this.setNewSize();
				// GD.Print("feck");
				// this.ButtonPressed = true;
				// this.Reparent(InteractableItemLayer);
			}

			
			// if(exited)
			// {
			// 	this.Scale = new Vector2(.33f, .33f);
			// 	offset -= new Vector2(100,100);
			// 	GD.Print("moving " + this.Name);
			// 	exited = false;
			// }
		}
	}
    public override void _Pressed()
    {
        base._Pressed();
		// while(ButtonPressed)
		// {
		// 	GD.Print("BEEP!");
		// };
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if(@event.IsActionReleased("Grab") && grabbed)
		{
			grabbed = false;
			GD.Print("dorp");
			ToggleMode = false;

		}
    }
}
