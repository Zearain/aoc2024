using Chickensoft.GoDotTest;
using Chickensoft.GodotTestDriver;

using FluentAssertions;

using Godot;

public class Day3Tests : TestClass
{
    private const string TEST_INPUT = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    private readonly string[] uncorruptedInstructions = new string[]
    {
        "mul(2,4)",
        "mul(5,5)",
        "mul(11,8)",
        "mul(8,5)",
    };

    private Day3 _dayScene = default!;
    private Fixture _fixture = default!;

    public Day3Tests(Node testScene) : base(testScene)
    {
    }

    [SetupAll]
    public async Task SetupAll()
    {
        _fixture = new Fixture(TestScene.GetTree());
        _dayScene = await _fixture.LoadAndAddScene<Day3>();
    }

    [CleanupAll]
    public void CleanupAll()
    {
        _fixture.Cleanup();
    }

    [Test]
    public void ParseUncorruptedInstructions_ShouldReturnUncorruptedInstructions()
    {
        var instructions = Day3.ParseUncorruptedInstructions(TEST_INPUT);

        instructions.Should().BeEquivalentTo(uncorruptedInstructions);
    }

    [Test]
    public void SumInstructions_ShouldReturnSumOfInstructions()
    {
        var sum = Day3.SumInstructions(uncorruptedInstructions);

        sum.Should().Be(2 * 4 + 5 * 5 + 11 * 8 + 8 * 5);
    }
}