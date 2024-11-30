using Godot;

public partial class Game : Node2D
{
    [Node("Player")]
    private Player _player;

    public override void _Ready()
    {
        _player.DayEntered += OnDayEntered;
    }

    public override void _ExitTree()
    {
        _player.DayEntered -= OnDayEntered;
    }

    private void OnDayEntered(BaseDay day)
    {
        GetTree().ChangeSceneToPacked(day.LevelScene);
    }
}
