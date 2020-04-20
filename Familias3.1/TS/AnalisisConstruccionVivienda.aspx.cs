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
    public partial class AnalisisConstruccionVivienda : System.Web.UI.Page
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
        protected static String TS;
        protected static String region;
        protected static String familia;
        protected static Boolean postAnalisis;
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarAnalisis);
            if (!IsPostBack)
            {
                try
                {
                    bdTS = new BDTS();
                    bdGEN = new BDGEN();
                    BDF = new BDFamilia();
                    F = mast.F;
                    S = mast.S;
                    U = mast.U;
                    L = mast.L;
                    dic = new Diccionario(L, S);
                    postAnalisis = false;
                    llenarElementos();
                    if (!String.IsNullOrEmpty(F))
                    {
                        try
                        {
                            familia = F;
                            llenarPnlRegistroAnalisis();
                            pnlAsignarClasif.Visible = true;
                            pnlFamilias.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.ToString() + ".");
                        }
                    }
                }
                catch
                {
                }
            }
        }
        protected void llenarElementos()
        {
            llenarPnlFamilias();
        }
        protected void llenarPnlFamilias()
        {
            lblpost.Text = L.Equals("es") ? "Es Análisis Posterior" : "Is Post-Analysis";
            lblDirec.Text = "&nbsp;" + dic.direccion + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblTSU.Text = dic.trabajadorS + ":";
            lblRegion.Text = dic.region + ":";
            lblTelef.Text = dic.telefono + ":";
            lblAplica.Text = dic.aplica + ":";
            lblComentario.Text = "&nbsp;" + dic.comentario + ":";
            lblDiagnostico.Text = "*" + dic.diagnostico + ":";
            lblAñoAnalisis.Text = "&nbsp;" + dic.año + ":";
            lblNotas.Text = "&nbsp;" + dic.nota + ":";
            lblNCuartos.Text = dic.TSnumeroCuartos + ":";
            lblPared.Text = dic.TSpared + ":";
            lblPiso.Text = dic.TSpiso + ":";
            lblTenencia.Text = dic.TStenencia + ":";
            lblTamaño.Text = dic.TStamañoTerreno + ":";
            lblVAñoAnalisis.Text = DateTime.Now.Year + "";
            revDdlDiagnostico.ErrorMessage = dic.msjCampoNecesario;
            llenarTSs();
            llenarRegion();
            llenarDiagnosticos();
            btnBuscar.Text = dic.buscar;
            btnGuardar.Text = dic.guardar;
            btnNuevaSeleccion.Text = dic.regresar;
            if (!S.Equals("F"))
            {
                lblRegion.Visible = false;
                ddlRegion.Visible = false;
            }
        }
        protected void llenarPnlRegistroAnalisis()
        {
            DataTable dtAnalisisActual = bdTS.anlVvnObtenerAnalisisAñoActual(L, S, familia);
            btnEliminarAnalisis.Visible = false;
            limpiarElementos();
            if (dtAnalisisActual.Rows.Count > 0)
            {
                lblVAñoAnalisis.Text = dtAnalisisActual.Rows[0]["Año"].ToString();
                lblVAplica.Text = dtAnalisisActual.Rows[0]["Aplica"].ToString();
                lblVComentario.Text = dtAnalisisActual.Rows[0]["Comentario"].ToString();
                ddlDiagnostico.SelectedValue = dtAnalisisActual.Rows[0]["Diagnostico"].ToString();
                txbNotas.Text = dtAnalisisActual.Rows[0]["Notas"].ToString();
                btnEliminarAnalisis.Visible = true;
            }
            F = mast.F;
            DataTable dtFamilia = BDF.obtenerDatos(S, F, L);
            DataRow rowFamilia = dtFamilia.Rows[0];
            lblVDirec.Text = rowFamilia["Address"].ToString() + ", " + rowFamilia["Area"].ToString();
            lblVTS.Text = rowFamilia["TS"].ToString();
            lblVTelef.Text = rowFamilia["Phone"].ToString();

            DataTable dtMedioAmbiente = bdTS.hstMedioAmbiente(S, F, L);
            DataRow rowMedioAmbiente = dtMedioAmbiente.Rows[0];
            String tamañoPropiedadX = rowMedioAmbiente["tamañoPropiedadX"].ToString();
            String tamañoPropiedadY = rowMedioAmbiente["tamañoPropiedadY"].ToString();
            String tamañoPropiedadXAreaVerde = rowMedioAmbiente["tamañoPropiedadXAreaVerde"].ToString();
            String tamañoPropiedadYAreaVerde = rowMedioAmbiente["tamañoPropiedadYAreaVerde"].ToString();
            String tenencia = rowMedioAmbiente["Tenencia"].ToString();
            String numeroCuartos = rowMedioAmbiente["NumeroCuartos"].ToString();
            String materialPared = rowMedioAmbiente["MaterialPared"].ToString();
            String calidadMaterialPared = rowMedioAmbiente["CalidadMaterialPared"].ToString();
            String materialPiso = rowMedioAmbiente["MaterialPiso"].ToString();
            String calidadMaterialPiso = rowMedioAmbiente["CalidadMaterialPiso"].ToString();
            lblVNCuartos.Text = numeroCuartos;
            lblVTenencia.Text = tenencia;
            lblVTamaño.Text = tamañoPropiedadX + " X " + tamañoPropiedadY;
            if (!tamañoPropiedadXAreaVerde.Equals("0") && !tamañoPropiedadYAreaVerde.Equals("0"))
            {
                String areaVerde = " (" + dic.TStamañoAreaCultivo + tamañoPropiedadXAreaVerde + " X " + tamañoPropiedadYAreaVerde + ")";
                lblVTamaño.Text = lblVTamaño.Text + areaVerde;
            }
            lblVPared.Text = !String.IsNullOrEmpty(calidadMaterialPared) ? materialPared + " (" + calidadMaterialPared + ")" : materialPared;
            lblVPiso.Text = !String.IsNullOrEmpty(calidadMaterialPiso) ? materialPiso + " (" + calidadMaterialPiso + ")" : materialPiso;
            //lblVCocinaCon.Text = 
            llenarTblVVnd();
        }

        protected void llenarTblVVnd()
        {
            lblVvd.Text = dic.viviendas;
            gdvVvd.Columns[0].HeaderText = dic.año;
            gdvVvd.Columns[1].HeaderText = dic.aplica;
            gdvVvd.Columns[2].HeaderText = dic.tipo;
            gdvVvd.Columns[3].HeaderText = dic.diagnostico;
            gdvVvd.Columns[4].HeaderText = dic.comentario;
            gdvVvd.Columns[5].HeaderText = dic.estado;
            gdvVvd.Columns[6].HeaderText = dic.notas;
            gdvVvd.Columns[7].HeaderText = dic.solicitud;
            gdvVvd.Columns[8].HeaderText = dic.horasRequeridas;
            gdvVvd.Columns[9].HeaderText = dic.exoneracion;
            gdvVvd.Columns[10].HeaderText = dic.horasTrabajadas;
            DataTable dtVvnd = bdTS.anlVvnObtenerAnalisis(L, S, familia);
            gdvVvd.DataSource = dtVvnd;
            gdvVvd.DataBind();
            verificarGvdVacio(dtVvnd, gdvVvd, lblVVnNoTiene);
        }

        protected void verificarGvdVacio(DataTable dt, GridView gdv, Label lbl)
        {
            if (dt.Rows.Count == 0)
            {
                lbl.Text = dic.noTiene;
                gdv.Visible = false;
                lbl.Visible = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                TS = ddlTS.SelectedValue;
                region = ddlRegion.SelectedValue;
                llenarGdvFamilias();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void llenarGdvFamilias()
        {
            DataTable tblFamilias = bdTS.anlVvnObtenerFamiliasTS(S, L, TS, region);
            if (tblFamilias.Rows.Count > 0)
            {
                gdvFamilias.Columns[0].Visible = true;
                gdvFamilias.Columns[1].HeaderText = dic.familia;
                gdvFamilias.Columns[2].HeaderText = "Análisis 2020";
                gdvFamilias.Columns[3].HeaderText = dic.clasificacion;
                gdvFamilias.Columns[4].HeaderText = dic.trabajadorS;
                gdvFamilias.Columns[5].HeaderText = dic.area;
                gdvFamilias.Columns[6].HeaderText = dic.direccion;
                gdvFamilias.Columns[7].HeaderText = dic.jefeCasa;
                gdvFamilias.Columns[8].HeaderText = dic.accion;
                gdvFamilias.DataSource = tblFamilias;
                gdvFamilias.DataBind();
                gdvFamilias.Columns[0].Visible = false;
                pnlGdvFamilias.Visible = true;
            }
            else
            {
                pnlGdvFamilias.Visible = false;
            }
        }

        protected void gdvFamilias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                gdvFamilias.Columns[0].Visible = true;
                int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
                familia = gdvFamilias.Rows[numFilaEsp].Cells[0].Text;
                Session["F"] = familia;
                mst.seleccionarFamilia(familia);
                if (e.CommandName == "cmdSeleccionar")
                {
                    postAnalisis = false;
                    llenarPnlRegistroAnalisis();
                    pnlAsignarClasif.Visible = true;
                    pnlFamilias.Visible = false;
                }
                gdvFamilias.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String diagnostico = ddlDiagnostico.SelectedValue;
                String nota = txbNotas.Text;
                string post;
                if (ckbPreOPost.Checked == true) { 
                    post = "1"; 
                } else 
                { 
                    post = "0"; 
                }
                bdTS.anlVvnNuevoAnalisis(S, familia, U, diagnostico, nota, post);
                llenarTblVVnd();
                btnEliminarAnalisis.Visible = true;
                mst.mostrarMsjNtf(dic.msjSeHaIngresado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void btnNuevaSeleccion_Click(object sender, EventArgs e)
        {
            llenarGdvFamilias();
            pnlAsignarClasif.Visible = false;
            pnlFamilias.Visible = true;
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
        protected void llenarRegion()
        {
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("", ""));
            DataTable tblRegiones;
            tblRegiones = bdGEN.obtenerRegiones();
            String codigo = "";
            String descripcion = "";
            ListItem item;
            foreach (DataRow row in tblRegiones.Rows)
            {
                codigo = row["Code"].ToString();
                descripcion = row["Des"].ToString();
                item = new ListItem(descripcion, codigo);
                ddlRegion.Items.Add(item);
            }
        }
        protected void llenarDiagnosticos()
        {
            ddlDiagnostico.Items.Clear();
            ddlDiagnostico.Items.Add(new ListItem("", ""));
            DataTable tblDiagnosticos;
            tblDiagnosticos = bdGEN.obtenerDiagnostico(S, L, postAnalisis);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblDiagnosticos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlDiagnostico.Items.Add(item);
            }
        }

        protected void ddlDiagnostico_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblVComentario.Text = "";
            lblVAplica.Text = "";
            String diagnostico = ddlDiagnostico.SelectedValue;
            DataTable tblComentario = bdGEN.obtenerComentarioDeDiagnostico(S, diagnostico, L);
            if (tblComentario.Rows.Count > 0)
            {
                DataRow rowComentario = tblComentario.Rows[0];
                String comentario = rowComentario["Comentario"].ToString();
                String aplica = rowComentario["Aplica"].ToString();
                lblVComentario.Text = comentario;
                lblVAplica.Text = aplica;
            }
        }

        protected void limpiarElementos()
        {
            ddlDiagnostico.SelectedValue = "";
            lblVAplica.Text = "";
            lblVComentario.Text = "";
            txbNotas.Text = "";
        }

        protected void btnEliminarAnalisis_Click(object sender, EventArgs e)
        {
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }

        protected void eliminarAnalisis(object sender, EventArgs e)
        {
            try
            {
                bdTS.anlVvnEliminarAnalisisAñoActual(S, familia, U);
                llenarTblVVnd();
                limpiarElementos();
                btnEliminarAnalisis.Visible = false;
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void ckbPreOPost_CheckedChanged(object sender, EventArgs e)
        {
            postAnalisis = ckbPreOPost.Checked;
            llenarDiagnosticos();        
        }
    }
}