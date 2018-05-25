using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Excecao
{
    public class ExcecaoIdentifivadorIndefinido : ExcecaoDeNegocio
    {
        public ExcecaoIdentifivadorIndefinido() : base("O id não pode ser vazio")
        {

        }
    }
}
