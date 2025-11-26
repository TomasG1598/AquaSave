using AquaSave.Models;
using AquaSave.Repositories;
using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace AquaSave.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly UserRepository _repo;

        public FrmLogin(InMemoryRepository repo)
        {
            InitializeComponent();
            _repo = new UserRepository(); // Ahora sí usa SQL
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correo = txCorreo.Text.Trim();
            string contrasena = txContrasena.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Debe ingresar correo y contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar correo
            if (!CorreoValido(correo))
            {
                MessageBox.Show("Debe ingresar un correo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Intentar login con SQL
            User usuario = _repo.Login(correo, contrasena);

            if (usuario != null)
            {
                MessageBox.Show($"Bienvenido, {usuario.Username}", "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormMain frm = new FormMain(usuario, _repo);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CorreoValido(string correo)
        {
            try
            {
                var dir = new MailAddress(correo);
                return dir.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Llamar el formulario de registro usando también SQL
            FormRegistro frm = new FormRegistro(new UserRepository(), this);
            frm.Show();
            this.Hide();
        }
    }
}
