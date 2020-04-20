using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Drawing;
using AjaxControlToolkit;
namespace Familias3._1.TS
{
    public partial class Resumen : System.Web.UI.Page
    {
        private static String F;
        private static String U;
        private static String S;
        private static String L;
        private static int año;
        private static int cantMeses;
        private static BDTS bdTS;
        private static BDFamilia bdF;
        private static Diccionario dic;
        protected static mast mst;
        protected static Color colorBueno = Color.MediumSeaGreen;
        protected static Color colorRegular = Color.Yellow;
        protected static Color colorMalo = Color.Crimson;
        protected static int soloUltimas;
        protected void Page_Load(object sender, EventArgs e)
        {
            mst = (mast)Master;
            año = DateTime.Now.Year;
            cantMeses = 4;
            if (!IsPostBack)
            {
                soloUltimas = 0;
                asignaColores();
                U = mast.U;
                S = mast.S;
                L = mast.L;
                F = mast.F;
                bdTS = new BDTS();
                bdF = new BDFamilia();
                dic = new Diccionario(L, S);
                try
                {
                    DataTable dt = bdF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    llenarNombres();
                    pnlContenedor.Visible = false;
                    try
                    {
                        buscar();
                        visibilizarPestaña(pnlInfoGeneral, lnkInfoGeneral);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjStc(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                    }
                }
                catch
                {
                }
            }
        }

        protected void lnkInfoGeneral_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlInfoGeneral, lnkInfoGeneral);
        }

        protected void lnkInfoBecas_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlInfoBecas, lnkInfoBecas);
        }

        protected void lnkInfoProgramas_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlInfoProgramas, lnkInfoProgramas);
        }

        protected void lnkInfoApadAfil_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlInfoApadAfil, lnkInfoApadAfil);
        }

        protected void lnkInfoOtros_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlInfoOtros, lnkInfoOtros);
        }

        protected void llenarNombres()
        {
            lblDirec.Text = dic.direccion + ":";
            lblTelef.Text = dic.telefono + ":";
            lblTSU.Text = dic.trabajadorS + ":";
            lblClasif.Text = dic.clasificacion + ":";
            lblCantMeses.Text = dic.mesesAtras + " (" + dic.actividades + "):";
            btnBuscar.Text = dic.buscar;
            lnkInfoGeneral.Text = "General";
            lnkInfoBecas.Text = dic.becas;
            lnkInfoProgramas.Text = dic.programasEducativos;
            lnkInfoApadAfil.Text = dic.apadrinamiento;
            lnkInfoOtros.Text = dic.otros;
            revTxbCantMeses_ValidatorCalloutExtender.PopupPosition = ValidatorCalloutPosition.Left;
            revtxbCantMeses.ErrorMessage = dic.msjParametroNecesario;
        }
        protected void llenarInfoFamiliar()
        {
            llenarTblApad();
            llenarTblApoJov();
            llenarAvisos();
            llenarTblDiferencias();
            llenarTblEduc();
            llenarTblMiembros();
            llenarTblEducGeneral();
            llenarTblNotas();
            llenarTblPsic();
            llenarTblSalud();
            llenarTblNADFAS();
        }
        protected void llenarInfoIndividual()
        {
            llenarTblCaja();
            llenarTblTS();
            llenarTblVVnd();
        }
        private void llenarAvisos()
        {
            DataTable dtAvisos = new BDFamilia().obtenerAvisos(S, F, L, true);
            gdvAvisos.Columns[0].HeaderText = dic.avisos;
            if (dtAvisos.Rows.Count > 0)
            {
                //gdvAvisos.Columns[1].Visible = false;
                gdvAvisos.DataSource = dtAvisos;
                gdvAvisos.DataBind();
            }
            else
            {
                //gdvAvisos.Columns[1].HeaderText = dic.avisos;
                //gdvAvisos.Columns[0].Visible = false;
                DataTable dtAvisosAux = new DataTable();
                dtAvisosAux.Columns.Add("Aviso");
                dtAvisosAux.Rows.Add(dic.noTiene);
                gdvAvisos.DataSource = dtAvisosAux;
                gdvAvisos.DataBind();
            }
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

        protected void llenarTblApad()
        {
            lblApad.Text = dic.TSactividadesApad;
            DataTable dtApad = bdTS.rsmApad(L, S, F, cantMeses);
            gdvApad.Columns[0].HeaderText = dic.actividad;
            gdvApad.Columns[1].HeaderText = dic.fecha;
            gdvApad.DataSource = dtApad;
            gdvApad.DataBind();
            verificarGvdVacio(dtApad, gdvApad, lblApadNoTiene);
        }

        protected void llenarTblApoJov()
        {
            lblApoJov.Text = dic.TSactividadesApoyoJov;
            gdvApoyoJov.Columns[0].HeaderText = dic.programa;
            gdvApoyoJov.Columns[1].HeaderText = dic.nombre;
            gdvApoyoJov.Columns[2].HeaderText = dic.obtenerMesAbr(1);
            gdvApoyoJov.Columns[3].HeaderText = dic.obtenerMesAbr(2);
            gdvApoyoJov.Columns[4].HeaderText = dic.obtenerMesAbr(3);
            gdvApoyoJov.Columns[5].HeaderText = dic.obtenerMesAbr(4);
            gdvApoyoJov.Columns[6].HeaderText = dic.obtenerMesAbr(5);
            gdvApoyoJov.Columns[7].HeaderText = dic.obtenerMesAbr(6);
            gdvApoyoJov.Columns[8].HeaderText = dic.obtenerMesAbr(7);
            gdvApoyoJov.Columns[9].HeaderText = dic.obtenerMesAbr(8);
            gdvApoyoJov.Columns[10].HeaderText = dic.obtenerMesAbr(9);
            gdvApoyoJov.Columns[11].HeaderText = dic.obtenerMesAbr(10);
            gdvApoyoJov.Columns[12].HeaderText = dic.obtenerMesAbr(11);
            gdvApoyoJov.Columns[13].HeaderText = dic.obtenerMesAbr(12);
            gdvApoyoJov.Columns[14].HeaderText = dic.total;
            DataTable dtAsistenciasPEduc = bdTS.rsmApjo(L, S, F, año);
            gdvApoyoJov.DataSource = dtAsistenciasPEduc;
            gdvApoyoJov.DataBind();
            verificarGvdVacio(dtAsistenciasPEduc, gdvApoyoJov, lblApoyoJovNoTiene);
        }

        protected void llenarTblCaja()
        {
            lblCaja.Text = dic.caja;

            //gdvCaja.Columns[0].HeaderText = dic.cuentaActiva;
            //gdvCaja.Columns[1].HeaderText = dic.actualizacion;

            lblCuentaActiva.Text = dic.cuentaActiva + ":";
            lblActualizacion.Text = dic.actualizacion + ":";
            DataTable dtCaja = bdTS.rsmCaja(L, S, F);
            if (dtCaja.Rows.Count > 0)
            {
                lblVCuentaActiva.Text = dtCaja.Rows[0]["Cta_Activa"].ToString();
                lblVActualizacion.Text = dtCaja.Rows[0]["Actualización"].ToString();
            }

            //gdvCaja.DataSource = dtCaja;
            //gdvCaja.DataBind();
            //verificarGvdVacio(dtCaja, gdvCaja, lblCajaNoTiene);
        }

        protected void llenarTblDiferencias()
        {
            int totalGastos = bdTS.rsmTotalGastos(S, F);
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
        protected void llenarTblEduc()
        {
            lblEduc.Text = dic.TSactividadesBecas;
            gdvEduc.Columns[0].HeaderText = dic.actividad;
            gdvEduc.Columns[1].HeaderText = dic.fecha;
            DataTable dtEduc = bdTS.rsmEduc(L, S, F, cantMeses);
            gdvEduc.DataSource = dtEduc;
            gdvEduc.DataBind();
            verificarGvdVacio(dtEduc, gdvEduc, lblEducNoTiene);
        }

        protected void llenarTblMiembros()
        {
            lblInfoGeneral.Text = dic.infoGeneral;
            gdvMiembros.Columns[0].HeaderText = dic.id;
            gdvMiembros.Columns[1].HeaderText = dic.nombre;
            gdvMiembros.Columns[2].HeaderText = dic.fechaNacimiento;
            gdvMiembros.Columns[3].HeaderText = dic.relacion;
            gdvMiembros.Columns[4].HeaderText = dic.inactivoRazon;
            gdvMiembros.Columns[5].HeaderText = dic.telefono;
            DataTable dtMiembros = bdTS.rsmMiembros(L, S, F);
            gdvMiembros.DataSource = dtMiembros;
            gdvMiembros.DataBind();
        }

        protected void llenarTblEducGeneral()
        {
            lblInfoEducGeneral.Text = dic.infoGeneral + " " + dic.educacion;
            gdvEducGeneral.Columns[0].HeaderText = dic.nombre;
            gdvEducGeneral.Columns[1].HeaderText = dic.afiliacion;
            gdvEducGeneral.Columns[2].HeaderText = dic.fase;
            gdvEducGeneral.Columns[3].HeaderText = dic.educacion;
            DataTable dtEducGeneral = bdTS.rsmMiembros(L, S, F);
            gdvEducGeneral.DataSource = dtEducGeneral;
            gdvEducGeneral.DataBind();
        }

        protected void llenarTblNotas(int soloUltimas)
        {
            lblNotas.Text = dic.calificaciones + " (" + DateTime.Now.Year + ")";
            gdvNotas.Columns[0].HeaderText = dic.miembro;
            gdvNotas.Columns[1].HeaderText = dic.nombre;
            gdvNotas.Columns[2].HeaderText = dic.fase;
            gdvNotas.Columns[3].HeaderText = dic.unidad;
            gdvNotas.Columns[4].HeaderText = dic.semaforo;
            gdvNotas.Columns[5].HeaderText = dic.aproboTodas;
            gdvNotas.Columns[6].HeaderText = dic.cantidadPerdidas;
            gdvNotas.Columns[7].HeaderText = dic.fuente;
            DataTable dtNotas = bdTS.rsmNotas(L, S, F, soloUltimas);
            gdvNotas.DataSource = dtNotas;
            gdvNotas.DataBind();
            gdvNotas.Columns[0].Visible = false;
            verificarGvdVacio(dtNotas, gdvNotas, lblNotasNoTiene);
        }

        protected void llenarTblPsic()
        {
            lblPsic.Text = dic.TSactividadesApoyoEduc;
            gdvPsic.Columns[0].HeaderText = dic.actividad;
            gdvPsic.Columns[1].HeaderText = dic.fecha;
            DataTable dtPsic = bdTS.rsmPsic(L, S, F, cantMeses);
            gdvPsic.DataSource = dtPsic;
            gdvPsic.DataBind();
            verificarGvdVacio(dtPsic, gdvPsic, lblPsicNoTiene);
        }

        protected void llenarTblSalud()
        {
            lblSalud.Text = dic.TSactividadesClinica;
            gdvSalud.Columns[0].HeaderText = dic.actividad;
            gdvSalud.Columns[1].HeaderText = dic.fecha;
            DataTable dtSalud = bdTS.rsmSalud(L, S, F, cantMeses);
            gdvSalud.DataSource = dtSalud;
            gdvSalud.DataBind();
            verificarGvdVacio(dtSalud, gdvSalud, lblSaludNoTiene);
        }

        protected void llenarTblNADFAS()
        {
            lblNADFASs.Text = "NADFAS " + "(" + (DateTime.Now.Year + 1) + ")";
            gdvNADFAS.Columns[0].HeaderText = dic.nombre;
            gdvNADFAS.Columns[1].HeaderText = dic.fechaInicio;
            DataTable dtNADFAS = bdTS.rsmNADFAS(S, F, L);
            gdvNADFAS.DataSource = dtNADFAS;
            gdvNADFAS.DataBind();
            verificarGvdVacio(dtNADFAS, gdvNADFAS, lblNADFASNoTiene);
        }

        protected void llenarTblTS()
        {
            lblTS.Text = dic.TSactividadesTS;
            DataTable dtTS = bdTS.rsmTS(L, S, F, cantMeses);
            gdvTS.Columns[0].HeaderText = dic.actividad;
            gdvTS.Columns[1].HeaderText = dic.fecha;
            gdvTS.DataSource = dtTS;
            gdvTS.DataBind();
            verificarGvdVacio(dtTS, gdvTS, lblTSNoTiene);
        }

        protected void llenarTblVVnd()
        {
            lblVvd.Text = dic.viviendas;
            gdvVvd.Columns[0].HeaderText = dic.año;
            gdvVvd.Columns[1].HeaderText = dic.tipo;
            gdvVvd.Columns[2].HeaderText = dic.aplica;
            gdvVvd.Columns[3].HeaderText = dic.diagnostico;
            gdvVvd.Columns[4].HeaderText = dic.estado;
            gdvVvd.Columns[5].HeaderText = dic.notas;
            gdvVvd.Columns[6].HeaderText = dic.solicitud;
            gdvVvd.Columns[7].HeaderText = dic.horasRequeridas;
            gdvVvd.Columns[8].HeaderText = dic.exoneracion;
            gdvVvd.Columns[9].HeaderText = dic.horasTrabajadas;
            DataTable dtVvnd = bdTS.rsmVvnd(L, S, F);
            gdvVvd.DataSource = dtVvnd;
            gdvVvd.DataBind();
            verificarGvdVacio(dtVvnd, gdvVvd, lblVVnNoTiene);
        }

        protected void ocultarPestañas()
        {
            pnlInfoGeneral.Visible = false;
            pnlInfoBecas.Visible = false;
            pnlInfoProgramas.Visible = false;
            pnlInfoApadAfil.Visible = false;
            pnlInfoOtros.Visible = false;
            lnkInfoGeneral.CssClass = "cabecera";
            lnkInfoBecas.CssClass = "cabecera";
            lnkInfoProgramas.CssClass = "cabecera";
            lnkInfoApadAfil.CssClass = "cabecera";
            lnkInfoOtros.CssClass = "cabecera";
            asignaColores();
        }

        protected void asignaColores()
        {
            lnkInfoGeneral.CssClass = lnkInfoGeneral.CssClass + " blueCbc";
            lnkInfoBecas.CssClass = lnkInfoBecas.CssClass + " orangeCbc";
            lnkInfoProgramas.CssClass = lnkInfoProgramas.CssClass + " purpleCbc";
            lnkInfoApadAfil.CssClass = lnkInfoApadAfil.CssClass + " pinkCbc";
            lnkInfoOtros.CssClass = lnkInfoOtros.CssClass + " greenCbc";
        }

        protected void visibilizarPestaña(Panel pnl, LinkButton lnk)
        {
            pnl.Visible = true;
            lnk.CssClass = lnk.CssClass + " c-activa";
        }

        protected void gdvNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //String miembro = e.Row.Cells[0].Text;
                //btnBuscar.Text = miembro;
                //DataTable dtUnidades = bdTS.rsmObtenerUnidades("F", miembro, "es");
                //DropDownList DropDownList1 =(DropDownList)e.Row.FindControl("ddlUnidades");
                //DropDownList1.DataSource = dtUnidades;
                //DropDownList1.DataTextField = "Code";
                //DropDownList1.DataValueField = "Des";
                //DropDownList1.DataBind();
            }
        }

        protected void buscar()
        {
            cantMeses = Int32.Parse(txbCantMeses.Text);
            llenarInfoFamiliar();
            llenarInfoIndividual();
            llenarAvisos();
            pnlContenedor.Visible = true;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cantMeses = Int32.Parse(txbCantMeses.Text);
            soloUltimas = 0;
            if ((cantMeses > 0) && (cantMeses < 13))
            {
                try
                {
                    buscar();
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("La cantidad de Meses Atrás, debe estar entre 1 y 12.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("The number of Months Ago, must be between 1 and 12.");
                }
            }
        }

        protected void gdvMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                LinkButton link = (LinkButton)gdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnMName");
                String M = link.Text;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
        }

        protected void btnMostrarTodasNotas_Click(object sender, EventArgs e)
        {
            llenarTblNotas();
        }

        protected void llenarTblNotas()
        {
            if (soloUltimas == 1)
            {
                btnMostrarTodasNotas.Text = dic.TSmostrarUltimas;
                soloUltimas = 0;
            }
            else
            {
                btnMostrarTodasNotas.Text = dic.TSmostrarTodas; ;
                soloUltimas = 1;
            }
            llenarTblNotas(soloUltimas);
        }
    }
}