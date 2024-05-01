#if TOOLS
using Godot;
using System;

[Tool]
public partial class StrictGridContainer : EditorPlugin
{
	public override void _EnterTree()
	{
		var script = ResourceLoader.Load<Script>("res://addons/strict_grid_container/StrictGrid.cs");
        var texture = GD.Load<Texture2D>("res://addons/strict_grid_container/StrictGridContainer.svg");
        AddCustomType("StrictGridContainer", "Container", script, texture);
	}

	public override void _ExitTree()
	{
		RemoveCustomType("StrictGrid");
		// Clean-up of the plugin goes here.
	}
}
#endif
