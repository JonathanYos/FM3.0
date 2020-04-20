using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;

namespace Familias3._1.MISC
{
    public partial class CambiarContraseña : System.Web.UI.Page
    {
        static BDUsuario BDU;
        static Diccionario dic;
        static String U;
        static String L;
        static String S;
        static mast mst;
        private static String msjTituloPswNoCumple;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BDU = new BDUsuario();
                U = mast.U;
                L = mast.L;
                S = mast.S;
                BDU = new BDUsuario();
                dic = new Diccionario(L, S);
                lblNuevaPsw.Text = "*" + dic.nuevaPsw + ":";
                lblConfPsw.Text = "*" + dic.confirmarPsw + ":";
                lblActPsw.Text = "*" + dic.actualPsw + ":";
                msjTituloPswNoCumple = "<table style=\"textalign:left\"><tr><td><b>" + (L.Equals("es") ? "La nueva contraseña no cumple con las reglas:" : "The new password does not comply with the rules:") + "</b></td></tr>";
                refTxbActPsw.ErrorMessage = dic.msjCampoNecesario;
                refTxbConfPsw.ErrorMessage = dic.msjCampoNecesario;
                refTxbNuevaPsw.ErrorMessage = dic.msjCampoNecesario;
                txbActPsw.MaxLength = 15;
                txbConfPsw.MaxLength = 15;
                txbNuevaPsw.MaxLength = 15;
                btnCambiarPsw.Text = dic.actualizar;
                btnVerReglas.Text = L.Equals("es") ? "Mostrar Reglas" : "Show Rules";
                txbActPsw.Focus();
            }
            mst = (mast)Master;
        }

        protected void btnCambiarPsw_Click(object sender, EventArgs e)
        {
            String newPsw = "";
            String confPsw = "";
            String actPsw = "";
            newPsw = txbNuevaPsw.Text;
            confPsw = txbConfPsw.Text;
            actPsw = txbActPsw.Text;
            DataTable a = BDU.verificarUltimas4Psw(U, newPsw);
            int diferenteAUltimas4Psw = Convert.ToInt32(a.Rows[0][0]);
            string msjAlertasPsw = "";
            int error = 0;

            string verificar = BDU.verificarCaracteresPsw(newPsw, U);
            string mayus = "M";
            string minus = "m";
            string num = "1";
            string carac = "8";

            if (diferenteAUltimas4Psw == 0)
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "No es distinta a las últimas 4." : "It is not different from the last 4.") + "</td></tr>";
            }
            if (mayus.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "No contiene mayúsculas." : "It does not contain capital letters.") + "</td></tr>";
            }

            if (minus.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "No contiene minúsculas." : "It does not contain lowercase letters.") + "</td></tr>";
            }

            if (num.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "No contiene números." : "It does not contain numbers.") + "</td></tr>";
            }

            if (carac.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "Tiene menos de 8 caracteres." : "It is less than 8 characters.") + "</td></tr>";
            }

            if (BDU.verificarNombrePsw(newPsw, U) != 0)
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- " + (L.Equals("es") ? "Incluye datos de su nombre o fechas fáciles de decifrar." : "It includes your name data or easy hacking dates.") + "</td></tr>";
            }

            if (newPsw == confPsw)
            {
                int sonIguales = BDU.verificarContraseña(U, actPsw);
                if (sonIguales == 1)
                {
                    if (newPsw != actPsw)
                    {
                        if (error > 0)
                        {
                            mst.mostrarMsjMdl(msjTituloPswNoCumple + msjAlertasPsw + "</table>");
                        }
                        else
                        {
                            try
                            {
                                BDU.cambiarContraseña(U, newPsw);
                                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                            }
                            catch (Exception ex)
                            {
                                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                            }
                        }
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf(dic.msjNoSePermiteMismaPsw);
                        txbActPsw.Focus();
                    }
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjEsaNoEsPsw);
                    txbActPsw.Focus();
                }
            }
            else
            {
                mst.mostrarMsjAdvNtf(dic.msjNoSonIdenticosPsw);
                txbActPsw.Focus();
            }
        }

        protected void btnVerReglas_Click(object sender, EventArgs e)
        {
            String msjReglas = "";
            if (L.Equals("es"))
            {
                msjReglas = "<table><tr><td><b>La nueva contraseña debe cumplir con las siguientes reglas:</b></td></tr>";
                msjReglas = msjReglas + "<tr><td>1. Tener mayúsculas y minúsculas.</td></tr>";
                msjReglas = msjReglas + "<tr><td>2. Contener números.</td></tr>";
                msjReglas = msjReglas + "<tr><td>3. Contener más de 7 caracteres.</td></tr>";
                msjReglas = msjReglas + "<tr><td>4. No incluir datos de su nombre o fechas fáciles de decifrar.</td></tr>";
                msjReglas = msjReglas + "<tr><td>5. No se debe repetir con las últimas 4 contraseñas.</td></tr></table>";
            }
            else
            {
                msjReglas = "<table><tr><td><b>The new password must comply with the following rules:</b></td></tr>";
                msjReglas = msjReglas + "<tr><td>1. Have capital and lowercase letters.</td></tr>";
                msjReglas = msjReglas + "<tr><td>2. Contain numbers.</td></tr>";
                msjReglas = msjReglas + "<tr><td>3. Contain more than 7 characters.</td></tr>";
                msjReglas = msjReglas + "<tr><td>4. Not include your name data or easy hacking dates.</td></tr>";
                msjReglas = msjReglas + "<tr><td>5. Should not be repeated with the last 4 passwords.</td></tr></table>";
            }
            mst.mostrarMsjMdl(msjReglas);
        }
    }
}