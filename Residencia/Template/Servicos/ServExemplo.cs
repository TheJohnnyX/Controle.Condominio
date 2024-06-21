using Exemplo;

namespace Exemplo
{
    public interface IServExemplo
    {
        ExemploDTO Exemplo(int id);
    }

    public class ServExemplo : IServExemplo
    {
        private readonly DataContext _dataContext;

        public ServExemplo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ExemploDTO Exemplo(int id)
        {
            var exemploDto = new ExemploDTO()
            {
                Texto = "Hello world!"
            };

            return exemploDto;
        }
    }
}
