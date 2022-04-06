using System;
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
    public partial class InicioSesion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            inicioCliente.i = 0;
        }

        protected void btnButon_Click(object sender, EventArgs e)
        {
            
            String contra = txtContrasenia.Text;
            String cedula = txtCedula.Text;
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
            String consulta = "SELECT tipo FROM usuarios WHERE cedula='"+cedula+"' AND contrasenia='"+contra+"'";
            SqlDataAdapter adaptador;
            DataTable tabladedatos = new DataTable();
            try
            {
                adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabladedatos);
            }catch(Exception ex)
            {

            }
            if(tabladedatos.Rows.Count  == 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('Usuario no existente');</script>");
            }else if(cedula.Equals("") || contra.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Complete todos los campos'),;</script>");
            }
            else
            {
               
                if(tabladedatos.Rows[0]["tipo"].ToString().Equals("Admin"))
                {
                 Response.Redirect("inicioAdmin.aspx");

                }
                else
                {
                    Response.Redirect("inicioCliente.aspx?cedula=" + cedula);
                    
                }

            }
        
        
        
        }
    }
}