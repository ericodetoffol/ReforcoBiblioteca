using Biblioteca.Dominio.Emprestimos;
using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Aplicacao.Emprestimos
{
    public class EmprestimoService : IEmprestimoService
    {
        private IEmprestimoRepositorio _repositorio;

        public EmprestimoService(IEmprestimoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Emprestimo Adicionar(Emprestimo emprestimo)
        {
            emprestimo.Validar();
            return _repositorio.Salvar(emprestimo);
        }

        public Emprestimo Editar(Emprestimo emprestimo)
        {
            if (emprestimo.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            emprestimo.Validar();
            return _repositorio.Editar(emprestimo);
        }

        public Emprestimo Get(long id)
        {
            if (id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            return _repositorio.Get(1);
        }

        public IEnumerable<Emprestimo> GetAll()
        {
            return _repositorio.GetAll();
        }

        public void Delete(Emprestimo emprestimo)
        {
            if (emprestimo.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            _repositorio.Delete(emprestimo);
        }
    }
}
