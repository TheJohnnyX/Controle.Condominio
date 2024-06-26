using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocao
{
    public class PromoverTransacaoEfetivarAssincrona
    {
        public int Id { get; set; }

        public int CodigoPromover { get; set; }

        public bool ProcessamentoInterno { get; set; }

        public bool InserirMovimento { get; set; }

        public bool AtualizarPizza { get; set; }

        public string MensagemDeErroAtualizarPizza { get; set; }

        public EnumSituacaoTransacao SituacaoTransacao { get; set; }
    }
}
