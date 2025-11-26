using AquaSave.Data;
using AquaSave.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AquaSave.Repositories
{
    public class UserRepository
    {
        // ============================================
        //  REGISTRAR USUARIO
        // ============================================

        public bool AddUser(User user)
        {
            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = @"INSERT INTO Usuarios (Nombre, Correo, Contrasena, Rol, FechaRegistro)
                                   VALUES (@Nombre, @Correo, @Contrasena, @Rol, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", user.Username);
                        cmd.Parameters.AddWithValue("@Correo", user.Correo);
                        cmd.Parameters.AddWithValue("@Contrasena", user.Contrasena);
                        cmd.Parameters.AddWithValue("@Rol", string.IsNullOrEmpty(user.Rol) ? "Usuario" : user.Rol);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en AddUser: " + ex.Message, ex);
            }
        }

        // ============================================
        //  LOGIN
        // ============================================

        public User Login(string correo, string contrasena)
        {
            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = @"SELECT UsuarioId, Nombre, Correo, Rol, FechaRegistro
                                   FROM Usuarios
                                   WHERE Correo = @Correo AND Contrasena = @Contrasena";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserId = Convert.ToInt32(reader["UsuarioId"]),
                                    Username = reader["Nombre"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                                };
                            }
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Login: " + ex.Message, ex);
            }
        }

        // ============================================
        //  RETOS DIARIOS → Tabla Retos
        // ============================================

        public List<Reto> RetoDiarios(string correo)
        {
            var lista = new List<Reto>();

            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = @"
                        SELECT RetoId, Titulo, Descripcion, Tipo, Puntos, Nivel
                        FROM Retos
                        WHERE Tipo = 'Diario'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Reto
                                {
                                    RetoId = Convert.ToInt32(reader["RetoId"]),
                                    Titulo = reader["Titulo"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Tipo = reader["Tipo"].ToString(),
                                    Puntos = Convert.ToInt32(reader["Puntos"]),
                                    Nivel = reader["Nivel"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error cargando RetoDiarios: " + ex.Message, ex);
            }

            return lista;
        }

        // ============================================
        //  RETOS SEMANALES → Tabla Retos
        // ============================================

        public List<Reto> RetoSemanales(string correo)
        {
            var lista = new List<Reto>();

            try
            {
                using (SqlConnection conn = DbConnection.GetInstance().GetConnection())
                {
                    string sql = @"
                        SELECT RetoId, Titulo, Descripcion, Tipo, Puntos, Nivel
                        FROM Retos
                        WHERE Tipo = 'Semanal'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Reto
                                {
                                    RetoId = Convert.ToInt32(reader["RetoId"]),
                                    Titulo = reader["Titulo"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Tipo = reader["Tipo"].ToString(),
                                    Puntos = Convert.ToInt32(reader["Puntos"]),
                                    Nivel = reader["Nivel"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error cargando RetoSemanales: " + ex.Message, ex);
            }

            return lista;
        }
    }
}