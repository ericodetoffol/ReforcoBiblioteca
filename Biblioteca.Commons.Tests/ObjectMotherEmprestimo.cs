using Biblioteca.Dominio.Emprestimos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Commons.Tests
{
    public static partial class ObjectMotherEmprestimo
    {
        public static Emprestimo GetEmprestimo()
        {
            return new Emprestimo()
            {
                NomeCliente = "Cliente",
                DataDevolucao = DateTime.Now.AddDays(20)
            };
        }

        public static Emprestimo GetEmprestimoInvalidoClienteNome()
        {
            return new Emprestimo()
            {
                NomeCliente = "",
                DataDevolucao = DateTime.Now.AddDays(20)
            };
        }
    }
}
