using ScreenSound.DB;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuMusicasPorAnoLancamento : Menu
    {
        public override void Executar(DAL<Musica> musicaDAL)
        {
            base.Executar(musicaDAL);
            ExibirTituloDaOpcao("Exibindo todas as músicas de determinado ano");
            Console.Write("Digite o ano de lançamento das músicas que quer exibir: ");
            string anoLancamentoMusica = Console.ReadLine()!;
            var musicasRecuperadas = musicaDAL.RecuperarVariosPor(musica => musica.AnoLancamento == Convert.ToInt32(anoLancamentoMusica));
            if (musicasRecuperadas is not null)
            {
                Console.WriteLine($"\nMusicas do ano de {anoLancamentoMusica}:");
                foreach (var musica in musicasRecuperadas)
                {
                    Console.WriteLine($"\n{musica.Nome}");
                }
                Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"\nNão há musicas do ano {anoLancamentoMusica} cadastradas");
                Console.WriteLine("Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
