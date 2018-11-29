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
using ZebraScannerSO;
using OposScanner_CCO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;

namespace Comtic_Biblioteca.Formularios
{
    public partial class frmLibro : Form
    {
        public frmLibro()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Visible = false;
            frmBiblioteca frmBiblioteca = new frmBiblioteca();
            frmBiblioteca.Show();
        }

        private void btnRegistrarLibro_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                clsInventario objLibro = new clsInventario();
                objLibro.CodigoLibro = txtCodLibro.Text;
                objLibro.Libro = txtNombreLibro.Text;

                if (objLibro.GestionarLibro())
                {
                    if (objLibro.Error.Equals("0"))
                    {
                        lblError.Text = "Ya existe un registro con el código ingresado.";
                    }
                    else if (objLibro.Error.Equals("1"))
                    {
                        QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                        QrCode qrCode = new QrCode();
                        qrEncoder.TryEncode(txtCodLibro.Text, out qrCode);

                        GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(150, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

                        MemoryStream ms = new MemoryStream();//Crear memoria de conjunto de datos, para almacenar el cod qr

                        renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                        var imageTemporal = new Bitmap(ms);
                        var imagen = new Bitmap(imageTemporal, new Size(new Point(150, 150)));//
                        panelResultado.BackgroundImage = imagen;

                        imagen.Save("QR.png", ImageFormat.Png);
                        btnImprimir.Enabled = true;

                        lblError.Text = "Registro guardado correctamente.";
                        objLibro.cargarInventario(GridViewLibros);
                    }
                }
                else
                {
                    lblError.Text = "Error! no se pudo agregar el registro " + objLibro.Error;
                }
                txtCodLibro.Clear();
                txtNombreLibro.Clear();
                objLibro = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtCodLibro_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnConsultarLibro_Click(object sender, EventArgs e)
        {
            clsInventario objConsultaLibro = new clsInventario();
            objConsultaLibro.CodigoLibro = txtCodLibro.Text;

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(txtCodLibro.Text, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(150, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();//Crear memoria de conjunto de datos, para almacenar el cod qr

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(150, 150)));//
            panelResultado.BackgroundImage = imagen;

            imagen.Save("QR.png", ImageFormat.Png);
            btnImprimir.Enabled = true;

            objConsultaLibro.consultarLibro(GridViewLibros);
            
            if (txtCodLibro.Text == "")
            {
                objConsultaLibro.cargarInventario(GridViewLibros);
            }
            txtCodLibro.Clear();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("SampleText",new Font("Arial", 80, FontStyle.Bold), Brushes.Black, 150, 125);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            try
            {

                Image img = Image.FromFile(@"D:/Desarrollos/COMTIC Prueba seleccion/Programa FINAL/Comtic_Biblioteca/Comtic_Biblioteca/bin/Debug/QR.png");

                //Adjust the size of the image to the page to print the full image without loosing any part of it
                Rectangle m = e.MarginBounds;
                m.Height = 50 / 50 * 100;//Divide la altura sobre el ancho de la imagen * el ancho del margen de la página
                m.Width = 50 / 50 * 100;//Divide el ancho de la imagen sobre el alto de la imagen * el alto de la página

                e.Graphics.DrawImage(img, m);//Le envía la imagen a pintar y el rectángulo

            }
            catch (Exception)
            {

            }
        }

        private void frmLibro_Load(object sender, EventArgs e)
        {
            clsInventario objInvetario = new clsInventario();
            objInvetario.cargarInventario(GridViewLibros);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult rs2 = MessageBox.Show("Si cierra el formulario se perderan todos los datos sin guardar.", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            {
                if (rs2 == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }else
                {
                    Form.ActiveForm.Visible = false;
                    frmBiblioteca frmBiblioteca = new frmBiblioteca();
                    frmBiblioteca.Show();
                }    
            }
        }
    }
}
