using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Filme> filmes = [];

app.MapGet("/", () => "api dos filmes favoritos da ana");

// crud:

app.MapPost("/api/cadastrar", ([FromBody] Filme filme) => 
{
    filmes.Add(filme);
    return Results.Ok(filme);
});

app.MapGet("/api/listar", () =>
{
    if (filmes.Count > 0)
    {
        return Results.Ok(filmes);
    }
    return Results.NotFound();
});

app.MapPut("/api/alterar/{id}", ([FromRoute] string id, [FromBody] Filme filmeAlterado) =>
{
    Filme? filme = filmes.Find(x => x.Id == id);
    if (filme == null)
    {
        return Results.NotFound();
    }

    filme.Nome = filmeAlterado.Nome;
    filme.Nota = filmeAlterado.Nota;
    filme.Diretor = filmeAlterado.Diretor;

    return Results.Ok(filme);
});

app.MapDelete("/api/deletar/{id}", ([FromRoute] string id) =>
{
    Filme? filme = filmes.Find(x => x.Id == id);
    if (filme == null)
    {
        return Results.NotFound();
    }

    filmes.Remove(filme);
    return Results.Ok();
});

app.Run();
