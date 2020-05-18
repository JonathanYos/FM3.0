using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Data.Objects;

namespace Familias3._1.Apadrinamiento
{
    public partial class BusquedaPadrinosAPAD : System.Web.UI.Page
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



        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void limpiarcampos()
        {
            ddlestado.SelectedIndex = 0;
            ddlpais.SelectedIndex = 0;
            txtnombre.Text = "";
            txtnumero.Text = "";
            cbhabla.Checked = false;
            tbhistorial.Visible = false;
            tbregistro.Visible = true;
        }
        private void llenarHistorial()
        {
            string estado = ddlestado.SelectedValue;
            string pais = ddlpais.SelectedValue;
            string numero = txtnumero.Text;
            string nombre = txtnombre.Text;

            string[] esvacio = { estado, pais, numero, nombre };
            if (string.IsNullOrEmpty(esvacio[0]) && string.IsNullOrEmpty(esvacio[1]) && string.IsNullOrEmpty(esvacio[2]) && string.IsNullOrEmpty(esvacio[3]) && cbhabla.Checked == false)
            {
                mst.mostrarMsjAdvNtf(dic.msjDebeingresarUno);
            }
            else
            {
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrWhiteSpace(nombre))
                {
                    nombre = "= S.SponsorNames";
                }
                else
                {
                    nombre = "LIKE ('" + txtnombre.Text + "')";
                }
                if (string.IsNullOrEmpty(pais) || string.IsNullOrWhiteSpace(pais))
                {
                    pais = "S.Country";
                }
                else
                {
                    pais = "'" + ddlpais.SelectedValue + "'";
                }
                if (string.IsNullOrEmpty(estado) || string.IsNullOrWhiteSpace(estado))
                {
                    estado = "S.StateOrProvince";
                }
                else
                {
                    estado = "'" + ddlestado.SelectedValue + "'";
                }
                if (string.IsNullOrEmpty(numero) || string.IsNullOrWhiteSpace(numero))
                {
                    numero = "S.SponsorId";
                }
                else
                {
                    numero = "'" + txtnumero.Text.Replace(" ", "") + "'";
                }
                int check;
                if (cbhabla.Checked == true)
                {
                    check = 1;
                }
                else
                {
                    check = 0;
                }
                string sqlconteo = "SELECT COUNT(*) conteo FROM dbo.Sponsor S INNER JOIN dbo.CdStateOrProvince P ON S.StateOrProvince=P.Code INNER JOIN dbo.CdCountry C ON S.Country=C.Code INNER JOIN dbo.CdGender G ON G.Code=S.Gender WHERE S.RecordStatus=' ' AND S.Gender NOT LIKE 'D' AND S.SponsorId = " + numero + " AND S.SponsorNames " + nombre + " AND S.StateOrProvince = " + estado + " AND S.Country = " + pais + " AND S.SpeaksSpanish=" + check + " ";
                int conteo = APD.ObtenerEntero(sqlconteo, "conteo");
                string sql = "SELECT S.SponsorId, S.SponsorNames,CASE WHEN '" + L + "'='es' THEN P.DescSpanish ELSE P.DescEnglish END Estado, CASE WHEN '" + L + "'='es' THEN C.DescSpanish ELSE C.DescEnglish END Pais, S.OrganizationContactNames,CASE WHEN S.SpeaksSpanish=0 THEN '" + dic.NoAPAD + "' ELSE '" + dic.SiAPAD + "' END 'Habla Español',CASE WHEN '" + L + "'='es' THEN G.DescSpanish ELSE G.DescEnglish END Genero FROM dbo.Sponsor S INNER JOIN dbo.CdStateOrProvince P ON S.StateOrProvince=P.Code INNER JOIN dbo.CdCountry C ON S.Country=C.Code INNER JOIN dbo.CdGender G ON G.Code=S.Gender WHERE S.RecordStatus=' ' AND S.Gender NOT LIKE 'D' AND S.SponsorId = " + numero + " AND S.SponsorNames " + nombre + " AND S.StateOrProvince = " + estado + " AND S.Country = " + pais + " AND S.SpeaksSpanish=" + check + " AND 0<(SELECT COUNT(*) FROM dbo.SponsorMemberRelation WHERE RecordStatus=' ' AND EndDate IS NULL AND SponsorId=S.SponsorId AND EndDate IS NULL) ORDER BY S.StateOrProvince ASC";
                if (conteo < 1)
                {
                    mst.mostrarMsjAdvNtf("Ningun Candidato Fue Encontrado");
                }
                else
                {
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
                    tbhistorial.Visible = true;
                    tbregistro.Visible = false;
                    gvhistorial.Columns[5].HeaderText = dic.PaisAPAD;
                }
            }

        }
        private void llenarlabelsyotros()
        {
            lblpais.Text = dic.PaisAPAD;
            lblestado.Text = dic.EstadoOProvinciaAPAD;
            lblnombre.Text = dic.nombres;
            lblhabla.Text = dic.HablaEspanolAPAD;
            lblnumero.Text = dic.NumeroPadrino;
            btnbuscar.Text = dic.buscar;

            string sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdStateOrProvince ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END  ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlestado.DataSource = datos;
            ddlestado.DataValueField = "Code";
            ddlestado.DataTextField = "descripcion";
            ddlestado.DataBind();
            ddlestado.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlestado.SelectedIndex = 0;
            string sql2 = " SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdCountry ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC ";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, ConnectionString);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlpais.DataSource = datos2;
            ddlpais.DataValueField = "Code";
            ddlpais.DataTextField = "descripcion";
            ddlpais.DataBind();
            ddlpais.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlpais.SelectedIndex = 0;
            tbhistorial.Visible = false;
            btnOtraB.Text = dic.OtraBusquedaAPAD;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            llenarHistorial();
        }
        protected void btnOtraB_Click(object sender, EventArgs e)
        {
            limpiarcampos();
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton link = (LinkButton)gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnSName");
            P = link.Text;
            Session["P"] = P;
            Response.Redirect("PerfilPadrinoAPAD.aspx");
        }
        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = dic.PaisAPAD;
                e.Row.Cells[2].Text = dic.EstadoOProvinciaAPAD;
                e.Row.Cells[4].Text = dic.OrganizacionAPAD;
                e.Row.Cells[5].Text = dic.HablaEspanolAPAD;
                e.Row.Cells[6].Text = dic.genero;
                e.Row.Cells[1].Text = dic.nombre;
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

            if (!IsPostBack)
            {
                try
                {
                    llenarlabelsyotros();
                }
                catch (Exception ex)
                {
                }
            }
        }

    }
}