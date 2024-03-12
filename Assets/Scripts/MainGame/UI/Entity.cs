using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

public partial class Entity : TextureButton
{
    private static Tuple<int,Godot.Vector2> inMenuScaleSize = new(1, new Godot.Vector2(70,70));
    public static TextureButton InitEntity(Texture2D itemTexture)
    {
        var newItem = new Entity {
            TextureNormal = itemTexture,
            StretchMode = StretchModeEnum.KeepAspectCentered,
            IgnoreTextureSize = true,
            CustomMinimumSize = new Godot.Vector2(70, 70),
            Size = inMenuScaleSize.Item2,
            MouseDefaultCursorShape = CursorShape.PointingHand,
            ActionMode = ActionModeEnum.Press,
            MouseFilter = MouseFilterEnum.Pass,
        };
        newItem = Utils.AttachScript(newItem, "Assets/Scripts/MainGame/UI/ItemMover.cs");
        return newItem;
    }

    public void setNewSize(){
        this.Size = this.TextureNormal.GetSize();
        this.Scale = new Godot.Vector2(.33f,.33f);
    }
}