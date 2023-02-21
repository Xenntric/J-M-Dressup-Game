using Godot;
using System;

public partial class SignalScript : Area2D
{
    public bool clicked {get;set;}
	public bool Hovering;
    Callable mouseSignalOn;
    Callable mouseSignalOff;
    Node2D thisParent;
    Node2D folderParent;

    Vector2 originalPos;

    public override void _Ready()
	{
        thisParent = this.GetParent<Node2D>();
        folderParent = thisParent.GetParent<Object>() as Node2D;
        originalPos = folderParent.Position + thisParent.Position;

        GD.Print("This Parent: "+ thisParent.Name +"\n" + "This Folder: " + folderParent.Name);
        GD.Print("Signal Script Connecting");
        mouseSignalOn = new Callable(this, "mouse_on");
        mouseSignalOff = new Callable(this, "mouse_off");
        this.Connect("mouse_entered",   mouseSignalOn);
        this.Connect("mouse_exited",    mouseSignalOff);
    }
    public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("Grab") && Hovering)
		{
			clicked = true;
			GD.Print("Grabbed: " + this.Name + " @ Z index: " + this.ZIndex);
		}
		else if (@event.IsActionReleased("Grab") && Hovering)
		{
			GD.Print("Released");

			clicked = false;
		}
	}

    public void mouse_on()
    {
        Hovering = true;
    }
    public void mouse_off()
    {
        Hovering = false;
    }
    public override void _Process(double delta)
	{
		if(clicked)
        {
            //GD.Print("mouse pos: " + GetViewport().GetMousePosition());
            thisParent.Position = GetViewport().GetMousePosition() - (originalPos);
        }
        else{this.Position = this.Position;}
	}

}
