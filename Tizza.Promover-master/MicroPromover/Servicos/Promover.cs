using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocao
{
    public class Promover
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVigencia { get; set; }

        public EnumStatusPromover Status { get; set; }

        public int CodigoPizza { get; set; }
    }

    public enum EnumStatusPromover
    {
        EmAberto = 1,
        Efetivado = 2,
        Cancelado = 9
    }
}
