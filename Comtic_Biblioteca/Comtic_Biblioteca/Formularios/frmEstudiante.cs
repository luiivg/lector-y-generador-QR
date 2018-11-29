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
    public partial class frmEstudiante : Form
    {
        public frmEstudiante()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            frmBiblioteca frmBiblioteca = new frmBiblioteca();
            frmBiblioteca.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                clsEstudiante objEstudiante = new clsEstudiante();
                objEstudiante.DocumentoEstudiante = txtDocumentoEstudiante.Text;
                objEstudiante.NombreEstudiante = txtNombreEstudiante.Text;

                if (objEstudiante.GestionarEstudiante())
                {
                    if (objEstudiante.Error.Equals("0"))
                    {
                        lblError.Text = "Ya existe un registro con el documento ingresado.";
                    }
                    else if (objEstudiante.Error.Equals("1"))
                    {
                        lblError.Text = "Registro guardado correctamente.";
                        objEstudiante.cargarEstudiantes(GridViewEstudiantes);
                    }
                }
                else
                {
                    lblError.Text = "Error! no se pudo agregar el registro " + objEstudiante.Error;
                }
                txtDocumentoEstudiante.Text = "";
                txtNombreEstudiante.Text = "";
                objEstudiante = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmEstudiante_Load(object sender, EventArgs e)
        {
            clsEstudiante objEstudiante = new clsEstudiante();
            objEstudiante.cargarEstudiantes(GridViewEstudiantes);
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
            clsEstudiante objConsultaEstudiante = new clsEstudiante();          
            objConsultaEstudiante.DocumentoEstudiante = txtDocumentoEstudiante.Text;
            objConsultaEstudiante.consultarEstudiantes(GridViewEstudiantes);            

            if (txtDocumentoEstudiante.Text == "")
            {
                objConsultaEstudiante.cargarEstudiantes(GridViewEstudiantes);
            }
            txtDocumentoEstudiante.Clear();
            txtNombreEstudiante.Clear();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            frmPrestamo frmPrestamo = new frmPrestamo();
            frmPrestamo.Show();
        }
    }
}
