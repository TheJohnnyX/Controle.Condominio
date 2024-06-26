using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocao
{
    public class PromoverTransacaoEfetivar
    {
        public int Id { get; set; }

        public int CodigoPromover { get; set; }

        public bool ProcessamentoInterno { get; set; }

        public bool InserirMovimento { get; set; }

        public bool AtualizarPizza { get; set; }

        public EnumSituacaoTransacao SituacaoTransacao { get; set; }
    }

    public enum EnumSituacaoTransacao
    {
        EmAndamento,
        Concluido
    }

}
