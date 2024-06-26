using MicroPromover.DTO;
using static System.Net.WebRequestMethods;

namespace Promocao
{
    public interface IMovimentoFinanceiroHelper
    {
        void InserirMovimentoEntrada(InserirMovtoCaixaDTO movtoCaixa);
        void InserirMovimentoSaida(InserirMovtoCaixaDTO movtoCaixa);
    }

    public class MovimentoFinanceiroHelper : IMovimentoFinanceiroHelper
    {
        public void InserirMovimentoEntrada(InserirMovtoCaixaDTO movtoCaixa)
        {
            var httpClient = new HttpClient();

            var url = "http://localhost:5289/api/MovimentacaoCaixa/InserirMovimentoDeEntrada";

            var result = httpClient.PostAsJsonAsync(url, movtoCaixa).Result;

            if (!result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;

                throw new Exception("Erro ao inserir movimento financeiro: " + content);
            }
        }

        public void InserirMovimentoSaida(InserirMovtoCaixaDTO movtoCaixa)
        {
            var httpClient = new HttpClient();

            var url = "http://localhost:5289/api/MovimentacaoCaixa/InserirMovimentoDeSaida";

            var result = httpClient.PostAsJsonAsync(url, movtoCaixa).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao inserir movimento financeiro");
            }
        }
    }
}
