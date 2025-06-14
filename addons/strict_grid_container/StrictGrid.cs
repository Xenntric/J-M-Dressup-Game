using Godot;
using System;
[Tool]
public partial class StrictGrid : Container
{
	[Export] int Columns;
	[Export] public Vector2 CellSize;
	[Export] bool RatioCellSize;
	[Export] Vector2 CellSpacing;
    [Export] Vector2 CellPadding;
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

        if(ScrollingMode) /// This is stupid to be doing this all in processing. Move it to an event called when the bool is changed
        {
            
            if (!hasScrollBar) 
            {
                scrollBar ??= new VScrollBar();
                scrollBar.MouseEntered += SBMouseEntered;
                scrollBar.MouseExited  += SBMouseExited;

                AddChild(scrollBar);
                hasScrollBar = true;
            }

            scrollBar.Size = new Vector2(15, Size.Y);
            Size = new (bounds.X + 5 + scrollBar.Size.X,bounds.Y);
            scrollBar.Position = new Vector2(bounds.X + 5, CellPadding.Y);
             
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

        Control originalChild = Children[0] as Control;
        originalChild.Position = new Vector2(0,0 + ScrollPercentage) + CellPadding;
        originalChild.Size = CellSize;
        originalChild.PivotOffset = CellSize/2;

        Columns = Math.Clamp(Columns, 1, Children.Count);

        var offset = Vector2.Zero;
        int returns = 0;
        var xMultiplier = 1;

        for (int i = 1; i < Children.Count; i++)
        {
            Control child = Children[i] as Control;
            child.Size = CellSize;
            child.PivotOffset = CellSize/2;
            
            if(xMultiplier >= Columns) { xMultiplier = 0; }
            if((i % Columns) == 0) { returns += 1; }
            
            offset.X = originalChild.Position.X + ((CellSize.X + CellSpacing.X) * xMultiplier);

            
            offset.Y = originalChild.Position.Y + ((CellSize.Y + CellSpacing.Y) * returns) ;

            child.Position = offset;
            xMultiplier++;

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
