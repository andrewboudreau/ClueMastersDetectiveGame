using ClueMastersDetectiveGame;

using System.Text;
Console.OutputEncoding = Encoding.UTF8;

foreach (var level in LevelRepository.Levels)
{
    Console.WriteLine($"Validating Level {level.Id}...");
    ConsoleRenderer.RenderGrid(level.Solution.Grid);
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
            Console.WriteLine("Level validation failed.");
            Console.WriteLine($"A clue in Level {level.Id} is not valid against the solution.");
            continue;
        }
    }

    Console.WriteLine("Level validation passed successfully.");
}


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
