using Biblioteca.Dominio.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Commons.Tests
{
    public static partial class ObjectMotherLivro
    {
        public static Livro GetLivro()
        {
            return new Livro()
            {
                Titulo = "Novo livro",
                Tema = "Tema",
                Autor = "Autor",
                Volume = 1,
                DataPublicacao = DateTime.Now.AddYears(-3),
                Disponibilidade = true
            };
        }

        public static Livro GetLivroInvalidoTitulo()
        {
            return new Livro()
            {
                Titulo = "",
                Tema = "Tema",
                Autor = "Autor",
                Volume = 1,
                DataPublicacao = DateTime.Now.AddYears(-3),
                Disponibilidade = true
            };
        }

        public static Livro GetLivroInvalidoTema()
        {
            return new Livro()
            {
                Titulo = "Novo livro",
                Tema = "Tem",
                Autor = "Autor",
                Volume = 1,
                DataPublicacao = DateTime.Now.AddYears(-3),
                Disponibilidade = true
            };
        }

        public static Livro GetLivroInvalidoVolume()
        {
            return new Livro()
            {
                Titulo = "Novo livro",
                Tema = "Tema",
                Autor = "Autor",
                Volume = -1,
                DataPublicacao = DateTime.Now.AddYears(-3),
                Disponibilidade = true
            };
        }
    }
}
