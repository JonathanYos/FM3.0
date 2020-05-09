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

namespace Familias3._1.EDUC
{
    public partial class ReporteAnualActividades : System.Web.UI.Page
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
        protected static string sql, creat, tipo;
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
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            BDM = new BDMiembro();
            // mst.contentCallEvent += new EventHandler(accionar);
            if (!IsPostBack)
            {
                mst = (mast)Master;
                BDM = new BDMiembro();
                APD = new BDAPAD();
                try
                {
                    if (string.IsNullOrEmpty(M)) { pnltodo.Visible = false; } else { ValoresIniciales(); }
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }
        }
        protected void ValoresIniciales()
        {
            DateTime Now = DateTime.Now;
            sql = "SELECT Code, DescSpanish FROM dbo.CdMemberActivityType WHERE Active = 1 AND Project IN ('*', '" + S + "')  AND FunctionalArea = 'EDUC' ORDER BY DescSpanish ";
            bdCombo(sql, ddlactividad, "Code", "DescSpanish");

            lblanio.Text = dic.año;
            chkinfogen.Text = "  " + dic.infoGeneral;
            btngenerar.Text = dic.generar;
            txtanio.Text = Now.Year.ToString();
            lblactividad.Text = dic.actividad;
        }
        protected void LlenarInfoGen()
        {
            string where;
            where = " WHERE Area = 'EDUC' ";

            where = where + " AND AÑO = " + txtanio.Text + " ";

            where = where + " AND  Actividad = '" + ddlactividad.SelectedItem.Text + "' ";

            sql = "SELECT Miembro, Nombre, Familia, dbo.fn_GEN_tipoMiembro('" + S + "', Miembro) TipoMiembro, Actividad, Año, SUM(CASE WHEN Mes = 1 THEN Total ELSE 0 END) AS Ene, SUM(CASE WHEN Mes = 2 THEN Total ELSE 0 END) AS Feb, SUM(CASE WHEN Mes = 3 THEN Total ELSE 0 END) AS Mar, SUM(CASE WHEN Mes = 4 THEN Total ELSE 0 END) AS Abr, SUM(CASE WHEN Mes = 5 THEN Total ELSE 0 END) AS May, SUM(CASE WHEN Mes = 6 THEN Total ELSE 0 END) AS Jun, SUM(CASE WHEN Mes = 7 THEN Total ELSE 0 END) AS Jul, SUM(CASE WHEN Mes = 8 THEN Total ELSE 0 END) AS Ago, SUM(CASE WHEN Mes = 9 THEN Total ELSE 0 END) AS Sep, SUM(CASE WHEN Mes = 10 THEN Total ELSE 0 END) AS Oct, SUM(CASE WHEN Mes = 11 THEN Total ELSE 0 END) AS Nov, SUM(CASE WHEN Mes = 12 THEN Total ELSE 0 END) AS Dic, SUM(Total) AS Total FROM dbo.fn_MISC_ActividadesPorMiembro('" + S + "') " + where + "GROUP BY Miembro, Nombre, Familia,  dbo.fn_GEN_tipoMiembro('" + S + "', Miembro), Actividad, Año ORDER BY Nombre, Familia,  Actividad, Año ";
            llenargrid(sql);
        }
        protected void btngenerar_Click(object sender, EventArgs e)
        {
            if (ddlactividad.SelectedIndex == 0 || txtanio.Text.Length < 4)
            {
                mst.mostrarMsjAdvNtf("Por fabor llene los campos de año y actividad");
            }
            else
            {
                if (chkinfogen.Checked == true)
                {
                    LlenarInfoGen();
                }
                else
                {
                    LlenarInfo();
                }
            }
        }

        protected void LlenarInfo()
        {
            string where = " WHERE Area = 'EDUC' ";

            where = where + " AND AÑO = " + txtanio.Text + " ";


            where = where + " AND  Actividad = '" + ddlactividad.SelectedItem.Text + "' ";


        sql = "SELECT Actividad, Año, SUM(CASE WHEN Mes = 1 THEN Total ELSE 0 END) AS Ene, SUM(CASE WHEN Mes = 2 THEN Total ELSE 0 END) AS Feb, SUM(CASE WHEN Mes = 3 THEN Total ELSE 0 END) AS Mar, SUM(CASE WHEN Mes = 4 THEN Total ELSE 0 END) AS Abr, SUM(CASE WHEN Mes = 5 THEN Total ELSE 0 END) AS May, SUM(CASE WHEN Mes = 6 THEN Total ELSE 0 END) AS Jun, SUM(CASE WHEN Mes = 7 THEN Total ELSE 0 END) AS Jul, SUM(CASE WHEN Mes = 8 THEN Total ELSE 0 END) AS Ago, SUM(CASE WHEN Mes = 9 THEN Total ELSE 0 END) AS Sep, SUM(CASE WHEN Mes = 10 THEN Total ELSE 0 END) AS Oct, SUM(CASE WHEN Mes = 11 THEN Total ELSE 0 END) AS Nov, SUM(CASE WHEN Mes = 12 THEN Total ELSE 0 END) AS Dic, SUM(Total) AS Total FROM dbo.fn_MISC_ActividadesPorMiembro('" + S + "') " + where +"GROUP BY Actividad, Año ";
            llenargrid(sql);
        }
        private void llenargrid(string sql)
        {
            try
            {
                DataTable tabledata = new DataTable();
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
        private void bdCombo(string sql, DropDownList ddl, string code, string desc)
        {
            ddl.Enabled = true;
            ddl.Items.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddl.DataSource = datos;
            ddl.DataValueField = code;
            ddl.DataTextField = desc;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddl.SelectedIndex = 0;
        }
    }
}