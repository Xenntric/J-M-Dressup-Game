using System;
using Dressup;
using Godot;

namespace DressupUI
{
	public partial class LiveItem : Sprite2D
	{
        [Export] public FolderItem.ItemType itemType;
        [Export] protected bool TestMode = false;
		public Globals globals {get;set;}
        public Control FolderContainer {get;set;}
		public Node ItemLayerNode {get;set;}

		private Vector2	TextureSize;
        public Vector2 PosOffset;

        private Area2D area;

        private bool inside;
        private bool grabbed;
        public override void _EnterTree()
        {
            if (!TestMode)
            {
                Scale = new Vector2(0, 0);
            }
            this.ZIndex = (int)itemType;
        }

        public override void _Ready()
		{
            if (!TestMode)
            {
			    globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
            }

			ProcessPriority = 1;
            area = GetNode<Area2D>(GetChild(0).GetPath());
            area.InputPickable = true;
            area.MouseEntered += HandleMouseEntered;
            area.MouseExited += HandleMouseExited;
            // IgnoreTextureSize = true;
            // TextureSize = this.TextureNormal.GetSize();
            // this.ButtonDown += AttachAndMove;
        }

		private void AttachAndMove()
		{
			PosOffset = GetViewport().GetMousePosition() - this.GlobalPosition;

            if (TestMode) { return; }
            globals.GrabbedItem = this;
            this.GetParent<Node>().MoveChild(this, 0); // might do this for zIndexes but now that i think to we shouldnt need to;
            // ButtonPressed = true;
            // SetZIndex(true);
        }

        private void HandleMouseEntered() 
        {
            inside = true;
            GD.Print(inside);
        }
		private void HandleMouseExited() 
        {
            inside = false;
            GD.Print(inside);
        }

        public override void _UnhandledInput(InputEvent @event)
		{
			// if (GetParent() is StrictGrid){ return; }
			base._Input(@event);
			if (@event.IsActionReleased("Grab")                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             )
			{
                if (!TestMode)
                {
                    globals.GrabbedItem = null;
                }
                // ToggleMode = false;
                // SetZIndex(false);
            }
			// else if (@event is InputEventMouseMotion eventMouseMotion && ButtonPressed)
			{
				// GlobalPosition = eventMouseMotion.Position - PosOffset;
			}
		}
	}
}