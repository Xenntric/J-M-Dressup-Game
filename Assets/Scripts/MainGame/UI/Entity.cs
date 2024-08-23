using Godot;
using System;

namespace DressupUI
{
    public partial class Entity : TextureButton
    {
        public enum ItemType {
            Shoes,
            Socks,
            Trousers,
            Dress,
            Outfit,
            Shirt,
            Headwear,
            Accessory,
        };
        [Export] public ItemType itemType;
        public void SetZIndex()
        {
            ZIndex = (int)itemType;
        }

        private static readonly Tuple<int,Godot.Vector2> inMenuScaleSize = new(1, new Godot.Vector2(70,70));
        public static TextureButton InitEntity(Texture2D itemTexture)
        {
            var newItem = new Entity {
                TextureNormal = itemTexture,
                StretchMode = StretchModeEnum.KeepAspectCentered,
                IgnoreTextureSize = true,
                CustomMinimumSize = new Godot.Vector2(70, 70),
                Size = inMenuScaleSize.Item2,
                MouseDefaultCursorShape = CursorShape.PointingHand,
                ActionMode = ActionModeEnum.Press,
                MouseFilter = MouseFilterEnum.Pass,
            };
            newItem = Utils.Scripts.AttachScript(newItem, "Assets/Scripts/MainGame/UI/ItemMover.cs");
            return newItem;
        }

        public void SetNewSize()
        {
            // var tween = GetTree().CreateTween();
            // tween.TweenProperty(GetNode(GetPath()), "size", TextureNormal.GetSize(), 0.15f)
			// 	 	 .SetTrans(Tween.TransitionType.Linear);

            // tween.Parallel().TweenProperty(GetNode(GetPath()), "scale", new Godot.Vector2(.33f,.33f), 0.15f)
            //         .SetTrans(Tween.TransitionType.Back)
            //         .SetEase(Tween.EaseType.In);
            this.Size = this.TextureNormal.GetSize();
            this.Scale = new Godot.Vector2(.33f,.33f);
        }
    }
}