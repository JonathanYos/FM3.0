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
    public partial class ActividadesDuplicadas : System.Web.UI.Page
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
            //       APD = new BDAPAD();
            dic = new Diccionario(L, S);
            if (!IsPostBack)
            {
                mst = (mast)Master;
                APD = new BDAPAD();
                try
                {
                    ValoresIniciales(); 
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }
        }
        protected void ValoresIniciales()
        {
            lblactividad.Text = dic.actividades;
            string where;

            if (S == "F")
            {
                where = "WHERE Active = 1 AND (Code LIKE 'CAL%') AND Project IN ('*', '" + S + "')  AND FunctionalArea ='EDUC' AND CodeInt IS NULL ";
            }
            else
            {
                where = "WHERE Active = 1 AND  Project IN ('*', '" + S + "')  AND FunctionalArea ='EDUC' AND CodeInt IS NULL ";
            }
            sql = "SELECT Code, DescSpanish FROM dbo.CdMemberActivityType " + where + " ORDER BY DescSpanish";
            bdCombo(sql, ddlactividad, "Code", "DescSpanish");
        }

        protected void ddlactividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where;
            if (S == "R" || S == "M")
            {
                where = "WHERE Area IN ('EDUC', 'MISC')  AND AÑO > 2012  ";
            }
            else if (S == "F")
            {
                where = "WHERE Area = 'EDUC' AND AÑO > 2013  AND Actividad LIKE '%califica%'  ";
            }
            else
            {
                where = "WHERE Area = 'EDUC' AND AÑO > 2012 ";
            }
            if (ddlactividad.SelectedIndex == 0)
            {
                gvhistorial.Columns.Clear();
                gvhistorial.DataSource = null;
                gvhistorial.DataBind();
                lbltotal.Text = "Total:" + gvhistorial.Rows.Count.ToString();
            }
            else
            {
                where = where + " AND  Actividad = '" + ddlactividad.SelectedItem.Text + "' ";
            }
            sql = "SELECT Miembro, Nombre, Familia, Estado_Afil, Actividad, Año FROM dbo.fn_MISC_ActivitiesList('" + S + "')  " + where + " GROUP BY Miembro, Nombre, Familia, Estado_Afil, Actividad, Año HAVING COUNT(*) > 1 ORDER BY Miembro, Año ";
            llenargrid(sql, gvhistorial);
            lbltotal.Text = "Total:" + gvhistorial.Rows.Count.ToString();
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
        private void llenargrid(string sql, GridView gv)
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
                gv.DataSource = tabledata;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }
    }
}
