using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
public struct loadItems 
{
    public List<Sprite2D> hats_sprites;
    public List<Sprite2D> tops_sprites;
    public List<Sprite2D> trousers_sprites;
    public List<Sprite2D> shoes_sprites;
    public List<Sprite2D> props_sprites;

    public List<Items> Hats;    
    

    public List<Items> Construct_Hats(string kind_of_clothes)
    {
        Hats = new List<Items>();
        var address = System.IO.Path.Combine(@"Assets/Art/Clothes/",kind_of_clothes);
        //address = System.IO.Path.Combine(address,"/");
        GD.Print("Getting clothes @ " + address);
        string[] hats_address = Directory.GetFiles(address , "*.png");

        if (!hats_address.IsEmpty())
        {
            GD.Print("Hats Found");
            foreach (var hat in hats_address)
            {
                var hat_item = new Items();
                var texture = (Texture2D)GD.Load(hat);

                hat_item.item_sprite = new Sprite2D();
                hat_item.item_collider = new CollisionShape2D();
                hat_item.item_area = new Area2D();
                hat_item.item_body = new StaticBody2D();
                hat_item.item = new Node2D();

                //GD.Print(hat_item.item_collider.GlobalTransform);
                //GD.Print(hat_item.item_collider.GlobalPosition);

                //hat_item.item_collider.DrawDashedLine();
                GD.Print("Currently looking without ext @ " + Path.GetFileNameWithoutExtension(hat));
                //var sprite = new Sprite2D();
                //var collider_area = new Area2D();
                //var collider_shape = new CollisionShape2D();

                hat_item.item_name = Path.GetFileNameWithoutExtension(hat);

                hat_item.item_sprite.Texture = texture;                

                Hats.Add(hat_item);
                //collider_area.AddChild(collider_shape);
                //sprite.AddChild(collider_area);
            }
            
            return(Hats);
        }
        GD.Print("No Hats Found");
        return null;
    }
}
