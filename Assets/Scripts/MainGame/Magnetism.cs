using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DressupUI;
using Godot;

namespace Dressup
{
	public partial class Magnetism : Node
	{
		private Globals globals;
        [Export] FolderItem.ItemType itemType;
		private Sprite2D MatchingSprite;
		private Tween tween;
		private bool inside = false;
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			
			var area = GetChild<Area2D>(0);
			area.MouseEntered += HandleMouseEntered;
			area.MouseExited += HandleMouseExited;

            foreach (LiveItem sprite in GetSprites())
            {
                sprite.Modulate = new Color(sprite.Modulate, 0);
            }
		}

		protected void HandleMouseEntered()
        {
			if (!globals.magnetise) { return; }
			inside = true;
			if (globals.GrabbedItem == null || (int)(globals.GrabbedItem as LiveItem).itemType != (int)itemType) { return; }
            CheckMatchingSprites();
        }

        private void CheckMatchingSprites()
        {
            for (int i = 1; i < GetChildCount(); i++)
            {
                if (GetChild<Sprite2D>(i).Name == globals.GrabbedItem.genericName)
                {
                    MatchingSprite = GetChild<Sprite2D>(i);
                    globals.magnetTarget = this;
                    tween = GetTree().CreateTween();
                    ModifyAlpha();
                    break;
                }
            }
        }

        protected void HandleMouseExited()
		{
			inside = false;
			if (MatchingSprite == null) { return; }
            globals.magnetTarget = null;
			tween?.Kill();
			Tween dim = GetTree().CreateTween();
			dim.TweenProperty(MatchingSprite, "modulate:a", 0f, .15f);
			MatchingSprite = null;
		}

        public void TranslateToMagnet()
        {
            var localTween = GetTree().CreateTween();
                localTween.SetParallel(true);
                localTween.TweenProperty(globals.GrabbedItem, "global_position", MatchingSprite.GlobalPosition, .5f)
                            .SetTrans(Tween.TransitionType.Expo)
                            .SetEase(Tween.EaseType.Out);
                localTween.TweenProperty(globals.GrabbedItem, "rotation", MatchingSprite.Rotation, .5f)
                .SetTrans(Tween.TransitionType.Expo)
                .SetEase(Tween.EaseType.Out);
                localTween.Chain();

            MatchingSprite.Modulate = new Color(MatchingSprite.Modulate, 0f);
        }
        private void ModifyAlpha()
        {
            tween.TweenProperty(MatchingSprite, "modulate:a", 0f, 1.25f)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.Out);
            tween.TweenProperty(MatchingSprite, "modulate:a", .33f, 1.25f)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.In);
            tween.SetLoops();
        }

        private List<LiveItem> GetSprites()
        {
            List<LiveItem> children = [];
            foreach (var child in GetChildren(true))
            {
                if (child is LiveItem) 
                { 
                    children.Add(child as LiveItem);
                }
            }
            return children;
        }
	}
}