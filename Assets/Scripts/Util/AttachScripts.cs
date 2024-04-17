using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

public class Utils 
{
    public static T AttachScript<T>(T node, string Script) where T : Node
    {
        ulong objId = node.GetInstanceId();
        node.SetScript(ResourceLoader.Load(Script));
        return GodotObject.InstanceFromId(objId) as T;
    }
};
