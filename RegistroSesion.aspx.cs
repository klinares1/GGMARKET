using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace NOCHESVI_DEFINITIVO
{
    public partial class RegistroSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static bool IsLetters(string letra)
        {
            foreach (Char ch in letra)
            {
                if (!Char.IsLetter(ch) && ch != 32)
                {
                    return false;
                }
            }
            return true;
        }
        protected void btnRegistr_Click(object sender, EventArgs e)
        {
            String contrasenia1 = txtConfirmarContraseniaRegistro.Text;
            String contrasenia2 = txtContraseniaRegistro.Text;
            String cedulaa = txtCedula.Text;
            String nombre = txtNombre.Text;
            if (contrasenia1.Equals("") || contrasenia2.Equals("") || cedulaa.Equals("") || nombre.Equals(""))
            {
            Response.Write("<script languaje='JavaScript'>alert('completa los campos');</script>");
            txtCedula.Text = "";
            }
            else if (cedulaa.Length < 6)
            {
                Response.Write("<script languaje='JavaScript'>alert('cedula muy corta, no es valida');</script>");
                txtCedula.Text = "";
            }
            else if (long.Parse(cedulaa) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('no se aceptan cedulas negativas');</script>");
                txtCedula.Text = "";
            }
            else if(contrasenia1.Length <6 )
            {
                Response.Write("<script languaje='JavaScript'>alert('contraseña muy corta, no es valida');</script>");
            }else if (!IsLetters(nombre))
            {
                Response.Write("<script languaje='JavaScript'>alert('solo se aceptan letras en el campo nombre');</script>");
                txtCedula.Text = "";
            }
            else
            {
                long cedula = long.Parse(cedulaa);
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT * FROM usuarios WHERE cedula='" + cedula + "'";
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex)
                {

                }
                if (!contrasenia1.Equals(contrasenia2))
                {
                    Response.Write("<script languaje='JavaScript'>alert('las contraseñas no coinciden');</script>");
                }
                else if (tabladedatos.Rows.Count == 1)
                {
                Response.Write("<script languaje='JavaScript'>alert('El usuario con esta cedula ya existe');</script>");
                txtCedula.Text = "";
                }
                else
                {
                    String tipe = "'cliente'";                    
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "INSERT usuarios values(" + cedula + "," + tipe + ",'" + contrasenia1 + "','"+nombre+"')";                    
                    comando.Connection = ConectarBD.GetConexion();               
                    comando.ExecuteNonQuery();
                    txtCedula.Text = "";
                    txtContraseniaRegistro.Text = "";
                    txtConfirmarContraseniaRegistro.Text = "";
                    txtNombre.Text = "";
                    Response.Redirect("inicioSesion.aspx");
                }
            }
        }
    }
}