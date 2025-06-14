using DressupUI;
using Godot;
using System;
using System.Collections.ObjectModel;

namespace Dressup
{
	public partial class Globals : Node2D
	{
		[Export] public Control ClothesControl;
		[Export] public TextureRect MenuPanel;
		[Export] public Container ItemFolders;
		[Export] public bool magnetise = false;
		[Export] public LiveItem GrabbedItem {get;set;}
		[Export] public TextureButton Magnetise {get;set;}

		public Godot.Collections.Array<FolderItem> OutFolderItems {get;set;}

		// Called when the node enters the scene tree for the first time.
		public override void _EnterTree()
		{
			OutFolderItems = new Godot.Collections.Array<FolderItem>();
			GrabbedItem = null;
		}

		public void HandleItemClicked()
		{

		}

		public void HandleItemDropped()
		{
			
		}
	}
}
