using Institucion_Educativa.Models;
using System.Data;
using System.Data.SqlClient;

namespace Institucion_Educativa.Data
{
    public class EstudianteData
    {
        private readonly string _connectionString;

        public EstudianteData(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ContarEstudiantes()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Estudiante", connection);
                connection.Open();
                count = (int)command.ExecuteScalar();
            }
            return count;
        }


        // Obtener todos los estudiantes
        public List<EstudianteModel> ObtenerEstudiantes()
        {
            List<EstudianteModel> estudiantes = new List<EstudianteModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spObtenerEstudiante", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estudiantes.Add(new EstudianteModel
                    {
                        EstudianteId = (int)reader["EstudianteId"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NumeroIdentificacion = reader["NumeroIdentificacion"].ToString(),
                        FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    });
                }

                reader.Close();
            }

            return estudiantes;
        }

        // Obtener estudiante por ID
        public EstudianteModel ObtenerEstudiantePorId(int estudianteId)
        {
            EstudianteModel estudiante = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spConsultarEstudiante", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@EstudianteId", estudianteId));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    estudiante = new EstudianteModel
                    {
                        EstudianteId = (int)reader["EstudianteId"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NumeroIdentificacion = reader["NumeroIdentificacion"].ToString(),
                        FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                }

                reader.Close();
            }

            return estudiante;
        }

        // Crear un nuevo estudiante
        public void CrearEstudiante(EstudianteModel estudiante)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spCrearEstudiante", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros para el stored procedure
                command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                command.Parameters.AddWithValue("@Apellido", estudiante.Apellido);
                command.Parameters.AddWithValue("@NumeroIdentificacion", estudiante.NumeroIdentificacion);
                command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                command.Parameters.AddWithValue("@Correo", estudiante.Correo);
                command.Parameters.AddWithValue("@Telefono", estudiante.Telefono);

                connection.Open();

                // Ejecutar el stored procedure y capturar el EstudianteId generado
                var estudianteId = (int)command.ExecuteScalar();  // Captura el ID devuelto
                estudiante.EstudianteId = estudianteId;  // Asignar el ID al modelo
            }
        }


        // Actualizar estudiante
        public void ActualizarEstudiante(EstudianteModel estudiante)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spActualizarEstudiante", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@EstudianteId", estudiante.EstudianteId));
                command.Parameters.Add(new SqlParameter("@Nombre", estudiante.Nombre));
                command.Parameters.Add(new SqlParameter("@Apellido", estudiante.Apellido));
                command.Parameters.Add(new SqlParameter("@NumeroIdentificacion", estudiante.NumeroIdentificacion));
                command.Parameters.Add(new SqlParameter("@FechaNacimiento", estudiante.FechaNacimiento));
                command.Parameters.Add(new SqlParameter("@Correo", estudiante.Correo));
                command.Parameters.Add(new SqlParameter("@Telefono", estudiante.Telefono));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Borrar estudiante
        public void BorrarEstudiante(int estudianteId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spBorrarEstudiante", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@EstudianteId", estudianteId));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //string Resultado = "";

        //string connectionString = "Server=DESKTOP-GF7KVM2\\SQLEXPRESS;Database=Institucion_educativa;Trusted_Connection=True;";

        //// Llamar al método que actualiza el estudiante
        ////ActualizarEstudiante(connectionString, Estudiantemodel);


        //public string ActualizarEstudiante(string connectionString, EstudianteModel estudiantemodel)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            // Abrir la conexión
        //            connection.Open();

        //            // Crear el comando para el stored procedure
        //            using (SqlCommand command = new SqlCommand("spActualizarEstudiante", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Agregar los parámetros necesarios para el stored procedure
        //                command.Parameters.Add(new SqlParameter("@EstudianteId", estudiantemodel.NumeroIdentificacion));
        //                command.Parameters.Add(new SqlParameter("@Nombre", estudiantemodel.Nombre));
        //                command.Parameters.Add(new SqlParameter("@Apellido", estudiantemodel.Apellido));
        //                command.Parameters.Add(new SqlParameter("@NumeroIdentificacion", estudiantemodel.NumeroIdentificacion));
        //                command.Parameters.Add(new SqlParameter("@FechaNacimiento", estudiantemodel.FechaNacimiento));
        //                command.Parameters.Add(new SqlParameter("@Correo", estudiantemodel.Correo));
        //                command.Parameters.Add(new SqlParameter("@Telefono", estudiantemodel.Telefono));

        //                // Ejecutar el stored procedure
        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    Resultado = ("Estudiante actualizado exitosamente.");
        //                }
        //                else
        //                {
        //                    Resultado = ("No se encontró el estudiante o no se pudo actualizar.");
        //                }
        //            }
        //            return Resultado;
        //        }
        //        catch (SqlException ex)
        //        {
        //            Resultado = ("Ocurrió un error al actualizar el estudiante: " + ex.Message);
        //            return Resultado;
        //        }
        //        finally
        //        {
        //            // Cerrar la conexión
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}

        //public string BorrarEstudiante(string connectionString, int estudianteId)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            // Abrir la conexión
        //            connection.Open();

        //            // Crear el comando para el stored procedure
        //            using (SqlCommand command = new SqlCommand("spBorrarEstudiante", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Agregar el parámetro necesario para el stored procedure
        //                command.Parameters.Add(new SqlParameter("@EstudianteId", estudianteId));

        //                // Ejecutar el stored procedure
        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    Resultado = ("Estudiante borrado exitosamente.");
        //                }
        //                else
        //                {
        //                    Resultado = ("No se encontró el estudiante o no se pudo borrar.");
        //                }
        //            }
        //            return Resultado;
        //        }
        //        catch (SqlException ex)
        //        {
        //            Resultado = ("Ocurrió un error al borrar el estudiante: ");
        //            return Resultado;
        //        }
        //        finally
        //        {
        //            // Cerrar la conexión
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }

        //}

        //public EstudianteModel ConsultarEstudiante(string connectionString, int estudianteId)
        //{
        //    // Usamos "using" para asegurarnos de cerrar la conexión correctamente
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            // Abrir la conexión
        //            connection.Open();

        //            // Crear el comando para el stored procedure
        //            using (SqlCommand command = new SqlCommand("spConsultarEstudiante", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Agregar el parámetro necesario para el stored procedure
        //                command.Parameters.Add(new SqlParameter("@EstudianteId", estudianteId));

        //                // Ejecutar el stored procedure y leer los datos
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        // Crear un objeto Estudiante y llenar los datos desde el reader
        //                        EstudianteModel estudiantemodel = new EstudianteModel()
        //                        {
        //                            EstudianteId = (int)reader["EstudianteId"],
        //                            Nombre = reader["Nombre"].ToString(),
        //                            Apellido = reader["Apellido"].ToString(),
        //                            NumeroIdentificacion = reader["NumeroIdentificacion"].ToString(),
        //                            FechaNacimiento = (DateTime)reader["FechaNacimiento"],
        //                            Correo = reader["Correo"].ToString(),
        //                            Telefono = reader["Telefono"].ToString()
        //                        };
        //                        return estudiantemodel;
        //                    }
        //                }
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            Resultado = ("Ocurrió un error al consultar el estudiante: " + ex.Message);
        //            return estudiantemodel;
        //        }
        //        finally
        //        {
        //            // Cerrar la conexión
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}
    }
}
