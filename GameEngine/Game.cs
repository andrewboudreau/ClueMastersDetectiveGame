namespace ClueMastersDetectiveGame;

public enum TokenShape
{
    Unknown,
    Bone,
    Ball,
    Bowl
}

public enum TokenColor
{
    Unknown,
    Red,
    Green,
    Blue
}

public class Token(TokenShape shape, TokenColor color)
{
    public TokenShape Shape { get; set; } = shape;
    public TokenColor Color { get; set; } = color;
    public bool Negative { get; set; } = false;

    public readonly static Token RedBone = new(TokenShape.Bone, TokenColor.Red);
    public readonly static Token RedBall = new(TokenShape.Ball, TokenColor.Red);
    public readonly static Token RedBowl = new(TokenShape.Bowl, TokenColor.Red);
    public readonly static Token RedShape = new(TokenShape.Unknown, TokenColor.Red);

    public readonly static Token GreenBone = new(TokenShape.Bone, TokenColor.Green);
    public readonly static Token GreenBall = new(TokenShape.Ball, TokenColor.Green);
    public readonly static Token GreenBowl = new(TokenShape.Bowl, TokenColor.Green);
    public readonly static Token GreenShape = new(TokenShape.Unknown, TokenColor.Green);

    public readonly static Token BlueBone = new(TokenShape.Bone, TokenColor.Blue);
    public readonly static Token BlueBall = new(TokenShape.Ball, TokenColor.Blue);
    public readonly static Token BlueBowl = new(TokenShape.Bowl, TokenColor.Blue);
    public readonly static Token BlueShape = new(TokenShape.Unknown, TokenColor.Blue);

    public readonly static Token ColorlessBone = new(TokenShape.Bone, TokenColor.Unknown);
    public readonly static Token ColorlessBall = new(TokenShape.Ball, TokenColor.Unknown);
    public readonly static Token ColorlessBowl = new(TokenShape.Bowl, TokenColor.Unknown);
}

public class Grid(Token[,]? tokens = default)
{
    public Token[,] Cells { get; set; }
        = tokens ?? new Token[3, 3];

    public void PlaceToken(int row, int col, Token token)
    {
        Cells[row, col] = token;
    }

    public Token GetToken(int row, int col)
    {
        return Cells[row, col];
    }

    public IEnumerable<(Token Token, int X, int Y)> Pattern()
    {
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                yield return (Cells[row, col], col, row);
            }
        }
    }

    public static Grid FromArray(Token[][] tokens)
    {
        int rows = tokens.Length;
        int cols = tokens[0].Length;
        var grid = new Grid(new Token[rows, cols]);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                grid.Cells[row, col] = tokens[row][col];
            }
        }

        return grid;
    }
}

public class Clue
{
    public Token[][] Pattern { get; set; }
    public int OffsetRow { get; set; }
    public int OffsetCol { get; set; }
    public bool IsNegative { get; set; }

    public Clue(Token[][] pattern, int offsetRow = 0, int offsetCol = 0, bool isNegative = false)
    {
        Pattern = pattern;
        OffsetRow = offsetRow;
        OffsetCol = offsetCol;
        IsNegative = isNegative;
    }

    public bool CheckAgainst(Grid userGrid)
    {
        int patternRows = Pattern.GetLength(0);
        int patternCols = Pattern.GetLength(1);

        for (int row = 0; row < patternRows; row++)
        {
            for (int col = 0; col < patternCols; col++)
            {
                var clueToken = Pattern[row][col];
                var gridToken = userGrid.GetToken(row + OffsetRow, col + OffsetCol);

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
