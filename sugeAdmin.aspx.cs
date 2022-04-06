using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NOCHESVI_DEFINITIVO
{
    public partial class sugeAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSugerencias.Text = "";
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
            String consulta = "SELECT * FROM sugerencia";
            SqlDataAdapter adaptador;
            DataTable tabladedatos = new DataTable();
            try
            {
                adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabladedatos);
            }
            catch (Exception ex) { }
            for(int i = 0; i < tabladedatos.Rows.Count; i++)
            {
                txtSugerencias.Text += "Sugerencia número: " + tabladedatos.Rows[i]["Id"].ToString() +
                    "\nCedula: " + tabladedatos.Rows[i]["cedulaUsu_fk"].ToString() + 
                    "\n" + tabladedatos.Rows[i]["queja"].ToString()+"\n\n";
            }                      
        }

        protected void btnEliminarTodo_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM sugerencia where Id>0";
            comando.Connection = ConectarBD.GetConexion();
            comando.ExecuteNonQuery();
            txtSugerencias.Text = "";
        }
    }
}