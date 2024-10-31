using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.DB
{
    internal class DAL<T> where T : class
    {
        private readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }

        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public void Atualizar(T objeto)
        {
            try
            {
                var returno = context.Set<T>().Update(objeto);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
        }

        public void Exclui(T objeto)
        {
            try
            {
                context.Set<T>().Remove(objeto);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
        }

        public T? RecuperarPor(Func<T, bool> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }

        public IList<T>? RecuperarVariosPor(Func<T, bool> condicao)
        {
            return context.Set<T>().Where(condicao).ToList();
        }

    }
}
