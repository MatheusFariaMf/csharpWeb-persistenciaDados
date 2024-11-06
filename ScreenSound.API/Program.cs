using Microsoft.AspNetCore.Mvc;
using ScreenSound.DB;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Injeções de dependências
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL < Artista > dal, string nome) =>
{
    var artista =  dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(artista);
    }
});

app.MapPost("/Artistas", ([FromServices] DAL < Artista > dal, [FromBody]Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok();
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    Artista? artista = dal.RecuperarPor(a => a.Id == id);
    if (artista is null) 
    {
        return Results.NotFound();
    }
    else
    {
        dal.Exclui(artista);
        return Results.NoContent();
    }
});

app.MapPut("Artistas", ([FromServices] DAL<Artista> dal, [FromBody]Artista artista) =>
{
    Console.WriteLine(artista);
    Artista? artistaASerAtualizado = dal.RecuperarPor(a => a.Id == artista.Id);
    if(artistaASerAtualizado is null)
        return Results.NotFound();
    else
    {
        if (artista.Nome is not null)
            artistaASerAtualizado.Nome = artista.Nome;
        
        if (artista.Bio is not null)
            artistaASerAtualizado.Bio = artista.Bio;

        if (artista.FotoPerfil is not null)
            artistaASerAtualizado.FotoPerfil = artista.FotoPerfil;

        if (artista.Musicas is not null)
            artistaASerAtualizado.Musicas = artista.Musicas;

        dal.Atualizar(artistaASerAtualizado);
        return Results.Ok();
    }
});


app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
{
    var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (musica is null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(musica);
    }
});

app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
{
    Musica? musica = dal.RecuperarPor(a => a.Id == id);
    if (musica is null)
    {
        return Results.NotFound();
    }
    else
    {
        dal.Exclui(musica);
        return Results.NoContent();
    }
});

app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
{
    Console.WriteLine(musica);
    Musica? musicaASerAtualizado = dal.RecuperarPor(a => a.Id == musica.Id);
    if (musicaASerAtualizado is null)
        return Results.NotFound();
    else
    {
        if (musica.Nome is not null)
            musicaASerAtualizado.Nome = musica.Nome;

        if (musica.AnoLancamento is not null)
            musicaASerAtualizado.AnoLancamento = musica.AnoLancamento;

        if (musica.Artista is not null)
            musicaASerAtualizado.Artista = musica.Artista;

        dal.Atualizar(musicaASerAtualizado);
        return Results.Ok();
    }
});



app.Run();
