using LibreriasSW.Base_de_datos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibreriasSW.Clases
{
    public class clsEstudiante
    {
        #region "Atributos"
        private string sDocumentoEstudiante;
        private string sNombreEstudiante;
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

        public string NombreEstudiante
        {
            get { return sNombreEstudiante; }
            set { sNombreEstudiante = value; }
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
        
        public bool GestionarEstudiante()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = new clsConexion().Conexion();
                    cmd.CommandText = "GestionarEstudiante";


                    //asignar paramentros
                    cmd.Parameters.AddWithValue("CedulaEstudiante_", sDocumentoEstudiante);
                    cmd.Parameters.AddWithValue("NombreEstudiante_", sNombreEstudiante);
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

        public void cargarEstudiantes(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT DOCUMENTO, NOMBRE FROM TBLESTUDIANTE ORDER BY ID_ESTUDIANTE DESC", new clsConexion().Conexion());
                dDataTable = new DataTable();
                dDataAdapter.Fill(dDataTable);
                data.DataSource = dDataTable;
            }
            catch (Exception ex)
            {
                sError = "No se pudo llenar el GridView" + ex.ToString();
            }
        }

        public void consultarEstudiantes(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT DOCUMENTO, NOMBRE FROM TBLESTUDIANTE WHERE DOCUMENTO LIKE '%" + sDocumentoEstudiante+"%'", new clsConexion().Conexion());
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
