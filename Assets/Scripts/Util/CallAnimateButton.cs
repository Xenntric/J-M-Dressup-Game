using Dressup;
using Godot;
using Godot.Collections;

namespace Utils
{
	public partial class CallAnimateButton : TextureButton
	{
		[Export] public DressupUI.MenuController menuController;
		[Export] public AnimationPlayer AnimationPlayer;
		[Export] public Godot.Collections.Array<StringName> Animation;
		[Export] bool Reversed;
		[Export] public Vector2 originPos;
		protected Tween tween;

		protected Globals globals;
		
		public override void _Ready()
		{
			globals = GetNode<Globals>(GetTree().Root.GetChild(0).GetPath());

			PivotOffset = new Vector2(Size.X/2, Size.Y/2);
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
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One * 1.1f, 0.15f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.In);
		}
		protected virtual  void HandleMouseExited()
		{
			tween.Kill();
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One, 0.10f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.In);

		}
		protected virtual  void HandleButtonDown()
		{
			tween.Kill();
			CreateTween().TweenProperty(GetNode(GetPath()), "scale", Vector2.One * .9f, 0.15f)
				 .SetTrans(Tween.TransitionType.Back)
				 .SetEase(Tween.EaseType.Out);
		}

		public virtual void CallAnimation(StringName animation)
		{
			if(AnimationPlayer.IsPlaying())
			{
				return;
			}

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
			// this.Disabled = true;
			tween = GetTree().CreateTween();
			// tween.Finished += ToggleDisabled;
			return tween;
		}

		protected void ToggleDisabled()
		{
			this.Disabled = !this.Disabled;
		}
	}
}
