using Godot;
using System;
using System.Collections.Generic;
public partial class MouseNode : Node
{
	public bool clicked {get;set;}
	public bool grabbed {get;set;}

	private bool handsFull {get;set;}
	private Node NodeUnderMouse{get;set;}
	public List<Node2D> Items_nodes{get;set;}
	public List<Area2D> Items_areas{get;set;}
	public List<Node2D> Zindex{get;set;}
	public bool Hovering;
	public int zIndexCount = 0;
	private Node2D nodeToGrab;
	private Sprite2D nodeSprite;
	public override void _Ready()
	{
		Items_nodes = new List<Node2D>();
		Items_areas = new List<Area2D>();
		Zindex = new List<Node2D>();
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("Grab"))
		{
			clicked = true;

			GD.Print("Clicked");
		}
		else if (@event.IsActionReleased("Grab"))
		{
			GD.Print("Released");
			clicked		= false;
			handsFull	= false;
			grabbed		= false;
			if(nodeToGrab != null){nodeSprite.Material = new CanvasItemMaterial();}
		}
	}
	public override void _Process(double delta)
	{
		//GD.Print("Hands Full: " + handsFull);
		//GD.Print("Clicked: " + clicked);

		if(Items_nodes.Count > 0)
		{
			if(nodeToGrab != null){nodeSprite.Material = new CanvasItemMaterial();}
            //GD.Print("NUMB OF NODES UNDER " + Items_nodes.Count);

            if (Items_nodes.Count > 1)
            {
                for (int i = 1; i < Items_nodes.Count; i++)
                {
                    if (Items_nodes[i].ZIndex > Items_nodes[i - 1].ZIndex && !handsFull)
                    {
                        nodeToGrab = Items_nodes[i];
                    }
				}
            }
            else { nodeToGrab = Items_nodes[0]; }
			highlightNode();

            if (clicked)
            {
                handsFull = true;
                GD.Print("YA GOT FULL HANDS");
                if (nodeToGrab != null)
                {
                    /** SET GRABBED OBJECT TO GREATEST Z VALUE**/
                    for (int i = 0; i < Zindex.Count; i++)
                    {
                        if (Zindex[i] == nodeToGrab)
                        {
                            var nodeToPop = Zindex[i];
                            Zindex.Remove(Zindex[i]);
                            Zindex.Add(nodeToPop);
                            for (int j = 0; j < Zindex.Count; j++)
                            {
                                Zindex[j].ZIndex = j;
                            }
                        }
                    }
                }
                var folderParent = nodeToGrab.GetParent<Object>() as Node2D;
                //var originalPos = folderParent.Position + Items_nodes[0].Position;
                nodeToGrab.Position = GetViewport().GetMousePosition() - folderParent.Position;
            }
        } else {if(nodeToGrab != null){nodeSprite.Material = new CanvasItemMaterial();} }
    }

	public void identifyNode(string nodePath)
	{
		GD.Print(nodePath);
	}
	public void addToZIndex(Node2D node)
	{
		node.ZIndex = zIndexCount;
		Zindex.Add(node);
		zIndexCount++;
	}
	private void highlightNode()
	{
			nodeSprite = nodeToGrab.GetChild(0).GetChild<Sprite2D>(1);
			GD.Print(nodeSprite.Name);
			var newCanvasMat = new CanvasItemMaterial();
			newCanvasMat.BlendMode = CanvasItemMaterial.BlendModeEnum.PremultAlpha;
			nodeSprite.Material = newCanvasMat;
	}
}
