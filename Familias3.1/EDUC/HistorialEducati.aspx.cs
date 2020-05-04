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
    public partial class HistorialEducati : System.Web.UI.Page
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
        protected static string sql;
        protected static int desicion;
        protected static string ruta;
        protected static string creat, tipo, id,anioelminar;
        protected static int accion;
        protected static int accionborrar = 0;
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
        private void agregaAvisos()
        {
            DataTable dtAvisos = new BDFamilia().obtenerAvisos(S, F, L, false);
            gdvAvisos.Columns[0].HeaderText = dic.avisos;
            if (dtAvisos.Rows.Count > 0)
            {
                gdvAvisos.DataSource = dtAvisos;
                gdvAvisos.DataBind();
            }
            else
            {
                DataTable dtAvisosAux = new DataTable();
                dtAvisosAux.Columns.Add("Aviso");
                dtAvisosAux.Rows.Add(dic.noTiene);
                gdvAvisos.DataSource = dtAvisosAux;
                gdvAvisos.DataBind();
            }
        }
        protected void ValoresIniciales()
        {
            tbinfo.Visible = false;
            gdvAvisos.Visible = false;
            gvhistorial.Visible = true;
            tbcheques.Visible = false;
            gvutiles.Visible = false;
            btnregresar.Visible = false;
            LLenarHistorial();
            llenarcombos();
            Traducir();
            DataTable dt = APD.InfoGen(S, M, L);
            try
            {
                string A = dt.Rows[0][1].ToString();
                if (A == "Afiliado" || A == "Affiliated")
                {
                    ruta = @"\\SVRAPP\FamilyFotos\Apadrinados";
                    desicion = 0;
                    Foto(S, M, desicion, ruta);
                }
                else if (string.IsNullOrEmpty(A))
                {

                }
                else
                {
                    ruta = @"\\SVRAPP\FamilyFotos\ExApadrinados";
                    desicion = 1;
                    Foto(S, M, desicion, ruta);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void LLenarHistorial()
        {
            sql = "SELECT 'Detalle' 'Revisar',  'Borrar' AS Borrar, CONVERT(nvarchar(30), MEY.CreationDateTime, 21) AS Crea, MEY.SchoolYear Año, CdG.DescSpanish Grado, CdES.DescSpanish Estado, cdEES.DescSpanish ExcepcionEstado, S.Name Escuela, cdEC.DescSpanish Carrera, MEY.Notes Notas, MEY.UserId 'Usuario' FROM	MemberEducationYear MEY INNER JOIN School S On MEY.Project=S.Project and MEY.SchoolCode=S.Code AND MEY.RecordStatus = S.RecordStatus INNER JOIN CdGrade CdG On MEY.Grade=CdG.Code INNER JOIN CdEducationStatus CdES On MEY.Status=CdES.Code LEFT JOIN CdEducationCareer cdEC ON cdEC.Code = MEY.Career LEFT JOIN CdEducationReasonNotToContinue cdEES ON cdEES.Code = MEY.ReasonNotToContinue WHERE MEY.RecordStatus=' ' AND MEY.Project = '" + S + "' AND MEY.MemberId = " + M + " GROUP BY MEY.SchoolYear, CdG.DescSpanish, cdEES.DescSpanish, CdG.Orden, CdEs.DescSpanish, S.Name, cdEC.DescSpanish, MEY.Notes, MEY.UserId, CONVERT(nvarchar(30), MEY.CreationDateTime, 21) ORDER BY MEY.SchoolYear DESC, CdG.Orden DESC ";
            llenargrid2(sql, gvhistorial);
        }
        private void llenargrid(string sql, GridView gv)
        {
            try
            {
                gv.Columns.Clear();
                gv.DataSource = null;
                gv.DataBind();
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
        private void llenargrid2(string sql, GridView gv)
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
                Response.Write(sql);
            }
        }
        protected void InformacionGen(string anio)
        {
            DataTable listTable = new DataTable();
            DateTime actualD = DateTime.Now;
            lblVanio.Text = actualD.Year.ToString();
            sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + anio + ") L ";
            //  Response.Write(sql);
            LlenarDataTable(sql, listTable);
            sql = "SELECT FirstNames+' '+ LastNames Nombre FROM dbo.Member WHERE MemberId='" + M + "' AND Project='" + S + "'";
            lblnombre.Text = APD.obtienePalabra(sql, "Nombre");
            if (string.IsNullOrEmpty(listTable.Rows[0]["Grado"].ToString()))
            {
                ddlgrado.SelectedIndex = 0;
            }
            else
            {
                sql = "SELECT Code FROM dbo.CdGrade WHERE ValidValue = 1 AND(DescSpanish = '" + listTable.Rows[0]["Grado"].ToString() + "' OR DescEnglish = '" + listTable.Rows[0]["Grado"].ToString() + "') ORDER BY Orden";
                string codigogrado = obtienePalabra(sql, "Code");
                ddlgrado.SelectedValue = codigogrado;
            }

            if (string.IsNullOrEmpty(listTable.Rows[0]["Estado_Educ"].ToString()))
            {
                ddlestado.SelectedIndex = 0;
            }
            else
            {
                sql = "SELECT Code FROM CdEducationStatus  WHERE ValidValue = 1 AND (DescSpanish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "')";
                string codigoestado = obtienePalabra(sql, "Code");
                ddlestado.SelectedValue = codigoestado;
            }
            if (string.IsNullOrEmpty(listTable.Rows[0]["RazonNoContinuar"].ToString()))
            {
                ddlexestado.SelectedIndex = 0;
            }
            else
            {
                sql = "SELECT Code FROM CdEducationReasonNotToContinue  WHERE Active = 1 AND (DescSpanish = '" + listTable.Rows[0]["RazonNoContinuar"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["RazonNoContinuar"].ToString() + "') ";
                string codigoexestado = obtienePalabra(sql, "Code");
                ddlexestado.SelectedValue = codigoexestado;
            }
            ddlcarrera.SelectedValue = listTable.Rows[0]["CarreraId"].ToString();
            ddlescuela.SelectedValue = listTable.Rows[0]["EscuelaId"].ToString();
            CreationDateL.Text = listTable.Rows[0]["CreationDT"].ToString();
            if (string.IsNullOrEmpty(listTable.Rows[0]["Edad"].ToString())) { lblnombre.Text = lblnombre.Text; }
            else { lblnombre.Text = lblnombre.Text + " - " + listTable.Rows[0]["Edad"].ToString(); }
            if (string.IsNullOrEmpty(listTable.Rows[0]["ExcepPorcentaje"].ToString())) { lblporcentaje.Text = lblporcentaje.Text; } else { lblporcentaje.Text = lblporcentaje.Text + ":  " + listTable.Rows[0]["ExcepPorcentaje"].ToString(); }
            if (string.IsNullOrEmpty(listTable.Rows[0]["Fase"].ToString())) { lbldesafiliacion.Text = lbldesafiliacion.Text; } else { lbldesafiliacion.Text = lbldesafiliacion.Text + ":  " + listTable.Rows[0]["Fase"].ToString(); }
            if (string.IsNullOrEmpty(listTable.Rows[0]["EstadoAfil"].ToString())) { lblestadoafil.Text = lblestadoafil.Text; } else { lblestadoafil.Text = lblestadoafil.Text + ":  " + listTable.Rows[0]["EstadoAfil"].ToString(); }
            if (string.IsNullOrEmpty(listTable.Rows[0]["TipoAfil"].ToString())) { lbltipoafil.Text = lbltipoafil.Text; } else { lbltipoafil.Text = lbltipoafil.Text + ":  " + listTable.Rows[0]["TipoAfil"].ToString(); }
            if (string.IsNullOrEmpty(listTable.Rows[0]["Clasificación"].ToString())) { lblclasificacion.Text = lblclasificacion.Text; } else { lblclasificacion.Text = lblclasificacion.Text + ":  " + listTable.Rows[0]["Clasificación"].ToString(); }
            chktienecertificado.Checked = Convert.ToBoolean(listTable.Rows[0]["TieneCertificado"]);
            string semaf = listTable.Rows[0]["Semaforo"].ToString();
            Double porc = obtienePorcentaje();
            lblVporcentaje.Text = porc.ToString() + "%";
            lblVSemaforo.Text = semaf;
            if (semaf == "VERD" || semaf == "Verde")
            {
                pnlVSemaforo.BackColor = Color.MediumSeaGreen;
            }
            else if (semaf == "ROJO" || semaf == "Rojo")
            {
                pnlVSemaforo.BackColor = Color.Crimson;
            }
            else if (semaf == "AMAR" || semaf == "Amarillo")
            {
                pnlVSemaforo.BackColor = Color.Yellow;
            }
            else
            {
                pnlVSemaforo.Visible = false;
            }
            lblVSemaforo.Visible = false;
            lblVanio.Text = anio;
        }
        protected void llenarcombos()
        {
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
                return "";
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
        public void Foto(string S, string M, int grad, string direccion)
        {
            try
            {
                con.Open();
                String sql = "SELECT CASE WHEN dbo.fn_SPON_LastMemberPhoto ('" + S + "','" + M + "') IS NULL THEN ' ' ELSE dbo.fn_SPON_LastMemberPhoto ('" + S + "','" + M + "') END resultado";
                string photourl = APD.obtienePalabra(sql, "resultado");
                String[] newurl = photourl.Split(':');

                string newsubstring = photourl.Replace(@"G:\", "");
                newsubstring = newsubstring.Replace(@"V:\", "");
                newsubstring = newsubstring.Replace(@"g:\", "");
                Boolean foto;
                foto = System.IO.File.Exists(direccion + newurl[1]);
                if (foto == false)
                {
                    imgApadFoto.Attributes["src"] = "../Images/CommonHope_Heart_RGB.png";
                }
                else
                {
                    if (grad == 0)
                    {
                        imgApadFoto.Attributes["src"] = "../Imagen.ashx?imageID=" + newurl[1].ToString();
                    }
                    else
                    {
                        imgApadFoto.Attributes["src"] = "../ImagenG.ashx?imageID=" + newurl[1].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                mst.mostrarMsjAdvNtf(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        protected Double obtienePorcentaje()
        {
            sql = "SELECT dbo.fn_BECA_PorcentajeReembolsos('" + S + "', " + M + ") Porcentaje";
            Double porcentaje = Convert.ToDouble(ObtenerEntero(sql, "Porcentaje"));
            return porcentaje;
        }
        protected void Traducir()
        {
            chkactividades.Text = dic.actividades;
            chkobservaciones.Text = dic.observaciones;
            chkrembolsos.Text = "Rembolsos";
            btnregresar.Text = dic.OtraBusquedaAPAD;
            string porcentaje, desafiliacion;
            if (L == "es") { porcentaje = "Porcentaje"; desafiliacion = "Desafiliación"; } else { porcentaje = "Percentage"; desafiliacion = "Desafiliation"; }
            lblescuela.Text = dic.EscuelaAPAD;
            lblgrado.Text = dic.grado;
            lblestado.Text = dic.estado;
            lblexcestado.Text = dic.excepcionEstadoEducativo;
            lblcarrera.Text = dic.carrera;
            lblporcentaje.Text = porcentaje;
            lblanioesc.Text = dic.añoEducativo;
            lblsemaforo.Text = dic.semaforo;
            lbldesafiliacion.Text = desafiliacion;
            lblestadoafil.Text = dic.estado;
            lblclasificacion.Text = dic.clasificacion;
            lbltipoafil.Text = dic.afilTIpo;
            chktienecertificado.Text = "¿Tiene Certificado?";
            btnactualizarinfo.Text = dic.actualizar;
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.año;
                e.Row.Cells[2].Text = dic.grado;
                e.Row.Cells[3].Text = dic.estado;
                e.Row.Cells[4].Text = dic.excepcionEstadoEducativo;
                e.Row.Cells[5].Text = dic.EscuelaAPAD;
                e.Row.Cells[6].Text = dic.carrera;
                e.Row.Cells[7].Text = dic.notas;
                e.Row.Cells[8].Text = dic.usuario;
                e.Row.Cells[9].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistorial.Rows)
            {
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btnver");

                Button btn2 = new Button();
                btn2 = (Button)gvr.FindControl("btneliminar");
                string ver;
                if (L == "es") { ver = "Ver"; } else { ver = "Watch"; }
                btn.Text = ver;
                btn2.Text = dic.eliminar;
            }
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;
                string anio;
                anio = clickedRow.Cells[1].Text;
                Traducir();
                InformacionGen(anio);
                tbinfo.Visible = true;
                btnregresar.Visible = true;
                gdvAvisos.Visible = true;
                gvhistorial.Visible = false;
                tbcheques.Visible = true;
                gvutiles.Visible = true;
                sql = "SELECT CONVERT(nvarchar(30), MA.CreationDateTime, 21) Crea, cdAT.Code Cod,dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, cdAT.DescSpanish AS Actividad, MA.Notes AS Observaciones, MA.UserId Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdAT ON MA.Type = cdAT.Code WHERE MA.RecordStatus = ' ' AND cdAT.FunctionalArea = 'EDUC' AND MA.Project = '" + S + "' AND YEAR(MA.ActivityDateTime) = " + lblVanio.Text + " AND MA.MemberId = " + M + " ORDER BY MA.CreationDateTime DESC ";
                llenargrid2(sql, gvutiles);
                accionborrar = 2;
            }
            if (e.CommandName == "del")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;
                anioelminar= clickedRow.Cells[1].Text;
                accionborrar =5;
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);

            }
        }

        protected void btnregresar_Click(object sender, EventArgs e)
        {
            tbinfo.Visible = false;
            gdvAvisos.Visible = false;
            gvhistorial.Visible = true;
            tbcheques.Visible = false;
            gvutiles.Visible = false;
            btnregresar.Visible = true;
        }
        protected void accionar(object sender, EventArgs e)
        {
            switch (accionborrar)
            {
                case 2:
                    sql = "UPDATE dbo.MemberActivity SET RecordStatus = 'H', ExpirationDateTime = GETDATE(), Notes = Notes + '" + U + "' WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND Convert(nvarchar(30), CreationDateTime, 21) = '" + creat + "' AND Type = '" + tipo + "'";
                    APD.ejecutarSQL(sql);
                    mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                    sql = "SELECT  CONVERT(nvarchar(30), MA.CreationDateTime, 21) Crea, cdAT.Code Cod,dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, cdAT.DescSpanish AS Actividad, MA.Notes AS Observaciones, MA.UserId Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdAT ON MA.Type = cdAT.Code WHERE MA.RecordStatus = ' ' AND cdAT.FunctionalArea = 'EDUC' AND MA.Project = '" + S + "' AND YEAR(MA.ActivityDateTime) = " + lblVanio.Text + " AND MA.MemberId = " + M + " ORDER BY MA.CreationDateTime DESC ";
                    llenargrid2(sql, gvutiles);
                    break;
                case 3:
                    sql = "UPDATE dbo.MemberEducationObservation SET RecordStatus = 'H', ExpirationDateTime = GETDATE(), Observation = Observation + ' (" + U + ")' WHERE RecordStatus = ' ' AND IdObservation = " + id + " ";
                    APD.ejecutarSQL(sql);
                    mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                    sql = "SELECT  cdC.DescSpanish AS Categoría, dbo.fn_GEN_FormatDate(MEO.ObservationDateTime, 'ES')  + ' ' + CONVERT(varchar, MEO.ObservationDateTime, 108)  AS Fecha, MEO.Observation AS Observación,  MEO.UserId AS Usuario,  MEO.IdObservation FROM  dbo.MemberEducationObservation MEO INNER JOIN dbo.CdMemberEducObservationCategory cdC ON MEO.Category = cdC.Code WHERE MEO.RecordStatus = ' ' AND MEO.Project = '" + S + "' AND YEAR(MEO.ObservationDateTime) = " + lblVanio.Text + " AND MEO.MemberId = " + M + " ORDER BY MEO.ObservationDateTime DESC ";
                    llenargrid2(sql, gvutiles);
                    break;
                case 4:
                    DataTable listTable = new DataTable();
                    sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + lblVanio.Text + ") L ";
                    LlenarDataTable(sql, listTable);
                    string certificado;
                    DateTime actual = DateTime.Now;
                    if (chktienecertificado.Checked == true) { certificado = "1"; } else { certificado = "0"; }
                    sql = "SELECT Code FROM dbo.CdGrade WHERE ValidValue = 1 AND(DescSpanish = '" + listTable.Rows[0]["Grado"].ToString() + "' OR DescEnglish = '" + listTable.Rows[0]["Grado"].ToString() + "') ORDER BY Orden";
                    string codigogrado = obtienePalabra(sql, "Code");
                    sql = "SELECT Code FROM CdEducationStatus  WHERE ValidValue = 1 AND (DescSpanish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "')";
                    string codigoestado = obtienePalabra(sql, "Code");
                    string carreracodigo;
                    if (string.IsNullOrEmpty(ddlcarrera.SelectedValue)) { carreracodigo = "NULL"; } else { carreracodigo = "'" + ddlcarrera.SelectedValue + "'"; }
                    sql = "INSERT INTO  dbo.MemberEducationYear SELECT Project, MemberId, SchoolYear, '" + ddlescuela.SelectedValue + "' SchoolCode, '" + ddlgrado.SelectedValue + "' Grade, GETDATE() CreationDateTime, RecordStatus,'" + U + "' UserId, ExpirationDateTime, ClassSection, PercentOfExpensesToPay, '" + ddlexestado.SelectedValue + "' ReasonNotToContinue, '" + ddlestado.SelectedValue + "' Status, " + carreracodigo + " Career, SingleTeacher, TransportationStartDate, TransportationEndDate, Notes, ExceptionPercent, '" + certificado + "' HasCertificate, NYSPackage, Typing FROM  dbo.MemberEducationYear WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND SchoolYear = " + lblVanio.Text + " AND Grade = '" + codigogrado + "' AND Status = '" + codigoestado + "' AND SchoolCode = '" + listTable.Rows[0]["EscuelaId"].ToString() + "'";
                    APD.ejecutarSQL(sql);
                    //inactiva el historial del record
                    sql = "UPDATE dbo.MemberEducationYear SET RecordStatus = 'H', ExpirationDateTime = GETDATE() WHERE  RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND schoolYear = " + lblVanio.Text + "  AND schoolCode = '" + listTable.Rows[0]["EscuelaId"].ToString() + "' AND grade = '" + codigogrado + "' AND Convert(nvarchar(30), CreationDateTime, 20) = '" + CreationDateL.Text + "' ";
                    APD.ejecutarSQL(sql);
                    LLenarHistorial();
                    tbinfo.Visible = false;
                    gdvAvisos.Visible = false;
                    gvhistorial.Visible = true;
                    tbcheques.Visible = false;
                    gvutiles.Visible = false;
                    break;
                    default:
                    mst.mostrarMsjAdvNtf("Ha ocurrido un error: por fabor cambie de funcion y vuelva a intentar si el problema persiste por fabor informe a sistemas.");
                    break;
                case 5:
                    DataTable listTable2 = new DataTable();

                    sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + anioelminar + ") L ";
                    LlenarDataTable(sql, listTable2);
                    sql = "SELECT Code FROM dbo.CdGrade WHERE ValidValue = 1 AND(DescSpanish = '" + listTable2.Rows[0]["Grado"].ToString() + "' OR DescEnglish = '" + listTable2.Rows[0]["Grado"].ToString() + "') ORDER BY Orden";
                    string codigogrado1 = obtienePalabra(sql, "Code");


                    sql = "UPDATE dbo.MemberEducationYear SET RecordStatus = 'H', ExpirationDateTime = GETDATE() WHERE  RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND schoolYear = " + anioelminar + "  AND schoolCode = '" + listTable2.Rows[0]["EscuelaId"].ToString() + "' AND grade = '" + codigogrado1 + "' AND Convert(nvarchar(30), CreationDateTime, 20) = '" + listTable2.Rows[0]["CreationDT"].ToString() + "' ";
                    APD.ejecutarSQL(sql);
                    //Response.Write(sql);
                    LLenarHistorial();
                    break;
            }
        }
        protected void chkrembolsos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkrembolsos.Checked == true)
            {
                chkactividades.Checked = false;
                chkobservaciones.Checked = false;
                sql = "SELECT Tipo, Autorizado, Pagado, Cantidad Total, [Cantidad Aprobada] Aprobado, Porcentaje, AprobadoPor, Observaciones FROM dbo.fn_GEN_HistorialPagos('" + S + "', " + M + ") WHERE YEAR(FechaA) = " + lblVanio.Text + " AND Area = 'EDUC' ORDER BY FechaA DESC ";
                llenargrid2(sql, gvutiles);
                accionborrar = 1;
            }
            else
            {
                limpiarGrid();
            }
        }
        protected void limpiarGrid()
        {
            gvutiles.Columns.Clear();
            gvutiles.DataSource = null;
            gvutiles.DataBind();
        }

        protected void chkactividades_CheckedChanged(object sender, EventArgs e)
        {
            if (chkactividades.Checked == true)
            {
                chkrembolsos.Checked = false;
                chkobservaciones.Checked = false;
                sql = "SELECT  CONVERT(nvarchar(30), MA.CreationDateTime, 21) Crea, cdAT.Code Cod,dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, cdAT.DescSpanish AS Actividad, MA.Notes AS Observaciones, MA.UserId Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdAT ON MA.Type = cdAT.Code WHERE MA.RecordStatus = ' ' AND cdAT.FunctionalArea = 'EDUC' AND MA.Project = '" + S + "' AND YEAR(MA.ActivityDateTime) = " + lblVanio.Text + " AND MA.MemberId = " + M + " ORDER BY MA.CreationDateTime DESC ";
                llenargrid2(sql, gvutiles);
                accionborrar = 2;
            }
            else
            {
                limpiarGrid();
            }
        }

        protected void chkobservaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (chkobservaciones.Checked == true)
            {
                chkrembolsos.Checked = false;
                chkactividades.Checked = false;
                sql = "SELECT  cdC.DescSpanish AS Categoría, dbo.fn_GEN_FormatDate(MEO.ObservationDateTime, 'ES')  + ' ' + CONVERT(varchar, MEO.ObservationDateTime, 108)  AS Fecha, MEO.Observation AS Observación,  MEO.UserId AS Usuario,  MEO.IdObservation FROM  dbo.MemberEducationObservation MEO INNER JOIN dbo.CdMemberEducObservationCategory cdC ON MEO.Category = cdC.Code WHERE MEO.RecordStatus = ' ' AND MEO.Project = '" + S + "' AND YEAR(MEO.ObservationDateTime) = " + lblVanio.Text + " AND MEO.MemberId = " + M + " ORDER BY MEO.ObservationDateTime DESC ";
                llenargrid2(sql, gvutiles);
                accionborrar = 3;
            }
            else
            {
                limpiarGrid();
            }
        }
        protected string verificarcampos(int conteo, string cadena)
        {
            if (conteo == 1)
            {
                cadena = cadena.Replace(",", "");
                return cadena;
            }
            else
            {
                return cadena;
            }
        }
        protected void btnactualizarinfo_Click(object sender, EventArgs e)
        {
            DataTable listTable = new DataTable();
            sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + lblVanio.Text + ") L ";
            LlenarDataTable(sql, listTable);
            int conteo = 0;
            string cambios = "";
            string desafiliacion;
            if (L == "es") { desafiliacion = "Desafiliación"; } else { desafiliacion = "Desafiliation"; }
            if (ddlgrado.SelectedItem.Text != listTable.Rows[0]["Grado"].ToString())
            {
                cambios = cambios + "," + dic.grado;
                conteo++;
                cambios = verificarcampos(conteo, cambios);
            }
            if (ddlescuela.SelectedValue != listTable.Rows[0]["EscuelaId"].ToString())
            {
                cambios = cambios + "," + dic.EscuelaAPAD;
                conteo++;
                cambios = verificarcampos(conteo, cambios);
            }
            if (ddlestado.SelectedItem.Text != listTable.Rows[0]["Estado_Educ"].ToString())
            {
                cambios = cambios + "," + dic.afilEstado;
                conteo++;

                cambios = verificarcampos(conteo, cambios);
            }
            if (ddlexestado.SelectedItem.Text != listTable.Rows[0]["RazonNoContinuar"].ToString())
            {
                cambios = cambios + "," + desafiliacion;
                conteo++;

                cambios = verificarcampos(conteo, cambios);
            }
            if (ddlcarrera.SelectedValue != listTable.Rows[0]["CarreraId"].ToString())
            {
                cambios = cambios + "," + dic.carrera;
                conteo++;
                cambios = verificarcampos(conteo, cambios);
            }
            if (conteo > 0)
            {
                accionborrar = 4;
                mst.mostrarMsjOpcionesMdl("Ha modificado los siguientes campos: " + cambios + ". ¿Desea Continuar? ");
            }
            else
            {
                mst.mostrarMsjAdvNtf("No se ha modificado la información");
            }
        }

        protected void gvutiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            //var clickedButton = e.CommandSource as Button;
            //var clickedRow = clickedButton.NamingContainer as GridViewRow;
            if (accionborrar == 2)
            {
                creat = gvutiles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                tipo = gvutiles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            }
            else if (accionborrar == 3)
            {
                id = gvutiles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Text;
            }
        }

        protected void gdvAvisos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.accion;
            }
            if (accionborrar == 1)
            {
                foreach (GridViewRow gvr in gdvAvisos.Rows)
                {
                    Button btn = new Button();
                    btn = (Button)gvr.FindControl("btnelimin");
                    btn.Visible = false;
                }
            }
            else
            {
                foreach (GridViewRow gvr in gdvAvisos.Rows)
                {
                    Button btn = new Button();
                    btn = (Button)gvr.FindControl("btnelimin");
                    btn.Text = dic.eliminar;
                }
            }
        }
    }
}