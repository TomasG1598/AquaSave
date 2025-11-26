using AquaSave.Models;
using AquaSave.Repositories;
using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace AquaSave.Forms
{
    public partial class FormRegistro : Form
    {
        private readonly UserRepository _repo;
        private readonly FrmLogin _loginForm;

        public FormRegistro(UserRepository repo, FrmLogin loginForm)
        {
            InitializeComponent();
            _repo = repo;
            _loginForm = loginForm;
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string nombre = txNombreCompleto.Text.Trim();
            string correo = txCorreo.Text.Trim();
            string contrasena = txContrasena.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Debe ingresar nombre completo, correo y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CorreoValido(correo))
            {
                MessageBox.Show("Debe ingresar un correo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = new User
                {
                    Username = nombre,
                    Correo = correo,
                    Contrasena = contrasena,
                    FullName = "Usuario"
                };

                bool ok = _repo.AddUser(user);
                if (ok)
                {
                    MessageBox.Show("Usuario registrado correctamente.");
                    txNombreCompleto.Clear();
                    txCorreo.Clear();
                    txContrasena.Clear();
                    AbrirFormInicioSesion();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CorreoValido(string correo)
        {
            try
            {
                var d = new MailAddress(correo);
                return d.Address == correo;
            }
            catch { return false; }
        }

        private void btnInicioSe_Click(object sender, EventArgs e)
        {
            AbrirFormInicioSesion();
        }

        private void AbrirFormInicioSesion()
        {
            _loginForm.Show();
            this.Close();
        }
    }
}