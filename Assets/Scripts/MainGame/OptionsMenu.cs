using Godot;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dressup
{
	public partial class OptionsMenu : Control
	{
		// Called when the node enters the scene tree for the first time.
		[Export] public Godot.Collections.Array<TextureButton> Buttons;
		private Globals globals;
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			Buttons[2].Pressed += HandleMagnetClick;
		}

		private void HandleMagnetClick()
		{
			var magnetButton = Buttons[2];
			var magnetButtonDisabled = magnetButton.GetChild<TextureRect>(1);
			var tweener = GetTree().CreateTween();
			tweener.Finished += () => ToggleDisabled(magnetButton);
			magnetButton.Disabled = true;
			globals.magnetise = !globals.magnetise;

			if(magnetButtonDisabled.Scale >= new Vector2(.5f,.5f))
			{
				tweener.TweenProperty(magnetButtonDisabled, "scale", Vector2.Zero, 0.25f)
					.SetTrans(Tween.TransitionType.Back)
					.SetEase(Tween.EaseType.In);
				return;
			}

			tweener.TweenProperty(magnetButtonDisabled, "scale", Vector2.One, 0.25f)
				.SetTrans(Tween.TransitionType.Back)
				.SetEase(Tween.EaseType.Out);
		}

		private static void ToggleDisabled(TextureButton tb)
		{
			tb.Disabled = !tb.Disabled;
		}
	}
}