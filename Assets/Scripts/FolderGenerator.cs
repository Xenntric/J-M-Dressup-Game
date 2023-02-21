using Godot;
using System;
using System.Collections.Generic;

public partial class FolderGenerator : Node2D
{

	//private Node MC;
	//private Script MCS;
	bool flicked;
	//SignalScript signalScript;
	public loadItems LoadItems;
	public RichTextLabel title;
	public List<List<Sprite2D>> items;
	public List<Items> hats;
	public List<Node2D> items_nodes;
	public enum ClothesFinder {Hats,Tops,Trousers,Shoes,Props}
	[Export] ClothesFinder clothesField = ClothesFinder.Hats;
	private string clothestoFind()
	{
		switch (clothesField)
		{
			case ClothesFinder.Hats:
			return "Hats";
			case ClothesFinder.Tops:
			return "Tops";
			case ClothesFinder.Trousers:
			return "Trousers";
			case ClothesFinder.Shoes:
			return "Shoes";
			case ClothesFinder.Props:
			return "Props";
		}
		return null;
	}
	public override void _Ready()
	{
		List<Items> hats = new List<Items>(LoadItems.Construct_Hats(clothestoFind()));
		items_nodes = new List<Node2D>();
		//MC = GetNode<Node>("../MouseControl");
		//GD.Print(MC.Name);

		GD.Print("Starting to load Items");

		Node2D old_hat_bounds = new Node2D();
		var old_hat = new Tuple<Node2D,Sprite2D>(new Node2D(), new Sprite2D());
		int count = 0;

		foreach (var item in hats)
		{
			var new_hat_collider    = new CollisionShape2D();
			var new_hat_shape_rect  = new RectangleShape2D();
			var new_hat_sprite      = new Sprite2D();
			var new_hat_area        = new Area2D();
			var new_hat_node        = new Node2D();

			new_hat_node            = item.item;
			new_hat_sprite          = item.item_sprite;
			new_hat_collider        = item.item_collider;
			new_hat_area            = item.item_area;

            //new_hat_node.Position = new Vector2() + GetNode<Node2D>(this.GetPath()).Position

			AddChild(new_hat_node);
			
			items_nodes.Add(new_hat_node);

			new_hat_node.AddChild(new_hat_area);

            ulong objId = new_hat_area.GetInstanceId();
			new_hat_area.SetScript(ResourceLoader.Load("Assets/Scripts/SignalScript.cs"));
			new_hat_area = InstanceFromId(objId) as Area2D;
            new_hat_area._Ready();
            new_hat_area.SetProcessInput(true);
            new_hat_area.SetProcess(true);

			new_hat_area.AddChild(new_hat_collider);
			new_hat_area.AddChild(new_hat_sprite);

			new_hat_shape_rect.Size = new_hat_sprite.GetRect().Size;
			new_hat_collider.Shape  = new_hat_shape_rect;

			new_hat_node.Name       = item.item_name;
			new_hat_sprite.Name     = item.item_name + " Sprite";
			new_hat_area.Name       = item.item_name + " Area2D";
			new_hat_collider.Name   = item.item_name + " Collision Shape";

			//Resource script = GD.Load("Assets/Scripts/SignalScript.cs");
			
			// ulong objId = new_hat_area.GetInstanceId();
			// // Replaces old C# instance with a new one. Old C# instance is disposed.
			// new_hat_area.SetScript(ResourceLoader.Load("Assets/Scripts/SignalScript.cs"));
			// // Get the new C# instance

			// new_hat_area = InstanceFromId(objId) as Area2D;


			GD.Print("Sprite Bounds: " + new_hat_sprite.GetRect());

			if(count > 0)
			{
				new_hat_node.Position = old_hat.Item1.Position + new Vector2(old_hat.Item2.GetRect().Size.X,0);
			}

			old_hat = new Tuple<Node2D, Sprite2D>(new_hat_node, new_hat_sprite);
			count++;
		}

		GD.Print("Succeeded");

		// var ding = new Node2D();
		// ding.Name = "ding";
		// AddChild(ding);
	}
	public override void _Process(double delta)
	{

	}
}
