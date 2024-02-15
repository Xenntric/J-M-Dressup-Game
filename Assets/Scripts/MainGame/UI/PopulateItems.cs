using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

public partial class PopulateItems : Control
{
	//private TextureRect ItemContainer;
	private TextureRect MasterContainer;
	private Container FolderContainer;
	private HBoxContainer Rows;
	private VBoxContainer Columns;
	private List<ItemList> ItemLists;
	private List<Texture> clothes;

	private Entity entity;
	[Export] public int AmountOfRows = 3;
	//private int itemScanned;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//ItemLists = new List<ItemList>();
		MasterContainer = GetNode<TextureRect>(GetChild<TextureRect>(0).GetPath());
		//Sulong objId = FolderContainer.GetInstanceId();
		//SFolderContainer.SetScript(ResourceLoader.Load("Assets/Scripts/MainGame/UI/ItemSelect.cs"));
		//SFolderContainer = InstanceFromId(objId) as Container;

		
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
			List<Texture2D> itemInFolder = new List<Texture2D>();
			foreach (string item in Directory.GetFiles(folder, "*.png"))
			{
				itemInFolder.Add((Texture2D) GD.Load(item));
				GD.Print(item);
			}

			int itemScanned = 0;

			FolderContainer = new Container();
			FolderContainer.Name = Path.GetFileNameWithoutExtension(folder);
			FolderContainer.Size = MasterContainer.Size;
			MasterContainer.AddChild(FolderContainer);
			Columns = new VBoxContainer();
			Columns.SetPosition(new Vector2(25,25));
			FolderContainer.AddChild(Columns);

			for (int i = 0; i < AmountOfRows; i++)
			{
				Rows = new HBoxContainer();
				Columns.AddChild(Rows);
				
				for (int j = 0; j < 3; j++)
				{
					//GD.Print(itemScanned + 1);
					//GD.Print(itemInFolder.Count());
					if(itemScanned < itemInFolder.Count())
					{
						var itemButton = new TextureButton();
						itemButton.TextureNormal = itemInFolder.ElementAt(itemScanned);
						itemButton.StretchMode = TextureButton.StretchModeEnum.KeepAspect;
						itemButton.IgnoreTextureSize = true;
						itemButton.CustomMinimumSize = new Vector2(70,70);
						Rows.AddChild(itemButton);
						itemScanned++;
					}
				}
			}
			
			// GD.Print(folder);
			// var newFolder = new ItemList();
			// 	newFolder.Name =  Path.GetFileNameWithoutExtension(folder);
			// 	newFolder.FixedIconSize = new Vector2I(75,90);
			// 	newFolder.SameColumnWidth = true;
			// 	newFolder.FixedColumnWidth = 75;
			// 	newFolder.MaxColumns = 3;
			// 	newFolder.Size = ItemContainer.Size;
			// 	newFolder.AllowSearch = false;
			// 	newFolder.AllowReselect = true;

			// 	//newFolder.Visible = false;

			foreach (string item in Directory.GetFiles(folder, "*.png"))
			{
				//var clothesContainer = new Container();
				//clothesContainer.Size = new Vector2(100,100);			

				//var texture = (Texture2D)GD.Load(item);
				//var newItem = new TextureRect();
				//newItem.Texture = texture;
				//newItem.Name = item;
				//newItem.ExpandMode = TextureRect.ExpandModeEnum.KeepSize; 
				//newItem.Size = (new Vector2(100,100));
				//newItem.StretchMode = 0;
				
				//clothesContainer.AddChild(newItem);
				////newItem.SetSize(new Vector2(100,100), false);
				//ItemContainer.AddChild(clothesContainer);
				//newFolder.AddIconItem(texture,true);
			
			}
			// // TabContainer.AddChild(newTab);

			// // }
			// // // var newTab = new ItemList();
			// // // newTab.Name =  Path.GetFileNameWithoutExtension(folder);
			//  ItemContainer.AddChild(newFolder);
			//  ulong objId = newFolder.GetInstanceId();
			//  newFolder.SetScript(ResourceLoader.Load("Assets/Scripts/MainGame/UI/ItemSelect.cs"));
			//  newFolder = InstanceFromId(objId) as ItemList;
			//  newFolder._Ready();

			// //var item = new TextureRect();
			// //item.Name = Path.GetFileNameWithoutExtension(folder);

			
		}
	}

	private void populateFolders()
	{

	}
}
