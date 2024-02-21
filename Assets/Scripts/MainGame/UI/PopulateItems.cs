using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

public partial class PopulateItems : TextureRect
{
    //private TextureRect ItemContainer;
    private TextureRect MasterContainer;
    private Container FolderContainer;
    private HBoxContainer Rows;
    private VBoxContainer Columns;
    [Export] public int AmountOfRows = 3;

    private int foldersScanned;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ProcessPriority = 0;
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
                Size = MasterContainer.Size
            };
            
			foldersScanned++;

            if (foldersScanned > 1)
            {
                FolderContainer.Visible = false;
            }

            MasterContainer.AddChild(FolderContainer);

            Columns = new VBoxContainer();
            Columns.SetPosition(new Vector2(25, 25));
            FolderContainer.AddChild(Columns);

            for (int i = 0; i < AmountOfRows; i++)
            {
                Rows = new HBoxContainer();
                Columns.AddChild(Rows);

                for (int j = 0; j < 3; j++)
                {
                    if (itemScanned < itemInFolder.Count)
                    {
                        var itemButton = new TextureButton
                        {
                            TextureNormal = itemInFolder.ElementAt(itemScanned),
                            StretchMode = TextureButton.StretchModeEnum.KeepAspect,
                            IgnoreTextureSize = true,
                            CustomMinimumSize = new Vector2(70, 70)
                        };
                        Rows.AddChild(itemButton);
                        itemScanned++;
                    }
                }
            }
        }
    }
}

