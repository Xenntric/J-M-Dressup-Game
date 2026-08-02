using Dressup;
using DressupUI;
using Godot;
using System;
using System.Collections.Generic;

public partial class Trash : Node
{
    private Globals globals;
    private bool inside = false;
    public bool Inside { get { return inside; } }

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

    public void Flush(LiveItem item)
    {
        globals.ItemsInPlace.Remove(item);
        Tween removalTween = CreateTween().SetParallel(true);
        removalTween.TweenProperty(item, "scale", Vector2.Zero, 0.5f)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.In)
            .Finished += () => FreeObj(item);
        removalTween.TweenProperty(item, "rotation", 720, 1f);
        removalTween.Chain();
    }

    public void Poof(LiveItem item)
    {

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

    public List<LiveItem> CheckConflictingItems(LiveItem newItem)
    {
        List<LiveItem> conflictingItems = [];
        foreach (FolderItem.ItemType newItemSlot in newItem.itemSlots)
        {
            foreach (LiveItem outItem in globals.ItemsInPlace)
            {
                if (newItem.Name == outItem.Name) { continue; }
                foreach (FolderItem.ItemType outItemSlot in outItem.itemSlots)
                {
                    if (newItemSlot == outItemSlot && newItem.doll == outItem.doll)
                    {
                        conflictingItems.Add(outItem);
                        GD.Print($"found conflicting item {newItem.Name}, {outItem.Name}");
                        GD.Print($"conflicting slots {newItemSlot}, {outItemSlot}");

                        break;
                    }
                }
            }
        }

        return conflictingItems;
    }

    public void CleanFlushedItems()
    {
        
    }
}
