using Microsoft.AspNetCore.Mvc;

namespace Exemplo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExemploController : Controller
    {
        private IServExemplo _servExemplo;

        public ExemploController(IServExemplo servExemplo)
        {
            _servExemplo = servExemplo;
        }

        [Route("/api/[Controller]/{id}")]
        [HttpGet]
        public IActionResult Exemplo(int id)
        {
            try
            {
                var exemploDto = _servExemplo.Exemplo(id);

                return Ok(exemploDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
