using Biblioteca.Aplicacao.Livros;
using Biblioteca.Commons.Tests;
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

namespace Biblioteca.Aplicacao.Tests
{
    [TestFixture]
    public class LivroAplicacaoTest
    {
        private Mock<ILivroRepositorio> _mockRepositorio;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = new Mock<ILivroRepositorio>();
        }

        [Test]
        public void LivroService_Adicionar_ShouldBeOk()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            _mockRepositorio.Setup(m => m.Salvar(modelo)).Returns(new Livro() { Id = 1 });
            LivroService service = new LivroService(_mockRepositorio.Object);
            Livro resultado = service.Adicionar(modelo);
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepositorio.Verify(repository => repository.Salvar(modelo));
        }

        [Test]
        public void LivroService_Adicionar_CampoNulo_ShouldBeFail()
        {
            Livro modelo = ObjectMotherLivro.GetLivroInvalidoTitulo();
            LivroService service = new LivroService(_mockRepositorio.Object);
            Action comparison = () => service.Adicionar(modelo);
            comparison.Should().Throw<ExcecaoCampoNulo>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void LivroService_Editar_ShouldBeOk()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            modelo.Id = 1;
            _mockRepositorio.Setup(m => m.Editar(modelo)).Returns(new Livro() { Id = 1, Autor = "Autor" });            
            LivroService service = new LivroService(_mockRepositorio.Object);
            Livro resultado = service.Editar(modelo);
            resultado.Should().NotBeNull();
            resultado.Autor.Should().Be("Autor");
            _mockRepositorio.Verify(repository => repository.Editar(modelo));
        }

        [Test]
        public void LivroService_Get_ShouldBeOk()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            modelo.Id = 1;
            _mockRepositorio.Setup(m => m.Get(1)).Returns(new Livro()
            {
                Id = 1,
                Titulo = "Novo",
                Tema = "Tema",
                Autor = "Autor",
                Volume = 1,
                DataPublicacao = DateTime.Now.AddYears(-3),
                Disponibilidade = true
            });
            LivroService service = new LivroService(_mockRepositorio.Object);
            Livro resultado = service.Get(1);
            resultado.Should().NotBeNull();
            _mockRepositorio.Verify(repository => repository.Get(1));
        }

        [Test]
        public void LivroService_Get_IdInvalido_ShouldBeFail()
        {
            LivroService service = new LivroService(_mockRepositorio.Object);
            Action comparison = () => service.Get(0);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void LivroService_Editar_CampoNulo_ShouldBeFail()
        {
            Livro modelo = ObjectMotherLivro.GetLivroInvalidoTitulo();
            modelo.Id = 1;
            LivroService service = new LivroService(_mockRepositorio.Object);
            Action comparison = () => service.Editar(modelo);
            comparison.Should().Throw<ExcecaoCampoNulo>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void LivroService_Editar_IdInvalido_ShouldBeFail()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            LivroService service = new LivroService(_mockRepositorio.Object);
            Action comparison = () => service.Editar(modelo);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
               
        [Test]
        public void LivroService_GetAll_ShouldBeOk()
        {
            _mockRepositorio.Setup(m => m.GetAll()).Returns(new List<Livro>());
            LivroService service = new LivroService(_mockRepositorio.Object);
            List<Livro> resultado = service.GetAll() as List<Livro>;
            resultado.Should().NotBeNull();
            _mockRepositorio.Verify(repository => repository.GetAll());
        }

        [Test]
        public void LivroService_Delete_ShouldBeOk()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            modelo.Id = 1;
            _mockRepositorio.Setup(m => m.Delete(modelo));
            LivroService service = new LivroService(_mockRepositorio.Object);
            service.Delete(modelo);
            _mockRepositorio.Verify(repository => repository.Delete(modelo));
        }

        [Test]
        public void LivroService_Delete_IdInvalido_ShouldBeOk()
        {
            Livro modelo = ObjectMotherLivro.GetLivro();
            LivroService service = new LivroService(_mockRepositorio.Object);
            Action comparison = () => service.Delete(modelo);
            comparison.Should().Throw<ExcecaoIdentifivadorIndefinido>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
