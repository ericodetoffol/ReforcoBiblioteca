using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infraestrutura
{
    public static class DadosAjuda
    {
        public static bool CompareDadosPequenos(this DateTime dt)
        {
            int resultado = DateTime.Compare(dt, DateTime.Now);
            if (resultado <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
