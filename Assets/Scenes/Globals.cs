using Godot;
using System;

namespace Dressup
{
	public partial class Globals : Node2D
	{
		[Export] public Control ClothesControl;
		[Export] public TextureRect MenuPanel;
		[Export] public Container ItemFolders;

		[Export] public bool magnetise;


		// Called when the node enters the scene tree for the first time.
		public override void _EnterTree()
		{

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
