using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using LibreriasSW.Base_de_datos;

namespace LibreriasSW.Clases
{
    public class clsPrestamo
    {
        #region "Atributos"
        private string sDocumentoEstudiante;
        private string sCodigoLibro;
        private DataTable dDataTable;
        private MySqlDataAdapter dDataAdapter;
        private string sError;
        #endregion

        #region "Propiedades"
        public string DocumentoEstudiante
        {
            get { return sDocumentoEstudiante; }
            set { sDocumentoEstudiante = value; }
        }
        public string CodigoLibro
        {
            get { return sCodigoLibro; }
            set { sCodigoLibro = value; }
        }

        public DataTable DataTable
        {
            get { return dDataTable; }
            set { dDataTable = value; }
        }

        public MySqlDataAdapter DataAdapter
        {
            get { return dDataAdapter; }
            set { dDataAdapter = value; }
        }
        public string Error
        {
            get { return sError; }
        }
        #endregion

        #region "Metodos"
        
        public bool PrestarLibro()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = new clsConexion().Conexion();
                    cmd.CommandText = "PrestarLibro";


                    //asignar paramentros
                    cmd.Parameters.AddWithValue("Cedula_", sDocumentoEstudiante);
                    cmd.Parameters.AddWithValue("CodigoLibro_", sCodigoLibro);
                    cmd.Parameters.Add("Respuesta", MySqlDbType.String);
                    cmd.Parameters["Respuesta"].Direction = ParameterDirection.ReturnValue;

                    //abrir la conexion
                    cmd.Connection.Open();

                    //ejecutar el query
                    cmd.ExecuteNonQuery();

                    sError = (string)cmd.Parameters["Respuesta"].Value;

                    return true;
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    return false;
                }
                finally
                {
                    cmd.Connection.Close();
                } // fin try
            } // fin using
        }

        public bool DevolverLibro()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = new clsConexion().Conexion();
                    cmd.CommandText = "DevolverLibro";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("Cedula_", sDocumentoEstudiante);
                    cmd.Parameters.AddWithValue("CodigoLibro_", sCodigoLibro);
                    cmd.Parameters.Add("Respuesta", MySqlDbType.String);
                    cmd.Parameters["Respuesta"].Direction = ParameterDirection.ReturnValue;

                    //abrir la conexion
                    cmd.Connection.Open();

                    //ejecutar el query
                    cmd.ExecuteNonQuery();

                    sError = (string)cmd.Parameters["Respuesta"].Value;
                    return true;
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    return false;
                }
                finally
                {
                    cmd.Connection.Close();
                } // fin try
            } // fin using
        }

        public void cargarPrestamos(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT P.CODIGO_LIBRO AS 'CODIGO LIBRO', I.NOMBRE_LIBRO AS 'NOMBRE LIBRO', P.DOCUMENTO_ESTUDIANTE AS 'DOCUMENTO ESTUDIANTE', E.NOMBRE AS 'NOMBRE ESTUDIANTE', P.FECHA_INICIO AS 'FECHA INICIO', P.FECHA_FIN AS 'FECHA FIN' FROM TBLPRESTAMO P INNER JOIN TBLESTUDIANTE E ON P.DOCUMENTO_ESTUDIANTE = E.DOCUMENTO INNER JOIN TBLINVENTARIO I ON P.CODIGO_LIBRO = I.ID_CODIGO ORDER BY ID_PRESTAMO DESC", new clsConexion().Conexion());
                dDataTable = new DataTable();
                dDataAdapter.Fill(dDataTable);
                data.DataSource = dDataTable;
            }
            catch (Exception ex)
            {
                sError = "No se pudo llenar el GridView" + ex.ToString();
            }
        }

        public void consultarPrestamo(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT P.CODIGO_LIBRO AS 'CODIGO LIBRO', I.NOMBRE_LIBRO AS 'NOMBRE LIBRO', P.DOCUMENTO_ESTUDIANTE AS 'DOCUMENTO ESTUDIANTE', E.NOMBRE AS 'NOMBRE ESTUDIANTE', P.FECHA_INICIO AS 'FECHA INICIO', P.FECHA_FIN AS 'FECHA FIN' FROM TBLPRESTAMO P INNER JOIN TBLESTUDIANTE E ON P.DOCUMENTO_ESTUDIANTE = E.DOCUMENTO INNER JOIN TBLINVENTARIO I ON P.CODIGO_LIBRO = I.ID_CODIGO WHERE P.CODIGO_LIBRO LIKE '%" + sCodigoLibro + "%' AND P.DOCUMENTO_ESTUDIANTE LIKE '%"+ sDocumentoEstudiante + "%' ORDER BY ID_PRESTAMO DESC", new clsConexion().Conexion());
                dDataTable = new DataTable();
                dDataAdapter.Fill(dDataTable);
                data.DataSource = dDataTable;
            }
            catch (Exception ex)
            {
                sError = "No se pudo llenar el GridView" + ex.ToString();
            }
        }
        #endregion

    }
}
