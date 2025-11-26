using AquaSave.Data;
using AquaSave.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AquaSave.Repositories
{
    public class ChallengeRepository
    {
        public List<Challenge> GetAll()
        {
            var list = new List<Challenge>();

            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = "SELECT RetoId, Titulo, Descripcion, Tipo, Puntos, Nivel FROM Retos";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Challenge
                                {
                                    id = Convert.ToInt32(reader["RetoId"]),
                                    titulo = reader["Titulo"].ToString(),
                                    descripcion = reader["Descripcion"].ToString(),
                                    tipo = reader["Tipo"].ToString(),
                                   puntos = Convert.ToInt32(reader["Puntos"]),
                                    dificultad = reader["Nivel"] as string
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en GetAll Retos: " + ex.Message, ex);
            }

            return list;
        }
    }
}
