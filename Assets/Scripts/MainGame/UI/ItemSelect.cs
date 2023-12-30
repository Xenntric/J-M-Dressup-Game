using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;

public partial class ItemSelect : ItemList
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemClicked += (index, pos, mb) => itemclicked(index, pos, mb);
	}

	public void itemclicked(long index, Godot.Vector2 pos, long mb)
	{
		GD.Print(index);
		SetItemDisabled((int)index, true);
	}
}
