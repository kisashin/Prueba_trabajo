using Institucion_Educativa.Business;
using Institucion_Educativa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Institucion_Educativa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly EstudianteBusiness _estudianteBusiness;

        [HttpGet("test-db-connection")]
        public IActionResult TestDbConnection()
        {
            try
            {
                // Llamar a la capa de datos para verificar la conexión
                int numEstudiantes = _estudianteBusiness.ContarEstudiantes(); // Método que vamos a agregar
                return Ok($"Conexión a la base de datos exitosa. Número de estudiantes: {numEstudiantes}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        public EstudianteController()
        {
            string connectionString = "Server=DESKTOP-GF7KVM2\\SQLEXPRESS;Database=Institucion_educativa;Trusted_Connection=True;";
            _estudianteBusiness = new EstudianteBusiness(connectionString);
        }

        // GET: api/Estudiante
        [HttpGet]  // Obtener todos los estudiantes usando spObtenerEstudiantes
        public ActionResult<IEnumerable<EstudianteModel>> GetEstudiantes()
        {
            var estudiantes = _estudianteBusiness.ObtenerEstudiantes();
            if (estudiantes == null || estudiantes.Count == 0)
            {
                return NotFound("No se encontraron estudiantes.");
            }
            return Ok(estudiantes);
        }

        // GET: api/Estudiante/5
        [HttpGet("{id}")]  // Obtener un estudiante por su ID usando spConsultarEstudiante
        public ActionResult<EstudianteModel> GetEstudiante(int id)
        {
            var estudiante = _estudianteBusiness.ObtenerEstudiantePorId(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }

        [HttpGet("test-post")]
        public IActionResult TestPost()
        {
            Console.WriteLine("GET request recibida en el método de prueba.");
            return Ok("Prueba GET ejecutada con éxito.");
        }

        // POST: api/Estudiante
        [HttpPost]
        public IActionResult PostEstudiante(EstudianteModel estudiante)
        {
            try
            {
                // Mensaje de depuración para verificar si el POST llega al controlador
                Console.WriteLine("Se ha recibido una solicitud POST para crear un estudiante.");

                _estudianteBusiness.CrearEstudiante(estudiante);
                return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.EstudianteId }, estudiante);
            }
            catch (Exception ex)
            {
                // Imprimir cualquier error
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error en la creación del estudiante.");
            }
        }


        // PUT: api/Estudiante/5
        [HttpPut("{id}")]  // Actualizar un estudiante usando spActualizarEstudiante
        public IActionResult PutEstudiante(int id, EstudianteModel estudiante)
        {
            if (id != estudiante.EstudianteId)
            {
                return BadRequest();
            }
            _estudianteBusiness.ActualizarEstudiante(estudiante);
            return NoContent();
        }

        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")]  // Eliminar un estudiante usando spBorrarEstudiante
        public IActionResult DeleteEstudiante(int id)
        {
            _estudianteBusiness.BorrarEstudiante(id);
            return NoContent();
        }
    }
}
