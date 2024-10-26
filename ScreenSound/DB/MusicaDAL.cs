using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.DB
{
    internal class MusicaDAL
    {
        public readonly ScreenSoundContext context;

        public MusicaDAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<Musica> ListarMusicas()
        {

            return context.Musicas.ToList();
        }

        public void AdicionarMusica(Musica musica)
        {
            context.Musicas.Add(musica);
            context.SaveChanges();
        }
        /// <summary>
        /// Atualiza os dados da musica
        /// </summary>
        /// <param name="artista"></param>
        public void AtualizarMusica(Musica musica)
        {
            try
            {
                var returno = context.Musicas.Update(musica);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }

        }
        /// <summary>
        /// Exclui uma música
        /// </summary>
        /// <param name="artista"></param>
        public void ExcluiMusica(Musica musica)
        {
            try
            {
                context.Musicas.Remove(musica);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }

        }

        public Musica? RecuperarPeloNome(string nome)
        {
            return context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));

        }
    }
}
