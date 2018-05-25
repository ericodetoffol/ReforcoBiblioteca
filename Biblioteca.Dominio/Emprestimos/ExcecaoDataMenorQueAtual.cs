using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Emprestimos
{
    public class ExcecaoDataMenorQueAtual : ExcecaoDeNegocio
    {
        public ExcecaoDataMenorQueAtual() : base("A data de devolução deve ser maior que a atual")
        {

        }
    }
}
