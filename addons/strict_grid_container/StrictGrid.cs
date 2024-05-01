using Godot;
using System;

[Tool]
public partial class StrictGrid : Container
{
	[Export] int Columns;
	[Export] Vector2I CellSize;
	[Export] bool RatioCellSize;
	[Export] Vector2 CellSpacing;
	[Export] Godot.Collections.Array<Godot.Node> Children;
    public override void _EnterTree()
    {
        base._EnterTree();
	}
    // Called when the node enters the scene tree for the first time.
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (RatioCellSize)
		{
			CellSize.Y = CellSize.X;
		}
		
		Children = this.GetChildren();
		Control prevChild;
		float yOffset = 0;
		float xOffset = 0;
		for (int i = 1; i < Children.Count; i++)
		{

			prevChild = Children[i-1] as Control;
			Control child = Children[i] as Control;
			prevChild.Size = CellSize;
			child.Size = CellSize;
			xOffset += CellSize.X + CellSpacing.X;
			if (i % Columns == 0)
			{
				xOffset = (0);
				yOffset += CellSize.Y;
				yOffset += CellSpacing.Y;
			}
			child.Position = new Vector2(
				xOffset, 
				yOffset);
			
			this.Size = new Vector2(xOffset, yOffset);

		}
	}
}
