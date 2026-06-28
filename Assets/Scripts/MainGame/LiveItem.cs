using System;
using System.Linq;
using Dressup;
using DressupUI;
using Godot;

namespace Dressup
{
	public partial class LiveItem : Sprite2D
	{
        [Export] public FolderItem.ItemType itemType;
        [Export] public bool TestMode = false;
		public Globals globals {get;set;}
        public Control FolderContainer {get;set;}
		public Node ItemLayerNode {get;set;}
		private Vector2	TextureSize;
        public Vector2 PosOffset;
        private Area2D area;
        public bool inside;
        private bool grabbed;

        public string genericName;
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

			ProcessPriority = 0;
            area = GetNode<Area2D>(GetChild(0).GetPath());
            area.InputPickable = true;
            area.MouseEntered += HandleMouseEntered;
            area.MouseExited += HandleMouseExited;
        }
        private void HandleMouseEntered() 
        {
            if (TestMode) { return; }
            inside = true;
            globals.PushItem(this);
        }
		private void HandleMouseExited() 
        {
            if (TestMode) { return; }
            inside = false;
            globals.PopItem(this);
        }
	}
}