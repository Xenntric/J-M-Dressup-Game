using Godot;
using System;
using System.Collections.Generic;
public partial class MenuScript : Node2D
{
	[Export] public AnimationPlayer SplashAnimation; 
	[Export] public Control MenuControlNode; 
	[Export] public PackedScene MainGame;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SplashAnimation.Play("Splash_Animation");
		SplashAnimation.Queue("Menu_Animation");
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("Grab") && SplashAnimation.IsPlaying())
		{
			SplashAnimation.Seek(5.2f);
			/// Undecided whether clicking through the splash screen should speed
			/// it up or skip ahead. Will ask J at a later date

			//SplashAnimation.SpeedScale = 7.5f;
			GD.Print("Clicked");
		}
	}

	public void _on_play_pressed()
	{
		if(MenuControlNode.Modulate.A >= .5f)
		{
			var scene = ResourceLoader.Load<PackedScene>(MainGame.ResourcePath);
			GetTree().ChangeSceneToPacked(scene);
			//ResourceLoader.Load<SceneTree>();
		}
	}
	public void _on_quit_pressed()
	{
		if(MenuControlNode.Modulate.A >= .5f)
		{
			GetTree().Quit();
		}
	}
}
