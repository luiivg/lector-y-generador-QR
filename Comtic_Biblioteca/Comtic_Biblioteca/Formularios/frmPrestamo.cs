using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriasSW.Clases;

namespace Comtic_Biblioteca.Formularios
{
    public partial class frmPrestamo : Form
    {
        public frmPrestamo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            frmBiblioteca frmBiblioteca = new frmBiblioteca();
            frmBiblioteca.Show();
        }

        private void btnPrestamo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            clsPrestamo objPrestamo = new clsPrestamo();
            objPrestamo.CodigoLibro = txtCodLibro.Text;
            objPrestamo.DocumentoEstudiante = txtDocEstudiante.Text;
            if (objPrestamo.PrestarLibro())
            {
                 if(objPrestamo.Error.Equals("0"))
                {
                    lblError.Text = "El libro no esta disponible.";
                }
                 else if (objPrestamo.Error.Equals("1"))
                {
                    lblError.Text = "Préstamo registrado exitosamente.";                    
                }
                else if (objPrestamo.Error.Equals("2"))
                {
                    Form.ActiveForm.Visible = false;
                    frmEstudiante frmEstudiante = new frmEstudiante();
                    frmEstudiante.Show();
                }
            }
            txtCodLibro.Clear();
            txtDocEstudiante.Clear();
            objPrestamo = null;
            //objPrestamo.cargarPrestamos(GridViewPrestamos);
        }

        private void frmPrestamo_Load(object sender, EventArgs e)
        {
            clsPrestamo objPrestamos = new clsPrestamo();
            objPrestamos.cargarPrestamos(GridViewPrestamos);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult rs2 = MessageBox.Show("Si cierra el formulario se perderan todos los datos sin guardar.", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            {
                if (rs2 == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    Form.ActiveForm.Visible = false;
                    frmBiblioteca frmBiblioteca = new frmBiblioteca();
                    frmBiblioteca.Show();
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            clsPrestamo objConsultaPrestamo = new clsPrestamo();
            objConsultaPrestamo.CodigoLibro = txtCodLibro.Text;
            objConsultaPrestamo.consultarPrestamo(GridViewPrestamos);

            if (txtCodLibro.Text == "")
            {
                objConsultaPrestamo.cargarPrestamos(GridViewPrestamos);
            }
            txtCodLibro.Clear();
        }

        private void btnDevolverLibro_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            clsPrestamo objPrestamo = new clsPrestamo();
            objPrestamo.CodigoLibro = txtCodLibro.Text;
            objPrestamo.DocumentoEstudiante = txtDocEstudiante.Text;
            if (objPrestamo.DevolverLibro())
            {
                if (objPrestamo.Error.Equals("1"))
                {
                    lblError.Text = "Devolucion registrado exitosamente.";
                }
                
            }
            txtCodLibro.Clear();
            txtDocEstudiante.Clear();
            objPrestamo = null;
        }
    }
}
