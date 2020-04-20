using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
namespace Familias3._1
{
    public partial class Login : System.Web.UI.Page
    {
        private static String U;
        private static int estadoLogin = 1;
        private static String idioma;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                idioma = "es";
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
                    Advertenciatb.Visible = true;
                    Formulario.Visible = false;
                    Cambio_Contraseña.Visible = false;
                    Instrucciones.Visible = false;
                    lblmens.Text = "Expiró el tiempo de su contraseña, por favor, cambiela. / Expire the time of your password, please, change it.";
                    Aceptarbtn.Text = "Cambiar / Changue";
                    instrucciones();
                    estadoLogin = 4;
                }
                else if (qd < 16 && qd > 0)
                {
                    Advertenciatb.Visible = true;
                    Formulario.Visible = false;
                    Cambio_Contraseña.Visible = false;
                    Instrucciones.Visible = false;
                    lblmens.Text = "Faltan " + qd + " días para el cambio de su contraseña. / " + qd + " days left to change your password.";
                    Aceptarbtn.Text = "Aceptar / Accept";
                    qd = 0;
                    estadoLogin = 7;
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(nombreUsuario, false);
                    Session["nombre"] = null;
                    Session["pass"] = null;
                    //if (Session["Init"] == null)
                    {
                        //Session["Init"] = "SI";
                        Response.Redirect("MISC/Buscar.aspx");
                    }
                    //else
                    //if (((String)Session["Init"]).Equals("SI"))
                    //{
                    //    Session["Init"] = "SI";
                    //    Response.Write("<script language=javascript> history.back(); </script>");
                    //}

                }
            }
            else if (resultado == 2)
            {
                Cambio_Contraseña.Visible = false;
                Advertenciatb.Visible = true;
                Formulario.Visible = false;
                lblmens.Text = "Contraseña incorrecta. / Incorrect password.";
                estadoLogin = 2;
            }
            else if (resultado == 3)
            {
                Advertenciatb.Visible = true;
                Formulario.Visible = false;
                Cambio_Contraseña.Visible = false;
                lblmens.Text = "Usuario incorrecto. / Incorrect user.";
                estadoLogin = 3;
            }
            else if (resultado == 0)
            {
                Advertenciatb.Visible = true;
                Formulario.Visible = false;
                Cambio_Contraseña.Visible = false;
                lblmens.Text = "Error de Conexion: Cierre e inicie otra vez la aplicación. / Connection Error: Close and start the application, again.";
                estadoLogin = 0;
            }
            else
            {
                Advertenciatb.Visible = true;
                Formulario.Visible = false;
                Cambio_Contraseña.Visible = false;
                lblmens.Text = "Ha ocurrido un error inesperado, por favor, envíe esta información a Sistemas. / An unexpected error has occurred, please, send this information to IT department.";
                estadoLogin = -1;
            }
        }

        protected void Aceptarbtn_Click(object sender, EventArgs e)
        {
            if (estadoLogin == 4)
            {
                Advertenciatb.Visible = false;
                Formulario.Visible = false;
                Cambio_Contraseña.Visible = true;
                Instrucciones.Visible = true;
            }
            else
            {
                if (estadoLogin == 5)
                {
                    Advertenciatb.Visible = false;
                    Formulario.Visible = false;
                    Cambio_Contraseña.Visible = true;
                    Instrucciones.Visible = true;
                }
                else
                {
                    if (estadoLogin == 6)
                    {
                        Instrucciones.Visible = false;
                        Advertenciatb.Visible = false;
                        Formulario.Visible = true;
                        Cambio_Contraseña.Visible = false;
                       // contenedor.Attributes["style"] = "top:180;";
                    }
                    else
                    {
                        if (estadoLogin == 7)
                        {
                            string nombreUsuario = Convert.ToString(Session["nombre"]);
                            FormsAuthentication.RedirectFromLoginPage(nombreUsuario, false);
                            Response.Redirect("MISC/Buscar.aspx");
                            Session["nombre"] = null;
                            Session["pass"] = null; 
                        }
                        else
                        {
                            Formulario.Visible = true;
                            Cambio_Contraseña.Visible = false;
                            Advertenciatb.Visible = false;
                            Instrucciones.Visible = false;
                            Ingresar.Text = estadoLogin + "";
                        }
                    }

                }
            }


        }

        public string verificar_contra(String cont)
        {
            string[] mayus = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Ñ" };
            string[] minus = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] num = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sql = "SELECT CompleteName FROM dbo.FwEmployee WHERE EmployeeId= '" + U + "'";
            //string nombre = APD.obtienePalabra(sql, "CompleteName");
            string res;
            if (mayus.Any(cont.Contains) && minus.Any(cont.Contains) && num.Any(cont.Contains) && cont.Length > 7)
            {
                res = "ok";
            }
            else
            {
                res = "no";
                if ((mayus.Any(cont.Contains)))
                {

                }
                else
                {
                    res = res + "M";
                }
                if (minus.Any(cont.Contains))
                {

                }
                else
                {
                    res = res + "m";
                }
                if (num.Any(cont.Contains))
                {

                }
                else
                {
                    res = res + "1";
                }
                if (cont.Length > 7)
                {

                }
                else
                {
                    res = res + "8";
                }

            }
            return res;
        }

        protected void btncambio_Click(object sender, EventArgs e)
        {
            string passnueva;
            string confirpass;
            string passactual;
            BDUsuario objBD = new BDUsuario();
            BDAPAD APD = new BDAPAD();
            passnueva = txtnuevac.Text;
            confirpass = txtconfirmarc.Text;
            passactual = txtactualc.Text;
            string nombre = Convert.ToString(Session["nombre"]);
            DataTable a = objBD.verificarUltimas4Psw(nombre, passnueva);
            int prueba = Convert.ToInt32(a.Rows[0][0]);
            string msj = "";
            int error = 0;

            string verificar = verificar_contra(passnueva);
            string mayus = "M";
            string minus = "m";
            string num = "1";
            string carac = "8";


            if (prueba == 0)
            {
                error = error + 1;
            }

            if (prueba == 0)
            {
                error = error + 1;
            }
            if (passnueva != confirpass)
            {
                error = error + 1;
            }
            if (passnueva == passactual)
            {
                error = error + 1;
            }
            if (mayus.Any(verificar.Contains))
            {
                error = error + 1;
            }

            if (minus.Any(verificar.Contains))
            {
                error = error + 1;
            }

            if (num.Any(verificar.Contains))
            {
                error = error + 1;
            }

            if (carac.Any(verificar.Contains))
            {
                error = error + 1;
            }
            msj = "";


            if (error > 0)
            {
                Cambio_Contraseña.Visible = false;
                Advertenciatb.Visible = true;
                Formulario.Visible = false;
                lblmens.Text = "La contraseña no cumple con las reglas. / The password does not comply with the rules.";
                Aceptarbtn.Text = "Aceptar / Accept";
                Instrucciones.Visible = false;
                estadoLogin = 5;
            }
            else
            {
                if (error == 0)
                {
                    int esnombre;
                    esnombre = ComprobarNombre(passnueva);
                    if (esnombre == 0)
                    {
                        string sql = "UPDATE FwEmployeePassword SET Pass3=Pass2, Pass2=Pass1, Pass1=FwEmployeePassword.Password, FwEmployeePassword.Password='" + passnueva + "', PasswordDate=GETDATE() WHERE EmployeeId='" + nombre + "' ";
                        Response.Write(sql);
                        APD.ejecutarSQL(sql);
                        Session["nombre"] = null;
                        Session["pass"] = null;
                        Cambio_Contraseña.Visible = false;
                        Advertenciatb.Visible = true;
                        Formulario.Visible = false;
                        Instrucciones.Visible = false;
                        lblmens.Text = "Cambio de contraseña exitoso. A continuación, inicie sesión con su nueva contraseña. / Successful password change. Then, log in using your new password.";
                        Aceptarbtn.Text = "Aceptar / Accept";
                        estadoLogin = 6;
                    }
                    else
                    {
                        Cambio_Contraseña.Visible = false;
                        Advertenciatb.Visible = true;
                        Formulario.Visible = false;
                        lblmens.Text = "La contraseña no cumple con las reglas. / The password does not comply with the rules.";
                        Aceptarbtn.Text = "OK";
                        Instrucciones.Visible = false;
                        estadoLogin = 5;
                    }
                }
            }
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
                lbltitul.Text = " Intructions:";
                lblmen.Text = " The new password must comply with the following rules:";
                lblreg1.Text = " 1. Have capital letters and lowercases.";
                lblreg2.Text = " 2. Contain numbers.";
                lblreg3.Text = " 3. Contain more than 8 characters.";
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
                lblreg3.Text = " 3. Contener más de 8 caracteres.";
                lblreg4.Text = " 4. No incluir datos de su nombre o fechas fáciles de decifrar.";
                lblreg5.Text = " 5. No se debe repetir con las últimas 4 contraseñas.";
                idioma = "en";
            }
        }

        private int ComprobarNombre(string nueva)
        {
            BDUsuario objBD = new BDUsuario();
            BDAPAD APD = new BDAPAD();
            string sql = "SELECT CompleteName FROM dbo.FwEmployee WHERE EmployeeId= '" + Session["nombre"] + "'";
            string nombre2 = APD.obtienePalabra(sql, "CompleteName");
            string[] nombre3 = nombre2.Split(' ');
            int conteo3 = 0;
            foreach (string numero in nombre3)
            {

                string sql3 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + numero + "%')";
                string numero5 = APD.obtienePalabra(sql3, "conteo");
                int conteo6 = Convert.ToInt32(numero5);
                conteo3 = conteo3 + conteo6;

                string remplazo = numero.Replace("á", "a");
                remplazo = remplazo.Replace("é", "e");
                remplazo = remplazo.Replace("í", "i");
                remplazo = remplazo.Replace("ó", "o");
                remplazo = remplazo.Replace("ú", "u");
                remplazo = remplazo.Replace("Á", "A");
                remplazo = remplazo.Replace("É", "E");
                remplazo = remplazo.Replace("Í", "I");
                remplazo = remplazo.Replace("Ó", "O");
                remplazo = remplazo.Replace("Ú", "U");
                string sql2 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + remplazo + "%')";
                string numero4 = APD.obtienePalabra(sql2, "conteo");
                int conteo2 = Convert.ToInt32(numero4);
                conteo3 = conteo3 + conteo2;

            }
            return conteo3;
        }

        

    }
}