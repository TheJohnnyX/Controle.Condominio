using Microsoft.AspNetCore.Mvc;

namespace Tizza
{
    [ApiController]
    [Route("[controller]")]
    public class CondominioController : ControllerBase
    {
        private IServCondominio _servCondominio;

        public CondominioController(IServCondominio servCondominio)
        {
            _servCondominio = servCondominio;
        }

        [HttpPost]
        public ActionResult Inserir([FromBody] InserirCondominioDTO inserirDto)
        {
            try
            {
                _servCondominio.Inserir(inserirDto);

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/[controller]/{id}")]
        [HttpGet]
        public ActionResult BuscarCondominio(int id)
        {
            try
            {
                var pizza = _servCondominio.BuscarCondominio(id);

                return Ok(pizza);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/[controller]")]
        [HttpGet]
        public ActionResult BuscarTodas()
        {
            try
            {
                var listaCondominio = _servCondominio.BuscarTodas();

                return Ok(listaCondominio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
