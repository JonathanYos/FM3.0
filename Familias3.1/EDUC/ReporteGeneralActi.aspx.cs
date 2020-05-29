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
    public partial class ReporteGeneralActi : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                mst = (mast)Master;
                BDM = new BDMiembro();
                APD = new BDAPAD();
                try
                {
                    // ValoresIniciales();
                    lnkb1.Text= "<span class='fa fa-trash'> <p>"+dic.eliminar+"</p></span>";
                    lnkb2.Text = "<span class='fa fa-check'> <p>"+dic.aceptar+"</p></span>";
                    lnkb3.Text = "<span class='fa fa-save'> <p>"+dic.guardar+"</p></span>";
                    lnkb4.Text = "<span class='fa fa-edit'> <p>"+dic.ModificarAPAD+"</p></span>";
                    lnkb5.Text = "<span class='fa fa-trash'> <p>" + dic.eliminar + "</p></span>";
                    lnkb6.Text = "<span class='fa fa-check'> <p>" + dic.aceptar + "</p></span>"; 
                    lnkb7.Text = "<span class='fa fa-save'> <p>"+dic.guardar+"</p></span>";
                    lnkb8.Text = "<span class='fa fa-edit'> <p>"+dic.ModificarAPAD+"</p></span>";
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }
        }

        protected void btngenerar_Click(object sender, EventArgs e)
        {
            if (VerificarFechas())
            {
                mst.mostrarMsjNtf("Este reporte puede tomar unos minutos");
                llenarlistado();
            }
            else
            {
                mst.mostrarMsjAdvNtf("Ha olvidado llenar una fecha o no ha seleccionado una categoria");
            }

        }
        protected void llenarlistado()
        {
            DateTime txta, txtd,año;
            txta = Convert.ToDateTime(txbafecha.Text);
            txtd = Convert.ToDateTime(txtdefecha.Text);
            string de = "CONVERT(datetime, CONVERT(varchar, '" + txtd.Year.ToString() + "/" + txtd.Month.ToString() + "/" + txtd.Day.ToString() + "', 111)) ";
            string a = "CONVERT(datetime, CONVERT(varchar, '" + txta.Year.ToString() + "/" + txta.Month.ToString() + "/" + txta.Day.ToString() + "', 111))";
            año = DateTime.Now;
            string where = "WHERE _fecha BETWEEN  " + de + " AND " + a + " ";

            if (ddlactividad.SelectedIndex == 0)
            {
                where = where + " AND FunctionalArea = 'EDUC' ";
            }
            else
            {
                where = where + "AND Actividad = '" + ddlactividad.SelectedItem.Text + "' ";
            }
            sql = "SELECT  Miembro, Nombre, Familia, TipoMiembro, Edad, Semaforo Semáforo, Grado, EstadoEduc, Escuela, NivelEduc, Actividad, Fecha_Asistencia, convert(nvarchar(20), HORA) + ':' + convert(nvarchar(20), MINUTO) Horario, Notes Observaciones, Usuario, Año, nMes, semanaMes FROM dbo.fn_GEN_ActividadesList('" + S + "', " + año.Year.ToString() + ") " + where + " ORDER BY Actividad, Año , Mes , dia , semanaMes ";
            llenargrid(sql);
            
            lbltotal.Text = "TOTAL: "+gvhistorial.Rows.Count.ToString();
        }
        protected bool VerificarFechas()
        {
            return txtdefecha.Text.Length < 10 || txbafecha.Text.Length < 10 ? false : true;
        }

        protected void ValoresIniciales()
        {
            sql = "SELECT Code, DescSpanish FROM dbo.CdMemberActivityType WHERE Active = 1 AND Project IN ('*', '" + S + "')  AND FunctionalArea = 'EDUC' ORDER BY DescSpanish ";
            bdCombo(sql, ddlactividad, "Code", "DescSpanish");
            lblactividad.Text = dic.actividad;
            lblfechade.Text = dic.DeFechaAPAD;
            lblfechaa.Text = dic.AFechaAPAD;
            btngenerar.Text = dic.generar;
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