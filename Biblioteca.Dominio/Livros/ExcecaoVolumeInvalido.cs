using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Livros
{
    public class ExcecaoVolumeInvalido : ExcecaoDeNegocio
    {
        public ExcecaoVolumeInvalido() : base("O volume esta menor que 0!")
        {

        }
    }
}
