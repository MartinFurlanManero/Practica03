using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Dominio;
using ProduccionBack.Servcios;

namespace Practica03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaManager servicio;

        public FacturaController()
        {
            servicio = new FacturaManager();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(servicio.GetFacturas());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(servicio.GetFacturaById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            try
            {
                if (factura == null)
                {
                    return BadRequest("Se esperaba una factura completa");
                }
                if (servicio.SaveFactura(factura))
                    return Ok("Factura registrado con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la factura!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }

        [HttpDelete("{nroFactura}")]
        public IActionResult Delete(int nroFactura)
        {
            var result = servicio.DeleteFactura(nroFactura);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Factura factura)
        {
            try
            {
                if (factura == null)
                {
                    return BadRequest("Se esperaba una factura completa");
                }
                if (servicio.Update(factura))
                    return Ok("Articulo actualizado con éxito!");
                else
                    return StatusCode(500, "No se pudo actualizar el articulo!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }
    }
}
