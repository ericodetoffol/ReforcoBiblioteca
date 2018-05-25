using Biblioteca.Dominio.Excecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Emprestimos
{
    public class ExcecaoLivroIndisponivel : ExcecaoDeNegocio
    {
        public ExcecaoLivroIndisponivel() : base("Livro nao esta disponivel")
        {

        }
    }
}
