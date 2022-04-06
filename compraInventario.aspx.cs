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
    public partial class compraInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventariosAdmin.aspx");
        }
      
        
    }
}