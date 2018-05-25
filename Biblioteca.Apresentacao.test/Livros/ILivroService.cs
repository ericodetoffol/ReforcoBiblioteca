using Biblioteca.Dominio.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Aplicacao.Livros
{
    public interface ILivroService
    {
        Livro Adicionar(Livro livro);
        Livro Editar(Livro livro);
        Livro Get(long id);
        IEnumerable<Livro> GetAll();
        void Delete(Livro livro);
    }
}
