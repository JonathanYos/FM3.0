using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;

namespace Familias3._1
{
    public partial class Pruebas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gdvPruebas.DataSource = new BDUsuario().obtenerFuncionesDeUsuario("FranciscoB", "F");
                gdvPruebas.DataBind();
            }
        }
    }
}