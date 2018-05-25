using Biblioteca.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Livros
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tema { get; set; }
        public string Autor { get; set; }
        public int Volume { get; set; }
        public DateTime DataPublicacao { get; set; }
        public bool Disponibilidade { get; set; }

        public void Validar()
        {
           if (string.IsNullOrEmpty(Titulo))
                throw new ExcecaoCampoNulo();
           if (string.IsNullOrEmpty(Tema))
                throw new ExcecaoCampoNulo();
           if (string.IsNullOrEmpty(Autor))
                throw new ExcecaoCampoNulo();
           if (Volume < 1)
                throw new ExcecaoVolumeInvalido();
           if (!DataPublicacao.CompareDadosPequenos())
                throw new ExcecaoDataDeLivro();
        }
    }
}
