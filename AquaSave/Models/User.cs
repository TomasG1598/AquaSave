using System;

namespace AquaSave.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public string Rol {  get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}