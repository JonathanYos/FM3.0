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
    public partial class RegistroRestriccionesAPAD : System.Web.UI.Page
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
        protected static string consulta;
        protected static int opcion;
        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        protected void accionar(object sender, EventArgs e)
        {
            switch (opcion)
            {
                case 1:
                    try
                    {
                        APD.ejecutarSQL(consulta);
                        LlenarHistorial(S);
                        Limpiar();
                        mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf(dic.msjNoSeRealizoExcp + ex.Message + ".");
                    }
                    break;
            }
        }
        private void GuardarRegistro()
        {
            string sql = "INSERT INTO dbo.MiscMemberSponsorInfo SELECT Project, MemberId, GETDATE() CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, Photo, PhotoDate, RetakePhotoDate, RetakePhotoUserId, LastCarnetPrintDate, SponsorshipLevel, SponsorshipType,'" + ddltipo.SelectedValue + "' Restriction,GETDATE() RestrictionDate, ExceptionPhotoDate FROM dbo.MiscMemberSponsorInfo WHERE RecordStatus = ' '  AND Project = '" + ddlsitio.SelectedValue + "' AND MemberId = " + txtmiembro.Text + "";
            APD.ejecutarSQL(sql);
            Limpiar();
            LlenarHistorial(S);
            mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
        }
        private void Limpiar()
        {
            txtmiembro.Text = "";
            ddlsitiob.SelectedValue = S;
            ddltipo.SelectedIndex = 0;
            ddlsitio.SelectedValue = S;
        }
        private void LlenarCombos()
        {
            string sql = "SELECT Code,CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END AS descripcion FROM dbo.CdSponsorShipRestriction WHERE Code!='OTRA' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddltipo.DataSource = datos;
            ddltipo.DataValueField = "Code";
            ddltipo.DataTextField = "descripcion";
            ddltipo.DataBind();
            ddltipo.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddltipo.SelectedIndex = 0;


            sql = "SELECT Code,CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM FwCdOrganization WHERE Code !='A' AND Code !='E' AND Code !='S' AND Code !='*' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlsitio.DataSource = datos2;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitio.SelectedIndex = 0;
            ddlsitio.SelectedValue = S;

            sql = "SELECT Code,CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM FwCdOrganization WHERE Code !='A' AND Code !='E' AND Code !='S' AND Code !='*' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter3 = new SqlDataAdapter(sql, con);
            DataTable datos3 = new DataTable();
            adapter2.Fill(datos3);
            ddlsitiob.DataSource = datos3;
            ddlsitiob.DataValueField = "Code";
            ddlsitiob.DataTextField = "descripcion";
            ddlsitiob.DataBind();
            ddlsitiob.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitiob.SelectedIndex = 0;
            ddlsitiob.SelectedValue = S;
        }
        private void LlenarHistorial(string sitio)
        {
            try
            {
                DataTable tabledata = new DataTable();
                string sql = "SELECT MS.Project,MS.MemberId,M.FirstNames+' '+M.LastNames Nombre,CASE WHEN '" + L + "'='es' THEN SR.DescSpanish ELSE SR.DescEnglish END Restriccion,CONVERT(VARCHAR,MS.RestrictionDate,101) 'Fecha de Restriccion', MS.UserId FROM dbo.MiscMemberSponsorInfo MS INNER JOIN dbo.Member M ON MS.MemberId=M.MemberId AND MS.RecordStatus=M.RecordStatus AND MS.Project=M.Project INNER JOIN dbo.CdSponsorShipRestriction SR ON MS.Restriction = SR.Code WHERE  MS.Project='" + sitio + "' AND MS.RecordStatus=' '";
                con.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
                DataSet setDatos = new DataSet();
                adaptador.Fill(setDatos, "listado");
                tabledata = setDatos.Tables["listado"];
                con.Close();
                gvhistorial.DataSource = tabledata;
                gvhistorial.DataBind();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }
        private void traducir()
        {
            lbltipo.Text = dic.TipoAPAD;
            btnguardar.Text = dic.guardar;
            lblsitio.Text = dic.sitio;
            lblmiembro.Text = dic.miembro;
            lblsitiob.Text = dic.sitio;
            btnbuscar.Text = dic.buscar;
        }
        private void VerificarNumero(string miembro, string sitio)
        {
            string sql = "SELECT COUNT(*) conteo FROM dbo.MiscMemberSponsorInfo WHERE RecordStatus=' ' AND MemberId='" + miembro + "' AND Project='" + sitio + "' AND Restriction IS NOT NULL AND RestrictionDate IS NOT NULL";
            int numero = APD.ObtenerEntero(sql, "conteo");
            string sql2 = "SELECT COUNT(*) conteo FROM dbo.Member WHERE RecordStatus=' ' AND MemberId='" + miembro + "' AND Project='" + sitio + "' AND AffiliationStatus='AFIL'";
            int numero2 = APD.ObtenerEntero(sql2, "conteo");
            if (numero2 == 0)
            {
                //InicioIf
                mst.mostrarMsjAdvNtf(dic.MsjmiembronoApadrinado);
                txtmiembro.Text = "";
                ddlsitio.SelectedValue = S;
                ddltipo.SelectedIndex = 0;
                //Fin If
            }
            else
            {
                //Inicio Else 
                if (numero == 1)
                {
                    mst.mostrarMsjAdvNtf(dic.MsjYaingresoRegistro);
                }
                else
                {
                    GuardarRegistro();
                }
                //Fin Else
            }

        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////

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
                try
                {
                    traducir();
                    LlenarCombos();
                    LlenarHistorial(S);
                }
                catch (Exception ex)
                {
                }
            }
        }


        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sitio = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            consulta = "INSERT INTO dbo.MiscMemberSponsorInfo SELECT Project, MemberId, GETDATE() CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, Photo, PhotoDate, RetakePhotoDate, RetakePhotoUserId, LastCarnetPrintDate, SponsorshipLevel, SponsorshipType,NULL Restriction,NULL RestrictionDate, ExceptionPhotoDate FROM dbo.MiscMemberSponsorInfo WHERE RecordStatus = ' '  AND Project = '" + sitio + "' AND MemberId = '" + miembro + "' AND Restriction IS NOT NULL AND RestrictionDate IS NOT NULL";
            opcion = 1;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);

        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string Fechares, Accion;
            if (L == "es")
            {
                Fechares = "Fecha de Restricción";
                Accion = "Acción";
            }
            else
            {
                Fechares = "Date Restriction";
                Accion = "Action";
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].Text = Accion;
                e.Row.Cells[0].Text = dic.sitio;
                e.Row.Cells[1].Text = dic.miembro;
                e.Row.Cells[2].Text = dic.nombre;
                e.Row.Cells[3].Text = dic.restriccionAPAD;
                e.Row.Cells[4].Text = Fechares;
                e.Row.Cells[5].Text = dic.usuario;
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            string numero = txtmiembro.Text;
            string sitio = ddlsitio.SelectedValue;
            if (ddlsitio.SelectedIndex == 0 || txtmiembro.Text == "" || ddltipo.SelectedIndex == 0)
            {
                mst.mostrarMsjAdvNtf(dic.CampoVacioAPAD);
            }
            else
            {
                VerificarNumero(numero, sitio);
            }

        }




        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LlenarHistorial(ddlsitiob.SelectedValue);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(dic.msjNoSeRealizoExcp + ex.Message + ".");
            }
        }

    }
}