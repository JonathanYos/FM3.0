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
    public partial class PerfilPadrinoAPAD : System.Web.UI.Page
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
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected static int numopcion;

        /////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void accionar(object sender, EventArgs e)
        {
            Response.Redirect("BusquedaPadrinosAPAD.aspx");
        }
        private void Apadrinados()
        {
            string sql = "SELECT CASE WHEN '" + L + "'='es' THEN O.DescSpanish ELSE O.DescEnglish END AS Sitio,SMR.MemberId,M.FirstNames+' '+M.LastNames 'Nombre', dbo.fn_GEN_FormatDate(SMR.StartDate,'" + L + "') 'Fecha de Inicio',dbo.fn_GEN_FormatDate(SMR.EndDate,'" + L + "') 'Fecha Fin', CASE WHEN '" + L + "'='es' THEN SMT.DescSpanish ELSE SMT.DescEnglish END Tipo,  dbo.fn_GEN_edad(M.BirthDate,'" + L + "') AS 'Cumpleaños'  FROM dbo.SponsorMemberRelation SMR INNER JOIN dbo.Member M ON SMR.MemberId=M.MemberId  AND SMR.RecordStatus=M.RecordStatus AND SMR.Project=M.Project INNER JOIN dbo.CdSponsorMemberRelationType SMT ON SMR.Type=SMT.Code INNER JOIN dbo.FwCdOrganization O ON SMR.Project=O.Code WHERE SMR.RecordStatus=' ' AND SMR.SponsorId='" + P + "' ";
            DataTable tabledata;
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            gvhistorial.DataSource = tabledata;
            gvhistorial.DataBind();
        }
        private void LlenarInformacion()
        {
            btnnuevab.Text = dic.nuevaBusqueda;
            string sql = "SELECT S.SponsorNames, CASE WHEN '" + L + "'='es' THEN P.DescSpanish ELSE P.DescEnglish END Estado, CASE WHEN '" + L + "'='es' THEN C.DescSpanish ELSE C.DescEnglish END Pais, S.OrganizationContactNames,CASE WHEN S.SpeaksSpanish=0 THEN '" + dic.NoAPAD + "' ELSE '" + dic.SiAPAD + "' END 'Habla Español',CASE WHEN '" + L + "'='es' THEN G.DescSpanish ELSE G.DescEnglish END Genero FROM dbo.Sponsor S INNER JOIN dbo.CdStateOrProvince P ON S.StateOrProvince=P.Code INNER JOIN dbo.CdCountry C ON S.Country=C.Code INNER JOIN dbo.CdGender G ON G.Code=S.Gender WHERE S.RecordStatus=' ' AND S.Gender NOT LIKE 'D' AND S.SponsorId = " + P + "";
            DataTable dt;
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            dt = setDatos.Tables["listado"];
            conexion.Close();

            string nombre = dt.Rows[0]["SponsorNames"].ToString();
            string estado = dt.Rows[0]["Estado"].ToString();
            string pais = dt.Rows[0]["Pais"].ToString();
            string organizacion = dt.Rows[0]["OrganizationContactNames"].ToString();
            string genero = dt.Rows[0]["Genero"].ToString();
            string espanol = dt.Rows[0]["Habla Español"].ToString();
            lblorganizacion.Text = dic.OrganizacionAPAD;
            lblnombre.Text = dic.nombre;
            lblespanol.Text = dic.HablaEspanolAPAD;
            lblestado.Text = dic.EstadoOProvinciaAPAD;
            lblpais.Text = dic.PaisAPAD;
            lblgenero.Text = dic.genero;
            lblVespanol.Text = espanol;
            lblVestado.Text = estado;
            lblVgenero.Text = genero;
            lblVnombre.Text = nombre;
            lblVpais.Text = pais;
            lblVorganizacion.Text = organizacion;
            lblid.Text = P;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "No.";
                e.Row.Cells[1].Text = dic.nombre;
                e.Row.Cells[2].Text = dic.sitio;
                e.Row.Cells[3].Text = dic.FechaInicioAPAD;
                e.Row.Cells[4].Text = dic.FechaFinAPAD;
                e.Row.Cells[5].Text = dic.TipoAPAD;
                e.Row.Cells[6].Text = dic.fechaNacimiento;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            P = mast.P;
            mst = (mast)Master;
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            P = (String)Session["P"];
            mst.contentCallEvent += new EventHandler(accionar);
            if (String.IsNullOrEmpty(P))
            {
                string mensaje;
                if (L == "es")
                {
                    mensaje = "No has seleccionado ningún padrino";
                }
                else
                {
                    mensaje = "You have not selected any sponsors";
                }
                mst.mostrarMsjOpcionMdl(mensaje);
            }
            if (!IsPostBack)
            {
                try
                {
                    LlenarInformacion();
                    Apadrinados();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btnnuevab_Click(object sender, EventArgs e)
        {
            Response.Redirect("BusquedaPadrinosAPAD.aspx");
        }
    }
}