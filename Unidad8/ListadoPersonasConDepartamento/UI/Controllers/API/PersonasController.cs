using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaUseCases _personaUseCases;

        public PersonasController(IPersonaUseCases personaUseCases)
        {
            _personaUseCases = personaUseCases;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult salida;
            List<Persona> listadoCompleto = new List<Persona>();

            try
            {

                listadoCompleto = _personaUseCases.getListadoPersonas();
                if (listadoCompleto.Count() == 0)
                {
                    salida = NoContent();
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }
            catch
            {
                salida = BadRequest();
            }
            return salida;

        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Intentamos obtener la persona por su ID
                var persona = _personaUseCases.getPersona(id);
                if (persona == null)
                {
                    return NotFound(); // Si no se encuentra la persona, se devuelve 404
                }
                return Ok(persona); // Si se encuentra, se devuelve con un código 200 OK
            }
            catch (Exception ex)
            {
                // En caso de error, se devuelve un error 500 con un mensaje
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // POST api/<PersonasController>
        [HttpPost]
        public IActionResult Post([FromBody] Persona persona)
        {
            try
            {
                if (persona == null)
                {
                    return BadRequest("Persona no válida.");
                }

                // Llamamos al caso de uso para crear la persona
                int result = _personaUseCases.addPersona(persona);

                if (result > 0)
                {
                    // Si se crea correctamente, devolvemos un código 201 (creado)
                    return CreatedAtAction(nameof(Get), new { id = persona.Id }, persona);
                }

                return BadRequest("No se pudo crear la persona.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Persona persona)
        {
            if (persona == null || persona.Id != id)
            {
                return BadRequest("Los datos de la persona son incorrectos.");
            }

            try
            {
                var existingPersona = _personaUseCases.getPersona(id);

                if (existingPersona == null)
                {
                    return NotFound(); // Si no se encuentra la persona, se devuelve 404
                }

                // Llamamos al caso de uso para actualizar la persona
                int result = _personaUseCases.updatePersona(id, persona);

                if (result > 0)
                {
                    // Si la actualización fue exitosa, devolvemos NoContent (204)
                    return NoContent();
                }

                return BadRequest("No se pudo actualizar la persona.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var persona = _personaUseCases.getPersona(id);

                if (persona == null)
                {
                    return NotFound(); // Si la persona no se encuentra, se devuelve 404
                }

                // Llamamos al caso de uso para eliminar la persona
                int result = _personaUseCases.deletePersona(id);

                if (result > 0)
                {
                    // Si la eliminación fue exitosa, devolvemos NoContent (204)
                    return NoContent();
                }

                return BadRequest("No se pudo eliminar la persona.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, se devuelve un error 500 con el mensaje de error
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
