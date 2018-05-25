using Biblioteca.Dominio.Emprestimos;
using Biblioteca.Dominio.Livros;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Tests
{
    [TestFixture]
    public class EmprestimoTest
    {
        private Mock<Livro> _mockLivro;

        [SetUp]
        public void Setup()
        {
            _mockLivro = new Mock<Livro>();
        }

        [Test]
        public void Emprestimo_Valid_ShouldBeSuccess()
        {
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.Id = 1;
            emprestimo.NomeCliente = "Isabel";
            emprestimo.Livro = _mockLivro.Object;
            _mockLivro.Object.Disponibilidade = true;
            emprestimo.DataDevolucao = DateTime.Now.AddDays(10);
            emprestimo.Validar();
        }

        [Test]
        public void Emprestimo_Valid_ClienteName_NullOrEmpty_ShouldBeFail()
        {
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.Id = 1;
            emprestimo.NomeCliente = "";
            emprestimo.Livro = _mockLivro.Object;
            emprestimo.DataDevolucao = DateTime.Now.AddDays(10);
            Action executeAction = emprestimo.Validar;
            executeAction.Should().Throw<ExcecaoNomeClienteNulo>();
        }

        [Test]
        public void Emprestimo_Valid_Livro_Unavailable_ShouldBeFail()
        {
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.Id = 1;
            emprestimo.NomeCliente = "Isabel";
            emprestimo.Livro = _mockLivro.Object;
            _mockLivro.Object.Disponibilidade = false;
            emprestimo.DataDevolucao = DateTime.Now.AddDays(10);
            Action executeAction = emprestimo.Validar;
            executeAction.Should().Throw<ExcecaoLivroIndisponivel>();
        }


        [Test]
        public void Emprestimo_Valid_DataDevolucao_LowerThanCurrent_ShouldBeFail()
        {
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.Id = 1;
            emprestimo.NomeCliente = "Isabel";
            emprestimo.Livro = _mockLivro.Object;
            emprestimo.DataDevolucao = DateTime.Now.AddDays(-10);
            Action executeAction = emprestimo.Validar;
            executeAction.Should().Throw<ExcecaoDataMenorQueAtual>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockLivro = null;
        }
    }
}
