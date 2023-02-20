using Godot;
using System;

public partial class SignalScript : Area2D
{
    //private MouseControl mouseControl;

    public Node mouseControlNode;
    private MouseNode MouseNode;

    Callable mouseNodeCallable;
    public override void _Ready()
	{
        GD.Print("Signal Script Connecting");
        mouseControlNode = new Node();
        mouseControlNode = GetNode<Node>(new NodePath("/root/DressUpScene/MouseControlNode"));
        mouseNodeCallable = new Callable(mouseControlNode, "Mousey");

       // Node(MouseNode.cs)::mouse_entered()

        GD.Print(mouseControlNode.Name);
        GD.Print("CUNT");
        
        this.Connect("mouse_entered", mouseNodeCallable);
        
        // if(this.IsConnected("mouse_entered", mouseNodeCallable))
        // {
        //     GD.Print("Connected to: " + mouseControlNode.Name);
        // }
    }

    public void mouse_entered()
    {
        GD.Print("MouseEntered");
        //this.Call("mouse_entered()");

        //GD.Print(this.Name);
    }

}
