using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.EDUC
{
    public partial class RegistroActividadesInd : System.Web.UI.Page
    {
        protected bd.BDMiembro BDM;
        protected Diccionario dic;
        protected static BDAPAD APD;
        protected static BDFamilia BDF;
        protected static String S;
        protected static String L;
        protected static String F;
        protected static mast mst;
        protected static String M;
        protected static String U;
        protected static string sql;
        protected static string member, fecha2;
        protected static int accion;
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
                    ValoresIniciales();
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }
        }
        protected void accionar(object sender, EventArgs e)
        {
            if (accion == 1)
            {
                tbinfo.Visible = false;
                limpiar();
                DateTime d = DateTime.Now;
                DateTime Now = Convert.ToDateTime(txtfechav.Text);
                string miembro = member;
                String fechaHora = Now.Year.ToString() + "/" + Now.Month.ToString() + "/" + Now.Day.ToString() + " " + d.ToLongTimeString();
                sql = "INSERT INTO  dbo.MemberActivity VALUES ('" + S + "', " + miembro + ", '" + ddltipo.SelectedValue + "', '" + Convert.ToDateTime(fechaHora).ToString("MM/dd/yyyy hh:mm:ss") + "', GETDATE(), ' ', '" + U + "', NULL, '" + txtobservaciones.Text + "')";
                //Response.Write(sql);
                APD.ejecutarSQL(sql);
                mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                LLenarhistorial();
                
            }
            else if (accion == 2)
            {

                sql = "UPDATE dbo.MemberActivity SET RecordStatus = 'H', ExpirationDateTime = GETDATE() WHERE RecordStatus = ''  AND Project = '" + S + "' AND MemberId = " + member + " AND convert(varchar, ActivityDateTime, 120) = convert(varchar, '" + fecha2 + "', 120) ";
                APD.ejecutarSQL(sql);
                LLenarhistorial();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
            }
        }
       
        protected void limpiar()
        {
            txtfamilia.Text = "";
            txtmiembro.Text = "";
            txtobservaciones.Text = "";
            txtfaro.Text = "";
        }
        protected void limpiarcombo(GridView gv)
        {
            gv.Columns.Clear();
            gv.DataSource = null;
            gv.DataBind();
        }
        private void llenarcombo(string sql, DropDownList ddl, string cod, string desc)
        {
            ddl.Enabled = true;
            ddl.Items.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddl.DataSource = datos;
            ddl.DataValueField = cod;
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
        protected void traductor()
        {
            DateTime hoy = DateTime.Now;
            lblfamilia.Text = dic.familia;
            lblfaro.Text = dic.idFaro;
            lblfecha.Text = dic.fecha;
            lblmiembro.Text = dic.miembro;
            lblobservaciones.Text = dic.observaciones;
            lbltipo.Text = dic.tipo;
            chkduplicados.Text = " Duplicados";
            chkinfogen.Text = " " + dic.infoGeneral;
            txtfechav.Text = hoy.ToString("yyyy-MM-dd");
            btningresar.Text = dic.ingresar;
            sql = "SELECT MAP.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, dbo.fn_GEN_CalcularEdad(M.BirthDate) AS Edad, dbo.fn_GEN_tipoMiembro(M.Project, M.MemberId) TipoMiembro,  convert(varchar(50), MAP.ActivityDateTime, 120) 'Fecha/Hora', cdMAT.DescSpanish AS Asistencia, MAP.UserId AS Usuario, MAP.Notes as Observaciones, M.LastFamilyId Familia  FROM dbo.MemberActivity MAP INNER JOIN dbo.Member M ON MAP.Project = M.Project AND MAP.MemberId = M.MemberId AND MAP.RecordStatus = M.RecordStatus INNER JOIN dbo.CdMemberActivityType cdMAT ON MAP.Type = cdMAT.Code WHERE MAP.RecordStatus = ' ' AND MAP.Project = '" + S + "' AND YEAR(MAP.ActivityDateTime) = " + hoy.Year.ToString() + " AND MONTH(MAP.ActivityDateTime) = " + hoy.Month.ToString() + " AND DAY(MAP.ActivityDateTime) = " + hoy.Day.ToString() + "  AND cdMAT.DescSpanish = '" + ddltipo.SelectedItem.Text + "'ORDER BY MAP.ActivityDateTime DESC ";
            llenargrid(sql, gvhistorial);
        }
        protected void ValoresIniciales()
        {
            txtobservaciones.Attributes.Add("maxlength", "120");
            sql = "SELECT Code, DescSpanish FROM dbo.CdMemberActivityType WHERE Active = 1 AND Project IN ('*', '" + S + "')  AND FunctionalArea = 'EDUC' ORDER BY DescSpanish ";
            llenarcombo(sql, ddltipo, "Code", "DescSpanish");
            lblfaro.Visible = false;
            txtfaro.Visible = false;
            if (S == "R") { lblfaro.Visible = true; txtfaro.Visible = true; }
            traductor();
        }

        protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltipo.SelectedIndex == 0)
            {
                limpiarcombo(gvhistorial);
            }
            else
            {
                LLenarhistorial();
            }
        }
        protected void LLenarhistorial()
        {
            if (string.IsNullOrEmpty(txtfechav.Text))
            {
                mst.mostrarMsjAdvNtf("Debe llenar la fecha");
            }
            else
            {
                DateTime hoy = Convert.ToDateTime(txtfechav.Text);
                sql = "SELECT MAP.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, dbo.fn_GEN_CalcularEdad(M.BirthDate) AS Edad, dbo.fn_GEN_tipoMiembro(M.Project, M.MemberId) TipoMiembro,  convert(varchar(50), MAP.ActivityDateTime, 120) 'Fecha/Hora', cdMAT.DescSpanish AS Asistencia, MAP.UserId AS Usuario, MAP.Notes as Observaciones, M.LastFamilyId Familia  FROM dbo.MemberActivity MAP INNER JOIN dbo.Member M ON MAP.Project = M.Project AND MAP.MemberId = M.MemberId AND MAP.RecordStatus = M.RecordStatus INNER JOIN dbo.CdMemberActivityType cdMAT ON MAP.Type = cdMAT.Code WHERE MAP.RecordStatus = ' ' AND MAP.Project = '" + S + "' AND YEAR(MAP.ActivityDateTime) = " + hoy.Year.ToString() + " AND MONTH(MAP.ActivityDateTime) = " + hoy.Month.ToString() + " AND DAY(MAP.ActivityDateTime) = " + hoy.Day.ToString() + "  AND cdMAT.DescSpanish = '" + ddltipo.SelectedItem.Text + "'ORDER BY MAP.ActivityDateTime DESC ";
                llenargrid(sql, gvhistorial);
            }

        }

        protected void llenarfamilia(string familia)
        {
            familia = familia.Replace(" ", "");
            if (VerficarFamilia(familia) == false)
            {
                mst.mostrarMsjAdvNtf(dic.MsjFamilianoAfiliada);
            }
            else
            {
                mst.mostrarModalYContenido(pnlfamilia);
                sql = "SELECT FMR.MemberId AS Miembro, M.FirstNames AS Nombres, M.LastNames AS Apellidos, dbo.fn_GEN_FormatDate(M.BirthDate, 'ES') AS Nacimiento, dbo.fn_GEN_CalcularEdad(M.BirthDate) AS Edad, cdFMR.DescSpanish AS Relación_Familiar, cdAS.DescSpanish AS Estado_Afil, dbo.fn_GEN_tipoMiembro(M.Project, M.MemberId) TipoMiembro, HeadOfHouse FROM dbo.FamilyMemberRelation FMR INNER JOIN dbo.CdFamilyMemberRelationType cdFMR ON FMR.Type = cdFMR.Code INNER JOIN dbo.Member M ON FMR.Project = M.Project AND FMR.MemberId = M.MemberId AND FMR.RecordStatus = M.RecordStatus LEFT JOIN dbo.CdAffiliationStatus cdAS ON cdAS.Code = M.AffiliationStatus WHERE FMR.RecordStatus = ' ' AND FMR.Project = '" + S + "' AND FMR.FamilyId = " + familia + " AND FMR.InactiveDate IS NULL ORDER BY cdFMR.DisplayOrder ";
                llenargrid(sql, gvfamilia);
            }
        }
        protected Boolean VerficarFamilia(String familia)
        {
            sql = "SELECT COUNT(*) conteo FROM dbo.Member WHERE LastFamilyId='" + familia + "' AND RecordStatus=' ' AND Project='" + S + "' AND AffiliationStatus='AFIL'";
            int temp2 = ObtenerEntero(sql, "conteo");
            if (temp2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                return -1;
            }
        }

        protected void btningresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfamilia.Text) && string.IsNullOrEmpty(txtmiembro.Text) && string.IsNullOrEmpty(txtfaro.Text))
            {
                mst.mostrarMsjAdvNtf("Solo debe llenar un campo");
            }
            else if (txtfaro.Text.Length > 0 && txtmiembro.Text.Length > 0 && txtfamilia.Text.Length > 0)
            {
                mst.mostrarMsjAdvNtf("Debe llenar solamente un campo");
            }
            else if ((txtfaro.Text.Length > 0 && txtmiembro.Text.Length > 0) || (txtfaro.Text.Length > 0 && txtfamilia.Text.Length > 0))
            {
                mst.mostrarMsjAdvNtf("Solo debe llenar un campo");
            }
            else if (txtfaro.Text.Length > 0 || txtmiembro.Text.Length > 0 || txtfamilia.Text.Length > 0)
            {
                if (txtfaro.Text.Length > 0)
                {
                    VerificarFaro();
                }
                else if (txtmiembro.Text.Length > 0)
                {
                    VerificarMiembro(txtmiembro.Text);
                }
                else if (txtfamilia.Text.Length > 0)
                {
                    llenarfamilia(txtfamilia.Text);
                }
            }

        }

        protected void gvfamilia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var clickedButton = e.CommandSource as Button;
            var clickedRow = clickedButton.NamingContainer as GridViewRow;
            string miembro = clickedRow.Cells[1].Text;
            mst.ocultarModalYContenido(pnlfamilia);
            VerificarMiembro(miembro);

        }
        protected void VerificarMiembro(string miembro)
        {
            string sql3 = "SELECT COUNT(*) conteo FROM dbo.Member WHERE MemberId='" + miembro + "' AND RecordStatus=' ' AND Project='" + S + "' AND AffiliationType='BECA' AND AffiliationStatus='AFIL' OR MemberId='" + miembro + "' AND RecordStatus=' ' AND Project='" + S + "' AND AffiliationType='NORM' AND AffiliationStatus='AFIL' ";
            int temp2 = ObtenerEntero(sql3, "conteo");
            if (temp2 <= 0)
            {
                mst.mostrarMsjAdvNtf(dic.MsjNohaSeleccionadomiembro);
            }
            else
            {
                IngresodeActividad(1, miembro);
            }
        }
        protected void IngresodeActividad(int correlativo, string miembro)
        {
            if (correlativo == 1)
            {
                if (TodoCorrecto() == true)
                {
                    DateTime Now = Convert.ToDateTime(txtfechav.Text);
                    int conteo = 0;
                    string mensaje = "Se ha encontrado lo siguiente: ";
                    if (FechaAnterior() == true)
                    {
                        mensaje = mensaje + "Esta fecha es anterior a la actual, ";
                        conteo++;
                    }
                    sql = "SELECT COUNT(*) AS Total FROM dbo.MemberActivity WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + miembro + " AND Type = '" + ddltipo.SelectedValue + "'  AND YEAR(ActivityDateTime) = " + Now.Year.ToString() + " AND MONTH(ActivityDateTime) = " + Now.Month.ToString() + " AND DAY(ActivityDateTime) = " + Now.Day.ToString();
                    if (ObtenerEntero(sql, "Total") > 0)
                    {
                        mensaje = mensaje + "Tiene un registro duplicado";
                        conteo++;
                    }
                    switch (conteo)
                    {
                        case 1:
                            mensaje = mensaje.Replace(",", "");
                            mensaje = mensaje + ". ¿Desea Continuar?";
                            break;
                        case 2:
                            mensaje = mensaje + ". ¿Desea Continuar?";
                            break;
                        case 0:
                            mensaje = "¿Esta seguro de ingresar?";
                            break;
                    }
                    if (chkinfogen.Checked == true)
                    {
                        Now = DateTime.Now;
                        DataTable dt = new DataTable();
                        sql = "SELECT M.FirstNames + ' ' + M.LastNames Nombre, E1.EstadoEduc + ' ' + E1.Grado  + '  ' +   E1.Escuela AS Educ, dbo.fn_GEN_tipoMiembro(M.Project, M.MemberId) TipoMiembro, dbo.fn_GEN_CalcularEdad(M.BirthDate) AS Edad FROM dbo.Member M LEFT OUTER JOIN dbo.fn_EDUC_añoEscolar(" + Now.Year.ToString() + ") E1 ON E1.Project = M.Project AND E1.MemberId = M.MemberId WHERE M.RecordStatus = ' ' AND M.Project = '" + S + "' AND M.MemberId = " + miembro;
                        LlenarDataTable(sql, dt);
                        tbinfo.Visible = true;
                        lblnombre.Text = dt.Rows[0]["Nombre"].ToString();
                        lbledad.Text = dt.Rows[0]["Edad"].ToString();
                        lbltipodemiem.Text = "(" + dt.Rows[0]["TipoMiembro"] + ")";
                        lbleduc.Text = dt.Rows[0]["Educ"].ToString();
                        dt.Clear();
                    }
                    accion = 1;
                    member = miembro;
                    mst.mostrarMsjOpcionesMdl(mensaje);
                }
            }
            else
            {

            }
        }
        protected Boolean FechaAnterior()
        {
            DateTime hoy = DateTime.Now;
            DateTime fecha = Convert.ToDateTime(txtfechav.Text);
            sql = "SELECT DATEDIFF(year, '" + fecha.ToShortDateString() + "', '" + hoy.ToShortDateString() + "') AS res;";
            int intento = ObtenerEntero(sql, "res");
            if (intento < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected Boolean TodoCorrecto()
        {
            int conteoErrores = 0;
            DateTime hoy = DateTime.Now;
            DateTime fecha = Convert.ToDateTime(txtfechav.Text);
            sql = "SELECT DATEDIFF(year, '" + fecha.ToShortDateString() + "', '" + hoy.ToShortDateString() + "') AS res;";
            int intento = ObtenerEntero(sql, "res");
            if (intento > 0)
            {
                mst.mostrarMsjAdvNtf("La fecha no puede ser mayor a la actual");
                conteoErrores++;
            }
            if (ddltipo.SelectedIndex == 0)
            {
                mst.mostrarMsjAdvNtf("No ha seleccionado una actividad");
                conteoErrores++;
            }
            if (conteoErrores > 0) { return false; } else { return true; }
        }

        protected void chkduplicados_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gv in gvhistorial.Rows)
            {
                string miembro = gv.Cells[1].Text;
                DateTime Now = Convert.ToDateTime(txtfechav.Text);
                if (gvhistorial.Rows.Count == 0)
                {

                }
                else
                {
                    if (chkduplicados.Checked == true)
                    {
                        sql = "SELECT COUNT(*) AS Total FROM dbo.MemberActivity WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + miembro + " AND Type = '" + ddltipo.SelectedValue + "'  AND YEAR(ActivityDateTime) = " + Now.Year.ToString() + " AND MONTH(ActivityDateTime) = " + Now.Month.ToString() + " AND DAY(ActivityDateTime) = " + Now.Day.ToString();
                        if (ObtenerEntero(sql, "Total") > 1)
                        {
                            gv.CssClass = "rcolor";
                        }
                    }
                    else
                    {
                        gv.CssClass = "wcolor";
                    }

                }

            }
        }

        protected void txtfechav_TextChanged(object sender, EventArgs e)
        {
            if (txtfechav.Text.Length <= 9 || ddltipo.SelectedIndex == 0)
            {

            }
            else
            {
                LLenarhistorial();
            }
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var clickedButton = e.CommandSource as Button;
            var clickedRow = clickedButton.NamingContainer as GridViewRow;
            string miembro = clickedRow.Cells[1].Text;
            string fecha = clickedRow.Cells[5].Text;
            member = miembro;
            fecha2 = fecha;
            accion = 2;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
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

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistorial.Rows)
            {
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btndelim");
                btn.Text = dic.eliminar;
            }
        }

        protected void gvfamilia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistorial.Rows)
            {
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btndelim");
                btn.Text = dic.seleccionar;
            }
        }

        protected void VerificarFaro()
        {
            string faro = txtfaro.Text;
            sql = "SELECT COUNT(*) conteo FROM dbo.Family WHERE RecordStatus=' ' AND Project='" + S + "' AND RFaroNumber='" + faro + "' AND AffiliationStatus='AFIL'";
            int prueba = ObtenerEntero(sql, "conteo");
            if (prueba == 0)
            {
                mst.mostrarMsjAdvNtf("Numero de faro invalido");
            }
            else
            {
                sql = "SELECT FamilyId FROM dbo.Family WHERE RecordStatus=' ' AND Project='" + S + "' AND RFaroNumber='" + faro + "' AND AffiliationStatus='AFIL'";
                string fam = Convert.ToString(ObtenerEntero(sql, "FamilyId"));
                llenarfamilia(fam);
            }
        }
    }
}