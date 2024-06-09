using Godot;
using Godot.Collections;

namespace DressupUI
{
	public partial class AnimateMenuButton : Utils.AnimateButton
	{
		[Export] public StrictGrid Clothes;
		
		public override void CallAnimation(StringName animation)
		{
			if(AnimationPlayer.IsPlaying())
			{
				return;
			}
			this.AnimationPlayer.Play(animation);
			Clothes.Visible = true;
			menuController.MenuDepth = MenuController.MenuDepthEnum.clothes;
		}
	}
}
