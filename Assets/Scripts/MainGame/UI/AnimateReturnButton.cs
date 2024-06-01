using System;
using Godot;
using Godot.Collections;

public partial class AnimateReturnButton : AnimateButton
{
	[Export] public Control MenuBody;

    public override void _Ready()
    {
        base._Ready();
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
			foreach (StrictGrid child in MenuBody.GetChildren())
			{
				child.Visible = false;
			}
			menuController.MenuDepth = MenuController.MenuDepthEnum.main;
			AnimationPlayer.AnimationFinished -= HideClothes;

		}
    }
}

