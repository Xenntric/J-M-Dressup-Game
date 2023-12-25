using Godot;
using System;
using System.Collections.Generic;
using System.IO;
public partial class MenuScript : Node2D
{
	[Export] public AnimationPlayer SplashAnimation; 
	[Export] public Control MenuControlNode; 
	[Export] public PackedScene MainGame;

	private VBoxContainer vBox;
	private bool cursorChanged = false;
	private bool splashSkipped = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SplashAnimation.Play("Splash_Animation");
		SplashAnimation.Queue("Menu_Animation");

		vBox = GetNode<VBoxContainer>(MenuControlNode.GetChild(0).GetPath());
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		if(MenuControlNode.Modulate.A >= .1f && !cursorChanged)
		{
			foreach (TextureButton child in vBox.GetChildren())
			{
				child.MouseDefaultCursorShape = Control.CursorShape.PointingHand;
			}
			cursorChanged = true;
		}
    }
    
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("Grab") && SplashAnimation.IsPlaying() && !splashSkipped)
		{
			SplashAnimation.Seek(5.2f);
			splashSkipped = true;
			
			/*
				Undecided whether clicking through the splash screen should speed
			  	it up or skip ahead. Will ask J at a later date
			*/

			//SplashAnimation.SpeedScale = 7.5f;
			GD.Print("Skipped Splash");
		}
	}

	public void _on_play_pressed()
	{
		if(MenuControlNode.Modulate.A >= .5f)
		{
			SplashAnimation.Play("Play_Transition");
		}
	}
	public void _on_animate_play_transition_finished()
	{
		var scene = ResourceLoader.Load<PackedScene>(MainGame.ResourcePath);
		GetTree().ChangeSceneToPacked(scene);
	}
	public void _on_quit_pressed()
	{
		if(MenuControlNode.Modulate.A >= .5f)
		{
			GetTree().Quit();
		}
	}
}
