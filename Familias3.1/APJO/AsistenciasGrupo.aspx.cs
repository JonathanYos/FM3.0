using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.APJO
{
    public partial class AsistenciasGrupo : System.Web.UI.Page
    {
        public static BDPROE bdPROE;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected static String programa;
        protected static String subPrograma;
        protected static String tipoActividad;
        public static Color colorBueno = Color.MediumSeaGreen;
        public static Color colorRegular = Color.Yellow;
        public static Color colorMalo = Color.Crimson;
        protected static String miembroSLCT;
        protected static String familia;
        protected static String fechaAsistenciaSLCT;
        protected static Boolean seleccionarFecha;
        protected static Boolean mostrarTodasActividades;
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarAsistencia);
            if (!IsPostBack)
            {
                bdPROE = new BDPROE();
                F = mast.F;
                S = mast.S;
                U = mast.U;
                L = mast.L;
                try
                {
                    dic = new Diccionario(L, S);
                    valoresIniciales();
                }
                catch
                {
                }
            }
        }
        public void valoresIniciales()
        {
            seleccionarFecha = false;
            mostrarTodasActividades = false;
            txbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txbFecha.Enabled = false;
            txbFecha.CssClass = "textBoxBlueForm date noPaste";
            txbImpresiones.MaxLength = 3;
            txbActImpresiones.MaxLength = 3;
            llenarValoresPredeterminados();
        }
        public void llenarValoresPredeterminados()
        {
            DataTable dt = bdPROE.obtenerValoresPredeterminados(S, U);
            if (dt.Rows.Count > 0)
            {
                llenarProgramas();
                ddlPrograma.SelectedValue = programa = dt.Rows[0]["DefaultProgram"].ToString();
                llenarSubProgramas();
                ddlSubPrograma.SelectedValue = subPrograma = dt.Rows[0]["DefaultSubProgram"].ToString();
                llenarActividades();
            }
            ddlPrograma.Enabled = ddlPrograma.Items.Count == 2 ? false : true;
            ddlPrograma.CssClass = "comboBoxBlueForm";
            ddlSubPrograma.Enabled = (ddlSubPrograma.Items.Count == 2) && (ddlPrograma.Items.Count == 2) ? false : true;
            ddlSubPrograma.CssClass = "comboBoxBlueForm";
        }
        protected void llenarProgramas()
        {
            ddlPrograma.Items.Clear();
            ddlPrograma.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdPROE.obtenerProgramas(S, L, U);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlPrograma.Items.Add(item);
            }
        }

        protected void llenarSubProgramas()
        {
            ddlSubPrograma.Items.Clear();
            ddlSubPrograma.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdPROE.obtenerSubProgramas(S, L, programa, U);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlSubPrograma.Items.Add(item);
            }
        }

        protected void llenarActividades()
        {
            ddlActividad.Items.Clear();
            ddlActividad.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdPROE.obtenerActividades(S, L, programa, subPrograma, mostrarTodasActividades);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlActividad.Items.Add(item);
            }
        }

        protected void ddlPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            programa = ddlPrograma.SelectedValue;
            llenarSubProgramas();
        }

        protected void ddlSubPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            subPrograma = ddlSubPrograma.SelectedValue;
            llenarActividades();
        }
        protected void ddlActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoActividad = ddlActividad.SelectedValue;
            llenarGdvAsistencias();
            btnBuscar.Visible = !String.IsNullOrEmpty(tipoActividad) ? true : false;
            btnAsignarSalidaTodos.Visible = !String.IsNullOrEmpty(tipoActividad) ? true : false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
            mst.ocultarModalYContenido(pnlReferencias);
            mst.mostrarMsjNtf(dic.msjSeHanIngresado);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mst.ocultarModalYContenido(pnlActualizar);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGdvReferencias();
            pnlActualizar.Visible = false;
            if (gdvReferencias.Rows.Count > 0)
            {
                mst.mostrarModalYContenido(pnlReferencias);
            }
            else
            {
                mst.mostrarMsjAdvNtf("No se encontraron referencias.");
            }
        }
        protected void btnAsignarSalidaTodos_Click(object sender, EventArgs e)
        {
            gdvAsistencias.Columns[0].Visible = true;
            gdvAsistencias.Columns[1].Visible = true;
            gdvAsistencias.Columns[2].Visible = true;
            gdvAsistencias.Columns[3].Visible = true;
            String fechaSalida;
            String miembro;
            String fechaAsistencia;
            String impresiones;
            String comentarios;
            String fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!seleccionarFecha)
            {
                fechaSalida = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                DateTime fechaSalidaAux = Convert.ToDateTime(txbFecha.Text);
                DateTime horaSalidaAsistenciaAux = Convert.ToDateTime(txbHoraSalida.Text);
                String fechaSalidaAsistenciaAuxStr = fechaSalidaAux.Year + "-" + fechaSalidaAux.Month + "-" + fechaSalidaAux.Day + " " + horaSalidaAsistenciaAux.Hour + ":" + horaSalidaAsistenciaAux.Minute;
                fechaSalida = fechaSalidaAsistenciaAuxStr;
            }

            foreach (GridViewRow row in gdvAsistencias.Rows)
            {
                miembro = row.Cells[0].Text;
                fechaAsistencia = row.Cells[1].Text;
                impresiones = row.Cells[8].Text;
                comentarios = row.Cells[9].Text;
                fechaSalida = HttpUtility.HtmlDecode(fechaSalida.Replace("&nbsp;", ""));
                comentarios = HttpUtility.HtmlDecode(comentarios.Replace("&nbsp;", ""));
                bdPROE.ingresarAsistencia(S, miembro, fechaAsistencia, tipoActividad, fechaCreacion, U, comentarios, impresiones, fechaSalida);
            }
            gdvAsistencias.Columns[0].Visible = false;
            gdvAsistencias.Columns[1].Visible = false;
            gdvAsistencias.Columns[2].Visible = false;
            gdvAsistencias.Columns[3].Visible = false;
            llenarGdvAsistencias();
        }
        protected void llenarGdvReferencias()
        {
            String fechaActividad = !seleccionarFecha ? DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss") : Convert.ToDateTime(txbFecha.Text).ToString("yyyy-MM-dd HH:mm:ss");
            gdvReferencias.Columns[0].Visible = true;
            gdvReferencias.Columns[1].Visible = true;
            gdvReferencias.DataSource = bdPROE.obtenerReferencias(S, L, tipoActividad, fechaActividad);
            gdvReferencias.DataBind();
            gdvReferencias.Columns[0].Visible = false;
            gdvReferencias.Columns[1].Visible = false;
        }

        protected void guardar()
        {
            gdvReferencias.Columns[0].Visible = true;
            gdvReferencias.Columns[1].Visible = true;

            DateTime ahora = DateTime.Now;
            String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
            String fechaAsistencia;
            String fechaSalida;
            String comentarios = txbComentarios.Text;
            String impresiones = txbImpresiones.Text;
            if (!seleccionarFecha)
            {
                fechaAsistencia = ahora.ToString("yyyy-MM-dd HH:mm:ss");
                fechaSalida = "";
            }
            else
            {
                DateTime fechaAsistenciaAux = Convert.ToDateTime(txbFecha.Text);

                DateTime horaAsistenciaAux = Convert.ToDateTime(txbHora.Text);
                String fechaAsistenciaAuxStr = fechaAsistenciaAux.Year + "-" + fechaAsistenciaAux.Month + "-" + fechaAsistenciaAux.Day + " " + horaAsistenciaAux.Hour + ":" + horaAsistenciaAux.Minute;
                fechaAsistencia = fechaAsistenciaAuxStr;

                if (!String.IsNullOrEmpty(txbHoraSalida.Text))
                {
                    DateTime horaSalidaAsistenciaAux = Convert.ToDateTime(txbHoraSalida.Text);
                    String fechaSalidaAsistenciaAuxStr = fechaAsistenciaAux.Year + "-" + fechaAsistenciaAux.Month + "-" + fechaAsistenciaAux.Day + " " + horaSalidaAsistenciaAux.Hour + ":" + horaSalidaAsistenciaAux.Minute;
                    fechaSalida = fechaSalidaAsistenciaAuxStr;
                }
                else
                {
                    fechaSalida = "";
                }
            }
            String miembro;
            String actividadReferencia;
            String estadoReferencia;
            foreach (GridViewRow row in gdvReferencias.Rows)
            {
                CheckBox check = row.FindControl("chkModificar") as CheckBox;
                if (check.Checked)
                {
                    miembro = row.Cells[0].Text;
                    bdPROE.ingresarAsistencia(S, miembro, fechaAsistencia, tipoActividad, fechaCreacion, U, comentarios, impresiones, fechaSalida);
                    estadoReferencia = row.Cells[2].Text;
                    if (estadoReferencia.Equals("REFE"))
                    {
                        actividadReferencia = row.Cells[1].Text;
                        bdPROE.cambiarEstadoReferencia(S, miembro, actividadReferencia, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "ASIS", null, U);
                    }
                }
            }
            gdvReferencias.Columns[0].Visible = false;
            gdvReferencias.Columns[1].Visible = false;
            llenarGdvAsistencias();
            llenarGdvReferencias();
            mst.mostrarMsjNtf(dic.msjSeHanIngresado);
        }

        protected void gdvAsistencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gdvAsistencias.Columns[0].Visible = true;
            gdvAsistencias.Columns[1].Visible = true;
            gdvAsistencias.Columns[2].Visible = true;
            gdvAsistencias.Columns[3].Visible = true;
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            miembroSLCT = gdvAsistencias.Rows[numFilaEsp].Cells[0].Text;
            fechaAsistenciaSLCT = gdvAsistencias.Rows[numFilaEsp].Cells[1].Text;
            String nombre = gdvAsistencias.Rows[numFilaEsp].Cells[5].Text;
            String fechaSalida = gdvAsistencias.Rows[numFilaEsp].Cells[2].Text;
            String impresiones = gdvAsistencias.Rows[numFilaEsp].Cells[8].Text;
            String comentarios = gdvAsistencias.Rows[numFilaEsp].Cells[9].Text;
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    fechaSalida = HttpUtility.HtmlDecode(fechaSalida.Replace("&nbsp;", ""));
                    comentarios = HttpUtility.HtmlDecode(comentarios.Replace("&nbsp;", ""));
                    txbActHoraSalida.Text = String.IsNullOrEmpty(fechaSalida) ? "" : Convert.ToDateTime(fechaSalida).ToString("HH:mm");
                    chkSalida.Checked = String.IsNullOrEmpty(fechaSalida) ? false : true;
                    lblVNombre.Text = nombre;
                    txbActComentarios.Text = comentarios;
                    txbActImpresiones.Text = impresiones;
                    pnlReferencias.Visible = false;
                    mst.mostrarModalYContenido(pnlActualizar);
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            else if (e.CommandName == "cmdEliminar")
            {
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
            gdvAsistencias.Columns[0].Visible = false;
            gdvAsistencias.Columns[1].Visible = false;
            gdvAsistencias.Columns[2].Visible = false;
            gdvAsistencias.Columns[3].Visible = false;
        }

        protected void llenarGdvAsistencias()
        {
            String fechaActividad = !seleccionarFecha ? DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss") : Convert.ToDateTime(txbFecha.Text).ToString("yyyy-MM-dd HH:mm:ss");
            DataTable dtAsistencias = bdPROE.obtenerAsistencias(S, L, tipoActividad, fechaActividad);
            if (dtAsistencias.Rows.Count > 0)
            {
                pnlAsistencias.Visible = true;
                gdvAsistencias.Columns[0].Visible = true;
                gdvAsistencias.Columns[1].Visible = true;
                gdvAsistencias.Columns[2].Visible = true;
                gdvAsistencias.DataSource = dtAsistencias;
                gdvAsistencias.DataBind();
                gdvAsistencias.Columns[0].Visible = false;
                gdvAsistencias.Columns[1].Visible = false;
                gdvAsistencias.Columns[2].Visible = false;
            }
            else
            {
                pnlAsistencias.Visible = false;
                mst.mostrarMsjAdvNtf("No se encontraron asistencias.");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String fechaSalida;
            if (!seleccionarFecha)
            {
                fechaSalida = chkSalida.Checked ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "";
            }
            else
            {
                if (!String.IsNullOrEmpty(txbActHoraSalida.Text))
                {
                    DateTime horaSalidaAsistenciaAux = Convert.ToDateTime(txbActHoraSalida.Text);
                    DateTime fechaSalidaAsistenciaAux = Convert.ToDateTime(fechaAsistenciaSLCT);
                    String fechaSalidaAsistenciaAuxStr = fechaSalidaAsistenciaAux.Year + "-" + fechaSalidaAsistenciaAux.Month + "-" + fechaSalidaAsistenciaAux.Day + " " + horaSalidaAsistenciaAux.Hour + ":" + horaSalidaAsistenciaAux.Minute;
                    fechaSalida = fechaSalidaAsistenciaAuxStr;
                }
                else
                {
                    fechaSalida = "";
                }
            }

            String notas = txbActComentarios.Text;
            String impresiones = txbActImpresiones.Text;
            DateTime ahora = DateTime.Now;
            String fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bdPROE.ingresarAsistencia(S, miembroSLCT, fechaAsistenciaSLCT, tipoActividad, fechaCreacion, U, notas, impresiones, fechaSalida);
            llenarGdvAsistencias();
            mst.ocultarModalYContenido(pnlActualizar);
            mst.mostrarMsjNtf(dic.msjSeHaActualizado);
        }
        protected void btnCancelarAct_Click(object sender, EventArgs e)
        {
            mst.ocultarModalYContenido(pnlActualizar);
        }


        protected void eliminarAsistencia(object sender, EventArgs e)
        {
            try
            {
                bdPROE.recuperarEstadoReferencia(S, miembroSLCT, tipoActividad, U);
                bdPROE.ingresarHistorico(S, miembroSLCT, fechaAsistenciaSLCT, tipoActividad, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), U);
                llenarGdvAsistencias();
                llenarGdvReferencias();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnCambiarFecha_Click(object sender, EventArgs e)
        {
            if (!seleccionarFecha)
            {
                txbFecha.Enabled = true;
                lblHora.Visible = true;
                txbHora.Visible = true;
                lblHoraSalida.Visible = true;
                txbHoraSalida.Visible = true;

                txbActHoraSalida.Visible = true;
                lblActHoraSalida.Visible = true;
                chkSalida.Visible = false;
                lblSalida.Visible = false;
                btnCambiarFecha.Text = "Fecha y Hora del Sistema";

                seleccionarFecha = !seleccionarFecha;
            }
            else
            {
                txbFecha.Enabled = false;
                txbFecha.CssClass = "textBoxBlueForm date noPaste";
                lblHora.Visible = false;
                txbHora.Visible = false;
                lblHoraSalida.Visible = false;
                txbHoraSalida.Visible = false;

                txbActHoraSalida.Visible = false;
                lblActHoraSalida.Visible = false;
                chkSalida.Visible = true;
                lblSalida.Visible = true;
                btnCambiarFecha.Text = "Fecha y Hora Manual";

                seleccionarFecha = !seleccionarFecha;
                llenarGdvAsistencias();
                txbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void txbFecha_TextChanged(object sender, EventArgs e)
        {
            llenarGdvAsistencias();
            txbHora.Text = DateTime.Now.ToString("HH:mm");
        }
        protected void chkFiltroActividades_CheckedChanged(object sender, EventArgs e)
        {
            mostrarTodasActividades = !mostrarTodasActividades;
            lblMostrarTodasActividades.Text = mostrarTodasActividades ? "Mostrar Actividades del Día" : "Mostrar Todas las Actividades";
            llenarActividades();
            pnlAsistencias.Visible = false;
        }
    }
}