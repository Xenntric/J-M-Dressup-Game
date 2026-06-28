// using System.Linq;
using System;
using System.Security.Cryptography;
using Dressup;
using Godot;
using Utils;

namespace Dressup
{
	public partial class FolderItem : TextureButton
	{
        public enum ItemType {
            Shoes, Socks, Trousers, Dress, Outfit, Shirt, Hair, Headwear, Accessory
        };
        [Export] public ItemType itemType;
        [Export] public int zIndexOverride;
		[Export] private Control FolderContainer;
		[Export] private Node ItemLayerNode;
		[Export] private Vector2 magnetPosition;
		protected Vector2 PosOffset;
		private Vector2	TextureSize;
		private Globals globals;
        public override void _EnterTree()
        {
            // Size = new Vector2(0, 0);
            IgnoreTextureSize = true;
        }

		public override void _Ready()
		{
            if (GetNode(GetPathTo(GetTree().Root)) != this) { runChecks(); }

			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			ProcessPriority = 1;
			TextureSize = this.TextureNormal.GetSize();
			ButtonDown += SpawnNewObj;
		}

        public void SetZIndex(bool grabbed)
        {
            ZIndex = globals.GrabbedItem == null ? (zIndexOverride != 0 ? zIndexOverride : (int)itemType) : (int)itemType;
        }

        protected void SpawnNewObj()
        {
            CreateNewObj();
            globals.OutFolderItems.Add(this);
            Shrink();
        }

        private void Shrink()
        {
            CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.Zero, 0.15f)
				 .SetTrans(Tween.TransitionType.Sine)
				 .SetEase(Tween.EaseType.In);
        }
        private void CreateNewObj()
        {
            var menumidpoint = this.Position + Size / 2;
            PackedScene seed = Finder.FindClothesScene(this);
            LiveItem copy = seed.Instantiate<LiveItem>();
            copy.TestMode = false;
            ItemLayerNode.AddChild(copy);
            copy.genericName = this.Name;
            GD.Print("spawning: ", copy.genericName);
            copy.Name = Name;
            copy.FolderContainer = FolderContainer;
            copy.itemType = itemType;
            copy.globals = globals;
            copy.GlobalPosition = this.GetViewport().GetMousePosition();
            copy.PosOffset = this.GetViewport().GetMousePosition() - copy.GlobalPosition;
            globals.GrabbedItem = copy;

            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(globals.GrabbedItem, "scale", Vector2.One, .5f)
								.SetTrans(Tween.TransitionType.Expo)
								.SetEase(Tween.EaseType.Out);
        }

        private void runChecks()
        {
            if (this.FolderContainer == null) { GD.PushError("FolderContainer not found for " + GetNode<FolderItem>(GetPathTo(this)).Name); }
            if (this.ItemLayerNode == null) { GD.PushError("ItemLayerNode not found for " + GetNode<FolderItem>(GetPathTo(this)).Name); }
        }
    }
}