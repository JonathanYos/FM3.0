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
    public partial class HistorialClasificacion : System.Web.UI.Page
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
        protected static String clasifSelec;
        protected static int añoSelec;
        protected static String strFechaCreacionSelec;
        protected static String inactivoSelec;
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
                    vista = mast.vista;
                    dic = new Diccionario(L, S);
                    llenarElementos();
                    if (vista)
                    {
                        cargarConSeguridad();
                    }
                }
                catch
                {
                }
            }
        }
        protected void llenarElementos()
        {
            ocultarMostrarElementos(false);
            llenarHistorialClasificaciones();
            llenarNombres();
        }
        protected void cargarConSeguridad()
        {
            btnCambiarClas.Visible = false;
        }
        protected void llenarNombres()
        {
            lblAñoNuevaClas.Text = dic.TSañoSeleccionado + ":";
            lblClasActual.Text = dic.clasificacionActual + ":";
            lblDirec.Text = dic.direccion + ":";
            lblFechaClasActual.Text = dic.fechaClasifActual + ":";
            lblNuevaClas.Text = dic.TSclasificacionAñoSelec + ":";
            lblTSU.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            btnGuardar.Text = dic.actualizar;
            btnNuevaSeleccion.Text = dic.regresar;
            btnCambiarClas.Text = dic.TSactualizarClasif;

            DataTable dtFamilia = BDF.obtenerDatos(S, F, L);
            DataRow rowFamilia = dtFamilia.Rows[0];
            lblVClasActual.Text = rowFamilia["Classification"].ToString();
            lblVFechaClasActual.Text = rowFamilia["ClassifDate"].ToString();
            lblVDirec.Text = rowFamilia["Address"].ToString() + ", " + rowFamilia["Area"].ToString();
            lblVTS.Text = rowFamilia["TS"].ToString();
            lblVTelef.Text = rowFamilia["Phone"].ToString();
            lblVNuevaClas.ForeColor = Color.Crimson;
        }
        protected void llenarPnlAsignarClasif()
        {
            ocultarMostrarElementos(true);
            llenarGdvCondiciones();
            lblVAñoNuevaClas.Text = añoSelec + "";
            lblVNuevaClas.Text = "<b>" + clasifSelec + "</b>";
        }

        protected void llenarPnlAsignarClasifCambiar()
        {
            ocultarMostrarElementos(true);
            llenarGdvCondicionesCambiar();
            lblVAñoNuevaClas.Text = añoSelec + "";
            lblVNuevaClas.Text = "<b>" + clasifSelec + "</b>";
        }

        protected void llenarGdvCondiciones()
        {
            gdvCondiciones.Columns[0].Visible = true;
            gdvCondiciones.Columns[1].HeaderText = dic.condicion;
            gdvCondiciones.Columns[2].HeaderText = dic.aplica;
            gdvCondiciones.Columns[3].HeaderText = dic.aplica;
            gdvCondiciones.DataSource = bdTS.clsObtenerCondicionesHistorial(S, F, L, añoSelec, strFechaCreacionSelec);
            gdvCondiciones.DataBind();
            gdvCondiciones.Columns[0].Visible = false;
            //if ((añoSelec == DateTime.Now.Year) && (inactivoSelec.Equals("False")) && actualizar)
            //{
            //    gdvCondiciones.Columns[2].Visible = false;
            //    gdvCondiciones.Columns[3].Visible = true;
            //}
            //else
            //{
            gdvCondiciones.Columns[2].Visible = true;
            gdvCondiciones.Columns[3].Visible = false;
            btnGuardar.Visible = false;
            //}
        }

        protected void llenarGdvCondicionesCambiar()
        {
            gdvCondiciones.Columns[0].Visible = true;
            gdvCondiciones.Columns[1].HeaderText = dic.condicion;
            gdvCondiciones.Columns[2].HeaderText = dic.aplica;
            gdvCondiciones.Columns[3].HeaderText = dic.aplica;
            gdvCondiciones.DataSource = bdTS.clsObtenerCondicionesHistorial(S, F, L, añoSelec, strFechaCreacionSelec);
            gdvCondiciones.DataBind();
            gdvCondiciones.Columns[0].Visible = false;
            gdvCondiciones.Columns[2].Visible = false;
            gdvCondiciones.Columns[3].Visible = true;
        }

        protected void llenarHistorialClasificaciones()
        {
            lblHistorial.Text = dic.TShistorialClasificacion;
            gdvHistorialClasif.Columns[0].Visible = true;
            gdvHistorialClasif.Columns[1].Visible = true;
            gdvHistorialClasif.Columns[2].Visible = true;
            gdvHistorialClasif.Columns[3].Visible = true;
            gdvHistorialClasif.Columns[4].HeaderText = dic.año;
            gdvHistorialClasif.Columns[5].HeaderText = dic.clasificacion;
            gdvHistorialClasif.Columns[6].HeaderText = dic.fechaRegistro;
            gdvHistorialClasif.Columns[7].HeaderText = dic.activo;
            gdvHistorialClasif.Columns[8].HeaderText = dic.usuario;
            gdvHistorialClasif.Columns[9].HeaderText = dic.accion;
            gdvHistorialClasif.DataSource = bdTS.clsHistorialClasificacion(S, F, L);
            gdvHistorialClasif.DataBind();
            gdvHistorialClasif.Columns[0].Visible = false;
            gdvHistorialClasif.Columns[1].Visible = false;
            gdvHistorialClasif.Columns[2].Visible = false;
            gdvHistorialClasif.Columns[3].Visible = false;
        }

        protected void ocultarMostrarElementos(Boolean valor)
        {
            btnGuardar.Visible = valor;
            btnNuevaSeleccion.Visible = valor;
            btnCambiarClas.Visible = !valor;
            lblAñoNuevaClas.Visible = valor;
            lblVAñoNuevaClas.Visible = valor;
            lblNuevaClas.Visible = valor;
            lblVNuevaClas.Visible = valor;
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
                try
                {
                    bdTS.clsIngresarClasificacion(S, F, clasificacion, añoSelec + "", U, "0", camposCondiciones, valoresCondiciones);
                    bdTS.clsHstCambiarClasificacion(S, F, U, clasificacion);
                    DataTable dtFamilia = BDF.obtenerDatos(S, F, L);
                    DataRow rowFamilia = dtFamilia.Rows[0];
                    lblVClasActual.Text = rowFamilia["Classification"].ToString();
                    lblVFechaClasActual.Text = rowFamilia["ClassifDate"].ToString();
                    llenarHistorialClasificaciones();
                    nuevaSeleccion();
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
                pnlAsignarClasif.Visible = false;
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

        protected void gdvHistorialClasif_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdSeleccionar")
            {
                int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
                try
                {
                    gdvHistorialClasif.Columns[0].Visible = true;
                    gdvHistorialClasif.Columns[1].Visible = true;
                    gdvHistorialClasif.Columns[2].Visible = true;
                    gdvHistorialClasif.Columns[3].Visible = true;
                    DateTime fechaCreacionSelec = Convert.ToDateTime(gdvHistorialClasif.Rows[numFilaEsp].Cells[0].Text);
                    strFechaCreacionSelec = fechaCreacionSelec.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    añoSelec = Int32.Parse(gdvHistorialClasif.Rows[numFilaEsp].Cells[1].Text);
                    clasifSelec = gdvHistorialClasif.Rows[numFilaEsp].Cells[2].Text;
                    inactivoSelec = gdvHistorialClasif.Rows[numFilaEsp].Cells[3].Text;
                    gdvHistorialClasif.Columns[0].Visible = false;
                    gdvHistorialClasif.Columns[1].Visible = false;
                    gdvHistorialClasif.Columns[2].Visible = false;
                    gdvHistorialClasif.Columns[3].Visible = false;
                    llenarPnlAsignarClasif();
                    pnlHistorial.Visible = false;
                    pnlAsignarClasif.Visible = true;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }

        protected void nuevaSeleccion()
        {
            ocultarMostrarElementos(false);
            pnlAsignarClasif.Visible = false;
            pnlHistorial.Visible = true;
        }

        protected void btnNuevaSeleccion_Click(object sender, EventArgs e)
        {
            nuevaSeleccion();
        }

        protected void btnCambiarClas_Click(object sender, EventArgs e)
        {
            int numFilaEsp = 0;
            try
            {
                gdvHistorialClasif.Columns[0].Visible = true;
                gdvHistorialClasif.Columns[1].Visible = true;
                gdvHistorialClasif.Columns[2].Visible = true;
                gdvHistorialClasif.Columns[3].Visible = true;
                DateTime fechaCreacionSelec = Convert.ToDateTime(gdvHistorialClasif.Rows[numFilaEsp].Cells[0].Text);
                strFechaCreacionSelec = fechaCreacionSelec.ToString("yyyy-MM-dd HH:mm:ss.fff");
                añoSelec = Int32.Parse(gdvHistorialClasif.Rows[numFilaEsp].Cells[1].Text);
                clasifSelec = gdvHistorialClasif.Rows[numFilaEsp].Cells[2].Text;
                inactivoSelec = gdvHistorialClasif.Rows[numFilaEsp].Cells[3].Text;
                gdvHistorialClasif.Columns[0].Visible = false;
                gdvHistorialClasif.Columns[1].Visible = false;
                gdvHistorialClasif.Columns[2].Visible = false;
                gdvHistorialClasif.Columns[3].Visible = false;
                llenarPnlAsignarClasifCambiar();
                pnlHistorial.Visible = false;
                pnlAsignarClasif.Visible = true;
                btnCambiarClas.Visible = false;
                btnGuardar.Visible = true;

            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
    }
}