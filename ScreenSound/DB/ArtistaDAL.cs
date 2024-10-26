using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.DB
{
    internal class ArtistaDAL
    {
        private readonly ScreenSoundContext context;

        public ArtistaDAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<Artista> ListarArtistas()
        {

            return context.Artistas.ToList();
        }

        public void AdicionarArtista(Artista artista)
        {
            context.Artistas.Add(artista);
            context.SaveChanges();  
        }
        /// <summary>
        /// Atualiza os dados de um determinado artista
        /// </summary>
        /// <param name="artista"></param>
        public void AtualizarArtista(Artista artista)
        {
            try
            {
                var returno = context.Artistas.Update(artista);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            
        }
        /// <summary>
        /// Exclui um artista do banco de dados
        /// </summary>
        /// <param name="artista"></param>
        public void ExcluiArtista(Artista artista)
        {
            try
            {
                context.Artistas.Remove(artista);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            
        }
        /// <summary>
        /// Recupera um artista pelo nome 
        /// </summary>
        /// <param name="artista"></param>
        public Artista? RecuperaPeloNome(string nomeArtista)
        {
            return context.Artistas.FirstOrDefault(a => a.Nome == nomeArtista);
        }

    }
}
