using Godot;
using Chickensoft.GoDotTest;
using Chickensoft.GodotTestDriver;
using LightMock.Generator;
using LightMoq;
using FluentAssertions;

public class Day1Tests : TestClass
{
    private const string PART1_INPUT = """
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """;

    private Day1 _day1 = default!;
    private Fixture _fixture = default!;

    public Day1Tests(Node testScene) : base(testScene)
    {
    }

    [SetupAll]
    public async Task SetupAll()
    {
        _fixture = new Fixture(TestScene.GetTree());
        _day1 = await _fixture.LoadAndAddScene<Day1>();
    }

    [CleanupAll]
    public void CleanupAll()
    {
        _fixture.Cleanup();
    }

    [Test]
    public void SetInput_ShouldPopulateInputAndLines()
    {
        _day1.SetInput(PART1_INPUT);

        _day1.Input.Should().Be(PART1_INPUT);
        _day1.Lines.Should().BeEquivalentTo(new[] { "3   4", "4   3", "2   5", "1   3", "3   9", "3   3" });
    }

    [Test]
    public void ParseCoordinateLists_ShouldReturnLeftAndRightLists()
    {
        _day1.SetInput(PART1_INPUT);

        var (left, right) = Day1.ParseCoordinateLists(_day1.Lines);

        left.Should().BeEquivalentTo(new[] { 3, 4, 2, 1, 3, 3 });
        right.Should().BeEquivalentTo(new[] { 4, 3, 5, 3, 9, 3 });
    }

    [Test]
    public void SortCoordinateLists_ShouldReturnSortedLeftAndRightLists()
    {
        var lists = (new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 });

        var (left, right) = Day1.SortCoordinateLists(lists);

        left.Should().BeEquivalentTo(new[] { 1, 2, 3, 3, 3, 4 });
        right.Should().BeEquivalentTo(new[] { 3, 3, 3, 4, 5, 9 });
    }

    [Test]
    public void Distance_ShouldReturnAbsoluteDifferenceBetweenTwoNumbers()
    {
        Day1.Distance(1, 3).Should().Be(2);
        Day1.Distance(2, 3).Should().Be(1);
        Day1.Distance(3, 3).Should().Be(0);
        Day1.Distance(3, 4).Should().Be(1);
        Day1.Distance(3, 5).Should().Be(2);
        Day1.Distance(4, 9).Should().Be(5);
    }

    [Test]
    public void Part1_ShouldReturnCorrectDistance()
    {
        _day1.SetInput(PART1_INPUT);

        var (left, right) = Day1.SortCoordinateLists(Day1.ParseCoordinateLists(_day1.Lines));

        var distance = Day1.TotalDistance(left, right);

        distance.Should().Be(11);
    }

    [Test]
    public void SimilarityScore_ShouldReturnCorrectScore()
    {
        var right = new[] { 4, 3, 5, 3, 9, 3 };

        Day1.SimilarityScore(3, right).Should().Be(9);
        Day1.SimilarityScore(4, right).Should().Be(4);
        Day1.SimilarityScore(2, right).Should().Be(0);
        Day1.SimilarityScore(1, right).Should().Be(0);
        Day1.SimilarityScore(3, right).Should().Be(9);
        Day1.SimilarityScore(3, right).Should().Be(9);
    }

    [Test]
    public void Part2_ShouldReturnCorrectScore()
    {
        _day1.SetInput(PART1_INPUT);

        var (left, right) = Day1.ParseCoordinateLists(_day1.Lines);

        var score = left.Sum(x => Day1.SimilarityScore(x, right));

        score.Should().Be(31);
    }
}