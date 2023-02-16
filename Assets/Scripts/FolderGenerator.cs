using Godot;
using System;
using System.Collections.Generic;

public partial class FolderGenerator : Node
{

    public loadItems LoadItems;

    public RichTextLabel title;
    public List<List<Sprite2D>> items;
    public List<Items> hats;
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
        GD.Print("Starting to load Items");

        foreach (var item in hats)
        {
            var new_hat_collider = new CollisionShape2D();
            var new_hat_sprite = new Sprite2D();
            var new_hat_area = new Area2D();
            new_hat_sprite = item.item_sprite;
            new_hat_collider = item.item_collider;
            new_hat_area = item.item_area;
            
            new_hat_sprite.Name     = item.item_name;
            new_hat_area.Name       = item.item_name + " Area2D";
            new_hat_collider.Name   = item.item_name + " Collision Shape";

            AddChild(new_hat_sprite);
            new_hat_sprite.AddChild(new_hat_area);
            new_hat_area.AddChild(new_hat_collider);
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
