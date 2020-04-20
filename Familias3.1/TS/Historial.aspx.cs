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
    public partial class Historial : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static Diccionario dic;
        protected static mast mst;
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
                dic = new Diccionario(L, S);
                try
                {
                    llenarElementos();
                }
                catch
                {
                }
            }
            mst = (mast)Master;
        }
        protected void llenarElementos()
        {
            lblAntecedentes.Text = "<b>" + dic.TSantecedentesFamiliares.ToUpper() + "</b>";
            lblSituacionSocial.Text ="<b>" + dic.TSsituacionSocial.ToUpper() + "</b>";
            lblMedioAmbiente.Text = "<b>" + dic.TSmedioAmbiente.ToUpper() + "</b>";
            llenarTS();
            llenarAñoAfil();
            llenarInfoGeneral();
            llenarUltimoAñoEduc();
            llenarMedioAmbiente();
            llenarOcupaciones();
            llenarIngresosExtra();
            llenarTblFamilia();
            llenarTblEducacion();
            llenarTblSalud();
            llenarTblViolencia();
            llenarTblSociales();
            llenarTblAdicciones();
            llenarTblOtros();
            llenarTblFocos();
        }
        protected void llenarTS()
        {
            DataTable tblTS = BDF.obtenerDatos(S, F, L);
            String TS = tblTS.Rows[0]["TS"].ToString();
            if (!String.IsNullOrEmpty(TS))
            {
                String infoTS = "";
                infoTS = "<b>" + dic.trabajadorS + ": </b>";
                infoTS = infoTS + "<u>" + TS + "</u>";
                lblTS.Text = infoTS;
            }
        }
        protected void llenarAñoAfil()
        {
            DataTable tblInfoAñoAfil = BDF.obtenerDatos(S, F, L);
            String añoAfil = tblInfoAñoAfil.Rows[0]["AfilYear"].ToString();
            if (!String.IsNullOrEmpty(añoAfil))
            {
                String infoAñoAfil = "";
                if (L.Equals("es"))
                {
                    infoAñoAfil = "La familia está afiliada con el Proyecto desde ";
                }
                else
                {
                    infoAñoAfil = "The family is affiliated with the Project since ";
                }

                infoAñoAfil = infoAñoAfil + "<u>" + añoAfil + "</u>.";
                lblAñoAfil.Text = infoAñoAfil;
            }
        }
 
        protected void llenarInfoGeneral(){
            lblInfoGeneral.Text = dic.infoGeneral;
            gdvMiembros.Columns[0].HeaderText = dic.miembro;
            gdvMiembros.Columns[1].HeaderText = dic.nombre;
            gdvMiembros.Columns[2].HeaderText = dic.fechaNacimiento;
            gdvMiembros.Columns[3].HeaderText = dic.afiliacion;
            gdvMiembros.Columns[4].HeaderText = dic.celular;
            gdvMiembros.Columns[5].HeaderText = dic.relacion;
            gdvMiembros.DataSource = bdTS.hstInfoGeneral(S, F, L);
            gdvMiembros.DataBind();
        }
 
        protected void llenarUltimoAñoEduc()
        {
            lblAñoEduc.Text = dic.educacion;
            gdvAñoEduc.Columns[0].HeaderText = dic.nombre;
            gdvAñoEduc.Columns[1].HeaderText = dic.informacion;
            gdvAñoEduc.DataSource = bdTS.hstUltimoAñoEduc(S, F, L);
            gdvAñoEduc.DataBind();
        }

        protected void llenarOcupaciones()
        {
            lblOcupaciones.Text = dic.ocupaciones;
            gdvOcupaciones.Columns[0].HeaderText = dic.nombre;
            gdvOcupaciones.Columns[1].HeaderText = dic.TSocupacion;
            gdvOcupaciones.Columns[2].HeaderText = dic.fechaInicio;
            gdvOcupaciones.Columns[3].HeaderText = dic.TSingresoMensual + " (Q)";
            gdvOcupaciones.Columns[4].HeaderText = dic.TSjornada;
            gdvOcupaciones.Columns[5].HeaderText = dic.TShorasSemanales;
            gdvOcupaciones.Columns[6].HeaderText = dic.TStieneIGGS;
            DataTable dtOcupaciones = bdTS.hstOcupaciones(S, F, L);
            gdvOcupaciones.DataSource = dtOcupaciones;
            gdvOcupaciones.DataBind();
            verificarGvdVacio(dtOcupaciones, gdvOcupaciones, lblOcupNoTiene);
        }
        protected void llenarIngresosExtra()
        {
            lblIngresosExtra.Text = dic.ingresosExtra;
            gdvIngresosExtra.Columns[0].HeaderText = dic.tipo;
            gdvIngresosExtra.Columns[1].HeaderText = dic.fechaInicio;
            gdvIngresosExtra.Columns[2].HeaderText = dic.TSingresoMensual + " (Q)";
            gdvIngresosExtra.Columns[3].HeaderText = dic.nota;
            DataTable dtIngresosExtra = bdTS.hstIngresosExtra(S, F, L);
            gdvIngresosExtra.DataSource = dtIngresosExtra;
            gdvIngresosExtra.DataBind();
            verificarGvdVacio(dtIngresosExtra, gdvIngresosExtra, lblIngExtNoTiene);
        }

        protected void llenarMedioAmbiente()
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
            //vvnLblJardín.Text = dic.TStamañoAreaCultivo + ":";
            vvnLblMaterialCcna.Text = dic.TSmaterial;
            vvnLblMaterialCsa.Text = dic.TSmaterial;
            vvnLblNoCuartos.Text = dic.TSnumeroCuartos + ":";
            vvnLblNotasCcna.Text = dic.notas;
            vvnLblNotasCsa.Text = dic.notas;
            vvnLblNotasHigiene.Text = dic.notas + ":";
            vvnLblOtros.Text = dic.otros;
            vvnLblParedCcna.Text = dic.TSpared + ":";
            vvnLblParedCsa.Text = dic.TSpared + ":";
            vvnLblPisoCsa.Text = dic.TSpiso + ":";
            //vvnLblSegundoNivel.Text = dic.TStieneSegundoPiso + ":";
            vvnLblServicios.Text = dic.TSservicios;
            //vvnLblTamaño.Text = dic.TStamañoTerreno + ":";
            vvnLblTechoCcna.Text = dic.TStecho + ":";
            vvnLblTechoCsa.Text = dic.TStecho + ":";
            //vvnLblTenencia.Text = dic.TStenencia + ":";
            //vvnLblTerreno.Text = dic.TSpropiedad;
            //vvnLblTieneEsc.Text = dic.TStieneEscritura + ":";
            DataTable dtMedioAmbiente = bdTS.hstMedioAmbiente(S, F, L);
            DataRow rowMedioAmbiente = dtMedioAmbiente.Rows[0];
            String tenencia = rowMedioAmbiente["Tenencia"].ToString();
            String tamañoPropiedadX = rowMedioAmbiente["tamañoPropiedadX"].ToString();
            String tamañoPropiedadY = rowMedioAmbiente["tamañoPropiedadY"].ToString();
            String tamañoPropiedadXAreaVerde = rowMedioAmbiente["tamañoPropiedadXAreaVerde"].ToString();
            String tamañoPropiedadYAreaVerde = rowMedioAmbiente["tamañoPropiedadYAreaVerde"].ToString();
            String direccion = BDF.obtenerDatos(S, F, L).Rows[0]["Address"].ToString();
            String pueblo = rowMedioAmbiente["Pueblo"].ToString();
            String municipio = rowMedioAmbiente["Municipio"].ToString();
            String tiempoEnLugar = rowMedioAmbiente["TiempoEnLugar"].ToString();
            String tieneSegundoNivel = rowMedioAmbiente["tieneSegundoNivel"].ToString();
            String tieneEscritura = rowMedioAmbiente["TieneEscritura"].ToString();
            lblUbicacionTerreno.Text = "<b>" + dic.TSubicacionTerreno + ":</b>";
            String areaVerde = ". ";
            if (L.Equals("es"))
            {
                if ((!String.IsNullOrEmpty(tamañoPropiedadXAreaVerde) || tamañoPropiedadXAreaVerde.Equals("0")) && (!String.IsNullOrEmpty(tamañoPropiedadYAreaVerde) || tamañoPropiedadYAreaVerde.Equals("0")))
                {
                    areaVerde = ", con un área verde de <u>" + tamañoPropiedadXAreaVerde + "</u> metros x <u>" + tamañoPropiedadYAreaVerde + "</u> metros. ";
                }
                lblVUbicacionTerreno.Text = "La familia vive en un terreno con tenencia: <u>" + tenencia + "</u>; que mide aproximadamente <u>" + tamañoPropiedadX + "</u> metros x <u>" + tamañoPropiedadY + "</u> metros" + areaVerde + tieneEscritura + " tiene escritura. " + tieneSegundoNivel + " tiene segundo nivel. " + " Ubicado en <u>" + direccion + "</u>, aldea <u>" + pueblo + "</u>, municipio de <u>" + municipio + "</u>. Tienen <u>" + tiempoEnLugar + "</u> de vivir en este lugar.";
            }
            else
            {
                if (!String.IsNullOrEmpty(tamañoPropiedadXAreaVerde) && !String.IsNullOrEmpty(tamañoPropiedadYAreaVerde))
                {
                    areaVerde = ", with a <u>" + tamañoPropiedadXAreaVerde + "</u> meters x <u>" + tamañoPropiedadYAreaVerde + "</u> meters cultivated property. ";
                }
                if(tieneEscritura.Equals("Yes"))
                {
                    tieneEscritura = "Has deed.";
                }
                else
                {
                     tieneEscritura = "Does not have deed.";
                }
                if (tieneSegundoNivel.Equals("Yes"))
                {
                    tieneSegundoNivel = "Has second floor.";
                }
                else
                {
                    tieneSegundoNivel = "Does not have second floor.";
                }
                lblVUbicacionTerreno.Text = "The family lives in a terrain with ownership: <u>" + tenencia + "</u>; which measures approximately <u>" + tamañoPropiedadX + "</u> meters x <u>" + tamañoPropiedadY + "</u> meters" + areaVerde + tieneEscritura + " " + tieneSegundoNivel + " Located in <u>" + direccion + "</u>, <u>" + pueblo + "</u> pueblo, <u>" + municipio + "</u> municipality. Have <u>" + tiempoEnLugar + "</u> to live in that place.";
            }
            String materialPared = rowMedioAmbiente["MaterialPared"].ToString();
            String calidadMaterialPared = rowMedioAmbiente["CalidadMaterialPared"].ToString();
            String notasPared = rowMedioAmbiente["NotasPared"].ToString();
            String materialTecho = rowMedioAmbiente["MaterialTecho"].ToString();
            String calidadMaterialTecho = rowMedioAmbiente["CalidadMaterialTecho"].ToString();
            String notasTecho = rowMedioAmbiente["NotasTecho"].ToString();
            String materialPiso = rowMedioAmbiente["MaterialPiso"].ToString();
            String calidadMaterialPiso = rowMedioAmbiente["CalidadMaterialPiso"].ToString();
            String notasPiso = rowMedioAmbiente["NotasPiso"].ToString();
            String materialParedCocina = rowMedioAmbiente["MaterialParedCocina"].ToString();
            String calidadMaterialParedCocina = rowMedioAmbiente["CalidadMaterialParedCocina"].ToString();
            String notasParedCocina = rowMedioAmbiente["NotasParedCocina"].ToString();
            String materialTechoCocina = rowMedioAmbiente["MaterialTechoCocina"].ToString();
            String calidadMaterialTechoCocina = rowMedioAmbiente["CalidadMaterialTechoCocina"].ToString();
            String notasTechoCocina = rowMedioAmbiente["NotasTechoCocina"].ToString();
            String electricidad = rowMedioAmbiente["Electricidad"].ToString();
            String agua = rowMedioAmbiente["Agua"].ToString();
            String drenaje = rowMedioAmbiente["Drenaje"].ToString();
            String baño = rowMedioAmbiente["Baño"].ToString();
            String numeroCuartos = rowMedioAmbiente["NumeroCuartos"].ToString();
            String higiene = rowMedioAmbiente["Higiene"].ToString();
            String notasHigiene = rowMedioAmbiente["NotasHigiene"].ToString();
            vvnLblVMatParedCsa.Text = materialPared;
            vvnLblVCaldParedCsa.Text = calidadMaterialPared;
            vvnLblVNotasParedCsa.Text = notasPared;
            vvnLblVMatTechoCsa.Text = materialTecho;
            vvnLblVCaldTechoCsa.Text = calidadMaterialTecho;
            vvnLblVNotasTechoCsa.Text = notasTecho;
            vvnLblVMatPisoCsa.Text = materialPiso;
            vvnLblVCaldPisoCsa.Text = calidadMaterialPiso;
            vvnLblVNotasPisoCsa.Text = notasPiso;
            vvnLblVMatParedCcna.Text = materialParedCocina;
            vvnLblVCaldParedCcna.Text = calidadMaterialParedCocina;
            vvnLblVNotasParedCcna.Text = notasParedCocina;
            vvnLblVMatTechoCcna.Text = materialTechoCocina;
            vvnLblVCaldTechoCcna.Text = calidadMaterialTechoCocina;
            vvnLblVNotasTechoCcna.Text = notasTechoCocina;
            vvnLblVElectricidad.Text = electricidad;
            vvnLblVAgua.Text = agua;
            vvnlblVDrenaje.Text = drenaje;
            vvnLblVExcretas.Text = baño;
            vvnLblVNoCuartos.Text = numeroCuartos;
            //vvnLblVSegundoNivel.Text = tieneSegundoNivel;
            //vvnLblVTieneEsc.Text = tieneEscritura;
            vvnLblVHigiene.Text = higiene;
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

        protected void llenarTblFamilia()
        {
            lblFamilia.Text = dic.familia;
            DataTable dtFamilia = bdTS.hstFamilia(S, F, L);

            gdvFamilia.Columns[0].HeaderText = dic.fecha;
            gdvFamilia.Columns[1].HeaderText = dic.nota;

            gdvFamilia.Columns[0].ItemStyle.Width = 100;
            gdvFamilia.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvFamilia.DataSource = dtFamilia;
            gdvFamilia.DataBind();

            verificarGvdVacio(dtFamilia, gdvFamilia, lblFamNoTiene);

        }

        protected void llenarTblEducacion()
        {
            lblEducacion.Text = dic.educacion;
            DataTable dtEducacion = bdTS.hstEducacion(S, F, L);

            gdvEducacion.Columns[0].HeaderText = dic.fecha;
            gdvEducacion.Columns[1].HeaderText = dic.nota;

            gdvEducacion.Columns[0].ItemStyle.Width = 100;
            gdvEducacion.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvEducacion.DataSource = dtEducacion;
            gdvEducacion.DataBind();

            verificarGvdVacio(dtEducacion, gdvEducacion, lblEducNoTiene);

        }

        protected void llenarTblSalud()
        {
            lblSalud.Text = dic.salud;
            DataTable dtSalud = bdTS.hstSalud(S, F, L);

            gdvSalud.Columns[0].HeaderText = dic.fecha;
            gdvSalud.Columns[1].HeaderText = dic.nota;

            gdvSalud.Columns[0].ItemStyle.Width = 100;
            gdvSalud.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvSalud.DataSource = dtSalud;
            gdvSalud.DataBind();

            verificarGvdVacio(dtSalud, gdvSalud, lblSaludNoTiene);

        }

        protected void llenarTblAdicciones()
        {
            lblAdicciones.Text = dic.TSadicciones;
            DataTable dtAdicciones = bdTS.hstAdicciones(S, F, L);

            gdvAdicciones.Columns[0].HeaderText = dic.fecha;
            gdvAdicciones.Columns[1].HeaderText = dic.nota;

            gdvAdicciones.Columns[0].ItemStyle.Width = 100;
            gdvAdicciones.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvAdicciones.DataSource = dtAdicciones;
            gdvAdicciones.DataBind();

            verificarGvdVacio(dtAdicciones, gdvAdicciones, lblAdicNoTiene);

        }

        protected void llenarTblViolencia()
        {
            lblViolencia.Text = dic.TSviolencia;
            DataTable dtViolencia = bdTS.hstViolencia(S, F, L);

            gdvViolencia.Columns[0].HeaderText = dic.fecha;
            gdvViolencia.Columns[1].HeaderText = dic.nota;

            gdvViolencia.Columns[0].ItemStyle.Width = 100;
            gdvViolencia.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvViolencia.DataSource = dtViolencia;
            gdvViolencia.DataBind();

            verificarGvdVacio(dtViolencia, gdvViolencia, lblVioNoTiene);

        }

        protected void llenarTblSociales()
        {
            lblSociales.Text = dic.TSsocialesLegales;
            DataTable dtSociales = bdTS.hstSocialLegal(S, F, L);

            gdvSociales.Columns[0].HeaderText = dic.fecha;
            gdvSociales.Columns[1].HeaderText = dic.nota;

            gdvSociales.Columns[0].ItemStyle.Width = 100;
            gdvSociales.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvSociales.DataSource = dtSociales;
            gdvSociales.DataBind();

            verificarGvdVacio(dtSociales, gdvSociales, lblSocNoTiene);

        }

        protected void llenarTblOtros()
        {
            lblOtros.Text = dic.TSotros;
            DataTable dtOtros = bdTS.hstOtros(S, F, L);

            gdvOtros.Columns[0].HeaderText = dic.fecha;
            gdvOtros.Columns[1].HeaderText = dic.nota;

            gdvOtros.Columns[0].ItemStyle.Width = 100;
            gdvOtros.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvOtros.DataSource = dtOtros;
            gdvOtros.DataBind();

            verificarGvdVacio(dtOtros, gdvOtros, lblOtrosNoTiene);

        }

        protected void llenarTblFocos()
        {
            lblFocos.Text = dic.TSfocos;
            DataTable dtFocos = bdTS.hstFocoAtencion(S, F, L);

            gdvFocos.Columns[0].HeaderText = dic.fecha;
            gdvFocos.Columns[1].HeaderText = dic.nota;

            gdvFocos.Columns[0].ItemStyle.Width = 100;
            gdvFocos.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            gdvFocos.DataSource = dtFocos;
            gdvFocos.DataBind();

            verificarGvdVacio(dtFocos, gdvFocos, lblFocosNoTiene);

        }
        protected void gdvMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                LinkButton link = (LinkButton)gdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnMName");
                M = link.Text;
                mst.seleccionarMiembro(M);
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
            }
        }
    }
}