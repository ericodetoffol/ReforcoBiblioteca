using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Excecao
{
    public class ExcecaoOperacaoNaoSuportada : ExcecaoDeNegocio
    {
        public ExcecaoOperacaoNaoSuportada() : base("Operação não suportada")
        {
        }
    }
}
