using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

namespace DressupUI
{
    public partial class PopulateItems : TextureRect
    {
        //private TextureRect ItemContainer;
        private TextureRect MasterContainer;
        public Container FolderContainer;
        private HBoxContainer Rows;
        private VBoxContainer Columns;
        [Export] public int AmountOfRows = 3;

        [Export] public Control InteractableItemLayer;
        [Export] public Area2D MenuArea;
        private int foldersScanned;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ProcessPriority = 0;
            GD.Print("Populate Items");
            MasterContainer = GetNode<TextureRect>(GetPath());
            CreateFolders();
        }

        public void CreateFolders()
        {
            foreach (var folder in Directory.GetDirectories(@"Assets/Art/Clothes/"))
            {
                List<Texture2D> itemInFolder = new();
                foreach (string item in Directory.GetFiles(folder, "*.png"))
                {
                    itemInFolder.Add((Texture2D)GD.Load(item));
                    GD.Print(item);
                }

                int itemScanned = 0;

                FolderContainer = new Container
                {
                    Name = Path.GetFileNameWithoutExtension(folder),
                    Size = MasterContainer.Size - new Vector2(30,35),
                    Position = new Vector2(15,15)
                };
                // var FolderArea = new Area2D
                // {
                //     Name = FolderContainer.Name + " Area",
                //      = FolderContainer.Size
                // };

                //MenuArea.MouseEntered += () => mouseExitedMenu();
                FolderContainer.MouseEntered += mouseExitedMenu;
                
                foldersScanned++;

                if (foldersScanned > 1)
                {
                    FolderContainer.Visible = false;
                }

                MasterContainer.AddChild(FolderContainer);

                Columns = new VBoxContainer();
                Columns.SetPosition(new Vector2(5, 5));
                FolderContainer.AddChild(Columns);

                for (int i = 0; i < AmountOfRows; i++)
                {
                    Rows = new HBoxContainer();
                    Columns.AddChild(Rows);

                    for (int j = 0; j < 3; j++)
                    {
                        if (itemScanned < itemInFolder.Count)
                        {
                            
                            var itemButton = Entity.InitEntity(itemInFolder.ElementAt<Texture2D>(itemScanned));
                            itemButton.ButtonDown += () => OnPressed(itemButton);
                            Rows.AddChild(itemButton);
                            itemScanned++;
                        }
                    }
                }
            }
        }

        private void OnPressed(TextureButton item)
        {
                    GD.Print("Pressed");

            if (item.Visible)
            {
                //CreateItem(item);
            }
        }
        private void mouseExitedMenu()
        {
        }

        private void CreateItem(TextureButton textureButton)
        {
            GD.Print("CreateItem");
            var texture = textureButton.TextureNormal;
            var item = new TextureButton
            {
                TextureNormal = texture,
                Scale = new Vector2(.33f,.33f),
                ActionMode = BaseButton.ActionModeEnum.Press,
                MouseFilter = MouseFilterEnum.Pass,
                ButtonPressed = true,
                Size = new Vector2(100,100),
            };
            
            item = Utils.Scripts.AttachScript<TextureButton>(item, "Assets/Scripts/MainGame/UI/ItemMover.cs");

            item.ButtonPressed = true;

            this.AddChild(item);
            item.ZIndex = 1;
            item.GlobalPosition = textureButton.GlobalPosition;
        }
    }
}
