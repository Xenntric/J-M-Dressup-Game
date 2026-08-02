using Godot;
using System;
using System.Linq;
using System.ComponentModel;
namespace Dressup
{
    public partial class MouseController
    {
        private readonly Globals globals;
        private Vector2 mousePos = new(0,0);
        private bool rotating;
        private LiveItem rotatingItem;
        private RichTextLabel rotationLabel;
        private Vector2 rotationLabelOffset = new(10,5);
        public MouseController(Globals globals)
        {
            this.globals = globals;
        }

        public void Input(InputEvent @event)
        {
            HandleRotation(@event);
            HandleMovement(@event);           
        }
        private void HandleMovement(InputEvent @event)
        {
            if (@event.IsActionReleased("Grab", true)) { Released(); }
            if (globals.itemStack?.Count <= 0) { return; }
            if (@event.IsActionPressed("Grab", true)) { Pressed(); }
            if (rotating) { return; }
            if (@event is InputEventMouseMotion eventMouseMotion) { Moving(eventMouseMotion); }
        }
        private void Pressed()
        {
            GD.Print(TakeStackNames());
            AttachAndMove(FindExpectedItem());
            globals.EmitSignal(nameof(globals.ItemClicked));
        }

        private void Released()
        {
            globals.EmitSignal(nameof(globals.ItemDropped));
            if (globals.trash.Inside && globals.GrabbedItem != null)
            {
                globals.trash.Flush(globals.GrabbedItem);
            }
            if (globals.magnetTarget != null)
            {
                globals.magnetTarget.TranslateToMagnet();
                GD.Print($"{globals.GrabbedItem} && {globals.trash.CheckConflictingItems(globals.GrabbedItem)}");
                globals.ItemsInPlace.Add(globals.GrabbedItem);
                var conflictingItems = globals.trash.CheckConflictingItems(globals.GrabbedItem);
                if (conflictingItems.Count > 0)
                {
                    foreach (var item in globals.trash.CheckConflictingItems(globals.GrabbedItem))
                    {
                        globals.trash.Flush(item);
                    }
                }

            }
            GD.Print(TakeStackNames());
            globals.GrabbedItem = null;
        }

        private void Moving(InputEventMouseMotion eventMouseMotion)
        {
            CleanUpItems();
            if (globals.GrabbedItem == null) { return; }
            mousePos = eventMouseMotion.Position;
            globals.GrabbedItem.GlobalPosition = mousePos - globals.GrabbedItem.PosOffset;
        }
        private void AttachAndMove(LiveItem item)
		{
			item.PosOffset = globals.GetViewport().GetMousePosition() - item.GlobalPosition;

            globals.GrabbedItem = item;
            var parent = item.GetParent<Node>();
            parent.MoveChild(item, parent.GetChildCount()); // might do this for zIndexes but now that i think to we shouldnt need to;
        }

        private LiveItem FindExpectedItem()
        {
            var expectedItem = globals.itemStack.Last();
            foreach (LiveItem item in globals.itemStack)
            {
                if (item == expectedItem) { continue; }
                if (item.GetIndex(false) > expectedItem.GetIndex(false))
                {
                    expectedItem = item;
                }
            }
            return expectedItem;
        }
        
        private void CleanUpItems() //Enter/Exit breaks with fast mouse movement and small hitareas, this is a fallback method to clear itemStack 
        {
            if (globals.GrabbedItem == null && globals.itemStack.Count > 0)
            {
                try 
                {
                    foreach (LiveItem item in globals.itemStack)
                    {
                        Vector2 mp = globals.GetViewport().GetMousePosition();
                        Vector2 itemOrigin = item.GlobalPosition - new Vector2(((item.Texture.GetWidth() * globals.getCharacterScale.X)), (item.Texture.GetHeight() * globals.getCharacterScale.Y));
                        Vector2 itemBounds = new(itemOrigin.X + (item.Texture.GetWidth() * globals.getCharacterScale.X)*2, itemOrigin.X + (item.Texture.GetHeight() * globals.getCharacterScale.Y)*2);
                        if (mp >= itemOrigin && mp <= itemBounds)
                        {
                            return;
                        }
                        globals.itemStack.Clear();
                    }
                }
                catch {
                    globals.itemStack.Clear();
                    throw new WarningException("Exiting Loop as ItemStack was Changed");
                }
            }
        }

        private string TakeStackNames()
        {
            string names = "";
            foreach (var item in globals.itemStack)
            {
                names += $"{item.Name} ";
            }
            return names;
        }
        private void HandleRotation(InputEvent @event)
        {
            if (@event.IsActionPressed("Rotate", true) && this.globals.GrabbedItem != null && rotating == false)
            {
                rotating = true;
                rotatingItem = globals.GrabbedItem.GetNode<LiveItem>(globals.GrabbedItem.GetPathTo(globals.GrabbedItem, true));
                CreateNewLabel();
            }
            
            if (@event is InputEventMouseMotion && rotating)
            {
                RotateItem();
            }

            if (@event.IsActionReleased("Rotate", true) && rotating == true)
            {
                rotating = false;
                var localTween = globals.GetTree().CreateTween();
                rotatingItem.PosOffset = new(0,0);
                localTween.TweenProperty(rotatingItem, "global_position", globals.GetViewport().GetMousePosition(), .05f)
                            .SetTrans(Tween.TransitionType.Expo)
                            .SetEase(Tween.EaseType.Out);
                globals.RemoveChild(rotationLabel);
                rotatingItem = null;
            }

            
        }

        private void CreateNewLabel()
        {
            rotationLabel ??= new RichTextLabel
            {
                CustomMinimumSize = new(75, 0),
                FitContent = true,
                ZIndex = 100
            };
            globals.AddChild(rotationLabel);
            rotationLabel.GlobalPosition = globals.GetViewport().GetMousePosition() + rotationLabelOffset;
        }
        private void RotateItem()
        {
            Vector2 itemPos = rotatingItem.GlobalPosition;
            Vector2 mp = globals.GetViewport().GetMousePosition();
            float angle = mp.AngleToPoint(itemPos);
            rotatingItem.GlobalRotation = angle;
            rotatingItem.PosOffset = globals.GetViewport().GetMousePosition() - rotatingItem.GlobalPosition;
            rotationLabel.GlobalPosition = globals.GetViewport().GetMousePosition() + rotationLabelOffset;
            string text = angle < 0 ? Mathf.RoundToInt(360 + Mathf.RadToDeg(angle)).ToString(): Mathf.RoundToInt(Mathf.RadToDeg(angle)).ToString();
            text += '°';
            rotationLabel.Text = text;
            // DrawableTexture2D drawableTexture2D = new(); need to draw a line to the point of rotation so its a little easier to tell
            // DrawLine(new Vector2(1.5f, 1.0f), new Vector2(1.5f, 4.0f), Colors.Green, 1.0f); // or it should be a shader.
            // drawableTexture2D.draw
        }
    }
}