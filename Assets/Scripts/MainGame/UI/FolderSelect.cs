using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
public partial class FolderSelect : Control
{
	private List<TextureButton> buttons;
	
	[Export] public Control FolderParent;
	private List<Container> folders;
	private Callable clicked;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessPriority = 1;
		buttons = new List<TextureButton>();
		folders = new List<Container>();

		foreach (var child in FolderParent.GetChildren())
		{
			folders.Add(child as Container);
		}

		foreach (TextureButton child in GetChildren().Cast<TextureButton>())
		{
			GD.Print(child.Name);

			child.Pressed += () => pressedButton(child);
		}
	}

	private void pressedButton(TextureButton button)
	{
		GD.Print("pressed " + button.Name);
		foreach (var item in folders)
		{
			if(item.Name != button.Name)
			{
				item.Visible = false;
			}
			else
			{
				item.Visible = true;
			}
		}
	}
}
