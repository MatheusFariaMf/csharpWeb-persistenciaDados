using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.DB;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GenerosExtensions
    {
        public static void AddEndPointsGeneros(this WebApplication app)
        {
            #region Endpoint Generos
            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
            {
                return Results.Ok(EntityListToResponseList(dal.Listar()));
            });

            app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
            {
                var Genero = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (Genero is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(EntityToResponse(Genero));
                }
            });

            app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromServices] DAL<Genero> dalGenero, [FromBody] GeneroRequest GeneroRequest) =>
            {
                var Genero = new Genero()
                {
                    Nome = GeneroRequest.Nome,
                    Descricao = GeneroRequest.Descricao
                };
                dal.Adicionar(Genero);
                return Results.Ok();
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
            {
                Genero? Genero = dal.RecuperarPor(a => a.Id == id);
                if (Genero is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    dal.Exclui(Genero);
                    return Results.NoContent();
                }
            });

            app.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit GeneroRequestEdit) =>
            {
                Genero? GeneroASerAtualizado = dal.RecuperarPor(a => a.Id == GeneroRequestEdit.Id);
                if (GeneroASerAtualizado is null)
                    return Results.NotFound();
                else
                {

                    if (GeneroRequestEdit.Nome is not null)
                        GeneroASerAtualizado.Nome = GeneroRequestEdit.Nome;

                    if (GeneroRequestEdit.Descricao is not null)
                        GeneroASerAtualizado.Descricao = GeneroRequestEdit.Descricao;

                    dal.Atualizar(GeneroASerAtualizado);
                    return Results.Ok();
                }
            });

            static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> GeneroList)
            {
                return GeneroList.Select(a => EntityToResponse(a)).ToList();
            }

            static GeneroResponse EntityToResponse(Genero Genero)
            {
                return new GeneroResponse(Genero.Id, Genero.Nome!, Genero.Descricao!);
            }
            #endregion Endpoint Generos
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }

            }

            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
        }
    }
}
