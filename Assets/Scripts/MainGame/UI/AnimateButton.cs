using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class AnimateButton : TextureButton
{
	[Export] AnimationPlayer AnimationPlayer;
	[Export] StringName Animation;
	[Export] bool Reversed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += () => CallAnimation();
	}

	private void CallAnimation()
	{
		if(Reversed)
		{
			AnimationPlayer.PlayBackwards(Animation);
		}
		else
		{
			AnimationPlayer.Play(Animation);
		}
	}
}
