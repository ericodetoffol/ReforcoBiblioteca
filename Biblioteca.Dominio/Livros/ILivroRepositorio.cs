using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Livros
{
    public interface ILivroRepositorio
    {
        Livro Salvar(Livro livro);
        Livro Editar(Livro livro);
        Livro Get(long id);
        IEnumerable<Livro> GetAll();
        void Delete(Livro livro);
    }
}
