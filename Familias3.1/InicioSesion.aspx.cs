using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        private static String U;
        private static int estadoLogin = 1;
        private static String idioma;
        private static String msjAceptar = "Aceptar / Accept";
        private static String msjCambiar = "Cambiar / Changue";
        private static String msjTituloPswNoCumple = "<table style=\"textalign:left\"><tr><td><b>La nueva contraseña no cumple con las reglas: / The new password does not comply with the rules:</b></td></tr>";
        private static Boolean hayAlertaPsw = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Advertenciatb.Visible = false;
                Cambio_Contraseña.Visible = false;
                txtuser.Focus();
                // contenedor.Attributes["style"] = "top:180;";
                revTxtUser.ErrorMessage = "Este parámetro es necesario. / This parameter is necesary.";
                revTxtPass.ErrorMessage = "Este parámetro es necesario. / This parameter is necesary.";
            }
        }

        protected void Ingresar_Click(object sender, EventArgs e)
        {
            BDUsuario objBD = new BDUsuario();
            BDAPAD APD = new BDAPAD();
            string nombreUsuario = txtuser.Text;
            string contraseña = txtpass.Text;
            int resultado = objBD.consultarCredenciales(nombreUsuario, contraseña);

            if (resultado == 1)
            {
                Session["nombre"] = nombreUsuario;
                Session["pass"] = contraseña;

                DataTable resul = objBD.ObtenerEnterodbs(nombreUsuario);
                int qd = Convert.ToInt32(resul.Rows[0][0]);
                Response.Write(qd);
                if (qd == 0)
                {
                    mostrarMsj("Expiró el tiempo de su contraseña, por favor, cambiela. / Expire the time of your password, please, change it.", msjCambiar);
                    Regresarbtn.Visible = true;
                    estadoLogin = 5;
                }
                else if (qd < 16 && qd > 0)
                {
                    mostrarMsj("Faltan " + qd + " días para el cambio de su contraseña. / " + qd + " days left to change your password.", msjCambiar);
                    hayAlertaPsw = true;
                    contenedor.Style.Add("width", "400px");
                    qd = 0;
                    Regresarbtn.Visible = true;
                    Cancelarbtn.Visible = true;
                    estadoLogin = 4;
                }
                else
                {

                    iniciarSesion(nombreUsuario);

                    //FormsAuthentication.RedirectFromLoginPage(nombreUsuario, false);
                    //Session["nombre"] = null;
                    //Session["pass"] = null;
                    ////if (Session["Init"] == null)
                    //{
                    //    //Session["Init"] = "SI";
                    //    Response.Redirect("MISC/Buscar.aspx");
                    //}
                    ////else
                    ////if (((String)Session["Init"]).Equals("SI"))
                    ////{
                    ////    Session["Init"] = "SI";
                    ////    Response.Write("<script language=javascript> history.back(); </script>");
                    ////}

                }
            }
            else if (resultado == 2)
            {
                mostrarMsj("Contraseña incorrecta. / Incorrect password.", msjAceptar);
                estadoLogin = 2;
            }
            else if (resultado == 3)
            {
                mostrarMsj("Usuario incorrecto. / Incorrect user.", msjAceptar);
                estadoLogin = 3;
            }
            else if (resultado == 0)
            {
                mostrarMsj("Error de Conexion: Cierre e inicie otra vez la aplicación. / Connection Error: Close and start the application, again.", msjAceptar);
                estadoLogin = 0;
            }
            else
            {
                mostrarMsj("Ha ocurrido un error inesperado, por favor, envíe esta información a Sistemas. / An unexpected error has occurred, please, send this information to IT department.", msjAceptar);
                estadoLogin = -1;
            }
        }

        protected void iniciarSesion(String nombreUsuario)
        {
            FormsAuthentication.RedirectFromLoginPage(nombreUsuario, false);
            Session["nombre"] = null;
            Session["pass"] = null;
            Response.Redirect("MISC/Buscar.aspx");
        }
        protected void Aceptarbtn_Click(object sender, EventArgs e)
        {
            if (estadoLogin == 4 || estadoLogin == 5 || estadoLogin == 7)
            {
                Advertenciatb.Visible = false;
                Formulario.Visible = false;
                Cambio_Contraseña.Visible = true;
                Instrucciones.Visible = true;
                if (estadoLogin == 4 || estadoLogin == 5)
                {
                    idioma = "es";
                    instrucciones();
                    Regresarbtn.Visible = false;
                }
                if (estadoLogin == 4)
                {
                    btnCancelarCambio.Visible = true;
                    Aceptarbtn.Text = msjAceptar;
                    Cancelarbtn.Visible = false;
                }
                if (estadoLogin == 7 && !hayAlertaPsw)
                    contenedor.Style.Add("width", "290px");
                if (hayAlertaPsw)
                    contenedor.Style.Add("width", "400px");
                txtactualc.Focus();
                estadoLogin = 6;
            }
            else if (estadoLogin == 2 || estadoLogin == 3 || estadoLogin == 8 || estadoLogin == 0 || estadoLogin == -1)
            {
                regresarALogin();
            }


        }
        protected void Cancelarbtn_Click(object sender, EventArgs e)
        {
            String nombreUsuario = txtuser.Text;
            iniciarSesion(nombreUsuario);
        }
        protected void Regresarbtn_Click(object sender, EventArgs e)
        {
            regresarALogin();
        }


        protected void btncambio_Click(object sender, EventArgs e)
        {
            String newPsw;
            String confPsw;
            String actPsw;
            String nombreUsuario = txtuser.Text;
            BDUsuario objBD = new BDUsuario();
            newPsw = txtnuevac.Text;
            confPsw = txtconfirmarc.Text;
            actPsw = txtactualc.Text;
            string nombre = Convert.ToString(Session["nombre"]);
            DataTable a = objBD.verificarUltimas4Psw(nombre, newPsw);
            int diferenteAUltimas4Psw = Convert.ToInt32(a.Rows[0][0]);
            string msjAlertasPsw = "";
            int error = 0;

            string verificar = objBD.verificarCaracteresPsw(newPsw, nombreUsuario);
            string mayus = "M";
            string minus = "m";
            string num = "1";
            string carac = "8";

            if (diferenteAUltimas4Psw == 0)
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- No es distinta a las últimas 4. / It is not different from the last 4.</td></tr>";
            }
            if (mayus.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- No contiene mayúsculas. / It does not contain capital letters.</td></tr>";
            }

            if (minus.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- No contiene minúsculas. / It does not contain lowercase letters.</td></tr>";
            }

            if (num.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- No contiene números. / It does not contain numbers.</td></tr>";
            }

            if (carac.Any(verificar.Contains))
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- Tiene menos de 8 caracteres. / It is less than 8 characters.</td></tr>";
            }

            if (objBD.verificarNombrePsw(newPsw, nombreUsuario) != 0)
            {
                error = error + 1;
                msjAlertasPsw = msjAlertasPsw + "<tr><td>- Incluye datos de su nombre o fechas fáciles de decifrar.  / It includes your name data or easy hacking dates.</td></tr>";
            }

            if (newPsw == confPsw)
            {
                int sonIguales = objBD.verificarContraseña(nombreUsuario, actPsw);
                if (sonIguales == 1)
                {
                    if (newPsw != actPsw)
                    {
                        if (error > 0)
                        {
                            mostrarMsj(msjTituloPswNoCumple + msjAlertasPsw + "</table>", msjAceptar);
                            contenedor.Style.Add("width", "400px");
                            estadoLogin = 7;
                        }
                        else
                        {
                            objBD.cambiarContraseña(nombreUsuario, newPsw);
                            Session["nombre"] = null;
                            Session["pass"] = null;
                            mostrarMsj("Cambio de contraseña exitoso. A continuación, inicie sesión con su nueva contraseña. / Successful password change. Then, log in using your new password.", msjAceptar);
                            estadoLogin = 8;
                        }
                    }
                    else
                    {
                        mostrarMsj("No se puede actualizar, ya que ingresó la misma contraseña. / Cannot be updated, since you entered the same password.", msjAceptar);
                        estadoLogin = 7;
                        txtactualc.Focus();
                    }
                }
                else
                {
                    mostrarMsj("Esa no es su contraseña actual. / That is not your current password.", msjAceptar);
                    estadoLogin = 7;
                    txtactualc.Focus();
                }
            }
            else
            {
                mostrarMsj("Esos valores, no son idénticos. / Those values ​​are not identical.", msjAceptar);
                estadoLogin = 7;
                txtactualc.Focus();
            }
        }
        protected void mostrarMsj(String msj, String msjBtn)
        {
            Cambio_Contraseña.Visible = false;
            Advertenciatb.Visible = true;
            Formulario.Visible = false;
            Instrucciones.Visible = false;
            lblmens.Text = msj;
            contenedor.Style.Add("width", "290px");
            Aceptarbtn.Text = msjBtn;
        }
        protected void regresarALogin()
        {
            Formulario.Visible = true;
            Cambio_Contraseña.Visible = false;
            Advertenciatb.Visible = false;
            Instrucciones.Visible = false;
            Regresarbtn.Visible = false;
            Cancelarbtn.Visible = false;
            btnCancelarCambio.Visible = false;
            hayAlertaPsw = false;
            contenedor.Style.Add("width", "290px");
            estadoLogin = 1;
        }
        protected void btnCancelarCambio_Click(object sender, EventArgs e)
        {
            String nombreUsuario = txtuser.Text;
            iniciarSesion(nombreUsuario);
        }
        protected void btnRegresarLogIn_Click(object sender, EventArgs e)
        {
            regresarALogin();
        }
        protected void btnlenguaje_Click(object sender, EventArgs e)
        {
            instrucciones();
            Cambio_Contraseña.Visible = true;
            Instrucciones.Visible = true;
        }

        protected void instrucciones()
        {
            if (idioma.Equals("en"))
            {
                btnlenguaje.Text = "Instrucciones en Español";
                lbltitul.Text = " Instructions:";
                lblmen.Text = " The new password must comply with the following rules:";
                lblreg1.Text = " 1. Have capital and lowercase letters.";
                lblreg2.Text = " 2. Contain numbers.";
                lblreg3.Text = " 3. Contain more than 7 characters.";
                lblreg4.Text = "  4. Not include your name data or easy hacking dates.";
                lblreg5.Text = "  5. Should not be repeated with the last 4 passwords.";
                idioma = "es";
            }
            else if (idioma.Equals("es"))
            {
                btnlenguaje.Text = "English Instructions";
                lbltitul.Text = " Instrucciones:";
                lblmen.Text = " La nueva contraseña debe cumplir con las siguientes reglas:";
                lblreg1.Text = " 1. Tener mayúsculas y minúsculas.";
                lblreg2.Text = " 2. Contener números.";
                lblreg3.Text = " 3. Contener más de 7 caracteres.";
                lblreg4.Text = " 4. No incluir datos de su nombre o fechas fáciles de decifrar.";
                lblreg5.Text = " 5. No se debe repetir con las últimas 4 contraseñas.";
                idioma = "en";
            }
        }


    }
}