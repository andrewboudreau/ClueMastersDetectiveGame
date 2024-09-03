namespace ClueMastersDetectiveGame;

public static class LevelRepository
{
    public static List<Level> Levels { get; } = [];

    static LevelRepository()
    {
        var solution = Grid.FromArray([
            [ Token.GreenBowl, Token.BlueBone, Token.RedBall ],
            [ Token.RedBone, Token.GreenBone, Token.BlueBowl ],
            [ Token.GreenBall, Token.RedBowl, Token.BlueBall]
        ]);

        var clue = new Clue([
            [Token.GreenBowl, Token.BlueBone, Token.RedBall],
            [Token.RedBone, Token.GreenBone, Token.BlueBowl],
            [Token.ColorlessBall, Token.RedShape, Token.BlueShape]
        ]);

        var solution = new Solution(solution);
        var puzzle = new Level(1, solution, [clue]);

        Levels.Add(puzzle);
    }

    public static Level GetLevelById(int id)
    {
        return Levels.Find(level => level.Id == id)
            ?? throw new InvalidOperationException($"Level {id} was not found.");
    }
}
