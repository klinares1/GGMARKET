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
    public partial class sugeCliente : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('No hay datos en la sugerencia');</script>");
            }
            else
            {
                
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT * FROM usuarios WHERE cedula=" + long.Parse(inicioCliente.registrado);
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }

                String consulta2 = "SELECT MAX(Id) as cantidad FROM sugerencia ";
                SqlDataAdapter adaptador2;
                DataTable tabladedatos2 = new DataTable();
                try
                {
                    adaptador2 = new SqlDataAdapter(consulta2, conexion);
                    adaptador2.Fill(tabladedatos2);
                }
                catch (Exception ex) { }
                long cantidad;
               
                if (tabladedatos2.Rows[0]["cantidad"].ToString().Equals(""))
                {
                    cantidad = 1;
                }
                else
                {
                    cantidad = long.Parse(tabladedatos2.Rows[0]["cantidad"].ToString());
                    cantidad++;
                }

                String consulta3 = "SELECT SYSDATETIME() as fecha ";
                SqlDataAdapter adaptador3;
                DataTable tabladedatos3 = new DataTable();
                try
                {
                    adaptador3 = new SqlDataAdapter(consulta3, conexion);
                    adaptador3.Fill(tabladedatos3);
                }
                catch (Exception ex) { }
                String acom = "";
                acom = "Nombre: " + tabladedatos.Rows[0]["nombre"].ToString() + "\nFecha: " +
                    tabladedatos3.Rows[0]["fecha"].ToString() + "\n" + TextBox1.Text;

                SqlCommand comando2 = new SqlCommand();
                comando2.CommandType = CommandType.Text;
                comando2.CommandText = "INSERT sugerencia values(" + cantidad + "," + inicioCliente.registrado+ ",'"+acom+"')";
                comando2.Connection = ConectarBD.GetConexion();
                comando2.ExecuteNonQuery();
            }

            TextBox1.Text = "";
        }
    }
}