using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
namespace Familias3._1.TS
{
    public partial class RegistrarCondiciones : System.Web.UI.Page
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
        protected static Boolean vista;
        protected static int categoriaActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarRegistro);
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
                try
                {
                    dic = new Diccionario(L, S);
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    try
                    {
                        cargarPgn();
                        vvnCargarPagina();
                        gstCargarPgn();
                        pssCargarPosesiones();
                        extCargarPgn();
                        ocpCargar();
                        if (vista)
                        {
                            cargarConSeguridad();
                        }
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjStc(dic.msjNoSeRealizoExcp + ex.ToString() + ".");
                    }
                    //if (Session["CatCONDF"] != null)
                    //{
                    //    String categoria = (String)Session["CatCONDF"];
                    //    if (categoria.Equals("VVN"))
                    //    {
                    //        ocultarPestañas();
                    //        visibilizarPestaña(pnlVivienda, lnkVivienda);
                    //        vvnCargarPagina();
                    //    }
                    //    else
                    //        if (categoria.Equals("GSTS"))
                    //        {
                    //            ocultarPestañas();
                    //            visibilizarPestaña(pnlGastos, lnkGastos);
                    //            gstCargarPgn();
                    //        }
                    //        else
                    //            if (categoria.Equals("OCPS"))
                    //            {
                    //                ocultarPestañas();
                    //                visibilizarPestaña(pnlOcupaciones, lnkOcupaciones);
                    //                pssCargarPosesiones();
                    //            }
                    //            else
                    //                if (categoria.Equals("PSNS"))
                    //                {
                    //                    ocultarPestañas();
                    //                    visibilizarPestaña(pnlPosesiones, lnkPosesiones);
                    //                    ocpCargar();
                    //                }
                    //}
                }
                catch
                {
                }
            }
        }
        protected void cargarPgn()
        {
            llenarNombres();
            visibilizarPestaña(pnlVivienda, lnkVivienda);
            categoriaActual = 1;
        }
        protected void cargarConSeguridad()
        {
            vvnCargarConSeguridad();
            gstCargarConSeguridad();
            ocpCargarConSeguridad();
            extCargarConSeguridad();
            pssCargarConSeguridad();
        }
        protected void llenarNombres()
        {
            lblDirec.Text = dic.direccion + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblClasif.Text = dic.clasificacion + ":";
            lnkVivienda.Text = dic.TSvivienda;
            lnkGastos.Text = dic.TSgastos;
            lnkOcupaciones.Text = dic.TSocupaciones;
            lnkIngresosExtra.Text = dic.ingresosExtra;
            lnkPosesiones.Text = dic.TSposesiones;
        }
        protected void lnkVivienda_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlVivienda, lnkVivienda);
            Session["CatCONDF"] = "VVN";
            categoriaActual = 1;

        }
        protected void lnkGastos_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlGastos, lnkGastos);
            Session["CatCONDF"] = "GSTS";
        }
        protected void lnkOcupaciones_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlOcupaciones, lnkOcupaciones);
            Session["CatCONDF"] = "OCPS";
            categoriaActual = 3;
        }
        protected void lnkIngresosExtra_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlIngresosExtra, lnkIngresosExtra);
            Session["CatCONDF"] = "INGE";
            categoriaActual = 4;
        }
        protected void lnkPosesiones_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlPosesiones, lnkPosesiones);
            Session["CatCONDF"] = "PSNS";
        }
        protected void ocultarPestañas()
        {
            pnlVivienda.Visible = false;
            pnlGastos.Visible = false;
            pnlOcupaciones.Visible = false;
            pnlIngresosExtra.Visible = false;
            pnlPosesiones.Visible = false;
            lnkVivienda.CssClass = "cabecera";
            lnkGastos.CssClass = "cabecera";
            lnkPosesiones.CssClass = "cabecera";
            lnkIngresosExtra.CssClass = "cabecera";
            lnkOcupaciones.CssClass = "cabecera";
            asignaColores();
        }

        protected void asignaColores()
        {
            lnkVivienda.CssClass = lnkVivienda.CssClass + " blueCbc";
            lnkGastos.CssClass = lnkGastos.CssClass + " greenCbc";
            lnkPosesiones.CssClass = lnkPosesiones.CssClass + " orangeCbc";
            lnkIngresosExtra.CssClass = lnkIngresosExtra.CssClass + " purpleCbc";
            lnkOcupaciones.CssClass = lnkOcupaciones.CssClass + " pinkCbc";
        }

        protected void visibilizarPestaña(Panel pnl, LinkButton lnk)
        {
            pnl.Visible = true;
            lnk.CssClass = lnk.CssClass + " c-activa";
        }

        //VIVIENDAS
        protected void vvnCargarPagina()
        {
            vvnLlenarElementos();
        }
        protected static String vvnFechaCreacionSLCT;
        protected static String vvnFechaInicioSLCT;
        protected static Boolean vvnEstadoSLCT;
        protected static Boolean vvnActualizar;
        protected void vvnCargarConSeguridad()
        {
            vvnLblVAgua.Text = vvnDdlAgua.SelectedItem.ToString();
            vvnLblVCaldParedCcna.Text = vvnDdlCaldParedCcna.SelectedItem.ToString();
            vvnLblVCaldParedCsa.Text = vvnDdlCaldParedCsa.SelectedItem.ToString();
            vvnLblVCaldPisoCsa.Text = vvnDdlCaldPisoCsa.SelectedItem.ToString();
            vvnLblVCaldTechoCcna.Text = vvnDdlCaldTechoCcna.SelectedItem.ToString();
            vvnLblVCaldTechoCsa.Text = vvnDdlCaldTechoCsa.SelectedItem.ToString();
            vvnLblVDrenaje.Text = vvnDdlDrenaje.SelectedItem.ToString();
            vvnLblVElectricidad.Text = vvnDdlElectricidad.SelectedItem.ToString();
            vvnLblVExcretas.Text = vvnDdlExcretas.SelectedItem.ToString();
            vvnLblVHigiene.Text = vvnDdlHigiene.SelectedItem.ToString();
            vvnLblVHigieneNotas.Text = vvnTxbHigieneNotas.Text;
            vvnLblVMatParedCcna.Text = vvnDdlMatParedCcna.SelectedItem.ToString();
            vvnLblVMatParedCsa.Text = vvnDdlMatParedCsa.SelectedItem.ToString();
            vvnLblVMatPisoCsa.Text = vvnDdlMatPisoCsa.SelectedItem.ToString();
            vvnLblVMatTechoCcna.Text = vvnDdlMatTechoCcna.SelectedItem.ToString();
            vvnLblVMatTechoCsa.Text = vvnDdlMatTechoCsa.SelectedItem.ToString();
            vvnLblVNoCuartos.Text = vvnDdlNoCuartos.SelectedItem.ToString();
            vvnLblVNotasParedCcna.Text = vvnTxbNotasParedCcna.Text;
            vvnLblVNotasParedCsa.Text = vvnTxbNotasParedCsa.Text;
            vvnLblVNotasPisoCsa.Text = vvnTxbNotasPisoCsa.Text;
            vvnLblVNotasTechoCcna.Text = vvnTxbNotasTechoCcna.Text;
            vvnLblVNotasTechoCsa.Text = vvnTxbNotasTechoCsa.Text;
            vvnLblVTamañoX.Text = vvnTxbTamañoX.Text;
            vvnLblVTamañoY.Text = vvnTxbTamañoY.Text;
            vvnLblVTamañoXJardin.Text = vvnTxbTamañoXJardin.Text;
            vvnLblVTamañoYJardin.Text = vvnTxbTamañoYJardin.Text;
            vvnLblVTenencia.Text = vvnDdlTenencia.SelectedItem.ToString();
            vvnLblSegundoNivel.CssClass = "labelForm";
            vvnLblTieneEsc.CssClass = "labelForm";
            if (vvnChkSegundoNivel.Checked)
            {
                vvnLblSegundoNivel.Text = (L.Equals("es") ? "Sí " : "") + vvnLblSegundoNivel.Text;
            }
            else
            {
                vvnLblSegundoNivel.Text = (L.Equals("es") ? "No " : "Does Not ") + vvnLblSegundoNivel.Text;
            }
            if (vvnChkTieneEsc.Checked)
            {
                vvnLblTieneEsc.Text = (L.Equals("es") ? "Sí " : "") + vvnLblTieneEsc.Text;
            }
            else
            {
                vvnLblTieneEsc.Text = (L.Equals("es") ? "No " : "Does Not ") + vvnLblTieneEsc.Text;
            }

            vvnDdlAgua.Visible = false;
            vvnDdlCaldParedCcna.Visible = false;
            vvnDdlCaldParedCsa.Visible = false;
            vvnDdlCaldPisoCsa.Visible = false;
            vvnDdlCaldTechoCcna.Visible = false;
            vvnDdlCaldTechoCsa.Visible = false;
            vvnDdlDrenaje.Visible = false;
            vvnDdlElectricidad.Visible = false;
            vvnDdlExcretas.Visible = false;
            vvnDdlHigiene.Visible = false;
            vvnTxbHigieneNotas.Visible = false;
            vvnDdlMatParedCcna.Visible = false;
            vvnDdlMatParedCsa.Visible = false;
            vvnDdlMatPisoCsa.Visible = false;
            vvnDdlMatTechoCcna.Visible = false;
            vvnDdlMatTechoCsa.Visible = false;
            vvnDdlNoCuartos.Visible = false;
            vvnTxbNotasParedCcna.Visible = false;
            vvnTxbNotasParedCsa.Visible = false;
            vvnTxbNotasPisoCsa.Visible = false;
            vvnTxbNotasTechoCcna.Visible = false;
            vvnTxbNotasTechoCsa.Visible = false;
            vvnTxbTamañoX.Visible = false;
            vvnTxbTamañoY.Visible = false;
            vvnTxbTamañoXJardin.Visible = false;
            vvnTxbTamañoYJardin.Visible = false;
            vvnDdlTenencia.Visible = false;
            vvnChkSegundoNivel.Visible = false;
            vvnChkTieneEsc.Visible = false;
            vvnBtnGuardarVvn.Visible = false;

            vvnLblVAgua.Visible = true;
            vvnLblVCaldParedCcna.Visible = true;
            vvnLblVCaldParedCsa.Visible = true;
            vvnLblVCaldPisoCsa.Visible = true;
            vvnLblVCaldTechoCcna.Visible = true;
            vvnLblVCaldTechoCsa.Visible = true;
            vvnLblVDrenaje.Visible = true;
            vvnLblVElectricidad.Visible = true;
            vvnLblVExcretas.Visible = true;
            vvnLblVHigiene.Visible = true;
            vvnLblVHigieneNotas.Visible = true;
            vvnLblVMatParedCcna.Visible = true;
            vvnLblVMatParedCsa.Visible = true;
            vvnLblVMatPisoCsa.Visible = true;
            vvnLblVMatTechoCcna.Visible = true;
            vvnLblVMatTechoCsa.Visible = true;
            vvnLblVNoCuartos.Visible = true;
            vvnLblVNotasParedCcna.Visible = true;
            vvnLblVNotasParedCsa.Visible = true;
            vvnLblVNotasPisoCsa.Visible = true;
            vvnLblVNotasTechoCcna.Visible = true;
            vvnLblVNotasTechoCsa.Visible = true;
            vvnLblVTamañoX.Visible = true;
            vvnLblVTamañoY.Visible = true;
            vvnLblVTamañoXJardin.Visible = true;
            vvnLblVTamañoYJardin.Visible = true;
            vvnLblVTenencia.Visible = true;
        }
        protected void vvnLlenarElementos()
        {
            vvnLlenarCombos();
            vvnLlenarNombres();
            vvnLlenarValores(bdTS.vvnObtenerCondicionesAct(S, F));
            vvnLlenarHistorial();
            vvnActualizar = false;
        }
        protected void vvnLlenarCombos()
        {
            vvnLlenarNumeroCuartos(vvnDdlNoCuartos);
            vvnLlenarItemsCatalogos(vvnDdlAgua, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdWater ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlCaldParedCcna, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlCaldParedCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlCaldPisoCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlCaldTechoCcna, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlCaldTechoCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlDrenaje, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdDrainage ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlElectricidad, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdElectricity ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlExcretas, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdBathroom ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlHigiene, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdQuality ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlMatParedCcna, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdWallMaterial ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlMatParedCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdWallMaterial ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlMatPisoCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdFloorMaterial ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlMatTechoCcna, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdCeilingMaterial ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlMatTechoCsa, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdCeilingMaterial ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            vvnLlenarItemsCatalogos(vvnDdlTenencia, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdPropertyOwnership ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");

        }
        protected void vvnLlenarHistorial()
        {
            DataTable dtHistorial = bdTS.vvnObtenerCondiciones(S, F, L);
            vvnGdvHistorial.Visible = false;
            vvnLblNoTiene.Visible = false;
            if (dtHistorial.Rows.Count > 0)
            {
                vvnGdvHistorial.Visible = true;
                vvnGdvHistorial.Columns[0].Visible = true;
                vvnGdvHistorial.Columns[1].Visible = true;
                vvnGdvHistorial.Columns[2].Visible = true;
                vvnGdvHistorial.DataSource = dtHistorial;
                vvnGdvHistorial.DataBind();
                vvnGdvHistorial.Columns[0].Visible = false;
                vvnGdvHistorial.Columns[1].Visible = false;
                vvnGdvHistorial.Columns[2].Visible = false;
            }
            else
            {
                vvnLblNoTiene.Visible = true;
            }
        }
        protected void vvnLlenarNombres()
        {
            vvnLblAgua.Text = dic.TSagua + ":";
            vvnLblCalidadCcna.Text = dic.TScalidad;
            vvnLblCalidadCsa.Text = dic.TScalidad;
            vvnLblCasa.Text = dic.TScasa;
            vvnLblCocina.Text = dic.TScocina;
            vvnLblDrenaje.Text = dic.TSdrenaje + ":";
            vvnLblElectricidad.Text = dic.TSelectricidad + ":";
            vvnLblExcretas.Text = dic.TSbaño + ":";
            vvnLblHigiene.Text = dic.TShigiene + ":";
            vvnLblJardín.Text = dic.TStamañoAreaCultivo + ":";
            vvnLblMaterialCcna.Text = dic.TSmaterial;
            vvnLblMaterialCsa.Text = dic.TSmaterial;
            vvnLblNoCuartos.Text = dic.TSnumeroCuartos + ":";
            vvnLblNotasCcna.Text = dic.nota;
            vvnLblNotasCsa.Text = dic.nota;
            vvnLblNotasHigiene.Text = dic.nota + ":";
            vvnLblOtros.Text = dic.otros;
            vvnLblParedCcna.Text = dic.TSpared + ":";
            vvnLblParedCsa.Text = dic.TSpared + ":";
            vvnLblPisoCsa.Text = dic.TSpiso + ":";
            vvnLblSegundoNivel.Text = dic.TStieneSegundoPiso;
            vvnLblServicios.Text = dic.TSservicios;
            vvnLblTamaño.Text = dic.TStamañoTerreno + ":";
            vvnLblTechoCcna.Text = dic.TStecho + ":";
            vvnLblTechoCsa.Text = dic.TStecho + ":";
            vvnLblTenencia.Text = dic.TStenencia + ":";
            vvnLblTerreno.Text = dic.TSpropiedad;
            vvnLblTieneEsc.Text = dic.TStieneEscritura;
            vvnLblHistorial.Text = L.Equals("es") ? "Historial de Vivienda" : "Living Place History";
            vvnLblNoTiene.Text = dic.noTiene;
            vvnBtnGuardarVvn.Text = dic.actualizar;
            vvnBtnNuevasCondicionesVVn.Text = dic.regresar;
            vvnBtnEliminarVvn.Text = dic.eliminar;
            vvnTxbHigieneNotas.MaxLength = 100;
            vvnTxbNotasParedCcna.MaxLength = 40;
            vvnTxbNotasParedCsa.MaxLength = 40;
            vvnTxbNotasPisoCsa.MaxLength = 40;
            vvnTxbNotasTechoCcna.MaxLength = 40;
            vvnTxbNotasTechoCsa.MaxLength = 40;
            vvnTxbTamañoX.MaxLength = 2;
            vvnTxbTamañoY.MaxLength = 2;
            vvnTxbTamañoXJardin.MaxLength = 2;
            vvnTxbTamañoYJardin.MaxLength = 2;
            vvnGdvHistorial.Columns[3].HeaderText = dic.fechaInicio;
            vvnGdvHistorial.Columns[4].HeaderText = dic.estado;
            vvnGdvHistorial.Columns[5].HeaderText = dic.usuario;
            vvnGdvHistorial.Columns[6].HeaderText = dic.acciones;
        }
        protected void vvnLlenarValores(DataTable dt)
        {
            int numFila = 0;
            vvnDdlAgua.SelectedValue = dt.Rows[numFila]["Water"].ToString();
            vvnDdlCaldParedCcna.SelectedValue = dt.Rows[numFila]["KitchenWallMaterialQuality"].ToString();
            vvnDdlCaldParedCsa.SelectedValue = dt.Rows[numFila]["WallMaterialQuality"].ToString();
            vvnDdlCaldPisoCsa.SelectedValue = dt.Rows[numFila]["FloorMaterialQuality"].ToString();
            vvnDdlCaldTechoCcna.SelectedValue = dt.Rows[numFila]["KitchenCeilingMaterialQuality"].ToString();
            vvnDdlCaldTechoCsa.SelectedValue = dt.Rows[numFila]["CeilingMaterialQuality"].ToString();
            vvnDdlDrenaje.SelectedValue = dt.Rows[numFila]["Drainage"].ToString();
            vvnDdlElectricidad.SelectedValue = dt.Rows[numFila]["Electricity"].ToString();
            vvnDdlExcretas.SelectedValue = dt.Rows[numFila]["Bathroom"].ToString();
            vvnDdlHigiene.SelectedValue = dt.Rows[numFila]["Hygiene"].ToString();
            vvnDdlMatParedCcna.SelectedValue = dt.Rows[numFila]["KitchenWallMaterial"].ToString();
            vvnDdlMatParedCsa.SelectedValue = dt.Rows[numFila]["WallMaterial"].ToString();
            vvnDdlMatPisoCsa.SelectedValue = dt.Rows[numFila]["FloorMaterial"].ToString();
            vvnDdlMatTechoCcna.SelectedValue = dt.Rows[numFila]["KitchenCeilingMaterial"].ToString();
            vvnDdlMatTechoCsa.SelectedValue = dt.Rows[numFila]["CeilingMaterial"].ToString();
            vvnDdlNoCuartos.SelectedValue = dt.Rows[numFila]["NumberOfRooms"].ToString();
            vvnDdlTenencia.SelectedValue = dt.Rows[numFila]["Ownership"].ToString();
            vvnTxbHigieneNotas.Text = dt.Rows[numFila]["HygieneNotes"].ToString();
            vvnTxbNotasParedCcna.Text = dt.Rows[numFila]["KitchenWallNotes"].ToString();
            vvnTxbNotasParedCsa.Text = dt.Rows[numFila]["WallNotes"].ToString();
            vvnTxbNotasPisoCsa.Text = dt.Rows[numFila]["FloorNotes"].ToString();
            vvnTxbNotasTechoCcna.Text = dt.Rows[numFila]["KitchenCeilingNotes"].ToString();
            vvnTxbNotasTechoCsa.Text = dt.Rows[numFila]["CeilingNotes"].ToString();
            vvnTxbTamañoX.Text = dt.Rows[numFila]["PropertySizeX"].ToString();
            vvnTxbTamañoY.Text = dt.Rows[numFila]["PropertySizeY"].ToString();
            vvnTxbTamañoXJardin.Text = dt.Rows[numFila]["CultivatedPropertySizeX"].ToString();
            vvnTxbTamañoYJardin.Text = dt.Rows[numFila]["CultivatedPropertySizeY"].ToString();
            Boolean tieneEscritura = (Boolean)dt.Rows[numFila]["HouseDeed"];
            vvnChkTieneEsc.Checked = tieneEscritura;
            Boolean tieneSegundoNivel = (Boolean)dt.Rows[numFila]["Has2ndFloor"];
            vvnChkSegundoNivel.Checked = tieneSegundoNivel;
        }
        protected void vvnLlenarItemsCatalogos(DropDownList ddl, String sql)
        {
            ddl.Items.Clear();
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerItemsCatalogos(sql);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddl.Items.Add(item);
            }
        }
        protected void vvnLlenarNumeroCuartos(DropDownList ddl)
        {
            DataTable tblNumeros = new DataTable();
            tblNumeros.Columns.Add("Code");
            tblNumeros.Columns.Add("Des");
            tblNumeros.Rows.Add("");
            ListItem item;
            item = new ListItem("", "");
            ddl.Items.Add(item);
            for (int i = 1; i < 12; i++)
            {
                item = new ListItem(i + "", i + "");
                ddl.Items.Add(item);
            }
            ddl.SelectedValue = "";
        }

        protected void vvnBtnGuardarVvn_Click(object sender, EventArgs e)
        {
            String agua = vvnDdlAgua.SelectedValue;
            String caldParedCcna = vvnDdlCaldParedCcna.SelectedValue;
            String caldParedCsa = vvnDdlCaldParedCsa.SelectedValue;
            String caldPisoCsa = vvnDdlCaldPisoCsa.SelectedValue;
            String caldTechoCcna = vvnDdlCaldTechoCcna.SelectedValue;
            String caldTechoCsa = vvnDdlCaldTechoCsa.SelectedValue;
            String drenaje = vvnDdlDrenaje.SelectedValue;
            String electricidad = vvnDdlElectricidad.SelectedValue;
            String excretas = vvnDdlExcretas.SelectedValue;
            String higiene = vvnDdlHigiene.SelectedValue;
            String matParedCcna = vvnDdlMatParedCcna.SelectedValue;
            String matParedCsa = vvnDdlMatParedCsa.SelectedValue;
            String matPisoCsa = vvnDdlMatPisoCsa.SelectedValue;
            String matTechoCcna = vvnDdlMatTechoCcna.SelectedValue;
            String matTechoCsa = vvnDdlMatTechoCsa.SelectedValue;
            String noCuartos = vvnDdlNoCuartos.SelectedValue;
            String tenencia = vvnDdlTenencia.SelectedValue;
            String higieneNotas = vvnTxbHigieneNotas.Text;
            String notasParedCcna = vvnTxbNotasParedCcna.Text;
            String notasParedCsa = vvnTxbNotasParedCsa.Text;
            String notasPisoCsa = vvnTxbNotasPisoCsa.Text;
            String notasTechoCcna = vvnTxbNotasTechoCcna.Text;
            String notasTechoCsa = vvnTxbNotasTechoCsa.Text;
            String tamañoX = vvnTxbTamañoX.Text;
            String tamañoY = vvnTxbTamañoY.Text;
            String tamañoXJardin = vvnTxbTamañoXJardin.Text;
            String tamañoYJardin = vvnTxbTamañoYJardin.Text;
            Boolean tieneEsc = vvnChkTieneEsc.Checked;
            Boolean segundoNivel = vvnChkSegundoNivel.Checked;
            try
            {
                if (!vvnActualizar)
                {
                    bdTS.vvnCambiarEstadoAInactivo(S, F, U);
                    bdTS.vvnActualizarCondiciones(S, F, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), U, tenencia, tamañoX, tamañoY, noCuartos, matParedCsa, caldParedCsa, matTechoCsa, caldTechoCsa, matPisoCsa, caldPisoCsa, matParedCcna, caldParedCcna, agua, electricidad, excretas, drenaje, higiene, higieneNotas, matTechoCcna, caldTechoCcna, tamañoXJardin, tamañoYJardin, notasTechoCsa, notasParedCsa, notasTechoCcna, notasParedCcna, notasPisoCsa, segundoNivel, tieneEsc, true);
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                }
                else
                {
                    bdTS.vvnActualizarCondiciones(S, F, vvnFechaInicioSLCT, U, tenencia, tamañoX, tamañoY, noCuartos, matParedCsa, caldParedCsa, matTechoCsa, caldTechoCsa, matPisoCsa, caldPisoCsa, matParedCcna, caldParedCcna, agua, electricidad, excretas, drenaje, higiene, higieneNotas, matTechoCcna, caldTechoCcna, tamañoXJardin, tamañoYJardin, notasTechoCsa, notasParedCsa, notasTechoCcna, notasParedCcna, notasPisoCsa, segundoNivel, tieneEsc, vvnEstadoSLCT);
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                }
                vvnLlenarHistorial();
                vvnPrepararElementosNuevoIngreso();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void vvnBtnEliminarVvn_Click(object sender, EventArgs e)
        {
            try
            {
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void vvnBtnNuevasCondicionesVVn_Click(object sender, EventArgs e)
        {
            vvnPrepararElementosNuevoIngreso();
        }
        protected void vvnPrepararElementosNuevoIngreso()
        {
            //vvnActualizar = false;
            //vvnBtnGuardarVvn.Text = dic.ingresar;
            //vvnBtnNuevasCondicionesVVn.Visible = false;
            //vvnBtnEliminarVvn.Visible = false;
            //DataTable dt = bdTS.vvnObtenerCondicionesAct(S, F);
            //vvnLlenarValores(dt);

            vvnActualizar = false;
            vvnBtnGuardarVvn.Visible = true;
            vvnBtnGuardarVvn.Text = dic.actualizar;
            vvnBtnNuevasCondicionesVVn.Visible = false;
            DataTable dt = bdTS.vvnObtenerCondicionesAct(S, F);
            vvnLlenarValores(dt);
        }
        protected void vvnPrepararElementosSeleccion()
        {
            //vvnActualizar = true;
            //vvnBtnGuardarVvn.Text = dic.actualizar;
            //vvnBtnNuevasCondicionesVVn.Visible = true;
            //vvnBtnEliminarVvn.Visible = true;
            //DataTable dt = bdTS.vvnObtenerCondicionesEsp(S, F, vvnFechaInicioSLCT);
            //vvnLlenarValores(dt);

            vvnActualizar = true;
            vvnBtnGuardarVvn.Visible = false;
            vvnBtnNuevasCondicionesVVn.Visible = true;
            DataTable dt = bdTS.vvnObtenerCondicionesEsp(S, F, vvnFechaInicioSLCT);
            vvnLlenarValores(dt);
        }
        protected void vvnGdvHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            vvnGdvHistorial.Columns[0].Visible = true;
            vvnGdvHistorial.Columns[1].Visible = true;
            vvnGdvHistorial.Columns[2].Visible = true;
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            vvnFechaCreacionSLCT = vvnGdvHistorial.Rows[numFilaEsp].Cells[0].Text;
            vvnFechaInicioSLCT = vvnGdvHistorial.Rows[numFilaEsp].Cells[1].Text;
            vvnEstadoSLCT = Boolean.Parse(vvnGdvHistorial.Rows[numFilaEsp].Cells[2].Text);
            if (e.CommandName == "cmdSeleccionar")
            {
                try
                {
                    vvnPrepararElementosSeleccion();
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            vvnGdvHistorial.Columns[0].Visible = false;
            vvnGdvHistorial.Columns[1].Visible = false;
            vvnGdvHistorial.Columns[2].Visible = false;
        }

        //GASTOS
        public static int totalGastos = 0;
        protected void gstBtnGuardarGastos_Click(object sender, EventArgs e)
        {
            try
            {
                float monto;
                String gasto;
                foreach (GridViewRow row in gdvGastos.Rows)
                {
                    TextBox txbMonto = row.FindControl("txbMonto") as TextBox;
                    if (!txbMonto.Text.Equals(""))
                    {
                        monto = float.Parse(txbMonto.Text);
                    }
                    else
                    {
                        monto = 0;
                    }
                    txbMonto.Text = monto + "";
                    gasto = row.Cells[0].Text;
                    if (!gasto.Equals("____"))
                    {
                        bdTS.gstInsertarGastoC(S, F, gasto, U, monto);
                    }
                }
                gstLlenarGdvGastos();
                gstCargarDiferencias();
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void gstCargarPgn()
        {
            gstLlenarGdvGastos();
            gstLlenarNombres();
            gstCargarDiferencias();
        }
        protected void gstCargarConSeguridad()
        {
            foreach (GridViewRow row in gdvGastos.Rows)
            {
                TextBox txbMonto = row.FindControl("txbMonto") as TextBox;
                txbMonto.Visible = false;
                Label lblVMonto = row.FindControl("lblVMonto") as Label;
                lblVMonto.Visible = true;
            }
            gstBtnGuardarGastos.Visible = false;
        }
        protected void gstLlenarNombres()
        {
            gstBtnGuardarGastos.Text = dic.actualizar;
        }
        protected void gstLlenarGdvGastos()
        {
            gdvGastos.Columns[0].Visible = true;
            DataTable dtGastos = bdTS.gstObtenerGastos(S, F, L);
            object sumObject;
            sumObject = dtGastos.Compute("Sum(Amount)", string.Empty);
            totalGastos = Int32.Parse(sumObject.ToString());
            String total = totalGastos + "";
            dtGastos.Rows.Add(false, "____", total, "Total:");
            gdvGastos.DataSource = dtGastos;
            gdvGastos.DataBind();
            gdvGastos.Columns[0].Visible = false;
            gdvGastos.Columns[1].HeaderText = dic.TSgasto;
            gdvGastos.Columns[2].HeaderText = dic.TSmonto + " (Q)";
        }
        protected void gstCargarDiferencias()
        {
            DataTable dtTotalOcupaciones = bdTS.rsmTotalesOcupaciones(S, F);
            DataTable dtTotalIngresosExtra = bdTS.rsmTotalIngresosExtra(S, F);
            if (dtTotalOcupaciones.Rows.Count > 0 && dtTotalIngresosExtra.Rows.Count > 0)
            {
                try
                {
                    gstLblTotalAportesOcupaciones.Text = (L.Equals("es") ? "Total de Aportes de Ocupaciones" : "Total of Occupations Contributions") + " (Q):";
                    gstLblTotalIngresosOcupaciones.Text = (L.Equals("es") ? "Total de Ingresos de Ocupaciones" : "Total of Occupations Incomes") + " (Q):";
                    gstLblTotalIngresosExtra.Text = (L.Equals("es") ? "Total de Ingresos Extra" : "Total of Additional Incomes") + " (Q):";
                    gstLblGastoMen.Text = (L.Equals("es") ? "Total de Gastos" : "Total of Expenses") + " (Q):";
                    gstLblGastoMen2.Text = (L.Equals("es") ? "Total de Gastos" : "Total of Expenses") + " (Q):";
                    gstLblDiferenciaAporte.Text = "Total (Q):";
                    gstLblDiferenciaIngreso.Text = "Total (Q):";

                    int totalIngresosOcupaciones = Int32.Parse(dtTotalOcupaciones.Rows[0]["TotalIngresos"].ToString());
                    int totalAportesOcupaciones = Int32.Parse(dtTotalOcupaciones.Rows[0]["TotalAportes"].ToString());
                    int totalIngresosExtra = Int32.Parse(dtTotalIngresosExtra.Rows[0]["TotalIngresos"].ToString());
                    gstLblVTotalAportesOcupaciones.Text = "+" + totalAportesOcupaciones + "";
                    gstLblVTotalIngresosOcupaciones.Text = "+" + totalIngresosOcupaciones + "";
                    gstLblVTotalIngresosExtra.Text = "+" + totalIngresosExtra + "";
                    gstLblVGastoMen.Text = "-" + totalGastos + "";
                    gstLblVGastosMen2.Text = "-" + totalGastos + "";
                    gstLblVDiferenciaAporte.Text = "<b>" + (totalAportesOcupaciones - totalGastos) + "</b>";
                    gstLblVDiferenciaIngreso.Text = "<b>" + ((totalIngresosOcupaciones + totalIngresosExtra) - totalGastos) + "</b>";
                    tblDiferenciaAporte.Visible = true;
                    tblDiferenciaIngresos.Visible = true;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsj(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }

        //OCUPACIONES
        protected static Boolean ocpActualizar;
        protected static String ocpMiembro;
        protected static DateTime ocpFechaCreacionSLCT;
        protected static DateTime ocpFechaInicioSLCT;
        protected static String ocpOcupacion;
        protected static Boolean ocpEsInactivo = false;
        protected static String ocpCategoria;
        protected void ocpCargar()
        {
            ocpLlenarGdvMiembros();
            ocpActualizar = false;
            ocpLlenarItemsCatalogos(ocpDdlOcupacion, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdOccupation ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            ocpLlenarItemsCatalogos(ocpDdlRazonFin, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdOccupationTerminationReason ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            ocpLlenarItemsCatalogos(ocpDdlJornada, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdWorkingDay ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            ocpLlenarItemsCatalogos(ocpDdlCategoria, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdOccupationCategory WHERE Code != 'VARI' ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            ocpLlenarNombres();
        }

        protected void ocpCargarConSeguridad()
        {
            //
        }
        protected void ocpSeleccionarMiembroConSeguridad()
        {
            ocpPnlGuardar.Visible = false;
            //ocpTxbAporte.Visible = false;
            ocpTxbFechaFin.Visible = false;
            ocpTxbFechaInicio.Visible = false;
            ocpTxbHorasSem.Visible = false;
            ocpTxbIngresos.Visible = false;
            ocpTxbIngresoSemanal.Visible = false;
            ocpTxbNumSemanas.Visible = false;
            ocpTxbLugarTrb.Visible = false;
            ocpDdlJornada.Visible = false;
            ocpDdlOcupacion.Visible = false;
            ocpDdlRazonFin.Visible = false;

            //ocpLblVAporte.Visible = true;
            ocpLblVFechaFin.Visible = true;
            ocpLblVFechaInicio.Visible = true;
            ocpLblVHorasSem.Visible = true;
            ocpLblVIngresos.Visible = true;
            ocpLblVJornada.Visible = true;
            ocpLblVJornada.Visible = true;
            ocpLblVOcupacion.Visible = true;
            ocpLblVRazonFin.Visible = true;


            ocpLblFechaInicio.Text = "&nbsp;" + dic.fechaInicio + ":";
            ocpLblIngresos.Text = "&nbsp;" + dic.TSingresoMensual + " (Q):";
            ocpLblOcupacion.Text = "&nbsp;" + dic.TSocupacion + ":";
        }
        protected void ocpSeleccionarOcupacionConSeguridad(String fechaInicio, String fechaFin)
        {
            ocpPnlGuardar.Visible = true;
            ocpBtnGuardarOcupacion.Visible = false;
            ocpBtnEliminarOcupacion.Visible = false;
            ocpBtnInsertarOcupacion.Visible = false;

            //ocpLblVAporte.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbAporte.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVFechaFin.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fechaFin + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVFechaInicio.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fechaInicio + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVHorasSem.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbHorasSem.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVIngresos.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbIngresos.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVIngresoSemanal.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbIngresoSemanal.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVNumSemanas.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbNumSemanas.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVLugarTrb.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpTxbLugarTrb.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVJornada.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpDdlJornada.SelectedItem.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVOcupacion.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpDdlOcupacion.SelectedItem.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            ocpLblVRazonFin.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ocpDdlRazonFin.SelectedItem.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            if (ocpChkTieneIGGS.Checked)
            {
                ocpLblTieneIGGS.Text = (L.Equals("es") ? "Sí " : "") + dic.TStieneIGGS;
            }
            else
            {
                ocpLblTieneIGGS.Text = (L.Equals("es") ? "No " : "Does Not ") + dic.TStieneIGGS;
            }
            ocpChkTieneIGGS.Visible = false;

            if (ocpChkTieneConstancia.Checked)
            {
                ocpLblTieneConstancia.Text = (L.Equals("es") ? "Sí Tiene Constancia" : "Has Certificate");
            }
            else
            {
                ocpLblTieneConstancia.Text = (L.Equals("es") ? "No Tiene Constancia" : "Does Not Has Cerificate");
            }
            ocpChkTieneConstancia.Visible = false;

            if (ocpCategoria.Equals("VARI"))
            {
                ocpLblCategoria.Text = "&nbsp;" + dic.categoria + ":";
                ocpLblVCategoria.Text = ocpDdlCategoria.SelectedItem + "";
                ocpDdlCategoria.Visible = false;
                ocpLblVCategoria.Visible = true;
            }
            if (ocpCategoria.Equals("INFO") || (ocpCategoria.Equals("VARI") && !String.IsNullOrEmpty(ocpTxbIngresoSemanal.Text)))
            {
                ocpTxbIngresoSemanal.Visible = ocpTxbNumSemanas.Visible = false;
                ocpLblVIngresoSemanal.Visible = ocpLblVNumSemanas.Visible = true;
                ocpTxbIngresoSemanal.Visible = true;
            }
            else if (ocpCategoria.Equals("FORM") || (ocpCategoria.Equals("VARI") && String.IsNullOrEmpty(ocpTxbIngresoSemanal.Text)))
            {
                ocpLblVIngresoSemanal.Visible = false;
                ocpLblVNumSemanas.Visible = false;
                ocpTxbIngresos.Visible = false;
                ocpLblVIngresos.Visible = true;
            }

        }
        protected void ocpLlenarGdvMiembros()
        {
            ocpTotalFamilia = 0;
            ocpGdvMiembros.Columns[0].Visible = true;
            ocpGdvMiembros.DataSource = bdTS.ocpObtenerMiembros(S, F, L);
            ocpGdvMiembros.DataBind();
            ocpGdvMiembros.Columns[0].Visible = false;
            ocpGdvMiembros.Columns[1].HeaderText = dic.miembro;
            ocpGdvMiembros.Columns[2].HeaderText = dic.nombre;
            ocpGdvMiembros.Columns[3].HeaderText = dic.relacion;
            ocpGdvMiembros.Columns[4].HeaderText = dic.inactivoRazon;
            ocpGdvMiembros.Columns[5].HeaderText = dic.afilTIpo;
            ocpGdvMiembros.Columns[6].HeaderText = dic.TSocupacion;
            ocpGdvMiembros.Columns[7].HeaderText = dic.TSingresoMensual + " (Q)";
            ocpGdvMiembros.Columns[8].HeaderText = dic.TSaporteMensual + " (Q)";
            ocpGdvMiembros.Columns[9].HeaderText = dic.accion;
        }
        protected void ocpSeleccionarMiembro()
        {
            //String nombre = ocpGdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            //ocpLblVNmbMiembro.Text = nombre;
            //ocpLlenarGridViewOcupaciones(ocpMiembro);
            //ocpPnlGuardar.Visible = true;
            //ocpPnlOcupaciones.Visible = true;
            //mst.seleccionarMiembro(ocpMiembro);
        }
        protected void ocpGdvMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                try
                {
                    ocpRealizarCambiosSegunCategoria();
                    String esInactivo = ocpGdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                    ocpEsInactivo = esInactivo.Equals("N") ? false : true;
                    ocpPnlMiembros.Visible = false;
                    ocpMiembro = ocpGdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                    String nombre = ocpGdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                    ocpLblVNmbMiembro.Text = nombre;
                    ocpLlenarGridViewOcupaciones(ocpMiembro);
                    ocpNuevaOcupacionPrepararElementos();
                    ocpPnlRegistro.Visible = true;
                    if (vista || ocpEsInactivo)
                    {
                        ocpSeleccionarMiembroConSeguridad();
                    }
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }
        protected void ocpGdvOcupaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "cmdSeleccionar")
            {
                try
                {
                    ocpLblOcupacion.Text = "&nbsp;" + dic.TSocupacion + ":";
                    ocpLblFechaInicio.Text = "&nbsp;" + dic.fechaInicio + ":";
                    ocpFechaCreacionSLCT = Convert.ToDateTime(ocpGdvOcupaciones.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text);
                    ocpOcupacion = ocpGdvOcupaciones.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[1].Text;
                    ocpFechaInicioSLCT = Convert.ToDateTime(ocpGdvOcupaciones.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[2].Text);
                    String fechaInicio = ocpGdvOcupaciones.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[4].Text;
                    ocpBtnInsertarOcupacion.Visible = true;
                    ocpActualizar = true;
                    int numFilaEsp = 0;
                    DataTable ocpEsp = bdTS.ocpObtenerOcupacionEsp(S, ocpMiembro, ocpOcupacion, ocpFechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss"), ocpFechaInicioSLCT.ToString("yyyy-MM-dd HH:mm:ss"));
                    ocpTxbFechaInicio.Text = ocpFechaInicioSLCT.ToString("dd/MM/yyyy");
                    ocpDdlRazonFin.SelectedValue = "";
                    ocpDdlOcupacion.SelectedValue = ocpOcupacion;
                    ocpDdlOcupacion.Enabled = false;
                    ocpDdlOcupacion.Visible = false;
                    String ocupacion = ocpDdlOcupacion.SelectedValue;
                    ocpLblVOcupacion.Visible = true;
                    ocpLblVOcupacion.Text = ocpDdlOcupacion.SelectedItem + "";
                    ocpTxbFechaInicio.Visible = false;
                    ocpLblVFechaInicio.Visible = true;
                    ocpLblVFechaInicio.Text = fechaInicio;
                    if (!String.IsNullOrEmpty(ocpEsp.Rows[numFilaEsp]["TerminationReason"].ToString()))
                    {
                        ocpDdlRazonFin.SelectedValue = ocpEsp.Rows[numFilaEsp]["TerminationReason"].ToString();
                    }
                    ocpDdlJornada.SelectedValue = ocpEsp.Rows[numFilaEsp]["WorkDay"].ToString();
                    String strFechaFin = ocpEsp.Rows[numFilaEsp]["EndDate"].ToString();
                    if ((!strFechaFin.Equals("01/01/1900 0:00:00")) && (!strFechaFin.Equals("1/01/1900 12:00:00 a. m.")) && (!strFechaFin.Equals("1900-00-00 00:00:00.000")) && (!String.IsNullOrEmpty(strFechaFin)))
                    {
                        DateTime ocpFechaFin = Convert.ToDateTime(strFechaFin);
                        ocpTxbFechaFin.Text = ocpFechaFin.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ocpTxbFechaFin.Text = "";
                    }
                    ocpTxbHorasSem.Text = ocpEsp.Rows[numFilaEsp]["WeeklyHours"].ToString();
                    String ingresos = ocpEsp.Rows[numFilaEsp]["MonthlyIncome"].ToString();
                    ocpTxbIngresos.Text = ingresos;
                    ocpLblVIngresos.Text = "&nbsp;" + ingresos;
                    String ingresoSemanal;
                    ocpTxbIngresoSemanal.Text = ingresoSemanal = ocpEsp.Rows[numFilaEsp]["WeeklyIncome"].ToString();
                    ocpTxbNumSemanas.Text = ocpEsp.Rows[numFilaEsp]["WorkedWeeks"].ToString();
                    ocpTxbLugarTrb.Text = ocpEsp.Rows[numFilaEsp]["WorkPlace"].ToString();
                    ocpChkTieneIGGS.Checked = (Boolean)ocpEsp.Rows[numFilaEsp]["HasIGSSAfil"];
                    ocpChkTieneConstancia.Checked = (Boolean)ocpEsp.Rows[numFilaEsp]["HasWorkCertificate"];
                    ocpCategoria = ocpEsp.Rows[numFilaEsp]["Category"].ToString();
                    ocpCategoria = !String.IsNullOrEmpty(ocpCategoria) ? ocpCategoria : "VARI";
                    ocpDdlCategoria.SelectedValue = !ocpCategoria.Equals("VARI") ? ocpCategoria : String.IsNullOrEmpty(ingresoSemanal) ? "FORM" : "INFO";
                    ocpRealizarCambiosSegunCategoria();
                    ocpRealizarCambiosSegunCategoria(ocpCategoria.Equals("FORM") ? true : (ocpCategoria.Equals("INFO") ? false : (String.IsNullOrEmpty(ingresoSemanal) ? true : false)));
                    //ocpTxbAporte.Text = ocpEsp.Rows[numFilaEsp]["MonthlyContribution"].ToString();
                    ocpBtnGuardarOcupacion.Text = dic.actualizar;
                    ocpBtnEliminarOcupacion.Visible = true;
                    if (vista || ocpEsInactivo)
                    {
                        String fechaFin = ocpGdvOcupaciones.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[5].Text;
                        ocpSeleccionarOcupacionConSeguridad(fechaInicio, fechaFin);
                    }
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }
        protected void ocpLlenarNombres()
        {
            ocpCategoria = "FORM";
            ocpRevTxbFechaFin_ValidatorCalloutExtender.PopupPosition = ValidatorCalloutPosition.Left;
            ocpLblMiembros.Text = dic.miembros;
            ocpLblOcupacionesMiembro.Text = dic.TShistorialOcupaciones;
            ocpLblNmbMiembro.Text = "&nbsp;" + dic.miembro + ":";
            ocpLblFechaFin.Text = "&nbsp;" + dic.fechaFin + ":";
            ocpLblFechaInicio.Text = "*" + dic.fechaInicio + ":";
            ocpLblHorasSem.Text = "&nbsp;" + dic.TShorasSemanales + ":";
            ocpLblIngresos.Text = "*" + dic.TSingresoMensual + " (Q):";
            ocpLblIngresoSemanal.Text = "&nbsp;" + dic.TSingresoSemanal + " (Q):";
            ocpLblNumSemanas.Text = "&nbsp;" + (L.Equals("es") ? "No. de Semanas al Mes" : "No. of Weeks in Month") + ":";
            ocpLblJornada.Text = "&nbsp;" + dic.TSjornada + ":";
            ocpLblLugarTrb.Text = "&nbsp;" + dic.TSlugarTrabajo + ":";
            ocpLblOcupacion.Text = "*" + dic.TSocupacion + ":";
            ocpLblCategoria.Text = "&nbsp;" + dic.categoria + ":";
            ocpLblRazonFin.Text = "&nbsp;" + dic.TSrazonFin + ":";
            ocpLblTieneIGGS.Text = dic.TStieneIGGS;
            ocpLblTieneConstancia.Text = L.Equals("es") ? "Tiene Constancia Laboral" : "Has Work Certificate";
            //ocpLblAporte.Text = dic.TSaporteMensual + " (Q):";
            ocpBtnGuardarOcupacion.Text = dic.ingresar;
            ocpBtnEliminarOcupacion.Text = dic.eliminar;
            ocpBtnInsertarOcupacion.Text = dic.TSnuevaOcupacion;
            ocpBtnRegresar.Text = dic.regresar;
            ocpLblNoTiene.Text = dic.noTiene;
            ocpRevDdlOcupacion.ErrorMessage = dic.msjCampoNecesario;
            ocpRevTxbFechaInicio.ErrorMessage = dic.msjCampoNecesario;
            ocpRevTxbIngresos.ErrorMessage = dic.msjCampoNecesario;
            if (S.Equals("es"))
            {
                ocpRevTxbFechaFin.ErrorMessage = "La fecha no es válida.";
            }
            else
            {
                ocpRevTxbFechaFin.ErrorMessage = "The date is not valid.";
            }
            ocpTxbFechaInicio.MaxLength = 10;
            ocpTxbFechaFin.MaxLength = 10;
            ocpTxbHorasSem.MaxLength = 2;
            ocpTxbIngresos.MaxLength = 5;
            ocpTxbIngresoSemanal.MaxLength = 5;
            ocpTxbNumSemanas.MaxLength = 1;
            ocpTxbLugarTrb.MaxLength = 50;
        }
        protected void ocpLlenarItemsCatalogos(DropDownList ddl, String sql)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerItemsCatalogos(sql);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddl.Items.Add(item);
            }
        }

        protected void ocpBtnInsertarOcupacion_Click(object sender, EventArgs e)
        {
            ocpNuevaOcupacionPrepararElementos();
            ocpActualizar = false;
            ocpDdlOcupacion.Enabled = true;
        }

        protected void ocpBtnGuardarOcupacion_Click(object sender, EventArgs e)
        {
            Boolean fechaInicioFormato = false;
            Boolean fechaFinFormato = false;
            String ocupacion = ocpDdlOcupacion.SelectedValue;

            String strFechaInicio = "";
            try
            {
                DateTime fechaInicio = Convert.ToDateTime(convertirAFechaAmericana(ocpTxbFechaInicio.Text));
                strFechaInicio = fechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                fechaInicioFormato = true;
            }
            String strfechaFin = "";
            if (!String.IsNullOrEmpty(ocpTxbFechaFin.Text))
            {
                try
                {
                    DateTime fechaFin = Convert.ToDateTime(convertirAFechaAmericana(ocpTxbFechaFin.Text));
                    strfechaFin = fechaFin.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    fechaFinFormato = true;
                }
            }
            //int horasSemanales = Int32.Parse(ocpTxbHorasSem.Text);
            String horasSemanales = ocpTxbHorasSem.Text;
            String razonFin = ocpDdlRazonFin.SelectedValue;
            //float ingresos = float.Parse(ocpTxbIngresos.Text);
            String categoriaAux = ocpDdlCategoria.SelectedValue;
            String ingresos = categoriaAux.Equals("FORM") ? ocpTxbIngresos.Text : ocpHdnIngresos.Value;
            String ingresosSemanal = categoriaAux.Equals("INFO") ? ocpTxbIngresoSemanal.Text : "";
            String cantSemanas = categoriaAux.Equals("INFO") ? ocpTxbNumSemanas.Text : "";
            Boolean tieneIGGS = categoriaAux.Equals("FORM") ? ocpChkTieneIGGS.Checked : false;
            Boolean tieneConstancia = categoriaAux.Equals("FORM") ? ocpChkTieneConstancia.Checked : false;
            //String aporte = ocpTxbAporte.Text;
            String aporte = "0";
            String jornada = ocpDdlJornada.Text;
            String lugarTrabajo = ocpTxbLugarTrb.Text;
            if (fechaInicioFormato)
            {
                ocpRevTxbFechaInicio.ErrorMessage = dic.msjFechaIncorrecta;
                ocpRevTxbFechaInicio.Visible = true;
            }
            if (fechaFinFormato)
            {
                ocpRevTxbFechaInicio.ErrorMessage = dic.msjFechaIncorrecta;
                ocpRevTxbFechaFin.Validate();
                ocpRevTxbFechaFin.Visible = true;
            }
            if (!fechaInicioFormato && !fechaFinFormato)
            {
                if ((!String.IsNullOrEmpty(strfechaFin) && !String.IsNullOrEmpty(razonFin)) || (String.IsNullOrEmpty(strfechaFin) && String.IsNullOrEmpty(razonFin)))
                {
                    Boolean fechasCronologicas = true;
                    if (!String.IsNullOrEmpty(strfechaFin))
                    {
                        if (Convert.ToDateTime(strFechaInicio) > Convert.ToDateTime(strfechaFin))
                        {
                            fechasCronologicas = false;
                        }
                    }
                    if (fechasCronologicas)
                    {
                        Boolean aporteEsIdoneo = true;
                        if (!String.IsNullOrEmpty(aporte))
                        {
                            if (Int32.Parse(aporte) > Int32.Parse(ingresos))
                            {
                                aporteEsIdoneo = false;
                            }
                        }
                        if (aporteEsIdoneo)
                        {
                            if (!ocpActualizar)
                            {
                                try
                                {
                                    if (!bdTS.ocpIngresarOcupacion(S, ocpMiembro, ocupacion, strFechaInicio, U, strfechaFin, horasSemanales, razonFin, ingresos, ingresosSemanal, cantSemanas, jornada, lugarTrabajo, tieneIGGS, tieneConstancia, aporte))
                                    {
                                        if (L.Equals("es"))
                                        {
                                            mst.mostrarMsjAdvNtf("Un miembro no puede tener más de una vez, la misma ocupación, iniciada en la misma fecha.");
                                        }
                                        else
                                        {
                                            mst.mostrarMsjAdvNtf("A member cannot have more than once, the same occupation, started on the same date.");
                                        }
                                    }
                                    else
                                    {
                                        mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                                        ocpNuevaOcupacionPrepararElementos();
                                        ocpLlenarGridViewOcupaciones(ocpMiembro);
                                        ocpLlenarGdvMiembros();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                                }
                            }

                            else
                            {
                                try
                                {
                                    if (!bdTS.ocpActualizarOcupacion(S, ocpMiembro, ocupacion, ocpFechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss"), ocpFechaInicioSLCT.ToString("yyyy-MM-dd HH:mm:ss"), strFechaInicio, U, strfechaFin, horasSemanales, razonFin, ingresos, ingresosSemanal, cantSemanas, jornada, lugarTrabajo, tieneIGGS, tieneConstancia, aporte))
                                    {
                                        if (L.Equals("es"))
                                        {
                                            mst.mostrarMsjAdvNtf("Un miembro no puede tener más de una vez, la misma ocupación, iniciada en la misma fecha.");
                                        }
                                        else
                                        {
                                            mst.mostrarMsjAdvNtf("A member cannot have more than once, the same occupation, started on the same date.");
                                        }
                                    }
                                    else
                                    {
                                        mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                                        ocpNuevaOcupacionPrepararElementos();
                                        ocpLlenarGridViewOcupaciones(ocpMiembro);
                                        ocpLlenarGdvMiembros();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                                }
                            }
                        }
                        else
                        {
                            String msj = L.Equals("es") ? "El Aporte Mensual no puede ser mayor que el Ingreso Mensual." : "The Monthly Contribution cannot be greater than the Monthly Income.";
                            mst.mostrarMsjAdvNtf(msj);
                        }
                    }
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("La Fecha de Fin no puede ser después que la Fecha de Inicio.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("The End Date cannot be before to the Start Date.");
                        }
                    }
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Si se ingresa Razón de Terminación, también es necesario ingresar la Fecha de Finalización.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("If the Termination Reason is entered, it is also necessary to enter the End Date.");
                    }
                }
            }
        }

        protected void ocpBtnRegresar_Click(object sender, EventArgs e)
        {
            ocpPnlRegistro.Visible = false;
            ocpPnlMiembros.Visible = true;
        }

        protected void ocpDdlOcupacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String ocupacion = ocpDdlOcupacion.SelectedValue;
                ocpCategoria = bdTS.ocpObtenerCategoria(ocupacion);
                ocpDdlCategoria.SelectedValue = !ocpCategoria.Equals("VARI") ? ocpCategoria : "";
                ocpRealizarCambiosSegunCategoria();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }
        protected void ocpDdlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean formal = ocpDdlCategoria.SelectedValue.Equals("FORM") ? true : false;
            ocpRealizarCambiosSegunCategoria(formal);
        }

        protected void ocpRealizarCambiosSegunCategoria()
        {
            if (ocpCategoria.Equals("FORM") || ocpCategoria.Equals("INFO"))
            {
                ocpLblVCategoria.Text = ocpDdlCategoria.SelectedItem.ToString();
                ocpDdlCategoria.Visible = false;
                ocpLblVCategoria.Visible = true;
                Boolean aux = ocpCategoria.Equals("FORM") ? true : false;
                ocpRealizarCambiosSegunCategoria(aux);
                ocpLblCategoria.Text = "&nbsp;" + dic.categoria + ":";
            }
            else if (ocpCategoria.Equals("VARI"))
            {
                ocpDdlCategoria.Visible = true;
                ocpLblVCategoria.Visible = false;
                ocpPonerFormularioSinCategoria();
                ocpDdlCategoria.Visible = true;
                ocpLblCategoria.Text = "*" + dic.categoria + ":";
                ocpTxbIngresos.Visible = true;
                ocpLblVIngresos.Visible = false;
            }
        }
        protected void ocpRealizarCambiosSegunCategoria(Boolean aux)
        {
            ocpLblTieneIGGS.Visible = aux;
            ocpChkTieneIGGS.Visible = aux;
            ocpLblTieneConstancia.Visible = aux;
            ocpChkTieneConstancia.Visible = aux;
            ocpLblIngresoSemanal.Visible = !aux;
            ocpTxbIngresoSemanal.Visible = !aux;
            ocpLblNumSemanas.Visible = !aux;
            ocpTxbNumSemanas.Visible = !aux;
            ocpTxbIngresos.Visible = aux;
            ocpLblVIngresos.Visible = !aux;
        }
        protected void ocpLlenarGridViewOcupaciones(String ocpMiembro)
        {
            ocpTotalMiembro = 0;
            DataTable dtOcupaciones = bdTS.ocpObtenerOcupaciones(S, ocpMiembro, L);
            ocpGdvOcupaciones.Visible = false;
            ocpLblNoTiene.Visible = false;
            if (dtOcupaciones.Rows.Count > 0)
            {
                ocpGdvOcupaciones.Visible = true;
                ocpGdvOcupaciones.Columns[0].Visible = true;
                ocpGdvOcupaciones.Columns[1].Visible = true;
                ocpGdvOcupaciones.Columns[2].Visible = true;
                ocpGdvOcupaciones.Columns[3].HeaderText = dic.TSocupacion;
                ocpGdvOcupaciones.Columns[4].HeaderText = dic.fechaInicio;
                ocpGdvOcupaciones.Columns[5].HeaderText = dic.fechaFin;
                ocpGdvOcupaciones.Columns[6].HeaderText = dic.TSingresoMensual + " (Q)";
                ocpGdvOcupaciones.Columns[7].HeaderText = dic.TSingresoSemanal + " (Q)";
                ocpGdvOcupaciones.Columns[8].HeaderText = dic.TSaporteMensual + " (Q)";
                ocpGdvOcupaciones.Columns[9].HeaderText = dic.TSlugarTrabajo;
                ocpGdvOcupaciones.Columns[10].HeaderText = dic.usuario;
                ocpGdvOcupaciones.Columns[11].HeaderText = dic.accion;
                ocpGdvOcupaciones.DataSource = dtOcupaciones;
                ocpGdvOcupaciones.DataBind();
                ocpGdvOcupaciones.Columns[0].Visible = false;
                ocpGdvOcupaciones.Columns[1].Visible = false;
                ocpGdvOcupaciones.Columns[2].Visible = false;
            }
            else
            {
                ocpLblNoTiene.Visible = true;
            }

        }

        protected void ocpLimpiarElementos()
        {
            ocpTxbFechaFin.Text = "";
            ocpTxbFechaInicio.Text = "";
            ocpTxbHorasSem.Text = "";
            ocpTxbIngresos.Text = "";
            ocpLblVIngresos.Text = "";
            ocpTxbIngresoSemanal.Text = "";
            ocpTxbNumSemanas.Text = "";
            ocpTxbLugarTrb.Text = "";
            //ocpTxbAporte.Text = "";
            ocpDdlJornada.SelectedValue = "";
            ocpDdlOcupacion.SelectedValue = "";
            ocpDdlRazonFin.SelectedValue = "";
            ocpChkTieneIGGS.Checked = false;
            ocpChkTieneConstancia.Checked = false;
        }

        protected void ocpNuevaOcupacionPrepararElementos()
        {
            ocpPnlGuardar.Visible = true;
            //ocpTxbAporte.Visible = true;
            ocpTxbFechaFin.Visible = true;
            ocpTxbFechaInicio.Visible = true;
            ocpTxbHorasSem.Visible = true;
            ocpTxbIngresos.Visible = true;
            ocpTxbLugarTrb.Visible = true;
            ocpDdlJornada.Visible = true;
            ocpDdlOcupacion.Visible = true;
            ocpDdlRazonFin.Visible = true;
            ocpPonerFormularioSinCategoria();

            //ocpLblVAporte.Visible = false;
            ocpLblVFechaFin.Visible = false;
            ocpLblVFechaInicio.Visible = false;
            ocpLblVHorasSem.Visible = false;
            ocpLblVIngresos.Visible = false;
            ocpLblVJornada.Visible = false;
            ocpLblVJornada.Visible = false;
            ocpLblVOcupacion.Visible = false;
            ocpLblVRazonFin.Visible = false;

            ocpLblOcupacion.Text = "*" + dic.TSocupacion + ":";
            ocpLblFechaInicio.Text = "*" + dic.fechaInicio + ":";

            ocpLblOcupacion.Text = "*" + dic.TSocupacion + ":";
            ocpLblCategoria.Text = "&nbsp;" + dic.categoria + ":";



            ocpBtnInsertarOcupacion.Visible = false;
            ocpBtnGuardarOcupacion.Text = dic.TSingresarOcupacion;
            ocpBtnGuardarOcupacion.Visible = true;
            ocpBtnEliminarOcupacion.Visible = false;
            ocpLimpiarElementos();
            ocpLblVOcupacion.Visible = false;
            ocpDdlOcupacion.Enabled = true;
            ocpDdlOcupacion.Visible = true;
        }
        protected void ocpPonerFormularioSinCategoria()
        {
            ocpChkTieneIGGS.Visible = false;
            ocpChkTieneConstancia.Visible = false;
            ocpLblTieneIGGS.Visible = false;
            ocpLblTieneConstancia.Visible = false;
            ocpTxbIngresoSemanal.Visible = false;
            ocpTxbNumSemanas.Visible = false;
            ocpLblIngresoSemanal.Visible = false;
            ocpLblNumSemanas.Visible = false;
            ocpLblVCategoria.Text = "";
            ocpDdlCategoria.Visible = false;
        }
        protected void ocpBtnEliminarOcupacion_Click(object sender, EventArgs e)
        {
            try
            {
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }


        protected void ocpbtnSeleccionarOtroMiembro_Click(object sender, EventArgs e)
        {

        }

        //INGRESOS EXTRA
        protected static Boolean extActualizar;
        protected static DateTime extFechaCreacionSLCT;
        protected static DateTime extFechaInicioSLCT;
        protected static String extTipo;
        protected static int extTotal;
        protected void extCargarPgn()
        {
            extLlenarNombres();
            extLlenarGdvIngresos();
            extActualizar = false;
            extLlenarItemsCatalogos(extDdlTipo, "SELECT CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des, Code FROM CdAdditionalIncomeType ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");

        }

        protected void extCargarConSeguridad()
        {
            extPnlGuardar.Visible = false;
        }

        protected void extSeleccionarIngresoExtraConSeguridad(String fechaInicio, String fechaFin)
        {
            extDdlTipo.Visible = false;
            extTxbFechaInicio.Visible = false;
            extTxbFechaFin.Visible = false;
            extTxbIngresos.Visible = false;
            extTxbNotas.Visible = false;

            extLblVTipo.Visible = true;
            extLblVFechaInicio.Visible = true;
            extLblVFechaFin.Visible = true;
            extLblVIngresos.Visible = true;
            extLblVNotas.Visible = true;

            extPnlGuardar.Visible = true;
            ocpBtnGuardarOcupacion.Visible = false;
            extBtnGuardarIngreso.Visible = false;
            extBtnEliminarIngreso.Visible = false;
            extBtnInsertarIngreso.Visible = false;

            extLblVFechaFin.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fechaFin + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            extLblVFechaInicio.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fechaInicio + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            extLblVIngresos.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + extTxbIngresos.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            extLblVNotas.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + extTxbNotas.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            extLblVTipo.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + extDdlTipo.SelectedItem.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            extLblIngresos.Text = "&nbsp;" + dic.TSingresoMensual + "(Q):";
        }

        protected void extLlenarNombres()
        {
            extRevTxbFechaFin_ValidatorCalloutExtender.PopupPosition = ValidatorCalloutPosition.Left;
            extLblFechaFin.Text = "&nbsp;" + dic.fechaFin + ":";
            extLblFechaInicio.Text = "*" + dic.fechaInicio + ":";
            extLblIngresos.Text = "*" + dic.TSingresoMensual + " (Q):";
            extLblNotas.Text = "&nbsp;" + dic.nota + ":";
            extLblTipo.Text = "*" + dic.tipo + ":";
            extBtnGuardarIngreso.Text = dic.TSingresarIngresoExtra;
            extBtnEliminarIngreso.Text = dic.eliminar;
            extBtnInsertarIngreso.Text = dic.TSnuevoIngresoExtra;
            extLblNoTiene.Text = dic.noTiene;
            extRevDdlTipo.ErrorMessage = dic.msjCampoNecesario;
            extRevTxbFechaInicio.ErrorMessage = dic.msjCampoNecesario;
            extRevTxbIngresos.ErrorMessage = dic.msjCampoNecesario;
            extLblHistorialIngresos.Text = dic.TShistorialIngresosExtra;
            if (S.Equals("es"))
            {
                extRevTxbFechaFin.ErrorMessage = "La fecha no es válida.";
            }
            else
            {
                extRevTxbFechaFin.ErrorMessage = "The date is not valid.";
            }
            extTxbFechaInicio.MaxLength = 10;
            extTxbFechaFin.MaxLength = 10;
            extTxbIngresos.MaxLength = 5;
            extTxbNotas.MaxLength = 40;
        }
        protected void extLlenarItemsCatalogos(DropDownList ddl, String sql)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerItemsCatalogos(sql);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddl.Items.Add(item);
            }
        }

        protected void extLlenarGdvIngresos()
        {
            extTotal = 0;
            DataTable dtIngresos = bdTS.extobtenerIngresosExtras(S, F, L);
            extGdvIngresos.Visible = false;
            extLblNoTiene.Visible = false;
            if (dtIngresos.Rows.Count > 0)
            {
                extGdvIngresos.Visible = true;
                extGdvIngresos.Columns[0].Visible = true;
                extGdvIngresos.Columns[1].Visible = true;
                extGdvIngresos.Columns[2].Visible = true;
                extGdvIngresos.Columns[3].Visible = true;
                extGdvIngresos.Columns[4].Visible = true;
                extGdvIngresos.Columns[5].Visible = true;
                extGdvIngresos.Columns[6].HeaderText = dic.tipo;
                extGdvIngresos.Columns[7].HeaderText = dic.fechaInicio;
                extGdvIngresos.Columns[8].HeaderText = dic.fechaFin;
                extGdvIngresos.Columns[9].HeaderText = dic.TSingresoMensual + " (Q)";
                extGdvIngresos.Columns[10].HeaderText = dic.nota;
                extGdvIngresos.Columns[11].HeaderText = dic.usuario;
                extGdvIngresos.Columns[12].HeaderText = dic.accion;
                extGdvIngresos.DataSource = dtIngresos;
                extGdvIngresos.DataBind();
                extGdvIngresos.Columns[0].Visible = false;
                extGdvIngresos.Columns[1].Visible = false;
                extGdvIngresos.Columns[2].Visible = false;
                extGdvIngresos.Columns[3].Visible = false;
                extGdvIngresos.Columns[4].Visible = false;
                extGdvIngresos.Columns[5].Visible = false;
            }
            else
            {
                extLblNoTiene.Visible = true;
            }

        }

        protected void extBtnGuardarIngreso_Click(object sender, EventArgs e)
        {
            Boolean fechaInicioFormato = false;
            Boolean fechaFinFormato = false;
            String tipo = extDdlTipo.SelectedValue;

            String strFechaInicio = "";
            try
            {
                DateTime fechaInicio = Convert.ToDateTime(convertirAFechaAmericana(extTxbFechaInicio.Text));
                strFechaInicio = fechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                fechaInicioFormato = true;
            }
            String strFechaFin = "";
            if (!String.IsNullOrEmpty(extTxbFechaFin.Text))
            {
                try
                {
                    DateTime fechaFin = Convert.ToDateTime(convertirAFechaAmericana(extTxbFechaFin.Text));
                    strFechaFin = fechaFin.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    fechaFinFormato = true;
                }
            }
            String ingresos = extTxbIngresos.Text;
            String notas = extTxbNotas.Text;

            if (fechaInicioFormato)
            {
                extRevTxbFechaInicio.ErrorMessage = dic.msjFechaIncorrecta;
                extRevTxbFechaInicio.Visible = true;
            }
            if (fechaFinFormato)
            {
                extRevTxbFechaInicio.ErrorMessage = dic.msjFechaIncorrecta;
                extRevTxbFechaFin.Validate();
                extRevTxbFechaFin.Visible = true;
            }
            if (!fechaInicioFormato && !fechaFinFormato)
            {
                Boolean fechasCronologicas = true;
                if (!String.IsNullOrEmpty(strFechaFin))
                {
                    if (Convert.ToDateTime(strFechaInicio) > Convert.ToDateTime(strFechaFin))
                    {
                        fechasCronologicas = false;
                    }
                }
                if (fechasCronologicas)
                {
                    if (!extActualizar)
                    {
                        try
                        {
                            DateTime ahora = DateTime.Now;
                            String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
                            if (!bdTS.extVerificarIngreso(S, F, tipo, fechaCreacion, strFechaInicio, 0))
                            {
                                if (L.Equals("es"))
                                {
                                    mst.mostrarMsjAdvNtf("Una familia no puede tener más de una vez, un Ingreso Extra del mismo tipo, iniciada en la misma fecha.");
                                }
                                else
                                {
                                    mst.mostrarMsjAdvNtf("A family cannot have more than once, a Additional Income of the same type, started on the same date.");
                                }
                            }
                            else
                            {
                                bdTS.extIngresarIngresoExtra(S, F, tipo, fechaCreacion, strFechaInicio, U, strFechaFin, ingresos, notas);
                                mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                                extNuevoIngresoPrepararElementos();
                                extLlenarGdvIngresos();
                            }
                        }
                        catch (Exception ex)
                        {
                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                        }
                    }

                    else
                    {
                        try
                        {
                            DateTime ahora = DateTime.Now;
                            String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
                            bdTS.extIngresarIngresoExtra(S, F, extTipo, fechaCreacion, extFechaInicioSLCT.ToString("yyy-MM-dd HH:mm:ss"), U, strFechaFin, ingresos, notas);
                            mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                            extNuevoIngresoPrepararElementos();
                            extLlenarGdvIngresos();
                            extActualizar = false;
                        }
                        catch (Exception ex)
                        {
                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                        }
                    }
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("La Fecha de Fin no puede ser después que la Fecha de Inicio.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("The End Date cannot be before to the Start Date.");
                    }
                }
            }
        }

        protected void extNuevoIngresoPrepararElementos()
        {
            extPnlGuardar.Visible = true;
            extTxbFechaFin.Visible = true;
            extTxbFechaInicio.Visible = true;
            extTxbIngresos.Visible = true;
            extTxbNotas.Visible = true;
            extDdlTipo.Visible = true;

            extLblVFechaFin.Visible = false;
            extLblVFechaInicio.Visible = false;
            extLblVIngresos.Visible = false;
            extLblVNotas.Visible = false;
            extLblVTipo.Visible = false;

            extLblTipo.Text = "*" + dic.tipo + ":";
            extLblFechaInicio.Text = "*" + dic.fechaInicio + ":";
            extLblIngresos.Text = "*" + dic.TSingresoMensual + " (Q):";
            extLblNotas.Text = "&nbsp;" + dic.nota + ":";

            extBtnInsertarIngreso.Visible = false;
            extBtnGuardarIngreso.Text = dic.TSingresarIngresoExtra;
            extBtnGuardarIngreso.Visible = true;
            extBtnEliminarIngreso.Visible = false;
            extLimpiarElementos();
            extLblVTipo.Visible = false;
            extDdlTipo.Enabled = true;
            extDdlTipo.Visible = true;

            extActualizar = false;
        }
        protected void extLimpiarElementos()
        {
            extTxbFechaFin.Text = "";
            extTxbFechaInicio.Text = "";
            extTxbIngresos.Text = "";
            extTxbNotas.Text = "";
            extDdlTipo.SelectedValue = "";
        }
        protected void extBtnEliminarIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        protected void extBtnInsertarIngreso_Click(object sender, EventArgs e)
        {
            extNuevoIngresoPrepararElementos();
            extActualizar = false;
            extDdlTipo.Enabled = true;
        }

        protected void extGdvIngresos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdSeleccionar")
            {
                extActualizar = true;
                try
                {
                    extGdvIngresos.Columns[0].Visible = true;
                    extGdvIngresos.Columns[1].Visible = true;
                    extGdvIngresos.Columns[2].Visible = true;
                    extGdvIngresos.Columns[3].Visible = true;
                    extGdvIngresos.Columns[4].Visible = true;
                    extLblTipo.Text = "&nbsp;" + dic.tipo + ":";
                    extLblFechaInicio.Text = "&nbsp;" + dic.fechaInicio + ":";
                    //extLblIngresos.Text = "&nbsp;" + dic.TSingresoMensual + ":";
                    //extLblNotas.Text = "&nbsp;" + dic.nota + ":";
                    extFechaCreacionSLCT = Convert.ToDateTime(extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text);
                    extTipo = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[1].Text;
                    extFechaInicioSLCT = Convert.ToDateTime(extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[2].Text);
                    String strFechaFin = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[3].Text;
                    String ingresoMensual = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[4].Text;
                    String notas = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[5].Text;
                    notas = HttpUtility.HtmlDecode(notas.Replace("&nbsp;", ""));
                    String fechaInicio = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[7].Text;
                    extBtnInsertarIngreso.Visible = true;
                    extTxbFechaInicio.Text = extFechaInicioSLCT.ToString("dd/MM/yyyy");
                    extDdlTipo.SelectedValue = extTipo;
                    extDdlTipo.Enabled = false;
                    extDdlTipo.Visible = false;
                    extLblVTipo.Visible = true;
                    extLblVTipo.Text = "&nbsp;" + extDdlTipo.SelectedItem + "";
                    extTxbFechaInicio.Visible = false;
                    extLblVFechaInicio.Visible = true;
                    extLblVFechaInicio.Text = "&nbsp;" + fechaInicio;
                    if (!String.IsNullOrEmpty(strFechaFin) && !strFechaFin.Equals("&nbsp;"))
                    {
                        DateTime extFechaFin = Convert.ToDateTime(strFechaFin);
                        extTxbFechaFin.Text = extFechaFin.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        extTxbFechaFin.Text = "";
                    }
                    extTxbIngresos.Text = ingresoMensual;
                    extTxbNotas.Text = notas;
                    extBtnGuardarIngreso.Text = dic.actualizar;
                    extBtnEliminarIngreso.Visible = true;
                    if (vista)
                    {
                        String fechaFin = extGdvIngresos.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[8].Text;
                        extSeleccionarIngresoExtraConSeguridad(fechaInicio, fechaFin);
                    }
                    extGdvIngresos.Columns[0].Visible = false;
                    extGdvIngresos.Columns[1].Visible = false;
                    extGdvIngresos.Columns[2].Visible = false;
                    extGdvIngresos.Columns[3].Visible = false;
                    extGdvIngresos.Columns[4].Visible = false;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }
        protected void extGdvIngresos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String fechaFin = e.Row.Cells[3].Text;
                fechaFin = fechaFin.Replace("&nbsp;", "");
                Label lblqy = (Label)e.Row.FindControl("lblIngreso");
                int qty = Int32.Parse(lblqy.Text);
                extTotal = String.IsNullOrEmpty(fechaFin) ? extTotal + qty : extTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].ColumnSpan = 2;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[10].ColumnSpan = 3;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[8].CssClass = "left";
                e.Row.Cells[8].Text = "Total: ";
                Label lblTotalqty = (Label)e.Row.FindControl("lblFtrIngreso");
                lblTotalqty.Text = "<b>" + extTotal.ToString() + "</b>";
            }
        }
        //POSESIONES
        protected void pssCargarPosesiones()
        {
            DataTable dtCategorias = bdGEN.obtenerItemsCatalogos("SELECT Code, CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des FROM CdPossessionCategory ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            pssLlenarGridview(pssGdvPosesiones1, dtCategorias.Rows[0]["Code"].ToString(), dtCategorias.Rows[0]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones2, dtCategorias.Rows[1]["Code"].ToString(), dtCategorias.Rows[1]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones3, dtCategorias.Rows[2]["Code"].ToString(), dtCategorias.Rows[2]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones4, dtCategorias.Rows[3]["Code"].ToString(), dtCategorias.Rows[3]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones5, dtCategorias.Rows[4]["Code"].ToString(), dtCategorias.Rows[4]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones6, dtCategorias.Rows[5]["Code"].ToString(), dtCategorias.Rows[5]["Des"].ToString());
            pssLlenarGridview(pssGdvPosesiones7, dtCategorias.Rows[6]["Code"].ToString(), dtCategorias.Rows[6]["Des"].ToString());
            pssBtnGuardar.Text = dic.actualizar;
        }
        protected void pssCargarConSeguridad()
        {
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones1);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones2);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones3);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones4);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones5);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones6);
            pssCargarCiertoGdvConSeguridad(pssGdvPosesiones7);
            pssBtnGuardar.Visible = false;
        }

        protected void pssCargarCiertoGdvConSeguridad(GridView gdvPosesiones)
        {
            foreach (GridViewRow row in gdvPosesiones.Rows)
            {
                TextBox txbCantidad = row.FindControl("txbCantidad") as TextBox;
                txbCantidad.Visible = false;
                Label lblVCantidad = row.FindControl("lblVCantidad") as Label;
                lblVCantidad.Visible = true;
            }
            gstBtnGuardarGastos.Visible = false;
        }

        protected void pssLlenarGridview(GridView gdv, String categoria, String descripcion)
        {
            gdv.DataSource = bdTS.pssObtenerPosesiones(S, F, L, categoria);
            gdv.DataBind();
            gdv.Columns[0].Visible = false;
            gdv.Columns[1].HeaderText = descripcion;
            gdv.Columns[2].HeaderText = dic.TScantidad;
        }

        protected void pssBtnGuardar_Click(object sender, EventArgs e)
        {
            pssGuardarGdv(pssGdvPosesiones1);
            pssGuardarGdv(pssGdvPosesiones2);
            pssGuardarGdv(pssGdvPosesiones3);
            pssGuardarGdv(pssGdvPosesiones4);
            pssGuardarGdv(pssGdvPosesiones5);
            pssGuardarGdv(pssGdvPosesiones6);
            pssGuardarGdv(pssGdvPosesiones7);
        }

        protected void pssGuardarGdv(GridView pssGdv)
        {
            int cantidad;
            String posesion;
            foreach (GridViewRow row in pssGdv.Rows)
            {
                TextBox txbCantidad = row.FindControl("txbCantidad") as TextBox;
                if (!txbCantidad.Text.Equals(""))
                {
                    cantidad = Int32.Parse(txbCantidad.Text);
                }
                else
                {
                    cantidad = 0;
                }
                txbCantidad.Text = cantidad + "";
                posesion = row.Cells[0].Text;
                try
                {
                    bdTS.pssInsertarPosesionC(S, F, posesion, U, cantidad);
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }

        protected void eliminarRegistro(object sender, EventArgs e)
        {
            try
            {
                switch (categoriaActual)
                {
                    case 1:
                        bdTS.vvnIngresarHistorico(S, F, vvnFechaInicioSLCT, U);
                        vvnPrepararElementosNuevoIngreso();
                        vvnLlenarHistorial();
                        mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                        break;
                    case 3:
                        ocpNuevaOcupacionPrepararElementos();
                        bdTS.ocpEliminarOcupacion(S, ocpMiembro, ocpOcupacion, ocpFechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss"), ocpFechaInicioSLCT.ToString("yyyy-MM-dd HH:mm:ss"), U);
                        ocpLlenarGridViewOcupaciones(ocpMiembro);
                        ocpLlenarGdvMiembros();
                        mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                        break;
                    case 4:
                        DateTime ahora = DateTime.Now;
                        String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
                        extNuevoIngresoPrepararElementos();
                        bdTS.extIngresarIngresoExtra(S, F, extTipo, fechaCreacion, extFechaInicioSLCT.ToString("yyyy-MM-dd HH:mm:ss"), U);
                        extLlenarGdvIngresos();
                        mst.mostrarMsjNtf(dic.msjSeHaEliminado);

                        break;
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
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

        protected static int ocpTotalMiembro;
        protected static int ocpTotalFamilia;
        protected void ocpGdvMiembros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblqy = (Label)e.Row.FindControl("lblIngresoMen");
                String strIngMen = lblqy.Text;
                strIngMen = strIngMen.Replace("&nbsp;", "");
                if (!String.IsNullOrEmpty(strIngMen))
                {
                    int qty = Int32.Parse(lblqy.Text);
                    ocpTotalFamilia = ocpTotalFamilia + qty;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].ColumnSpan = 5;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[8].ColumnSpan = 2;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[6].CssClass = "left";
                e.Row.Cells[6].Text = "Total: ";
                Label lblTotalqty = (Label)e.Row.FindControl("lblFtrIngresoMen");
                lblTotalqty.Text = "<b>" + ocpTotalFamilia.ToString() + "</b>";
            }
        }
        protected void ocpGdvOcupaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String fechaFin = e.Row.Cells[5].Text;
                fechaFin = fechaFin.Replace("&nbsp;", "");
                Label lblqy = (Label)e.Row.FindControl("lblIngresoMen");
                String strIngMen = lblqy.Text;
                strIngMen = strIngMen.Replace("&nbsp;", "");
                if (!String.IsNullOrEmpty(strIngMen) && String.IsNullOrEmpty(fechaFin))
                {
                    int qty = Int32.Parse(lblqy.Text);
                    ocpTotalMiembro = ocpTotalMiembro + qty;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].ColumnSpan = 2;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[7].ColumnSpan = 4;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[5].CssClass = "left";
                e.Row.Cells[5].Text = "Total: ";
                Label lblTotalqty = (Label)e.Row.FindControl("lblFtrIngresoMen");
                lblTotalqty.Text = "<b>" + ocpTotalMiembro.ToString() + "</b>";
            }
        }
    }
}