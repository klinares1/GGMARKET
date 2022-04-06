using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NOCHESVI_DEFINITIVO
{
    public partial class comprasCliente : System.Web.UI.Page
    {
        public static String cedula;
        public static String nombre;
        public static ArrayList listaValor = new ArrayList();
        public static ArrayList listaProducto = new ArrayList();
        public static ArrayList listaCantidad = new ArrayList();
        public static ArrayList listaValorTotal = new ArrayList();
        public static ArrayList listaNit = new ArrayList();
        public static long valor=0,numFactura;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void mostrarTabla()
        {
            txtCantidad.Text = "";
            lblTabla.Text = "";
            string tabla = "";
            tabla = "<table border=3 bordercolor=gray>";
            tabla += "<tr>";
            tabla += "<td><FONT SIZE=4px>Num</td>";
            tabla += "<td><FONT SIZE=4px>Producto</td>";
            tabla += "<td><FONT SIZE=4px>Valor unitario</td>";
            tabla += "<td><FONT SIZE=4px>Cantidad</td>";            
            tabla += "<td><FONT SIZE=4px>Valor total</td>";
            tabla += "</tr>";

            for (int i = 0; i < listaProducto.Count; i++)
            {
                tabla += "<tr>";
                tabla += "<td><FONT SIZE=4px>" + (i + 1) + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaProducto[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaValor[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaCantidad[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaValorTotal[i] + "</td>";                
                tabla += "</tr>";
            }
            tabla += "</table>";
            lblTabla.Text = tabla;
        }

        public void cancerlarTodo()
        {
            listaValor.Clear();
            listaProducto.Clear();
            listaCantidad.Clear();
            listaValorTotal.Clear();
            listaNit.Clear();
            mostrarTabla();
            lblTabla.Text = "";
            txtCantidad.Text = "";
            txtTotal.Text = "";
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            String canti = txtCantidad.Text;
            if (canti.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para agregar digita la cantidad');</script>");
            }
            else if (int.Parse(canti) <= 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('No se aceptan valores negativos o ceros');</script>");
            }
            else
            {
                int cantidad = int.Parse(canti);
                String nit = listaProductos.SelectedItem.Value.ToString();
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT * FROM inventarios WHERE idProducto=" + long.Parse(nit);
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                long cantidadpasable;
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }
                cantidadpasable = long.Parse(tabladedatos.Rows[0]["cantidad"].ToString());
                long validador = cantidadpasable - cantidad;
                if (validador >= 0)
                {
                    Boolean esta = false;
                    for (int i = 0; i < listaProducto.Count; i++)
                    {
                        if (listaProducto[i].Equals(tabladedatos.Rows[0]["producto"].ToString()))
                        {
                            esta = true;
                        }
                    }
                    if (esta == false)
                    {
                        listaValor.Add(tabladedatos.Rows[0]["valorVendida"].ToString());
                        listaProducto.Add(tabladedatos.Rows[0]["producto"].ToString());
                        listaCantidad.Add(cantidad);
                        listaValorTotal.Add((long.Parse(tabladedatos.Rows[0]["valorVendida"].ToString()) * cantidad));
                        listaNit.Add(tabladedatos.Rows[0]["nitIdentificador"].ToString());
                        mostrarTabla();                        
                        valor += (long.Parse(tabladedatos.Rows[0]["valorVendida"].ToString()) * cantidad);                    
                        txtTotal.Text = string.Format("{0:c}", valor);
                    }
                    else
                    {
                        Response.Write("<script languaje='JavaScript'>alert('Ya agregaste de este tipo, si quieres borralo y vuelve a agregar.');</script>");
                    }
                }
                else
                {
                        Response.Write("<script languaje='JavaScript'>alert('No disponemos con esta cantidad, puedes comprar: " + cantidadpasable + "');</script>");
                }


            }
        }

        protected void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            cancerlarTodo();
            valor = 0;
            txtTotal.Text = "";
            comprasCliente.numFactura = 0;
        }

        protected void btnCancelarEspecifico_Click(object sender, EventArgs e)
        {
            String canti = txtCantidad.Text;
            if (canti.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para Eliminar digita la posición');</script>");
            }
            else if (listaCantidad.Count < (int.Parse(canti) - 1)  )
            {
                Response.Write("<script languaje='JavaScript'>alert('Esa posición no existe');</script>");
            }
            else if (int.Parse(canti) < 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('No se aceptan valores negativos o ceros');</script>");
            }
            else
            {
                int posicion = int.Parse(canti);
                posicion--;
                long val = long.Parse(listaValorTotal[posicion].ToString());
                valor -= val;
                txtTotal.Text = string.Format("{0:c}", valor);
                listaValor.RemoveAt(posicion);
                listaProducto.RemoveAt(posicion);
                listaCantidad.RemoveAt(posicion);
                listaValorTotal.RemoveAt(posicion);
                mostrarTabla();

            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
            for (int i = 0; i < listaProducto.Count; i++)
            {
                String consulta = "SELECT * FROM inventarios WHERE nitIdentificador=" + listaNit[i];
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }

                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandType = CommandType.Text;
                    int cant = int.Parse(listaCantidad[i].ToString());
                    long acom = int.Parse(tabladedatos.Rows[0]["cantidad"].ToString()) - cant;
                

                comando2.CommandText = "UPDATE inventarios set cantidad=" + acom + " WHERE nitIdentificador=" + int.Parse(tabladedatos.Rows[0]["nitIdentificador"].ToString());
                    comando2.Connection = ConectarBD.GetConexion();
                    comando2.ExecuteNonQuery();

                if (acom == 0)
                {
                    String consulta7 = "SELECT * FROM producto WHERE fk_nitProveedor=" + listaNit[i];
                    SqlDataAdapter adaptador7;
                    DataTable tabladedatos7 = new DataTable();
                    try
                    {
                        adaptador7 = new SqlDataAdapter(consulta7, conexion);
                        adaptador7.Fill(tabladedatos7);
                    }
                    catch (Exception ex) { }
                    if(tabladedatos7.Rows.Count == 0)
                    {
                        SqlCommand comando5 = new SqlCommand();
                        comando5.CommandType = CommandType.Text;
                        comando5.CommandText = "DELETE FROM inventarios WHERE nitIdentificador="+listaNit[i];
                        comando5.Connection = ConectarBD.GetConexion();
                        comando5.ExecuteNonQuery();
                    }

                }
                    String consulta3 = "SELECT MAX(IdCompra) as cantidad FROM Ventas ";
                    SqlDataAdapter adaptador3;
                    DataTable tabladedatos3 = new DataTable();
                    try
                    {
                        adaptador3 = new SqlDataAdapter(consulta3, conexion);
                        adaptador3.Fill(tabladedatos3);
                    }
                    catch (Exception ex) { }
                int cantidad;
                    if (tabladedatos3.Rows[0]["cantidad"].ToString().Equals(""))
                {
                    cantidad = 1;
                }else
                {
                     cantidad = int.Parse(tabladedatos3.Rows[0]["cantidad"].ToString());
                    cantidad++;
                }
                    

                    String consulta4 = "SELECT * FROM usuarios where Cedula=" + inicioCliente.registrado;
                    SqlDataAdapter adaptador4;
                    DataTable tabladedatos4 = new DataTable();
                    try
                    {
                        adaptador4 = new SqlDataAdapter(consulta4, conexion);
                        adaptador4.Fill(tabladedatos4);
                    }
                    catch (Exception ex) { }
                if (i == 0)
                {
                    String consulta6 = "SELECT MAX(numFactura) as factura FROM Ventas ";
                    SqlDataAdapter adaptador6;
                    DataTable tabladedatos6 = new DataTable();
                    try
                    {
                        adaptador6 = new SqlDataAdapter(consulta6, conexion);
                        adaptador6.Fill(tabladedatos6);
                    }
                    catch (Exception ex) { }
                    if (tabladedatos6.Rows[0]["factura"].ToString().Equals(""))
                    {
                        comprasCliente.numFactura = 1;
                    }
                    else
                    {
                        comprasCliente.numFactura = int.Parse(tabladedatos6.Rows[0]["factura"].ToString());
                        comprasCliente.numFactura++;
                    }
                    

                }
                    SqlCommand comando3 = new SqlCommand();
                    comando3.CommandType = CommandType.Text;
                    comando3.CommandText = "INSERT Ventas values(" + cantidad + ",'" + tabladedatos4.Rows[0]["nombre"].ToString() + "'," + inicioCliente.registrado + ",'" + listaProducto[i] + "'," + listaValor[i] + "," + listaCantidad[i] + "," + listaValorTotal[i]  +","+ comprasCliente.numFactura+ ")";
                    comando3.Connection = ConectarBD.GetConexion();
                    comando3.ExecuteNonQuery();           
                    

            }
            Response.Write("<script languaje='JavaScript'>alert('Compra exitosa');</script>");
            cancerlarTodo();
        }
    }
}