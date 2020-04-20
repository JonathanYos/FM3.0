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
    public partial class AsignarTS : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        public static Boolean vista;
        protected static mast mst;
        protected static Diccionario dic;
        protected static DateTime fechaCreacionSLCT;
        protected static String empleadoSLCT;
        protected static DateTime fechaInicioSLCT;
        protected static DateTime fechaFinSLCT;
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
                vista = mast.vista;
                dic = new Diccionario(L, S);
                try
                {
                    llenarElementos();
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    if (vista)
                    {
                        cargarConSeguridad();
                    }
                }
                catch
                {
                }
            }
            mst = (mast)Master;
        }
        protected void cargarConSeguridad()
        {
            pnlIngresarAsignacion.Visible = false;
        }
        protected void llenarElementos()
        {
            llenarFormAsignarTS();
            llenarGdvAsignaciones();
        }
        protected void llenarFormAsignarTS()
        {
            lblTSU.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblDirec.Text = dic.direccion + ":";
            lblClasif.Text = dic.clasificacion + ":";
            revDdlTS.ErrorMessage = dic.msjCampoNecesario;
            lblAsignacionesTS.Text = dic.TShistorialAsignacionesTS;
            lblFechaInicio.Text = "&nbsp;" + dic.fechaInicio + ":";
            lblNoTieneATS.Text = dic.noTiene;
            lblTS.Text = "*" + dic.trabajadorS + ":";
            lblTSAct.Text = "*" + dic.trabajadorS + ":";
            lblFechaInicioAct.Text = "&nbsp;" + dic.fechaInicio + ":";
            lblFechaFin.Text = "&nbsp;" + dic.fechaFin + ":";
            txbFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txbFechaInicio.Enabled = false;
            lblFechaInicio.Visible = false;
            txbFechaInicio.Visible = false;
            btnAsignarNuevoTS.Text = dic.TSnuevaAsignacion;
            btnGuardar.Text = dic.asignar;
            btnGuardarAct.Text = dic.asignar;
            llenarTSs();
        }
        protected void llenarGdvAsignaciones()
        {
            DataTable dtAsignacionesTS = bdTS.asgTSObtenerAsignaciones(S, F, L);
            lblNoTieneATS.Visible = false;
            gdvAsignacionesTS.Visible = false;
            if (dtAsignacionesTS.Rows.Count > 0)
            {
                gdvAsignacionesTS.Visible = true;
                gdvAsignacionesTS.Columns[0].Visible = true;
                gdvAsignacionesTS.Columns[1].Visible = true;
                gdvAsignacionesTS.Columns[2].Visible = true;
                gdvAsignacionesTS.Columns[3].Visible = true;
                gdvAsignacionesTS.Columns[4].HeaderText = dic.trabajadorS;
                gdvAsignacionesTS.Columns[5].HeaderText = dic.fechaInicio;
                gdvAsignacionesTS.Columns[6].HeaderText = dic.fechaFin;
                gdvAsignacionesTS.Columns[7].HeaderText = dic.acciones;
                gdvAsignacionesTS.DataSource = dtAsignacionesTS;
                gdvAsignacionesTS.DataBind();
                gdvAsignacionesTS.Columns[0].Visible = false;
                gdvAsignacionesTS.Columns[1].Visible = false;
                gdvAsignacionesTS.Columns[2].Visible = false;
                gdvAsignacionesTS.Columns[3].Visible = false;
            }
            else
            {
                lblNoTieneATS.Text = dic.noTiene;
                lblNoTieneATS.Visible = true;
            }
        }
        protected void llenarTSs()
        {
            ddlTS.Items.Clear();
            ddlTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS(S);
            String codigo = "";
            String empleado = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["EmployeeId"].ToString();
                empleado = row["EmployeeId"].ToString();
                item = new ListItem(empleado, codigo);
                ddlTS.Items.Add(item);
            }
        }

        protected void mostrarPnlActualizarAsignacion()
        {
            pnlIngresarAsignacion.Visible = false;
            pnlActualizarAsignacion.Visible = true;
        }

        protected void mostrarPnlIngresarAsignacion()
        {
            pnlActualizarAsignacion.Visible = false;
            pnlIngresarAsignacion.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String empleado = ddlTS.SelectedValue;
            DateTime fechaInicio = Convert.ToDateTime(convertirAFechaAmericana(txbFechaInicio.Text));
            try
            {
                if (bdTS.asgTSNuevaAsignacion(S, F, empleado, U, fechaInicio.ToString("yyyy-MM-dd HH:mm:ss.fff")))
                {
                    llenarGdvAsignaciones();
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVTS.Text = rowF["TS"].ToString();
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("No es posible asignar a ese Trabajador Social, ya que ya está activo.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("Is not possible to assign this Social Worker, since is already active.");
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void btnGuardarAct_Click(object sender, EventArgs e)
        {
            String strFechaFin = txbFechaFin.Text;
            if (!String.IsNullOrEmpty(strFechaFin))
            {
                DateTime fechaFin = Convert.ToDateTime(txbFechaFin.Text);
                strFechaFin = fechaFin.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                strFechaFin = "";
            }
            bdTS.asgTSActualizarAsignacion(S, F, empleadoSLCT, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), U, fechaInicioSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaFinSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), strFechaFin);
            mostrarPnlIngresarAsignacion();
            llenarGdvAsignaciones();
        }
        protected void gdvAsignacionesTS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            fechaCreacionSLCT = Convert.ToDateTime(gdvAsignacionesTS.Rows[numFilaEsp].Cells[0].Text);
            empleadoSLCT = gdvAsignacionesTS.Rows[numFilaEsp].Cells[1].Text;
            fechaInicioSLCT = Convert.ToDateTime(gdvAsignacionesTS.Rows[numFilaEsp].Cells[2].Text);
            String strFechaInicio = gdvAsignacionesTS.Rows[numFilaEsp].Cells[5].Text;
            String strFechaFin = gdvAsignacionesTS.Rows[numFilaEsp].Cells[3].Text;
            if (e.CommandName == "cmdActualizar")
            {
                lblVTSAct.Text = empleadoSLCT;
                lblVFechaInicioAct.Text = strFechaInicio;
                if ((!strFechaFin.Equals("&nbsp;")) && (!String.IsNullOrEmpty(strFechaFin)))
                {
                    DateTime fechaFin = Convert.ToDateTime(strFechaFin);
                    txbFechaFin.Text = fechaFin.ToString("dd/MM/yyyy");
                }
                else
                {
                    txbFechaFin.Text = "";
                }
                mostrarPnlActualizarAsignacion();
            }
            else if (e.CommandName == "cmdEliminar")
            {
                if ((((strFechaFin.Equals("&nbsp;")) || (String.IsNullOrEmpty(strFechaFin))) && (bdTS.asgTSExisteActivo(S, F, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff")) > 0)) || ((!strFechaFin.Equals("&nbsp;")) && (!String.IsNullOrEmpty(strFechaFin))))
                {
                    bdTS.asgTSEliminarAsignacion(S, F, empleadoSLCT, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    llenarGdvAsignaciones();
                }
            }
        }

        protected void btnAsignarNuevoTS_Click(object sender, EventArgs e)
        {
            mostrarPnlIngresarAsignacion();
        }
        private String convertirAFechaAmericana(String fechaOriginal)
        {
            String[] elementosFecha = fechaOriginal.Split('/');
            String dia = elementosFecha[0].ToString();
            String mes = elementosFecha[1].ToString();
            String año = elementosFecha[2].ToString();
            String fechaAmericana = año + "-" + mes + "-" + dia;
            return fechaAmericana;
        }
    }
}