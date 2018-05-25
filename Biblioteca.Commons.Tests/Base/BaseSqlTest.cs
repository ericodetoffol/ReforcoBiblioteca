using Biblioteca.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Commons.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_TABLES =
             "TRUNCATE TABLE [dbo].[TBEmprestimo]; " +
             "DELETE FROM TBLivro DBCC CHECKIDENT('TBLivro',RESEED,0);";

        private const string INSERT_LIVRO = "INSERT INTO TBLivro(Titulo,Tema,Autor,Volume,DataPublicacao,Disponibilidade) " +
            "VALUES ('Livro Teste', 'Tema Teste', 'Autor Teste', 1, GETDATE(), 1)";
        private const string INSERT_EMPRESTIMO = "INSERT INTO TBEmprestimo (NomeCliente,LivroId,DataDevolucao) VALUES ('Nome Teste', 1, GETDATE())";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TABLES);
            Db.Update(INSERT_LIVRO);
            Db.Update(INSERT_EMPRESTIMO);
        }
    }
}
