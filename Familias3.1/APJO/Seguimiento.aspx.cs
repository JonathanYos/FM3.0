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
    public partial class Seguimiento : System.Web.UI.Page
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
            tblTipos = bdPROE.obtenerActividades(S, L, programa, subPrograma, true);
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
            llenarGdvReferencias();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
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
       
        protected void llenarGdvReferencias()
        {
            DataTable dtReferencias = bdPROE.obtenerReferencias(S, L, tipoActividad);
            pnlReferencias.Visible = false;
            btnGuardar.Visible = false;
            if (dtReferencias.Rows.Count > 0)
            {
                pnlReferencias.Visible = true;
                btnGuardar.Visible = true;
                gdvReferencias.Columns[0].Visible = true;
                gdvReferencias.Columns[1].Visible = true;
                gdvReferencias.DataSource = dtReferencias;
                gdvReferencias.DataBind();
                gdvReferencias.Columns[0].Visible = false;
                gdvReferencias.Columns[1].Visible = false;
            }
            else
            {
                mst.mostrarMsjAdvNtf("No se encontraron referencias.");
            }
        }

        protected void guardar()
        {
            gdvReferencias.Columns[0].Visible = true;
            gdvReferencias.Columns[1].Visible = true;
            String miembro;
            String actividadReferencia;
            String estadoReferencia;
            String notaEncargado;
            String nuevoEstado = "";
            foreach (GridViewRow row in gdvReferencias.Rows)
            {
                CheckBox chk = row.FindControl("chkModificar") as CheckBox;
                TextBox txb = row.FindControl("txbNotaEncargado") as TextBox;
                notaEncargado = txb.Text;
                miembro = row.Cells[0].Text;
                estadoReferencia = row.Cells[2].Text;
                actividadReferencia = row.Cells[1].Text;
                if (!chk.Checked || !String.IsNullOrEmpty(notaEncargado))
                {
                    nuevoEstado = !chk.Checked ? "EXCL" : estadoReferencia;
                    bdPROE.cambiarEstadoReferencia(S, miembro, actividadReferencia, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nuevoEstado, notaEncargado, U);
                }
            }
            gdvReferencias.Columns[0].Visible = false;
            gdvReferencias.Columns[1].Visible = false;
            llenarGdvReferencias();
            mst.mostrarMsjNtf(dic.msjSeHanIngresado);
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
                llenarGdvReferencias();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
    }
}