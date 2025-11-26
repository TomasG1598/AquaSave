using AquaSave.Models;
using AquaSave.Repositories;
using System;
using System.Windows.Forms;

namespace AquaSave.Forms
{
    public partial class FormMain : Form
    {
        private readonly User _usuario;
        private readonly UserRepository _repo; // <-- UN SOLO REPO!

        public FormMain(User usuario, UserRepository repo)
        {
            InitializeComponent();
            _usuario = usuario;
            _repo = repo;

            this.Load += FormMain_Load; // Siempre se carga aquí
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = $"AquaSave - {_usuario.FullName}";

            var retosDiarios = _repo.RetoDiarios(_usuario.Correo);
            var retosSemanales = _repo.RetoSemanales(_usuario.Correo);

            if ((retosDiarios == null || retosDiarios.Count == 0) &&
                (retosSemanales == null || retosSemanales.Count == 0))
            {
                MessageBox.Show("No tienes retos asignados actualmente.", "Información");
                return;
            }

            dataGridDiarios.DataSource = retosDiarios;
            dataGridSemanales.DataSource = retosSemanales;

            OcultarColumnas(dataGridDiarios);
            OcultarColumnas(dataGridSemanales);
        }

        private void OcultarColumnas(DataGridView grid)
        {
            if (grid.Columns.Count == 0) return;

            if (grid.Columns.Contains("id"))
                grid.Columns["id"].Visible = false;

            if (grid.Columns.Contains("usuarioAsig"))
                grid.Columns["usuarioAsig"].Visible = false;

            if (grid.Columns.Contains("descripcion"))
                grid.Columns["descripcion"].Visible = false;

            if (grid.Columns.Contains("titulo"))
                grid.Columns["titulo"].HeaderText = "🏷️ Título";

            if (grid.Columns.Contains("puntos"))
                grid.Columns["puntos"].HeaderText = "⭐ Puntos";

            if (grid.Columns.Contains("tipo"))
                grid.Columns["tipo"].HeaderText = "📅 Tipo";

            if (grid.Columns.Contains("dificulta"))
                grid.Columns["dificulta"].HeaderText = "🔥 Dificultad";

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.MinimumWidth = 100;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            grid.AutoResizeColumns();
        }

        private void dataGridDiarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dataGridDiarios.Rows[e.RowIndex];

            string titulo = fila.Cells["titulo"].Value?.ToString() ?? "Sin título";
            string descripcion = fila.Cells["descripcion"].Value?.ToString() ?? "Sin descripción";
            string tipo = fila.Cells["tipo"].Value?.ToString() ?? "N/A";
            string puntos = fila.Cells["puntos"].Value?.ToString() ?? "0";

            MessageBox.Show(
                $"Reto: {titulo}\n\nDescripción: {descripcion}\nTipo: {tipo}\nPuntos: {puntos}",
                "Detalle del reto",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void dataGridSemanales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dataGridSemanales.Rows[e.RowIndex];

            string titulo = fila.Cells["titulo"].Value?.ToString() ?? "Sin título";
            string descripcion = fila.Cells["descripcion"].Value?.ToString() ?? "Sin descripción";
            string tipo = fila.Cells["tipo"].Value?.ToString() ?? "N/A";
            string puntos = fila.Cells["puntos"].Value?.ToString() ?? "0";

            MessageBox.Show(
                $"Reto: {titulo}\n\nDescripción: {descripcion}\nTipo: {tipo}\nPuntos: {puntos}",
                "Detalle del reto",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }
    }
}
