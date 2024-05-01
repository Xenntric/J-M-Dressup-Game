using Godot;
using System;
using System.Reflection;

public partial class ItemMover : Entity
{
	private Vector2 PosOffset;
	private Container FolderContainer;
	private Vector2 oldMousePos;
    [Export] public Control InteractableItemLayer;

	private bool mouseMoving;
	// Called when the node enters the scene tree for the first time.

	public override void _EnterTree()
	{
		// FolderContainer = GetNode<Container>("/root/DressUpScene/ClothesControl/Menu Panel/Accessories");
		// InteractableItemLayer = GetNode<Control>("/root/DressUpScene/ClothesControl/ItemControl");
	}

	public override void _Ready()
	{
		this.ProcessPriority = 1;
		GD.Print("ItemMover");
		this.ButtonDown += () => AttachAndMove();
	}

	private void AttachAndMove()
	{
		PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;
	}
	
	public override void _Process(double delta)
	{
		var currentMousePos = this.GetViewport().GetMousePosition();
		mouseMoving = oldMousePos != currentMousePos;
		oldMousePos = currentMousePos;
		
		if(ButtonPressed && mouseMoving)
		{
			GlobalPosition = currentMousePos - PosOffset;
			
			//GD.Print ("mousepos: " + currentMousePos + "\n" + "itempos: " + this.GlobalPosition);
			// if(!FolderContainer.GetRect().HasPoint(FolderContainer.GetLocalMousePosition()));
			// {
			// 	this.setNewSize();
			// 	// GD.Print("feck");
			// 	// this.ButtonPressed = true;
			// 	// this.Reparent(InteractableItemLayer);
			// }
			// if(exited)
			// {
			// 	this.Scale = new Vector2(.33f, .33f);
			// 	offset -= new Vector2(100,100);
			// 	GD.Print("moving " + this.Name);
			// 	exited = false;
			// }
		}
	}
}
