using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace NOCHESVI_DEFINITIVO
{
    public partial class ProveedoresAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
         
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            String telefono = txtTelefono.Text;
            String nit = txtNit.Text;
            String nombre = txtNombre.Text;
            String producto = txtProducto.Text;
            String valor = txtValorProducto.Text;             

            if (telefono.Equals("") || nit.Equals("") || nombre.Equals("") || producto.Equals("") || valor.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('completa los campos');</script>");
            }
            else if (nit.Length < 6)
            {
                Response.Write("<script languaje='JavaScript'>alert('nit muy corto');</script>");
                txtNit.Text = "";
            }
            else if (!IsLetters(nombre))
            {
                Response.Write("<script languaje='JavaScript'>alert('Valores no permitidos en el campo nombre');</script>");
                
            }
            else if (!IsLetters(producto))
            {
                Response.Write("<script languaje='JavaScript'>alert('Valores no permitidos en el campo producto');</script>");
                
            }
            else if (long.Parse(nit) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('no se aceptan nit negativas');</script>");
                txtNit.Text = "";
            }else if(long.Parse(valor) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('Precios negativos no se aceptan');</script>");
            }
            else
            {
                long nitt = long.Parse(nit);
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT * FROM proveedor WHERE nit=" + nitt+ "";
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
                

                if (tabladedatos.Rows.Count == 1)
                {
                    Response.Write("<script languaje='JavaScript'>alert('El usuario con esta cedula ya existe');</script>");
                    limpiar();
                }
                else
                {                                       

                    String consulta2 = "SELECT tipoProdudcto FROM Producto WHERE tipoProdudcto='" + producto + "'";
                    SqlDataAdapter adaptador2;
                    DataTable tabladedatos2 = new DataTable();
                    try
                    {
                        adaptador2 = new SqlDataAdapter(consulta2, conexion);
                        adaptador2.Fill(tabladedatos2);
                    }
                    catch (Exception ex){}
                    if (tabladedatos2.Rows.Count == 1)
                    {
                        Response.Write("<script languaje='JavaScript'>alert('Ya tienes un proveedor con este producto');</script>");
                        limpiar();
                    }
                    else
                    {
                        String consulta3 = "SELECT MAX(IdProducto) as cantidad FROM Producto ";
                        SqlDataAdapter adaptador3;
                        DataTable tabladedatos3 = new DataTable();
                        try
                        {
                            adaptador3 = new SqlDataAdapter(consulta3, conexion);
                            adaptador3.Fill(tabladedatos3);
                        }
                        catch (Exception ex) { }
                        int cantidad=0;
                        if (tabladedatos3.Rows[0]["cantidad"].ToString().Equals(""))
                        {
                            cantidad = 1;
                        }
                        else
                        {
                            cantidad = int.Parse(tabladedatos3.Rows[0]["cantidad"].ToString());
                            cantidad++;
                        }

                        
                        SqlCommand comando = new SqlCommand();
                            comando.CommandType = CommandType.Text;
                            comando.CommandText = "INSERT proveedor values(" + nitt + ",'" + nombre + "','" + telefono + "')";                            
                            comando.Connection = ConectarBD.GetConexion();
                            comando.ExecuteNonQuery();

                        SqlCommand comando2 = new SqlCommand();
                            comando2.CommandType = CommandType.Text;
                            comando2.CommandText = "INSERT Producto values("+ cantidad +",'" + producto.ToLower()+ "'," + long.Parse(valor)+","+nitt+")";
                            comando2.Connection = ConectarBD.GetConexion();
                            comando2.ExecuteNonQuery();

                        String consulta4 = "SELECT MAX(IdProducto) as cantidad FROM inventarios ";
                        SqlDataAdapter adaptador4;
                        DataTable tabladedatos4 = new DataTable();
                        try
                        {
                            adaptador4 = new SqlDataAdapter(consulta4, conexion);
                            adaptador4.Fill(tabladedatos4);
                        }
                        catch (Exception ex) { }
                        int cantidadInventario=0;
                        if (tabladedatos4.Rows[0]["cantidad"].ToString().Equals(""))
                        {
                            cantidadInventario = 1;
                        }
                        else
                        {
                            cantidadInventario = int.Parse(tabladedatos4.Rows[0]["cantidad"].ToString());
                            cantidadInventario++;
                        }
                      Response.Write("<script languaje='JavaScript'>alert('Proveedor creado');</script>");


                        SqlCommand comando3 = new SqlCommand();
                        comando3.CommandType = CommandType.Text;
                        long extra = (long.Parse(valor) * 30) / 100;
                        long valorVendido = extra + long.Parse(valor);
                        comando3.CommandText = "INSERT inventarios values(" + cantidadInventario + ",'" + producto.ToLower() + "',"+0+"," + long.Parse(valor) + "," + valorVendido + ","+ nitt+")";
                        comando3.Connection = ConectarBD.GetConexion();
                        comando3.ExecuteNonQuery();
                        limpiar();
                        cargarDatos();
                        }
                }
            }

        }
        public void limpiar()
        {
           txtTelefono.Text = "";
           txtNit.Text = "";
           txtNombre.Text = "";
           txtProducto.Text = "";
           txtValorProducto.Text = "";
        }

        public void cargarDatos()
        {
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
            String consulta = "SELECT nit, nombre, telefono, IdProducto as id,tipoProdudcto as producto, valorUnitario as valor FROM proveedor prov,Producto prod WHERE prov.nit = prod.fk_nitProveedor";
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            String nitt = txtNit.Text;

            if (nitt.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Si quiere eliminar digite la cedula...');</script>");
            }
            else
            {
                long nit = long.Parse(nitt);
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT nit FROM proveedor prov,Producto prod WHERE prov.nit = prod.fk_nitProveedor AND nit=" + nit;

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
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "DELETE FROM proveedor WHERE nit=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                    comando.Connection = ConectarBD.GetConexion();
                    comando.ExecuteNonQuery();
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandType = CommandType.Text;
                    comando2.CommandText = "DELETE FROM Producto WHERE fk_nitProveedor=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                    comando2.Connection = ConectarBD.GetConexion();
                    comando2.ExecuteNonQuery();
                    String consulta4 = "SELECT * FROM inventarios WHERE nitIdentificador="+ int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                    SqlDataAdapter adaptador4;
                    DataTable tabladedatos4 = new DataTable();
                    try
                    {
                        adaptador4 = new SqlDataAdapter(consulta4, conexion);
                        adaptador4.Fill(tabladedatos4);
                    }
                    catch (Exception ex) { }
                    if(int.Parse(tabladedatos4.Rows[0]["cantidad"].ToString())==0)
                    {
                        SqlCommand comando3 = new SqlCommand();
                        comando3.CommandType = CommandType.Text;
                        comando3.CommandText = "DELETE FROM inventarios WHERE nitIdentificador=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                        comando3.Connection = ConectarBD.GetConexion();
                        comando3.ExecuteNonQuery();
                    }
                    limpiar();
                    cargarDatos();

                }
                else
                {
                    Response.Write("<script languaje='JavaScript'>alert('No existe este nit...');</script>");
                    txtNit.Text = "";
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String telefono = txtTelefono.Text;
            String nit = txtNit.Text;
            String nombre = txtNombre.Text;
            String producto = txtProducto.Text;
            String valor = txtValorProducto.Text;

            if (telefono.Equals("") || nit.Equals("") || nombre.Equals("") || producto.Equals("") || valor.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('completa los campos');</script>");
            }
            else if (nit.Length < 6)
            {
                Response.Write("<script languaje='JavaScript'>alert('nit muy corto);</script>");
                txtNit.Text = "";
            }else if (!IsLetters(nombre))
            {
                Response.Write("<script languaje='JavaScript'>alert('Valores no permitidos');</script>");
                txtNit.Text = "";
            }
            else if (long.Parse(nit) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('no se aceptan nit negativas');</script>");
                txtNit.Text = "";
            }
            else if (long.Parse(valor) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('Precios negativos no se aceptan');</script>");
            }
            else
            {
                long nitt = long.Parse(nit);
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT * FROM proveedor WHERE nit=" + nitt + "";
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }
                if (tabladedatos.Rows.Count == 0)
                {
                    Response.Write("<script languaje='JavaScript'>alert('El proveedor con este nit no existe');</script>");
                    limpiar();
                }
                else
                {
                                 
            

                        SqlCommand comando2 = new SqlCommand();
                        comando2.CommandType = CommandType.Text;                       
                        comando2.CommandText = "UPDATE proveedor set nombre='" + nombre + "',telefono=" + telefono + " WHERE nit=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                        comando2.Connection = ConectarBD.GetConexion();
                        comando2.ExecuteNonQuery();

                        String consulta4 = "SELECT * FROM inventarios WHERE nitIdentificador=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                    SqlDataAdapter adaptador4;
                        DataTable tabladedatos4 = new DataTable();
                        try
                        {
                            adaptador4 = new SqlDataAdapter(consulta4, conexion);
                            adaptador4.Fill(tabladedatos4);
                        }
                        catch (Exception ex) { }

                        if (int.Parse(tabladedatos4.Rows[0]["cantidad"].ToString()) == 0)
                        {
                           SqlCommand comando = new SqlCommand();
                           comando.CommandType = CommandType.Text;
                           comando.CommandText = "UPDATE Producto set valorUnitario=" + valor + " WHERE fk_nitProveedor=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                           comando.Connection = ConectarBD.GetConexion();
                           comando.ExecuteNonQuery();

                            SqlCommand comando3 = new SqlCommand();
                            comando3.CommandType = CommandType.Text;
                            long extra = (long.Parse(valor) * 30) / 100;
                            long valorVendido = extra + long.Parse(valor);
                            comando3.CommandText = "UPDATE inventarios set producto='" + producto + "',valorComprado=" + valor + ",valorVendida=" + valorVendido + " WHERE nitIdentificador=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                            comando3.Connection = ConectarBD.GetConexion();
                            comando3.ExecuteNonQuery();


                        }

                        limpiar();
                        cargarDatos();
                    
                }
            }



        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            String nitt = txtNit.Text;
            if (txtNit.Text == "")
            {
                Response.Write("<script languaje='JavaScript'>alert('Para buscar digite el nit...');</script>");
            }
            else
            {
                long nit = long.Parse(nitt);
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT nit, nombre, telefono, IdProducto as id,tipoProdudcto as producto, valorUnitario as valor FROM proveedor prov,Producto prod WHERE prov.nit = prod.fk_nitProveedor AND nit="+nit;
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
                    txtNit.Text = tabladedatos.Rows[0]["nit"].ToString();
                    txtNombre.Text = tabladedatos.Rows[0]["nombre"].ToString();
                    txtProducto.Text = tabladedatos.Rows[0]["producto"].ToString();
                    txtTelefono.Text = tabladedatos.Rows[0]["telefono"].ToString();
                    txtValorProducto.Text = tabladedatos.Rows[0]["valor"].ToString();
                    cargarDatos();
                }
                else
                {
                    Response.Write("<script languaje='JavaScript'>alert('No existe Nit...');</script>");
                    txtNit.Text = "";
                }


            }


        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}