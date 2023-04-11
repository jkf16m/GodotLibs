using System;
using System.Collections.Generic;
using Godot;


public class NullNodeException : Exception{
    public NullNodeException(string message) : base(message){}
}
public static class NodeExtensions{
    public static void ReparentTo(this Node node, Node newParent){
        node.GetParent().RemoveChild(node);
        newParent.AddChild(node);
    }

    public static Dictionary<string, Node> GetTypedChildren<T>(this Node node) where T : Node{
        var result = new Dictionary<string, Node>();
        foreach(var child in node.GetChildren()){
            if(child is T)
                result.Add(child.GetType().Name, (T)child);
        }
        return result;
    }

    public static T GetNodeByTypeOrNull<T>(this Node node) where T : Node{
        foreach(var child in node.GetChildren()){
            if(child is T)
                return (T)child;
        }

        return null;
    }

    public static T GetNodeByType<T>(this Node node) where T : Node{
        var result = node.GetNodeByTypeOrNull<T>();

        if(result == null)
            throw new NullNodeException($"Node {node.Name} doesn't have a child of type {typeof(T).Name}");

        return result;
    }
}