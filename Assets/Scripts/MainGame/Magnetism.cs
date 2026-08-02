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
		private Tween alphaTween;
        private Tween positionTween;
		private bool inside = false;
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			globals.ItemClicked += HandleMouseClicked;

			var area = GetChild<Area2D>(0);
			area.MouseEntered += HandleMouseEntered;
			area.MouseExited += HandleMouseExited;

            foreach (LiveItem sprite in GetSprites())
            {
                sprite.Modulate = new Color(sprite.Modulate, 0);
            }
		}
		protected void HandleMouseClicked()
        {
            if (inside && (int)(globals.GrabbedItem as LiveItem).itemType == (int)itemType)
            {
                CheckMatchingSprites();
                positionTween?.Kill();
                positionTween = null;
            }
        }

		protected void HandleMouseEntered()
        {
			if (!globals.magnetise) { return; }
			inside = true;
			if (globals.GrabbedItem == null || (int)(globals.GrabbedItem as LiveItem).itemType != (int)itemType) { return; }
            CheckMatchingSprites();
        }

        public void CheckMatchingSprites()
        {
            for (int i = 1; i < GetChildCount(); i++)
            {
                if (GetChild<Sprite2D>(i).Name == globals.GrabbedItem.genericName)
                {
                    MatchingSprite = GetChild<Sprite2D>(i);
                    globals.magnetTarget = this;
                    GD.Print("matched");
                    if (alphaTween == null)
                    {
                        alphaTween = GetTree().CreateTween();
                        ModifyAlpha();
                    }

                    break;
                }
            }
        }

        protected void HandleMouseExited()
        {
            GD.Print($"Exited {this.Name}");
            inside = false;
            KillTweens();
            globals.magnetTarget = null;
            MatchingSprite = null;
        }

        private void KillTweens()
        {
            alphaTween?.Kill();
            alphaTween = null;
            foreach (LiveItem sprite in GetSprites())
            {
                if (sprite.Modulate.A == 0) { continue; }
                Tween dim = GetTree().CreateTween();
                dim.TweenProperty(sprite, "modulate:a", 0f, .15f);
            }
        }

        public void TranslateToMagnet()
        {
            if (positionTween == null)
            {
                positionTween ??= GetTree().CreateTween();
                positionTween.SetParallel(true);
                positionTween.TweenProperty(globals.GrabbedItem, "global_position", MatchingSprite.GlobalPosition, .5f)
                                .SetTrans(Tween.TransitionType.Expo)
                                .SetEase(Tween.EaseType.Out);
                positionTween.TweenProperty(globals.GrabbedItem, "rotation", MatchingSprite.Rotation, .5f)
                                .SetTrans(Tween.TransitionType.Expo)
                                .SetEase(Tween.EaseType.Out);
                positionTween.Chain();
            }

            positionTween = null;
            MatchingSprite.Modulate = new Color(MatchingSprite.Modulate, 0f);
        }
        private void ModifyAlpha()
        {
            alphaTween.TweenProperty(MatchingSprite, "modulate:a", 0f, 1.25f)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.In);
            alphaTween.TweenProperty(MatchingSprite, "modulate:a", .33f, 1.25f)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.Out);
            alphaTween.SetLoops();
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