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
    public partial class PerfilFamilia : System.Web.UI.Page
    {
        protected static BDFamilia BDF;
        protected static Diccionario dic;
        protected static String S;
        protected static String M;
        protected static String L;
        protected static String F;
        protected static mast mst;
        protected void Page_Load(object sender, EventArgs e)
        {

            F = mast.F;
            M = mast.M;
            L = mast.L;
            S = mast.S;
            if (S.Equals("E") || S.Equals("A"))
            {
                gdvAvisos.Visible = false;
                tblAfil.Visible = false;
            }
            if (!IsPostBack)
            {
                mst = (mast)Master;
                try
                {
                    BDF = new BDFamilia();
                    dic = new Diccionario(L, S);
                    agregaInfoGen();
                    agregaPalabras();
                    agregaMiembrosAct();
                    agregaAvisos();
                }
                catch
                {

                }
            }
        }
        private void agregaAvisos()
        {
            DataTable dtAvisos = new BDFamilia().obtenerAvisos(S, F, L, false);
            gdvAvisos.Columns[0].HeaderText = dic.avisos;
            if (dtAvisos.Rows.Count > 0)
            {
                //gdvAvisos.Columns[1].Visible = false;
                gdvAvisos.DataSource = dtAvisos;
                gdvAvisos.DataBind();
            }
            else
            {
                //gdvAvisos.Columns[1].HeaderText = dic.avisos;
                //gdvAvisos.Columns[0].Visible = false;
                DataTable dtAvisosAux = new DataTable();
                dtAvisosAux.Columns.Add("Aviso");
                dtAvisosAux.Rows.Add(dic.noTiene);
                gdvAvisos.DataSource = dtAvisosAux;
                gdvAvisos.DataBind();
            }
        }
        protected void agregaInfoGen()
        {
            DataTable dtFamilia = BDF.obtenerDatos(S, F, L);
            DataRow rowF = dtFamilia.Rows[0];
            lblVDirec.Text = rowF["Address"].ToString();
            lblVPueblo.Text = rowF["Pueblo"].ToString();
            lblVPhone.Text = rowF["Phone"].ToString();
            lblVArea.Text = rowF["Area"].ToString();
            lblVEtnia.Text = rowF["Etnia"].ToString();
            String afilEstado = rowF["AfilEstado"].ToString();
            if (!String.IsNullOrEmpty(afilEstado))
            lblVAfilStatus.Text = afilEstado + " (" + rowF["AfilEstadoDate"].ToString() + ")";
            String clasif = rowF["Classification"].ToString();
            if (!String.IsNullOrEmpty(clasif))
            {
                lblVClasif.Text = "<b>" + clasif + "</b>" + " (" + rowF["ClassifDate"].ToString() + ")";
            }
            lblVTS.Text = rowF["TS"].ToString();
        }
        protected void agregaMiembrosAct()
        {
            gdvMiembrosAct.DataSource = BDF.obtenerActivos(S, F, L);
            gdvMiembrosAct.DataBind();
            gdvMiembrosInact.DataSource = BDF.obtenerInactivos(S, F, L);
            gdvMiembrosInact.DataBind();
        }
        protected void agregaPalabras()
        {
            btnAct.Text = dic.mostrarSoloActivos;
            btnInact.Text = dic.mostrarInactivos;
            lblAfil.Text = dic.afiliacion;
            lblAfilStatus.Text = dic.afilEstado + ":";
            lblArea.Text = dic.area + ":";
            lblClasif.Text = dic.clasificacion + ":";
            lblDirec.Text = dic.direccion + ":";
            lblEtnia.Text = dic.etnia + ":";
            lblGeneralInfo.Text = dic.infoGeneral;
            lblMiembros.Text = dic.miembros;
            lblMiembros2.Text = dic.miembros;
            lblPhone.Text = dic.telefono + ":";
            lblPueblo.Text = dic.pueblo + ":";
            lblTS.Text = dic.trabajadorS + ":";
            gdvMiembrosAct.Columns[1].HeaderText = dic.miembro;
            gdvMiembrosAct.Columns[2].HeaderText = dic.nombre;
            gdvMiembrosAct.Columns[3].HeaderText = dic.relacion;
            gdvMiembrosAct.Columns[4].HeaderText = dic.fechaNacimiento;
            gdvMiembrosAct.Columns[5].HeaderText = dic.afilEstado;
            gdvMiembrosAct.Columns[6].HeaderText = dic.afilTIpo;
            gdvMiembrosAct.Columns[7].HeaderText = dic.otraAfil;
            gdvMiembrosInact.Columns[1].HeaderText = dic.id;
            gdvMiembrosInact.Columns[2].HeaderText = dic.nombre;
            gdvMiembrosInact.Columns[3].HeaderText = dic.relacion;
            gdvMiembrosInact.Columns[4].HeaderText = dic.fechaNacimiento;
            gdvMiembrosInact.Columns[5].HeaderText = dic.afilEstado;
            gdvMiembrosInact.Columns[6].HeaderText = dic.afilTIpo;
            gdvMiembrosInact.Columns[7].HeaderText = dic.inactivoRazon;
            gdvMiembrosInact.Columns[8].HeaderText = dic.inactivoFecha;
        }
        protected void btnInact_Click(object sender, EventArgs e)
        {
            agregaAvisos();
            pnlActiv.Visible = false;
            pnlInac.Visible = true;
        }
        protected void btnAct_Click(object sender, EventArgs e)
        {
            agregaAvisos();
            pnlInac.Visible = false;
            pnlActiv.Visible = true;
        }

        protected void gdvMiembrosAct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                LinkButton link = (LinkButton)gdvMiembrosAct.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnMName");
                M = link.Text;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
        }

        protected void gdvMiembrosInact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                LinkButton link = (LinkButton)gdvMiembrosInact.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnMName");
                M = link.Text;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
        }
    }
}