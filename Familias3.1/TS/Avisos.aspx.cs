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
    public partial class RegistrarAvisos : System.Web.UI.Page
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
        protected static String area;
        protected static String aviso;
        protected static DateTime fechaAviso;
        protected static DateTime fechaCreacion;
        protected static Boolean vista;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asignaColores();
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
                    visibilizarPestaña(pnlOpciones, lnkOpciones);
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    if(vista){
                        cargarConSeguridad();
                    }
                }
                catch
                {
                }
            }
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarAviso);
        }
        protected void cargarConSeguridad(){
            pnlIngresarAviso.Visible = false;
            gdvAvisosFamiliares.Columns[9].Visible = false;

            lblVNota.Text = txbNota.Text;
            txbNota.Visible = false;
            lblVNota.Visible = true;
            btnGuardarNota.Visible = false;
        }
        protected void llenarElementos()
        {
            llenarFormInsertarAviso();
            llenarGdvAvisos();
            txbNota.MaxLength = 100;
            try
            {
                txbNota.Text = bdTS.avsObtenerNota(S, F);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.ToString() + ".");
            }
            lnkNotaLibre.Text = dic.notaLibre;
            lnkOpciones.Text = dic.predeterminados;
            lblNota.Text = dic.nota + ":";
            btnGuardarNota.Text = dic.actualizar;
        }

        protected void lnkOpciones_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlOpciones, lnkOpciones);
        }

        protected void lnkNotaLibre_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlNotaLibre, lnkNotaLibre);
        }
        protected void ocultarPestañas()
        {
            pnlOpciones.Visible = false;
            pnlNotaLibre.Visible = false;
            lnkOpciones.CssClass = "cabecera";
            lnkNotaLibre.CssClass = "cabecera";
            asignaColores();
        }

        protected void asignaColores()
        {
            lnkOpciones.CssClass = lnkOpciones.CssClass + " blueCbc";
            lnkNotaLibre.CssClass = lnkNotaLibre.CssClass + " pinkCbc";
        }

        protected void visibilizarPestaña(Panel pnl, LinkButton lnk)
        {
            pnl.Visible = true;
            lnk.CssClass = lnk.CssClass + " c-activa";
        }
        protected void llenarFormInsertarAviso()
        {
            lblTS.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblDirec.Text = dic.direccion + ":";
            lblClasif.Text = dic.clasificacion + ":";
            revDdlAviso.ErrorMessage = dic.msjCampoNecesario;
            lblAviso.Text = "*" + dic.aviso + ":";
            lblAvisosFamiliares.Text = dic.avisosFamiliares;
            lblFechaAviso.Text = "&nbsp;" + dic.fechaAviso + ":";
            lblInactivo.Text = dic.inactivo;
            lblAvisoAct.Text = "&nbsp;" + dic.aviso + ":";
            lblFechaAvisoAct.Text = "&nbsp;" + dic.fechaAviso + ":";
            lblNoTieneAF.Text = dic.noTiene;
            btnNuevoAviso.Text = dic.TSnuevoAviso;
            btnGuardar.Text = dic.TSingresarAviso;
            btnGuardarAct.Text = dic.actualizar;
            txbFechaAviso.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txbFechaAviso.Enabled = false;
            llenarAvisos();
        }

        protected void llenarGdvAvisos()
        {
            DataTable dtAvisos = bdTS.avsObtenerAvisosFamiliares(S, F, L);
            lblNoTieneAF.Visible = false;
            gdvAvisosFamiliares.Visible = false;
            if (dtAvisos.Rows.Count > 0)
            {
                gdvAvisosFamiliares.Visible = true;
                gdvAvisosFamiliares.Columns[0].Visible = true;
                gdvAvisosFamiliares.Columns[1].Visible = true;
                gdvAvisosFamiliares.Columns[2].Visible = true;
                gdvAvisosFamiliares.Columns[3].Visible = true;
                gdvAvisosFamiliares.Columns[4].Visible = true;
                gdvAvisosFamiliares.Columns[5].HeaderText = dic.aviso;
                gdvAvisosFamiliares.Columns[6].HeaderText = dic.fecha;
                gdvAvisosFamiliares.Columns[7].HeaderText = dic.usuario;
                gdvAvisosFamiliares.Columns[8].HeaderText = dic.estado;
                gdvAvisosFamiliares.Columns[9].HeaderText = dic.acciones;
                gdvAvisosFamiliares.DataSource = dtAvisos;
                gdvAvisosFamiliares.DataBind();
                gdvAvisosFamiliares.Columns[0].Visible = false;
                gdvAvisosFamiliares.Columns[1].Visible = false;
                gdvAvisosFamiliares.Columns[2].Visible = false;
                gdvAvisosFamiliares.Columns[3].Visible = false;
                gdvAvisosFamiliares.Columns[4].Visible = false;
            }
            else
            {
                lblNoTieneAF.Text = dic.noTiene;
                lblNoTieneAF.Visible = true;
            }
        }

        protected void llenarAvisos()
        {
            ddlAviso.Items.Clear();
            ddlAviso.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerItemsAvisos(S, L, area);
            String codigo = "";
            String aviso = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["Code"].ToString();
                aviso = row["Des"].ToString();
                item = new ListItem(aviso, codigo);
                ddlAviso.Items.Add(item);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String aviso = ddlAviso.SelectedValue;
            DateTime fechaAviso = Convert.ToDateTime(convertirAFechaAmericana(txbFechaAviso.Text));
            try
            {
                if (bdTS.avsNuevoAviso(S, F, area, aviso, fechaAviso.ToString("MM/dd/yyyy HH:mm:ss"), U))
                {
                    llenarGdvAvisos();
                    prepararPnlNuevoAviso();
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Una familia solo puede tener un aviso de cada tipo.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("A family can only have one warning of each type.");
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void gdvAvisosFamiliares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            aviso = gdvAvisosFamiliares.Rows[numFilaEsp].Cells[0].Text;
            fechaAviso = Convert.ToDateTime(gdvAvisosFamiliares.Rows[numFilaEsp].Cells[1].Text);
            fechaCreacion = Convert.ToDateTime(gdvAvisosFamiliares.Rows[numFilaEsp].Cells[3].Text);
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    lblVAvisoAct.Text = "&nbsp;&nbsp;" + gdvAvisosFamiliares.Rows[numFilaEsp].Cells[5].Text;
                    //if(gdvAvisosFamiliares.Rows[numFilaEsp].Cells[4].ToString()=="True"){
                    //    chkInactivo.Checked = false;
                    //}else
                    //{
                    //    chkInactivo.Checked = true;
                    //}
                    chkInactivo.Checked = Convert.ToBoolean(gdvAvisosFamiliares.Rows[numFilaEsp].Cells[4].Text);
                    lblVFechaAvisoAct.Text = gdvAvisosFamiliares.Rows[numFilaEsp].Cells[6].Text;
                    pnlIngresarAviso.Visible = false;
                    pnlActualizarAviso.Visible = true;
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
        }

        protected void eliminarAviso(object sender, EventArgs e)
        {
            try
            {
                bdTS.avsEliminarAviso(S, F, area, aviso, fechaAviso.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U);
                llenarGdvAvisos();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                prepararPnlNuevoAviso();
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
                bdTS.avsActualizarAviso(S, F, area, aviso, fechaAviso.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U, chkInactivo.Checked);
                prepararPnlNuevoAviso();
                llenarGdvAvisos();
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnNuevoAviso_Click(object sender, EventArgs e)
        {
            prepararPnlNuevoAviso();
        }

        protected void prepararPnlNuevoAviso()
        {
            ddlAviso.SelectedValue = "";
            txbFechaAviso.Text = DateTime.Now.ToString("dd/MM/yyyy");
            pnlActualizarAviso.Visible = false;
            pnlIngresarAviso.Visible = true;
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

        protected void btnGuardarNota_Click(object sender, EventArgs e)
        {
            try
            {
                String nota = txbNota.Text;
                bdTS.avsIngresarNota(S, F, U, nota);
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.ToString() + ".");
            }
        }
    }
}