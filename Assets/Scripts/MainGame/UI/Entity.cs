using Godot;
using System;
using System.Collections.Generic;

public partial class Entity
{
    public ItemList entity{get;set;}
    public string entity_name {get;set;}
    public string entity_file_name {get;set;}
    public ImageTexture entity_texture {get;set;}

    //public Area2D entity_area {get;set;}
    //public CollisionShape2D entity_collider {get;set;}
    //public Sprite2D entity_sprite {get;set;}
    //public Node2D entity_node{get;set;}
    //public StaticBody2D entity_body {get;set;}
    public bool clickable {get;set;}
    public int layer {get;set;}
}
