using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;

namespace Familias3._1.MISC
{
    public partial class CambiarPreferencias : System.Web.UI.Page
    {
        static BDUsuario BDU;
        static Diccionario dic;
        static String U;
        static String L;
        static String S;
        static mast mst;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                U = mast.U;
                L = mast.L;
                S = mast.S;
                BDU = new BDUsuario();
                dic = new Diccionario(L, S);
                agregarSitios();
                lblSitio.Text = dic.sitio + ":";
                lblIdioma.Text = dic.msjCambiarIdioma;
                btnCambiarPref.Text = dic.guardar;
                mst = (mast)Master;
            }
        }

        protected void btnCambiarPref_Click(object sender, EventArgs e)
        {
            try
            {
                String nSitio = ddlSitio.SelectedValue;
                Boolean cambiarL = chkIdioma.Checked;
                String nIdioma = "";
                if (cambiarL)
                {
                    if (L.Equals("es"))
                    {
                        nIdioma = "en";
                    }
                    else
                        if (L.Equals("en"))
                        {
                            nIdioma = "es";
                        }
                }
                else
                {
                    nIdioma = L;
                }
                btnCambiarPref.Text = nIdioma;
                BDU.cambiarPreferencias(U, nSitio, nIdioma);
                Response.Redirect("~/MISC/Buscar.aspx");
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void agregarSitios()
        {
            ddlSitio.Items.Add(new ListItem("", S));
            DataTable dtSitios = BDU.obtenerSitios(L, S);
            String nombreSitio;
            String idSitio;
            ListItem itemS;
            foreach (DataRow row in dtSitios.Rows)
            {
                nombreSitio = row["Site"].ToString();
                idSitio = row["Code"].ToString();
                itemS = new ListItem(nombreSitio, idSitio);
                ddlSitio.Items.Add(itemS);

            }
        }

    }
}