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
    public partial class Actividades : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static Boolean vista;
        protected static mast mst;
        protected static Diccionario dic;
        protected static String tipo;
        protected static String area;
        protected static DateTime fechaActividad;
        protected static DateTime fechaCreacion;
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
                area = "TS";
                try
                {
                    llenarElementos();
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
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
            mst.contentCallEvent += new EventHandler(eliminarActividad);
        }

        protected void cargarConSeguridad()
        {
            pnlIngresarActividad.Visible = false;
            gdvActividadesFamiliares.Columns[7].Visible = false;
        }

        protected void llenarElementos()
        {
            llenarFormInsertarActividad();
            llenarGdvActividades();
        }

        protected void llenarFormInsertarActividad()
        {
            lblTSU.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblDirec.Text = dic.direccion + ":";
            lblClasif.Text = dic.clasificacion + ":";
            revDdlActividad.ErrorMessage = dic.msjCampoNecesario;
            lblActividad.Text = "*" + dic.actividad + ":";
            lblActividadesFamiliares.Text = dic.actividadesFamiliares;
            lblFechaActividad.Text = "&nbsp;" + dic.fechaActividad + ":";
            lblNotas.Text = "&nbsp;" + dic.nota + ":";
            lblActividadAct.Text = "&nbsp;" + dic.actividad + ":";
            lblFechaActividadAct.Text = "&nbsp;" + dic.fechaActividad + ":";
            lblNoTieneAct.Text = dic.noTiene;
            btnNuevaActividad.Text = dic.TSnuevaActividad;
            lblActNotas.Text = "&nbsp;" + dic.nota + ":";
            btnGuardar.Text = dic.TSingresarActividad;
            btnGuardarAct.Text = dic.actualizar;
            txbFechaActividad.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txbFechaActividad.Enabled = false;
            llenarActividades();
            txbNotas.MaxLength = 40;
            txbActNotas.MaxLength = 40;
        }

        protected void llenarGdvActividades()
        {
            DataTable dtActividades = bdTS.actObtenerActividadesFamiliares(S, F, L);
            lblNoTieneAct.Visible = false;
            gdvActividadesFamiliares.Visible = false;
            if (dtActividades.Rows.Count > 0)
            {
                gdvActividadesFamiliares.Visible = true;
                gdvActividadesFamiliares.Columns[0].Visible = true;
                gdvActividadesFamiliares.Columns[1].Visible = true;
                gdvActividadesFamiliares.Columns[2].Visible = true;
                gdvActividadesFamiliares.Columns[3].HeaderText = dic.actividad;
                gdvActividadesFamiliares.Columns[4].HeaderText = dic.fecha;
                gdvActividadesFamiliares.Columns[5].HeaderText = dic.usuario;
                gdvActividadesFamiliares.Columns[6].HeaderText = dic.nota;
                gdvActividadesFamiliares.Columns[7].HeaderText = dic.acciones;
                gdvActividadesFamiliares.DataSource = dtActividades;
                gdvActividadesFamiliares.DataBind();
                gdvActividadesFamiliares.Columns[0].Visible = false;
                gdvActividadesFamiliares.Columns[1].Visible = false;
                gdvActividadesFamiliares.Columns[2].Visible = false;
            }
            else
            {
                lblNoTieneAct.Text = dic.noTiene;
                lblNoTieneAct.Visible = true;
            }
        }

        protected void llenarActividades()
        {
            ddlActividad.Items.Clear();
            ddlActividad.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerItemsActividades(S, L, area);
            String codigo = "";
            String aviso = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["Code"].ToString();
                aviso = row["Des"].ToString();
                item = new ListItem(aviso, codigo);
                ddlActividad.Items.Add(item);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String tipo = ddlActividad.SelectedValue;
            DateTime fechaActividad = Convert.ToDateTime(convertirAFechaAmericana(txbFechaActividad.Text));
            String notas = txbNotas.Text;
            try
            {
                if (bdTS.actNuevaActividad(S, F, tipo, fechaActividad.ToString("yyyy-MM-dd HH:mm:ss"), U, notas))
                {
                    llenarGdvActividades();
                    prepararPnlNuevoActividad();
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Una familia solo puede tener una actividad de cada tipo, el mismo dia.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("A family can only have one activity of each type, the same day.");
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void gdvActividadesFamiliares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            tipo = gdvActividadesFamiliares.Rows[numFilaEsp].Cells[0].Text;
            fechaActividad = Convert.ToDateTime(gdvActividadesFamiliares.Rows[numFilaEsp].Cells[1].Text);
            fechaCreacion = Convert.ToDateTime(gdvActividadesFamiliares.Rows[numFilaEsp].Cells[2].Text);
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    lblVActividadAct.Text = "&nbsp;&nbsp;" + gdvActividadesFamiliares.Rows[numFilaEsp].Cells[3].Text;
                    //if(gdvAvisosFamiliares.Rows[numFilaEsp].Cells[4].ToString()=="True"){
                    //    chkInactivo.Checked = false;
                    //}else
                    //{
                    //    chkInactivo.Checked = true;
                    //}
                    String notas = gdvActividadesFamiliares.Rows[numFilaEsp].Cells[6].Text;
                    if (notas.Equals("&nbsp;"))
                    {
                        notas = "";
                    }
                    txbActNotas.Text = notas;
                    lblVFechaActividadAct.Text = gdvActividadesFamiliares.Rows[numFilaEsp].Cells[3].Text;
                    pnlIngresarActividad.Visible = false;
                    pnlActualizarActividad.Visible = true;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            else if (e.CommandName == "cmdEliminar")
            {
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
        }

        protected void eliminarActividad(object sender, EventArgs e)
        {
            try
            {
                bdTS.actEliminarActividad(S, F, tipo, fechaActividad.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U);
                llenarGdvActividades();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                prepararPnlNuevoActividad();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnGuardarAct_Click(object sender, EventArgs e)
        {
            try
            {
                String notas = txbActNotas.Text;
                bdTS.actActualizarActividad(S, F, tipo, fechaActividad.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U, notas);
                prepararPnlNuevoActividad();
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                llenarGdvActividades();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnNuevaActividad_Click(object sender, EventArgs e)
        {
            prepararPnlNuevoActividad();
        }

        protected void prepararPnlNuevoActividad()
        {
            ddlActividad.SelectedValue = "";
            txbNotas.Text = "";
            txbFechaActividad.Text = DateTime.Now.ToString("dd/MM/yyyy");
            pnlActualizarActividad.Visible = false;
            pnlIngresarActividad.Visible = true;
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