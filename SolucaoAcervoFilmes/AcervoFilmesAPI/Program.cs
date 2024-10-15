using AcervoFilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

List<Filme> filmes = [];

app.MapGet("/", () => "api dos filmes favoritos da ana");

// crud:

app.MapPost("/api/cadastrar", async ([FromBody] Filme filme, AppDbContext context) => 
{
    context.Add(filme);
    await context.SaveChangesAsync();
    return Results.Created($"/filme/{filme.Id}", filme);
    /*
    filmes.Add(filme);
    return Results.Ok(filme);
    */
});

app.MapGet("/api/filme/{id}", async ([FromRoute] string id, AppDbContext context) =>
{
    var filme = await context.Filmes
                                .Where(p => p.Id.Contains(id))
                                .ToListAsync();
    return filme.Any() ? Results.Ok(filme) : Results.NotFound("Nenhum filme encontrado.");
    /*
    Filme? filme = filmes.Find(x => x.Id == id);
    if (filme == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(filme);
    */
});

app.MapGet("/api/listar", async (AppDbContext context) =>
{
    var filmes = await context.Filmes.ToListAsync();
    return Results.Ok(filmes);
    /*
    if (filmes.Count > 0)
    {
        return Results.Ok(filmes);
    }
    return Results.NotFound();
    */
});

app.MapPut("/api/alterar/{id}", async ([FromRoute] string id, [FromBody] Filme filmeAlterado, AppDbContext context) =>
{
    var filme = await context.Filmes.FindAsync(id);
    if (filme == null)
    {
        return Results.NotFound();
    }
    
    filme.Nome = filmeAlterado.Nome;
    filme.Nota = filmeAlterado.Nota;
    filme.Diretor = filmeAlterado.Diretor;
    
    await context.SaveChangesAsync();
    return Results.Ok();
    /*
    Filme? filme = filmes.Find(x => x.Id == id);
    if (filme == null)
    {
        return Results.NotFound();
    }

    filme.Nome = filmeAlterado.Nome;
    filme.Nota = filmeAlterado.Nota;
    filme.Diretor = filmeAlterado.Diretor;

    return Results.Ok(filme);
    */
});

app.MapDelete("/api/deletar/{id}", async ([FromRoute] string id, AppDbContext context) =>
{
    var filme = await context.Filmes.FindAsync(id);
    if (filme == null)
    {
        return Results.NotFound();
    }

    context.Filmes.Remove(filme);
    await context.SaveChangesAsync();
    return Results.Ok();

    /*
    Filme? filme = filmes.Find(x => x.Id == id);
    if (filme == null)
    {
        return Results.NotFound();
    }

    filmes.Remove(filme);
    return Results.Ok();
    */
});

app.Run();
