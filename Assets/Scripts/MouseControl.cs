using Godot;
using System;

public partial class MouseControl : Node
{
    public override void _Process(double delta)
	{
        
	}
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        
        if (@event.IsActionPressed("Grab"))
        {
            
            GD.Print("Ding");
        }



        // if(@event.GetType() == Input.IsMouseButtonPressed(MouseButton.Left))
        
    }
    
}
