using Godot;
using Godot.Collections;

public partial class AnimateMenuButton : AnimateButton
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
