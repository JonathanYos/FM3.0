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
    public partial class CartasFamiliaAPAD : System.Web.UI.Page
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
        public string categoria;
        public int temp3;
        public string sitio;
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            mst = (mast)Master;
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            determinarcategoria();

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
        private void determinarcategoria()
        {
            string sql = "SELECT MONTH(GETDATE()) Mes";
            int mes = ObtenerEntero(sql, "Mes");
            if (mes < 6)
            {
                categoria = "PRIM";
            }
            else
            {
                categoria = "SEGU";

            }
        }
        private void IngresoCarta(string padrino, string me, string me2)
        {

            bool IsUpdated = false;
            string query = "INSERT INTO MemberSponsorLetter (Project, SponsorId, MemberId, SponsorOrMember, DateTimeWritten, CreationDateTime, RecordStatus, UserId, Category, Notes) VALUES('" + me2 + "', '" + padrino + "', '" + me + "', 'M',GETDATE(), GETDATE(), ' ', '" + U + "', '" + categoria + "', NULL)";
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = query;
                        cmd.Connection = sqlCon;
                        sqlCon.Open();
                        IsUpdated = cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception e)
                    {
                        mst.mostrarMsjAdvNtf(e.Message);
                    }
                    finally
                    {
                        sqlCon.Close();
                    }
                }
            }
        }
        private void llenarcombo()
        {
            string sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdLetterCategory WHERE Code='" + categoria + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcategoria.DataSource = datos;
            ddlcategoria.DataValueField = "Code";
            ddlcategoria.DataTextField = "descripcion";
            ddlcategoria.DataBind();
            ddlcategoria.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlcategoria.SelectedValue = categoria;
            ddlcategoria.Enabled = false;

            string sql2 = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code !='A' AND Code!='E' AND Code!='S' AND Code!='*' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlsitio.DataSource = datos2;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitio.SelectedValue = categoria;
        }
        private void llenarhistorial(int familia, string sitio)
        {
            gvhistorial2.Visible = true;
            string sql = "SELECT M.Project 'Sitio',M.MemberId 'Miembro',M.FirstNames+' '+M.LastNames 'Nombre',ML.SponsorId 'Padrinos', S.SponsorNames ,ML.DateTimeWritten 'Escrita', ML.DateSent 'Enviada',CASE WHEN '" + L + "'='es' THEN LC.DescSpanish ELSE LC.DescEnglish END  'Categoria' FROM dbo.Member M INNER JOIN dbo.MemberSponsorLetter ML ON M.MemberId=ML.MemberId AND M.Project=ML.Project AND M.RecordStatus=ML.RecordStatus INNER JOIN dbo.CdLetterCategory LC ON ML.Category=LC.Code INNER JOIN dbo.Sponsor S ON M.RecordStatus=S.RecordStatus AND ML.SponsorId = S.SponsorId WHERE M.LastFamilyId='" + familia + "' AND M.RecordStatus=' ' AND M.Project='" + sitio + "' AND M.AffiliationStatus='AFIL' AND YEAR(ML.CreationDateTime)=YEAR(GETDATE()) ORDER BY ML.CreationDateTime DESC";
            DataTable tabledata;
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            gvhistorial2.DataSource = tabledata;
            gvhistorial2.DataBind();
        }
        private void mostrarhistorialF()
        {
            try
            {
                sitio = ddlsitio.SelectedValue;
                temp3 = Convert.ToInt32(txbfamilia.Text);
                if (string.IsNullOrEmpty(txbfamilia.Text) || string.IsNullOrWhiteSpace(txbfamilia.Text))
                {
                    mostrarhistorialM();
                }
                string sql3 = "SELECT COUNT(*) conteo FROM dbo.Member WHERE LastFamilyId='" + temp3 + "' AND RecordStatus=' ' AND Project='" + sitio + "' AND AffiliationStatus='AFIL'";
                int temp2 = ObtenerEntero(sql3, "conteo");
                if (temp2 <= 0)
                {
                    veringreso();
                    mst.mostrarMsjAdvNtf(dic.MsjFamilianoAfiliada);
                    gvhistorial2.Visible = false;
                }
                else
                {
                    string sql4 = "SELECT COUNT(*) conteo FROM Member M WHERE 0 = (SELECT COUNT(*) FROM MemberSponsorLetter MSL WHERE MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND MSL.Category = '" + categoria + "'  AND YEAR(DateTimeWritten) = YEAR(GETDATE())) AND M.RecordStatus = ' ' AND M.Project = '" + sitio + "' AND M.LastFamilyId = '" + temp3 + "' AND M.AffiliationStatus = 'AFIL'";
                    int apadsincarta = ObtenerEntero(sql4, "conteo");
                    if (apadsincarta <= 0)
                    {
                        verhistorial();
                        mst.mostrarMsjAdvNtf(dic.MsjYaingresoRegistro);
                        llenarhistorial(temp3, sitio);
                        btningresar.Visible = false;
                        string sql1 = "SELECT M.Project AS Sitio, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, dbo.fn_gen_npadrinos(M.Project,M.MemberId) Padrinos FROM Member M WHERE 0 = (SELECT COUNT(*) FROM MemberSponsorLetter MSL WHERE MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND MSL.Category = '" + categoria + "'  AND YEAR(DateTimeWritten) = YEAR(GETDATE())) AND M.RecordStatus = ' ' AND M.Project = '" + sitio + "' AND M.LastFamilyId = '" + temp3 + "' AND M.AffiliationStatus = 'AFIL' AND  dbo.fn_gen_npadrinos(M.Project,M.MemberId)>0";
                        DataTable tabledata;
                        SqlConnection conexion = new SqlConnection(ConnectionString);
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(sql1, conexion);
                        DataSet setDatos = new DataSet();
                        adaptador.Fill(setDatos, "listado");
                        tabledata = setDatos.Tables["listado"];
                        conexion.Close();
                        gvhistorial.DataSource = tabledata;
                        gvhistorial.DataBind();
                    }
                    else
                    {
                        string sql1 = "SELECT M.Project AS Sitio, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, dbo.fn_gen_npadrinos(M.Project,M.MemberId) Padrinos FROM Member M WHERE 0 = (SELECT COUNT(*) FROM MemberSponsorLetter MSL WHERE MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND MSL.Category = '" + categoria + "'  AND YEAR(DateTimeWritten) = YEAR(GETDATE())) AND M.RecordStatus = ' ' AND M.Project = '" + sitio + "' AND M.LastFamilyId = '" + temp3 + "' AND M.AffiliationStatus = 'AFIL' AND  dbo.fn_gen_npadrinos(M.Project,M.MemberId)>0";
                        DataTable tabledata;
                        SqlConnection conexion = new SqlConnection(ConnectionString);
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(sql1, conexion);
                        DataSet setDatos = new DataSet();
                        adaptador.Fill(setDatos, "listado");
                        tabledata = setDatos.Tables["listado"];
                        conexion.Close();
                        gvhistorial.DataSource = tabledata;
                        gvhistorial.DataBind();
                        llenarhistorial(temp3, sitio);
                        verhistorial();
                    }

                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }
        private void mostrarhistorialM()
        {
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            if (string.IsNullOrEmpty(txtapad.Text) || string.IsNullOrWhiteSpace(txtapad.Text))
            {
                mostrarhistorialF();
            }
            int temp;
            string numero = txtapad.Text;
            try
            {
                sitio = ddlsitio.SelectedValue;
                string sql3 = "SELECT COUNT(*) conteo FROM dbo.Member WHERE MemberId='" + numero + "' AND RecordStatus=' ' AND Project='" + sitio + "' AND AffiliationType='BECA' AND AffiliationStatus='AFIL' OR MemberId='" + numero + "' AND RecordStatus=' ' AND Project='" + S + "' AND AffiliationType='NORM' AND AffiliationStatus='AFIL' ";
                int temp2 = ObtenerEntero(sql3, "conteo");

                if (temp2 <= 0)
                {
                    veringreso();
                    mst.mostrarMsjAdvNtf(dic.MsjNohaSeleccionadomiembro);
                    gvhistorial2.Visible = false;
                }
                else
                {
                    string sql = "SELECT LastFamilyId Familia FROM dbo.Member WHERE MemberId='" + numero + "' AND RecordStatus=' ' AND Project='" + sitio + "'";
                    temp3 = ObtenerEntero(sql, "Familia");
                    string sql4 = "SELECT COUNT(*) conteo FROM Member M WHERE 0 = (SELECT COUNT(*) FROM MemberSponsorLetter MSL WHERE MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND MSL.Category = '" + categoria + "'  AND YEAR(DateTimeWritten) = YEAR(GETDATE())) AND M.RecordStatus = ' ' AND M.Project = '" + sitio + "' AND M.LastFamilyId = '" + temp3 + "' AND M.AffiliationStatus = 'AFIL'";
                    int apadsincarta = ObtenerEntero(sql4, "conteo");
                    if (apadsincarta <= 0)
                    {
                        verhistorial();
                        mst.mostrarMsjAdvNtf(dic.MsjYaingresoRegistro);
                        btningresar.Visible = false;
                        llenarhistorial(temp3, sitio);

                    }
                    else
                    {
                        string sql1 = "SELECT M.Project AS Sitio, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, dbo.fn_gen_npadrinos(M.Project,M.MemberId) Padrinos FROM Member M WHERE 0 = (SELECT COUNT(*) FROM MemberSponsorLetter MSL WHERE MSL.RecordStatus = M.RecordStatus AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId AND MSL.Category = '" + categoria + "'  AND YEAR(DateTimeWritten) = YEAR(GETDATE())) AND M.RecordStatus = ' ' AND M.Project = '" + sitio + "' AND M.LastFamilyId = '" + temp3 + "' AND M.AffiliationStatus = 'AFIL' AND  dbo.fn_gen_npadrinos(M.Project,M.MemberId)>0";
                        DataTable tabledata;
                        SqlConnection conexion = new SqlConnection(ConnectionString);
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(sql1, conexion);
                        DataSet setDatos = new DataSet();
                        adaptador.Fill(setDatos, "listado");
                        tabledata = setDatos.Tables["listado"];
                        conexion.Close();
                        gvhistorial.DataSource = tabledata;
                        gvhistorial.DataBind();
                        llenarhistorial(temp3, sitio);
                        verhistorial();
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
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
                return -1;
            }
        }
        private void traducir()
        {
            btnbuscar.Text = dic.buscar;
            lblapad.Text = dic.miembro;
            btnregresar.Text = dic.nuevaBusqueda;
            lblcategoria.Text = dic.categoriaAPAD;
            btningresar.Text = dic.guardar;
            lblfamilia.Text = dic.familia;
            lblsitio.Text = dic.sitio;
            ddlsitio.SelectedValue = S;
        }
        private void valoresiniciales()
        {
            llenarcombo();
            traducir();
            veringreso();
        }
        private void verhistorial()
        {
            historial.Visible = true;
            pnlbusqueda.Visible = false;
        }
        private void veringreso()
        {
            historial.Visible = false;
            pnlbusqueda.Visible = true;
            txtapad.Text = "";
            txbfamilia.Text = "";
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            string miembro = txtapad.Text;
            string familia = txbfamilia.Text;
            if (string.IsNullOrEmpty(miembro) && string.IsNullOrEmpty(familia) || ddlsitio.SelectedIndex == 0)
            {
                mst.mostrarMsjAdvNtf(dic.CampoVacioAPAD);
            }
            else
            {
                if (!string.IsNullOrEmpty(miembro) && !string.IsNullOrEmpty(familia))
                {
                    mst.mostrarMsjAdvNtf(dic.MsjAmbosCamposestanllenos);
                }
                else
                {
                    if (!string.IsNullOrEmpty(miembro) && string.IsNullOrEmpty(familia))
                    {
                        mostrarhistorialM();
                    }
                    if (string.IsNullOrEmpty(miembro) && !string.IsNullOrEmpty(familia))
                    {
                        mostrarhistorialF();
                    }
                }

            }
        }

        protected void btnregresar_Click(object sender, EventArgs e)
        {
            veringreso();
            gvhistorial2.DataSource = null;
            gvhistorial2.DataBind();
            btningresar.Visible = true;
            gvhistorial.DataSource = null;
            gvhistorial.DataBind();
        }
        protected void btningresar_Click(object sender, EventArgs e)
        {
            string me = string.Empty;
            string me2 = string.Empty;
            int conteo = 0;
            foreach (GridViewRow gvrow in gvhistorial.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkentregado");

                if (chk.Checked)
                {
                    conteo = conteo + 1;
                }
            }
            if (conteo == 0)
            {
                mst.mostrarMsjAdvNtf(dic.MsjNohaSeleccionadomiembro);
            }
            else
            {
                foreach (GridViewRow gvrow in gvhistorial.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkentregado");
                    if (chk.Checked)
                    {
                        me2 = gvrow.Cells[1].Text;
                        me = gvrow.Cells[2].Text;
                        string consulta = "SELECT COUNT(*) conteo FROM dbo.SponsorMemberRelation WHERE RecordStatus=' ' AND EndDate IS NULL AND Project = '" + me2 + "' AND MemberId=" + me + "";
                        int resultado = ObtenerEntero(consulta, "conteo");
                        string tabla = "SELECT SponsorId FROM dbo.SponsorMemberRelation WHERE RecordStatus=' ' AND EndDate IS NULL AND Project = '" + me2 + "' AND MemberId=" + me + "";
                        DataTable tabledata;
                        SqlConnection conexion = new SqlConnection(ConnectionString);
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(tabla, conexion);
                        DataSet setDatos = new DataSet();
                        adaptador.Fill(setDatos, "listado");
                        tabledata = setDatos.Tables["listado"];
                        conexion.Close();
                        for (int a = 0; a < resultado; a++)
                        {
                            string padrino = tabledata.Rows[a]["SponsorId"].ToString(); ;
                            IngresoCarta(padrino, me, me2);
                        }

                    }
                }
                mostrarhistorialF();
                mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
            }
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string hola = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        }
        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.sitio;
                e.Row.Cells[2].Text = dic.miembro;
                e.Row.Cells[3].Text = dic.nombre;
            }
        }

    }
}