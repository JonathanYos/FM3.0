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
    public partial class RegistroCarnetsAPAD : System.Web.UI.Page
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
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            mst = (mast)Master;
            if (!IsPostBack)
            {
                try
                {
                    Traducir();
                    llenarhistorial();
                    llenarcombos();
                    llenarmese();
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }

        }
        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        protected void GuardarCarnet()
        {
            if (string.IsNullOrEmpty(txbmiembro.Text) || ddlsitio.SelectedIndex == 0)
            {
                mst.mostrarMsjNtf(dic.CampoVacioAPAD);
            }
            else
            {
                string sitio = ddlsitio.SelectedValue;
                string miembro = txbmiembro.Text;
                string sql = "SELECT COUNT(*) conteo FROM dbo.Member WHERE RecordStatus=' ' AND MemberId='" + miembro + "' AND Project='" + sitio + "' AND AffiliationStatus='AFIL'";
                int resultado = APD.ObtenerEntero(sql, "conteo");
                if (resultado == 0)
                {
                    mst.mostrarMsjNtf(dic.MsjmiembronoApadrinado);
                }
                else
                {
                    sql = "SELECT COUNT(*) conteo FROM MemberSolicitudeCard WHERE RecordStatus=' ' AND Project='" + sitio + "' AND MemberId='" + miembro + "'";
                    int resultado2 = APD.ObtenerEntero(sql, "conteo");
                    if (resultado2 == 1)
                    {
                        mst.mostrarMsjNtf(dic.MsjYaingresoRegistro);
                    }
                    else
                    {
                        string sql2 = "INSERT INTO MemberSolicitudeCard VALUES('" + sitio + "','" + miembro + "',GETDATE(),GETDATE(),'" + U + "',' ','" + U + "',NULL,NULL)";
                        APD.ejecutarSQL(sql2);
                        Limpiar();
                        llenarhistorial();
                        mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                    }
                }
            }
        }
        protected void Limpiar()
        {
            ddlsitio.SelectedValue = S;
            txbmiembro.Text = "";
            txbanio.Text = "";
            ddlmes.SelectedIndex = 0;
            ddlsite.SelectedValue = S;
        }
        private void llenarcombos()
        {
            string SQL = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' AND Code NOT LIKE'*' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlsitio.DataSource = datos;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitio.SelectedValue = S;

            ddlsite.DataSource = datos;
            ddlsite.DataValueField = "Code";
            ddlsite.DataTextField = "descripcion";
            ddlsite.DataBind();
            ddlsite.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsite.SelectedValue = S;


        }
        private void llenarhistorial()
        {
            DataTable tabledata = new DataTable();
            string sql = "SELECT  CASE WHEN '" + L + "'='es' THEN O.DescSpanish ELSE O.DescEnglish END Sitio,C.MemberId, M.FirstNames +' '+M.LastNames Nombre,M.LastFamilyId Familia, CONVERT(VARCHAR,C.SolicitudeDate,101) 'Solicitud', CONVERT(VARCHAR,C.PrintDate,101) Renovacion, C.UserId Usuario,C.Project FROM MemberSolicitudeCard C INNER JOIN dbo.Member M ON C.Project=M.Project AND C.RecordStatus=M.RecordStatus AND C.MemberId=M.MemberId INNER JOIN dbo.FwCdOrganization O ON C.Project=O.Code WHERE C.Project='" + S + "' AND YEAR(C.SolicitudeDate) = YEAR(C.SolicitudeDate) AND MONTH(C.SolicitudeDate)=MONTH(C.SolicitudeDate) AND C.RecordStatus=' '";
            con.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            con.Close();
            gvhistorial.DataSource = tabledata;
            gvhistorial.DataBind();
        }
        private void llenarmese()
        {

            ddlmes.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            string[] categoria = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            string[] category = { "January", "February", "March", "April", "May", "June", "Jaly", "August", "September", "October", "November", "December" };
            if (L == "es")
            {
                for (int i = 0; i < 12; i++)
                {
                    ddlmes.Items.Insert(i + 1, new ListItem(categoria[i], "C" + i));
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    ddlmes.Items.Insert(i + 1, new ListItem(category[i], "C" + i));
                }
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
                mst.mostrarMsjNtf("Error" + ex.Message);
                string temp = "||||||";
                return temp;
            }

        }
        private void Traducir()
        {
            btnbuscar.Text = dic.buscar;
            btningresar.Text = dic.guardar;
            lblmiembro.Text = dic.miembro;
            lblsitio.Text = dic.sitio;
            lblsite.Text = dic.sitio;
            if (L == "es")
            {
                lblmes.Text = " Mes";
                lblanio.Text = " Año";
            }
            else
            {
                lblmes.Text = " Month";
                lblanio.Text = " Year";
            }

        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            gvhistorial.DataSource = null;
            gvhistorial.DataBind();
            if (string.IsNullOrEmpty(txbanio.Text) && ddlsite.SelectedValue == S && ddlmes.SelectedIndex == 0)
            {
                llenarhistorial();
            }
            else
            {
                string mes;
                string anio3;
                string sitio;
                int anio = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

                string sitio2 = ddlsite.SelectedValue;
                int meso = ddlmes.SelectedIndex;


                if (string.IsNullOrEmpty(txbanio.Text))
                {
                    anio3 = "AND YEAR(C.SolicitudeDate) = YEAR(C.SolicitudeDate) ";
                }
                else
                {
                    int anio2 = Convert.ToInt32(txbanio.Text);
                    anio3 = "AND YEAR(C.SolicitudeDate) = " + anio2 + " ";
                }
                if (ddlmes.SelectedIndex == 0)
                {
                    mes = "AND MONTH(C.SolicitudeDate)=MONTH(C.SolicitudeDate) ";
                }
                else
                {
                    mes = "AND MONTH(C.SolicitudeDate)=" + meso + " ";
                }
                if (ddlsite.SelectedIndex == 0)
                {
                    sitio = "C.Project=C.Project ";
                }
                else
                {
                    sitio = "C.Project='" + ddlsite.SelectedValue + "' ";
                }
                DataTable tabledata = new DataTable();
                string sql = "SELECT  CASE WHEN '" + L + "'='es' THEN O.DescSpanish ELSE O.DescEnglish END Sitio,C.MemberId, M.FirstNames +' '+M.LastNames Nombre,M.LastFamilyId Familia, CONVERT(VARCHAR,C.SolicitudeDate,101) 'Solicitud', CONVERT(VARCHAR,C.PrintDate,101) Renovacion, C.SolicitudeUser Usuario, C.Project FROM MemberSolicitudeCard C INNER JOIN dbo.Member M ON C.Project=M.Project AND C.RecordStatus=M.RecordStatus AND C.MemberId=M.MemberId INNER JOIN dbo.FwCdOrganization O ON C.Project=O.Code WHERE " + sitio + " " + anio3 + " " + mes + " AND C.RecordStatus=' '";
                con.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
                DataSet setDatos = new DataSet();
                adaptador.Fill(setDatos, "listado");
                tabledata = setDatos.Tables["listado"];
                con.Close();

                int totalregistros = tabledata.Rows.Count;
                if (totalregistros > 0)
                {
                    gvhistorial.DataSource = tabledata;
                    gvhistorial.DataBind();
                }
                else
                {
                    mst.mostrarMsjNtf(dic.msjNoEncontroResultados);
                    gvhistorial.DataSource = tabledata;
                    gvhistorial.DataBind();
                }
            }
        }
        protected void btningresar_Click(object sender, EventArgs e)
        {
            GuardarCarnet();
        }
        

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            { 
            e.Row.Cells[0].Text=dic.sitio;
            e.Row.Cells[1].Text = dic.miembro;
            e.Row.Cells[2].Text = dic.nombre;
            e.Row.Cells[3].Text = dic.familia;
            e.Row.Cells[4].Text = dic.solicitud;
            e.Row.Cells[5].Text = dic.RenovacionAPAD;
            e.Row.Cells[6].Text = dic.usuario;
            }
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            string nombre = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            string consulta = "SELECT Project FROM dbo.Member WHERE RecordStatus=' ' AND MemberId="+miembro+" AND FirstNames+' '+LastNames = '"+nombre+"'";
            string project = obtienePalabra(consulta, "Project");
            Response.Write(consulta+project);
            
            //string sql = "INSERT INTO dbo.MemberSolicitudeCard(Project,MemberId,CreationDateTime,SolicitudeDate,SolicitudeUser,RecordStatus,UserId,ExpirationDateTime,PrintDate) SELECT Project,MemberId,GETDATE() CreationDateTime,SolicitudeDate,SolicitudeUser,'H'RecordStatus,'"+U+"' UserId,GETDATE() ExpirationDateTime,PrintDate FROM dbo.MemberSolicitudeCard WHERE RecordStatus=' ' AND MemberId="+miembro+" AND Project='"+project+"'";
            //Response.Write(sql);
            //APD.ejecutarSQL(sql);
        }
        
    }
}