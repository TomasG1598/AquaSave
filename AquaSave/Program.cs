using System;
using System.Windows.Forms;
using AquaSave.Repositories;
using AquaSave.Models;

namespace AquaSave
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Crear repo en memoria o repositorio real después
            var repo = new InMemoryRepository();

            // Abrir Login
            Application.Run(new Forms.FrmLogin(repo));
        }
    }
}