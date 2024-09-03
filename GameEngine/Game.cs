namespace ClueMastersDetectiveGame;

public enum TokenShape
{
    Bone,
    Ball,
    Bowl
}

public enum TokenColor
{
    Red,
    Green,
    Blue,
    Gray // For negative clues
}

public class Token(TokenShape shape, TokenColor color)
{
    public TokenShape Shape { get; set; } = shape;
    public TokenColor Color { get; set; } = color;
}

public class Grid
{
    public Token[,] Cells { get; set; }

    public Grid()
    {
        Cells = new Token[3, 3];
    }

    public void PlaceToken(int row, int col, Token token)
    {
        Cells[row, col] = token;
    }

    public Token GetToken(int row, int col)
    {
        return Cells[row, col];
    }

    public IEnumerable<(Token Token, int X, int Y)> Tokens()
    {
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                yield return (Cells[row, col], col, row);
            }
        }
    }
}

public class Clue(Grid grid, bool isNegative = false)
{
    public Grid Grid { get; set; } = grid;
    public bool IsNegative { get; set; } = isNegative;

    public bool CheckAgainst(Grid userGrid)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var clueToken = Grid.GetToken(row, col);
                var gridToken = userGrid.GetToken(row, col);

                if (clueToken != null)
                {
                    if (IsNegative)
                    {
                        if (clueToken.Shape == gridToken?.Shape && clueToken.Color == gridToken?.Color)
                        {
                            return false; // A negative clue pattern was found, invalid solution
                        }
                    }
                    else
                    {
                        if (clueToken.Shape != gridToken?.Shape || clueToken.Color != gridToken?.Color)
                        {
                            return false; // A positive clue pattern does not match
                        }
                    }
                }
            }
        }
        return true;
    }
}

public class Solution(Grid grid)
{
    public Grid Grid { get; set; } = grid;

    public bool Check(Grid userGrid, List<Clue> clues)
    {
        foreach (var clue in clues)
        {
            if (!clue.CheckAgainst(userGrid))
            {
                if (!clue.IsNegative)
                {
                    return false; // Positive clue not satisfied
                }
            }
        }

        foreach (var clue in clues)
        {
            if (clue.IsNegative && !clue.CheckAgainst(userGrid))
            {
                return false; // Negative clue pattern was found
            }
        }

        return true;
    }
}

public class Level(int id, Solution solution, List<Clue> clues)
{
    public int Id { get; private set; } = id;
    public Solution Solution { get; private set; } = solution;
    public List<Clue> Clues { get; private set; } = clues;
}
