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
    public partial class ComprasAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static ArrayList listaNit = new ArrayList();
        public static ArrayList listaNombreProv = new ArrayList();
        public static ArrayList listaValor = new ArrayList();
        public static ArrayList listaProducto = new ArrayList();
        public static ArrayList listaCantidad = new ArrayList();
        public static ArrayList listaValorTotal = new ArrayList();
        public static long valor, numFactura;
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            String canti= txtCantidad.Text;
            if (canti.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para agregar digita la cantidad');</script>");
            }
            else if (int.Parse(canti) <= 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('No se aceptan valores negativos');</script>");
            }
            else
            {
                int cantidad = int.Parse(canti);
                String nit=  listaProductos.SelectedItem.Value.ToString();
                String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";
                String consulta = "SELECT prov.nit,prov.nombre,prod.valorUnitario as valor,prod.tipoProdudcto as tipo FROM Producto prod,proveedor prov WHERE fk_nitProveedor=" + long.Parse(nit)+ " AND fk_nitProveedor=nit";
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }
                Boolean esta=false;
                for(int i =0; i < listaNit.Count; i++)
                {
                    if (listaProducto[i].Equals(tabladedatos.Rows[0]["tipo"].ToString()))
                    {
                        esta = true;
                    }
                }
                if(esta == false){
                    listaNit.Add(tabladedatos.Rows[0]["nit"].ToString());
                    listaNombreProv.Add(tabladedatos.Rows[0]["nombre"].ToString());
                    listaValor.Add(tabladedatos.Rows[0]["valor"].ToString());
                    listaProducto.Add(tabladedatos.Rows[0]["tipo"].ToString());
                    listaCantidad.Add(cantidad);
                    listaValorTotal.Add((long.Parse(tabladedatos.Rows[0]["valor"].ToString()) * cantidad));
                    mostrarTabla();
                    valor += (long.Parse(tabladedatos.Rows[0]["valor"].ToString()) * cantidad);
                    txtTotal.Text = string.Format("{0:c}", valor);
                }
                else
                {
                    Response.Write("<script languaje='JavaScript'>alert('Ya agregaste de este tipo, si quieres borralo y vuelve a agregar.');</script>");
                }
            }
        }

        public void mostrarTabla()
        {
            txtCantidad.Text = "";
            lblTabla.Text = "";
            string tabla = "";
            tabla = "<table border=3 bordercolor=gray>";
            tabla += "<tr>";
            tabla += "<td><FONT SIZE=4px>Num</td>";
            tabla += "<td><FONT SIZE=4px>Nit</td>";
            tabla += "<td><FONT SIZE=4px>Proveedor</td>";
            tabla += "<td><FONT SIZE=4px>Valor unitario</td>";
            tabla += "<td><FONT SIZE=4px>Producto</td>";
            tabla += "<td><FONT SIZE=4px>Cantidad</td>";
            tabla += "<td><FONT SIZE=4px>Valor total</td>";

            tabla += "</tr>";

            for (int i = 0; i < listaNit.Count; i++)
            {
                tabla += "<tr>";
                tabla += "<td><FONT SIZE=4px>" + (i+1) + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaNit[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaNombreProv[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaValor[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaProducto[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaCantidad[i] + "</td>";
                tabla += "<td><FONT SIZE=4px>" + listaValorTotal[i] + "</td>";
                tabla += "</tr>";
            }
            tabla += "</table>";
            lblTabla.Text = tabla;
        }
        protected void btnComprar_Click(object sender, EventArgs e)
        {
            String conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klina\\OneDrive\\Escritorio\\NOCHESVI´DEFINITIVO\\nochesVIP.mdf;Integrated Security=True;Connect Timeout=30";

            for(int i =0; i< listaNit.Count; i++)
            {                
                String consulta = "SELECT * FROM inventarios,proveedor  WHERE nitIdentificador=" + listaNit[i] + " AND nitIdentificador=nit";
                SqlDataAdapter adaptador;
                DataTable tabladedatos = new DataTable();
                try
                {
                    adaptador = new SqlDataAdapter(consulta, conexion);
                    adaptador.Fill(tabladedatos);
                }
                catch (Exception ex) { }

                if (int.Parse(tabladedatos.Rows[0]["cantidad"].ToString()) >= 0){
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandType = CommandType.Text;
                    int cant = int.Parse(listaCantidad[i].ToString());
                    long acom = cant + int.Parse(tabladedatos.Rows[0]["cantidad"].ToString());                    
                    comando2.CommandText = "UPDATE inventarios set cantidad=" + acom + " WHERE nitIdentificador=" + int.Parse(tabladedatos.Rows[0]["nit"].ToString());
                    comando2.Connection = ConectarBD.GetConexion();
                    comando2.ExecuteNonQuery();

                    String consulta3 = "SELECT MAX(IdCompra) as cantidad FROM Compras ";
                    SqlDataAdapter adaptador3;
                    DataTable tabladedatos3 = new DataTable();
                    try
                    {
                        adaptador3 = new SqlDataAdapter(consulta3, conexion);
                        adaptador3.Fill(tabladedatos3);
                    }
                    catch (Exception ex) { }
                    int cantidad;
                    if (tabladedatos3.Rows[0]["cantidad"].ToString().Equals("")){
                        cantidad = 1;
                    }
                    else
                    {
                        cantidad = int.Parse(tabladedatos3.Rows[0]["cantidad"].ToString());
                        cantidad++;
                    }

                    String consulta4 = "SELECT * FROM proveedor where nit="+listaNit[i];
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
                        String consulta6 = "SELECT MAX(numFactura) as factura FROM Compras ";
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
                            ComprasAdmin.numFactura = 1;
                        }
                        else
                        {
                            ComprasAdmin.numFactura = int.Parse(tabladedatos6.Rows[0]["factura"].ToString());
                            ComprasAdmin.numFactura++;
                        }

                       

                    }

                    SqlCommand comando3 = new SqlCommand();
                    comando3.CommandType = CommandType.Text;                
                    comando3.CommandText = "INSERT Compras values(" + cantidad + "," + listaNit[i] + ",'" + tabladedatos4.Rows[0]["Nombre"].ToString() + "','" + tabladedatos4.Rows[0]["telefono"].ToString() + "','"+listaProducto[i] +"',"+ listaCantidad[i] + ","+listaValor[i]+","+listaValorTotal[i]+","+ ComprasAdmin.numFactura + ")";
                    comando3.Connection = ConectarBD.GetConexion();
                    comando3.ExecuteNonQuery();
                   

                }
            }
            Response.Write("<script languaje='JavaScript'>alert('Compra exitosa');</script>");
            cancerlarTodo();
        }

       

        protected void btnCancelarEspecifico_Click1(object sender, EventArgs e)
        {
            String canti = txtCantidad.Text;
            if (canti.Equals(""))
            {
                Response.Write("<script languaje='JavaScript'>alert('Para Eliminar digita la posición');</script>");
            }else if(listaNit.Count < (int.Parse(canti)-1) )
            {
                Response.Write("<script languaje='JavaScript'>alert('Esa posición no existe');</script>");
            }else if (int.Parse(canti) <= 0)
            {
                Response.Write("<script languaje='JavaScript'>alert('No se aceptan valores negativos');</script>");
            }
            else
            {
                int posicion = int.Parse(canti);
                posicion--;
                long val = long.Parse(listaValorTotal[posicion].ToString());
                valor -= val;
                txtTotal.Text = string.Format("{0:c}", valor);
                listaNit.RemoveAt(posicion);
                listaNombreProv.RemoveAt(posicion);
                listaValor.RemoveAt(posicion);
                listaProducto.RemoveAt(posicion);
                listaCantidad.RemoveAt(posicion);
                listaValorTotal.RemoveAt(posicion);
                mostrarTabla();
            }
        }
        public void cancerlarTodo()
        {
            listaNit.Clear();
            listaNombreProv.Clear();
            listaValor.Clear();
            listaProducto.Clear();
            listaCantidad.Clear();
            listaValorTotal.Clear();
            mostrarTabla();
            lblTabla.Text = "";
            txtTotal.Text = "";
            txtCantidad.Text = "";
            ComprasAdmin.numFactura = 0;
        }
        protected void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            cancerlarTodo();
        }


    }
}