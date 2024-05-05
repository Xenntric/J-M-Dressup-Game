using Godot;
using Godot.NativeInterop;
using System;
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
	
	// Called when the node enters the scene tree for the first time.

	public override void _EnterTree()
	{
		// FolderContainer = GetNode<Container>("/root/DressUpScene/ClothesControl/Menu Panel/Accessories");
		// InteractableItemLayer = GetNode<Control>("/root/DressUpScene/ClothesControl/ItemControl");
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

		if(GetParent() != ItemLayerNode)
		{
			this.Reparent(ItemLayerNode);
			SetZIndex();
			this.Size = TextureSize;
			this.Position = new Vector2(Position.X - menusize.X, Position.Y - menusize.Y);
			GlobalPosition = GetViewport().GetMousePosition() - PosOffset;
			ToggleMode = true;
			this.ButtonPressed = true;
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
