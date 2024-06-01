using Godot;
using System;

public partial class MenuController : Control
{


	public enum MenuDepthEnum {
		main,
		clothes
	}

	public MenuDepthEnum MenuDepth {get;set;}
}
