using Microsoft.AspNetCore.Mvc;

namespace Promocao
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoverController : ControllerBase
    {
        public IServPromover _servPromover;

        public PromoverController(IServPromover servPromover)
        {
            _servPromover = servPromover;
        }

        [HttpPost]
        public IActionResult Inserir(InserirPromoverDTO inserirPromoverDto)
        {
            try
            {
                _servPromover.Inserir(inserirPromoverDto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[controller]/{id}")]
        [HttpPut]
        public IActionResult Editar(int id, [FromBody] EditarPromoverDTO editarPromoverDTO)
        {
            try
            {
                _servPromover.Editar(id, editarPromoverDTO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[controller]/Efetivar/{id}")]
        [HttpPost]
        public IActionResult Efetivar(int id)
        {
            try
            {
                _servPromover.Efetivar(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[controller]/EfetivarAssincrono/{id}")]
        [HttpPost]
        public IActionResult EfetivarAssincrono(int id)
        {
            try
            {
                _servPromover.EfetivarAssincrono(id);

                return Ok("O processamento está em andamento. Aguarde...");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[controller]/VerificarAndamentoProcessamento/{CodigoPromover}")]
        [HttpGet]
        public IActionResult VerificarAndamentoProcessamento(int CodigoPromover)
        {
            try
            {
                var andamento = _servPromover.VerificarAndamentoProcessamento(CodigoPromover);

                return Ok(andamento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[controller]/Cancelar/{id}")]
        [HttpPost]
        public IActionResult Cancelar(int id)
        {
            try
            {
                _servPromover.Cancelar(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var listaPromocao = _servPromover.BuscarTodos();

                return Ok(listaPromocao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
