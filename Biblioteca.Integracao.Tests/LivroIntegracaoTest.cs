using Biblioteca.Aplicacao.Livros;
using Biblioteca.Commons.Tests;
using Biblioteca.Commons.Tests.Base;
using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using Biblioteca.Infra.Dados.Livros;
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
    public class LivroIntegracaoTest
    {
        private ILivroService _service;
        private ILivroRepositorio _repositorio;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repositorio = new LivroRepositorio();
            _service = new LivroService(_repositorio);
        }

        [Test]
        public void LivroIntegracao_Adicionar_ShouldBeOk()
        {
            int biggerThan = 0;
            int sizeListExpected = 2;
            Livro livro = _service.Adicionar(ObjectMotherLivro.GetLivro());
            livro.Id.Should().BeGreaterThan(biggerThan);
            var last = _service.Get(livro.Id);
            last.Should().NotBeNull();
            var posts = _service.GetAll();
            posts.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void LivroIntegracao_Adicionar_Volume_Invalido_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivroInvalidoVolume();
            Action comparison = () => _service.Adicionar(livro);
            comparison.Should().Throw<ExcecaoVolumeInvalido>();
        }

        [Test]
        public void LivroIntegracao_Adicionar_DataPublicacao_Menor_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            livro.DataPublicacao = DateTime.Now.AddYears(7);
            Action comparison = () => _service.Adicionar(livro);
            comparison.Should().Throw<ExcecaoDataDeLivro>();
        }

        [Test]
        public void LivroIntegracao_Editar_ShouldBeOk()
        {
            int idSearch = 1;
            Livro livro = _service.Get(idSearch);
            string oldTema = livro.Tema;
            livro.Tema = "Programação Tdd";
            Livro result = _service.Editar(livro);
            result.Should().NotBeNull();
            result.Id.Should().Be(livro.Id);
            result.Tema.Should().NotBe(oldTema);
        }

        [Test]
        public void LivroIntegracao_Editar_DataPublicacao_Menor_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            livro.Id = 1;
            livro.DataPublicacao = DateTime.Now.AddYears(7);
            Action executeAction = () => _service.Editar(livro);
            executeAction.Should().Throw<ExcecaoDataDeLivro>();
        }

        [Test]
        public void LivroIntegracao_Editar_IdInvalido_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            Action executeAction = () => _service.Editar(livro);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void LivroIntegracao_Get_ShouldBeOk()
        {
            int idSearch = 1;
            Livro livro = _service.Get(idSearch);
            livro.Should().NotBeNull();
            List<Livro> livros = _service.GetAll() as List<Livro>;
            var found = livros.Find(x => x.Id == livro.Id);
            livro.Id.Should().Be(found.Id);
        }

        [Test]
        public void LivroIntegracao_Get_IdInvalido_ShouldBeFail()
        {
            int idSearch = 0;
            Action actionExecute = () => _service.Get(idSearch);
            actionExecute.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void LivroIntegracao_GetAll_ShouldBeOk()
        {
            int sizeListExpected = 1;
            var livro = _service.GetAll();
            livro.Should().NotBeNull();
            livro.Count().Should().Be(sizeListExpected);
        }

        [Test]
        public void LivroIntegracao_Delete_ShouldBeOk()
        {
            int idSearch = 1;
            Livro livro = _service.Get(idSearch);
            _service.Delete(livro);
            Livro result = _service.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void LivroIntegracao_Delete_IdInvalido_ShouldBeOk()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            Action executeAction = () => _service.Delete(livro);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

    }
}
