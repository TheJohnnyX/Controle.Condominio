using Microsoft.AspNetCore.Mvc;
using Tizza;

namespace Tizza
{
    [ApiController]
    [Route("[controller]")]
    public class MoradorController : ControllerBase
    {
        private IServMorador _servMorador;

        public MoradorController(IServMorador servMorador)
        {
            _servMorador = servMorador;
        }

        [HttpPost]
        public ActionResult Inserir([FromBody] InserirMoradorDTO inserirDto)
        {
            try
            {
                _servMorador.Inserir(inserirDto);

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/[controller]/{id}")]
        [HttpGet]
        public ActionResult BuscarMorador(int id)
        {
            try
            {
                var pizza = _servMorador.BuscarMorador(id);

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
                var listaMorador = _servMorador.BuscarTodas();

                return Ok(listaMorador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("/api/[controller]/InformarPromocaoPizza/{codigoPizza}")]
        //[HttpPost()]
        //public ActionResult InformarPromocaoPizza(int codigoPizza, [FromBody] InformarPromocaoPizzaDTO informarPromocaoPizzaDto)
        //{
        //    try
        //    {
        //        _servPizza.InformarPromocaoPizza(codigoPizza, informarPromocaoPizzaDto);
        //        return Ok("Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
