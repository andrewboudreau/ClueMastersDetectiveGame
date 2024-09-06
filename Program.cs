using ClueMastersDetectiveGame;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

<<<<<<< HEAD
// Validation checks
var level1 = LevelRepository.Level1();
Grid solutionCopy = new(level1.Solution.Grid.Tokens.Clone() as Token[,]);

// Check if the solution is valid
bool isSolutionValid = level1.Solution.Validate(solutionCopy, level1.Clues);
if (!isSolutionValid)
{
    throw new InvalidOperationException("Level 1 solution is not valid against its own clues.");
}

// Check each clue individually
foreach (var clue in level1.Clues)
{
    bool isClueValid = clue.Validate(solutionCopy);
    if (!isClueValid)
    {
        throw new InvalidOperationException($"A clue in Level 1 is not valid against the solution.");
    }
}

Console.WriteLine("Level 1 validation passed successfully.");
=======

var level1 = LevelRepository.Level1();

level1.Solution.Check()

>>>>>>> 9cd9f411f3b9a865de61a37a37459f19c2e2e468

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

    var isCorrect = puzzle.Solution.Validate(userGrid, puzzle.Clues);
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
