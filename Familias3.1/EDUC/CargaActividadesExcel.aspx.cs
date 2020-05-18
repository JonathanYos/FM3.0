using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.EDUC
{
    public partial class CargaActividadesExcel : System.Web.UI.Page
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
        protected static DataTable dtAsistenciasAux;
        protected static DataTable dtAsistencias;
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
                dtAsistenciasAux = new DataTable();
                dtAsistencias = new DataTable();
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
            txtnotas.Attributes.Add("maxlength", "120");
            txtdup.Enabled = false;
            sql = "SELECT Code, DescSpanish FROM dbo.CdMemberActivityType WHERE Active = 1 AND Project IN ('*', '" + S + "')  AND FunctionalArea = 'EDUC' ORDER BY DescSpanish ";
            bdCombo(sql, ddlactividad, "Code", "DescSpanish");
            Parte1();
            Traducir();
        }
        protected void Parte1()
        {
            btnregresar.Visible = false;
            txtdup.Text = "";
            txtdup.Visible = false;
            lbldup.Visible = false;
            lblfecha.Visible = false;
            lblhora.Visible = false;
            lblnotas.Visible = false;
            lblregistroca.Visible = false;
            lblvresgistroca.Visible = false;
            txtnotas.Visible = false;
            txtnotas.Text = "";
            txbafecha.Text = "";
            txbafecha.Visible = false;
            txthora.Visible = false;
            fluarchivo.Visible = true;
            btnguardarpartedos.Visible = false;
            btnguardar.Visible = true;
        }
        protected void Parte2()
        {
            btnregresar.Visible = true;
            lblhora.Visible = false;
            txtdup.Visible = true;
            lbldup.Visible = true;
            lblfecha.Visible = true;
            lblnotas.Visible = true;
            lblregistroca.Visible = true;
            lblvresgistroca.Visible = true;
            txtnotas.Visible = true;
            txbafecha.Visible = true;
            fluarchivo.Visible = false;
            txthora.Visible = true;
            btnguardarpartedos.Visible = true;
            btnguardar.Visible = false;
        }
        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if (ddlactividad.SelectedIndex == 0 || !fluarchivo.HasFile)
            {
                mst.mostrarMsjAdvNtf("Ingrese una actividad");
            }
            else
            {
                if (!Convert.IsDBNull(fluarchivo.PostedFile) &
                        fluarchivo.PostedFile.ContentLength > 0)
                {
                    fluarchivo.SaveAs(Server.MapPath(".") + "\\" + fluarchivo.FileName);

                    OleDbConnection myExcelConn = new OleDbConnection
                        ("Provider=Microsoft.ACE.OLEDB.12.0; " +
                            "Data Source=" + Server.MapPath(".") + "\\" + fluarchivo.FileName +
                            ";Extended Properties=Excel 12.0;");
                    try
                    {
                        string miembro, nombre, tipoafil, nombrepuesto;

                        myExcelConn.Open();

                        OleDbCommand objOleDB = new OleDbCommand("SELECT *FROM [Asistencia$]", myExcelConn);

                        OleDbDataReader objBulkReader = null;
                        objBulkReader = objOleDB.ExecuteReader();
                        dtAsistenciasAux.Load(objBulkReader);

                        dtAsistencias.Columns.Add(dic.miembro);
                        dtAsistencias.Columns.Add("Nombre (Opcional)");
                        dtAsistencias.Columns.Add(dic.nombre);
                        dtAsistencias.Columns.Add(dic.afilTIpo);
                        foreach (DataRow rowAsistencia in dtAsistenciasAux.Rows)
                        {

                            miembro = rowAsistencia[0].ToString();
                            nombrepuesto = rowAsistencia[1].ToString();
                            if (miembro == "Miembro" || miembro == "miembro" || string.IsNullOrEmpty(miembro))
                            {

                            }
                            else
                            {
                                if (VericarApadrinado(miembro))
                                {
                                    nombre = nomiembro(miembro);
                                    tipoafil = nombremiem(miembro);
                                    if (string.IsNullOrEmpty(nombrepuesto))
                                    {
                                        nombrepuesto = "";
                                    }
                                }
                                else
                                {
                                    nombrepuesto = "MIEMBRO_NO_EXISTE";
                                    nombre = " ";
                                    tipoafil = " ";
                                }
                                dtAsistencias.Rows.Add(miembro, nombrepuesto, nombre, tipoafil);
                            }
                        }
                        if (dtAsistencias.Rows.Count > 0)
                        {
                            GridView1.DataSource = dtAsistencias;
                            GridView1.DataBind();
                        }
                        Parte2();
                        DateTime now = DateTime.Now;
                        txbafecha.Text = now.ToString("MM/dd/yyyy");
                        txthora.Text = now.ToString("HH:mm");
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf("Ha ocurrido un error por fabor enviar a sistemas lo siguiente: " + ex.Message);
                    }
                    finally
                    {
                        myExcelConn.Close(); myExcelConn = null;
                    }
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
                mst.mostrarMsjAdvNtf("Error" + ex.Message);
                string temp = "||||||";
                return temp;
            }

        }
        protected string nomiembro(string miem)
        {
            sql = "SELECT FirstNames+' '+LastNames nombre FROM dbo.Member WHERE RecordStatus=' ' AND MemberId='" + miem + "' AND Project='" + S + "'";
            return obtienePalabra(sql, "nombre");
        }
        protected string nombremiem(string miem)
        {
            sql = "SELECT dbo.fn_GEN_tipoMiembro('" + S + "', '" + miem + "') TipoMiembro";
            return obtienePalabra(sql, "TipoMiembro");
        }
        protected bool VericarApadrinado(string miem)
        {
            sql = "SELECT COUNT(*) conteo FROM dbo.Member WHERE RecordStatus=' ' AND MemberId='" + miem + "' AND Project='" + S + "'";
            int ver = ObtenerEntero(sql, "conteo");
            if (ver > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void Traducir()
        {
            btnregresar.Text = dic.regresar;
            lblhora.Text = "Hora";
            lbldup.Text = "Registros Duplicados";
            lblactividades.Text = dic.actividades;
            btnguardar.Text = dic.guardar;
            lblfecha.Text = dic.fecha;
            lblregistroca.Text = "Registros Cargados";
            lblnotas.Text = dic.notas;
            btnguardarpartedos.Text = dic.guardar;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string palabra = "";
            int conteodups = 0;
            int conteoregistors = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.accion;
                e.Row.Cells[1].Text = dic.miembro;
                e.Row.Cells[3].Text = dic.nombreMiembro;
                e.Row.Cells[4].Text = dic.afilTIpo;
            }
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gvr.FindControl("chkselect");
                string miembro = gvr.Cells[2].Text;
                string mmm = gvr.Cells[1].Text;
                int conteo = 0;
                foreach (GridViewRow gvr2 in GridView1.Rows)
                {
                    string uso = gvr2.Cells[1].Text;
                    if (uso == mmm)
                    {
                        conteo++;
                    }
                }
                if (conteo > 1)
                {
                    gvr.CssClass = "dcolor";
                    palabra = palabra + mmm + ",";
                    conteodups++;
                }
                if (miembro == "MIEMBRO_NO_EXISTE")
                {
                    chk.Checked = false;
                    chk.Enabled = false;
                    gvr.CssClass = "icolor";
                    conteoregistors--;
                }
                else
                {
                    chk.Checked = true;
                }
                conteoregistors++;
            }
            if (conteodups == 1)
            {
                palabra = palabra.Replace(",", "");
            }
            lblvresgistroca.Text = conteoregistors.ToString();
            txtdup.Text = palabra;
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

        protected void btnguardarpartedos_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
            {
                mst.mostrarMsjAdvNtf("No tiene registros");
            }
            else
            { int conteo = 0;
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)gvr.FindControl("chkselect");
                    string miembro = gvr.Cells[2].Text;
                    string mmm = gvr.Cells[1].Text;
                        if (chk.Checked == true)
                        {
                            conteo++;
                        }
                }
                if(conteo==0){ mst.mostrarMsjAdvNtf("No ha seleccionado ningun miembro"); } else
                {
                    int registros = 0;
                    foreach (GridViewRow gvr in GridView1.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)gvr.FindControl("chkselect");
                        string miembro = gvr.Cells[2].Text;
                        string mmm = gvr.Cells[1].Text;
                        string fecha = txbafecha.Text + " " + txthora.Text+":00";
                        if (chk.Checked == true)
                        {
                            IngresarActividad(mmm, fecha);
                            registros++;
                        }
                    }
                    mst.mostrarMsjNtf("Se realizaron "+ registros+ " ingresos.");
                }
            }
        }

        protected void btnregresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CargaActividadesExcel.aspx");
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
        protected void IngresarActividad(string member, string activityDT)
        {
            sql = "INSERT INTO  dbo.MemberActivity VALUES ('" + S + "', " + member + ", '" + ddlactividad.SelectedValue + "', '" + activityDT + "', GETDATE(), ' ', '" + U + "', NULL, '" + txtnotas.Text + "')";
             APD.ejecutarSQL(sql);
        }
    }
}