using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using LibreriasSW.Base_de_datos;
using System.Data;
using System.Windows.Forms;

namespace LibreriasSW.Clases
{
    public class clsInventario
    {
        #region "Atributos"
        private string sCodigoLibro;
        private string sLibro;
        private DataTable dDataTable;
        private MySqlDataAdapter dDataAdapter;
        private string sError;
        #endregion

        #region "Propiedades"
        public string CodigoLibro
        {
            get { return sCodigoLibro; }
            set { sCodigoLibro = value; }
        }

        public string Libro
        {
            get { return sLibro; }
            set { sLibro = value; }
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
        

        public bool GestionarLibro()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = new clsConexion().Conexion();
                    cmd.CommandText = "GestionarInventario";


                    //asignar paramentros
                    cmd.Parameters.AddWithValue("CodigoLibro_", sCodigoLibro);
                    cmd.Parameters.AddWithValue("Libro_", sLibro);
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

        public void consultarLibro(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT ID_CODIGO AS 'CODIGO LIBRO', NOMBRE_LIBRO AS LIBRO,(CASE ESTADO_LIBRO WHEN 0 THEN 'DISPONIBLE' WHEN 1 THEN 'PRESTADO' END) AS 'ESTADO LIBRO' FROM TBLINVENTARIO WHERE ID_CODIGO LIKE '%"+ sCodigoLibro + "%'", new clsConexion().Conexion());
                dDataTable = new DataTable();
                dDataAdapter.Fill(dDataTable);
                data.DataSource = dDataTable;
            }
            catch (Exception ex)
            {
                sError = "No se pudo llenar el GridView" + ex.ToString();
            }
        }
        public void cargarInventario(DataGridView data)
        {
            try
            {
                dDataAdapter = new MySqlDataAdapter("SELECT ID_CODIGO AS 'CODIGO LIBRO', NOMBRE_LIBRO AS LIBRO,(CASE ESTADO_LIBRO WHEN 0 THEN 'DISPONIBLE' WHEN 1 THEN 'PRESTADO' END) AS 'ESTADO LIBRO' FROM TBLINVENTARIO ORDER BY ESTADO_LIBRO, ID_INVENTARIO", new clsConexion().Conexion());
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
