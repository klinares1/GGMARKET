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
    public partial class ClientesAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

     public void cargarDatos()
        {
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
            String consulta = "SELECT * FROM usuarios";
            SqlDataAdapter adaptador;
            DataTable tabladedatos = new DataTable();
            try
            {
                adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabladedatos);
                datos.DataSource = tabladedatos;
                datos.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String contrasenia2 = txtContrasenia.Text;
            String cedulaa = txtCedula.Text;
            String nombre = txtNombre.Text;
            if (contrasenia2.Equals("") || cedulaa.Equals("") || nombre.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('completa los campos');</script>");
                
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
            }else if (!IsLetters(nombre))
            {
                Response.Write("<script languaje='JavaScript'>alert('Valores no permitidos en el campo nombre');</script>");

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
                if (!contrasenia2.Equals(contrasenia2))
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
                    if (listaTipo.SelectedValue.Equals("Selecciona tipo"))
                    {
                        Response.Write("<script languaje='JavaScript'>alert('Selecciona el tipo');</script>");
                    }
                    else
                    {
                        SqlCommand comando = new SqlCommand();
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "INSERT usuarios values(" + cedula + ",'" + listaTipo.SelectedValue + "','" + contrasenia2 + "','" + nombre + "')";
                        comando.Connection = ConectarBD.GetConexion();
                        comando.ExecuteNonQuery();
                        Response.Write("<script languaje='JavaScript'>alert('Usuario creado');</script>");
                        txtCedula.Text = "";
                        txtContrasenia.Text = "";
                        txtNombre.Text = "";
                        cargarDatos();

                    }      
                }
                
            }

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
        protected void Button5_Click(object sender, EventArgs e)
        {
            txtCedula.Text = "";
            txtContrasenia.Text = "";
            txtContrasenia.Text = "";
            txtNombre.Text = "";
            listaTipo.SelectedIndex = 0;
            cargarDatos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            String cedulaa = txtCedula.Text;

            if (cedulaa.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Si quiere eliminar digite la cedula...');</script>");
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
                catch (Exception ex){}

                if (tabladedatos.Rows.Count == 1)
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "DELETE FROM usuarios WHERE cedula="+cedula;
                    comando.Connection = ConectarBD.GetConexion();
                    comando.ExecuteNonQuery();
                    txtCedula.Text = "";
                    cargarDatos();

                }
                else
                {
                Response.Write("<script languaje='JavaScript'>alert('No existe esta cedula...');</script>");
                txtCedula.Text = "";
                }


            }            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String cedulaa = txtCedula.Text;

            if (cedulaa.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para buscar digite la cedula...');</script>");
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
                catch (Exception ex) { }

                if (tabladedatos.Rows.Count == 1)
                {
                    String contrasenia = txtContrasenia.Text;
                    String nombre = txtNombre.Text;
                    String tipo="";

                  
                    if (contrasenia.Equals("") || nombre.Equals("")){
                    Response.Write("<script languaje='JavaScript'>alert('Completa los campos');</script>");
                     }else if(contrasenia.Length < 6){
                    Response.Write("<script languaje='JavaScript'>alert('La contraseña debe tener más de 6 caracteres');</script>");
                    }
                    else
                    {
                        if (listaTipo.SelectedValue.Equals("Selecciona tipo"))
                        {
                            Response.Write("<script languaje='JavaScript'>alert('Selecciona el tipo');</script>");
                        }
                        else
                        { 
                            tipo = listaTipo.SelectedValue;
                            SqlCommand comando = new SqlCommand();
                            comando.CommandType = CommandType.Text;
                            comando.CommandText = "UPDATE usuarios set tipo='" + tipo + "',contrasenia='" + contrasenia + "',nombre='" + nombre + "' WHERE cedula='" + cedulaa + "'";
                            comando.Connection = ConectarBD.GetConexion();
                            comando.ExecuteNonQuery();
                            txtCedula.Text = "";
                            txtCedula.Text = "";
                            txtContrasenia.Text = "";
                            txtNombre.Text = "";
                            listaTipo.SelectedIndex = 0;
                            cargarDatos();
                        }        
                    }       
                }
                else
                {
                    Response.Write("<script languaje='JavaScript'>alert('No existe esta cedula...');</script>");
                    txtCedula.Text = "";
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            String cedulaa = txtCedula.Text;

            if (cedulaa.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para buscar digite la cedula...');</script>");
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
                catch (Exception ex) { }

                if (tabladedatos.Rows.Count == 1)
                {
                    txtCedula.Text = tabladedatos.Rows[0]["cedula"].ToString();
                   if(tabladedatos.Rows[0]["tipo"].ToString().Equals("Admin"))
                    {listaTipo.SelectedIndex = 1;}
                    else
                    {listaTipo.SelectedIndex = 2;}

                    txtContrasenia.Text = tabladedatos.Rows[0]["contrasenia"].ToString();
                    txtContrasenia.Text = tabladedatos.Rows[0]["contrasenia"].ToString();
                    txtNombre.Text = tabladedatos.Rows[0]["nombre"].ToString();
                    cargarDatos();
                }
                else
                {
                    Response.Write("<script languaje='JavaScript'>alert('No existe esta cedula...');</script>");
                    txtCedula.Text = "";
                }


            }
        }
    }
}