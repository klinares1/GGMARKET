using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NOCHESVI_DEFINITIVO
{
    public partial class GraficosAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVentas_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("graficosVentas.aspx");
        }

        protected void btnCompras_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("graficosCompras.aspx");
        }
    }
}