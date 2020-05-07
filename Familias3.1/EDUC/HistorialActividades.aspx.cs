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
    public partial class HistorialActividades : System.Web.UI.Page
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
        protected static string sql,creat,tipo;
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
            mst.contentCallEvent += new EventHandler(accionar);
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
            Traducir();
            LlenarInfo();
            LlenarHistorial();
        }
        protected void accionar(object sender, EventArgs e)
        {
            sql = "UPDATE dbo.MemberActivity SET RecordStatus = 'H', ExpirationDateTime = GETDATE(), Notes = Notes + '" + U + "' WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND Convert(nvarchar(30), CreationDateTime, 21) = '" + creat + "' AND Type = '" + tipo + "'";
            APD.ejecutarSQL(sql);
            mst.mostrarMsjAdvNtf(dic.msjSeHaEliminado);
            LlenarHistorial();
        }
        protected void Traducir()
        {

            DateTime hoy = DateTime.Now;
            txtfechav.Text = hoy.ToString("yyyy-MM-dd");
            lblactivitit.Text = dic.actividades;
            txtobservaciones.Attributes.Add("maxlength", "120");
            lblactividad.Text = dic.actividad;
            lblanio.Text = dic.año;
            lblescuela.Text = dic.EscuelaAPAD;
            lblestado.Text = dic.estado;
            lblfamilia.Text = dic.familia;
            lblfecha.Text = dic.fecha;
            lblgrado.Text = dic.grado;
            lblnombre.Text = dic.nombre;
            lbltithis.Text = "Historial de Actividades";
            btningresar.Text = dic.ingresar;
            lblobservaciones.Text = dic.observaciones;
        }
        protected void LlenarInfo()
        {
            DateTime now = DateTime.Now;

            sql = "SELECT dbo.fn_BECA_actualizaInfoEduc('" + S + "', '" + U + "') Resul ";
            int n = ObtenerEntero(sql, "Resul");
            int anioactual = 0;
            if (n == 0)
            {
                tbregisto.Visible = false;
            }
            else
            {
                sql = "SELECT CASE WHEN MAX(SchoolYear) IS NULL THEN 0 ELSE MAX(SchoolYear) END  Ultimo FROM dbo.MemberEducationYear WHERE  RecordStatus = ' ' and Project = '" + S + "' AND Memberid = " + M;
                anioactual = ObtenerEntero(sql, "Ultimo");
            }
            if (anioactual == 0)
            {
                if (now.Year >= 10)
                {
                    anioactual = now.Year;
                }
                else
                {
                    anioactual = now.Year - 1;
                }
            }
            lblvanio.Text = anioactual.ToString();

            sql = "SELECT Code, DescSpanish actividad FROM dbo.CdMemberActivityType WHERE FunctionalArea = 'EDUC' AND Project IN ('*', '" + S + "') AND Active = 1 ";
            bdCombo(sql, ddlactividad, "Code", "actividad");

            DataTable listTable = new DataTable();

            sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + anioactual.ToString() + ") L ";
            LlenarDataTable(sql, listTable);

            lblvnombre.Text = listTable.Rows[0]["Nombres"].ToString() + " " + listTable.Rows[0]["Apellidos"].ToString();
            lblvfamilia.Text = listTable.Rows[0]["Familia"].ToString();

            if (EsApadrinado() == true)
            {
                lblvgrado.Text = listTable.Rows[0]["Grado"].ToString();
                lblvestado.Text = listTable.Rows[0]["Estado_Educ"].ToString();
                lblvescuela.Text = listTable.Rows[0]["Escuela"].ToString();
            }
        }
        protected bool EsApadrinado()
        {
            sql = "SELECT COUNT(*) AS Total FROM dbo.Member M WHERE RecordStatus = ' ' AND Project = '" + S + "' AND Memberid = " + M + " AND AffiliationStatus = 'AFIL'";
            if (ObtenerEntero(sql, "Total") > 0)
            {
                return true;
            }
            else
            {
                return false;
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
        protected void LlenarHistorial()
        {
            sql = "SELECT 'Borrar' '_', CONVERT(nvarchar(30), MA.CreationDateTime, 21) Crea, cdAT.Code Cod, dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, cdAT.DescSpanish AS Actividad, MA.Notes AS Observaciones, MA.UserId Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdAT ON MA.Type = cdAT.Code WHERE MA.RecordStatus = ' ' AND MA.Project = '" + S + "' AND cdAT.FunctionalArea = 'EDUC' AND MA.MemberId = " + M + " " + "ORDER BY MA.CreationDateTime DESC ";
            llenargrid(sql,gvhistorial);

            sql = "SELECT dbo.fn_BECA_actualizaInfoEduc('" + S + "', '" + U + "') Resul ";
            int n = ObtenerEntero(sql, "Resul");

            if (n > 1)
            {
                foreach (GridViewRow gvr in gvhistorial.Rows)
                {
                    Button btn2 = new Button();
                    btn2 = (Button)gvr.FindControl("btneliminar");
                    btn2.Text = dic.eliminar;
                }
            }
            else
            {
                foreach (GridViewRow gvr in gvhistorial.Rows)
                {
                    Button btn2 = new Button();
                    btn2 = (Button)gvr.FindControl("btneliminar");
                    btn2.Visible = false;
                }
            }
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
                Response.Write(sql+ex.Message);
            }
        }
        protected void btningresar_Click(object sender, EventArgs e)
        {
            if (ddlactividad.SelectedIndex == 0  || txtfechav.Text.Length<10)
            {
                mst.mostrarMsjAdvNtf("No ha llenado los campos necesarios");
            }
            else
            {
                DateTime d = DateTime.Now;
                DateTime Now = Convert.ToDateTime(txtfechav.Text);
                String fechaHora = Now.Year.ToString() + "/" + Now.Month.ToString() + "/" + Now.Day.ToString() + " " + d.ToLongTimeString();
                sql = "INSERT INTO dbo.MemberActivity VALUES ('" + S + "', " + M + ", '" + ddlactividad.SelectedValue + "', '" + Convert.ToDateTime(fechaHora).ToString("MM/dd/yyyy hh:mm:ss") + "', GETDATE(), ' ', '" + U + "', NULL, '" + txtobservaciones.Text + "')";
                APD.ejecutarSQL(sql);
                mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                LlenarHistorial();
                limpiar();
            }
        }
        protected void limpiar()
        {
            DateTime hoy = DateTime.Now;
            txtfechav.Text = hoy.ToString("yyyy-MM-dd");
            ddlactividad.SelectedIndex = 0;
            txtobservaciones.Text = "";
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var clickedButton = e.CommandSource as Button;
            var clickedRow = clickedButton.NamingContainer as GridViewRow;
            creat = clickedRow.Cells[0].Text;
            tipo = clickedRow.Cells[1].Text;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }
    }
}