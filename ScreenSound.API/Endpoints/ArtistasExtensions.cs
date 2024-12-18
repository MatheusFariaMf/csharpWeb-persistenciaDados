﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.DB;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndPointsArtistas(this WebApplication app)
        {
            #region Endpoint Artistas
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                return Results.Ok(EntityListToResponseList(dal.Listar()));
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(EntityToResponse(artista));
                }
            });

            app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.nome, artistaRequest.bio);
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

            app.MapPut("Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
            {
                Artista? artistaASerAtualizado = dal.RecuperarPor(a => a.Id == artistaRequestEdit.id);
                if (artistaASerAtualizado is null)
                    return Results.NotFound();
                else
                {
                    if (artistaRequestEdit.nome is not null)
                        artistaASerAtualizado.Nome = artistaRequestEdit.nome;

                    if (artistaRequestEdit.bio is not null)
                        artistaASerAtualizado.Bio = artistaRequestEdit.bio;


                    dal.Atualizar(artistaASerAtualizado);
                    return Results.Ok();
                }
            });

            static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
            {
                return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
            }

            static ArtistaResponse EntityToResponse(Artista artista)
            {
                return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
            }
            #endregion Endpoint Artistas
        }
    }
}
