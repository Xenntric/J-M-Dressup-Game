using Godot;
using Godot.Collections;

namespace Utils
{
	public partial class AnimateButton : TextureButton
	{
		[Export] public DressupUI.MenuController menuController;
		[Export] public AnimationPlayer AnimationPlayer;
		[Export] public Godot.Collections.Array<StringName> Animation;
		[Export] bool Reversed;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Pressed += () => CallAnimation(Animation[0]);
		}

		public virtual void CallAnimation(StringName animation)
		{
			if(AnimationPlayer.IsPlaying())
			{
				return;
			}

			if(Reversed)
			{
				AnimationPlayer.PlayBackwards(animation);
			}
			else
			{
				AnimationPlayer.Play(animation);
			}
		}
	}
}
