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
    public partial class EnviodeCartaAPAD : System.Web.UI.Page
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
            APD = new BDAPAD();
            if (!IsPostBack)
            {
                try
                {
                    APD = new BDAPAD();
                    dic = new Diccionario(L, S);
                    ValoresIniciales();
                }
                catch (Exception ex)
                {
                }
            }

        }
        private void ValoresIniciales()
        {
            LlenarLabelsyOtros();
            LlenarCombos();
        }
        private void LlenarCombos()
        {
            string sql = "SELECT Code, CASE WHEN '" + L + "' ='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdLetterCategory WHERE Active =1";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlCategoria.DataSource = datos;
            ddlCategoria.DataValueField = "Code";
            ddlCategoria.DataTextField = "descripcion";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        private void MostrarHistorial()
        {
            string fechade = txtdefecha.Text + " 00:00:00";
            string fechaa = txbafecha.Text + " 23:59:59";
            string categoria;
            if (ddlCategoria.SelectedIndex == 0) { categoria = "Category"; } else { categoria = "'" + ddlCategoria.SelectedValue + "'"; }
            string sql = "SELECT  MSL.Project, MSL.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre,M.LastFamilyId AS Familia,  convert(char, MSL.DateTimeWritten, 120) 'Escrita', S.SponsorId AS NoPadrino, S.SponsorNames AS NombrePadrino, CASE WHEN '" + L + "' = 'es' THEN CLC.DescSpanish ELSE CLC.DescEnglish END AS Categoria FROM MemberSponsorLetter MSL INNER JOIN Member M ON MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND M.RecordStatus = ' ' AND M.AffiliationStatus = 'AFIL' AND MSL.SponsorOrMember = 'M' AND MSL.DateSent IS NULL INNER JOIN Sponsor S ON S.SponsorId = MSL.SponsorId AND MSL.RecordStatus = S.RecordStatus INNER JOIN CdLetterCategory CLC ON CLC.Code = MSL.Category WHERE convert(char, MSL.DateTimeWritten, 120) BETWEEN '" + fechade + "' AND '" + fechaa + "' AND Category=" + categoria + " ORDER BY DateTimeWritten DESC";
            string sql2 = "SELECT COUNT(*) conteo FROM MemberSponsorLetter MSL INNER JOIN Member M ON MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND M.RecordStatus = ' ' AND M.AffiliationStatus = 'AFIL' AND MSL.SponsorOrMember = 'M' AND MSL.DateSent IS NULL INNER JOIN Sponsor S ON S.SponsorId = MSL.SponsorId AND MSL.RecordStatus = S.RecordStatus INNER JOIN CdLetterCategory CLC ON CLC.Code = MSL.Category WHERE M.Project = '" + S + "' AND convert(char, MSL.DateTimeWritten, 120) BETWEEN '" + fechade + "' AND '" + fechaa + "' AND Category=" + categoria + "";
            int conteo = APD.ObtenerEntero(sql2, "conteo");
            lblconteo.Text = dic.total + " =" + conteo;
            DataTable tabledata;
            con.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            con.Close();
            gvhistorial.DataSource = tabledata;
            gvhistorial.DataBind();
            ingreso.Visible = false;
            historial.Visible = true;
        }
        private void LlenarLabelsyOtros()
        {
            btnOtraB.Text = dic.OtraBusquedaAPAD;
            revtxtfecha.ErrorMessage = dic.msjFechaVaciaAPAD;
            rfvafecha.ErrorMessage = dic.msjFechaVaciaAPAD;
            lblafecha.Text = dic.DeFechaAPAD;
            lbldefecha.Text = dic.AFechaAPAD;
            btnbuscar.Text = dic.buscar;
            ingreso.Visible = true;
            historial.Visible = false;
            lblCategoria.Text = dic.categoriaAPAD;
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            string fecha = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;

            string sql = "INSERT INTO dbo.MemberSponsorLetter SELECT Project,SponsorId,MemberId,SponsorOrMember,DateTimeWritten,GETDATE() CreationDateTime, RecordStatus,'" + U + "' UserId,ExpirationDateTime,GETDATE() DateSent, Category,Notes,Translated, SentToGuatemala FROM dbo.MemberSponsorLetter WHERE RecordStatus = ' ' AND MemberId='" + miembro + "' AND DateSent IS NULL AND convert(char, DateTimeWritten, 120)='" + fecha + "'";
            SqlCommand cmd2 = null;

            cmd2 = new SqlCommand(sql, con);

            try
            {
                con.Open();
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            finally
            {
                con.Close();
            }
            MostrarHistorial();
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, S);
            DateTime fechai = Convert.ToDateTime(txtdefecha.Text);
            DateTime fechaf = Convert.ToDateTime(txbafecha.Text);

            if (fechai > fechaf)
            {
                mst.mostrarMsjAdvNtf(dic.FechaMayorAPAD);
            }
            else
            {
                MostrarHistorial();
            }
        }

        protected void btnOtraB_Click(object sender, EventArgs e)
        {
            ingreso.Visible = true;
            historial.Visible = false;
            txbafecha.Text = "";
            txtdefecha.Text = "";
            ddlCategoria.SelectedIndex = 0;
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.sitio;
                e.Row.Cells[1].Text = dic.miembro;
                e.Row.Cells[2].Text = dic.nombre;
                e.Row.Cells[3].Text = dic.familia;
                e.Row.Cells[4].Text = dic.EscritaAPAD;
                e.Row.Cells[5].Text = dic.NumeroPadrino;
                e.Row.Cells[6].Text = dic.padrinos;
                e.Row.Cells[7].Text = dic.categoriaAPAD;
            }
        }

    }
}