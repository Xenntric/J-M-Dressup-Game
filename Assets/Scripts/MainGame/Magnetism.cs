using DressupUI;
using Godot;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace Dressup
{
	public partial class Magnetism : Node
	{
		private Globals globals;
		
		protected enum ItemType {
            Shoes,
            Socks,
            Trousers,
            Dress,
            Outfit,
            Shirt,
            Headwear,
            Accessory,
        };
        [Export] ItemType itemType;
		private Sprite2D MatchingSprite;
		private Tween tween;
		private bool inside = false;

		private float amplitude;
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());

			GetChild<Area2D>(0).MouseEntered += HandleMouseEntered;
			GetChild<Area2D>(0).MouseExited += HandleMouseExited;
		}

		protected void HandleMouseEntered()
        {

            if (!globals.magnetise || globals.GrabbedItem == null || (int)(globals.GrabbedItem as Entity).itemType != (int)itemType)
            {
                return;
            }

            CheckMatchingSprites();
        }

        private void CheckMatchingSprites()
        {
            for (int i = 1; i < GetChildCount(); i++)
            {
                if (GetChild<Sprite2D>(i).Name == globals.GrabbedItem.Name)
                {
                    inside = true;
                    MatchingSprite = GetChild<Sprite2D>(i);
                    tween = GetTree().CreateTween();
                    tween.SetLoops().TweenProperty(MatchingSprite, "modulate:a", .33f, 1.25f)
                        .SetTrans(Tween.TransitionType.Sine)
                        .SetEase(Tween.EaseType.In)
                        .FromCurrent();
                    tween.Chain().TweenProperty(MatchingSprite, "modulate:a", 0f, 1.25f)
                        .SetTrans(Tween.TransitionType.Sine)
                        .SetEase(Tween.EaseType.Out);
                    break;
                }
            }
        }

        protected void HandleMouseExited()
		{
			GD.Print("Magnetism - exited");
			inside = false;
			if(MatchingSprite == null)
			{
				return;
			}
			tween?.Kill();
			Tween dim = GetTree().CreateTween();
			dim.TweenProperty(MatchingSprite, "modulate:a", 0f, .15f);
			MatchingSprite = null;
		}

		public override void _Input(InputEvent @event)
		{
			if(!globals.magnetise)
			{
				return;
			}

			base._Input(@event);

			if(@event.IsActionReleased("Grab"))
			{
				if(inside)
				{
					GD.Print(globals.GrabbedItem.Name);
					GD.Print(MatchingSprite.Name);
					var localTween = GetTree().CreateTween();
					localTween.TweenProperty(globals.GrabbedItem, "global_position", MatchingSprite.GlobalPosition, .5f)
								.SetTrans(Tween.TransitionType.Expo)
								.SetEase(Tween.EaseType.Out);
					MatchingSprite.Modulate = new Color(MatchingSprite.Modulate, 0f);
					MatchingSprite = null;
				}
				tween?.Kill();
			}
			else if (@event is InputEventMouseMotion eventMouseMotion && globals.GrabbedItem != null && inside && MatchingSprite == null)
			{	
				CheckMatchingSprites();
			}
		}
	}
}