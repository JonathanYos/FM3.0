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
    public partial class ResumenBecas : System.Web.UI.Page
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
        protected static String Site;
        protected static String Member;
        protected static string sql;
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
            if (!IsPostBack)
            {
                mst = (mast)Master;
                BDM = new BDMiembro();
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
            llenarcombos();
            Traducir();
        }
        protected void Traducir()
        {
            string porcentaje;
            if (L == "es") { porcentaje = "Porcentaje"; } else { porcentaje = "Percentage"; }
            sql = "SELECT FirstNames+' '+ LastNames Nombre FROM dbo.Member WHERE MemberId='" + M + "' AND Project='" + S + "'";
            lblnombre.Text = APD.obtienePalabra(sql, "Nombre");
            lblescuela.Text = dic.EscuelaAPAD;
            lblgrado.Text = dic.estado;
            lblestado.Text = dic.estado;
            lblexcestado.Text = dic.excepcionEstadoEducativo;
            lblcarrera.Text = dic.carrera;
            lblporcentaje.Text = porcentaje;
        }
        protected void llenarcombos()
        {
            sql = "SELECT Code, CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END 'Categoria' FROM dbo.CdMemberEducObservationCategory WHERE Active = 1 ORDER BY DescSpanish ";
            //bdCombo(sql, categoriaCombo, "Code", "Categoria");

            //lista activiades
            sql = "SELECT Code, CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END 'Actividad' FROM dbo.CdMemberActivityType WHERE Project IN ('" + S + "', '*') AND FunctionalArea = 'EDUC' AND Active = 1 AND NotesRequired = 0 ORDER BY DescSpanish ";
            //bdCombo(sql, , "Code", "Actividad");

            //lista grados
            sql = "SELECT Code,CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END 'Grado' FROM dbo.CdGrade WHERE ValidValue = 1 ORDER BY Orden";
            bdCombo(sql, ddlgrado, "Code", "Grado");

            //lista estados educativos
            sql = "SELECT Code, CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END Estado  FROM CdEducationStatus  WHERE ValidValue = 1 ";
            bdCombo(sql, ddlestado, "Code", "Estado");

            //lista excepciones de estados educativos
            sql = "SELECT Code,CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END Excep  FROM CdEducationReasonNotToContinue  WHERE Active = 1 ";
            bdCombo(sql, ddlexestado, "Code", "Excep");

            //lista escuelas
            sql = "SELECT Code, Name AS Escuela FROM dbo.School WHERE RecordStatus = ' ' AND Project = '" + S + "' AND (Active = 1) ORDER BY Name";
            bdCombo(sql, ddlescuela, "Code", "Escuela");

            //lista carreras
            sql = "SELECT Code,CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END carrera FROM dbo.CdEducationCareer WHERE  EducationLevel <> 'UNIV' ORDER BY DescSpanish";
            bdCombo(sql, ddlcarrera, "Code", "carrera");
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