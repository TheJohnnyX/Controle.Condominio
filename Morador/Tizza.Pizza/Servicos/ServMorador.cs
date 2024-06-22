using Tizza;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace Tizza
{
    public interface IServMorador
    {
        void Inserir(InserirMoradorDTO inserirMoradorDTO);
        
        MoradorView BuscarMorador(int id);

        List<Morador> BuscarTodas();
    }

    public class ServMorador : IServMorador
    {
        public DataContext _dataContext;
        private IResidenciaHelper _residenciaHelper;

        public ServMorador(DataContext dataContext, IResidenciaHelper residenciaHelper)
        {
            _dataContext = dataContext;
            _residenciaHelper = residenciaHelper;
        }

        public void Inserir(InserirMoradorDTO inserirMoradorDto)
        {
            var morador = new Morador();

            ValidarResidencia(inserirMoradorDto.CodigoResidencia);

            morador.CodigoResidencia = inserirMoradorDto.CodigoResidencia;
            morador.Nome = inserirMoradorDto.Nome;


            var verifica_morador = _dataContext.Morador.FirstOrDefault(p => p.CodigoResidencia == inserirMoradorDto.CodigoResidencia);

            if (verifica_morador == null)
            {
                _dataContext.Add(morador);

                _dataContext.SaveChanges();
            }
            else
            {
                throw new Exception("Já existe um morador cadastrado para essa residencia!");
            }
        }

        public void ValidarResidencia(int codigoResidencia)
        {
            var residencia = _residenciaHelper.RetornarResidencia(codigoResidencia);

            if (residencia == null)
            {
                throw new Exception("A residencia não existe!");
            }
        }

        public MoradorView BuscarMorador(int id)
        {
            var morador = _dataContext.Morador.FirstOrDefault(p => p.Id == id);

            if (morador == null)
            {
                throw new Exception("Pizza não encontrada");
            }

            var residencia= _residenciaHelper.RetornarResidenciaComCache(morador.CodigoResidencia);

            var view = new MoradorView();

            view.Id = morador.Id;
            view.CodigoResidencia = morador.CodigoResidencia;
            view.Nome = morador.Nome;
            view.CodigoCondominio = morador.CodigoCondominio;
            view.Residencia = residencia;

            return view;
        }

        public List<Morador> BuscarTodas()
        {
            var listaMorador = _dataContext.Morador.ToList();

            return listaMorador;
        }
    }
}
