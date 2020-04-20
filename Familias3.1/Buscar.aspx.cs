using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1
{
    public partial class SEARCH : System.Web.UI.Page
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
            txbMemberId.Focus();
        }

        protected void lblBuscar_Click(object sender, EventArgs e)
        {
            String memberId = txbMemberId.Text;
            String familyId = txbFamilyId.Text;
            if (!String.IsNullOrEmpty(memberId) && !String.IsNullOrEmpty(familyId))
            {
                mst.mostrarMsjAdv(dic.msjDebeingresarUno);
            }
            else if (String.IsNullOrEmpty(memberId) && String.IsNullOrEmpty(familyId))
            {
                mst.mostrarMsjAdv(dic.msjDebeingresarUno);
            }
            else if (!String.IsNullOrEmpty(memberId) && String.IsNullOrEmpty(familyId))
            {
                Session["M"] = memberId;
                Response.Redirect("PerfilMiembro.aspx");
            }
            else if (String.IsNullOrEmpty(memberId) && !String.IsNullOrEmpty(familyId))
            {
                Session["F"] = familyId;
                Response.Redirect("PerfilFamilia.aspx");
            }
        }
        public void btnNuevaVst_Click(object sender, EventArgs e)
        {

        }
    }
}