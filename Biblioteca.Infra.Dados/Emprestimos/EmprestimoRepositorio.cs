using Biblioteca.Dominio.Emprestimos;
using Biblioteca.Dominio.Excecao;
using Biblioteca.Dominio.Livros;
using Biblioteca.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Dados.Emprestimos
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        public void Delete(Emprestimo emprestimo)
        {
            if (emprestimo.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            string sql = "DELETE FROM TBEmprestimo WHERE Id = @Id";
            Db.Delete(sql, new object[] { "Id", emprestimo.Id });
        }

        public Emprestimo Get(long id)
        {
            if (id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            string sql = "SELECT * FROM TBEmprestimo WHERE Id = @Id";
            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Emprestimo> GetAll()
        {
            string sql = "SELECT * FROM TBEmprestimo";
            return Db.GetAll(sql, Make);
        }

        public Emprestimo Salvar(Emprestimo emprestimo)
        {
            emprestimo.Validar();
            string sql = "INSERT INTO TBEmprestimo (NomeCliente, LivroId, DataDevolucao) VALUES (@NomeCliente, @LivroId, @DataDevolucao)";
            emprestimo.Id = Db.Insert(sql, Take(emprestimo, false));
            return emprestimo;
        }

        public Emprestimo Editar(Emprestimo emprestimo)
        {
            emprestimo.Validar();
            if (emprestimo.Id < 1)
                throw new ExcecaoIdentifivadorIndefinido();
            string sql = "UPDATE TBEmprestimo SET NomeCliente = @NomeCliente, LivroId = @LivroId, DataDevolucao = @DataDevolucao WHERE Id = @Id";
            Db.Update(sql, Take(emprestimo, true));
            return emprestimo;
        }

        private static Func<IDataReader, Emprestimo> Make = reader =>
           new Emprestimo
           {
               Id = Convert.ToInt32(reader["Id"]),
               NomeCliente = reader["NomeCliente"].ToString(),
               Livro = new Livro
               {
                   Id = Convert.ToInt32(reader["LivroId"])
               },
               DataDevolucao = Convert.ToDateTime(reader["DataDevolucao"])
           };
        
        private object[] Take(Emprestimo emprestimo, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@NomeCliente", emprestimo.NomeCliente,
                "@LivroId", emprestimo.Livro.Id,
                "@DataDevolucao", emprestimo.DataDevolucao,
                "@Id", emprestimo.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                 "@NomeCliente", emprestimo.NomeCliente,
                "@LivroId", emprestimo.Livro.Id,
                "@DataDevolucao", emprestimo.DataDevolucao
              };
            }
            return parametros;
        }
    }
}
