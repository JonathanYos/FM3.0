using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.Apadrinamiento
{
    public partial class ReporteCartasAPAD : System.Web.UI.Page
    {
        protected BDMiembro BDM;
        protected Diccionario dic;
        protected static BDAPAD APD;
        protected static BDFamilia BDF;
        protected static String S;
        protected static String L;
        protected static String F;
        protected static mast mst;
        protected static String M;
        protected static String U;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            mst = (mast)Master;
            dic = new Diccionario(L, S);

            if (!IsPostBack)
            {
                BDM = new BDMiembro();
                APD = new BDAPAD();
                dic = new Diccionario(L, S);
                try
                {
                    LLenarIngreso();
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void LLenarIngreso()
        {
            pnldatos.Visible = true;
            pnlinfo.Visible = false;
            lbldefecha.Text = "De ";
            lblafecha.Text = " A";
            string sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code IN('F','R','M','N')";
            LLenarCombo(sql, ddlCategoria);
            lblCategoria.Text = dic.sitio;
        }
        private void valoresiniciales()
        {
            LLenarGrids();
            LLenarLabels();
        }
        private void LLenarLabels()
        {
            if (L == "es")
            {
                lblprocgradmemb.Text = "Procesos de Graduación de Miembros";
            }
            else
            {
                lblprocgradmemb.Text = "Member Graduation Solicitudes";
            }
        }
        private void LLenarGrids()
        {
            string sql = "SELECT DescSpanish 'Plan Graduación' ,(SELECT COUNT(*) FROM dbo.MemberDisaffiliationSolicitude WHERE RecordStatus  = ' ' AND Project = '" + S + "'  AND DisaffiliationType = 'GRAP' AND Status = 'ABAN' AND GraduationPlan = L.Code AND StatusDate BETWEEN '2019/11/1' AND  '2019/11/15') 'Abandonada',(SELECT COUNT(*) FROM dbo.MemberDisaffiliationSolicitude WHERE RecordStatus  = ' ' AND Project = '" + S + "'  AND DisaffiliationType = 'GRAP' AND Status = 'COMP' AND GraduationPlan = L.Code AND StatusDate BETWEEN '2019/11/1' AND  '2019/11/15') 'Completa',(SELECT COUNT(*) FROM dbo.MemberDisaffiliationSolicitude WHERE RecordStatus  = ' ' AND Project = '" + S + "'  AND DisaffiliationType = 'GRAP' AND Status = 'PEND' AND GraduationPlan = L.Code AND StatusDate BETWEEN '2019/11/1' AND  '2019/11/15') 'Pendiente' FROM dbo.CdAffiliationGraduationPlan L";
            LLenarGrid(sql, gvgrad);

        }
        private void LLenarGrid(string sql, GridView gv)
        {
            DataTable tabledata = new DataTable();
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            gv.DataSource = tabledata;
            gv.DataBind();

        }

        protected void gvgrad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (L == "es")
                {
                    e.Row.Cells[0].Text = "Plan de Graduación";
                    e.Row.Cells[1].Text = "Abandonada";
                    e.Row.Cells[2].Text = "Completa";
                    e.Row.Cells[3].Text = "Pendiente";
                }
                else
                {
                    e.Row.Cells[0].Text = "Graduation Plan";
                    e.Row.Cells[1].Text = "Abandoned";
                    e.Row.Cells[2].Text = "Completed";
                    e.Row.Cells[3].Text = "Pending";
                }
            }
        }
        private void LLenarCombo(string sql, DropDownList combo)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            combo.DataSource = datos;
            combo.DataValueField = "Code";
            combo.DataTextField = "descripcion";
            combo.DataBind();
            combo.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
    }
}