using Godot;
using System;

public partial class SignalScript : Area2D
{
	public bool clicked {get;set;}
	public bool Hovering;
	Callable mouseSignalOn;
	Callable mouseSignalOff;

	Callable pingNode;

	Node2D thisParent;
	Node2D folderParent;

	Vector2 originalPos;

	MouseNode MN;
	public override void _Ready()
	{
		MN = GetNode<MouseNode>(new NodePath("/root/DressUpScene/MouseControlNode"));
		
        thisParent = this.GetParent<Node2D>();
		folderParent = thisParent.GetParent<Object>() as Node2D;
		originalPos = folderParent.Position + thisParent.Position;

		// GD.Print("This Parent: "+ thisParent.Name +"\n" + "This Folder: " + folderParent.Name);
		// GD.Print("Signal Script Connecting");
	    mouseSignalOn = new Callable(this, "mouse_on");
	    mouseSignalOff = new Callable(this, "mouse_off");
		// pingNode = new Callable(GetNode<Node>(new NodePath("/root/DressUpScene/MouseControlNode")), "identifyNode");

		this.Connect("mouse_entered",   mouseSignalOn);
		this.Connect("mouse_exited",    mouseSignalOff);
		// this.Connect("mouse_entered",   pingNode);
        // EmitSignal("mouse_entered", this.GetPath());

		//MN.addNode(thisParent, null);
	}

	public void mouse_on()
	{
		//thisParent.ZIndex = 1;
        MN.Items_nodes.Add(thisParent);
	}
	public void mouse_off()
	{
		//thisParent.ZIndex = 0;
        MN.Items_nodes.Remove(thisParent);
	}
}
