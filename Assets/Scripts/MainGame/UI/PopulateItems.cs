using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;

public partial class PopulateItems : Control
{
	private TabContainer TabContainer;
	private List<ItemList> ItemLists;

	private Entity entity;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//ItemLists = new List<ItemList>();
		TabContainer = GetNode<TabContainer>(GetChild<TabContainer>(0).GetPath());
		createFolders();
		//populateFolders();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void createFolders()
	{
		foreach (var folder in Directory.GetDirectories(@"Assets/Art/Clothes/"))
		{
			GD.Print(folder);
			var newFolder = new ItemList();
				newFolder.Name =  Path.GetFileNameWithoutExtension(folder);
				newFolder.FixedIconSize = new Vector2I(75,90);
				newFolder.SameColumnWidth = true;
				newFolder.FixedColumnWidth = 75;
				newFolder.MaxColumns = 3;

			foreach (string item in Directory.GetFiles(folder, "*.png"))
			{
				var texture = (Texture2D)GD.Load(item);
				newFolder.AddIconItem(texture,true);
				
			// TabContainer.AddChild(newTab);

			}
			// var newTab = new ItemList();
			// newTab.Name =  Path.GetFileNameWithoutExtension(folder);
			TabContainer.AddChild(newFolder);
			ulong objId = newFolder.GetInstanceId();
			newFolder.SetScript(ResourceLoader.Load("Assets/Scripts/MainGame/UI/ItemSelect.cs"));
			newFolder = InstanceFromId(objId) as ItemList;
			newFolder._Ready();
		}
	}
}
