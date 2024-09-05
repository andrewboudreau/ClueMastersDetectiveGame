namespace ClueMastersDetectiveGame;

public static class LevelRepository
{
    private readonly static Token BBone = Token.BlueBone;
    private readonly static Token BBowl = Token.BlueBowl;
    private readonly static Token BBall = Token.BlueBall;
    private readonly static Token GBone = Token.GreenBone;
    private readonly static Token GBowl = Token.GreenBowl;
    private readonly static Token GBall = Token.GreenBall;
    private readonly static Token RBone = Token.RedBone;
    private readonly static Token RBowl = Token.RedBowl;
    private readonly static Token RBall = Token.RedBall;
    private readonly static Token CBone = Token.ColorlessBone;
    private readonly static Token CBowl = Token.ColorlessBowl;
    private readonly static Token CBall = Token.ColorlessBall;
    private readonly static Token Blue = Token.BlueShape;
    private readonly static Token Green = Token.GreenShape;
    private readonly static Token Red = Token.RedShape;

    private readonly static Token Any = Token.Any;
    private readonly static Token[] AnyRow = [Token.Any, Token.Any, Token.Any];

    public static List<Level> Levels { get; } = [];

    static LevelRepository()
    {
        Levels.Add(Level1());
        Levels.Add(Level2());
        Levels.Add(Level3());
    }

    public static Level GetLevelById(int id)
    {
        return Levels.Find(level => level.Id == id)
            ?? throw new InvalidOperationException($"Level {id} was not found.");
    }

    public static Level Level1()
    {
        return new Level(1,
            Solution.FromArray([
                [ Token.GreenBowl, Token.BlueBone, Token.RedBall ],
                [ Token.RedBone, Token.GreenBone, Token.BlueBowl ],
                [ Token.GreenBall, Token.RedBowl, Token.BlueBall ]
            ]),
            new Clue([
                [ Token.GreenBowl, Token.BlueBone, Token.RedBall ],
                [ Token.RedBone, Token.GreenBone, Token.BlueBowl ],
                [ Token.ColorlessBall, Token.RedShape, Token.BlueShape ]
            ])
        );
    }

    public static Level Level2()
    {
        return new Level(2,
            Solution.FromArray([
                [BBowl, BBall, BBone],
                [GBowl, GBone, GBall],
                [RBall, RBone, RBowl]
            ]),
            new Clue([
                [CBowl, Any, Any],
                [CBowl, Any, Any],
                [CBall, Any, Any]
            ]),
            new Clue([
                [Blue, Blue, Blue],
                [Any, Any, Any],
                [Any, Any, Any]
            ]),
            new Clue([
                [Any, Any, Any],
                [Green, Green, Green],
                [Any, Any, Any]
            ]),
            new Clue([
                [Any, Any, CBone],
                [Any, Any, CBall],
                [Any, Any, CBowl]
            ])
        );
    }

    public static Level Level3()
    {
        return new Level(2,
            Solution.FromArray([
                [ BBall, BBone, RBowl ],
                [ GBone, GBall, RBone ],
                [ BBowl, RBall, GBowl ]
            ]),
            new Clue([[Blue, CBone, Any], AnyRow, AnyRow]),
            new Clue([[Any, Blue, CBowl], AnyRow, AnyRow]),
            new Clue([[CBall, Any, Any], [GBone, Any, Any], [Blue, Any, Any]]),
            new Clue([[Any, Any, Red], [Any, Any, RBone], [Any, Any, CBowl]]),
            new Clue([AnyRow, AnyRow, [CBone, Red, Any]]),
            new Clue([AnyRow, AnyRow, [Any, CBall, Green]])
        );
    }
}
