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
			Pressed += toggle;
		}
		private void toggle()
		{
			GD.Print(globals.magnetise);
		}
	}
}