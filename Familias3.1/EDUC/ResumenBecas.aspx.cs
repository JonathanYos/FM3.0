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
        protected static string sql;
        protected static int desicion;
        protected static string ruta;
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
            agregaAvisos();
            llenarcombos();
            Traducir();
            InformacionGen();
            historialobs();
            sql = "SELECT dbo.fn_EDUC_semaforoEsp('" + S + "', " + M + ", " + lblVanio.Text + ", MECG.Unit) AS Semáforo, MECG.Unit AS Unidad, cdS.DescSpanish AS Fuente, MECG.TotalClasses 'Total Clases', CASE WHEN MECG.ApprovedAll = 1 THEN 'SI' ELSE 'NO' END AS 'Ganó todas', MECG.Average80 AS 'Promedio (>80%)', MECG.FailedClasses AS 'Cantidad Pérdidas', (SELECT COUNT(*) FROM MemberEducationClassFailed AS MECF WHERE RecordStatus = ' ' AND Ref = MECG.Ref) AS Registradas, MECG.Notes Notas, MECG.UserId Usuario FROM dbo.MemberEducationClassGrade AS MECG INNER JOIN dbo.CdSchoolGradeSource AS cdS ON MECG.Source = cdS.Code WHERE MECG.RecordStatus = ' ' AND MECG.Project = '" + S + "' AND MECG.MemberId = " + M + " AND MECG.SchoolYear = " + lblVanio.Text + " ORDER BY MECG.Unit ";
            llenargrid(sql, gvhistotirialcal);
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
        protected void Traducir()
        {
            string porcentaje, desafiliacion, nobservacion, ver;
            if (L == "es") { porcentaje = "Porcentaje"; desafiliacion = "Desafiliación"; ver = "Ver"; nobservacion = "Nueva Observacion"; } else { porcentaje = "Percentage"; ver = "Watch"; desafiliacion = "Desafiliation"; nobservacion = "New Observation"; }
            sql = "SELECT FirstNames+' '+ LastNames Nombre FROM dbo.Member WHERE MemberId='" + M + "' AND Project='" + S + "'";
            lblnombre.Text = APD.obtienePalabra(sql, "Nombre");
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
            btnguardarobs.Text = dic.guardar;
            lbltituloobs.Text = dic.observaciones;
            lblnuevaobs.Text = nobservacion;
            chktienecertificado.Text = "¿Tiene Certificado?";
            btnactualizarinfo.Text = dic.actualizar;
            btnguardarcal.Text = dic.guardar;
            btnvercali.Text = ver;
            btntoposrem.Text = "Rembolsos por Tipo";
            btnautorizarem.Text = "Autorizar Rembolsos";
            btndetallesrem.Text = "Detalles Rembolsos";

        }
        protected void llenarcombos()
        {
            //observaciones
            sql = "SELECT Code, CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END 'Categoria' FROM dbo.CdMemberEducObservationCategory WHERE Active = 1 ORDER BY DescSpanish ";
            bdCombo(sql, ddltipoobs, "Code", "Categoria");
            //catetegorias para ver
            sql = "SELECT 'ACTI' Code, 'Actividades' 'Descripcion' UNION ALL SELECT 'HEDU' Code, 'Historial Educativo' UNION ALL SELECT 'HARE' Code, 'Historial Areas' UNION ALL SELECT 'TELE' Code, 'Teléfonos' UNION ALL SELECT 'INFG' Code, 'Información General' UNION ALL SELECT 'HUTI' Code, 'Historial Feria Utiles' UNION ALL SELECT 'HCAL' Code, 'Historial Calificaciones' ";
            bdCombo(sql, ddlcategoriascal, "Code", "Descripcion");

            //lista activiades
            sql = "SELECT Code, CASE WHEN 'es'='" + L + "' THEN DescSpanish ELSE DescEnglish END 'Actividad' FROM dbo.CdMemberActivityType WHERE Project IN ('" + S + "', '*') AND FunctionalArea = 'EDUC' AND Active = 1 AND NotesRequired = 0 ORDER BY DescSpanish ";
            bdCombo(sql, ddltipocal, "Code", "Actividad");

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
        protected void InformacionGen()
        {
            txtnotasobs.Attributes.Add("maxlength", "300");
            txtnotascal.Attributes.Add("maxlength", "120");
            DataTable listTable = new DataTable();
            DateTime actualD = DateTime.Now;
            lblVanio.Text = actualD.Year.ToString();
            sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + lblVanio.Text + ") L ";
            LlenarDataTable(sql, listTable);
            sql = "SELECT Code FROM dbo.CdGrade WHERE ValidValue = 1 AND(DescSpanish = '" + listTable.Rows[0]["Grado"].ToString() + "' OR DescEnglish = '" + listTable.Rows[0]["Grado"].ToString() + "') ORDER BY Orden";
            string codigogrado = obtienePalabra(sql, "Code");
            sql = "SELECT Code FROM CdEducationStatus  WHERE ValidValue = 1 AND (DescSpanish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["Estado_Educ"].ToString() + "')";
            string codigoestado = obtienePalabra(sql, "Code");
            sql = "SELECT Code FROM CdEducationReasonNotToContinue  WHERE Active = 1 AND (DescSpanish = '" + listTable.Rows[0]["RazonNoContinuar"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["RazonNoContinuar"].ToString() + "') ";
            string codigoexestado = obtienePalabra(sql, "Code");
            ddlcarrera.SelectedValue = listTable.Rows[0]["CarreraId"].ToString();
            ddlgrado.SelectedValue = codigogrado;
            ddlescuela.SelectedValue = listTable.Rows[0]["EscuelaId"].ToString();
            ddlestado.SelectedValue = codigoestado;
            ddlexestado.SelectedValue = codigoexestado;
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
            }
        }
        private void historialobs()
        {
            try
            {
                sql = "SELECT  cdC.DescSpanish AS Categoria, dbo.fn_GEN_FormatDate(MEO.ObservationDateTime, 'ES')  + ' ' + CONVERT(varchar, MEO.ObservationDateTime, 108)  AS Fecha, MEO.Observation AS Observacion,  MEO.UserId AS Usuario,  MEO.IdObservation  FROM  dbo.MemberEducationObservation MEO INNER JOIN dbo.CdMemberEducObservationCategory cdC ON MEO.Category = cdC.Code  WHERE MEO.RecordStatus = ' ' AND MEO.Project = '" + S + "' AND YEAR(MEO.ObservationDateTime) = " + lblVanio.Text + " AND MEO.MemberId = " + M + " ORDER BY MEO.ObservationDateTime DESC ";
                llenargrid2(sql, gvhistoriaobs);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        protected void btnvercali_Click(object sender, EventArgs e)
        {
            switch (ddlcategoriascal.SelectedValue)
            {
                case "ACTI":
                    sql = "SELECT cdMAT.DescSpanish AS ACTIVIDAD, dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, MA.Notes AS Observaciones, MA.UserId AS Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdMAT ON MA.Type = cdMAT.Code WHERE MA.RecordStatus = ' ' AND cdMAT.FunctionalArea = 'EDUC' AND MA.Project = '" + S + "' AND YEAR(MA.ActivityDateTime) = " + lblVanio.Text + " AND MA.MemberId = " + M + " ORDER BY MA.ActivityDateTime DESC ";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                case "HEDU":
                    sql = "SELECT MEY.SchoolYear Año, CdG.DescSpanish Grado, CdES.DescSpanish Estado, S.Name Escuela, cdEC.DescSpanish Carrera,  MEY.UserId 'Usuario' FROM	MemberEducationYear MEY INNER JOIN School S On MEY.Project=S.Project and MEY.SchoolCode=S.Code AND MEY.RecordStatus = S.RecordStatus INNER JOIN CdGrade CdG On MEY.Grade=CdG.Code INNER JOIN CdEducationStatus CdES On MEY.Status=CdES.Code LEFT JOIN CdEducationCareer cdEC ON cdEC.Code = MEY.Career WHERE MEY.RecordStatus=' ' AND MEY.Project = '" + S + "' AND MEY.MemberId = " + M + " GROUP BY MEY.SchoolYear, CdG.DescSpanish, CdEs.DescSpanish, S.Name, cdEC.DescSpanish, MEY.UserId ORDER BY MEY.SchoolYear DESC  ";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                case "HARE":
                    int days = 180;
                    DateTime actual = DateTime.Now;
                    sql = "SELECT Fecha, Actividad FROM fn_GEN_HistorialFamiliar('" + S + "', " + F + ", 'es', " + days.ToString() + ", '" + actual.ToString("MM/dd/yyyy HH:mm:ss TT") + "') WHERE Fecha IS NOT NULL UNION ALL SELECT FECHA, Actividad FROM fn_GEN_HistorialIndividual('" + S + "', " + M + ", 'es', " + days.ToString() + ", '" + actual.ToString("MM/dd/yyyy HH:mm:ss TT") + "') WHERE Fecha IS NOT NULL AND Area NOT IN ('BECA') ";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                case "TELE":
                    sql = "SELECT * FROM dbo.fn_GEN_ListaTelefonosFamilia('" + S + "', " + F + ") L";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                case "INFG":
                    sql = "SELECT * FROM dbo.fn_BECA_miscIngo('" + S + "'," + M + ")";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                case "HUTI":
                    sql = "SELECT   MEF.SchoolYear Año, CdST.DescSpanish TipoBolsa, CASE WHEN CdSSa.DescSpanish IS NULL THEN Cast(LeathersS as nvarchar(5)) +'-'+ MEF.LeatherSD ELSE Cast(LeathersS as nvarchar(5)) +'-'+ CdSSa.DescSpanish END as ZapatoCuero,CASE WHEN CdSSb.DescSpanish IS NULL THEN (Cast(TennisSS as nvarchar(5)) +'-'+ MEF.TennisSD) ELSE Cast(TennisSS as nvarchar(5)) +'-'+ CdSSb.DescSpanish END as ZapatoTenis, BlouseShirt BlusaCamisa, MEF.Polo, Cloth Tela, Sweater Sueter, Tshirt Playera, Jacket Chumpa, Pants, Vest Chaleco, S.name ProxEscuela, cdEC.DescSpanish ProxCarrera, MEF.UserId Usuario FROM MemberEducationFair MEF LEFT JOIN CdSchoolSuppliesTypeBag CdST ON MEF.SchoolSuppliesTypeBag = CdST.Code LEFT JOIN CdShoeStyle CdSSa ON CdSSa.Code=MEF.LeatherSD LEFT JOIN CdShoeStyle CdSSb ON CdSSb.Code=MEF.TennisSD LEFT JOIN dbo.CdEducationCareer cdEC ON cdEC.Code = MEF.NextCareer  LEFT JOIN dbo.School S ON S.Code = MEF.NextSchool AND S.Project = MEF.Project AND S.RecordStatus = MEF.RecordStatus WHERE  MEF.Project='" + S + "' and MEF.RecordStatus=' ' and MEF.MemberId=" + M + " ORDER BY MEF.SchoolYear DESC ";
                    llenargrid(sql, gvhistotirialcal);
                    break;
                default:
                    sql = "SELECT dbo.fn_EDUC_semaforoEsp('" + S + "', " + M + ", " + lblVanio.Text + ", MECG.Unit) AS Semáforo, MECG.Unit AS Unidad, cdS.DescSpanish AS Fuente, MECG.TotalClasses 'Total Clases', CASE WHEN MECG.ApprovedAll = 1 THEN 'SI' ELSE 'NO' END AS 'Ganó todas', MECG.Average80 AS 'Promedio (>80%)', MECG.FailedClasses AS 'Cantidad Pérdidas', (SELECT COUNT(*) FROM MemberEducationClassFailed AS MECF WHERE RecordStatus = ' ' AND Ref = MECG.Ref) AS Registradas, MECG.Notes Notas, MECG.UserId Usuario FROM dbo.MemberEducationClassGrade AS MECG INNER JOIN dbo.CdSchoolGradeSource AS cdS ON MECG.Source = cdS.Code WHERE MECG.RecordStatus = ' ' AND MECG.Project = '" + S + "' AND MECG.MemberId = " + M + " AND MECG.SchoolYear = " + lblVanio.Text + " ORDER BY MECG.Unit ";
                    llenargrid(sql, gvhistotirialcal);
                    break;
            }
        }

        protected void btntoposrem_Click(object sender, EventArgs e)
        {
            sql = "SELECT Tipo, SUM(Cantidad) Total, SUM([Cantidad Aprobada]) Aprobado FROM dbo.fn_GEN_HistorialPagos('" + S + "', " + M + ") WHERE YEAR(FechaA) = " + lblVanio.Text + " AND Area = 'EDUC' GROUP BY  Tipo ORDER BY  Tipo ";
            llenargrid(sql, gvrem);
            if (gvrem.Rows.Count == 0)
            {
                mst.mostrarMsjNtf(dic.msjNoEncontroResultados);
            }
        }

        protected void btndetallesrem_Click(object sender, EventArgs e)
        {
            sql = "SELECT Tipo, SUM(Cantidad) Total, SUM([Cantidad Aprobada]) Aprobado FROM dbo.fn_GEN_HistorialPagos('" + S + "', " + M + ") WHERE YEAR(FechaA) = " + lblVanio.Text + " AND Area = 'EDUC' GROUP BY  Tipo ORDER BY  Tipo";
            llenargrid(sql, gvrem);
            if (gvrem.Rows.Count == 0)
            {
                mst.mostrarMsjNtf(dic.msjNoEncontroResultados);
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
            if (ddlestado.SelectedItem.Text != listTable.Rows[0]["EstadoAfil"].ToString())
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
                accion = 1;
                mst.mostrarMsjOpcionesMdl("Ha modificado los siguientes campos: " + cambios + ". ¿Desea Continuar? ");
            }
            else
            {
                mst.mostrarMsjAdvNtf("No se ha modificado la información");
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
        protected void accionar(object sender, EventArgs e)
        {
            if (accion == 1)
            {
                DataTable listTable = new DataTable();
                sql = "SELECT * FROM dbo.fn_GEN_InfoGenMiembro('" + S + "', " + M + ", " + lblVanio.Text + ") L ";
                LlenarDataTable(sql, listTable);
                string certificado;
                DateTime actual = DateTime.Now;
                if (chktienecertificado.Checked == true) { certificado = "1"; } else { certificado = "0"; }
                sql = "SELECT Code FROM dbo.CdGrade WHERE ValidValue = 1 AND(DescSpanish = '" + listTable.Rows[0]["Grado"].ToString() + "' OR DescEnglish = '" + listTable.Rows[0]["Grado"].ToString() + "') ORDER BY Orden";
                string codigogrado = obtienePalabra(sql, "Code");
                sql = "SELECT Code FROM CdEducationStatus  WHERE ValidValue = 1 AND (DescSpanish='" + listTable.Rows[0]["EstadoAfil"].ToString() + "' OR DescEnglish='" + listTable.Rows[0]["EstadoAfil"].ToString() + "')";
                string codigoestado = obtienePalabra(sql, "Code");
                Response.Write(sql);
                sql = "INSERT INTO  dbo.MemberEducationYear SELECT Project, MemberId, SchoolYear, '" + ddlescuela.SelectedValue + "' SchoolCode, '" + ddlgrado.SelectedValue + "' Grade, '" + actual.ToString("MM/dd/yyyy HH:mm:ss TT") + "' CreationDateTime, RecordStatus,'" + U + "' UserId, ExpirationDateTime, ClassSection, PercentOfExpensesToPay, '" + ddlexestado.SelectedValue + "' ReasonNotToContinue, '" + ddlestado.SelectedValue + "' Status, '" + ddlcarrera.SelectedValue + "' Career, SingleTeacher, TransportationStartDate, TransportationEndDate, Notes, ExceptionPercent, '" + certificado + "' HasCertificate, NYSPackage, Typing FROM  dbo.MemberEducationYear WHERE RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND SchoolYear = " + lblVanio.Text + " AND Grade = '" + codigogrado + "' AND Status = '" + codigoestado + "' AND SchoolCode = '" + listTable.Rows[0]["EscuelaId"].ToString() + "'";
                APD.ejecutarSQL(sql);

                //inactiva el historial del record
                sql = "UPDATE dbo.MemberEducationYear SET RecordStatus = 'H', ExpirationDateTime = '" + actual.ToString("MM/dd/yyyy HH:mm:ss TT") + " ' WHERE  RecordStatus = ' ' AND Project = '" + S + "' AND MemberId = " + M + " AND schoolYear = " + lblVanio.Text + "  AND schoolCode = '" + listTable.Rows[0]["EscuelaId"].ToString() + "' AND grade = '" + codigogrado + "' AND Convert(nvarchar(30), CreationDateTime, 20) = '" + CreationDateL.Text + "' ";
                APD.ejecutarSQL(sql);
            }
            if (accion == 2)
            {
                try
                {
                    sql = "UPDATE dbo.MemberEducationObservation SET RecordStatus = 'H', ExpirationDateTime = GETDATE(), Observation = Observation + ' (" + U + ")' WHERE RecordStatus = ' ' AND IdObservation = " + idObsL.Text + " ";
                    APD.ejecutarSQL(sql);
                    historialobs();
                    mst.mostrarMsjNtf(dic.RegistroEliminadoAPAD);
                    idObsL.Text = "";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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
                return "";
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }

        protected void gvhistoriaobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.categoria;
                e.Row.Cells[1].Text = dic.fecha;
                e.Row.Cells[2].Text = dic.observaciones;
                e.Row.Cells[3].Text = dic.usuario;
                e.Row.Cells[5].Text = dic.accion;
            }
            foreach (GridViewRow gvr in gvhistoriaobs.Rows)
            {
                Button btn = new Button();
                btn = (Button)gvr.FindControl("btnmodificar");

                Button btn2 = new Button();
                btn2 = (Button)gvr.FindControl("btneliminar");

                btn.Text = dic.actualizar;
                btn2.Text = dic.eliminar;
            }
        }

        protected void btnguardarobs_Click(object sender, EventArgs e)
        {
            if (btnguardarobs.Text == dic.actualizar)
            {
                if (ddltipoobs.SelectedIndex == 0 && string.IsNullOrEmpty(txtnotasobs.Text))
                {
                    mst.mostrarMsjAdvNtf("Debe llenar los dos campos");
                }
                else
                {
                    string categoria, notas;
                    categoria = ddltipoobs.SelectedValue;
                    notas = txtnotasobs.Text;
                    sql = "INSERT INTO dbo.MemberEducationObservation SELECT IdObservation, GETDATE(), Project, MemberId, Category, ObservationDateTime, '" + notas + "', RecordStatus, '" + U + "', ExpirationDateTime FROM dbo.MemberEducationObservation WHERE RecordStatus = ' ' AND IdObservation = " + idObsL.Text;
                    APD.ejecutarSQL(sql);
                    ddltipoobs.Enabled = true;
                    ddltipoobs.SelectedIndex = 0;
                    txtnotasobs.Text = "";
                    historialobs();
                    mst.mostrarMsjNtf(dic.RegistroModificadoAPAD);
                    idObsL.Text = "";
                    btnguardarobs.Text = dic.guardar;
                }
            }
            if (btnguardarobs.Text == dic.guardar)
            {
                if (ddltipoobs.SelectedIndex == 0 && string.IsNullOrEmpty(txtnotasobs.Text))
                {
                    mst.mostrarMsjAdvNtf("Debe llenar los dos campos");
                }
                else
                {
                    int n;
                    sql = "SELECT MAX(IdObservation) 'Ultimo' FROM MemberEducationObservation ";
                    n = ObtenerEntero(sql, "Ultimo") + 1;
                    DateTime fecha = DateTime.Now;

                    sql = "INSERT INTO MemberEducationObservation VALUES (" + n.ToString() + ", GETDATE(), '" + S + "', " + M + ", '" + ddltipoobs.SelectedValue + "', GETDATE(), '" + txtnotasobs.Text + "', ' ', '" + U + "', NULL)";
                    APD.ejecutarSQL(sql);
                    ddltipoobs.SelectedIndex = 0;
                    txtnotasobs.Text = "";
                    historialobs();
                    mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                }
            }
        }

        protected void gvhistoriaobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "mod")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;

                idObsL.Text = clickedRow.Cells[4].Text;
                string categoria, notas, codigocategoria;
                notas = clickedRow.Cells[2].Text;
                categoria = clickedRow.Cells[0].Text;
                sql = "SELECT Code FROM dbo.CdMemberEducObservationCategory WHERE Active = 1 AND (DescEnglish='" + categoria + "' OR DescSpanish='" + categoria + "') ORDER BY DescSpanish ";
                codigocategoria = obtienePalabra(sql, "Code");
                ddltipoobs.SelectedValue = codigocategoria;
                txtnotasobs.Text = notas;
                btnguardarobs.Text = dic.actualizar;
                ddltipoobs.Enabled = false;
            }
            if (e.CommandName == "del")
            {
                var clickedButton = e.CommandSource as Button;
                var clickedRow = clickedButton.NamingContainer as GridViewRow;
                idObsL.Text = clickedRow.Cells[4].Text;
                accion = 2;
                mst.mostrarMsjOpcionesMdl("¿Desea eliminar esta observacion?");

            }
        }

        protected void btnguardarcal_Click(object sender, EventArgs e)
        {
            DateTime actual = DateTime.Now;
            sql = "INSERT INTO dbo.MemberActivity VALUES('" + S + "', " + M + ", '" + ddltipocal.SelectedValue + "', GETDATE(), GETDATE(), ' ', '" + U + "', NULL, '" + txtnotascal.Text + "')";
            APD.ejecutarSQL(sql);
            Response.Write(sql);
            ddltipocal.SelectedIndex = 0;
            txtnotascal.Text = "";
            sql = "SELECT cdMAT.DescSpanish AS ACTIVIDAD, dbo.fn_GEN_FormatDate(MA.ActivityDateTime, 'ES') AS Fecha, MA.Notes AS Observaciones, MA.UserId AS Usuario FROM dbo.MemberActivity MA INNER JOIN dbo.CdMemberActivityType cdMAT ON MA.Type = cdMAT.Code WHERE MA.RecordStatus = ' ' AND cdMAT.FunctionalArea = 'EDUC' AND MA.Project = '" + S + "' AND YEAR(MA.ActivityDateTime) = " + lblVanio.Text + " AND MA.MemberId = " + M + " ORDER BY MA.ActivityDateTime DESC ";
            llenargrid(sql, gvhistotirialcal);

        }
    }
}