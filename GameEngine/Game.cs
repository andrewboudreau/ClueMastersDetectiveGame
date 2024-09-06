namespace ClueMastersDetectiveGame;

public enum TokenShape
{
    Unknown,
    Shape1,
    Shape2,
    Shape3
}

public enum TokenColor
{
    Unknown,
    Red,
    Green,
    Blue
}

public class Token
{
    public TokenShape Shape { get; set; }
    public TokenColor Color { get; set; }

    public Token(TokenShape shape, TokenColor color)
    {
        Shape = shape;
        Color = color;
    }

    public bool IsColorless => Color == TokenColor.Unknown;
    public bool IsShapeless => Shape == TokenShape.Unknown;
    public bool IsAny => IsColorless && IsShapeless;

    public bool IsColor(TokenColor color) => Color == color;
    public bool IsShape(TokenShape shape) => Shape == shape;

    public override bool Equals(object? obj)
    {
        if (obj is not Token other)
            return false;

        if (IsAny || other.IsAny)
            return true;

        if (IsColorless || other.IsColorless)
            return Shape == other.Shape;

        if (IsShapeless || other.IsShapeless)
            return Color == other.Color;

        return Shape == other.Shape && Color == other.Color;
    }

    public override int GetHashCode() => HashCode.Combine(Shape, Color);

    public static bool operator ==(Token left, Token right) => Equals(left, right);
    public static bool operator !=(Token left, Token right) => !Equals(left, right);

    public readonly static Token RedBone = new(TokenShape.Shape1, TokenColor.Red);
    public readonly static Token RedBall = new(TokenShape.Shape2, TokenColor.Red);
    public readonly static Token RedBowl = new(TokenShape.Shape3, TokenColor.Red);

    public readonly static Token GreenBone = new(TokenShape.Shape1, TokenColor.Green);
    public readonly static Token GreenBall = new(TokenShape.Shape2, TokenColor.Green);
    public readonly static Token GreenBowl = new(TokenShape.Shape3, TokenColor.Green);

    public readonly static Token BlueBone = new(TokenShape.Shape1, TokenColor.Blue);
    public readonly static Token BlueBall = new(TokenShape.Shape2, TokenColor.Blue);
    public readonly static Token BlueBowl = new(TokenShape.Shape3, TokenColor.Blue);

    public readonly static Token ColorlessBone = new(TokenShape.Shape1, TokenColor.Unknown);
    public readonly static Token ColorlessBall = new(TokenShape.Shape2, TokenColor.Unknown);
    public readonly static Token ColorlessBowl = new(TokenShape.Shape3, TokenColor.Unknown);

    public readonly static Token Any = new(TokenShape.Unknown, TokenColor.Unknown);
}

public class Grid
{
    public Token[,] Tokens { get; set; }

    public Grid(Token[,]? tokens = null)
    {
        Tokens = tokens ?? new Token[3, 3];
    }

    public void PlaceToken(int row, int col, Token token) => Tokens[row, col] = token;

    public Token GetToken(int row, int col) => Tokens[row, col];

    public static Grid FromArray(Token[][] tokens)
    {
        int rows = tokens.Length;
        int cols = tokens[0].Length;
        var grid = new Grid(new Token[rows, cols]);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                grid.Tokens[row, col] = tokens[row][col];
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

    public bool Validate(Grid userGrid)
    {
        int patternRows = Pattern.Length;
        int patternCols = Pattern[0].Length;

        for (int row = 0; row < patternRows; row++)
        {
            for (int col = 0; col < patternCols; col++)
            {
                var clueToken = Pattern[row][col];
                var gridToken = userGrid.GetToken(row + OffsetRow, col + OffsetCol);

                if (IsNegative && clueToken.Equals(gridToken))
                    return false;

                if (!IsNegative && !clueToken.Equals(gridToken))
                    return false;
            }
        }

        return true;
    }
}

public class Solution(Grid grid)
{
    public Grid Grid { get; private set; } = grid;

    public bool Validate(Grid userGrid)
    {
        var rows = Grid.Tokens.GetLength(0);
        var cols = Grid.Tokens.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var token = Grid.GetToken(row, col);
                var userToken = userGrid.GetToken(row, col);

                if (token.Equals(userToken))
                {
                    continue;
                }
            }
        }

        return true;
    }

    public static Solution FromArray(Token[][] tokens) => new(Grid.FromArray(tokens));
}

public class Level
{
    public int Id { get; private set; }
    public Solution Solution { get; private set; }
    public List<Clue> Clues { get; private set; }

    public Level(int id, Solution solution, params Clue[] clues)
    {
        Id = id;
        Solution = solution;
        Clues = new List<Clue>(clues);
    }

    public bool Validate(Grid userGrid)
    {
        foreach (var clue in Clues)
        {
            if (!clue.Validate(userGrid))
            {
                return false;
            }

        }

        return Solution.Validate(userGrid);
    }
}
