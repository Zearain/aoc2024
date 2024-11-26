using Godot;

public partial class GlobalNodeResolver : Node
{
    public override void _Ready()
    {
        GetTree().NodeAdded += OnNodeAdded;

        // Resolver needs to be executed for all nodes already in the scene tree (auto-load nodes)
        foreach (var node in GetTree().Root.GetChildren()) {
            OnNodeAdded(node);
        }
    }

    public override void _ExitTree()
    {
        GetTree().NodeAdded -= OnNodeAdded;
    }

    private void OnNodeAdded(Node node)
    {
        NodeResolver.ResolveNodes(node);
    }
}