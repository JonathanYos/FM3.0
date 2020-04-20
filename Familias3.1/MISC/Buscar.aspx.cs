using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.MISC
{
    public partial class Buscar : System.Web.UI.Page
    {
        static String L;
        static String S;
        static Diccionario dic;
        protected static mast mst;
        protected void Page_Load(object sender, EventArgs e)
        {
            L = mast.L;
            S = mast.S;
            dic = new Diccionario(L, S);
            mst = (mast)Master;
            mst.borrarIds();
            lblMemberId.Text = dic.idMiembro + ":";
            lblFamilyId.Text = dic.idFamilia + ":";
            lblFaroId.Text = dic.idFaro + ":";
            lnkBuscarFamilias.Text = dic.familias;
            lnkBuscarMiembrosInfoEduc.Text = dic.MiembrosPorEducInfo;
            lnkBuscarMiembrosOtraInfo.Text = dic.MiembrosPorOtraInfo;
            lblBuscar.Text = dic.buscar + ":";
            if (L.Equals("es"))
            {
                lblO.Text = "o";
            }
            else
            {
                lblO.Text = "or";
            }
            configurarPorSitio();
            txbMemberId.Focus();
        }

        protected void configurarPorSitio()
        {
            if (S.Equals("E") || S.Equals("A"))
            {
                lnkBuscarMiembrosInfoEduc.Visible = false;
                lnkBuscarFamilias.Visible = false;
                lblComa.Visible = false;
                lblO.Visible = false;
            }
            else
                if (S.Equals("R"))
                {
                    lblFaroId.Visible = true;
                    txbFaroId.Visible = true;
                }
        }

        protected void lblBuscar_Click(object sender, EventArgs e)
        {
            String memberId = txbMemberId.Text;
            String familyId = txbFamilyId.Text;
            String faroId = txbFaroId.Text;
            if (!String.IsNullOrEmpty(memberId) && String.IsNullOrEmpty(familyId) && String.IsNullOrEmpty(faroId))
            {
                DataTable dtMiembro = new BDMiembro().obtenerDatos(S, memberId, L);
                if (dtMiembro.Rows.Count > 0)
                {
                    Session["M"] = memberId;
                    Response.Redirect("~/MISC/PerfilMiembro.aspx");
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroMiembro);
                }
            }
            else if (String.IsNullOrEmpty(memberId) && !String.IsNullOrEmpty(familyId) && String.IsNullOrEmpty(faroId))
            {

                DataTable dtFamilia = new BDFamilia().obtenerDatos(S, familyId, L);
                if (dtFamilia.Rows.Count > 0)
                {
                    Session["F"] = familyId;
                    Response.Redirect("~/MISC/PerfilFamilia.aspx");
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroFamilia);
                }
            }
            else if (String.IsNullOrEmpty(memberId) && String.IsNullOrEmpty(familyId) && !String.IsNullOrEmpty(faroId))
            {
                String familyIdFaro = "";
                familyIdFaro = (new BDFamilia().obtenerIdAPartirDeFaro(S, faroId)) + "";
                DataTable dtFamilia = new BDFamilia().obtenerDatos(S, familyIdFaro, L);
                if (dtFamilia.Rows.Count > 0)
                {
                    Session["F"] = familyIdFaro;
                    Response.Redirect("~/MISC/PerfilFamilia.aspx");
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroFamilia);
                }
            }else 
            {
                mst.mostrarMsjAdvNtf(dic.msjDebeingresarUno);
            }
        }
        public void btnNuevaVst_Click(object sender, EventArgs e)
        {

        }
    }
}