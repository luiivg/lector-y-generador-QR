using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comtic_Biblioteca
{
    public partial class frmBiblioteca : Form
    {
        public frmBiblioteca()
        {
            InitializeComponent();
        }

        private void btnEstudiante_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            Formularios.frmEstudiante frmEstudiante = new Formularios.frmEstudiante();
            frmEstudiante.Show();
        }

        private void btnPrestamo_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            Formularios.frmPrestamo frmPrestamo = new Formularios.frmPrestamo();
            frmPrestamo.Show();
        }

        private void btnLibro_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            Formularios.frmLibro frmLibro = new Formularios.frmLibro();
            frmLibro.Show();
        }

        private void frmBiblioteca_Comtic_Biblioteca(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("", "Cerrar",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
