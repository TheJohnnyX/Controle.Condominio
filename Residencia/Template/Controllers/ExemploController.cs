using Microsoft.AspNetCore.Mvc;
using Residencia.DTO;
using Residencia.Servicos;

namespace Residencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : Controller
    {
        private IServResidencia _servResidencia;

        public ResidenciaController(IServResidencia servResidencia)
        {
            _servResidencia = servResidencia;
        }

        [HttpPost]
        public IActionResult Inserir(InserirResidenciaDTO inserirResidenciaDto)
        {
            try
            {
                var residencia = new Residencia();

                residencia.Endereco = inserirResidenciaDto.Endereco;
                residencia.NomeLocal = inserirResidenciaDto.NomeLocal;
                residencia.Morador = inserirResidenciaDto.Morador;
                residencia.TaxaCondominio = inserirResidenciaDto.TaxaCondominio;

                _servResidencia.Inserir(residencia);

                var retornoInsercao = new { Codigoresidencia = residencia.Id };

                return Ok(retornoInsercao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[Controller]/{id}")]
        [HttpPut]
        public IActionResult Editar(int id, EditarResidenciaDTO editarResidenciaDto)
        {
            try
            {
                var residencia = _servResidencia.BuscarResidencia(id);

                residencia.Endereco = editarResidenciaDto.Endereco;
                residencia.NomeLocal = editarResidenciaDto.NomeLocal;
                residencia.Morador = editarResidenciaDto.Morador;
                residencia.TaxaCondominio = editarResidenciaDto.TaxaCondominio;

                _servResidencia.Editar(residencia);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[Controller]/{id}")]
        [HttpGet]
        public IActionResult BuscarResidencia(int id)
        {
            try
            {
                var residencia = _servResidencia.BuscarResidencia(id);

                return Ok(residencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/[Controller]")]
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var listaResidencia = _servResidencia.BuscarTodos();

                return Ok(listaResidencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
