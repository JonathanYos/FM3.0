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
    public partial class ApadrinadosPendAPAD : System.Web.UI.Page
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
        protected static int eleccion;

        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void accionar(object sender, EventArgs e)
        {
            switch (eleccion)
            {
                case 1:
                    Response.Redirect("ApadrinadosPendAPAD.aspx");
                    break;
            }
        }
        private void llenarcombos()
        {
            string sql = "SELECT Code ,CASE WHEN 'es'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdSponsorShipLevel WHERE Code='PARC' OR Code='NING'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlnivel.DataSource = datos;
            ddlnivel.DataValueField = "Code";
            ddlnivel.DataTextField = "descripcion";
            ddlnivel.DataBind();
            ddlnivel.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlnivel.SelectedIndex = 0;

            sql = "SELECT Code,Pueblo descripcion FROM dbo.CdgeographicPueblo WHERE Active =1 AND Project='" + S + "'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlpueblo.DataSource = datos2;
            ddlpueblo.DataValueField = "Code";
            ddlpueblo.DataTextField = "descripcion";
            ddlpueblo.DataBind();
            ddlpueblo.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlpueblo.SelectedIndex = 0;

            sql = "SELECT Code, CASE WHEN ''='' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdGrade WHERE ValidValue=1 ORDER BY Orden ASC";
            SqlDataAdapter adapter3 = new SqlDataAdapter(sql, con);
            DataTable datos3 = new DataTable();
            adapter3.Fill(datos3);
            ddlgrado.DataSource = datos3;
            ddlgrado.DataValueField = "Code";
            ddlgrado.DataTextField = "descripcion";
            ddlgrado.DataBind();
            ddlgrado.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlgrado.SelectedIndex = 0;
        }
        private void traducir()
        {
            Ingreso.Visible = true;
            historial.Visible = false;
            btnotrab.Text = dic.nuevaBusqueda;
            lbledad.Text = dic.edad;
            lblgrado.Text = dic.grado;
            lblnivel.Text = dic.afilNivel;
            lblpueblo.Text = dic.pueblo;
            btnbuscar.Text = dic.buscar;
        }

        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////


        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtedad.Text) && ddlgrado.SelectedIndex == 0 && ddlnivel.SelectedIndex == 0 && ddlpueblo.SelectedIndex == 0)
            {
                Ingreso.Visible = false;
                historial.Visible = true;
                string sql = "SELECT MSI.MemberId,M.FirstNames +' '+M.LastNames Nombre, dbo.fn_GEN_edad(M.BirthDate, 'es') Edad, F.Address Vivienda, M.LastFamilyId, dbo.fn_BECA_InfoEducUltimo(MSI.Project,MSI.MemberId) Grado , F.Pueblo, CASE WHEN ''='' THEN L.DescSpanish ELSE L.DescEnglish END Tipo,CASE WHEN ''='' THEN SR.DescSpanish ELSE SR.DescEnglish END Restriccion, CONVERT(VARCHAR,MSI.RestrictionDate,101) Tiempo FROM dbo.MiscMemberSponsorInfo MSI INNER JOIN dbo.Member M ON MSI.MemberId=M.MemberId AND MSI.Project=M.Project AND MSI.RecordStatus=M.RecordStatus LEFT JOIN dbo.CdSponsorShipRestriction SR ON MSI.Restriction = SR.Code LEFT JOIN dbo.CdGrade G ON M.LastGradePassed = G.Code INNER JOIN dbo.Family F ON M.LastFamilyId = F.FamilyId INNER JOIN dbo.CdSponsorShipLevel L ON MSI.SponsorShipLevel = L.Code AND MSI.RecordStatus=F.RecordStatus AND MSI.Project=F.Project WHERE M.RecordStatus = ' ' AND M.Project = '" + S + "' AND M.AffiliationStatus = 'AFIL' AND (MSI.SponsorshipLevel NOT IN ('COMP', 'OLD') AND (MSI.SponsorshipLevel IN ('NING','PARC')))ORDER BY MSI.MemberId ASC";
                DataTable tabledata = new DataTable();
                con.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
                DataSet setDatos = new DataSet();
                adaptador.Fill(setDatos, "listado");
                tabledata = setDatos.Tables["listado"];
                con.Close();
                gvhistorial.DataSource = tabledata;
                gvhistorial.DataBind();
                gvhistorial.Visible = true;
                //    //  LLenarhistorial(sql);
            }
            else
            {
                string edad, pueblo, nivel, grado;

                if (string.IsNullOrEmpty(txtedad.Text))
                {
                    edad = "AND M.BirthDate=M.BirthDate ";
                }
                else
                {
                    edad = "AND dbo.fn_GEN_edad(M.BirthDate, 'es') LIKE('" + txtedad.Text + "%') ";
                }
                if (ddlgrado.SelectedIndex == 0)
                {
                    grado = " ";
                }
                else
                {
                    grado = " AND dbo.fn_BECA_InfoEducUltimo(MSI.Project,MSI.MemberId) LIKE('%" + ddlgrado.SelectedItem.Text + "%') ";
                }
                if (ddlnivel.SelectedIndex == 0)
                {
                    nivel = " AND MSI.SponsorshipLevel=MSI.SponsorshipLevel ";
                }
                else
                {
                    nivel = " AND (MSI.SponsorshipLevel = '" + ddlnivel.SelectedValue + "') ";
                }
                if (ddlpueblo.SelectedIndex == 0)
                {
                    pueblo = "AND F.Pueblo=F.Pueblo";
                }
                else
                {
                    pueblo = "AND F.Pueblo = '" + ddlpueblo.SelectedItem.Text + "' ";
                }
                try
                {

                    string sql = "SELECT MSI.MemberId,M.FirstNames +' '+M.LastNames Nombre,dbo.fn_GEN_edad(M.BirthDate, 'es') Edad, F.Address Vivienda, M.LastFamilyId,dbo.fn_BECA_InfoEducUltimo(MSI.Project,MSI.MemberId) Grado , F.Pueblo, CASE WHEN ''='' THEN L.DescSpanish ELSE L.DescEnglish END Tipo, CASE WHEN ''='' THEN SR.DescSpanish ELSE SR.DescEnglish END Restriccion, CONVERT(VARCHAR,MSI.RestrictionDate,101) Tiempo FROM dbo.MiscMemberSponsorInfo MSI INNER JOIN dbo.Member M ON MSI.MemberId=M.MemberId AND MSI.Project=M.Project AND MSI.RecordStatus=M.RecordStatus LEFT JOIN dbo.CdSponsorShipRestriction SR ON MSI.Restriction = SR.Code LEFT JOIN dbo.CdGrade G ON M.LastGradePassed = G.Code INNER JOIN dbo.Family F ON M.LastFamilyId = F.FamilyId INNER JOIN dbo.CdSponsorShipLevel L ON MSI.SponsorShipLevel = L.Code AND MSI.RecordStatus=F.RecordStatus AND MSI.Project=F.Project WHERE M.RecordStatus = ' ' AND M.Project = '" + S + "' AND M.AffiliationStatus = 'AFIL' AND (MSI.SponsorshipLevel NOT IN ('COMP', 'OLD')) " + nivel + " " + edad + " " + pueblo + " " + grado + " ORDER BY MSI.MemberId ASC";
                    DataTable tabledata = new DataTable();
                    con.Open();
                    SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
                    DataSet setDatos = new DataSet();
                    adaptador.Fill(setDatos, "listado");
                    tabledata = setDatos.Tables["listado"];
                    con.Close();
                    gvhistorial.DataSource = tabledata;
                    gvhistorial.DataBind();
                    gvhistorial.Visible = true;
                    if (tabledata.Rows.Count == 0)
                    {
                        eleccion = 1;
                        mst.mostrarMsjOpcionMdl(dic.msjNoEncontroResultados);
                    }
                    else
                    {
                        Ingreso.Visible = false;
                        historial.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    string sql = "SELECT MSI.MemberId,M.FirstNames +' '+M.LastNames Nombre,dbo.fn_GEN_edad(M.BirthDate, 'es') Edad, F.Address Vivienda, M.LastFamilyId,dbo.fn_BECA_InfoEducUltimo(MSI.Project,MSI.MemberId) Grado , F.Pueblo, CASE WHEN ''='' THEN L.DescSpanish ELSE L.DescEnglish END Tipo, CASE WHEN ''='' THEN SR.DescSpanish ELSE SR.DescEnglish END Restriccion, CONVERT(VARCHAR,MSI.RestrictionDate,101) Tiempo FROM dbo.MiscMemberSponsorInfo MSI INNER JOIN dbo.Member M ON MSI.MemberId=M.MemberId AND MSI.Project=M.Project AND MSI.RecordStatus=M.RecordStatus LEFT JOIN dbo.CdSponsorShipRestriction SR ON MSI.Restriction = SR.Code LEFT JOIN dbo.CdGrade G ON M.LastGradePassed = G.Code INNER JOIN dbo.Family F ON M.LastFamilyId = F.FamilyId INNER JOIN dbo.CdSponsorShipLevel L ON MSI.SponsorShipLevel = L.Code AND MSI.RecordStatus=F.RecordStatus AND MSI.Project=F.Project WHERE M.RecordStatus = ' ' AND M.Project = '" + S + "' AND M.AffiliationStatus = 'AFIL' AND (MSI.SponsorshipLevel NOT IN ('COMP', 'OLD') " + nivel + " " + edad + " " + pueblo + " ORDER BY MSI.MemberId ASC";
                    mst.mostrarMsjAdvNtf(dic.msjNoSeRealizoExcp + ex.Message + sql + ".");
                }
            }
        }
        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.nombre;
                e.Row.Cells[1].Text = dic.edad;
                e.Row.Cells[2].Text = dic.grado;
                e.Row.Cells[3].Text = dic.pueblo;
                e.Row.Cells[4].Text = dic.TipoAPAD.Replace(":", "");
            }
        }

        protected void btnotrab_Click(object sender, EventArgs e)
        {
            Ingreso.Visible = true;
            historial.Visible = false;
            ddlgrado.SelectedIndex = 0;
            ddlnivel.SelectedIndex = 0;
            ddlpueblo.SelectedIndex = 0;
            txtedad.Text = "";
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string mensaje;
            if (L == "es")
            {
                mensaje = "Reporte General en Desarrollo";
            }
            else
            {
                mensaje = "General Report in Development";
            }
            mst.mostrarMsjAdvNtf(mensaje);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(accionar);
            if (!IsPostBack)
            {
                if (!mst.verificarFuncion("BAP"))
                {
                    Ingreso.Visible = false;
                }
                if (!mst.verificarFuncion("CIAP") == false)
                {
                    gvhistorial.Visible = false;
                }
                try
                {

                    traducir();
                    llenarcombos();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}