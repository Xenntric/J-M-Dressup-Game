using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace DressupUI
{
	public partial class AnimateReturnButton : Utils.CallAnimateButton
	{
		[Export] public Control MenuBody;
		public override void _Ready()
		{
			base._Ready();
			Pressed += () => CallAnimation(Animation[0]);

		}
		public override void CallAnimation(StringName animation)
		{
            if(AnimationPlayer.IsPlaying())
			{
				return;
			}
			switch(menuController.MenuDepth)
			{
				case MenuController.MenuDepthEnum.main:
				{
					AnimationPlayer.PlayBackwards(Animation[0]);
					break;
				};
				case MenuController.MenuDepthEnum.clothes:
				{
					// if(AnimationPlayer.IsConnected(AnimationPlayer.SignalName.AnimationFinished, Callable.From(() => HideClothes(null))))
					// {
					// 	return;
					// }
					AnimationPlayer.AnimationFinished += HideClothes;
					AnimationPlayer.PlayBackwards(Animation[1]);
					break;
				}
			}
		}
		
		private void HideClothes(StringName animName)
		{
			GD.Print("Hide");
			if(animName == Animation[1])
			{
				foreach (StrictGrid child in MenuBody.GetChildren().Cast<StrictGrid>())
				{
					child.Visible = false;
				}
				menuController.MenuDepth = MenuController.MenuDepthEnum.main;
				AnimationPlayer.AnimationFinished -= HideClothes;
			}
		}
	}
}
