using MicroPromover.DTO;
using static System.Net.WebRequestMethods;

namespace Promocao
{
    public interface IPizzaHelper
    {
        void InformarPromocaoNaPizza(int codigoPizza, InformarPromocaoNaPizzaDTO InformarPromocaoDto);
    }

    public class PizzaHelper : IPizzaHelper
    {
        public void InformarPromocaoNaPizza(int codigoPizza, InformarPromocaoNaPizzaDTO InformarPromocaoDto)
        {
            var httpClient = new HttpClient();

            var url = "http://localhost:5189/api/Pizza/InformarPromocaoPizza/" + codigoPizza;

            var result = httpClient.PostAsJsonAsync(url, InformarPromocaoDto).Result;

            if (!result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;

                throw new Exception("Erro ao inserir movimento financeiro: " + content);
            }
        }
    }
}
