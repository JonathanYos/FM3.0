using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.MISC
{
    public partial class BusquedaMiembroInfoEduc : System.Web.UI.Page
    {
        static protected BDGEN bdGEN;
        static Diccionario dic;
        static String U;
        static String L;
        public static String S;
        static mast mst;
        public static Color colorBueno = Color.MediumSeaGreen;
        public static Color colorRegular = Color.Yellow;
        public static Color colorMalo = Color.Crimson;
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
            if (!IsPostBack)
            {
                U = mast.U;
                L = mast.L;
                S = mast.S;
                if (S.Equals("E") || S.Equals("A"))
                {
                    mst.redir("MISC/Buscar.aspx");
                }
                bdGEN = new BDGEN();
                dic = new Diccionario(L, S);
                colocarNombres();
            }
        }
        protected void colocarNombres()
        {
            refTxbAño.ErrorMessage = dic.msjParametroNecesario;
            lblAño.Text = "*" + dic.año + ":";
            lblApad.Text = "&nbsp;" + dic.apadrinados;
            lblCarrera.Text = "&nbsp;" + dic.carrera + ":";
            lblCentroEduc.Text = "&nbsp;" + dic.centroEducativo + ":";
            lblDesaf.Text = "&nbsp;" + dic.desafiliados;
            lblEstadoEduc.Text = "&nbsp;" + dic.estadoEducativo + ":";
            lblExcEstadoEduc.Text = "&nbsp;" + dic.excepcionEstadoEducativo + ":";
            lblGrad.Text = "&nbsp;" + dic.graduados;
            lblGrado.Text = "&nbsp;" + dic.grado + ":";
            lblMaestro.Text = "&nbsp;" + dic.maestro + ":";
            lblNivelEduc.Text = "&nbsp;" + dic.nivelEduc + ":";
            lblPueblo.Text = "&nbsp;" + dic.pueblo + ":";
            lblTipoAfil.Text = "&nbsp;" + dic.afilTIpo + ":";
            lblTipoEscuela.Text = "&nbsp;" + dic.tipoEscuela + ":";
            lnkBuscarFamilias.Text = dic.familias;
            lnkBuscarMiembrosOtraInfo.Text = dic.MiembrosPorOtraInfo;
            lnkBuscarPorNumero.Text = dic.PorId;
            lblBuscar.Text = dic.buscar + ":";
            txbAño.Text = DateTime.Now.Year + "";
            btnNuevaBusqueda.Text = dic.nuevaBusqueda;
            if (L.Equals("es"))
            {
                lblO.Text = "o";
            }
            else
            {
                lblO.Text = "or";
            }
            btnBuscar.Text = dic.buscar;
            llenarCarrerasEduc();
            llenarCentrosEduc();
            llenarEstadosEduc();
            llenarExcepcionesEstadoEduc();
            llenarGrado();
            llenarNivelEduc();
            llenarMaestros();
            llenarTiposAfil();
            llenarTiposEscuela();
            llenarPueblo();
        }
        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            pnlMostrar.Visible = false;
            pnlBusquedaMiembrosInfoEduc.Visible = true;
        }
        protected void llenarCarrerasEduc()
        {
            ddlCarrera.Items.Clear();
            ddlCarrera.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerCarrerasEduc(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlCarrera.Items.Add(item);
            }
        }
        protected void llenarExcepcionesEstadoEduc()
        {
            ddlExcEstdoEduc.Items.Clear();
            ddlExcEstdoEduc.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerExcepcionesEstadoEduc(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlExcEstdoEduc.Items.Add(item);
            }
        }
        protected void llenarGrado()
        {
            ddlGrado.Items.Clear();
            ddlGrado.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerGradosApad(L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlGrado.Items.Add(item);
            }
        }
        protected void llenarNivelEduc()
        {
            ddlNivelEduc.Items.Clear();
            ddlNivelEduc.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerNivelEduc(L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlNivelEduc.Items.Add(item);
            }
        }
        protected void llenarPueblo()
        {
            ddlPueblo.Items.Clear();
            ddlPueblo.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerPueblos(S, L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlPueblo.Items.Add(item);
            }
        }

        protected void llenarTiposAfil()
        {
            ddlTipoAfil.Items.Clear();
            ddlTipoAfil.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTiposAfil(L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlTipoAfil.Items.Add(item);
            }
        }
        protected void llenarTiposEscuela()
        {
            ddlTipoEscuela.Items.Clear();
            ddlTipoEscuela.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTiposEscuela(L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlTipoEscuela.Items.Add(item);
            }
        }
        protected void llenarMaestros()
        {
            ddlMaestro.Items.Clear();
            ddlMaestro.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerMaestros(S);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["EmployeeId"].ToString();
                Tipo = row["EmployeeId"].ToString();
                item = new ListItem(Tipo, Code);
                ddlMaestro.Items.Add(item);
            }
        }
        protected void llenarCentrosEduc()
        {
            ddlCentroEduc.Items.Clear();
            ddlCentroEduc.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerCentrosEduc(S);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Name"].ToString();
                item = new ListItem(Tipo, Code);
                ddlCentroEduc.Items.Add(item);
            }
        }
        protected void llenarEstadosEduc()
        {
            ddlEstadoEduc.Items.Clear();
            ddlEstadoEduc.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerEstadosEducativosBusquedas(L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlEstadoEduc.Items.Add(item);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            String año = txbAño.Text;
            int intAño = Int32.Parse(año);
            if ((intAño >= 2002) && (intAño <= DateTime.Now.Year))
            {
                String filCarrera = ddlCarrera.SelectedValue;
                String filCentroEduc = ddlCentroEduc.SelectedValue;
                String filEstadoEduc = ddlEstadoEduc.SelectedValue;
                String filExcEstadoEduc = ddlExcEstdoEduc.SelectedValue;
                String filGrado = ddlGrado.SelectedValue;
                String filMaestro = ddlMaestro.SelectedValue;
                String filNivelEduc = ddlNivelEduc.SelectedValue;
                String filPueblo = ddlPueblo.SelectedValue;
                String filTipoAfil = ddlTipoAfil.SelectedValue;
                String filTipoEscuela = ddlTipoEscuela.SelectedValue;
                Boolean apad = chkApad.Checked;
                Boolean grad = chkGrad.Checked;
                Boolean desaf = chkGrad.Checked;
                DataTable dtMiembros = bdGEN.obtenerMiembrosInfoEduc(S, L, año, filCarrera, filCentroEduc, filEstadoEduc, filExcEstadoEduc, filGrado, filMaestro, filNivelEduc, filPueblo, filTipoAfil, filTipoEscuela, apad, grad, desaf);
                if (dtMiembros.Rows.Count != 0)
                {
                    try
                    {
                        llenarGdvMiembrosInfoEduc(dtMiembros);
                        pnlBusquedaMiembrosInfoEduc.Visible = false;
                        lblTotal.Text = "Total: " + dtMiembros.Rows.Count;
                        pnlMostrar.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                    }
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroResultados);
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("El Año para Información Educativa, debe ser entre 2002 y el año actual.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("The year for Educational Information, must be between 2002 and the current year.");
                }
            }
        }
        public void llenarGdvMiembrosInfoEduc(DataTable dtMiembros)
        {
            gdvMiembrosInfoEduc.Visible = true;
            gdvMiembrosInfoEduc.Columns[0].Visible = true;
            gdvMiembrosInfoEduc.Columns[1].Visible = true;
            gdvMiembrosInfoEduc.Columns[2].HeaderText = dic.miembro;
            gdvMiembrosInfoEduc.Columns[3].HeaderText = dic.familia;
            gdvMiembrosInfoEduc.Columns[4].HeaderText = dic.nombres;
            gdvMiembrosInfoEduc.Columns[5].HeaderText = dic.apellidos;
            gdvMiembrosInfoEduc.Columns[6].HeaderText = dic.nombreUsual;
            gdvMiembrosInfoEduc.Columns[7].HeaderText = dic.fechaNacimiento;
            gdvMiembrosInfoEduc.Columns[8].HeaderText = dic.genero;
            gdvMiembrosInfoEduc.Columns[9].HeaderText = dic.tipoMiembro;
            gdvMiembrosInfoEduc.Columns[10].HeaderText = dic.semaforo;
            gdvMiembrosInfoEduc.Columns[11].HeaderText = dic.clasificacion;
            gdvMiembrosInfoEduc.Columns[12].HeaderText = dic.area;
            gdvMiembrosInfoEduc.Columns[13].HeaderText = dic.pueblo;
            gdvMiembrosInfoEduc.Columns[14].HeaderText = dic.trabajadorS;
            gdvMiembrosInfoEduc.Columns[15].HeaderText = dic.direccion;
            gdvMiembrosInfoEduc.Columns[16].HeaderText = dic.region;
            gdvMiembrosInfoEduc.Columns[17].HeaderText = dic.afilEstadoFecha;
            gdvMiembrosInfoEduc.Columns[18].HeaderText = dic.año;
            gdvMiembrosInfoEduc.Columns[19].HeaderText = dic.grado;
            gdvMiembrosInfoEduc.Columns[20].HeaderText = dic.estadoEducativo;
            gdvMiembrosInfoEduc.Columns[21].HeaderText = dic.nivelEduc;
            gdvMiembrosInfoEduc.Columns[22].HeaderText = dic.centroEducativo;
            gdvMiembrosInfoEduc.Columns[23].HeaderText = dic.carrera;
            gdvMiembrosInfoEduc.Columns[24].HeaderText = dic.excepcionEstadoEducativo;
            gdvMiembrosInfoEduc.DataSource = dtMiembros;
            gdvMiembrosInfoEduc.DataBind();
            gdvMiembrosInfoEduc.Columns[0].Visible = false;
            gdvMiembrosInfoEduc.Columns[1].Visible = false;
        }

        protected void gdvMiembrosInfoEduc_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMemberId")
            {
                gdvMiembrosInfoEduc.Columns[0].Visible = true;
                String M = gdvMiembrosInfoEduc.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvMiembrosInfoEduc.Columns[0].Visible = false;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
            if (e.CommandName == "cmdFamilyId")
            {
                gdvMiembrosInfoEduc.Columns[1].Visible = true;
                String F = gdvMiembrosInfoEduc.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                gdvMiembrosInfoEduc.Columns[1].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }
    }
}