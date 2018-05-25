using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Emprestimos
{
    public interface IEmprestimoRepositorio
    {
        Emprestimo Salvar(Emprestimo emprestimo);
        Emprestimo Editar(Emprestimo emprestimo);
        Emprestimo Get(long id);
        IEnumerable<Emprestimo> GetAll();
        void Delete(Emprestimo emprestimo);
    }
}
