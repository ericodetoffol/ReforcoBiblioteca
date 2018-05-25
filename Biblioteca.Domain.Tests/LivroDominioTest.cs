using Biblioteca.Dominio.Livros;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Tests
{
    [TestFixture]
    public class LivroDominioTest
    {
        [Test]
        public void Livro_Valid_ShouldBeSuccess()
        {
            Livro livro = new Livro();
            livro.Id = 1;
            livro.Titulo = "Testes Automatizados";
            livro.Tema = "Programaçao";
            livro.Autor = "Jose";
            livro.Volume = 1;
            livro.DataPublicacao = DateTime.Now.AddYears(-3);
            livro.Disponibilidade = true;
            livro.Validar();
        }

        [Test]
        public void Livro_Valid_FieldNullOrEmpty_ShouldBeFail()
        {
            Livro livro = new Livro();
            livro.Id = 1;
            livro.Titulo = "";
            livro.Tema = "Programaçao";
            livro.Autor = "Jose";
            livro.Volume = 1;
            livro.DataPublicacao = DateTime.Now.AddYears(-3);
            livro.Disponibilidade = true;
            Action executeAction = livro.Validar;
            executeAction.Should().Throw<ExcecaoCampoNulo>();
        }

        [Test]
        public void Livro_VolumeInvalido_ShouldBeFail()
        {
            Livro livro = new Livro();
            livro.Id = 1;
            livro.Titulo = "Testes Automatizados";
            livro.Tema = "Programaçao";
            livro.Autor = "Jose";
            livro.Volume = -1;
            livro.DataPublicacao = DateTime.Now.AddYears(-3);
            livro.Disponibilidade = true;
            Action executeAction = livro.Validar;
            executeAction.Should().Throw<ExcecaoVolumeInvalido>();
        }

        [Test]
        public void Livro_Valid_DataPublicacao_GreaterThanCurrentDate_ShouldBeFail()
        {
            Livro livro = new Livro();
            livro.Id = 1;
            livro.Titulo = "Testes Automatizados";
            livro.Tema = "Programaçao";
            livro.Autor = "Jose";
            livro.Volume = 1;
            livro.DataPublicacao = DateTime.Now.AddYears(1);
            livro.Disponibilidade = true;
            Action executeAction = livro.Validar;
            executeAction.Should().Throw<ExcecaoDataDeLivro>();
        }
    }
}
