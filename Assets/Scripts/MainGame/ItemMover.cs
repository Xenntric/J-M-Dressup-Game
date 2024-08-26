using Dressup;
using Godot;
using Godot.NativeInterop;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace DressupUI
{
	public partial class ItemMover : Entity
	{
		private Globals globals;
		private Vector2 PosOffset;
		[Export] private Control FolderContainer;
		private Vector2 oldMousePos;
		[Export] private Node ItemLayerNode;
		private bool mouseMoving;
		private Vector2	TextureSize;
		private bool grabbed;
		[Export] private Vector2 magnetPosition;

		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());
			this.ProcessPriority = 1;
			this.TextureSize = this.TextureNormal.GetSize();
			this.ButtonDown += AttachAndMove;
		}

		private void AttachAndMove()
		{
			PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;
			if(GetParent() != ItemLayerNode)
			{
				var menumidpoint = this.Position + Size/2;
				SetZIndex();
				SetSize(TextureSize);

				Position = menumidpoint - Size*.33f/2;

				Reparent(ItemLayerNode);

				PosOffset = this.GetViewport().GetMousePosition() - this.GlobalPosition;

				ToggleMode = true;
			}

			globals.GrabbedItem = this;
			ButtonPressed = true;
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			base._Input(@event);
			if(@event.IsActionReleased("Grab")                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             )
			{
				ToggleMode = false;
				globals.GrabbedItem = null;
			}
			else if (@event is InputEventMouseMotion eventMouseMotion && ButtonPressed)
			{
				GlobalPosition = eventMouseMotion.Position - PosOffset;
			}
		}
	}
}