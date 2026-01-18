using Dressup;
using Godot;

namespace DressupUI
{
	public partial class LiveItem : FolderItem
	{
		public Control FolderContainer {get;set;}
		public Node ItemLayerNode {get;set;}
		private Vector2	TextureSize;
		private Globals globals;
        private bool grabbed;
        public override void _EnterTree()
        {
            Size = new Vector2(0, 0);
        }

        public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			ProcessPriority = 1;
            IgnoreTextureSize = true;
            TextureSize = this.TextureNormal.GetSize();
			this.ButtonDown += AttachAndMove;
		}

		private void AttachAndMove()
		{
			PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;
            globals.GrabbedItem = this;
			ButtonPressed = true;
            SetZIndex(true);
        }

        public override void _UnhandledInput(InputEvent @event)
		{
			if (GetParent() is StrictGrid){ return; }
			base._Input(@event);
			if (@event.IsActionReleased("Grab")                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             )
			{
				ToggleMode = false;
				globals.GrabbedItem = null;
                SetZIndex(false);
            }
			else if (@event is InputEventMouseMotion eventMouseMotion && ButtonPressed)
			{
				GlobalPosition = eventMouseMotion.Position - PosOffset;
			}
		}
	}
}