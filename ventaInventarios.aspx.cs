using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;


namespace NOCHESVI_DEFINITIVO
{
    public partial class ventaInventarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("InventariosAdmin.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ReportParameter p = new ReportParameter("numFactura",txtnumFactura.Text);
            ReportViewer1.LocalReport.SetParameters(p);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}