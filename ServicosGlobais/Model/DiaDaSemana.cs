using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Model
{
    public class DiaDaSemana
    {
        public int Ordem { get; set; }
        public string Nome { get; set; }

        public bool isAtivo { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
