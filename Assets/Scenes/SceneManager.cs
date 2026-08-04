using Godot;
using System;

public partial class SceneManager : CanvasLayer
{
    private PackedScene[] SceneStack = [];
    public PackedScene Top() { return SceneStack[SceneStack.Length]; }
    public PackedScene[] GetStack() { return SceneStack; }
    public void TransitionTo(PackedScene nextScene)
    {
		var scene = ResourceLoader.Load<PackedScene>(nextScene.ResourcePath);
		GetTree().ChangeSceneToPacked(scene);
    }
}
