using Godot;
using System;
using System.Linq;

[Tool]
public partial class StrictGrid : Container
{
	[Export] int Columns;
	[Export] Vector2 CellSize;
	[Export] bool RatioCellSize;
	[Export] Vector2 CellSpacing;
	Godot.Collections.Array<Node> Children;
	Vector2 bounds = new();
    [Export] bool ScrollingMode;

    private float ScrollPercentage;

    private ScrollBar scrollBar;
    private bool hasScrollBar;
    private bool scrolling;
    public bool Reorder;

	
    public override void _EnterTree()
    {
        base._EnterTree();
	}
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        if (GetChildren().Count > 0 )
        {
		    GenerateStrictGrid();
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        ClipContents = ScrollingMode;

		if (GetChildren().Count > 0 && Engine.IsEditorHint() || scrolling)
		{
			GenerateStrictGrid();
		}

        if(ScrollingMode)
        {
            
            if (!hasScrollBar)
            {
                scrollBar ??= new VScrollBar();
                scrollBar.MouseEntered += SBMouseEntered;
                scrollBar.MouseExited  += SBMouseExited;

                AddChild(scrollBar);
                hasScrollBar = true;
            }

            scrollBar.Size = new Vector2(20, Size.Y);
            Size = new (bounds.X + scrollBar.Size.X,Size.Y);
            scrollBar.Position = new Vector2(bounds.X + 5, 0);
            
            scrollBar.MaxValue = scrollBar.Size.Y;
            ScrollPercentage = -Convert.ToSingle(scrollBar.Value * scrollBar.MaxValue / 100  *.4);
        }
        else
        {
            if(hasScrollBar)
            {
                scrollBar.MouseExited  -= SBMouseExited;
                scrollBar.MouseEntered -= SBMouseEntered;
                this.RemoveChild(scrollBar);
                hasScrollBar = false;
            }
        }
    }

    private void GenerateStrictGrid()
    {
        if (RatioCellSize)
        {
            CellSize.Y = CellSize.X;
        }

        Children = ReturnAcceptableChildren();

        (Children[0] as Control).Position = new Vector2(0,0 + ScrollPercentage);

        Control prevChild;
        Columns = Math.Clamp(Columns, 1, Children.Count);

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

            if (i % Columns == 0)
            {
                xOffset = (0);
                yOffset += CellSize.Y;
                yOffset += CellSpacing.Y;
                returns += 1;
            }

            child.Position = new Vector2(
                xOffset,
                yOffset + ScrollPercentage);

            bounds = new Vector2((Children[Columns - 1] as Control).Position.X + CellSize.X, (Children[^1] as Control).Position.Y + CellSize.Y);

            if (Engine.IsEditorHint() && !ScrollingMode)
            {
                // Running bounds related code stops working outside of editor, not sure why.
                this.Size = bounds;
            }
        }
    }

    private Godot.Collections.Array<Node> ReturnAcceptableChildren()
    {
        Godot.Collections.Array<Node> array = new();

        foreach (var item in GetChildren())
        {
            if(item is ScrollBar)
            {
                break;
            }

            array.Add(item);
        }

        return array;
    }

    private void SBMouseEntered()
    {
        scrolling = true;
    }
    private void SBMouseExited()
    {
        scrolling = false;
    }
}
