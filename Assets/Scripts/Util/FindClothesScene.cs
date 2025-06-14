using System;
using Godot;

namespace Utils
{
    public class Finder
    {
        public static PackedScene FindClothesScene<T>(T node) where T : Node
        {
            string name = node.Name;
            name = name.ToLower();
            var path = "Assets/Objects/";
            if(name.StartsWith('j')) { path += "Julius/"; }
            if(name.StartsWith('m')) { path += "Matt/"; }
            path += name;
            path += ".tscn";
 
            return GD.Load<PackedScene>(path);
        }
    }
}
