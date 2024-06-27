using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace Tizza
{
    public interface IMoradorHelper
    {
        MoradorDTO RetornarMorador(int codigoMorador);

        MoradorDTO RetornarMoradorComCache(int codigoMorador);
    }

    public class MoradorHelper : IMoradorHelper
    {
        private const string _moradorController = "api/Morador/";
        private IMemoryCache _memoryCache;

        public MoradorHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public MoradorDTO RetornarMorador(int codigoMorador)
        {
            var httpClient = new HttpClient();

            var urlMorador = BuscarUrlMorador();

            var url = urlMorador + _moradorController + codigoMorador;


            var resposta = httpClient.GetAsync(url).Result;

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Morador " + codigoMorador + " não encontrada.");
            }

            if (resposta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }

            var Morador = resposta.Content.ReadFromJsonAsync<MoradorDTO>().Result;

            InserirMoradorNoCache(Morador);

            return Morador;
        }

        public void InserirMoradorNoCache(MoradorDTO MoradorDto)
        {
            _memoryCache.Set("Morador" + MoradorDto.Id, MoradorDto, TimeSpan.FromHours(1));
        }

        public MoradorDTO RetornarMoradorComCache(int codigoMorador)
        {
            var Morador = _memoryCache.Get<MoradorDTO>("Morador" + codigoMorador);

            if (Morador != null)
            {
                return Morador;
            }

            Morador = RetornarMorador(codigoMorador);

            return Morador;
        }
        public string BuscarUrlMorador()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string url = configuration["UrlMorador"];

            return url;
        }
    }
}
