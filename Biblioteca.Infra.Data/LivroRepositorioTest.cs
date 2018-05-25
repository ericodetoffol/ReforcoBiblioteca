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

namespace Biblioteca.Infra.Dados.Test
{
    [TestFixture]
    public class LivroRepositorioTest
    {
        private ILivroRepositorio _repositorio;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repositorio = new LivroRepositorio();
        }

        [Test]
        public void LivroRepositorio_Salvar_ShouldBeOk()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            Livro result = _repositorio.Salvar(livro);
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void LivroRepositorio_Salvar_Invalido_Volume_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivroInvalidoVolume();
            Action executeAction = () => _repositorio.Salvar(livro);
            executeAction.Should().Throw<ExcecaoVolumeInvalido>();
        }

        [Test]
        public void LivroRepositorio_Editar_ShouldBeOk()
        {
            int idSearch = 1;
            Livro livro = _repositorio.Get(idSearch);
            livro.Id = 1;
            string TituloAntigo = livro.Titulo;
            livro.Titulo = "Titulo";
            Livro result = _repositorio.Editar(livro);
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Titulo.Should().NotBe(TituloAntigo);
        }

        [Test]
        public void LivroRepositorio_Editar_Invalido_Volume_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivroInvalidoVolume();
            livro.Id = 1;
            Action executeAction = () => _repositorio.Editar(livro);
            executeAction.Should().Throw<ExcecaoVolumeInvalido>();
        }

        [Test]
        public void LivroRepositorio_Editar_IdInvalidoo_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            Action executeAction = () => _repositorio.Editar(livro);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void LivroRepositorio_Get_ShouldBeOk()
        {
            int biggerThan = 0;
            int idSearch = 1;
            Livro result = _repositorio.Get(idSearch);
            result.Should().NotBeNull();
            result.Id.Should().Be(idSearch);
            result.Id.Should().BeGreaterThan(biggerThan);
        }

        [Test]
        public void LivroRepositorio_Get_IdInvalidoo_ShouldBeFail()
        {
            int idSearch = 0;
            Action actionExecute = () => _repositorio.Get(idSearch);
            actionExecute.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }

        [Test]
        public void LivroRepositorio_GetAll_ShouldBeOk()
        {
            int sizeListExpected = 1;
            int idFirstLivroListExpected = 1;
            List<Livro> result = _repositorio.GetAll() as List<Livro>;
            result.Should().NotBeNull();
            result.Count().Should().Be(sizeListExpected);
            result.First().Id.Should().Be(idFirstLivroListExpected);
        }

        [Test]
        public void LivroRepositorio_Delete_ShouldBeOk()
        {
            int idSearch = 1;
            Livro livro = _repositorio.Get(idSearch);
            _repositorio.Delete(livro);
            Livro result = _repositorio.Get(idSearch);
            result.Should().BeNull();
        }

        [Test]
        public void LivroRepositorio_Delete_IdInvalidoo_ShouldBeFail()
        {
            Livro livro = ObjectMotherLivro.GetLivro();
            Action executeAction = () => _repositorio.Delete(livro);
            executeAction.Should().Throw<ExcecaoIdentifivadorIndefinido>();
        }
    }
}
