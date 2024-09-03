namespace ClueMastersDetectiveGame;

public static class LevelRepository
{
    public static List<Level> Levels { get; } = [];

    static LevelRepository()
    {
        // Initialize the first level
        var solutionGrid = new Grid();
        solutionGrid.PlaceToken(0, 0, new Token(TokenShape.Ball, TokenColor.Green));
        solutionGrid.PlaceToken(0, 1, new Token(TokenShape.Bone, TokenColor.Red));
        solutionGrid.PlaceToken(0, 2, new Token(TokenShape.Bowl, TokenColor.Blue));
        solutionGrid.PlaceToken(1, 0, new Token(TokenShape.Bone, TokenColor.Red));
        solutionGrid.PlaceToken(1, 1, new Token(TokenShape.Ball, TokenColor.Green));
        solutionGrid.PlaceToken(1, 2, new Token(TokenShape.Bowl, TokenColor.Blue));
        solutionGrid.PlaceToken(2, 0, new Token(TokenShape.Bone, TokenColor.Red));
        solutionGrid.PlaceToken(2, 1, new Token(TokenShape.Ball, TokenColor.Green));
        solutionGrid.PlaceToken(2, 2, new Token(TokenShape.Bowl, TokenColor.Blue));
        var solution = new Solution(solutionGrid);

        var clueGrid = new Grid();
        clueGrid.PlaceToken(0, 0, new Token(TokenShape.Ball, TokenColor.Green));
        clueGrid.PlaceToken(0, 1, new Token(TokenShape.Bone, TokenColor.Red));
        clueGrid.PlaceToken(1, 1, new Token(TokenShape.Ball, TokenColor.Green));
        var firstClue = new Clue(clueGrid);

        var negativeClueGrid = new Grid();
        negativeClueGrid.PlaceToken(1, 0, new Token(TokenShape.Bowl, TokenColor.Gray));
        negativeClueGrid.PlaceToken(1, 1, new Token(TokenShape.Bowl, TokenColor.Gray));
        var negativeClue = new Clue(negativeClueGrid, true);

        var clues = new List<Clue> { firstClue, negativeClue };
        var puzzle = new Level(1, solution, clues);

        Levels.Add(puzzle);
    }

    public static Level GetLevelById(int id)
    {
        return Levels.Find(level => level.Id == id)
            ?? throw new InvalidOperationException($"Level {id} was not found.");
    }
}
