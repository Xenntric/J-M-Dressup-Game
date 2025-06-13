using Godot;
namespace DressupUI
{
	public partial class AnimateMenuButton : Utils.CallAnimateButton
	{
		[Export] public StrictGrid Clothes;

        public override void _Ready()
        {
            base._Ready();
            Pressed += () => CallAnimation(Animation[0]);

        }
        public override void CallAnimation(StringName animation)
        {
            base.CallAnimation(animation);
			Clothes.Visible = true;
			menuController.MenuDepth = MenuController.MenuDepthEnum.clothes;
        }
	}
}
