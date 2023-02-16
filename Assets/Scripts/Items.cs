using Godot;
using System;
using System.Collections.Generic;

public partial class Items
{
    public Node2D item;
    public string item_name {get;set;}
    public string item_file_name {get;set;}
    public Area2D item_area {get;set;}
    public CollisionShape2D item_collider {get;set;}
    public Sprite2D item_sprite {get;set;}
    public bool clickable {get;set;}
    public int layer {get;set;}
}
