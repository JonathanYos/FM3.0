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
    public partial class RegalosSelAPAD : System.Web.UI.Page
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


        /////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void comprobar_registro(string fecha, string miembro)
        {
            string sql = "INSERT INTO dbo.MemberGift SELECT	Project, MemberId, Category, SelectionDateTime,GETDATE() CreationDateTime,' ' RecordStatus,'" + U + "' UserId,ExpirationDateTime,Type,Notes,GETDATE() DeliveryDateTime,SizeGift FROM DBO.MemberGift  WHERE CONVERT(VARCHAR,SelectionDateTime,120)='" + fecha + "'  AND MemberId='" + miembro + "'";
            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                dic = new Diccionario(L, S);
                con.Open();
                cmd.ExecuteNonQuery();
                filtrargrid();
                mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            finally
            {

                con.Close();
            }
        }
        private void filtrargrid()
        {
            string categoria, tipo;
            if (ddlcat.SelectedIndex == 0) { categoria = "Category"; } else { categoria = "'" + ddlcat.SelectedValue + "'"; }
            if (ddltipo.SelectedIndex == 0) { tipo = "Type"; } else { tipo = "'" + ddltipo.SelectedValue + "'"; }

            string fromdate = txtfechade.Text + " 00:00:00.000";
            string todate = txtfechaa.Text + " 23:59:59.999";
            DataTable tr = new DataTable();
            string sql = "select G.Project Sitio, G.MemberId as Miembro, M.FirstNames + ' ' +M.LastNames as Nombre,  convert(varchar, G.SelectionDateTime, 120) as 'Selección', CGC.DescSpanish as Categoria,CGT.DescSpanish as Tipo, G.Notes as Notas, dbo.fn_GEN_CalcularEdad(M.BirthDate) as Edad, convert(varchar, M.BirthDate, 101) as 'Cumpleaños'  from MemberGift G left join Member M on G.MemberId=M.MemberId and G.Project=M.Project and G.RecordStatus = M.RecordStatus left join CdGiftCategory CGC on G.Category = CGC.Code and G.Project=CGC.Project left join CdGiftType CGT on G.Type = CGT.Code and G.Project=CGT.Project WHERE  SelectionDateTime Between '" + fromdate + "' and '" + todate + "' AND G.RecordStatus like ' ' and  DeliveryDateTime is null AND G.Category=" + categoria + " AND G.Type=" + tipo + " ORDER by SelectionDateTime asc";
            SqlCommand comando = new SqlCommand(sql, con);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            adaptador.Fill(tr);
            con.Close();
            gvhistorial.DataSource = tr;
            gvhistorial.DataBind();

            string sql2 = "select COUNT(*) conteo  from MemberGift G left join Member M on G.MemberId=M.MemberId and G.Project=M.Project and G.RecordStatus = M.RecordStatus left join CdGiftCategory CGC on G.Category = CGC.Code and G.Project=CGC.Project left join CdGiftType CGT on G.Type = CGT.Code and G.Project=CGT.Project WHERE  SelectionDateTime Between '" + fromdate + "' and '" + todate + "' AND G.RecordStatus like ' ' and DeliveryDateTime is null AND G.Category=" + categoria + " AND G.Type=" + tipo + "";
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;

            try
            {
                con.Open();
                daUser = new SqlDataAdapter(sql2, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row["conteo"]);
                lblVconteo.Text = "Total= " + temp;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            gvhistorial.Visible = true;
            rango2.Visible = false;
        }
        private void LlenarCombos()
        {
            string sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdGiftCategory WHERE Active='1' AND Project='" + S + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcat.DataSource = datos;
            ddlcat.DataValueField = "Code";
            ddlcat.DataTextField = "descripcion";
            ddlcat.DataBind();
            ddlcat.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            string sql2 = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdGiftType WHERE Active='1' AND Project='" + S + "'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddltipo.DataSource = datos2;
            ddltipo.DataValueField = "Code";
            ddltipo.DataTextField = "descripcion";
            ddltipo.DataBind();
            ddltipo.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            //SQL = "SELECT Code, CASE WHEN '" + Ls + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            //SqlDataAdapter adapter2 = new SqlDataAdapter(SQL, con);
            //DataTable datos2 = new DataTable();
            //adapter2.Fill(datos2);
            //ddlsitio.DataSource = datos2;
            //ddlsitio.DataValueField = "Code";
            //ddlsitio.DataTextField = "descripcion";
            //ddlsitio.DataBind();
            //ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        private void LlenarLabelsyOtros()
        {
            revtxtfecha.ErrorMessage = dic.msjFechaVaciaAPAD;
            revtxtfechaa.ErrorMessage = dic.msjFechaVaciaAPAD;
            lblcat.Text = dic.categoriaAPAD;
            lbltipo.Text = dic.TipoAPAD;
            rango2.Visible = true;
            lblafecha.Text = dic.AFechaAPAD;
            lbldefecha.Text = dic.DeFechaAPAD;
            historial.Visible = false;
            btnaceptarf.Text = dic.AceptarAPAD;
            btncancelarf.Text = dic.CancelarAPAD;
            btnOtraB.Text = dic.OtraBusquedaAPAD;
        }
        private void vaciarcampos()
        {
            filtrargrid();
        }
        private void valoresiniciales()
        {
            LlenarLabelsyOtros();
            LlenarCombos();
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////

        protected void btnaceptarf_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, S);
            if (string.IsNullOrEmpty(txtfechaa.Text) || string.IsNullOrEmpty(txtfechade.Text))
            {
                mst.mostrarMsjAdvNtf(dic.msjFechaVaciaAPAD);
            }
            else
            {
                DateTime date2 = Convert.ToDateTime(this.txtfechaa.Text);
                DateTime date1 = Convert.ToDateTime(this.txtfechade.Text);
                if (date1 > date2)
                {
                    mst.mostrarMsjAdvNtf(dic.FechaMayorAPAD);
                }
                else
                {

                    filtrargrid();
                    historial.Visible = true;
                }
            }
        }
        protected void btncancelar_Click(object sender, EventArgs e)
        {
            vaciarcampos();
        }
        protected void btnOtraB_Click(object sender, EventArgs e)
        {
            historial.Visible = false;
            rango2.Visible = true;
            txtfechaa.Text = "";
            txtfechade.Text = "";
            ddlcat.SelectedIndex = 0;
            ddltipo.SelectedIndex = 0;
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            string fecha = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;

            comprobar_registro(fecha, miembro);
        }
        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            dic = new Diccionario(L, S);
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.miembro;
                e.Row.Cells[2].Text = dic.nombre;
                e.Row.Cells[3].Text = dic.fechaSeleccionAPAD;
                e.Row.Cells[4].Text = dic.categoriaAPAD;
                e.Row.Cells[5].Text = dic.TipoAPAD;
                e.Row.Cells[6].Text = dic.notasAPAD;
                e.Row.Cells[7].Text = dic.edad;
                e.Row.Cells[8].Text = dic.fechaNacimiento;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            dic = new Diccionario(L, S);
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            mst = (mast)Master;
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            if (!IsPostBack)
            {
                try
                {
                    dic = new Diccionario(L, S);
                    valoresiniciales();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}