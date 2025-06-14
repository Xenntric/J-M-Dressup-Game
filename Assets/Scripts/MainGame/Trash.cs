using Dressup;
using DressupUI;
using Godot;

public partial class Trash : Node
{
    private LiveItem liveItem;
    private Globals globals;
    private bool inside = false;

    public override void _Ready()
    {
        globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());

        GetChild<Area2D>(0).MouseEntered += HandleMouseEntered;
        GetChild<Area2D>(0).MouseExited += HandleMouseExited;
    }

    protected void HandleMouseEntered()
    {
        inside = true;
    }
    protected void HandleMouseExited()
    {
        inside = false;
    }

    public override void _Input(InputEvent @event)
    {
        if(!inside || globals.GrabbedItem == null){return;}
        base._Input(@event);
        if(@event.IsActionReleased("Grab")                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             )
        {
            LiveItem grabbedItem = globals.GrabbedItem;
            grabbedItem.PivotOffset = grabbedItem.Size/2;
            Tween removalTween = CreateTween().SetParallel(true);
                removalTween.TweenProperty(grabbedItem, "scale", Vector2.Zero, 0.5f)
				 .SetTrans(Tween.TransitionType.Sine)
				 .SetEase(Tween.EaseType.In)
                 .Finished += () => FreeObj(grabbedItem);
                removalTween.TweenProperty(grabbedItem, "rotation", 720, 1f);
                removalTween.Chain();
        }
    }

    private void MatchToFolderItem(Node item)
    {
        foreach (var obj in globals.OutFolderItems)
        {
            if(item.Name == obj.Name)
            {
                globals.OutFolderItems.Remove(obj);

                Tween createFolderObj = CreateTween().SetParallel(false);
                createFolderObj.TweenProperty(obj, "scale", Vector2.One, 0.3f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.Out);
                // createFolderObj.TweenProperty(obj, "scale", Vector2.One, 0.65f)
				//  .SetTrans(Tween.TransitionType.Circ)
				//  .SetEase(Tween.EaseType.Out);
                createFolderObj.Chain();
                break;
            }
        }
    }

    private void FreeObj(Node node)
    {
        MatchToFolderItem(node);

        node.GetNode<LiveItem>(node.GetPath()).Free();
    }
}
