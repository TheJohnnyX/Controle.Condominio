
namespace Tizza
{
    public interface IServCondominio
    {
        void Inserir(InserirCondominioDTO inserirCondominioDTO);

        CondominioViewDTO BuscarCondominio(int id);

        List<Condominio> BuscarTodas();
    }

    public class ServCondominio : IServCondominio
    {
        public DataContext _dataContext;
        private IResidenciaHelper _residenciaHelper;
        private IMoradorHelper _moradorHelper;

        public ServCondominio(DataContext dataContext, IResidenciaHelper residenciaHelper, IMoradorHelper moradorHelper)
        {
            _dataContext = dataContext;
            _residenciaHelper = residenciaHelper;
            _moradorHelper = moradorHelper;
        }

        public void Inserir(InserirCondominioDTO inserirCondominioDto)
        {
            var condominio = new Condominio();

            ValidarResidencia(inserirCondominioDto.CodigoResidencia);

            ValidarMorador(inserirCondominioDto.CodigoMorador);

            condominio.Valor = inserirCondominioDto.Valor;
            condominio.CodigoResidencia = inserirCondominioDto.CodigoResidencia;
            condominio.CodigoMorador = inserirCondominioDto.CodigoMorador;

            _dataContext.Add(condominio);
            _dataContext.SaveChanges();
        }

        public void ValidarResidencia(int codigoResidencia)
        {
            var residencia = _residenciaHelper.RetornarResidencia(codigoResidencia);

            if (residencia == null)
            {
                throw new Exception("A residencia não existe!");
            }
        }

        public void ValidarMorador(int codigoMorador)
        {
            var residencia = _moradorHelper.RetornarMorador(codigoMorador);

            if (residencia == null)
            {
                throw new Exception("O morador não existe!");
            }
        }

        public CondominioViewDTO BuscarCondominio(int id)
        {
            var condominio = _dataContext.Condominio.FirstOrDefault(p => p.Id == id);

            if (condominio == null)
            {
                throw new Exception("Condomínio não encontrado");
            }

            var residencia = _residenciaHelper.RetornarResidenciaComCache(condominio.CodigoResidencia);

            var view = new CondominioViewDTO();

            view.Id = condominio.Id;
            view.Valor = condominio.Valor;
            view.CodigoResidencia = condominio.CodigoResidencia;

            view.CodigoMorador = condominio.CodigoMorador;

            return view;
        }

        public List<Condominio> BuscarTodas()
        {
            var listaCondominio = _dataContext.Condominio.ToList();

            return listaCondominio;
        }
    }
}
