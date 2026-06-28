using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dressup
{
	public partial class Globals : Node2D
	{
		[Export] public Control ClothesControl;
		[Export] public TextureRect MenuPanel;
		[Export] public Container ItemFolders;
        [Export] private Node2D CharacterNode;
        [Export] private OptionsMenu OptionsMenu;
        public OptionsMenu getOptionsMenu { get { return OptionsMenu; }}

        [Export] public Trash trash;
		[Export] public bool magnetise = true;
        public Magnetism magnetTarget;
		[Export] public LiveItem GrabbedItem {get;set;}
        public Godot.Collections.Array<FolderItem> OutFolderItems { get; set; } = [];
        public List<LiveItem> itemStack {get;} = [];
        private Vector2 characterScale;
        public Vector2 getCharacterScale { get { return characterScale; }}
        private MouseController mouseController;
        public override void _EnterTree()
		{
			GrabbedItem = null;
            mouseController = new MouseController(this);
            characterScale = CharacterNode.Scale;
		}

		public void HandleItemClicked()
		{

		}

		public void HandleItemDropped()
		{
			
		}

        public override void _Input(InputEvent @event)
        {
            mouseController.Input(@event);
        }

        public void PushItem(LiveItem item)
        {
            itemStack.Add(item);
            OptionsMenu.ToggleAllButtons(false);
        }

        public void PopItem(LiveItem item)
        {
            itemStack.Remove(item);
            if (itemStack.Count == 0)
            {
                OptionsMenu.ToggleAllButtons(true);
            }
        }
	}
}
