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


namespace Familias3._1.APJO
{
    public partial class Referencias1 : System.Web.UI.Page
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
        protected static String Member;
        protected static String miembroselect;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public string sql;
        protected static string modfecha;
        protected static string mdcodigo;
        protected static string miembroreferido;
        protected static string nombrereferido;
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            mst = (mast)Master;
            dic = new Diccionario(L, S);
            mst.contentCallEvent += new EventHandler(accionar);
            if (!IsPostBack)
            {
                BDM = new BDMiembro();
                APD = new BDAPAD();
                dic = new Diccionario(L, S);
                try
                {
                    valoresiniciales();
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["tablaRef"] != null)
            {
                //VerificarDia("LU", "MATU");
            }
        }
        private void valoresiniciales()
        {
            llenarLabels();
            MostrarOpciones();
        }
        private void llenarLabels()
        {
            txtmod.Attributes.Add("maxlength", "497");
            btnmodicar.Text = dic.ModificarAPAD;
            btncancelar.Text = dic.cancelar;
            pnlfiltros.Visible = false;
            ddlsubprograma.Items.Clear();
            ddlsubprograma.Enabled = false;
            txtdescripcion.Attributes.Add("maxlength", "497");
            string subprograma;

            if (L == "es") { lbldescripcion.Text = "Mensaje de referente"; } else { lbldescripcion.Text = "Reference Message"; }
            lblfamilia.Text = dic.familia;
            btnbuscar.Text = dic.buscar;
            lbljornada.Text = "Jornada";
            lblprograma.Text = dic.programa;
            if (L == "es") subprograma = "Subprograma"; else subprograma = "SubProgram";
            lblsubprograma.Text = subprograma;
            btnbuscarfam.Text = "Buscar";
            lblNotasReferenciar.Text = "Notas de la persona de referencia";
            string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] codigo = { "LU", "MA", "MI", "JU", "VI", "SA", "DO" };

            for (int i = 0; i < 7; i++)
            {
                if (L == "es")
                {
                    ListItem s = new ListItem(dias[i], "" + codigo[i] + "");
                    cbldias.Items.Add(s);
                }
                else
                {
                    ListItem s = new ListItem(days[i], "" + codigo[i] + "");
                    cbldias.Items.Add(s);
                }

            }
            foreach (ListItem ad in cbldias.Items)
            {
                ad.Selected = true;
            }
            ddlsubprograma.Items.Clear();
            ddlsubprograma.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsubprograma.SelectedIndex = 0;
            ddlsubprograma.Enabled = false;
        }
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            tblEstadisticas.Visible = false;

            buscar();
            tblEstadisticas.Visible = true;
            //ClientScript.RegisterStartupScript(GetType(), "mostrarLigthbox", "MostrarLigthbox();", true);
            mst.ocultarModalYContenido(pnlModificar);
            mst.mostrarModalYContenido(pnlfiltros);
            pnlfiltros.Visible = true;
        }
        private void buscar()
        {
            pnlCalendario.Visible = true;
            int conteototal = 0;
            foreach (ListItem ad in cbldias.Items)
            {
                if (ad.Selected)
                {
                    conteototal++;
                }
            }

            string miembro = txtmiembro.Text;

            if (conteototal == 0 && ddljornada.SelectedIndex == 0)
            {

            }
            else
            {
                if (conteototal > 0 && ddljornada.SelectedIndex == 0)
                {
                    foreach (ListItem ad in cbldias.Items)
                    {
                        if (ad.Selected)
                        {
                            VerificarDia(ad.Value, "-1");
                        }
                    }
                }
                else
                {
                    if (ddljornada.SelectedIndex > 0 && conteototal == 0)
                    {
                    }
                    else
                    {
                        foreach (ListItem ad in cbldias.Items)
                        {
                            if (ad.Selected)
                            {
                                VerificarDia(ad.Value, ddljornada.SelectedValue);
                            }
                        }
                        Session["tablaRef"] = tblEstadisticas;
                    }
                }

            }

        }
        private void VerificarDia(String dia, String jornada)
        {

            LlenarTabla(dia, jornada);

        }
        private void LlenarTabla(String dia, String jornada)
        {
            string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string descripcionDia = "";
            switch (dia)
            {
                case "LU":
                    if (L == "es")
                    {
                        descripcionDia = dias[0].ToString();
                    }
                    else
                    {
                        descripcionDia = days[0].ToString();
                    }
                    break;
                case "MA":
                    if (L == "es")
                    {
                        descripcionDia = dias[1].ToString();
                    }
                    else
                    {
                        descripcionDia = days[1].ToString();
                    }
                    break;
                case "MI":
                    if (L == "es")
                    {
                        descripcionDia = dias[2].ToString();
                    }
                    else
                    {
                        descripcionDia = days[2].ToString();
                    }
                    break;
                case "JU":
                    if (L == "es")
                    {
                        descripcionDia = dias[3].ToString();
                    }
                    else
                    {
                        descripcionDia = days[3].ToString();
                    }
                    break;
                case "VI":
                    if (L == "es")
                    {
                        descripcionDia = dias[4].ToString();
                    }
                    else
                    {
                        descripcionDia = days[4].ToString();
                    }
                    break;

                case "SA":
                    if (L == "es")
                    {
                        descripcionDia = dias[5].ToString();
                    }
                    else
                    {
                        descripcionDia = days[5].ToString();
                    }
                    break;

                case "DO":
                    if (L == "es")
                    {
                        descripcionDia = dias[6].ToString();
                    }
                    else
                    {
                        descripcionDia = days[6].ToString();
                    }
                    break;
                default: break;
            }
            string jnd, program, program2, subprogram2, program3, subprogram;
            if (jornada == "-1")
            {
                jnd = "cdEAT.WorkDay";
            }
            else
            {
                jnd = "'" + jornada + "'";
            }
            if (ddlprograma.SelectedIndex == 0)
            {
                program = "cdSP.Program";
                program2 = "cdSP.Program";
                program3 = "cdP.Code";
            }
            else
            {
                program = "'" + ddlprograma.SelectedValue + "'";
                program2 = "'" + ddlprograma.SelectedValue + "'";
                program3 = "'" + ddlprograma.SelectedValue + "'";
            }
            if (ddlsubprograma.SelectedIndex == 0)
            {
                subprogram = "cdEAT.SubPrograma";
                subprogram2 = "cdSP.Code";
            }
            else
            {
                subprogram = "'" + ddlsubprograma.SelectedValue + "'";
                subprogram2 = "'" + ddlsubprograma.SelectedValue + "'";
            }
            string sql = "SELECT COUNT(*) AS nSubProgramas FROM CdSubProgram cdSP WHERE cdSP.Project='" + S + "' AND cdSP.Active = 1 AND cdSP.Program=" + program2 + " AND cdSP.Code=" + subprogram2 + " AND 0 < (SELECT COUNT(*) FROM CdMemberEducationActivityType CdEAT WHERE cdSP.Code = CdEAT.SubPrograma AND cdSP.Active = cdEAT.Active AND cdEAT.Day ='" + dia + "' AND cdEAT.WorkDay = " + jnd + ")";

            int rowsp = ObtenerEntero(sql, "nSubProgramas");

            if (rowsp > 0)
            {
                TableRow filaDia = new TableRow();
                TableCell celdaDia = new TableCell();
                DataTable dtProgramas = new DataTable();

                sql = @"SELECT cdP.Code, CASE WHEN '" + L + "'='es' THEN cdP.DescSpanish ELSE cdP.DescEnglish END descripcion FROM CdProgram cdP WHERE  cdP.Project = '" + S + @"' AND cdP.Active = 1 AND cdP.Code = " + program3 + " AND 0 < (SELECT COUNT(*) FROM CdSubProgram cdSP INNER JOIN CdMemberEducationActivityType cdEAT ON cdSp.Code = cdEAT.SubPrograma AND cdSP.Active = cdEAT.Active WHERE cdSP.Active = cdP.Active AND cdEAT.Day = '" + dia + @"' AND cdEAT.Project = cdP.Project AND cdEAT.Active = 1 AND cdEAT.WorkDay = " + jnd + " AND cdP.Code = cdSP.Program)";
                LlenarDataTable(sql, dtProgramas);
                int conteototal = dtProgramas.Rows.Count;
                celdaDia.RowSpan = rowsp;
                celdaDia.Text = "<b>" + descripcionDia + "</b>";
                celdaDia.CssClass = "";
                filaDia.Cells.Add(celdaDia);
                foreach (DataRow drPrograma in dtProgramas.Rows)
                {
                    TableCell celdaPrograma = new TableCell();
                    string codigoPrograma = drPrograma[0].ToString();
                    string descripcionPrograma = drPrograma[1].ToString();
                    DataTable dtSubProgramas = new DataTable();
                    sql = "SELECT cdSP.Code, CASE WHEN '" + L + "' = 'es' THEN cdSP.DescSpanish ELSE cdSP.DescEnglish END AS Des FROM CdSubProgram cdSP INNER JOIN CDProgram cdP ON cdSP.Program = cdP.Code AND cdSP.Active = cdP.Active AND cdP.Code = cdSP.Program WHERE cdSP.Project='" + S + "' AND cdSP.Active = 1 AND cdSP.Program='" + codigoPrograma + "' AND 0 < (SELECT COUNT(*) FROM CdMemberEducationActivityType CdEAT WHERE cdSp.Code = " + subprogram + " AND cdSP.Active = cdEAT.Active AND cdEAT.Day ='" + dia + "' AND cdEAT.WorkDay = " + jnd + " AND cdP.Code = cdSP.Program)";
                    LlenarDataTable(sql, dtSubProgramas);
                    rowsp = dtSubProgramas.Rows.Count;
                    celdaPrograma.RowSpan = rowsp;
                    celdaPrograma.Text = "<em>" + descripcionPrograma + "</em>";
                    filaDia.Cells.Add(celdaPrograma);
                    foreach (DataRow dr2 in dtSubProgramas.Rows)
                    {
                        TableCell celdaSubPrograma = new TableCell();
                        string codigoSubPrograma = dr2[0].ToString();
                        string descripcionSubPrograma = dr2[1].ToString();
                        celdaSubPrograma.Text = descripcionSubPrograma;
                        filaDia.Cells.Add(celdaSubPrograma);
                        TableCell celdaDdlActividades = new TableCell();
                        DropDownList ddlActividad = new DropDownList();
                        Button btnactividad = new Button();
                        DataTable dtActividades = new DataTable();

                        sql = "SELECT cdEAT.Code, CASE WHEN '" + L + "' = 'es' THEN cdEAT.DescSpanish +' ('+ cdEAT.Schedule+')' ELSE cdEAT.DescEnglish+' ('+ cdEAT.Schedule+')' END Des FROM CdMemberEducationActivityType cdEAT INNER JOIN CdSubProgram cdSP ON cdEAT.SubPrograma= cdSP.Code AND cdEAT.Active = cdSP.Active AND cdSP.Code = '" + codigoSubPrograma + "' INNER JOIN CdProgram cdP ON cdSP.Program = cdP.Code AND cdSP.Active = cdP.Active AND cdP.Project = cdEAT.Project AND cdP.Code = cdSP.Program WHERE cdEAT.Active = 1 AND cdEAT.Project = '" + S + "' AND cdEAT.WorkDay = " + jnd + " AND cdEAT.Day = '" + dia + "' ORDER BY CASE WHEN '" + L + "' = 'es' THEN cdEAT.DescSpanish ELSE cdEAT.DescEnglish END";

                        LlenarDataTable(sql, dtActividades);

                        ddlActividad.DataSource = dtActividades;
                        ddlActividad.DataTextField = "Des";
                        ddlActividad.DataValueField = "Code";
                        ddlActividad.ID = dia + "-" + codigoSubPrograma + "-" + jornada;
                        ddlActividad.Attributes.Add("onchange", "val(this.value)");
                        ddlActividad.DataBind();
                        ddlActividad.Width = 300;
                        ddlActividad.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlActividad.SelectedIndex = 0;
                        ddlActividad.CssClass = "combos";
                        celdaDdlActividades.Controls.Add(ddlActividad);
                        filaDia.Cells.Add(celdaDdlActividades);
                        tblEstadisticas.Rows.Add(filaDia);
                        filaDia = new TableRow();
                    }
                }
            }
        }
        private void MostrarOpciones()
        {
            ddljornada.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddljornada.Items.Add(new ListItem("Matutina", "MATU"));
            ddljornada.Items.Add(new ListItem("Vespertina", "VESP"));
            ddljornada.Items.Add(new ListItem("Completa", "COMP"));

            sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdProgram WHERE Active = 1 AND Project='" + S + "' AND FunctionalArea='PROE'";
            llenarcombo(sql, ddlprograma, "Code", "descripcion");


        }
        private void LLenarHistorial(string sql, GridView gv)
        {
            DataTable tabledata = new DataTable();
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            gv.DataSource = tabledata;
            gv.DataBind();
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
                Response.Write(ex.Message + SQL);
                DataTable d = new DataTable();
                return d;
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
                Response.Write(ex.Message + SQL);
                temp = 5;
                return temp;
            }
        }
        private void llenarcombo(string sql, DropDownList ddl, string codigo, string descripcion)
        {
            ddl.Items.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddl.DataSource = datos;
            ddl.DataValueField = codigo;
            ddl.DataTextField = descripcion;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddl.SelectedIndex = 0;
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
                mst.mostrarMsjNtf("Error" + ex.Message);
                string temp = "||||||";
                return temp;
            }
        }
        protected void btnReferir_Click(object sender, EventArgs e)
        {
            var actividad = VCodigo.Value;
            var miembro = Member;
            string descripcion = txtdescripcion.Text;
            txtdescripcion.Text = "";
            try
            {
                string consulta = "SELECT COUNT(*) conteo FROM dbo.MemberEducationReference WHERE EducationActivityType=" + actividad + " AND RecordStatus=' ' AND Project='" + S + "'";
                int conteo = ObtenerEntero(consulta, "conteo");

                consulta = "SELECT CASE  WHEN Capacity is NULL THEN -1 ELSE Capacity END Capacidad FROM dbo.CdMemberEducationActivityType WHERE Active=1 AND Project='" + S + "' AND Code=" + actividad + "";
                int cupo = ObtenerEntero(consulta, "Capacidad");

                string estado = "";
                if (cupo > conteo)
                {
                    estado = "REFE";
                }
                else
                {
                    if (cupo == -1)
                    {
                        estado = "REFE";
                    }
                    else
                    {
                        estado = "BANC";
                    }

                }

                string sql = @"INSERT INTO dbo.MemberEducationReference(Project,MemberId,EducationActivityType,CreationDateTime,UserId,RecordStatus,ExpirationDateTime,ReferenceDateTime,ReferenceBy,Status,StatusDate,Reason,ReferenceNotes,AttendanceNotes) VALUES('" + S + "','" + miembro + "','" + actividad + "',GETDATE(),'" + U + "',' ',NULL,GETDATE(),'" + U + "','" + estado + "',GETDATE(),' ','" + descripcion + "',NULL)";
                APD.ejecutarSQL(sql);
                mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                /////////////////////////////////
                pnlfiltros.Visible = false;
                string familia = txtmiembro.Text;
                sql = "SELECT M.MemberId,M.LastNames,M.FirstNames,CONVERT(VARCHAR,M.BirthDate,110)+ ' '+ dbo.fn_GEN_edad(M.BirthDate, '" + L + "') 'Cumpleaños/Edad',F.Classification 'Clasificación' FROM dbo.Member M INNER JOIN dbo.Family F ON M.RecordStatus=F.RecordStatus AND M.Project=F.Project AND M.LastFamilyId=F.FamilyId WHERE M.RecordStatus=' 'AND M.LastFamilyId='" + familia + "' AND M.Project='" + S + "'";
                LLenarHistorial(sql, gvhistorial);
                pnlfiltros.Visible = true;
                buscar();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.miembro;
                e.Row.Cells[1].Text = dic.apellidos;
                e.Row.Cells[2].Text = dic.nombres;
                e.Row.Cells[3].Text = dic.edad;
                e.Row.Cells[4].Text = dic.clasificacion;
                e.Row.Cells[5].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistorial.Rows)
            {
                string referir, historial;
                if (L == "es")
                {
                    referir = "Referir";
                    historial = "Historial";
                }
                else
                {
                    referir = "Reference";
                    historial = "History";
                }
                string miembro = gvr.Cells[0].Text;
                sql = "SELECT COUNT(*) conteo FROM dbo.MemberEducationReference WHERE Project='" + S + "' AND MemberId='" + miembro + "' AND RecordStatus=' '";
                int conteo = ObtenerEntero(sql, "conteo");
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btnhistorial");
                btn.Text = historial;
                Button btn2 = new Button();
                btn2 = (Button)gvr.FindControl("btnreferirt");
                btn2.Text = referir;
                if (conteo <= 0)
                {
                    btn.Visible = false;
                }
            }
        }
        protected void ddlprograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlprograma.SelectedIndex > 0)
            {
                ddlsubprograma.Enabled = true;
                sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdSubProgram S WHERE S.Active=1 AND S.Project='" + S + "' AND S.Program='" + ddlprograma.SelectedValue + "' AND 0<(SELECT COUNT(*) conteo FROM dbo.CdProgram P WHERE P.Active = 1 AND P.Project=S.Project AND P.Code=S.Program AND P.FunctionalArea='PROE')";
                llenarcombo(sql, ddlsubprograma, "Code", "descripcion");

            }
            else
            {
                ddlsubprograma.Items.Clear();
                ddlsubprograma.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlsubprograma.SelectedIndex = 0;
                ddlsubprograma.Enabled = false;
            }
        }
        protected void btnbuscarfam_Click(object sender, EventArgs e)
        {
            LlenarInformacion();
        }
        private void LlenarInformacion()
        {
            string miembro = txtmiembro.Text;
            string sql = "SELECT COUNT(*) conteo FROM Family WHERE Project='F' AND RecordStatus=' ' AND FamilyId='" + miembro + "' AND AffiliationStatus='AFIL'";
            string resultado = obtienePalabra(sql, "conteo");
            if (resultado == "0")
            {
                mst.mostrarMsjAdvNtf("La familia no es Afiliada");
            }
            else
            {
                if (string.IsNullOrEmpty(txtmiembro.Text))
                {
                    mst.mostrarMsjAdvNtf("Por favor llene el miembro");
                }
                else
                {
                    sql = "SELECT M.MemberId,M.LastNames,M.FirstNames,CONVERT(VARCHAR,M.BirthDate,110)+ ' '+ dbo.fn_GEN_edad(M.BirthDate, '" + L + "') 'Cumpleaños/Edad',F.Classification 'Clasificación' FROM dbo.Member M INNER JOIN dbo.Family F ON M.RecordStatus=F.RecordStatus AND M.Project=F.Project AND M.LastFamilyId=F.FamilyId WHERE M.RecordStatus=' 'AND M.LastFamilyId='" + miembro + "' AND M.Project='" + S + "'";
                    LLenarHistorial(sql, gvhistorial);
                }
            }
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Referir")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;

                string nombre = clickedRow.Cells[1].Text + " " + clickedRow.Cells[2].Text;
                string miembro = clickedRow.Cells[0].Text;

                Member = miembro;
                lblnombre.Text = nombre + " (" + miembro + ")";
                pnlfiltros.Visible = true;
                pnlHistorial.Visible = false;
            }
            if (e.CommandName == "VerHistorial")
            {
                pnlfiltros.Visible = false;
                mst.ocultarModalYContenido(pnlCalendario);
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;

                string nombre = clickedRow.Cells[1].Text + " " + clickedRow.Cells[2].Text;
                string miembro = clickedRow.Cells[0].Text;

                miembroselect = miembro;
                nombrereferido = nombre;
                //sql = "SELECT ME.EducationActivityType, CASE WHEN '" + L + "'='es' THEN ET.DescSpanish+' ('+ET.Schedule+')' ELSE ET.DescEnglish +' ('+ET.Schedule+')' END Actividad, CONVERT(VARCHAR,ME.ReferenceDateTime,120) 'Fecha de Referencia', CASE WHEN '" + L + "' = 'es' THEN SP.DescSpanish ELSE SP.DescEnglish END SubPrograma, CASE WHEN '" + L + "' = 'es' THEN P.DescSpanish ELSE P.DescEnglish END Programa, WorkDay Jornada, Day dia FROM dbo.MemberEducationReference ME INNER JOIN dbo.CdMemberEducationActivityType ET ON ET.Code = ME.EducationActivityType AND ET.Active = 1 INNER JOIN dbo.CdSubProgram SP ON SP.Code = ET.SubPrograma AND ET.Active = SP.Active INNER JOIN dbo.CdProgram P ON SP.Program = P.Code AND SP.Active = P.Active WHERE ME.Project = '" + S + "' AND ME.RecordStatus = ' ' AND ME.MemberId = '" + miembro.ToString() + "'";
                sql = "SELECT ME.EducationActivityType, CASE WHEN '" + L + "' = 'es' THEN ET.DescSpanish + ' (' + ET.Schedule + ')' ELSE ET.DescEnglish + ' (' + ET.Schedule + ')' END Actividad, CONVERT(VARCHAR, ME.ReferenceDateTime,120) 'Fecha de Referencia', CASE WHEN '" + L + "' = 'es' THEN SP.DescSpanish ELSE SP.DescEnglish END SubPrograma, CASE WHEN '" + L + "' = 'es' THEN P.DescSpanish ELSE P.DescEnglish END Programa, CASE WHEN '" + L + "' = 'es' THEN J.DescSpanish ELSE J.DescEnglish END Jornada, CASE WHEN '" + L + "' = 'es' THEN D.DescSpanish ELSE D.DescEnglish END dia, SUBSTRING(ME.ReferenceNotes, 1, 25) + '...' Comentarios FROM dbo.MemberEducationReference ME INNER JOIN dbo.CdMemberEducationActivityType ET ON ET.Code = ME.EducationActivityType AND ET.Active = 1 INNER JOIN dbo.CdSubProgram SP ON SP.Code = ET.SubPrograma AND ET.Active = SP.Active INNER JOIN dbo.CdProgram P ON SP.Program = P.Code AND SP.Active = P.Active INNER JOIN dbo.CdDay D ON D.Code = ET.Day INNER JOIN dbo.CdWorkingDay J ON J.Code = ET.WorkDay WHERE ME.Project = '" + S + "' AND ME.RecordStatus = ' ' AND ME.MemberId = '" + miembro.ToString() + "'";
                DataTable dt = new DataTable();
                LlenarDataTable(sql, dt);
                gvhistorialmiembro.DataSource = dt;
                gvhistorialmiembro.DataBind();
                lblmiembroselec.Text = nombre + "(" + miembroselect + ")";
                pnlHistorial.Visible = true;
                //mst.ocultarModalYContenido(pnlCalendario);
            }
        }

        protected void gvhistorialmiembro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;

                string fecha = clickedRow.Cells[2].Text;
                string codigoactividad = clickedRow.Cells[0].Text;
                mdcodigo = codigoactividad;
                modfecha = fecha;
                sql = "SELECT ReferenceNotes FROM dbo.MemberEducationReference WHERE Project='" + S + "' AND MemberId='" + miembroselect + "' AND CONVERT(VARCHAR,ReferenceDateTime,120)='" + modfecha + "' AND RecordStatus=' '";
                string notas = APD.obtienePalabra(sql, "ReferenceNotes");
                txtmod.Text = notas;

                mst.mostrarModalYContenido(pnlModificar);
                lblmodificicacion.Text = nombrereferido + " (" + miembroselect + ")";
                pnlModificar.Visible = true;
            }
            if (e.CommandName == "Eliminar")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;

                string fecha = clickedRow.Cells[2].Text;
                string codigoactividad = clickedRow.Cells[0].Text;
                mdcodigo = codigoactividad;
                modfecha = fecha;
                mst.ocultarModalYContenido(pnlModificar);
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            mst.ocultarModalYContenido(pnlModificar);
            txtmod.Text = "";
        }

        protected void btnmodicar_Click(object sender, EventArgs e)
        {
            string notas = txtmod.Text;
            sql = "INSERT INTO dbo.MemberEducationReference(Project,MemberId,EducationActivityType,CreationDateTime,UserId,RecordStatus,ExpirationDateTime,ReferenceDateTime,ReferenceBy,Status,StatusDate,Reason,ReferenceNotes,AttendanceNotes,AttendedBy) SELECT Project, MemberId, EducationActivityType, GETDATE() CreationDateTime, '" + U + "' UserId, RecordStatus, ExpirationDateTime, ReferenceDateTime, ReferenceBy, Status, StatusDate, Reason, '" + notas + "' ReferenceNotes, AttendanceNotes, AttendedBy FROM dbo.MemberEducationReference WHERE Project = '" + S + "' AND MemberId = '" + miembroselect + "' AND CONVERT(VARCHAR, ReferenceDateTime, 120) = '" + modfecha + "' AND RecordStatus=' '";
            APD.ejecutarSQL(sql);
            mst.ocultarModalYContenido(pnlModificar);
        }
        protected void accionar(object sender, EventArgs e)
        {
            sql = "INSERT INTO dbo.MemberEducationReference(Project,MemberId,EducationActivityType,CreationDateTime,UserId,RecordStatus,ExpirationDateTime,ReferenceDateTime,ReferenceBy,Status,StatusDate,Reason,ReferenceNotes,AttendanceNotes,AttendedBy) SELECT Project, MemberId, EducationActivityType, GETDATE() CreationDateTime, '" + U + "' UserId,'H' RecordStatus,GETDATE() ExpirationDateTime, ReferenceDateTime, ReferenceBy, Status, StatusDate, Reason, ReferenceNotes, AttendanceNotes, AttendedBy FROM dbo.MemberEducationReference WHERE Project = '" + S + "' AND MemberId = '" + miembroselect + "' AND CONVERT(VARCHAR, ReferenceDateTime, 120) = '" + modfecha + "' AND RecordStatus=' '";
            APD.ejecutarSQL(sql);
            sql = "UPDATE dbo.MemberEducationReference SET RecordStatus='H',ExpirationDateTime = GETDATE() WHERE Project = '" + S + "' AND MemberId = '" + miembroselect + "' AND CONVERT(VARCHAR, ReferenceDateTime, 120) = '" + modfecha + "' AND RecordStatus=' '";
            APD.ejecutarSQL(sql);
            LlenarInformacion();
        }

        protected void gvhistorialmiembro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string jornada, dia, subprograma, fecharef, notas;
            if (L == "es") { jornada = "Jornada"; dia = "Día"; subprograma = "Subprograma"; fecharef = "Fecha de Referencia"; notas = "Notas de Referencia"; } else { jornada = "WorkDay"; dia = "Day"; subprograma = "Subprogram"; fecharef = "Reference Date"; notas = "Reference Notes"; }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.actividad;
                e.Row.Cells[2].Text = fecharef;
                e.Row.Cells[3].Text = subprograma;
                e.Row.Cells[4].Text = dic.programa;
                e.Row.Cells[5].Text = jornada;
                e.Row.Cells[6].Text = dia;
                e.Row.Cells[7].Text = notas;
                e.Row.Cells[8].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistorialmiembro.Rows)
            {
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btnmodificarf");
                btn.Text = dic.ModificarAPAD;
                Button btn2 = new Button();
                btn2 = (Button)gvr.FindControl("btneliminarf");
                btn2.Text = dic.eliminar;
            }
        }
    }
}