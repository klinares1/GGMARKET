using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NOCHESVI_DEFINITIVO
{
    public partial class inicioCliente : System.Web.UI.Page
    {
        public static String registrado;
        public static int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            i++;
            if (i == 1)
            {
                registrado = Request.Params.Get("cedula");
            }
        }
    }
}