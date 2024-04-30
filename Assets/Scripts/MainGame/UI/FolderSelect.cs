using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
public partial class FolderSelect : Control
{
	[Export] public Control FolderParent;
	private List<Container> folders;
	private Callable clicked;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessPriority = 1;
		GD.Print("Folder Select");
		folders = new List<Container>();

		foreach (var child in FolderParent.GetChildren())
		{
			folders.Add(child as Container);
		}

		foreach (TextureButton child in GetChildren().Cast<TextureButton>())
		{
			GD.Print("Binding event for " + child.Name);

			child.Pressed += () => PressedButton(child);
		}
	}

	private void PressedButton(TextureButton button)
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
