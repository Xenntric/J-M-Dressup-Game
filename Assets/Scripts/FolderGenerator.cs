using Godot;
using System;
using System.Collections.Generic;

public partial class FolderGenerator : Node
{

    //private Node MC;
    //private Script MCS;
    bool flicked;
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

        foreach (var item in hats)
        {
            var new_hat_collider    = new CollisionShape2D();
            var new_hat_shape_rect  = new RectangleShape2D();
            var new_hat_sprite      = new Sprite2D();
            var new_hat_area        = new Area2D();
            //var new_hat_body        = new StaticBody2D();
            var new_hat_node        = new Node2D();

            new_hat_node            = item.item;
            new_hat_sprite          = item.item_sprite;
            new_hat_collider        = item.item_collider;
            new_hat_area            = item.item_area;
            //new_hat_body            = item.item_body;


            AddChild(new_hat_node);
            items_nodes.Add(new_hat_node);


            new_hat_node.AddChild(new_hat_area);
            new_hat_area.AddChild(new_hat_collider);
            new_hat_area.AddChild(new_hat_sprite);

            //new_hat_area.InputPickable  = true;
            //new_hat_area.CollisionLayer = 1;
            
            new_hat_shape_rect.Size = new_hat_sprite.GetRect().Size;
            new_hat_collider.Shape  = new_hat_shape_rect;
            //new_hat_collider.GlobalTransform = new Transform2D(new_hat_sprite.Transform.X, new_hat_sprite.Transform.Y, Vector2.Zero);
            
            

            //new_hat_area.ShapeOwnerAddShape(new_hatInstanceFromId(new_hat_collider.GetInstanceId()),  new_hat_collider.Shape);


            new_hat_node.Name       = item.item_name;
            new_hat_sprite.Name     = item.item_name + " Sprite";
            new_hat_area.Name       = item.item_name + " Area2D";
            new_hat_collider.Name   = item.item_name + " Collision Shape";
            //new_hat_collider.Shape  = new Rect2(new_hat_sprite.Transform.X, new_hat_sprite.Transform.Y);


            //new_hat_area.ShapeOwnerAddShape(new_hat_collider.Shape.GetInstanceId(),new Rect2( Vector2.Zero, new_hat_sprite.GetRect().));
            //new_mouse_signal += () => GD.Print("Damn");


            //var colour = new Color("b2d90a");
            //new_hat_collider.Shape.Draw(new_hat_collider.Shape._GetRid(),colour);
            //new_hat_sprite.DrawRect(new_hat_sprite.GetRect(),colour,true);
            //new_hat_area.AddUserSignal("mouse_entered()",null);

            GD.Print("Sprite Bounds: " + new_hat_sprite.GetRect());
            //new_hat_collider.DrawRect(new_hat_sprite.GetRect(),colour,true);

        }

        GD.Print("Succeeded");

        // var ding = new Node2D();
        // ding.Name = "ding";
        // AddChild(ding);
	}
    public override void _Process(double delta)
	{

        foreach (var item in items_nodes)
        {



        }
	}
}
