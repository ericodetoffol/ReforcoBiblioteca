using Biblioteca.Aplicacao.Emprestimos;
using Biblioteca.Commons.Tests;
using Biblioteca.Dominio.Emprestimos;
using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Tests
{
    [TestFixture]
    public class EmprestimoAplicacaoTest
    {
        private Mock<IEmprestimoRepositorio> _mockRepositorio;
        private Livro _livro;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = new Mock<IEmprestimoRepositorio>();
            _livro = new Livro();
            _livro.Disponibilidade = true;
        }

        [Test]
        public void EmprestimoService_Adicionar_ShouldBeOK()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimo();
            modelo.Livro = _livro;
            _mockRepositorio.Setup(m => m.Salvar(modelo)).Returns(new Emprestimo() { Id = 1 });
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Emprestimo resultado = service.Adicionar(modelo);
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepositorio.Verify(repositorio => repositorio.Salvar(modelo));
        }

        [Test]
        public void EmprestimoService_Adicionar_NomeClienteNulo_ShouldBeFail()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimoInvalidoClienteNome();
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Action comparison = () => service.Adicionar(modelo);
            comparison.Should().Throw<ExcecaoNomeClienteNulo>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void EmprestimoService_Editar_ShouldBeOk()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimo();
            modelo.Id = 1;
            modelo.Livro = _livro;
            _mockRepositorio.Setup(m => m.Editar(modelo)).Returns(new Emprestimo() { Id = 1, NomeCliente = "Nome do Cliente" });
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Emprestimo resultado = service.Editar(modelo);
            resultado.Should().NotBeNull();
            _mockRepositorio.Verify(repositorio => repositorio.Editar(modelo));
        }

        [Test]
        public void EmprestimoService_Get_ShouldBeOk()
        {
            _mockRepositorio.Setup(m => m.Get(1)).Returns(new Emprestimo()
            {
                Id = 1,
                NomeCliente = "Cliente Nome",
                DataDevolucao = DateTime.Now.AddDays(10)
            });
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Emprestimo resultado = service.Get(1);
            resultado.Should().NotBeNull();
            _mockRepositorio.Verify(repositorio => repositorio.Get(1));
        }

        [Test]
        public void EmprestimoService_Get_IdInvalido_ShouldBeFail()
        {
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Action comparison = () => service.Get(0);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void EmprestimoService_Editar_NomeClienteInvalido_ShouldBeFail()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimoInvalidoClienteNome();
            modelo.Id = 1;
            modelo.Livro = _livro;
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Action comparison = () => service.Editar(modelo);
            comparison.Should().Throw<ExcecaoNomeClienteNulo>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void EmprestimoService_Editar_IdInvalido_ShouldBeFail()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimo();
            modelo.Livro = _livro;
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Action comparison = () => service.Editar(modelo);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void EmprestimoService_GetAll_ShouldBeOk()
        {
            _mockRepositorio.Setup(m => m.GetAll()).Returns(new List<Emprestimo>());
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            List<Emprestimo> resultado = service.GetAll() as List<Emprestimo>;
            resultado.Should().NotBeNull();
            _mockRepositorio.Verify(repositorio => repositorio.GetAll());
        }

        [Test]
        public void EmprestimoService_Delete_ShouldBeOk()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimo();
            modelo.Id = 1;
            modelo.Livro = _livro;
            _mockRepositorio.Setup(m => m.Delete(modelo));
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            service.Delete(modelo);
            _mockRepositorio.Verify(repositorio => repositorio.Delete(modelo));
        }

        [Test]
        public void EmprestimoService_Delete_IdInvalido_ShouldBeFail()
        {
            Emprestimo modelo = ObjectMotherEmprestimo.GetEmprestimo();
            modelo.Livro = _livro;
            EmprestimoService service = new EmprestimoService(_mockRepositorio.Object);
            Action comparison = () => service.Delete(modelo);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
