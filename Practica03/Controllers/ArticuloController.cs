using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Dominio;
using ProduccionBack.Servcios;




namespace Practica03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private IArticuloManager servicio;

        public ArticuloController()
        {
            servicio = new ArticuloManager();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(servicio.GetArticulos());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("Se esperaba un articulo completo");
                }
                if (servicio.SaveArticulo(articulo))
                    return Ok("Articulo registrado con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la orden!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = servicio.DeleteArticulo(id);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Articulo articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("Se esperaba un articulo completo");
                }
                if (servicio.UpdateArticulo(articulo))
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
