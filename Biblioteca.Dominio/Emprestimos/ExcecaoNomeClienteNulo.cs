using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Emprestimos
{
    public class ExcecaoNomeClienteNulo : ExcecaoDeNegocio
    {
        public ExcecaoNomeClienteNulo() : base("O Nome do Cliente não pode ser nulo")
        {

        }
    }
}
