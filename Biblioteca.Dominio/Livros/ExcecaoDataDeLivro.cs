using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Livros
{
    public class ExcecaoDataDeLivro : ExcecaoDeNegocio
    {
        public ExcecaoDataDeLivro() : base("A Data deve ser menor que a data atual!")
        {

        }
    }
}
