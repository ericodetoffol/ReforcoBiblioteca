using Biblioteca.Dominio.Livros;
using Biblioteca.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Emprestimos
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public Livro Livro { get; set; }
        public DateTime DataDevolucao { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(NomeCliente))
                throw new ExcecaoNomeClienteNulo();
            if (DataDevolucao.CompareDadosPequenos())
                throw new ExcecaoDataMenorQueAtual();
            if (Livro.Disponibilidade == false)
                throw new ExcecaoLivroIndisponivel();
        }
    }
}
