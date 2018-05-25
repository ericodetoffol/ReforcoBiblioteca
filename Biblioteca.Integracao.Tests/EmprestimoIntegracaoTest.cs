using Biblioteca.Aplicacao.Emprestimos;
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

namespace Biblioteca.Integracao.Tests
{
    [TestFixture]
    public class EmprestimoIntegracaoTest
    {
        private IEmprestimoService _service;
        private IEmprestimoRepositorio _repositorio;
        private Livro _livro;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repositorio = new EmprestimoRepositorio();
            _service = new EmprestimoService(_repositorio);

            _livro = ObjectMotherLivro.GetLivro();
            _livro.Id = 1;
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_ShouldBeOk()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            Emprestimo result = _service.Adicionar(emprestimo);
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_NomeCliente_Invalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimoInvalidoClienteNome();
            emprestimo.Livro = _livro;
            Action executeAction = () => _service.Adicionar(emprestimo);
            executeAction.Should().Throw<ExcecaoNomeClienteNulo>();
        }

        [Test]
        public void EmprestimoRepositorio_Salvar_LivroInvalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = false;
            Action executeAction = () => _service.Adicionar(emprestimo);
            executeAction.Should().Throw<ExcecaoLivroIndisponivel>();
        }


        [Test]
        public void EmprestimoRepositorio_Editar_NomeCliente_Invalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimoInvalidoClienteNome();
            emprestimo.Id = 1;
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = true;
            Action executeAction = () => _service.Editar(emprestimo);
            executeAction.Should().Throw<ExcecaoNomeClienteNulo>();
        }

        [Test]
        public void EmprestimoRepositorio_Editar_IdInvalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = true;
            Action executeAction = () => _service.Editar(emprestimo);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void EmprestimoRepositorio_Get_ShouldBeOk()
        {
            int idSearch = 1;
            Emprestimo result = _service.Get(idSearch);
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
        }

        [Test]
        public void EmprestimoRepositorio_Get_IdInvalido_ShouldBeFail()
        {
            int idSearch = 0;
            Action executeAction = () => _service.Get(idSearch);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void EmprestimoRepositorio_GetAll_ShouldBeOk()
        {
            int sizeListExpected = 1;
            var result = _service.GetAll();
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void EmprestimoRepositorio_Delete_ShouldBeOk()
        {
            int idSearch = 1;
            Emprestimo emprestimo = _service.Get(idSearch);
            emprestimo.Livro.Disponibilidade = true;
            _service.Delete(emprestimo);
            Emprestimo result = _service.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void EmprestimoRepositorio_Delete_IdInvalido_ShouldBeFail()
        {
            Emprestimo emprestimo = ObjectMotherEmprestimo.GetEmprestimo();
            emprestimo.Livro = _livro;
            _livro.Disponibilidade = true;
            Action executeAction = () => _service.Delete(emprestimo);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }
    }
}
