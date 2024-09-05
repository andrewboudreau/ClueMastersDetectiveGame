using ClueMastersDetectiveGame;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var level1 = LevelRepository.Level1();

level1.Solution.Check()


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

    var isCorrect = puzzle.Solution.Check(userGrid, puzzle.Clues);
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
