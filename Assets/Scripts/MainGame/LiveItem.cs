using System.Linq;
using Dressup;
using DressupUI;
using Godot;
using Godot.Collections;
namespace Dressup
{
	public partial class LiveItem : Sprite2D
	{
        public enum Doll { Julius, Matt, none };
        [Export] public Doll doll;
        [Export] public FolderItem.ItemType itemType;
        [Export] public Array<FolderItem.ItemType> itemSlots = [];
        [Export] public bool TestMode = false;
		public Globals globals {get;set;}
        public Control FolderContainer {get;set;}
		public Node ItemLayerNode {get;set;}
        public string genericName;
        public Vector2 PosOffset;
        private Area2D area;
		private Vector2	TextureSize;
        public bool inside;
        private bool grabbed;
        public override void _EnterTree()
        {
            if (!TestMode)
            {
                Scale = new Vector2(0, 0);
            }
            if (itemSlots.Count == 0)
            {
                GD.Print($"defaulting itemType for slots {itemType}");
                itemSlots.Add(itemType);
            }
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

        private void SetZIndex()
        {
            switch(itemType)
            {
                case FolderItem.ItemType.Dress:    { ZIndex = 1; break; }
                case FolderItem.ItemType.Shirt:    { ZIndex = 1; break; }
                case FolderItem.ItemType.Outfit:   { ZIndex = 1; break; }
                default:{ ZIndex = 0; break; }
            }
        }
	}
}