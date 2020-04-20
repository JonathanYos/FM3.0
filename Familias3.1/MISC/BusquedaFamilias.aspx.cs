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
    public partial class BusquedaFamilias : System.Web.UI.Page
    {
        static protected BDGEN bdGEN;
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
                if (S.Equals("E") || S.Equals("A"))
                {
                    mst.redir("MISC/Buscar.aspx");
                }
                bdGEN = new BDGEN();
                dic = new Diccionario(L, S);
                colocarNombres();
            }
            mst = (mast)Master;
        }
        protected void colocarNombres()
        {
            txbDireccion.MaxLength = 80;
            lblArea.Text = dic.area + ":";
            lblDireccion.Text = dic.direccion + ":";
            lblEstadoAfil.Text = dic.afilEstado + ":";
            lblRegion.Text = dic.region + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lnkBuscarMiembrosInfoEduc.Text = dic.MiembrosPorEducInfo;
            lnkBuscarMiembrosOtraInfo.Text = dic.MiembrosPorOtraInfo;
            lblBuscar.Text = dic.buscar + ":";
            lnkBuscarPorNumero.Text = dic.PorId;
            btnNuevaBusqueda.Text = dic.nuevaBusqueda;
            btnBuscar.Text = dic.buscar;
            llenarEstadosAfil();
            llenarAreas();
            llenarTS();
            llenarRegion();
            if (!S.Equals("F"))
            {
                lblRegion.Visible = false;
                ddlRegion.Visible = false;
            }
        }

        protected void llenarEstadosAfil()
        {
            ddlEstadoAfil.Items.Clear();
            ddlEstadoAfil.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerEstadosAfil(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlEstadoAfil.Items.Add(item);
            }
            ddlEstadoAfil.SelectedValue = "AFIL";
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

        protected void llenarRegion()
        {
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("", ""));
            DataTable tblRegiones;
            tblRegiones = bdGEN.obtenerRegiones();
            String codigo = "";
            String descripcion = "";
            ListItem item;
            foreach (DataRow row in tblRegiones.Rows)
            {
                codigo = row["Code"].ToString();
                descripcion = row["Des"].ToString();
                item = new ListItem(descripcion, codigo);
                ddlRegion.Items.Add(item);
            }
        }
        protected void llenarTS()
        {
            ddlTS.Items.Clear();
            ddlTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS(S);
            String codigo = "";
            String empleado = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["EmployeeId"].ToString();
                empleado = row["EmployeeId"].ToString();
                item = new ListItem(empleado, codigo);
                ddlTS.Items.Add(item);
            }
        }
        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            pnlMostrar.Visible = false;
            pnlBusquedaFamilia.Visible = true;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String TS = ddlTS.SelectedValue;
                String estadoTS = "Activo";
                String estadoAfil = ddlEstadoAfil.SelectedValue;
                String area = ddlArea.SelectedValue;
                String region = ddlRegion.SelectedValue;
                String direccion = txbDireccion.Text;
                DataTable dtFamilias = bdGEN.obtenerFamiliasBusqueda(S, L, TS, estadoTS, estadoAfil, area, region, direccion);
                if (dtFamilias.Rows.Count != 0)
                {
                    llenarGdvFamilias(dtFamilias);
                    pnlBusquedaFamilia.Visible = false;
                    lblTotal.Text = "Total: " + dtFamilias.Rows.Count;
                    pnlMostrar.Visible = true;
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroResultados);
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void llenarGdvFamilias(DataTable dtFamilias)
        {
            gdvFamilias.Columns[0].Visible = true;
            gdvFamilias.Columns[1].HeaderText = dic.familia;
            gdvFamilias.Columns[2].HeaderText = dic.jefeCasa;
            gdvFamilias.Columns[3].HeaderText = dic.direccion;
            gdvFamilias.Columns[4].HeaderText = dic.area;
            gdvFamilias.Columns[5].HeaderText = dic.trabajadorS;
            gdvFamilias.Columns[6].HeaderText = dic.clasificacion;
            gdvFamilias.Columns[7].HeaderText = dic.afilEstado;
            gdvFamilias.DataSource = dtFamilias;
            gdvFamilias.DataBind();
            gdvFamilias.Columns[0].Visible = false;
        }
        protected void gdvFamilias_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdFamilyId")
            {
                gdvFamilias.Columns[1].Visible = true;
                String F = gdvFamilias.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvFamilias.Columns[1].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }
    }
}