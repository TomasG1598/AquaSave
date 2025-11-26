using AquaSave.Data;
using System;
using System.Data.SqlClient;

namespace AquaSave.Repositories
{
    public class WeeklyChallengeRepository
    {
        public bool AssignWeeklyChallenge(int usuarioId, int retoId, DateTime fecha)
        {
            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = @"INSERT INTO Participaciones (UsuarioId, RetoId, Fecha, Estado)
                                   VALUES (@UsuarioId, @RetoId, @Fecha, @Estado)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                        cmd.Parameters.AddWithValue("@RetoId", retoId);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@Estado", "Pendiente");

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar reto semanal: " + ex.Message, ex);
            }
        }
    }
}