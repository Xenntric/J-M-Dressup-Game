using DressupUI;
using Godot;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

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
		private bool canMagnetise;
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());

			GetChild<Area2D>(0).MouseEntered += HandleMouseEntered;
			GetChild<Area2D>(0).MouseExited += HandleMouseExited;
		}

		protected void HandleMouseEntered()
		{
			
			if(!globals.magnetise || globals.GrabbedItem == null || (int)(globals.GrabbedItem as Entity).itemType != (int)itemType)
			{
				return;
			}

			GD.Print(Name);

			GD.Print($"Handle Mouse Entered With -{globals.GrabbedItem.Name}");

			for (int i = 1; i < GetChildCount(); i++)
			{
				if(GetChild<Sprite2D>(i).Name == globals.GrabbedItem.Name)
				{
					MatchingSprite = GetChild<Sprite2D>(i);
					GD.Print($"{MatchingSprite.Name}, while holding {globals.GrabbedItem.Name}");
					tween = GetTree().CreateTween();
					tween.SetLoops().TweenProperty(MatchingSprite, "modulate:a", .33f, 1.25f)
						.SetTrans(Tween.TransitionType.Sine)
						.SetEase(Tween.EaseType.In)
						.FromCurrent();
					tween.Chain().TweenProperty(MatchingSprite, "modulate:a", 0f, 1.25f)
						.SetTrans(Tween.TransitionType.Sine)
						.SetEase(Tween.EaseType.Out);
					canMagnetise = true;
					if(!globals.GrabbedItem.IsConnected("button_up", Callable.From(HandleButtonUp)))
					{
						GD.Print($"yay, grabbed item is: {globals.GrabbedItem}");
						// GD.Print()
						globals.GrabbedItem.ButtonUp += HandleButtonUp;
					}
				}
			}
		}
		protected void HandleMouseExited()
		{
			if(!globals.magnetise)
			{
				return;
			}

			canMagnetise = false;
			if(MatchingSprite == null || globals.GrabbedItem == null)
			{
				return;
			}
			if(globals.GrabbedItem.IsConnected("button_up", Callable.From(HandleButtonUp)))
			{
				globals.GrabbedItem.ButtonUp -= HandleButtonUp;
			}
            MatchingSprite.Modulate = new Godot.Color(MatchingSprite.Modulate, 0f);
			MatchingSprite = null;
			tween.Kill();
		}

		protected void HandleButtonUp()
		{
			if(!globals.magnetise)
			{
				return;
			}

			if(MatchingSprite == null)
			{
				return;
			}
			GD.Print($"HandleButtonUp - {globals.GrabbedItem.Name}, {MatchingSprite.Name}");
			
			globals.GrabbedItem.GlobalPosition = MatchingSprite.GlobalPosition;
			HandleMouseExited();
			globals.GrabbedItem = null;
			GD.Print("begone");
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
				if(canMagnetise)
				{
					HandleButtonUp();
				}
				// grabbed = false;
				// ToggleMode = false;
				// globals.GrabbedItem = null;
			}
		}
	}
}