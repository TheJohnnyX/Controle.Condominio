using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace Tizza
{
    public interface IResidenciaHelper
    {
        ResidenciaDTO RetornarResidencia(int codigoResidencia);

        ResidenciaDTO RetornarResidenciaComCache(int codigoResidencia);
    }

    public class ResidenciaHelper : IResidenciaHelper
    {
        private const string _residenciaController = "api/Residencia/";
        private IMemoryCache _memoryCache;

        public ResidenciaHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public ResidenciaDTO RetornarResidencia(int codigoResidencia)
        {
            var httpClient = new HttpClient();

            var urlResidencia = BuscarUrlResidencia();

            var url = urlResidencia + _residenciaController + codigoResidencia;


            var resposta = httpClient.GetAsync(url).Result;

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Residencia " + codigoResidencia + " não encontrada.");
            }

            if (resposta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }

            var residencia = resposta.Content.ReadFromJsonAsync<ResidenciaDTO>().Result;

            InserirResidenciaNoCache(residencia);

            return residencia;
        }

        public void InserirResidenciaNoCache(ResidenciaDTO residenciaDto)
        {
            _memoryCache.Set("Residencia" + residenciaDto.Id, residenciaDto, TimeSpan.FromHours(1));
        }

        public ResidenciaDTO RetornarResidenciaComCache(int codigoResidencia)
        {
            var residencia = _memoryCache.Get<ResidenciaDTO>("Residencia" + codigoResidencia);

            if (residencia != null)
            {
                return residencia;
            }

            residencia = RetornarResidencia(codigoResidencia);

            return residencia;
        }
        public string BuscarUrlResidencia()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string url = configuration["UrlResidencia"];

            return url;
        }
    }
}
