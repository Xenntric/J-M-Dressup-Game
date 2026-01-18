using System;
using Dressup;
using Godot;
using Godot.Collections;

namespace Utils
{
	public partial class CallAnimateButton : TextureButton
	{
		[Export] public DressupUI.MenuController menuController;
		[Export] public AnimationPlayer AnimationPlayer;
		[Export] public Array<StringName> Animation;
		[Export] bool Reversed;
		[Export] public Vector2 originPos;
		protected Globals globals;
		protected Tween tween;
		protected bool inside;
		
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());

			PivotOffset = Size/2;
			if(Animation != null)
			{
				Pressed += () => CallAnimation(Animation[0]);
			}

			MouseEntered += HandleMouseEntered;
			MouseExited  += HandleMouseExited;
			ButtonDown 	 += HandleButtonDown;
			
			this.originPos = this.Position;
		}

        public override void _EnterTree()
        {
            base._EnterTree();
			this.originPos = this.Position;
        }
        
		protected virtual void HandleMouseEntered()
		{
			inside = true;
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One * 1.1f, 0.15f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.In);
		}
		protected virtual void HandleMouseExited()
		{
			inside = false;
			tween?.Kill();
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One, 0.10f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.In);

		}
		protected virtual void HandleButtonDown()
		{
			tween?.Kill();
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One * .9f, 0.15f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.Out);
		}
		protected virtual void HandleButtonUp()
		{
			tween?.Kill();
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One * .9f, 0.15f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.Out);
		}
		public virtual void CallAnimation(StringName animation)
		{
			if(AnimationPlayer.IsPlaying()) { return; }

			if(Reversed)
			{
				AnimationPlayer.PlayBackwards(animation);
			}
			else
			{
				AnimationPlayer.Play(animation);
			}
		}

		protected new Tween CreateTween()
		{
			tween = GetTree().CreateTween();
			return tween;
		}

		protected void ToggleDisabled()
		{
			this.Disabled = !this.Disabled;
		}
	}
}
