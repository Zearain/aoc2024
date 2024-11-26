using System.Reflection;

using Godot;

public static class NodeResolver
{
    public static void ResolveNodes(Node node)
    {
        var fields = node.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<NodeAttribute>();
            if (attribute != null)
            {
                var path = attribute.NodePath;
                var target = node.GetNode(path);
                if (target == null)
                {
                    GD.PushError($"Failed to resolve node at path '{path}' for field '{field.Name}' on object of type '{node.GetType().Name}'.");
                    continue;
                }

                field.SetValue(node, target);
            }
        }
    }
}