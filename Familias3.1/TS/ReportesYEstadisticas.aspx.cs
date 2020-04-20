using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.TS
{
    public partial class ReporteActividadesFamiliares : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected static String filtro;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bdTS = new BDTS();
                bdGEN = new BDGEN();
                BDF = new BDFamilia();
                F = mast.F;
                S = mast.S;
                U = mast.U;
                L = mast.L;
                dic = new Diccionario(L, S);
                try
                {
                    llenarElementos();
                    visibilizarPestaña(pnlReporte, lnkReporte);
                }
                catch
                {
                }
            }
            DataTable dtTS = bdGEN.obtenerTS(S);
            mst = (mast)Master;
        }

        protected void lnkReporte_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlReporte, lnkReporte);
        }

        protected void lnkEstadisticas_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlEstadisticas, lnkEstadisticas);
        }

        protected void lnkFamiliasTS_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlFamiliasTS, lnkFamiliasTS);
            try
            {
                fmlLlenarFormFamiliasTS();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void ocultarPestañas()
        {
            pnlReporte.Visible = false;
            pnlEstadisticas.Visible = false;
            lnkReporte.CssClass = "cabecera";
            lnkEstadisticas.CssClass = "cabecera";
            lnkFamiliasTS.CssClass = "cabecera";
            asignaColores();
        }

        protected void asignaColores()
        {
            lnkReporte.CssClass = lnkReporte.CssClass + " blueCbc";
            lnkEstadisticas.CssClass = lnkEstadisticas.CssClass + " pinkCbc";
            lnkFamiliasTS.CssClass = lnkFamiliasTS.CssClass + " greenCbc";
        }

        protected void visibilizarPestaña(Panel pnl, LinkButton lnk)
        {
            pnl.Visible = true;
            lnk.CssClass = lnk.CssClass + " c-activa";
        }

        protected void llenarElementos()
        {
            asignaColores();
            lnkReporte.Text = dic.TSinformeVisitas;
            lnkFamiliasTS.Text = dic.TSinformeFamilias;
            lnkEstadisticas.Text = dic.TSestadisticasVisitas;
            rprtLlenarFormReporte();
            estLlenarFormEstadisticas();
            fmlLlenarFormFamiliasTS();
        }

        protected void rprtLlenarFormReporte()
        {
            lblArea.Text = dic.area + ":";
            lblTipoVisita.Text = dic.TStipoVisita + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblObjetivo.Text = dic.TSenfoque + ":";
            lblFecha.Text = dic.fecha + ":";
            btnBuscar.Text = dic.buscar;
            llenarMeses(ddlMesFecha);
            DateTime hoy = DateTime.Today;
            DateTime fecha3MesAnterior = hoy.AddMonths(-3);
            txbDiaFecha.Text = fecha3MesAnterior.Day + "";
            ddlMesFecha.SelectedValue = fecha3MesAnterior.Month + "";
            txbAñoFecha.Text = fecha3MesAnterior.Year + "";
            llenarAreas();
            llenarTS();
            llenarTiposVst();
            llenarObjetivos();
            btnNuevaBusqueda.Text = dic.nuevaBusqueda;
        }

        protected void estLlenarFormEstadisticas()
        {
            llenarMeses(ddlMesFechaInicio);
            llenarMeses(ddlMesFechaFinal);
            lblFechaInicio.Text = dic.fechaInicio + ":";
            lblFechaFinal.Text = dic.fechaFin + ":";
            btnGenerar.Text = dic.generar;
            DateTime hoy = DateTime.Today;
            DateTime fechaMesAnterior = hoy.AddMonths(-1);
            txbDiaFechaInicio.Text = "1";
            ddlMesFechaInicio.SelectedValue = fechaMesAnterior.Month + "";
            txbAñoFechaInicio.Text = fechaMesAnterior.Year + "";
            txbDiaFechaFinal.Text = DateTime.DaysInMonth(fechaMesAnterior.Year, fechaMesAnterior.Month) + "";
            ddlMesFechaFinal.SelectedValue = fechaMesAnterior.Month + "";
            txbAñoFechaFinal.Text = fechaMesAnterior.Year + "";
        }

        protected void fmlLlenarFormFamiliasTS()
        {
            TableRow tblAreas = new TableRow();
            DataTable dtAreas = bdGEN.obtenerAreas(S, "");
            dtAreas.Rows.Add(dic.total, dic.total);
            TableCell dtPrimero = new TableCell();
            dtPrimero.CssClass = "center";
            tblAreas.Cells.Add(dtPrimero);
            foreach (DataRow rowArea in dtAreas.Rows)
            {
                TableCell tblCeldaArea = new TableCell();
                tblCeldaArea.Width = 20;
                String area = rowArea["Code"].ToString();
                String descripcionArea = rowArea["Des"].ToString();
                String areaVertical = "";
                for (int i = 0; i < area.Length; i++)
                {
                    areaVertical = areaVertical + area[i] + "</br>";
                }
                tblCeldaArea.Text = areaVertical;
                tblCeldaArea.ToolTip = descripcionArea;
                tblCeldaArea.CssClass = "center";
                tblAreas.Cells.Add(tblCeldaArea);
            }
            tblFamilias.Rows.Add(tblAreas);
            DataTable dtTS = bdGEN.obtenerTS(S);
            dtTS.Rows.Add("*");
            foreach (DataRow rowTS in dtTS.Rows)
            {
                TableRow tblRowTS = new TableRow();
                TableCell tblCeldaTS = new TableCell();
                String TS = rowTS["EmployeeId"].ToString();
                if (TS.Equals("*"))
                {
                    tblCeldaTS.Text = dic.TSfamiliasconTS + ":";
                }
                else
                {
                    tblCeldaTS.Text = TS + ":";
                }
                tblCeldaTS.CssClass = "left";
                tblRowTS.Cells.Add(tblCeldaTS);
                DataTable dtFamiliasPorArea = bdTS.famPorTSPorAldea(S, TS);
                foreach (DataRow rowAreaTS in dtFamiliasPorArea.Rows)
                {
                    TableCell tblCeldaArea = new TableCell();
                    String areaTS = rowAreaTS["Code"].ToString();
                    String num = rowAreaTS["Num"].ToString();
                    if (num.Equals("0"))
                    {
                        num = "";
                    }
                    tblCeldaArea.Text = num;
                    tblRowTS.Cells.Add(tblCeldaArea);
                }
                DataTable dtFamiliasTotal = bdTS.famPorTS(S, TS);
                TableCell tblCeldaTotalPorTS = new TableCell();
                String numTotalPorTS = dtFamiliasTotal.Rows[0]["Num"].ToString();
                if (numTotalPorTS.Equals("0"))
                {
                    numTotalPorTS = "";
                }
                tblCeldaTotalPorTS.Text = numTotalPorTS;
                tblRowTS.Cells.Add(tblCeldaTotalPorTS);
                tblFamilias.Rows.Add(tblRowTS);
            }

            TableRow tblRowsinTS = new TableRow();
            TableCell tblCeldaSinTSNombre = new TableCell();
            tblCeldaSinTSNombre.CssClass = "left";
            tblCeldaSinTSNombre.Text = dic.TSfamiliassinTS + ":";
            tblRowsinTS.Cells.Add(tblCeldaSinTSNombre);
            DataTable dtSinTS = bdTS.famSinTS(S);
            foreach (DataRow rowSinTS in dtSinTS.Rows)
            {
                TableCell tblCeldasinTS = new TableCell();
                String num = rowSinTS["Num"].ToString();
                if (num.Equals("0"))
                {
                    num = "";
                }
                tblCeldasinTS.Text = num;
                tblRowsinTS.Cells.Add(tblCeldasinTS);
            }
            TableCell tblCeldasinTSTotal = new TableCell();
            String numTotalSinTS = bdTS.famTotalSinTS(S);
            tblCeldasinTSTotal.Text = numTotalSinTS;
            tblRowsinTS.Cells.Add(tblCeldasinTSTotal);
            tblFamilias.Rows.Add(tblRowsinTS);



            TableRow tblRowsTotalporArea = new TableRow();
            TableCell tblCeldaTotalporArea = new TableCell();
            tblCeldaTotalporArea.CssClass = "left";
            tblCeldaTotalporArea.Text = dic.total + ":";
            tblRowsTotalporArea.Cells.Add(tblCeldaTotalporArea);
            DataTable dtTotalporArea = bdTS.famTotalFamiliasporArea(S);
            foreach (DataRow rowTotalporArea in dtTotalporArea.Rows)
            {
                TableCell tblCeldaTotalPorArea = new TableCell();
                String num = rowTotalporArea["Num"].ToString();
                tblCeldaTotalPorArea.Text = num;
                tblRowsTotalporArea.Cells.Add(tblCeldaTotalPorArea);
            }
            TableCell tblCeldaTotal = new TableCell();
            String numTotal = bdTS.famTotal(S);
            tblCeldaTotal.Text = numTotal;
            tblRowsTotalporArea.Cells.Add(tblCeldaTotal);
            tblFamilias.Rows.Add(tblRowsTotalporArea);
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            String strDiaFechaInicio = txbDiaFechaInicio.Text;
            String strMesFechaInicio = ddlMesFechaInicio.Text;
            String strAñoFechaInicio = txbAñoFechaInicio.Text;
            String strDiaFechaFinal = txbDiaFechaFinal.Text;
            String strMesFechaFinal = ddlMesFechaFinal.Text;
            String strAñoFechaFinal = txbAñoFechaFinal.Text;
            if ((!strDiaFechaInicio.Equals("")) && (!strMesFechaInicio.Equals("")) && (!strAñoFechaInicio.Equals("")) && (!strDiaFechaFinal.Equals("")) && (!strMesFechaFinal.Equals("")) && (!strAñoFechaFinal.Equals("")))
            {
                if (validarFechas(strAñoFechaInicio, strMesFechaInicio, strDiaFechaInicio) && validarFechas(strAñoFechaFinal, strMesFechaFinal, strDiaFechaFinal))
                {
                    int intDiaFechaInicio = Int32.Parse(strDiaFechaInicio);
                    int intMesFechaInicio = Int32.Parse(strMesFechaInicio);
                    int intAñoFechaInicio = Int32.Parse(strAñoFechaInicio);
                    int intDiaFechaFin = Int32.Parse(strDiaFechaFinal);
                    int intMesFechaFin = Int32.Parse(strMesFechaFinal);
                    int intAñoFechaFin = Int32.Parse(strAñoFechaFinal);
                    DateTime fechaInicio = new DateTime(intAñoFechaInicio, intMesFechaInicio, intDiaFechaInicio);
                    DateTime fechaFin = new DateTime(intAñoFechaFin, intMesFechaFin, intDiaFechaFin);
                    if (fechaInicio < fechaFin)
                    {
                        estLlenarEstadisticas(fechaInicio, fechaFin);
                    }
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("La Fecha de Inicio no puede ser después, ni igual que la Fecha de Finalización.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("The Start Date cannot be after, nor the same as the End Date.");
                        }
                    }
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Por favor, asegurese de que ambas fechas sean correctas.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("Please, make sure both dates are correct.");
                    }
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, ingrese ambas fechas.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, enter both dates.");
                }
            }
        }

        protected void estLlenarEstadisticas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dtTiposVisitas = bdTS.vstObtenerTipos(S, L);
            dtTiposVisitas.Rows.Add("Total", "Total");
            DataTable dtTS = bdGEN.obtenerTS2(S);
            dtTS.Rows.Add(dic.otros);
            dtTS.Rows.Add("Total");

            TableRow tblRowTitulo = new TableRow();
            TableCell tblCeldaTitulo = new TableCell();
            tblCeldaTitulo.Text = dic.actividadesFamiliares;
            tblCeldaTitulo.CssClass = "center";
            tblCeldaTitulo.ColumnSpan = dtTiposVisitas.Rows.Count + 2;
            tblRowTitulo.Cells.Add(tblCeldaTitulo);
            tblEstadisticas.Rows.Add(tblRowTitulo);

            Table tblEstadisticas2N = new Table();
            TableRow tblRowTSTitulo = new TableRow();
            TableCell tblCeldaTSTitulo = new TableCell();
            tblCeldaTSTitulo.RowSpan = 3;
            tblCeldaTSTitulo.Text = dic.trabajadorS;
            tblCeldaTSTitulo.CssClass = "center";
            tblRowTSTitulo.Cells.Add(tblCeldaTSTitulo);
            tblEstadisticas2N.Rows.Add(tblRowTSTitulo);

            TableRow tblRowActividad = new TableRow();
            TableCell tblCeldaActividad = new TableCell();
            tblCeldaActividad.Text = dic.actividad;
            tblCeldaActividad.CssClass = "center";
            tblCeldaActividad.ColumnSpan = dtTiposVisitas.Rows.Count + 1;
            tblRowActividad.Cells.Add(tblCeldaActividad);
            tblEstadisticas2N.Rows.Add(tblRowActividad);

            TableRow tblRowTipoVisita = new TableRow(); ;
            foreach (DataRow rowTipo in dtTiposVisitas.Rows)
            {
                TableCell tblCeldaTipoVisita = new TableCell();
                tblCeldaTipoVisita.Text = rowTipo["Des"].ToString();
                tblCeldaTipoVisita.CssClass = "center";
                tblRowTipoVisita.Cells.Add(tblCeldaTipoVisita);
            }
            tblEstadisticas2N.Rows.Add(tblRowTipoVisita);


            foreach (DataRow rowTSInfo in dtTS.Rows)
            {
                TableRow tblRowTSInfo = new TableRow();
                TableCell tblCeldaTS = new TableCell();
                String TS = rowTSInfo["EmployeeId"].ToString();
                tblCeldaTS.Text = TS + ":";
                tblCeldaTS.CssClass = "left";
                tblRowTSInfo.Cells.Add(tblCeldaTS);

                foreach (DataRow rowTipo in dtTiposVisitas.Rows)
                {
                    TableCell tblCeldaNumeroVisitas = new TableCell();
                    String tipoVisita = rowTipo["Code"].ToString();
                    tblCeldaNumeroVisitas.Text = bdTS.estObtenerNumeroVisitas(S, TS, tipoVisita, fechaInicio.ToString("yyyy-MM-dd"), fechaFin.ToString("yyyy-MM-dd"));
                    tblRowTSInfo.Cells.Add(tblCeldaNumeroVisitas);
                }
                tblEstadisticas2N.Rows.Add(tblRowTSInfo);
            }

            TableRow tblRowEstadisticas2N = new TableRow();
            TableCell tblCeldaEstadisticas2N = new TableCell();
            tblCeldaEstadisticas2N.Controls.Add(tblEstadisticas2N);
            tblRowEstadisticas2N.Cells.Add(tblCeldaEstadisticas2N);
            tblEstadisticas.Rows.Add(tblRowEstadisticas2N);

        }
        protected void llenarAreas()
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerAreas(S, L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlArea.Items.Add(item);
            }
        }

        protected void llenarObjetivos()
        {
            ddlObjetivo.Items.Clear();
            ddlObjetivo.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerObjetivosVisita(S, L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlObjetivo.Items.Add(item);
            }
        }

        protected void llenarTiposVst()
        {
            ddlTipoVisita.Items.Clear();
            ddlTipoVisita.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdTS.vstObtenerTipos(S, L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlTipoVisita.Items.Add(item);
            }
            ddlTipoVisita.SelectedValue = "VICA";
        }

        protected void llenarTS()
        {
            ddlTS.Items.Clear();
            ddlTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS(S);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["EmployeeId"].ToString();
                Tipo = row["EmployeeId"].ToString();
                item = new ListItem(Tipo, Code);
                ddlTS.Items.Add(item);
            }
        }

        protected void llenarGdvFamilias()
        {
            String strDiaFecha = txbDiaFecha.Text;
            String strMesFecha = ddlMesFecha.Text;
            String strAñoFecha = txbAñoFecha.Text;
            Boolean fechaCorrecta = true;

            if ((!strDiaFecha.Equals("")) && (!strMesFecha.Equals("")) && (!strAñoFecha.Equals("")))
            {
                if (!validarFechas(strAñoFecha, strMesFecha, strDiaFecha))
                {
                    fechaCorrecta = false;
                }
            }
            else
            {
                fechaCorrecta = false;
            }
            if (fechaCorrecta)
            {
                String area = ddlArea.SelectedValue;
                String TS = ddlTS.SelectedValue;
                String tipoVisita = ddlTipoVisita.SelectedValue;
                String objetivoVisita = ddlObjetivo.SelectedValue;
                int intDiaFecha = Int32.Parse(strDiaFecha);
                int intMesFecha = Int32.Parse(strMesFecha);
                int intAñoFecha = Int32.Parse(strAñoFecha);
                DateTime fecha = new DateTime(intAñoFecha, intMesFecha, intDiaFecha);
                DataTable dtFamilias = bdTS.rprtObtenerFamilias(S, L, area, TS, tipoVisita, objetivoVisita, fecha.ToString("yyyy-MM-dd"), filtro);
                if (dtFamilias.Rows.Count > 0)
                {
                    gdvFamilias.Columns[0].Visible = true;
                    gdvFamilias.Columns[1].HeaderText = dic.familia;
                    gdvFamilias.Columns[2].HeaderText = dic.direccion;
                    gdvFamilias.Columns[3].HeaderText = dic.area;
                    gdvFamilias.Columns[4].HeaderText = dic.jefeCasa;
                    gdvFamilias.Columns[5].HeaderText = dic.TSNoApadrinadosFaseII;
                    gdvFamilias.Columns[6].HeaderText = dic.fechaUltimaVisita;
                    gdvFamilias.Columns[7].HeaderText = dic.TStipoVisita;
                    gdvFamilias.Columns[8].HeaderText = dic.trabajadorS;
                    gdvFamilias.Columns[9].HeaderText = dic.TSenfoque;
                    gdvFamilias.DataSource = dtFamilias;
                    gdvFamilias.DataBind();
                    gdvFamilias.Columns[0].Visible = false;
                    lblTotal.Text = "Total: " + dtFamilias.Rows.Count;
                    pnlBusquedaFamilia.Visible = false;
                    pnlMostrar.Visible = true;
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroResultados);
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, asegurese de que la fecha sea correcta.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, make sure the date is correct.");
                }
            }
        }
        protected void llenarMeses(DropDownList ddlMes)
        {
            ddlMes.Items.Clear();
            ddlMes.Items.Add(new ListItem("", ""));
            DataTable tblMeses = new DataTable();
            tblMeses.Columns.Add("Code", typeof(String));
            tblMeses.Columns.Add("Month", typeof(String));
            if (L.Equals("es"))
            {
                tblMeses.Rows.Add("1", "Ene");
                tblMeses.Rows.Add("2", "Feb");
                tblMeses.Rows.Add("3", "Mar");
                tblMeses.Rows.Add("4", "Abr");
                tblMeses.Rows.Add("5", "May");
                tblMeses.Rows.Add("6", "Jun");
                tblMeses.Rows.Add("7", "Jul");
                tblMeses.Rows.Add("8", "Ago");
                tblMeses.Rows.Add("9", "Sep");
                tblMeses.Rows.Add("10", "Oct");
                tblMeses.Rows.Add("11", "Nov");
                tblMeses.Rows.Add("12", "Dic");
            }
            else
            {
                tblMeses.Rows.Add("1", "Jan");
                tblMeses.Rows.Add("2", "Feb");
                tblMeses.Rows.Add("3", "Mar");
                tblMeses.Rows.Add("4", "Apr");
                tblMeses.Rows.Add("5", "May");
                tblMeses.Rows.Add("6", "Jun");
                tblMeses.Rows.Add("7", "Jul");
                tblMeses.Rows.Add("8", "Agu");
                tblMeses.Rows.Add("9", "Sep");
                tblMeses.Rows.Add("10", "Oct");
                tblMeses.Rows.Add("11", "Nov");
                tblMeses.Rows.Add("12", "Dec");
            }
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblMeses.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Month"].ToString();
                item = new ListItem(Des, Code);
                ddlMes.Items.Add(item);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            filtro = "T";
            llenarGdvFamilias();
        }
        protected void btnBuscarNecesita_Click(object sender, EventArgs e)
        {
            filtro = "N";
            llenarGdvFamilias();
        }
        protected void btnBuscarRealizado_Click(object sender, EventArgs e)
        {
            filtro = "R";
            llenarGdvFamilias();
        }
        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            pnlMostrar.Visible = false;
            pnlBusquedaFamilia.Visible = true;
        }
        protected void gdvFamilias_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdFamilyId")
            {
                gdvFamilias.Columns[0].Visible = true;
                String F = gdvFamilias.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                gdvFamilias.Columns[0].Visible = false;
                mst.seleccionarFamilia(F);
                Response.Redirect("~/MISC/PerfilFamilia.aspx");
            }
        }
        protected Boolean validarFechas(String strYear, String strMonth, String strDay)
        {
            int year = Int32.Parse(strYear);
            int month = Int32.Parse(strMonth);
            int day = Int32.Parse(strDay);
            Boolean check = false;
            if (year <= DateTime.MaxValue.Year && year >= DateTime.MinValue.Year)
            {
                if (month >= 1 && month <= 12)
                {
                    if (DateTime.DaysInMonth(year, month) >= day && day >= 1)
                        check = true;
                }
            }
            return check;
        }
    }
}