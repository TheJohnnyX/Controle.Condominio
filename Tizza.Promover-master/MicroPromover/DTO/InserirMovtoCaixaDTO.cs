namespace MicroPromover.DTO
{
    public class InserirMovtoCaixaDTO
    {
        public DateTime DataMovimento { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public int TipoVinculo { get; set; }

        public int? CodigoVinculacao { get; set; }
    }
}
