using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.TS
{
    public partial class AsignarClasificacion : System.Web.UI.Page
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
        protected static String familia;
        protected static String region;
        protected static String clasifSigAño = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
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
                    llenarElementos();
                    if (!String.IsNullOrEmpty(F))
                    {
                        try
                        {
                            familia = F;
                            llenarPnlAsignarClasif();
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
            lblAñoNuevaClas.Text = dic.añoProximaClasificacion + ":";
            lblClasActual.Text = dic.clasificacionActual + ":";
            lblDirec.Text = dic.direccion + ":";
            lblFechaClasActual.Text = dic.fechaClasifActual + ":";
            lblNuevaClas.Text = dic.proximaClasificacion + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblTSU.Text = dic.trabajadorS + ":";
            lblRegion.Text = dic.region + ":";
            lblTelef.Text = dic.telefono + ":";
            llenarTSs();
            llenarRegion();
            btnBuscar.Text = dic.buscar;
            btnGuardar.Text = dic.guardar;
            btnNuevaSeleccion.Text = dic.regresar;
            if(!S.Equals("F")){
                lblRegion.Visible = false;
                ddlRegion.Visible = false;
            }
        }
        protected void llenarPnlAsignarClasif()
        {
            F = mast.F;
            DataTable dtFamilia = BDF.obtenerDatos(S, F, L);
            DataRow rowFamilia = dtFamilia.Rows[0];
            lblVAñoNuevaClas.Text = (DateTime.Now.Year + 1) + "";
            lblVClasActual.Text = rowFamilia["Classification"].ToString();
            lblVDirec.Text = rowFamilia["Address"].ToString() + ", " + rowFamilia["Area"].ToString();
            lblVTS.Text = rowFamilia["TS"].ToString();
            lblVTelef.Text = rowFamilia["Phone"].ToString();
            lblVFechaClasActual.Text = rowFamilia["ClassifDate"].ToString();
            lblVNuevaClas.ForeColor = Color.Crimson;
            clasifSigAño = bdTS.clsObtenerClasificacionSiguienteAño(S, F, L).Rows[0]["Classification"].ToString();
            lblVNuevaClas.Text = "<b>" + bdTS.clsObtenerClasificacionSiguienteAño(S, F, L).Rows[0]["Classification"].ToString() + "</b>";
            llenarGdvCondiciones();
        }
        protected void llenarGdvFamilias()
        {
            DataTable tblFamilias = bdTS.clsObtenerFamiliasTS(S, L, TS, region);
            if (tblFamilias.Rows.Count > 0)
            {
                gdvFamilias.Columns[0].Visible = true;
                gdvFamilias.Columns[1].HeaderText = dic.familia;
                gdvFamilias.Columns[2].HeaderText = DateTime.Now.Year + "";
                gdvFamilias.Columns[3].HeaderText = (DateTime.Now.Year + 1) + "";
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
        protected void llenarGdvCondiciones()
        {
            gdvCondiciones.Columns[0].Visible = true;
            gdvCondiciones.Columns[1].HeaderText = dic.condicion;
            gdvCondiciones.Columns[2].HeaderText = dic.aplica;
            gdvCondiciones.DataSource = bdTS.clsObtenerCondiciones(S, familia, L, (DateTime.Now.Year + 1));
            gdvCondiciones.DataBind();
            gdvCondiciones.Columns[0].Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int contadorCondiciones = 0;
            int contadorCampos = 1;
            gdvCondiciones.Columns[0].Visible = true;
            String camposCondiciones = "";
            String valoresCondiciones = "";

            foreach (GridViewRow row in gdvCondiciones.Rows)
            {
                CheckBox check = row.FindControl("chkAplica") as CheckBox;
                String condicion = row.Cells[0].Text;
                int aplica = 0;
                if (check.Checked)
                {
                    aplica = 1;
                    contadorCondiciones++;
                }
                camposCondiciones = camposCondiciones + "Condition" + contadorCampos + ", PointsC" + contadorCampos + ", ";
                valoresCondiciones = valoresCondiciones + "'" + condicion + "', " + aplica + ", ";
                contadorCampos++;
            }
            camposCondiciones = camposCondiciones.Substring(0, camposCondiciones.Length - 2);
            valoresCondiciones = valoresCondiciones.Substring(0, valoresCondiciones.Length - 2);
            gdvCondiciones.Columns[0].Visible = false;
            if (contadorCondiciones > 0)
            {
                String clasificacion = "";
                if ((contadorCondiciones == 1) || (contadorCondiciones == 2))
                {
                    clasificacion = "C";
                }
                else if ((contadorCondiciones == 3) || (contadorCondiciones == 4))
                {
                    clasificacion = "B";
                }
                else if (contadorCondiciones >= 5)
                {
                    clasificacion = "A";
                }
                String añoClasificacion = (DateTime.Now.Year + 1) + "";
                try
                {
                    bdTS.clsIngresarClasificacion(S, familia, clasificacion, añoClasificacion, U, "0", camposCondiciones, valoresCondiciones);
                    //bdTS.clsCambiarSiguienteClasificacion(S, F, U, clasificacion);
                    if (String.IsNullOrEmpty(clasifSigAño))
                    {
                        mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                    }
                    else
                    {
                        mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                    }

                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
                pnlAsignarClasif.Visible = false;
                pnlFamilias.Visible = true;
                llenarGdvFamilias();
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Debe al menos seleccionar una condición.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("You must at least select one condition.");
                }
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            TS = ddlTS.SelectedValue;
            region = ddlRegion.SelectedValue;
            llenarGdvFamilias();
        }
        protected void btnNuevaSeleccion_Click(object sender, EventArgs e)
        {
            llenarGdvFamilias();
            pnlAsignarClasif.Visible = false;
            pnlFamilias.Visible = true;
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
                if ((e.CommandName == "cmdActualizar") || (e.CommandName == "cmdIngresar"))
                {
                    llenarPnlAsignarClasif();
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

        protected void gdvFamilias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.Cells[3].Text.Equals("&nbsp;"))
                //{
                //    foreach (TableCell cell in e.Row.Cells)
                //    {
                //        if (cell.TabIndex == 3)
                //        {
                //            cell.BackColor = System.Drawing.Color.Aquamarine;
                //        }
                //    }
                //}
                if ((!e.Row.Cells[2].Text.Equals(e.Row.Cells[3].Text)) && (!e.Row.Cells[3].Text.Equals("-")))
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Thistle;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Thistle;
                }
            }
        }
        protected void gdvCondiciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdSeleccionar")
            {
                try
                {
                    String clasificacion = "";
                    int contadorCondiciones = 0;
                    foreach (GridViewRow row in gdvCondiciones.Rows)
                    {
                        CheckBox check = row.FindControl("chkAplica") as CheckBox;
                        if (check.Checked)
                        {
                            contadorCondiciones++;
                        }
                    }
                    if (contadorCondiciones > 0)
                    {
                        if ((contadorCondiciones == 1) || (contadorCondiciones == 2))
                        {
                            clasificacion = "C";
                        }
                        else if ((contadorCondiciones == 3) || (contadorCondiciones == 4))
                        {
                            clasificacion = "B";
                        }
                        else if (contadorCondiciones >= 5)
                        {
                            clasificacion = "A";
                        }
                    }
                    else
                    {
                        clasificacion = "";
                    }
                    lblVNuevaClas.Text = clasificacion;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }
    }
}