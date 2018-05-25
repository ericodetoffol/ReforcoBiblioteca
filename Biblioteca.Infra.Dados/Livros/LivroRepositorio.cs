using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using Biblioteca.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Dados.Livros
{
    public class LivroRepositorio : ILivroRepositorio
    {
        public void Delete(Livro livro)
        {
            if (livro.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            string sql = "DELETE FROM TBLivro WHERE Id = @Id";
            Db.Delete(sql, new object[] { "Id", livro.Id });
        }

        public Livro Get(long id)
        {
            if (id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            string sql = "SELECT * FROM TBLivro WHERE Id = @Id";
            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Livro> GetAll()
        {
            string sql = "SELECT * FROM TBLivro";
            return Db.GetAll(sql, Make);
        }

        public Livro Salvar(Livro livro)
        {
            livro.Validar();
            string sql = "INSERT INTO TBLivro(Titulo,Tema,Autor,Volume,DataPublicacao,Disponibilidade) " +
            "VALUES (@Titulo, @Tema, @Autor, @Volume, @DataPublicacao, @Disponibilidade)";
            livro.Id = Db.Insert(sql, Take(livro, false));
            return livro;
        }

        public Livro Editar(Livro livro)
        {
            if (livro.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            livro.Validar();
            string sql = "UPDATE TBLivro SET Titulo = @Titulo, Tema = @Tema, Autor = @Autor, Volume = @Volume, " +
                "DataPublicacao = @DataPublicacao,  Disponibilidade = @Disponibilidade WHERE Id = @Id";
            Db.Update(sql, Take(livro, true));
            return livro;
        }

        private static Func<IDataReader, Livro> Make = reader =>
           new Livro
           {
               Id = Convert.ToInt32(reader["Id"]),
               Titulo = reader["Titulo"].ToString(),
               Tema = reader["Tema"].ToString(),
               Autor = reader["Autor"].ToString(),
               Volume = Convert.ToInt32(reader["Volume"]),
               DataPublicacao = Convert.ToDateTime(reader["DataPublicacao"]),
               Disponibilidade = Convert.ToBoolean(reader["Disponibilidade"])
           };

        private object[] Take(Livro livro, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@Titulo", livro.Titulo,
                "@Tema", livro.Tema,
                "@Autor", livro. Autor,
                "@Volume", livro.Volume,
                "@DataPublicacao", livro.DataPublicacao,
                "@Disponibilidade", livro.Disponibilidade,
                "@Id", livro.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@Titulo", livro.Titulo,
                "@Tema", livro.Tema,
                "@Autor", livro. Autor,
                "@Volume", livro.Volume,
                "@DataPublicacao", livro.DataPublicacao,
                "@Disponibilidade", livro.Disponibilidade
              };
            }
            return parametros;
        }
    }
}
