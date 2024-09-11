using Institucion_Educativa.Data;
using Institucion_Educativa.Models;

namespace Institucion_Educativa.Business
{
    public class EstudianteBusiness
    {
        private readonly EstudianteData _estudianteData;

        public EstudianteBusiness(string connectionString)
        {
            _estudianteData = new EstudianteData(connectionString);
        }

        public List<EstudianteModel> ObtenerEstudiantes()
        {
            return _estudianteData.ObtenerEstudiantes();
        }

        public EstudianteModel ObtenerEstudiantePorId(int estudianteId)
        {
            return _estudianteData.ObtenerEstudiantePorId(estudianteId);
        }

        public void CrearEstudiante(EstudianteModel estudiante)
        {
            _estudianteData.CrearEstudiante(estudiante);
        }

        public void ActualizarEstudiante(EstudianteModel estudiante)
        {
            _estudianteData.ActualizarEstudiante(estudiante);
        }

        public void BorrarEstudiante(int estudianteId)
        {
            _estudianteData.BorrarEstudiante(estudianteId);
        }

        public int ContarEstudiantes()
        {
            return _estudianteData.ContarEstudiantes();
        }

    }
}
