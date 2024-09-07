using ClueMastersDetectiveGame;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var levels = new List<Level>() { LevelRepository.Level1(), LevelRepository.Level2() };

foreach (var level in levels) 
{
    Grid solutionCopy = new(level.Solution.Grid.Tokens.Clone() as Token[,]);

    // Check if the solution is valid
    bool isSolutionValid = level.Solution.Validate(solutionCopy);
    if (!isSolutionValid)
    {
        throw new InvalidOperationException("Level 1 solution is not valid against its own clues.");
    }

    // Check each clue individually
    foreach (var clue in level.Clues)
    {
        bool isClueValid = clue.Validate(solutionCopy);
        if (!isClueValid)
        {
            throw new InvalidOperationException($"A clue in Level 1 is not valid against the solution.");
        }
    }

    Console.WriteLine("Level validation passed successfully.");
}


app.MapGet("/levels", () => Results.Ok(LevelRepository.Levels));

app.MapGet("/levels/{id:int}", (int id) =>
{
    var puzzle = LevelRepository.GetLevelById(id);
    if (puzzle == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(puzzle);
});

app.MapPost("/levels/{id:int}/check", (int id, Grid userGrid) =>
{
    var puzzle = LevelRepository.GetLevelById(id);
    if (puzzle == null)
    {
        return Results.NotFound();
    }

    var isCorrect = puzzle.Solution.Validate(userGrid);
    return Results.Ok(new { isCorrect });
});

app.MapGet("/levels/{id:int}/solution", (int id) =>
{
    var puzzle = LevelRepository.GetLevelById(id);
    if (puzzle == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(puzzle.Solution);
});

app.Run();
