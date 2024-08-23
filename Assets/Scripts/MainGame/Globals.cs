using Godot;
using System;

namespace Dressup
{
	public partial class Globals : Node2D
	{
		[Export] public Control ClothesControl;
		[Export] public TextureRect MenuPanel;
		[Export] public Container ItemFolders;
		[Export] public bool magnetise = false;

		[Export] public TextureButton GrabbedItem {get;set;}

		// Called when the node enters the scene tree for the first time.
		public override void _EnterTree()
		{
			GrabbedItem = null;
		}

		public override void _Process(double delta)
		{
			if(GrabbedItem != null)
			{
				// GD.Print(GrabbedItem.Name);
			}
		}


		public void HandleItemClicked()
		{

		}

		public void HandleItemDropped()
		{
			
		}
	}
}
