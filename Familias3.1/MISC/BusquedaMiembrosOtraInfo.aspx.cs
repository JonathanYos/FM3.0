using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.MISC
{
    public partial class BusquedaMiembrosOtraInfo : System.Web.UI.Page
    {
        static protected BDGEN bdGEN;
        static Diccionario dic;
        static String U;
        static String L;
        public static String S;
        static mast mst;
        static String nombres;
        static String apellidos;
        static String dia;
        static String mes;
        static String año;
        static String añoEduc;
        static String nombreUsual;
        static String area;
        static String TS;
        static String tipoAfil;
        public static Color colorBueno = Color.MediumSeaGreen;
        public static Color colorRegular = Color.Yellow;
        public static Color colorMalo = Color.Crimson;
        static Boolean afil;
        static Boolean apad;
        static Boolean otros;
        static Boolean grad;
        static Boolean gradMiembro;
        static Boolean desaf;
        static Boolean desafMiembro;
        static Boolean incluirInfoEduc;
        static Boolean emp;
        static Boolean famEmp;
        protected static int tipoBusqueda = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                U = mast.U;
                L = mast.L;
                S = mast.S;
                bdGEN = new BDGEN();
                dic = new Diccionario(L, S);
                colocarNombres();
                llenarAreas();
                llenarMeses();
                llenarTS();
                llenarTiposAfil();
                configurarSiEmp();
            }
            mst = (mast)Master;
        }

        protected void configurarSiEmp()
        {
            if (S.Equals("E") || S.Equals("A"))
            {
                lblEmp.Text = "&nbsp;&nbsp;" + dic.empleados;
                lblFamEmp.Text = "&nbsp;&nbsp;" + dic.otros;
                tblFiltrosAfil.Visible = false;
                tblFiltrosEmp.Visible = true;
                lblArea.Visible = false;
                ddlArea.Visible = false;
                lblTS.Visible = false;
                ddlTS.Visible = false;
                lblTipoAfil.Visible = false;
                ddlTipoAfil.Visible = false;
                lnkBuscarMiembrosInfoEduc.Visible = false;
                lnkBuscarFamilias.Visible = false;
                lblComa.Visible = false;
                lblO.Visible = false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                nombres = txbNombres.Text;
                apellidos = txbApell.Text;
                nombreUsual = txbNombreUsual.Text;
                dia = txbDia.Text;
                if (!String.IsNullOrEmpty(dia))
                {
                    dia = Int32.Parse(dia.ToString()) + "";
                }
                mes = ddlMes.SelectedValue;
                if (!String.IsNullOrEmpty(mes))
                {
                    mes = Int32.Parse(ddlMes.SelectedValue) + "";
                }
                año = txbAño.Text;
                if (!String.IsNullOrEmpty(año))
                {
                    año = Int32.Parse(año.ToString()) + "";
                }
                añoEduc = txbAñoEscolar.Text;
                area = ddlArea.SelectedValue;
                TS = ddlTS.SelectedValue;
                tipoAfil = ddlTipoAfil.SelectedValue;
                afil = chkAfil.Checked;
                apad = chkApad.Checked;
                otros = chkOtros.Checked;
                grad = chkGrad.Checked;
                gradMiembro = chkParienG.Checked;
                desaf = chkDesaf.Checked;
                desafMiembro = chkParienD.Checked;
                incluirInfoEduc = rdbIncluirInfoEduc.Checked;
                emp = chkEmp.Checked;
                famEmp = chkFamEmp.Checked;
                gdvMiembrosOtraInfo.Visible = false;
                gdvMiembrosOtraInfoEduc.Visible = false;
                DataTable dtMiembros;
                if (!incluirInfoEduc)
                {
                    if (!S.Equals("E") && !S.Equals("A"))
                    {
                        dtMiembros = bdGEN.obtenerMiembrosOtraInfo(S, L, nombres, apellidos, dia, mes, año, nombreUsual, area, TS, tipoAfil, incluirInfoEduc, añoEduc, apad, afil, otros, desaf, desafMiembro, grad, gradMiembro);
                        llenarGdvMiembrosOtraInfo(dtMiembros);
                        tipoBusqueda = 1;
                    }
                    else
                    {
                        dtMiembros = bdGEN.obtenerMiembrosOtraInfoEmp(S, L, nombres, apellidos, dia, mes, año, nombreUsual, emp, famEmp);
                        llenarGdvMiembrosOtraInfoEmp(dtMiembros);
                        tipoBusqueda = 2;
                    }
                    if (dtMiembros.Rows.Count != 0)
                    {
                        lblTotal.Text = "Total: " + dtMiembros.Rows.Count;
                        pnlBusquedaMiembrosOtraInfo.Visible = false;
                        pnlMostrar.Visible = true;
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf(dic.msjNoEncontroResultados);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(añoEduc))
                    {
                        int intAñoEduc = Int32.Parse(añoEduc);
                        if ((intAñoEduc >= 2002) && (intAñoEduc <= DateTime.Now.Year))
                        {
                            dtMiembros = bdGEN.obtenerMiembrosOtraInfo(S, L, nombres, apellidos, dia, mes, año, nombreUsual, area, TS, tipoAfil, incluirInfoEduc, añoEduc, apad, afil, otros, desaf, desafMiembro, grad, gradMiembro);
                            llenarGdvMiembrosOtraInfoEduc(dtMiembros);
                            tipoBusqueda = 3;
                            if (dtMiembros.Rows.Count != 0)
                            {
                                lblTotal.Text = "Total: " + dtMiembros.Rows.Count;
                                pnlBusquedaMiembrosOtraInfo.Visible = false;
                                pnlMostrar.Visible = true;
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
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("Por favor, ingrese el Año para Información Educativa.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("Please, enter the Year for Educational Information.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void llenarGdvMiembrosOtraInfo(DataTable dtMiembros)
        {
            gdvMiembrosOtraInfo.Visible = true;
            gdvMiembrosOtraInfo.Columns[0].Visible = true;
            gdvMiembrosOtraInfo.Columns[1].Visible = true;
            gdvMiembrosOtraInfo.Columns[2].HeaderText = dic.miembro;
            gdvMiembrosOtraInfo.Columns[3].HeaderText = dic.familia;
            gdvMiembrosOtraInfo.Columns[4].HeaderText = dic.nombres;
            gdvMiembrosOtraInfo.Columns[5].HeaderText = dic.apellidos;
            gdvMiembrosOtraInfo.Columns[6].HeaderText = dic.nombreUsual;
            gdvMiembrosOtraInfo.Columns[7].HeaderText = dic.fechaNacimiento;
            gdvMiembrosOtraInfo.Columns[8].HeaderText = dic.genero;
            gdvMiembrosOtraInfo.Columns[9].HeaderText = dic.afilTIpo;
            gdvMiembrosOtraInfo.Columns[10].HeaderText = dic.tipoMiembro;
            gdvMiembrosOtraInfo.Columns[11].HeaderText = dic.semaforo;
            gdvMiembrosOtraInfo.Columns[12].HeaderText = dic.clasificacion;
            gdvMiembrosOtraInfo.Columns[13].HeaderText = dic.area;
            gdvMiembrosOtraInfo.Columns[14].HeaderText = dic.pueblo;
            gdvMiembrosOtraInfo.Columns[15].HeaderText = dic.trabajadorS;
            gdvMiembrosOtraInfo.Columns[16].HeaderText = dic.direccion;
            gdvMiembrosOtraInfo.Columns[17].HeaderText = dic.region;
            gdvMiembrosOtraInfo.Columns[18].HeaderText = dic.afilEstadoFecha;
            gdvMiembrosOtraInfo.DataSource = dtMiembros;
            gdvMiembrosOtraInfo.DataBind();
            gdvMiembrosOtraInfo.Columns[0].Visible = false;
            gdvMiembrosOtraInfo.Columns[1].Visible = false;
        }

        protected void llenarGdvMiembrosOtraInfoEmp(DataTable dtMiembros)
        {
            gdvMiembrosOtraInfoEmp.Visible = true;
            gdvMiembrosOtraInfoEmp.Columns[0].Visible = true;
            gdvMiembrosOtraInfoEmp.Columns[1].Visible = true;
            gdvMiembrosOtraInfoEmp.Columns[2].HeaderText = dic.miembro;
            gdvMiembrosOtraInfoEmp.Columns[3].HeaderText = dic.familia;
            gdvMiembrosOtraInfoEmp.Columns[4].HeaderText = dic.nombres;
            gdvMiembrosOtraInfoEmp.Columns[5].HeaderText = dic.apellidos;
            gdvMiembrosOtraInfoEmp.Columns[6].HeaderText = dic.nombreUsual;
            gdvMiembrosOtraInfoEmp.Columns[7].HeaderText = dic.fechaNacimiento;
            gdvMiembrosOtraInfoEmp.Columns[8].HeaderText = dic.genero;
            gdvMiembrosOtraInfoEmp.Columns[9].HeaderText = dic.direccion;
            gdvMiembrosOtraInfoEmp.DataSource = dtMiembros;
            gdvMiembrosOtraInfoEmp.DataBind();
            gdvMiembrosOtraInfoEmp.Columns[0].Visible = false;
            gdvMiembrosOtraInfoEmp.Columns[1].Visible = false;
        }

        protected void llenarGdvMiembrosOtraInfoEduc(DataTable dtMiembros)
        {
            gdvMiembrosOtraInfoEduc.Visible = true;
            gdvMiembrosOtraInfoEduc.Columns[0].Visible = true;
            gdvMiembrosOtraInfoEduc.Columns[1].Visible = true;
            gdvMiembrosOtraInfoEduc.Columns[2].HeaderText = dic.miembro;
            gdvMiembrosOtraInfoEduc.Columns[3].HeaderText = dic.familia;
            gdvMiembrosOtraInfoEduc.Columns[4].HeaderText = dic.nombres;
            gdvMiembrosOtraInfoEduc.Columns[5].HeaderText = dic.apellidos;
            gdvMiembrosOtraInfoEduc.Columns[6].HeaderText = dic.nombreUsual;
            gdvMiembrosOtraInfoEduc.Columns[7].HeaderText = dic.fechaNacimiento;
            gdvMiembrosOtraInfoEduc.Columns[8].HeaderText = dic.genero;
            gdvMiembrosOtraInfoEduc.Columns[9].HeaderText = dic.afilTIpo;
            gdvMiembrosOtraInfoEduc.Columns[10].HeaderText = dic.tipoMiembro;
            gdvMiembrosOtraInfoEduc.Columns[11].HeaderText = dic.semaforo;
            gdvMiembrosOtraInfoEduc.Columns[12].HeaderText = dic.clasificacion;
            gdvMiembrosOtraInfoEduc.Columns[13].HeaderText = dic.area;
            gdvMiembrosOtraInfoEduc.Columns[14].HeaderText = dic.pueblo;
            gdvMiembrosOtraInfoEduc.Columns[15].HeaderText = dic.trabajadorS;
            gdvMiembrosOtraInfoEduc.Columns[16].HeaderText = dic.direccion;
            gdvMiembrosOtraInfoEduc.Columns[17].HeaderText = dic.region;
            gdvMiembrosOtraInfoEduc.Columns[18].HeaderText = dic.afilEstadoFecha;
            gdvMiembrosOtraInfoEduc.Columns[19].HeaderText = dic.año;
            gdvMiembrosOtraInfoEduc.Columns[20].HeaderText = dic.grado;
            gdvMiembrosOtraInfoEduc.Columns[21].HeaderText = dic.estadoEducativo;
            gdvMiembrosOtraInfoEduc.Columns[22].HeaderText = dic.nivelEduc;
            gdvMiembrosOtraInfoEduc.Columns[23].HeaderText = dic.centroEducativo;
            gdvMiembrosOtraInfoEduc.Columns[24].HeaderText = dic.carrera;
            gdvMiembrosOtraInfoEduc.Columns[25].HeaderText = dic.excepcionEstadoEducativo;
            gdvMiembrosOtraInfoEduc.DataSource = dtMiembros;
            gdvMiembrosOtraInfoEduc.DataBind();
            gdvMiembrosOtraInfoEduc.Columns[0].Visible = false;
            gdvMiembrosOtraInfoEduc.Columns[1].Visible = false;
        }
        protected void colocarNombres()
        {
            txbNombres.MaxLength = 40;
            txbApell.MaxLength = 40;
            txbNombreUsual.MaxLength = 40;
            lblApad.Text = dic.apadrinados;
            lblApell.Text = dic.apellidos + ":";
            lblArea.Text = dic.area + ":";
            lblDesaf.Text = dic.desafiliados;
            lblGrad.Text = dic.graduados;
            lblNacim.Text = dic.nacimiento + " " + dic.msjFormatoFecha + ":";
            lblNombres.Text = dic.nombres + ":";
            lblNombreUsual.Text = dic.nombreUsual + ":";
            lblOtros.Text = dic.otros;
            lblAfil.Text = dic.afiliados;
            lblParienD.Text = dic.miembrosFamDes;
            lblParienG.Text = dic.miembrosFamGrad;
            lblTipoAfil.Text = dic.afilTIpo + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lnkBuscarFamilias.Text = dic.familias;
            lnkBuscarMiembrosInfoEduc.Text = dic.MiembrosPorEducInfo;
            lnkBuscarPorNumero.Text = dic.PorId;
            lblBuscar.Text = dic.buscar + ":";
            lblIncluirInfoEduc.Text = "&nbsp;" + dic.incluirInfoEduc;
            lblAñoEcolar.Text = dic.añoEducativo + ":";
            if (L.Equals("es"))
            {
                lblO.Text = "o";
            }
            else
            {
                lblO.Text = "or";
            }
            btnBuscar.Text = dic.buscar;
            btnCopiar.Text = dic.copiar;
            btnNuevaBusqueda.Text = dic.nuevaBusqueda;
            lblBuscar.Text = dic.buscar + ":";
            if (L.Equals("es"))
            {
                lblO.Text = "o";
            }
            else
            {
                lblO.Text = "or";
            }
        }
        protected void llenarAreas()
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerAreas(S, L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlArea.Items.Add(item);
            }
        }

        protected void llenarMeses()
        {
            ddlMes.Items.Clear();
            ddlMes.Items.Add(new ListItem("", ""));
            DataTable tblMeses = new DataTable();
            tblMeses.Columns.Add("Code", typeof(String));
            tblMeses.Columns.Add("Month", typeof(String));
            if (L.Equals("es"))
            {
                tblMeses.Rows.Add("1", "ene");
                tblMeses.Rows.Add("2", "feb");
                tblMeses.Rows.Add("3", "mar");
                tblMeses.Rows.Add("4", "abr");
                tblMeses.Rows.Add("5", "may");
                tblMeses.Rows.Add("6", "jun");
                tblMeses.Rows.Add("7", "jul");
                tblMeses.Rows.Add("8", "ago");
                tblMeses.Rows.Add("9", "sep");
                tblMeses.Rows.Add("10", "oct");
                tblMeses.Rows.Add("11", "nov");
                tblMeses.Rows.Add("12", "dic");
            }
            else
            {
                tblMeses.Rows.Add("1", "jan");
                tblMeses.Rows.Add("2", "feb");
                tblMeses.Rows.Add("3", "mar");
                tblMeses.Rows.Add("4", "apr");
                tblMeses.Rows.Add("5", "may");
                tblMeses.Rows.Add("6", "jun");
                tblMeses.Rows.Add("7", "jul");
                tblMeses.Rows.Add("8", "agu");
                tblMeses.Rows.Add("9", "sep");
                tblMeses.Rows.Add("10", "oct");
                tblMeses.Rows.Add("11", "nov");
                tblMeses.Rows.Add("12", "dec");
            }
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblMeses.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Month"].ToString();
                item = new ListItem(Des, Code);
                ddlMes.Items.Add(item);
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

        protected void llenarTS()
        {
            ddlTS.Items.Clear();
            ddlTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS(S);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["EmployeeId"].ToString();
                Tipo = row["EmployeeId"].ToString();
                item = new ListItem(Tipo, Code);
                ddlTS.Items.Add(item);
            }
        }

        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            pnlMostrar.Visible = false;
            pnlBusquedaMiembrosOtraInfo.Visible = true;
        }
        protected void gdvMiembrosOtraInfo_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMemberId")
            {
                gdvMiembrosOtraInfo.Columns[0].Visible = true;
                String M = gdvMiembrosOtraInfo.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvMiembrosOtraInfo.Columns[0].Visible = false;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
            if (e.CommandName == "cmdFamilyId")
            {
                gdvMiembrosOtraInfo.Columns[1].Visible = true;
                String F = gdvMiembrosOtraInfo.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                gdvMiembrosOtraInfo.Columns[1].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }

        protected void gdvMiembrosOtraInfoEmp_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMemberId")
            {
                gdvMiembrosOtraInfoEmp.Columns[0].Visible = true;
                String M = gdvMiembrosOtraInfoEmp.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvMiembrosOtraInfoEmp.Columns[0].Visible = false;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
            if (e.CommandName == "cmdFamilyId")
            {
                gdvMiembrosOtraInfoEmp.Columns[1].Visible = true;
                String F = gdvMiembrosOtraInfoEmp.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                gdvMiembrosOtraInfoEmp.Columns[1].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }

        protected void gdvMiembrosOtraInfoEduc_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMemberId")
            {
                gdvMiembrosOtraInfoEduc.Columns[0].Visible = true;
                String M = gdvMiembrosOtraInfoEduc.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvMiembrosOtraInfoEduc.Columns[0].Visible = false;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
            if (e.CommandName == "cmdFamilyId")
            {
                gdvMiembrosOtraInfoEduc.Columns[1].Visible = true;
                String F = gdvMiembrosOtraInfoEduc.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                gdvMiembrosOtraInfoEduc.Columns[1].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }

        protected void btnCopiar_Click(object sender, EventArgs e)
        {
            String nombreGdv = "";
            String nombreContentPlace = "ContentPlaceHolder1_";
            if (tipoBusqueda == 1)
            {
                nombreGdv = nombreContentPlace + "gdvMiembrosOtraInfo";
            }
            else if (tipoBusqueda == 2)
            {
                nombreGdv = nombreContentPlace + "gdvMiembrosOtraInfoEmp";
            }
            else if (tipoBusqueda == 3)
            {
                nombreGdv = nombreContentPlace + "gdvMiembrosOtraInfoEduc";
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "Script",
           "<script type=\"text/javascript\">selectElementContents(\"" +
           nombreGdv + "\",\"error\");</script>");
        }

    }
}