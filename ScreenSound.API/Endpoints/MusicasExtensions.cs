using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.DB;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointsMusicas(this WebApplication app)
        {
            #region Endpoint Musicas
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                return Results.Ok(EntityListToResponseList(dal.Listar()));
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
                    return Results.Ok(EntityToResponse(musica));
                }
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.nome, musicaRequest.anoLancamento);
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

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
            {
                Musica? musicaASerAtualizado = dal.RecuperarPor(a => a.Id == musicaRequestEdit.id);
                if (musicaASerAtualizado is null)
                    return Results.NotFound();
                else
                {

                    if (musicaRequestEdit.nome is not null)
                        musicaASerAtualizado.Nome = musicaRequestEdit.nome;

                    if (musicaRequestEdit.anoLancamento is not null)
                        musicaASerAtualizado.AnoLancamento = musicaRequestEdit.anoLancamento;

                    dal.Atualizar(musicaASerAtualizado);
                    return Results.Ok();
                }
            });

            static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
            {
                return musicaList.Select(a => EntityToResponse(a)).ToList();
            }

            static MusicaResponse EntityToResponse(Musica musica)
            {
                return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
            }
            #endregion Endpoint Musicas
        }
    }
}
