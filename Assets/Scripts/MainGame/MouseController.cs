using Godot;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Dressup
{
    public partial class MouseController
    {
        private readonly Globals globals;
        private Vector2 mousePos = new(0,0);
        public MouseController(Globals globals)
        {
            this.globals = globals;
        }

        public void Input(InputEvent @event)
        {
            if (@event.IsActionReleased("Grab", true))
            {
                Released();
            }
            if (globals.itemStack?.Count <= 0) { return; }
            if (@event.IsActionPressed("Grab", true))
            {
               Pressed();
            }

            if (@event is InputEventMouseMotion eventMouseMotion)
            {
                Moving(eventMouseMotion);
            }
        }

        private void Pressed()
        {
            AttachAndMove(FindExpectedItem());
        }

        private void Released()
        {

            if (globals.trash.Inside && globals.GrabbedItem != null)
            {
                globals.trash.Flush();
            }
            else if (globals.magnetTarget != null)
            {
                globals.magnetTarget.translateToMagnet();
            }
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
                        if ( mp >= itemOrigin && mp <= itemBounds)
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
    }
}