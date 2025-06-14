// using System.Linq;
using Dressup;
using Godot;
using Utils;

namespace DressupUI
{
	public partial class FolderItem : TextureButton
	{
        public enum ItemType {
            Shoes, Socks, Trousers,
            Dress, Outfit, Shirt,
            Headwear, Accessory,
        };
        [Export] public ItemType itemType;
		[Export] private Control FolderContainer;
		[Export] private Node ItemLayerNode;
		[Export] private Vector2 magnetPosition;
		protected Vector2 PosOffset;
		private Vector2	TextureSize;
		private Globals globals;

		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			ProcessPriority = 1;
			TextureSize = this.TextureNormal.GetSize();
			ButtonDown += SpawnNewObj;
		}

        public void SetZIndex()
        {
            ZIndex = (int)itemType;
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

            GD.Print(copy, ItemLayerNode);
            ItemLayerNode.AddChild(copy);
            copy.Name = Name;
            copy.FolderContainer = FolderContainer;
            copy.magnetPosition = magnetPosition;
            copy.itemType = itemType;
            copy.globals = globals;
            copy.SetZIndex();
            copy.SetSize(TextureSize);

            copy.Position = menumidpoint - Size * .33f / 2;
            copy.GlobalPosition = this.GlobalPosition - copy.Size*.33f/2 + Size/2;
            copy.PosOffset = this.GetViewport().GetMousePosition() - copy.GlobalPosition;
            copy.ToggleMode = true;
            globals.GrabbedItem = copy;
            copy.ButtonPressed = true;
        }
    }
}