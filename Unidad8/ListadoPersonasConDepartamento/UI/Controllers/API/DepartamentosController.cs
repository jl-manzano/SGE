using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace UI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoUseCases _departamentoUseCases;

        // Constructor para inyectar el caso de uso de Departamento
        public DepartamentosController(IDepartamentoUseCases departamentoUseCases)
        {
            _departamentoUseCases = departamentoUseCases;
        }

        // GET: api/<DepartamentosController>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult salida;
            List<Departamento> listadoDepartamentos;

            try
            {
                // Recupera el listado de departamentos desde el caso de uso
                listadoDepartamentos = _departamentoUseCases.getDepartamentos();

                if (listadoDepartamentos.Count == 0)
                {
                    salida = NoContent(); // Si no hay departamentos, devuelve un 204 (sin contenido)
                }
                else
                {
                    salida = Ok(listadoDepartamentos); // Devuelve un 200 OK con los departamentos
                }
            }
            catch
            {
                salida = BadRequest(); // En caso de error, devuelve un 400 (solicitud incorrecta)
            }

            return salida;
        }

        // GET api/<DepartamentosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Intentamos obtener el departamento por su ID
                var departamento = _departamentoUseCases.getDepartamento(id);

                if (departamento == null)
                {
                    return NotFound(); // Si no se encuentra el departamento, devuelve un 404
                }

                return Ok(departamento); // Si se encuentra, devuelve el departamento con un 200 OK
            }
            catch (Exception ex)
            {
                // En caso de error, devuelve un error 500 con el mensaje
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // POST api/<DepartamentosController>
        [HttpPost]
        public IActionResult Post([FromBody] Departamento departamento)
        {
            try
            {
                if (departamento == null)
                {
                    return BadRequest("Departamento no válido."); // Si el cuerpo está vacío o es nulo
                }

                // Llamamos al caso de uso para agregar el departamento
                int result = _departamentoUseCases.addDepartamento(departamento);

                if (result > 0)
                {
                    // Si el departamento se crea correctamente, devolvemos un 201 (creado)
                    return CreatedAtAction(nameof(Get), new { id = departamento.IdDepartamento }, departamento);
                }

                return BadRequest("No se pudo crear el departamento."); // Si no se pudo crear
            }
            catch (Exception ex)
            {
                // En caso de error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // PUT api/<DepartamentosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Departamento departamento)
        {
            if (departamento == null || departamento.IdDepartamento != id)
            {
                return BadRequest("Los datos del departamento son incorrectos."); // Si los datos no son correctos
            }

            try
            {
                var existingDepartamento = _departamentoUseCases.getDepartamento(id);

                if (existingDepartamento == null)
                {
                    return NotFound(); // Si no se encuentra el departamento, devuelve un 404
                }

                // Llamamos al caso de uso para actualizar el departamento
                int result = _departamentoUseCases.updateDepartamento(id, departamento);

                if (result > 0)
                {
                    // Si la actualización fue exitosa, devolvemos NoContent (204)
                    return NoContent();
                }

                return BadRequest("No se pudo actualizar el departamento."); // Si no se pudo actualizar
            }
            catch (Exception ex)
            {
                // En caso de error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // DELETE api/<DepartamentosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var departamento = _departamentoUseCases.getDepartamento(id);

                if (departamento == null)
                {
                    return NotFound(); // Si el departamento no se encuentra, devuelve 404
                }

                // Llamamos al caso de uso para eliminar el departamento
                int result = _departamentoUseCases.deleteDepartamento(id);

                if (result > 0)
                {
                    // Si la eliminación fue exitosa, devolvemos NoContent (204)
                    return NoContent();
                }

                return BadRequest("No se pudo eliminar el departamento."); // Si no se pudo eliminar
            }
            catch (Exception ex)
            {
                // En caso de error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
