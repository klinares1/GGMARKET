using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NOCHESVI_DEFINITIVO
{
    public class ConectarBD
    {


    private static SqlConnection ObjConexion;
        private static string Error;
        public static SqlConnection GetConexion()
        {
            if (ObjConexion != null)
                return ObjConexion;
            ObjConexion = new SqlConnection();
            ObjConexion.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                ObjConexion.Open();
                return ObjConexion;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null;
            }
        }
        public static void CerrarConexion()
        {
            if (ObjConexion != null)
                ObjConexion.Close();
        }
    }
}