using Residencia.DTO;

namespace Residencia.Servicos
{
    public interface IServResidencia
    {
        void Inserir(Residencia residencia);
        void Editar(Residencia residencia);
        Residencia BuscarResidencia(int id);
        List<Residencia> BuscarTodos();
    }

    public class ServResidencia : IServResidencia
    {
        private readonly DataContext _dataContext;

        public ServResidencia(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Inserir(Residencia residencia)
        {
            _dataContext.Add(residencia);

            _dataContext.SaveChanges();
        }

        public void Editar(Residencia residencia)
        {
            _dataContext.SaveChanges();
        }

        public Residencia BuscarResidencia(int id)
        {
            var residencia = _dataContext.residencias.FirstOrDefault(x => x.Id == id);

            return residencia;
        }

        public List<Residencia> BuscarTodos()
        {
            var listaResidencia = _dataContext.residencias.ToList();

            return listaResidencia;
        }
    }
}
