using Godot;
using System;
using Utils;
namespace Dressup
{
	public partial class MagnetiseScript : CallAnimateButton
	{
		// Called when the node enters the scene tree for the first time.
		
		public override void _Ready()
		{
			base._Ready();
			Pressed += tiggle;
		}
		private void tiggle()
		{
			GD.Print(globals.magnetise);
		}
	}
}