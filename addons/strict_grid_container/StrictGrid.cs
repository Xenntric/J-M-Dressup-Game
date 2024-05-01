using Godot;
using System;

[Tool]
public partial class StrictGrid : Container
{
	[Export] int Columns;
	[Export] Vector2 CellSize;
	[Export] bool RatioCellSize;
	[Export] Vector2 CellSpacing;
	Godot.Collections.Array<Node> Children;
	Vector2 bounds = new();
	
    public override void _EnterTree()
    {
        base._EnterTree();
	}
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		GenerateStrictGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		if (Engine.IsEditorHint())
		{
			GenerateStrictGrid();
		}
    }

    private void GenerateStrictGrid()
    {
        if (RatioCellSize)
        {
            CellSize.Y = CellSize.X;
        }

        Children = this.GetChildren();
        Control prevChild;

        float yOffset = 0;
        float xOffset = 0;
        int returns = 1;

        for (int i = 1; i < Children.Count; i++)
        {
            prevChild = Children[i - 1] as Control;
            Control child = Children[i] as Control;
            prevChild.Size = CellSize;
            child.Size = CellSize;
            xOffset += CellSize.X + CellSpacing.X;

			if(child.Position.X > prevChild.Position.X)
			{
				bounds.X = child.Position.X + CellSize.X;
			}

            if (i % Columns == 0)
            {
                xOffset = (0);
                yOffset += CellSize.Y;
                yOffset += CellSpacing.Y;
                returns += 1;

				if (child.Position.Y > prevChild.Position.Y)
				{
					bounds.Y = child.Position.Y + CellSize.Y;
				}
            }

            child.Position = new Vector2(
                xOffset,
                yOffset);

			if (Engine.IsEditorHint())
			{
				// Running bounds related code stops working outside of editor, not sure why.
				this.Size = bounds;
			}
        }
    }
}
