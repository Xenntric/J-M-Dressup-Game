using Godot;
using System;
using System.Collections.Generic;
public partial class MouseNode : Node
{
	public bool clicked {get;set;}
	private Node NodeUnderMouse{get;set;}
	public List<Node2D> Items_nodes{get;set;}
	public List<Area2D> Items_areas{get;set;}

	public bool Hovering;
	public override void _Ready()
	{
		Items_nodes = new List<Node2D>();
		Items_areas = new List<Area2D>();

	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("Grab"))
		{
			clicked = true;

			GD.Print("Grabbed");
		}
		else if (@event.IsActionReleased("Grab"))
		{
			GD.Print("Released");

			clicked = false;
		}
	}
	public override void _Process(double delta)
	{
		if(Items_nodes.Count >= 1 && clicked)
		{
			
			//GD.Print(Items_nodes[0].Name);
			if(clicked)
			{
					var folderParent = Items_nodes[0].GetParent<Object>() as Node2D;
					//var originalPos = folderParent.Position + Items_nodes[0].Position;

					Items_nodes[0].Position = GetViewport().GetMousePosition() - folderParent.Position;
			} else {Items_nodes[0].Position = Items_nodes[0].Position;}
		}
			
		//GD.Print("Current Nodes: " + Items_nodes.Capacity);
	}

	public void addNode(Node2D node2D, Area2D area2D)
	{
		Items_nodes.Add(node2D);
	}
	public void identifyNode(string nodePath)
	{
		GD.Print(nodePath);
	}

	private void _on_area_2d_mouse_entered(NodePath nodePath)
	{		
	    Hovering = true;
	}
	private void _on_area_2d_mouse_exited()
	{
	    Hovering = false;
	}
	
}
