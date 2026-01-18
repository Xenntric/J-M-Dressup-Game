using Godot;
using System;
using Utils;
namespace Dressup
{
	public partial class MagnitiseButton : CallAnimateButton
	{		
		public override void _Ready()
		{
			base._Ready();
			Pressed += Toggle;
		}
		private void Toggle()
		{
			GD.Print(globals.magnetise);
		}
	}
}