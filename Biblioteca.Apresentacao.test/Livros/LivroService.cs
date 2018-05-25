using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Aplicacao.Livros
{
    public class LivroService : ILivroService
    {
        private ILivroRepositorio _repositorio;

        public LivroService(ILivroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Livro Adicionar(Livro livro)
        {
            livro.Validar();
            return _repositorio.Salvar(livro);
        }

        public Livro Editar(Livro livro)
        {
            if (livro.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            livro.Validar();
            return _repositorio.Editar(livro);
        }

        public Livro Get(long id)
        {
            if (id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            return _repositorio.Get(id);
        }

        public IEnumerable<Livro> GetAll()
        {
            return _repositorio.GetAll();
        }

        public void Delete(Livro livro)
        {
            if (livro.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            _repositorio.Delete(livro);
        }
    }
}
