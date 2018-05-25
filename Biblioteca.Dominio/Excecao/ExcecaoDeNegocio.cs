using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Excecao
{
    public class ExcecaoDeNegocio : Exception
    {
        public ExcecaoDeNegocio(string message) : base(message)
        {

        }
    }
}
