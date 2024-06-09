using Godot;

namespace Utils
{
    public class Scripts 
    {
        public static T AttachScript<T>(T node, string Script) where T : Node
        {
            ulong objId = node.GetInstanceId();
            node.SetScript(ResourceLoader.Load(Script));
            return GodotObject.InstanceFromId(objId) as T;
        }
    };
}
