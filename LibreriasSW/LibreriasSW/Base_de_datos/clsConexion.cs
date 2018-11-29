using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasSW.Base_de_datos
{

    public class clsConexion
    {
        public MySqlConnection conexion;

        private static MySqlConnection connMY = new MySqlConnection();
        #region "Metodos"
        private static string CrearCadena()
        {
            //String para cadena de conexion
            StringBuilder sCadena = new StringBuilder("");

            sCadena.Append("Server=<SERVIDOR>;");
            sCadena.Append("Port=<PUERTO>;");
            sCadena.Append("DataBase=<BASE>;");
            sCadena.Append("Uid=<USER>;");
            sCadena.Append("Pwd=<PASSWORD>;");
            sCadena.Replace("<SERVIDOR>", "104.236.105.136");
            sCadena.Replace("<PUERTO>", "3306");
            sCadena.Replace("<BASE>", "lvargas");
            sCadena.Replace("<USER>", "lvargas");
            sCadena.Replace("<PASSWORD>", "C0mt1c2017");

            return Convert.ToString(sCadena);
        } // end CrearConexion()

        private static string CrearCadena(string Servidor, string Puerto, string Base, string Usuario, string Password)
        {
            //String para cadena de conexion
            StringBuilder sCadena = new StringBuilder("");

            sCadena.Append("Server=<SERVIDOR>;");
            sCadena.Append("Port=<PUERTO>;");
            sCadena.Append("DataBase=<BASE>;");
            sCadena.Append("Uid=<USER>;");
            sCadena.Append("Pwd=<PASSWORD>;");
            sCadena.Replace("<SERVIDOR>", Servidor);
            sCadena.Replace("<PUERTO>", Puerto);
            sCadena.Replace("<BASE>", Base);
            sCadena.Replace("<USER>", Usuario);
            sCadena.Replace("<PASSWORD>", Password);

            return Convert.ToString(sCadena);
        } // end CrearCadena(5)

        public MySqlConnection Conexion()
        {
            // objeto de conexion
            if (connMY.State != System.Data.ConnectionState.Open)
                connMY.ConnectionString = CrearCadena();
            return connMY;
        } // end Conexion()

        public MySqlConnection Conexion(string Servidor, string Puerto, string Base, string Usuario, string Password)
        {
            // objeto de conexion
            if (connMY.State != System.Data.ConnectionState.Open)
                connMY.ConnectionString = CrearCadena(Servidor, Puerto, Base, Usuario, Password);
            return connMY;
        } // end Conexion()
        #endregion

    }
}

