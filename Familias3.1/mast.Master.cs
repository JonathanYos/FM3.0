using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Drawing;
namespace Familias3._1
{
    public partial class mast : System.Web.UI.MasterPage
    {
        public static String U;
        public static String S;
        public static String L;
        public static String M;
        public static String F;
        public static String P;
        public static Boolean vista;
        public static BDUsuario UBD;
        public static BDMiembro MBD;
        static Diccionario dic;
        public static Seguridad s;
        protected static Color colorMsj;
        protected static Color colorMsjAdv;
        protected static Boolean auxFamilia;
        protected static Boolean auxMiembro;
        public event EventHandler contentCallEvent;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                this.redir("~/Login.aspx");
            }
            s = new Seguridad();
            UBD = new BDUsuario();
            colocarTitulo();
            if (!IsPostBack)
            {
                try
                {
                    iniciarComponentes();
                    colocarTitulo();
                }
                catch (Exception ex)
                {

                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public void borrarIds()
        {
            pnlMiembroSel.Visible = false;
            lblMiembroSel.Text = "";
            lnkMiembroSel.Text = "";
            pnlFamiliaSel.Visible = false;
            lblFamiliaSel.Text = "";
            lnkFamiliaSel.Text = "";
            Session["M"] = null;
            Session["F"] = null;
        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
        }
        public Boolean verificarFuncion(String funcion)
        {
            return UBD.verificarFuncion(S, U, funcion);
        }

        protected void iniciarComponentes()
        {
            MBD = new BDMiembro();
            U = Request.ServerVariables["AUTH_USER"].ToString();
            L = UBD.consultarIdioma(U);
            S = UBD.consultarSitio(U);
            M = (String)Session["M"];
            F = (String)Session["F"];
            P = (String)Session["P"];
            vista = false;
            lblUsuario.Text = U;
            dic = new Diccionario(L, S);
            imgCommonHopeLogo.ImageUrl = "~/Images/FamiliasdeEsperanza_Logo_RGBHeader.png";
            lblBuscar.Text = dic.buscar;
            lblMenu.Text = dic.menu;
            lblRepetir.Text = dic.repetir;
            lblSitio.Text = UBD.consultarSNombre(U);
            lnkChanPref.Text = dic.cambiarPref;
            lnkChanPsw.Text = dic.cambiarPsw;
            lnkLogout.Text = dic.cerrarSesion;
            //colorMsj = System.Drawing.ColorTranslator.FromHtml("#179cd8");
            colorMsjAdv = System.Drawing.ColorTranslator.FromHtml("#909090");
            colorMsj = System.Drawing.ColorTranslator.FromHtml("#0285c2");
            llenarMenu();
            llenarIds();
            seguridad();
        }
        public void seleccionarMiembro(String miembro)
        {
            Session["M"] = null;
            Session["F"] = null;
            Session["M"] = miembro;
            M = (String)Session["M"];
            F = (String)Session["F"];
            llenarIds();
        }
        public void seleccionarFamilia(String familia)
        {
            Session["M"] = null;
            Session["F"] = null;
            Session["F"] = familia;
            M = (String)Session["M"];
            F = (String)Session["F"];
            llenarIds();
        }
        public void llenarIds()
        {
            if (!String.IsNullOrEmpty(F))
            {
                pnlMiembroSel.Visible = false;
                pnlFamiliaSel.Visible = true;
                lblFamiliaSel.Text = "<b>" + dic.familia + ":</b>";
                lnkFamiliaSel.Text = S + F;
                DataTable dtFaro = new BDFamilia().obtenerDatos(S, F, L);
                if (dtFaro.Rows.Count > 0)
                {
                    String faro = dtFaro.Rows[0]["RFaroNumber"].ToString();
                    if (!String.IsNullOrEmpty(faro) && !faro.Equals("0"))
                    {
                        lblFaroSel.Text = "&nbsp;<b>" + "Faro" + ":</b>&nbsp;" + faro;
                        lblFaroSel.Visible = true;
                    }
                }
            }
            if (!String.IsNullOrEmpty(M))
            {
                DataTable dtMiembro = new BDMiembro().obtenerDatos(S, M, L); ;
                lblMiembroSel.Text = "<b>" + dic.miembro + ":</b>";
                String nombreCompleto = dtMiembro.Rows[0]["FirstNames"].ToString() + " " + dtMiembro.Rows[0]["LastNames"].ToString();
                lblNombreMiembroSel.Text = nombreCompleto;
                lnkMiembroSel.Text = M;
                pnlMiembroSel.Visible = true;
                F = dtMiembro.Rows[0]["FamilyId"].ToString();
                lblFamiliaSel.Text = "<b>" + dic.familia + ":</b>";
                lnkFamiliaSel.Text = S + F;
                pnlFamiliaSel.Visible = true;
            }
        }
        protected void llenarMenu()
        {
            /* DataTable tblAreas = objBD.obtenerAreasDeUsuario(nombreUsuario,sitioSeleccionado,idiomaSeleccionado);
             DataTable tblFuncion;
             String codigoArea;
             String nombreArea;
             String nombreFuncion;
             foreach(DataRow rowArea in tblAreas.Rows){
                 codigoArea = rowArea["Code"].ToString();
                 nombreArea = rowArea["Area"].ToString();
                 MenuItem itemArea = new MenuItem();
                 itemArea.Text = nombreArea;
                 tblFuncion = objBD.obtenerFuncionesDeUsuario(nombreUsuario,sitioSeleccionado,idiomaSeleccionado, codigoArea);
                 foreach(DataRow rowFuncion in tblFuncion.Rows){
                     nombreFuncion = rowFuncion["Trans"].ToString();
                     MenuItem itemFuncion = new MenuItem();
                     itemFuncion.Text = nombreFuncion;
                     itemArea.ChildItems.Add(itemFuncion);
                 }
                 mnuPrincipal.Items.Add(itemArea);
                 mnuPrincipal.CssClass = "menu";
             }*/
        }
        public static Boolean regresar = false;
        //MENSAJE DE NOTIFICACIÓN VERDE
        public void mostrarMsjNtf(String msj)
        {
            ContentPlaceHolder1.Visible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "Script",
            "<script type=\"text/javascript\">ShowMessage(\"" +
            msj + "\",\"success\");</script>");
        }
        //MENSAJE DE NOTIFICACIÓN ROJO
        public void mostrarMsjAdvNtf(String msj)
        {
            ContentPlaceHolder1.Visible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "Script",
           "<script type=\"text/javascript\">ShowMessage(\"" +
           msj + "\",\"error\");</script>");
        }
        //MENSAJE CON BORDE AZUL, SIN MODAL Y CON BOTÓN DE "ACEPTAR"
        public void mostrarMsj(String msj)
        {
            btnOk.Text = dic.aceptar;
            regresar = false;
            btnOk.Visible = true;
            ContentPlaceHolder1.Visible = false;
            lblAdvMensaje.Text = dic.mensaje + ":";
            lblMensaje.Text = msj;
            pnlMensaje.BorderColor = colorMsj;
            pnlMensaje.Visible = true;
        }

        //MENSAJE CON BORDE AZUL, SIN MODAL Y SIN BOTÓN DE "ACEPTAR"
        public void mostrarMsjStc(String msj)
        {
            regresar = false;
            btnOk.Visible = false;
            ContentPlaceHolder1.Visible = false;
            lblAdvMensaje.Text = dic.mensaje + ":";
            lblMensaje.Text = msj;
            pnlMensaje.Visible = true;
        }

        //MENSAJE CON BORDE AZUL, SIN MODAL Y CON BOTÓN DE "ACEPTAR" QUE REGRESA A PANTALLA PRINCIPAL "MISC/Buscar.aspx"
        public void mostrarMsjRegresar(String msj)
        {
            regresar = true;
            btnOk.Visible = true;
            ContentPlaceHolder1.Visible = false;
            lblAdvMensaje.Text = dic.mensaje + ":";
            lblMensaje.Text = msj;
            pnlMensaje.Visible = true;
        }

        //MENSAJE CON BORDE AZUL, CON MODAL Y CON BOTÓN DE "ACEPTAR"
        public void mostrarMsjMdl(String msj)
        {
            btnOkMdl.Text = dic.aceptar;
            lblAdvMensajeMdl.Text = dic.advertencia + ":";
            lblMensajeMdl.Text = msj;
            pnlMensajeMdl.BorderColor = colorMsj;
            pnlOpcionesMdl.Visible = false;
            ContentPlaceHolder2.Visible = false;
            pnlFiltroOscuro.Visible = true;
            pnlMensajeMdl.Visible = true;
        }
        //MENSAJE CON BORDE AZUL, CON MODAL Y CON BOTÓN DE "SÍ" Y "NO", EL BOTÓN "SÍ" SE PUEDE ADAPTAR A ALGUNA FUNCIÓN DE PÁGINA ALUMNA
        public void mostrarMsjOpcionesMdl(String msj)
        {
            btnAceptarPnlOpcionesMdl.Text = dic.Si;
            btnCancelarPnlOpcionesMdl.Text = dic.No;
            btnCancelarPnlOpcionesMdl.Visible = true;
            //ContentPlaceHolder1.Visible = false;
            lblAdvOpcionesMdl.Text = dic.advertencia + ":";
            lblOpcionesMdl.Text = msj;
            pnlOpcionesMdl.BorderColor = colorMsj;
            pnlMensajeMdl.Visible = false;
            ContentPlaceHolder2.Visible = false;
            pnlFiltroOscuro.Visible = true;
            pnlOpcionesMdl.Visible = true;
        }
        //MENSAJE CON BORDE AZUL, CON MODAL Y CON BOTÓN DE "ACEPTAR", EL BOTÓN "ACEPTAR" SE PUEDE ADAPTAR A ALGUNA FUNCIÓN DE PÁGINA ALUMNA
        public void mostrarMsjOpcionMdl(String msj)
        {
            btnAceptarPnlOpcionesMdl.Text = dic.aceptar;
            btnCancelarPnlOpcionesMdl.Visible = false;
            //ContentPlaceHolder1.Visible = false;
            lblAdvOpcionesMdl.Text = dic.advertencia + ":";
            lblOpcionesMdl.Text = msj;
            pnlOpcionesMdl.BorderColor = colorMsjAdv;
            pnlMensajeMdl.Visible = false;
            ContentPlaceHolder2.Visible = false;
            pnlFiltroOscuro.Visible = true;
            pnlOpcionesMdl.Visible = true;
        }
        public void mostrarModalYContenido(Panel pnl)
        {
            ContentPlaceHolder2.Visible = true;
            pnlOpcionesMdl.Visible = false;
            pnlFiltroOscuro.Visible = true;
            pnl.Visible = true;
        }

        public void ocultarModalYContenido(Panel pnl)
        {
            pnlFiltroOscuro.Visible = false;
            pnl.Visible = false;
        }

        public void mostrarFormularioSeleccionarMiembro()
        {
            ContentPlaceHolder1.Visible = false;
            lblSelecMiembro.Text = dic.idMiembro + ":";
            btnSeleccionarMiembro.Text = dic.seleccionar;
            txbNumeroMiembro.Focus();
            pnlSelecMiembro.Visible = true;
        }


        public void mostrarFormularioSeleccionarFamilia()
        {
            ContentPlaceHolder1.Visible = false;
            lblSelecFamilia.Text = dic.idFamilia + ":";
            if (S.Equals("R"))
            {
                lblSelecFaro.Text = dic.idFaro + ":";
                lblSelecFaro.Visible = true;
                txbNumeroFaro.Visible = true;
            }
            btnSeleccionarFamilia.Text = dic.seleccionar;
            txbNumeroFamilia.Focus();
            pnlSelecFamilia.Visible = true;
        }

        public void mostrarFormularioSeleccionarPadrino()
        {
            ContentPlaceHolder1.Visible = false;
            lblSelecPadrino.Text = "Padrino" + ":";
            btnSeleccionarPadrino.Text = dic.seleccionar;
            txbNumeroPadrino.Focus();
            pnlSelectPadrino.Visible = true;
        }
        //PROCEDIMIENTO DE EVENTO DE BOTÓN DE "OK" DE "pnlMensaje"
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (regresar)
            {
                this.redir("~/MISC/Buscar.aspx");
            }
            else
            {
                pnlMensaje.Visible = false;
                lblMensaje.Text = "";
                ContentPlaceHolder1.Visible = true;
                regresar = false;
                if (auxFamilia)
                {
                    ContentPlaceHolder1.Visible = false;
                    pnlSelecFamilia.Visible = true;
                    txbNumeroFamilia.Focus();
                    auxFamilia = false;
                }
                if (auxMiembro)
                {
                    ContentPlaceHolder1.Visible = false;
                    pnlSelecMiembro.Visible = true;
                    txbNumeroMiembro.Focus();
                    auxMiembro = false;
                }
            }
        }
        //PROCEDIMIENTO DE EVENTO DE BOTÓN DE "OK" DE "pnlMensajeMdl"
        protected void btnOkMdl_Click(object sender, EventArgs e)
        {
            if (regresar)
            {
                this.redir("~/MISC/Buscar.aspx");
            }
            else
            {
                ocultarModalYContenido(pnlMensajeMdl);
                lblMensajeMdl.Text = "";
                ContentPlaceHolder1.Visible = true;
                regresar = false;
                if (auxFamilia)
                {
                    ContentPlaceHolder1.Visible = false;
                    pnlSelecFamilia.Visible = true;
                    txbNumeroFamilia.Focus();
                    auxFamilia = false;
                }
                if (auxMiembro)
                {
                    ContentPlaceHolder1.Visible = false;
                    pnlSelecMiembro.Visible = true;
                    txbNumeroMiembro.Focus();
                    auxMiembro = false;
                }
            }
        }
        protected void btnAceptarPnlOpcionesMdl_Click(object sender, EventArgs e)
        {
            if (contentCallEvent != null)
                contentCallEvent(this, EventArgs.Empty);
            ocultarModalYContenido(pnlOpcionesMdl);
            lblOpcionesMdl.Text = "";
            //ContentPlaceHolder1.Visible = true;
        }
        protected void btnCancelarPnlOpcionesMdl_Click(object sender, EventArgs e)
        {
            ocultarModalYContenido(pnlOpcionesMdl);
            lblOpcionesMdl.Text = "";
            ContentPlaceHolder1.Visible = true;
        }
        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlFiltroOscuro.Visible = false;
            pnlOpcionesMdl.Visible = false;
        }

        protected void lnkRepetir_Click(object sender, EventArgs e)
        {
            Session["F"] = null;
            Session["M"] = null;
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            redir(paginaActual);
        }

        protected void btnSeleccionarMiembro_Click(object sender, EventArgs e)
        {
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            String miembro = txbNumeroMiembro.Text;
            if (new BDMiembro().obtenerDatos(S, miembro, L).Rows.Count > 0)
            {
                seleccionarMiembro(miembro);
                redir(paginaActual);
            }
            else
            {
                //pnlSelecMiembro.Visible = false;
                auxMiembro = true;
                mostrarMsjAdvNtf(dic.msjNoEncontroMiembro);
                ContentPlaceHolder1.Visible = false;
            }
        }

        protected void btnSeleccionarFamilia_Click(object sender, EventArgs e)
        {
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            String familia = txbNumeroFamilia.Text;
            String faro = txbNumeroFaro.Text;
            if (!String.IsNullOrEmpty(familia) && String.IsNullOrEmpty(faro))
            {
                if (new BDFamilia().obtenerDatos(S, familia, L).Rows.Count > 0)
                {
                    seleccionarFamilia(familia);
                    redir(paginaActual);
                }
                else
                {
                    //pnlSelecFamilia.Visible = false;
                    auxFamilia = true;
                    mostrarMsjAdvNtf(dic.msjNoEncontroFamilia);
                    ContentPlaceHolder1.Visible = false;
                }
            }
            else if (String.IsNullOrEmpty(familia) && !String.IsNullOrEmpty(faro))
            {
                familia = (new BDFamilia().obtenerIdAPartirDeFaro(S, faro)) + "";
                if (!familia.Equals("0"))
                {
                    seleccionarFamilia(familia);
                    redir(paginaActual);
                }
                else
                {
                    ////pnlSelecFamilia.Visible = false;
                    auxFamilia = true;
                    mostrarMsjAdvNtf(dic.msjNoEncontroFamilia);
                    ContentPlaceHolder1.Visible = false;
                }
            }
            else
            {
                //pnlSelecFamilia.Visible = false;
                auxFamilia = true;
                mostrarMsjAdvNtf(dic.msjDebeingresarUno);
                ContentPlaceHolder1.Visible = false;
            }

        }

        protected void btnSeleccionarPadrino_Click(object sender, EventArgs e)
        {
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            String padrino = txbNumeroPadrino.Text;
            P = padrino;
        }
        protected void lnkMiembroSel_Click(object sender, EventArgs e)
        {
            this.redir("~/MISC/PerfilMiembro.aspx");
        }
        protected void lnkFamiliaSel_Click(object sender, EventArgs e)
        {
            this.redir("~/MISC/PerfilFamilia.aspx");
        }
        public void redir(String URL)
        {
            Response.Redirect(URL);
        }

        public void colocarTitulo()
        {
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            String titulo = s.retornaTitulo(paginaActual, L);
            lblTituloFn.Text = titulo;
            lblTituloFn.Font.Bold = true;
            Page.Title = titulo;
        }

        public void seguridad()
        {
            String paginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            if (s.paginaEsPermitida(U, S, paginaActual))
            {
                int sSel = s.retornaSeguridadSeleccion(paginaActual);

                if ((sSel == (int)Selec.FamAfil) || (sSel == (int)Selec.FamAfilGradDesa) || (sSel == (int)Selec.FamRegi))
                {
                    if (String.IsNullOrEmpty(F))
                    {
                        mostrarFormularioSeleccionarFamilia();
                    }
                    else
                    {
                        DataTable dtFamilia = new BDFamilia().obtenerDatos(S, F);
                        if (dtFamilia.Rows.Count > 0)
                        {
                            String estadoAfil = dtFamilia.Rows[0]["AffiliationStatus"].ToString();
                            if (!estadoAfil.Equals("AFIL"))
                            {
                                vista = true;
                            }
                            pnlRepetir.Visible = true;
                        }
                        else
                        {
                            mostrarFormularioSeleccionarFamilia();
                        }
                       
                    }
                }
                else
                    if ((sSel == (int)Selec.Afil) || (sSel == (int)Selec.AfilApadGrad) || (sSel == (int)Selec.FamRegi) || (sSel == (int)Selec.MiemFamRegi))
                    {
                        if (String.IsNullOrEmpty(M))
                        {
                            mostrarFormularioSeleccionarMiembro();
                        }
                        else
                        {
                            pnlRepetir.Visible = true;
                        }
                    }
                    else
                        if ((sSel == (int)Selec.Padr))
                        {
                            if (String.IsNullOrEmpty(P))
                            {
                                mostrarFormularioSeleccionarPadrino();
                            }
                            else
                            {
                                pnlRepetir.Visible = true;
                            }
                        }
            }
            else
            {
                redir("~/MISC/Buscar.aspx");
            }
        }
    }
}