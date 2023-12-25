using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

public partial class ArtTweenIn : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Curve TweenInCurve;
	[Export] public float Delay;

	[Export] private AnimatedSprite2D Portraits;
	[Export] private AnimatedSprite2D Logo;
	[Export] private Node2D Usernames;

	public List<Godot.Vector2> originPoints;
	private Godot.Vector2 screenRes;
	//private Godot.Vector2 originPoint;

	public override void _Ready()
	{
		List<Godot.Vector2>originPoints = new List<Godot.Vector2>();

		screenRes.X = GetViewport().GetVisibleRect().Size.X;
		screenRes.Y = GetViewport().GetVisibleRect().Size.Y;

		var startingY = screenRes.Y;

		

		originPoints.Add(new Godot.Vector2(Logo.Position.X,Logo.Position.Y));
		originPoints.Add(new Godot.Vector2(Usernames.Position.X,Usernames.Position.Y));
		Logo.Position	   = new Godot.Vector2(Logo.Position.X,startingY);
		Usernames.Position = new Godot.Vector2(Usernames.Position.X,startingY);
		
		
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Logo.Position.Lerp(originPoints[0],.5f);

		//Velocity = velocity;
	}
}
