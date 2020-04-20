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

namespace Familias3._1.San_Pablo
{
    public partial class CartaPadrinoAPAD : System.Web.UI.Page
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
        protected static String P;
        protected static int numAccion;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            P = mast.P;
            P = (String)Session["P"];
            mst = (mast)Master;
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            mst.contentCallEvent += new EventHandler(accionar);
            if (string.IsNullOrEmpty(P))
            {
                string notiene;
                if (L == "es")
                {
                    notiene = "No has seleccionado ningún padrino";
                }
                else
                {
                    notiene = "You have not selected any sponsors";
                }
                numAccion = 1;
                mst.mostrarMsjOpcionMdl(notiene);
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    valoresiniciales();
                }
                catch (Exception ex)
                {
                }
            }

        }
        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void accionar(object sender, EventArgs e)
        {
            switch (numAccion)
            {
                case 1:
                    Response.Redirect("BusquedaPadrinosAPAD.aspx");
                    break;
                case 2:
                    Eliminar();
                    break;
            }
        }
        public void ejecutarSQL(String sql)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf("Error: " + ex.Message);
            }
            finally
            {

                con.Close();
            }
        }
        protected void Eliminar()
        {
            string sql = "INSERT INTO dbo.MemberSponsorLetter SELECT Project,SponsorId,MemberId,SponsorOrMember,DateTimeWritten,GETDATE() CreationDateTime,'H' RecordStatus,'" + U + "' UserId,GETDATE() ExpirationDateTime,DateSent,Category, Notes,Translated,SentToGuatemala FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + lblnotas1.Text + "' AND MemberId='" + lblVmiembro.Text + "' AND SponsorOrMember='S'  AND SponsorId='" + P + "' AND CONVERT(VARCHAR,DateTimeWritten,101)='" + lblVfecha.Text + "'";
            ejecutarSQL(sql);
            sql = "UPDATE dbo.MemberSponsorLetter SET RecordStatus='H', ExpirationDateTime=GETDATE() WHERE RecordStatus=' ' AND Project='" + lblnotas1.Text + "' AND MemberId='" + lblVmiembro.Text + "' AND SponsorOrMember='S'  AND SponsorId='" + P + "' AND CONVERT(VARCHAR,DateTimeWritten,101)='" + lblVfecha.Text + "'";
            ejecutarSQL(sql);
            traducir();
            llenarhistorial();
            mst.mostrarMsjNtf(dic.RegistroEliminadoAPAD);
        }
        private void GCarta()
        {
            foreach (GridViewRow gvrow in gvapadrinado.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkentregado");

                if (chk.Checked)
                {
                    try
                    {
                        string sitio = gvrow.Cells[1].Text;
                        string miembro = gvrow.Cells[2].Text;
                        string notas = txbnotas.Text;
                        string sql2 = " SELECT Code FROM dbo.FwCdOrganization WHERE DescSpanish='" + sitio + "' OR DescEnglish='" + sitio + "' ";
                        string sitio2 = obtienePalabra(sql2, "Code");
                        string sql = "INSERT INTO dbo.MemberSponsorLetter VALUES('" + sitio2 + "','" + P + "','" + miembro + "','S',GETDATE(),GETDATE(),' ','" + U + "',NULL, NULL,NULL,'" + notas + "',NULL,NULL)";
                        ejecutarSQL(sql);
                        llenarhistorial();
                        mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf(ex.Message);
                    }
                }
            }
        }
        private void GuardarCarta()
        {
            int a = 0;
            foreach (GridViewRow gvrow in gvapadrinado.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkentregado");

                if (chk.Checked)
                {
                    a -= -1;
                }
            }
            if (a <= 0)
            {
                mst.mostrarMsjAdvNtf(dic.MsjNohaSeleccionadomiembro);
            }
            else
            {
                GCarta();
                traducir();
            }
        }
        private void llenarapadrinado()
        {
            string sql = "SELECT CASE WHEN '" + L + "'='es' THEN O.DescSpanish ELSE O.DescEnglish END AS Sitio, SMR.MemberId,M.FirstNames+' '+M.LastNames 'Nombre' FROM dbo.SponsorMemberRelation SMR INNER JOIN dbo.Member M ON SMR.MemberId=M.MemberId AND SMR.RecordStatus=M.RecordStatus AND SMR.Project=M.Project INNER JOIN dbo.CdSponsorMemberRelationType SMT ON SMR.Type=SMT.Code INNER JOIN dbo.FwCdOrganization O ON O.Code=SMR.Project WHERE SMR.RecordStatus=' ' AND SMR.SponsorId='" + P + "' AND SMR.EndDate IS NULL ";
            DataTable dt = new DataTable();
            LlenarDataTable(sql, dt);
            gvapadrinado.DataSource = dt;
            gvapadrinado.DataBind();
        }
        public DataTable LlenarDataTable(String SQL, DataTable tabledata)
        {
            try
            {
                con.Open();
                SqlCommand comando = new SqlCommand(SQL, con);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(tabledata);
                con.Close();
                return tabledata;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
                DataTable d = new DataTable();
                return d;
            }

        }
        private void llenarhistorial()
        {
            string sql2 = "SELECT SM.Project, SM.MemberId, M.FirstNames+' '+M.LastNames Nombre, SM.StartDate Inicio, SM.EndDate Fin FROM dbo.SponsorMemberRelation SM INNER JOIN dbo.Member M ON M.MemberId=SM.MemberId AND SM.RecordStatus=M.RecordStatus AND M.Project=SM.Project WHERE SM.RecordStatus=' ' AND SponsorId='" + P + "'ORDER BY SM.MemberId";
            DataTable dt2 = new DataTable();
            LlenarDataTable(sql2, dt2);
            gvhisapad.DataSource = dt2;
            gvhisapad.DataBind();


            string sql = "SELECT SM.Project, CONVERT(VARCHAR,SM.DateTimeWritten,101) Enviada,CONVERT(VARCHAR,SM.SentToGuatemala,101) 'Enviada a Guatemala', CONVERT(VARCHAR,SM.Translated,101) Traducida, CONVERT(VARCHAR,SM.DateSent,101) Entregado, M.FirstNames+' '+M.LastNames Nombre, SM.Notes 'Notes' FROM dbo.MemberSponsorLetter SM INNER JOIN dbo.Member M ON M.MemberId=SM.MemberId AND SM.RecordStatus=M.RecordStatus AND M.Project=SM.Project WHERE SM.RecordStatus=' 'AND SM.SponsorId='" + P + "' AND SM.SponsorOrMember='S' ORDER BY SM.DateTimeWritten DESC";
            DataTable dt = new DataTable();
            LlenarDataTable(sql, dt);
            gvhistorial.DataSource = dt;
            gvhistorial.DataBind();
        }
        public int ObtenerEntero(String SQL, String title)
        {


            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;

            try
            {
                con.Open();
                daUser = new SqlDataAdapter(SQL, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row[title]);
                return temp;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
                temp = 5;
                return temp;
            }
        }
        public string obtienePalabra(String sql, String titulo)
        {
            try
            {
                SqlDataAdapter daUser = new SqlDataAdapter();
                DataTableReader adap;
                DataTable tableData = new DataTable();
                string temp = "";


                con.Open();
                daUser = new SqlDataAdapter(sql, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                temp = Convert.ToString(tableData.Rows[0][titulo]);

                return temp;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf("Error" + ex.Message);
                string temp = "||||||";
                return temp;
            }

        }
        private void traducir()
        {
            btnnuevab.Text = dic.nuevaBusqueda;
            lblVNombre.Visible = false;
            lblVfecha.Visible = false;
            lblNombre.Visible = false;
            lblfecha.Visible = false;
            btnaceptar.Text = dic.AceptarAPAD;
            btncancelar.Text = dic.CancelarAPAD;
            lblfecha.Text = dic.FechaAPAD;
            lblNombre.Text = dic.nombre;
            lblpadrino.Text = P;
            lblnotas.Text = dic.notasAPAD;
            gvapadrinado.Visible = true;
            btnmodificar.Text = dic.ModificarAPAD;
            btneliminar.Text = dic.EliminarAPAD;
            btnmodificar.Visible = false;
            btneliminar.Visible = false;
            btnaceptar.Visible = true;
            txbnotas.Text = "";
            foreach (GridViewRow gvrow in gvapadrinado.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkentregado");
                chk.Checked = true;
            }
        }
        private void valoresiniciales()
        {
            traducir();
            llenarapadrinado();
            llenarhistorial();
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            if (btnaceptar.Text == dic.ModificarAPAD)
            {

            }
            else
            {
                GuardarCarta();
            }
        }
        protected void btncancelar_Click1(object sender, EventArgs e)
        {
            llenarhistorial();
            traducir();
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO dbo.MemberSponsorLetter SELECT Project,SponsorId,MemberId,SponsorOrMember,DateTimeWritten,GETDATE() CreationDateTime, RecordStatus,'" + U + "' UserId,ExpirationDateTime,DateSent,Category,'" + txbnotas.Text + "' Notes,Translated,SentToGuatemala FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + lblnotas1.Text + "' AND MemberId='" + lblVmiembro.Text + "' AND SponsorOrMember='S'  AND SponsorId='" + P + "' AND CONVERT(VARCHAR,DateTimeWritten,101)='" + lblVfecha.Text + "'";
            ejecutarSQL(sql);
            llenarhistorial();
            traducir();
            mst.mostrarMsjNtf(dic.RegistroModificadoAPAD);
        }


        protected void btneliminar_Click(object sender, EventArgs e)
        {
            numAccion = 2;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }
        protected void gvapadrinado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.sitio;
                e.Row.Cells[2].Text = dic.miembro;
                e.Row.Cells[3].Text = dic.nombre;
            }
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Text;
            string notas = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text;
            string fecha = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            string sitio = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            string sql = "SELECT MemberId N FROM dbo.Member WHERE FirstNames+' '+LastNames='" + HttpUtility.HtmlDecode(miembro) + "' AND RecordStatus=' ' AND Project='" + sitio + "'";
            int miembron = ObtenerEntero(sql, "N");
            lblVfecha.Text = fecha;
            lblfecha.Visible = true;
            lblNombre.Visible = true;
            lblVfecha.Visible = true;
            lblVNombre.Visible = true;
            lblVNombre.Text = miembro;
            txbnotas.Text = HttpUtility.HtmlDecode(notas.Replace("&nbsp;", ""));
            gvapadrinado.Visible = false;
            btnmodificar.Visible = true;
            btneliminar.Visible = true;
            btnaceptar.Visible = false;
            lblnotas1.Text = sitio;
            lblVmiembro.Text = Convert.ToString(miembron);
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string mensaje, traducida;
                if (L == "es")
                {
                    mensaje = "Enviare a Guatemala";
                    traducida = "Traducida";
                }
                else
                {
                    mensaje = "Send to Guatemala";
                    traducida = "Translated";
                }
                e.Row.Cells[0].Text = dic.sitio;
                e.Row.Cells[1].Text = dic.fechaInicio;
                e.Row.Cells[2].Text = mensaje;
                e.Row.Cells[3].Text = traducida;
                e.Row.Cells[4].Text = dic.FechaEntregaAPAD;
                e.Row.Cells[5].Text = dic.nombre;
                e.Row.Cells[6].Text = dic.notas;
            }
        }

        protected void btnnuevab_Click(object sender, EventArgs e)
        {
            Response.Redirect("BusquedaPadrinosAPAD.aspx");
        }
    }
}