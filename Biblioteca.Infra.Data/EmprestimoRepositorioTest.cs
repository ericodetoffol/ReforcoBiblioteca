using Biblioteca.Commons.Tests;
using Biblioteca.Commons.Tests.Base;
using Biblioteca.Dominio.Emprestimos;
using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using Biblioteca.Infra.Dados.Emprestimos;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Dados.Test
{
    [TestFixture]
    public class EmprestimoRepositorioTest
    {
        private IEmprestimoRepositorio _repositorio;
        private Livro _livro;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repositorio = new EmprestimoRepositorio();
            _livro = ObjectMotherLivro.GetLivro();
            _livro.Id = 1;
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_ShouldBeOk()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            Emprestimo result = _repositorio.Salvar(emprestimo);
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_NomeCliente_Nulo_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimoInvalidoClienteNome();
            emprestimo.Livro = _livro;
            Action executeAction = () => _repositorio.Salvar(emprestimo);
            executeAction.Should().Throw<ExcecaoNomeClienteNulo>();
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_Livro_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = false;
            Action executeAction = () => _repositorio.Salvar(emprestimo);
            executeAction.Should().Throw<ExcecaoLivroIndisponivel>();
        }

        [Test]
        public void EmprestimoRepositorio_Editar_Livro_ShouldBeOk()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Id = 1;
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = false;
            Action executeAction = () => _repositorio.Editar(emprestimo);
            executeAction.Should().Throw<ExcecaoLivroIndisponivel>();
        }

        [Test]
        public void EmprestimoRepositorio_Editar_IdInvalido_ShouldBeOk()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            Action executeAction = () => _repositorio.Editar(emprestimo);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void EmprestimoRepositorio_Get_ShouldBeOk()
        {
            int biggerThan = 0;
            int idSearch = 1;
            Emprestimo result = _repositorio.Get(idSearch);
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Id.Should().BeGreaterThan(biggerThan);
        }

        [Test]
        public void EmprestimoRepositorio_Get_IdInvalido_ShouldBeFail()
        {
            int idSearch = 0;
            Action actionExecute = () => _repositorio.Get(idSearch);
            actionExecute.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void EmprestimoRepositorio_GetAll_ShouldBeOk()
        {
            int sizeListExpected = 1;
            int idFirstEmprestimoListExpected = 1;
            List<Emprestimo> result = _repositorio.GetAll() as List<Emprestimo>;
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
            result.First().Id.Should().Be(idFirstEmprestimoListExpected);
        }

        [Test]
        public void EmprestimoRepositorio_Delete_ShouldBeOk()
        {
            int idSearch = 1;
            Emprestimo emprestimo = _repositorio.Get(idSearch);
            _repositorio.Delete(emprestimo);
            Emprestimo result = _repositorio.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void EmprestimoRepositorio_Delete_IdInvalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            Action executeAction = () => _repositorio.Delete(emprestimo);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }
    }
}
