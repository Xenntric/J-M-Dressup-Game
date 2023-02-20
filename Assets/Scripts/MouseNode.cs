using Godot;
using System;

public partial class MouseNode : Node
{
    public bool clicked {get;set;}
    private Node NodeUnderMouse{get;set;}
    private bool Hovering;
    public override void _Ready()
	{
        this.AddUserSignal("mouse_entered");
        GD.Print("Signal list:" + this.GetSignalList());
        
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        
        if (@event.IsActionPressed("Grab") && Hovering)
        {
            clicked = true;

            GD.Print("Grabbed");
        }
        else if (@event.IsActionReleased("Grab"))
        {
            GD.Print("Released");

            clicked = false;
        }
    //GD.Print("Signal Conntections: " + this.GetSignalConnectionList("mouse_entered"));


        // if(@event.GetType() == Input.IsMouseButtonPressed(MouseButton.Left))
        
    }
    public override void _Process(double delta)
	{
    }

    public void Mousey()
    {
        GD.Print("Fuck your Father");

    }


    // private void _on_area_2d_mouse_entered(NodePath nodePath)
    // {

    //     //Node2D node = GetNode<>(nodePath);
        
    //     Hovering = true;
    // }
    // private void _on_area_2d_mouse_exited()
    // {
    //     Hovering = false;
    // }
    
}
