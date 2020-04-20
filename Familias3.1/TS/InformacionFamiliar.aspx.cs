using AjaxControlToolkit;
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
    public partial class InformacionFamiliar : System.Web.UI.Page
    {
        public static BDMISC bdMISC;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asignaColores();
                bdMISC = new BDMISC();
                bdGEN = new BDGEN();
                BDF = new BDFamilia();
                F = mast.F;
                S = mast.S;
                U = mast.U;
                L = mast.L;
                dic = new Diccionario(L, S);
                try
                {
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    cargarPagina();
                }
                catch
                {
                }
            }
            mst = (mast)Master;
        }
        protected void cargarPagina()
        {
            lblDirec.Text = dic.direccion + ":";
            lblTelef.Text = dic.telefono + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblClasif.Text = dic.clasificacion + ":";
            lnkIngresarMiembro.Text = dic.ingresarMiembro;
            lnkModificarMiembro.Text = dic.actualizarMiembro;
            lnkModificarFamilia.Text = dic.actualizarFamilia;
            lnkReasignarFamiliaMiembro.Text = dic.actualizar + " " + dic.relacionesFamiliares;
            lnkTelefonos.Text = dic.telefonos;
            mdfFCargarPagina();
            ingMCargarPagina();
            mdfMCargarPagina();
            rsgCargarPagina();
            tlfCargarPagina();
            visibilizarPestaña(pnlIngresarMiembro, lnkIngresarMiembro);
            String categoria = (String)Session["CatINFOF"];
            if (categoria.Equals("INGM"))
            {
                ocultarPestañas();
                visibilizarPestaña(pnlIngresarMiembro, lnkIngresarMiembro);
            }
            else
                if (categoria.Equals("MDFM"))
                {
                    ocultarPestañas();
                    visibilizarPestaña(pnlModificarMiembro, lnkModificarMiembro);
                }
                else
                    if (categoria.Equals("MDFF"))
                    {
                        ocultarPestañas();
                        visibilizarPestaña(pnlModificarFamilia, lnkModificarFamilia);
                    }
                    else
                        if (categoria.Equals("RSG"))
                        {
                            ocultarPestañas();
                            visibilizarPestaña(rsgPnlReasignarFamiliaAMiembro, lnkReasignarFamiliaMiembro);
                        }
                        else
                            if (categoria.Equals("TLF"))
                            {
                                ocultarPestañas();
                                visibilizarPestaña(pnlTelefonos, lnkTelefonos);
                            }
        }
        protected void lnkIngresarMiembro_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlIngresarMiembro, lnkIngresarMiembro);
            Session["CatINFOF"] = "INGM";
        }

        protected void lnkModificarMiembro_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlModificarMiembro, lnkModificarMiembro);
            Session["CatINFOF"] = "MDFM";
        }

        protected void lnkModificarFamilia_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlModificarFamilia, lnkModificarFamilia);
            Session["CatINFOF"] = "MDFF";
        }

        protected void lnkReasignarFamiliaMiembro_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(rsgPnlReasignarFamiliaAMiembro, lnkReasignarFamiliaMiembro);
            Session["CatINFOF"] = "RSG";
        }
        protected void lnkTelefonos_Click(object sender, EventArgs e)
        {
            ocultarPestañas();
            visibilizarPestaña(pnlTelefonos, lnkTelefonos);
            Session["CatINFOF"] = "TLF";
        }
        protected void ocultarPestañas()
        {
            pnlIngresarMiembro.Visible = false;
            pnlModificarMiembro.Visible = false;
            pnlModificarFamilia.Visible = false;
            rsgPnlReasignarFamiliaAMiembro.Visible = false;
            pnlTelefonos.Visible = false;
            lnkIngresarMiembro.CssClass = "cabecera";
            lnkModificarMiembro.CssClass = "cabecera";
            lnkModificarFamilia.CssClass = "cabecera";
            lnkReasignarFamiliaMiembro.CssClass = "cabecera";
            lnkTelefonos.CssClass = "cabecera";
            asignaColores();
        }

        protected void asignaColores()
        {
            lnkIngresarMiembro.CssClass = lnkIngresarMiembro.CssClass + " orangeCbc";
            lnkModificarMiembro.CssClass = lnkModificarMiembro.CssClass + " pinkCbc";
            lnkModificarFamilia.CssClass = lnkModificarFamilia.CssClass + " blueCbc";
            lnkReasignarFamiliaMiembro.CssClass = lnkReasignarFamiliaMiembro.CssClass + " greenCbc";
            lnkTelefonos.CssClass = lnkTelefonos.CssClass + " purpleCbc";
        }

        protected void visibilizarPestaña(Panel pnl, LinkButton lnk)
        {
            pnl.Visible = true;
            lnk.CssClass = lnk.CssClass + " c-activa";
        }


        //INGRESAR MIEMBRO
        static protected String ingMnombres;
        static protected String ingMapellidos;
        static protected String ingMstrFechaNacimiento;
        static protected String ingMnombreUsual;
        static protected String ingMgenero;
        static protected String ingMCUI;
        static protected Boolean ingMtenemosCUI;
        static protected String ingMultimoGrado;
        static protected String ingMpuedeLeer;
        static protected String ingMnumeroCelular;
        static protected String ingMotraAfil;
        static protected String ingMmiembro;
        static protected String ingMmadreBio;
        static protected String ingMpadreBio;
        protected void ingMCargarPagina()
        {
            ingMllenarPnlIngresar();
            ingMllenarGdvMiembros(bdMISC.mdfFobtenerMiembros(S, F, L));
        }
        protected void ingMllenarPnlIngresar()
        {
            ingMRevTxbNombres.ErrorMessage = dic.msjCampoNecesario;
            ingMRevTxbApellidos.ErrorMessage = dic.msjCampoNecesario;
            ingMRevddlGenero.ErrorMessage = dic.msjCampoNecesario;
            ingMRevTxbNombres.ErrorMessage = dic.msjCampoNecesario;
            ingMRevtxbDiaNacimiento.ErrorMessage = dic.msjCampoNecesario;
            ingMRevddlMesNacimiento.ErrorMessage = dic.msjCampoNecesario;
            ingMRevtxbAñoNacimiento.ErrorMessage = dic.msjCampoNecesario;
            ingMlblNombres.Text = "*" + dic.nombres + ":";
            ingMlblNacimiento.Text = "*" + dic.fechaNacimiento + ":";
            ingMlblNombreUsual.Text = "&nbsp;" + dic.nombreUsual + ":";
            ingMlblCUI.Text = "&nbsp;" + dic.DPI + ":";
            ingMlblUltimoGrado.Text = "&nbsp;" + dic.ultimoGrado + ":";
            ingMlblNumeroCelular.Text = "&nbsp;" + dic.telefono + ":";
            ingMlblApellidos.Text = "*" + dic.apellidos + ":";
            ingMlblGenero.Text = "*" + dic.genero + ":";
            ingMlblHayCUI.Text = "&nbsp;" + dic.tieneCUI;
            ingMlblPuedeLeer.Text = "&nbsp;" + dic.lee + ":";
            ingMlblUltimoGrado.Text = "&nbsp;" + dic.ultimoGrado + ":";
            ingMlblOtraAfil.Text = "&nbsp;" + dic.otraAfil + ":";
            ingMlblNombreMadre.Text = "&nbsp;" + dic.nombreMadre + ":";
            ingMlblNombrePadre.Text = "&nbsp;" + dic.nombrePadre + ":";
            ingMlblNumeroMadre.Text = "&nbsp;" + dic.numeroMadre + ":";
            ingMlblNumeroPadre.Text = "&nbsp;" + dic.numeroPadre + ":";
            ingMbtnAceptar.Text = dic.aceptar;
            ingMbtnGuardar.Text = dic.guardar;
            ingMbtnReestablecerRel.Text = dic.TSreestablecerRelaciones;
            ingMbtnReestablecer.Text = dic.corregirDatos;
            ingMbtnGuardarConAjenas.Text = dic.guardar;
            ingMbtnReestablecerConAjenas.Text = dic.TSreestablecerRelaciones;
            ingMlblRelacionesajenas.Text = dic.TSinactivarRelacionOtraFamilia;
            llenarAlfabetismo(ingMddlPuedeLeer);
            llenarGenero(ingMddlGenero);
            llenarGrados(ingMddlUltimoGrado);
            llenarMeses(ingMddlMesNacimiento);
            llenarOtrasAfiliaciones(ingMddlOtraAfil);
            ingMtxbNombres.MaxLength = 40;
            ingMtxbApellidos.MaxLength = 40;
            ingMtxbNombreUsual.MaxLength = 40;
            ingMtxbCUI.MaxLength = 20;
        }
        protected void ingMbtnGuardar_Click(object sender, EventArgs e)
        {
            int verificar = ingMverificarRelaciones();
            if (verificar == 0)
            {
                try
                {
                    ingMmiembro = bdMISC.ingMingresarMiembro(S, U, F, ingMnombres, ingMapellidos, ingMstrFechaNacimiento, ingMgenero, ingMnombreUsual, ingMCUI, ingMtenemosCUI, ingMultimoGrado, ingMpuedeLeer, ingMnumeroCelular, ingMotraAfil, ingMmadreBio, ingMpadreBio);
                    if (ingMhayMiembrosConOtrasRelaciones == 0)
                    {
                        try
                        {
                            ingMguardarRelaciones();
                            ingMlimpiarElementos();
                            mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                            ingMdesactivarActivarElementos(true);
                            ingMbtnGuardar.Visible = false;
                            ingMbtnReestablecerRel.Visible = false;
                            ingMbtnReestablecer.Visible = false;
                            ingMbtnAceptar.Visible = true;
                            mst.seleccionarMiembro(ingMmiembro);
                        }
                        catch (Exception ex)
                        {
                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                        }
                    }
                    else if (ingMhayMiembrosConOtrasRelaciones == 1)
                    {
                        ingMprepararLlenarRelacionesAjenas();
                    }
                    //else if (ingMhayMiembrosConOtrasRelaciones == 2)
                    //{
                    //    if(L.Equals("es"))
                    //    {
                    //        mst.mostrarMsj("No se pueden realizar los cambios; los siguientes miembros son Jefes de Casa en otra familia: \n" + ingMMiembrosJefesOtrasRelaciones + "\n");
                    //    }
                    //    else
                    //    {
                    //        mst.mostrarMsj("The changes cannot be made; the following members are Head of Houese in another family: \n" + ingMMiembrosJefesOtrasRelaciones + "\n"); 
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            else if (verificar == 1)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("No especificó ningún Jefe de Casa activo. Debe haber un Jefe de Casa activo.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("You did not specify any active Head of House. There must be an active Head of House.");
                }
            }
            else if (verificar == 2)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Especificó más de un Jefe de Casa activo. Debe haber un solo Jefe de Casa activo.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("You specified more than one active Head of House. There must be only one active Head of House.");
                }
            }
            else if (verificar == 3)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, asegurese de que todos los miembros tengan asignada una relación.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, make sure all members are assigned a relation.");
                }
            }
        }

        protected void ingMprepararLlenarRelacionesAjenas()
        {
            ingMllenarRelacionesAjenas();
            ingMbtnGuardar.Visible = false;
            ingMbtnReestablecerRel.Visible = false;
            ingMbtnReestablecer.Visible = false;
            ingMbtnGuardarConAjenas.Visible = true;
            ingMbtnReestablecerConAjenas.Visible = true;
            ingMActivarDesactivarGdvRelaciones(false);
        }
        protected void ingMActivarDesactivarGdvRelaciones(Boolean activo)
        {
            foreach (GridViewRow row in ingMgdvMiembros.Rows)
            {
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                ddlRelacion.Enabled = activo;
                ddlRazonInactivo.Enabled = activo;
            }
        }
        protected void ingMllenarRelacionesAjenas()
        {
            ingMpnlRelacionesAjenas.Visible = true;
            ingMgdvRelacionesAjenas.Columns[0].HeaderText = dic.miembro;
            ingMgdvRelacionesAjenas.Columns[1].HeaderText = dic.familia;
            ingMgdvRelacionesAjenas.Columns[2].HeaderText = dic.nombre;
            ingMgdvRelacionesAjenas.Columns[3].HeaderText = dic.relacion;
            ingMgdvRelacionesAjenas.Columns[4].HeaderText = dic.inactivoRazon;
            ingMgdvRelacionesAjenas.DataSource = bdMISC.mdfFobtenerRelacionesActivasOtrasFamilias(S, F, L, ingMmiembrosAExcluir);
            ingMgdvRelacionesAjenas.DataBind();
            ingMgdvRelacionesAjenas.Columns[5].Visible = false;
            if (ingMhayJefesEnRelacionesAjenas)
            {
                ingMgdvRelacionesAjenas.Columns[4].Visible = false;
                ingMpnlMiembros.Visible = false;
                ingMbtnGuardarConAjenas.Visible = false;
            }
        }
        protected void ingMbtnReestablecer_Click(object sender, EventArgs e)
        {
            ingMdesactivarActivarElementos(true);
            ingMbtnGuardar.Visible = false;
            ingMbtnReestablecerRel.Visible = false;
            ingMbtnReestablecer.Visible = false;
            ingMbtnAceptar.Visible = true;
            ingMllenarGdvMiembros(bdMISC.mdfFobtenerMiembros(S, F, L));
        }
        protected void ingMbtnAceptar_Click(object sender, EventArgs e)
        {
            ingMnombres = ingMtxbNombres.Text;
            ingMapellidos = ingMtxbApellidos.Text;
            ingMstrFechaNacimiento = "";
            String diaNacimiento = ingMtxbDiaNacimiento.Text;
            String mesNacimiento = ingMddlMesNacimiento.SelectedValue;
            String añoNacimiento = ingMtxbAñoNacimiento.Text;
            DateTime fechaNacimiento = DateTime.Now;
            Boolean fechaNacimientoCorrecta = true;
            if (!String.IsNullOrEmpty(diaNacimiento) && !String.IsNullOrEmpty(mesNacimiento) && !String.IsNullOrEmpty(añoNacimiento))
            {
                if (validarFechas(añoNacimiento, mesNacimiento, diaNacimiento))
                {
                    fechaNacimiento = Convert.ToDateTime(añoNacimiento + "-" + mesNacimiento + "-" + diaNacimiento);
                    ingMstrFechaNacimiento = fechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    fechaNacimientoCorrecta = false;
                }
            }
            if (fechaNacimientoCorrecta)
            {
                if (fechaNacimiento <= DateTime.Now)
                {
                    ingMnombreUsual = ingMtxbNombreUsual.Text;
                    ingMgenero = ingMddlGenero.SelectedValue;
                    ingMCUI = ingMtxbCUI.Text;
                    ingMtenemosCUI = ingMchkHayCUI.Checked;
                    ingMultimoGrado = ingMddlUltimoGrado.SelectedValue;
                    ingMpuedeLeer = ingMddlPuedeLeer.SelectedValue;
                    String tel1 = ingMtxbNumeroCelular1.Text;
                    String tel2 = ingMtxbNumeroCelular2.Text;
                    ingMnumeroCelular = "";
                    Boolean numeroEsCorrecto = false;
                    if (!String.IsNullOrEmpty(tel1) && !String.IsNullOrEmpty(tel2))
                    {
                        if (tel1.Length == 4 && tel2.Length == 4)
                        {
                            ingMnumeroCelular = tel1 + "-" + tel2;
                            numeroEsCorrecto = true;
                        }
                    }

                    if (String.IsNullOrEmpty(tel1) && String.IsNullOrEmpty(tel2))
                    {
                        numeroEsCorrecto = true;
                    }

                    if (numeroEsCorrecto)
                    {
                        ingMotraAfil = ingMddlOtraAfil.SelectedValue;

                        //Validaciones para comprobar si los Padres Biológicos, son aceptables.
                        ingMlblVNombreMadre.Text = "";
                        ingMlblVNombrePadre.Text = "";
                        int resultadoMadre = 1;
                        int resultadoPadre = 1;
                        String madreBio = ingMtxbNumeroMadre.Text;
                        String padreBio = ingMtxbNumeroPadre.Text;
                        ingMmadreBio = madreBio;
                        ingMpadreBio = padreBio;
                        if (!String.IsNullOrEmpty(madreBio))
                        {
                            DataTable dtMadre = new BDMiembro().obtenerDatos(S, madreBio, L);
                            if (dtMadre.Rows.Count != 0)
                            {
                                String generoMadre = dtMadre.Rows[0]["Gender"].ToString();
                                if (generoMadre.Equals("Female") || generoMadre.Equals("Femenino"))
                                {
                                    resultadoMadre = 1;
                                }
                                else
                                {
                                    resultadoMadre = 2;
                                }
                            }
                            else
                            {
                                resultadoMadre = 3;
                            }
                        }
                        if (!String.IsNullOrEmpty(padreBio))
                        {
                            DataTable dtPadre = new BDMiembro().obtenerDatos(S, padreBio, L);
                            if (dtPadre.Rows.Count != 0)
                            {
                                String generoPadre = dtPadre.Rows[0]["Gender"].ToString();
                                if (generoPadre.Equals("Male") || generoPadre.Equals("Masculino"))
                                {
                                    resultadoPadre = 1;
                                }
                                else
                                {
                                    resultadoPadre = 2;
                                }
                            }
                            else
                            {
                                resultadoPadre = 3;
                            }
                        }

                        if (resultadoMadre == 1 && resultadoPadre == 1)
                        {

                            if (!String.IsNullOrEmpty(madreBio) && !madreBio.Equals("0"))
                            {
                                mdfMtxbNumeroMadre.Text = madreBio;
                                DataTable dtMadre = new BDMiembro().obtenerDatos(S, madreBio, L);
                                if (dtMadre.Rows.Count != 0)
                                {
                                    ingMlblVNombreMadre.Text = "&nbsp;&nbsp;" + dtMadre.Rows[0]["FirstNames"].ToString() + " " + dtMadre.Rows[0]["LastNames"].ToString();
                                }
                            }
                            else
                            {
                                mdfMtxbNumeroMadre.Text = "";
                            }

                            if (!String.IsNullOrEmpty(padreBio) && !padreBio.Equals("0"))
                            {
                                mdfMtxbNumeroPadre.Text = padreBio;
                                DataTable dtPadre = new BDMiembro().obtenerDatos(S, padreBio, L);
                                if (dtPadre.Rows.Count != 0)
                                {
                                    ingMlblVNombrePadre.Text = "&nbsp;&nbsp;" + dtPadre.Rows[0]["FirstNames"].ToString() + " " + dtPadre.Rows[0]["LastNames"].ToString();
                                }
                            }
                            else
                            {
                                mdfMtxbNumeroPadre.Text = "";
                            }

                            ingMdesactivarActivarElementos(false);
                            ingMincluirMiembro();
                            ingMbtnGuardar.Visible = true;
                            ingMbtnReestablecerRel.Visible = true;
                            ingMbtnReestablecer.Visible = true;
                            ingMbtnAceptar.Visible = false;
                            ingMgdvMiembros.Columns[9].Visible = true;
                            ingMgdvMiembros.Columns[10].Visible = true;
                            ingMgdvMiembros.Columns[11].Visible = false;
                            ingMgdvMiembros.Columns[12].Visible = false;
                        }
                        else
                            if (resultadoMadre == 1 && resultadoPadre == 2)
                            {
                                if (L.Equals("es"))
                                {
                                    mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Padre Biológico, no es de género masculino.");
                                }
                                else
                                {
                                    mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Father, is not male.");
                                }
                            }
                            else
                                if (resultadoMadre == 1 && resultadoPadre == 3)
                                {
                                    if (L.Equals("es"))
                                    {
                                        mst.mostrarMsjAdvNtf("El número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                    }
                                    else
                                    {
                                        mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Father, does not belong to any member.");
                                    }
                                }
                                else
                                    if (resultadoMadre == 2 && resultadoPadre == 1)
                                    {
                                        if (L.Equals("es"))
                                        {
                                            mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino.");
                                        }
                                        else
                                        {
                                            mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female.");
                                        }
                                    }
                                    else
                                        if (resultadoMadre == 2 && resultadoPadre == 2)
                                        {
                                            if (L.Equals("es"))
                                            {
                                                mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino. Y el miembro que trata de asignar como Padre Biológico, no es de género masculino.");
                                            }
                                            else
                                            {
                                                mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female. And the member that you try to assign as Biologycal Father, is not male.");
                                            }
                                        }
                                        else
                                            if (resultadoMadre == 2 && resultadoPadre == 3)
                                            {
                                                if (L.Equals("es"))
                                                {
                                                    mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino. Y el número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                                }
                                                else
                                                {
                                                    mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female. And the Id that you entered for a Biologycal Father, does not belong to any member.");
                                                }
                                            }
                                            else
                                                if (resultadoMadre == 3 && resultadoPadre == 1)
                                                {
                                                    if (L.Equals("es"))
                                                    {
                                                        mst.mostrarMsjAdvNtf("El número que ingresó para la Madre Biológica, no pertenece a ningún miembro.");
                                                    }
                                                    else
                                                    {
                                                        mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Mother, does not belong to any member.");
                                                    }
                                                }
                                                else
                                                    if (resultadoMadre == 3 && resultadoPadre == 2)
                                                    {
                                                        if (L.Equals("es"))
                                                        {
                                                            mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Padre Biológico, no es de género masculino. Y el número que ingresó para el Madre Biológica, no pertenece a ningún miembro.");
                                                        }
                                                        else
                                                        {
                                                            mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Father, is not male. And the Id that you entered for a Biologycal Mother, does not belong to any member.");
                                                        }
                                                    }
                                                    else
                                                        if (resultadoMadre == 3 && resultadoPadre == 3)
                                                        {
                                                            if (L.Equals("es"))
                                                            {
                                                                mst.mostrarMsjAdvNtf("El número que ingresó para la Madre Biológica, no pertenece a ningún miembro. Y el número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                                            }
                                                            else
                                                            {
                                                                mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Mother, does not belong to any member. And the Id that you entered for a Biologycal Father, does not belong to any member.");
                                                            }
                                                        }
                    }
                    else
                    {
                        String msj = L.Equals("es") ? "Por favor, asegurese de que el Número de Teléfono tenga 8 dígitos." : "Please, make sure that the Phone Number has 8 digits.";
                        mst.mostrarMsjAdvNtf(msj);
                    }
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("La Fecha de Nacimiento no puede ser después que la fecha actual.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("The Birth Date cannot be before to the current date.");
                    }
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("La Fecha de Nacimiento, no es correcta.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("The Birth Date is not correct.");
                }
            }
        }

        protected void ingMbtnGuardarConAjenas_Click(object sender, EventArgs e)
        {
            if (ingMverificarRelacionesAjenas())
            {
                try
                {
                    ingMguardarRelacionesAjenas();
                    ingMguardarRelaciones();
                    ingMpnlRelacionesAjenas.Visible = false;
                    ingMActivarDesactivarGdvRelaciones(true);
                    ingMbtnGuardarConAjenas.Visible = false;
                    ingMbtnReestablecerConAjenas.Visible = false;
                    ingMbtnGuardar.Visible = true;

                    ingMlimpiarElementos();
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                    ingMbtnGuardar.Visible = false;
                    ingMbtnReestablecerRel.Visible = false;
                    ingMbtnReestablecer.Visible = false;
                    ingMbtnAceptar.Visible = true;
                    mst.seleccionarMiembro(ingMmiembro);
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
                    mst.mostrarMsjAdvNtf("Por favor, asegurese de asignar como Inactivas las relaciones en otras familias.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, make sure assign relations in other families as Inactive.");
                }
            }
        }

        protected void ingMbtnReestablecerConAjenas_Click(object sender, EventArgs e)
        {
            ingMpnlRelacionesAjenas.Visible = false;
            ingMActivarDesactivarGdvRelaciones(true);
            ingMbtnGuardarConAjenas.Visible = false;
            ingMbtnReestablecerConAjenas.Visible = false;
            ingMbtnGuardar.Visible = true;
            ingMbtnReestablecerRel.Visible = true;
            ingMbtnReestablecer.Visible = true;
        }

        protected void ingMbtnReestablecerRel_Click(object sender, EventArgs e)
        {
            ingMincluirMiembro();
            ingMgdvMiembros.Columns[9].Visible = true;
            ingMgdvMiembros.Columns[10].Visible = true;
            ingMgdvMiembros.Columns[11].Visible = false;
            ingMgdvMiembros.Columns[12].Visible = false;
        }

        protected void ingMdesactivarActivarElementos(Boolean activo)
        {
            ingMtxbAñoNacimiento.Enabled = activo;
            ingMtxbApellidos.Enabled = activo;
            ingMtxbCUI.Enabled = activo;
            ingMtxbDiaNacimiento.Enabled = activo;
            ingMtxbNombres.Enabled = activo;
            ingMtxbNombreUsual.Enabled = activo;
            ingMtxbNumeroCelular1.Enabled = activo;
            ingMtxbNumeroCelular2.Enabled = activo;
            ingMtxbNumeroMadre.Enabled = activo;
            ingMtxbNumeroPadre.Enabled = activo;
            ingMddlGenero.Enabled = activo;
            ingMddlMesNacimiento.Enabled = activo;
            ingMddlOtraAfil.Enabled = activo;
            ingMddlPuedeLeer.Enabled = activo;
            ingMddlUltimoGrado.Enabled = activo;
            ingMchkHayCUI.Enabled = activo;
        }
        protected void ingMlimpiarElementos()
        {
            ingMtxbAñoNacimiento.Text = "";
            ingMtxbApellidos.Text = "";
            ingMtxbCUI.Text = "";
            ingMtxbDiaNacimiento.Text = "";
            ingMtxbNombres.Text = "";
            ingMtxbNombreUsual.Text = "";
            ingMtxbNumeroCelular1.Text = "";
            ingMtxbNumeroCelular2.Text = "";
            ingMtxbNumeroMadre.Text = "";
            ingMtxbNumeroPadre.Text = "";
            ingMlblVNombreMadre.Text = "";
            ingMlblVNombrePadre.Text = "";
            ingMddlGenero.SelectedValue = "";
            ingMddlMesNacimiento.SelectedValue = "";
            ingMddlOtraAfil.SelectedValue = "";
            ingMddlPuedeLeer.SelectedValue = "";
            ingMddlUltimoGrado.SelectedValue = "";
            ingMchkHayCUI.Checked = false;
        }
        protected void ingMllenarGdvMiembros(DataTable dtMiembros)
        {
            ingMlblRelaciones.Text = dic.relacionesFamiliares;
            ingMgdvMiembros.Columns[0].Visible = true;
            ingMgdvMiembros.Columns[1].Visible = true;
            ingMgdvMiembros.Columns[2].Visible = true;
            ingMgdvMiembros.Columns[3].Visible = true;
            ingMgdvMiembros.Columns[4].Visible = true;
            ingMgdvMiembros.Columns[5].Visible = true;
            ingMgdvMiembros.Columns[11].Visible = true;
            ingMgdvMiembros.Columns[12].Visible = true;
            ingMgdvMiembros.Columns[6].HeaderText = dic.miembro;
            ingMgdvMiembros.Columns[7].HeaderText = dic.nombre;
            ingMgdvMiembros.Columns[8].HeaderText = dic.edad;
            ingMgdvMiembros.Columns[9].HeaderText = dic.relacion;
            ingMgdvMiembros.Columns[10].HeaderText = dic.inactivoRazon;
            ingMgdvMiembros.Columns[11].HeaderText = dic.relacion;
            ingMgdvMiembros.Columns[12].HeaderText = dic.inactivoRazon;
            ingMgdvMiembros.DataSource = dtMiembros;
            ingMgdvMiembros.DataBind();
            ingMgdvMiembros.Columns[0].Visible = false;
            ingMgdvMiembros.Columns[1].Visible = false;
            ingMgdvMiembros.Columns[2].Visible = false;
            ingMgdvMiembros.Columns[3].Visible = false;
            ingMgdvMiembros.Columns[4].Visible = false;
            ingMgdvMiembros.Columns[5].Visible = false;
            ingMgdvMiembros.Columns[9].Visible = false;
            ingMgdvMiembros.Columns[10].Visible = false;
        }
        protected void ingMincluirMiembro()
        {
            String nombre = ingMnombres + " " + ingMapellidos;
            String edad = bdGEN.obtenerEdad(ingMstrFechaNacimiento, L);
            DataTable dtMiembros = bdMISC.mdfFobtenerMiembros(S, F, L);
            DataRow dtRow = dtMiembros.NewRow();
            dtRow["MemberId"] = "";
            dtRow["Nombre"] = nombre;
            dtRow["Edad"] = edad;
            dtRow["Relacion"] = "";
            dtRow["RazonInactivo"] = "";
            dtRow["Genero"] = ingMgenero;
            dtRow["fechaCreacion"] = DateTime.Now;
            dtRow["fechaActivo"] = DateTime.Now;
            dtRow["yaTieneRelacion"] = "N";
            dtMiembros.Rows.InsertAt(dtRow, 0);
            ingMllenarGdvMiembros(dtMiembros);
        }
        protected void ingMgdvMiembros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlNuevaRelacion = (DropDownList)e.Row.FindControl("ddlRelacionNueva");
                ddlNuevaRelacion.Items.Clear();
                ddlNuevaRelacion.Items.Add(new ListItem("", ""));
                String miembro = e.Row.Cells[0].Text;
                String genero = e.Row.Cells[2].Text;
                String codigo = "";
                String empleado = "";
                ListItem item;
                foreach (DataRow row in bdGEN.obtenerRelaciones(L, genero).Rows)
                {
                    codigo = row["Code"].ToString();
                    empleado = row["Des"].ToString();
                    item = new ListItem(empleado, codigo);
                    ddlNuevaRelacion.Items.Add(item);
                }
                ddlNuevaRelacion.SelectedValue = e.Row.Cells[0].Text;
                DropDownList ddlRazonInactivo = (DropDownList)e.Row.FindControl("ddlRazonInactivo");
                ddlRazonInactivo.Items.Clear();
                ddlRazonInactivo.Items.Add(new ListItem("", ""));
                foreach (DataRow row in bdGEN.obtenerRazonesInactivo(L).Rows)
                {
                    codigo = row["Code"].ToString();
                    empleado = row["Des"].ToString();
                    item = new ListItem(empleado, codigo);
                    ddlRazonInactivo.Items.Add(item);
                }
                ddlRazonInactivo.SelectedValue = e.Row.Cells[1].Text;
                if ((String.IsNullOrEmpty(miembro)) || (miembro.Equals("&nbsp;")))
                {
                    ddlRazonInactivo.Enabled = false;
                }
            }
        }
        static Boolean ingMhayJefesEnRelacionesAjenas = false;
        static String ingMmiembrosAExcluir = "";
        protected void ingMgdvRelacionesAjenas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ingMhayJefesEnRelacionesAjenas = false;
                DropDownList ddlRazonInactivo = (DropDownList)e.Row.FindControl("ddlRazonInactivo");
                ddlRazonInactivo.Items.Clear();
                String tipo = e.Row.Cells[5].Text;
                ddlRazonInactivo.Items.Add(new ListItem("", ""));
                String codigo = "";
                String empleado = "";
                ListItem item;

                foreach (DataRow row in bdGEN.obtenerRazonesInactivo(L).Rows)
                {
                    codigo = row["Code"].ToString();
                    empleado = row["Des"].ToString();
                    item = new ListItem(empleado, codigo);
                    ddlRazonInactivo.Items.Add(item);
                }
                if (tipo.Equals("JEFE") || tipo.Equals("JEFM"))
                {
                    ddlRazonInactivo.Enabled = false;
                    ingMhayJefesEnRelacionesAjenas = true;
                }
            }
        }
        protected void ingMguardarRelaciones()
        {
            ingMgdvMiembros.Columns[0].Visible = true;
            ingMgdvMiembros.Columns[1].Visible = true;
            ingMgdvMiembros.Columns[2].Visible = true;
            ingMgdvMiembros.Columns[3].Visible = true;
            ingMgdvMiembros.Columns[4].Visible = true;
            foreach (GridViewRow row in ingMgdvMiembros.Rows)
            {
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                String relacion = ddlRelacion.SelectedValue;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                String relacionSLCT = row.Cells[0].Text;
                String razonInactivoSLCT = row.Cells[1].Text;
                if (razonInactivoSLCT.Equals("&nbsp;"))
                {
                    razonInactivoSLCT = "";
                }
                String fechaCreacionSLCT = row.Cells[3].Text;
                String fechaActivoSLCT = row.Cells[4].Text;
                String yaTiene = row.Cells[5].Text;
                String miembro = row.Cells[6].Text;
                if (yaTiene.Equals("S"))
                {
                    if (!relacionSLCT.Equals(relacion) || (!razonInactivoSLCT.Equals(razonInactivo)))
                    {
                        bdMISC.mdfFActualizarRelacion(S, miembro, F, relacion, U, razonInactivo);
                    }
                }
                else
                {
                    bdMISC.mdfFActualizarRelacion(S, ingMmiembro, F, relacion, U, razonInactivo);
                }
            }
            ingMllenarGdvMiembros(bdMISC.mdfFobtenerMiembros(S, F, L));
            mdfMllenarGdvMiembros();
            mdfFllenarPnlGridViewMiembros();
            ingMgdvMiembros.Columns[0].Visible = false;
            ingMgdvMiembros.Columns[1].Visible = false;
            ingMgdvMiembros.Columns[2].Visible = false;
            ingMgdvMiembros.Columns[3].Visible = false;
            ingMgdvMiembros.Columns[4].Visible = false;
        }
        protected void ingMguardarRelacionesAjenas()
        {
            ingMgdvRelacionesAjenas.Columns[5].Visible = true;
            foreach (GridViewRow row in ingMgdvRelacionesAjenas.Rows)
            {
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                String miembro = row.Cells[0].Text;
                String familia = row.Cells[1].Text;
                String relacion = row.Cells[5].Text;
                bdMISC.mdfFActualizarRelacion(S, miembro, familia, relacion, U, razonInactivo);
                bdMISC.mdfFCambiarFamiliaMiembro(S, miembro, F, U);
            }

        }
        protected Boolean ingMverificarRelacionesAjenas()
        {
            Boolean noHayVacias = true;
            foreach (GridViewRow row in ingMgdvRelacionesAjenas.Rows)
            {
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                if (String.IsNullOrEmpty(razonInactivo))
                {
                    noHayVacias = false;
                }
            }
            return noHayVacias;
        }
        static int ingMhayMiembrosConOtrasRelaciones = 0;
        //String ingMMiembrosJefesOtrasRelaciones = "";
        protected int ingMverificarRelaciones()
        {
            int numJefes = 0;
            Boolean hayVacios = false;
            ingMhayMiembrosConOtrasRelaciones = 0;
            ingMmiembrosAExcluir = "";
            //ingMMiembrosJefesOtrasRelaciones = "";
            foreach (GridViewRow row in ingMgdvMiembros.Rows)
            {
                String razonInactivoSLCT = row.Cells[1].Text;
                if (razonInactivoSLCT.Equals("&nbsp;"))
                {
                    razonInactivoSLCT = "";
                }
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                String relacion = ddlRelacion.SelectedValue;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                if (razonInactivo.Equals("") && !razonInactivoSLCT.Equals(""))
                {

                    String miembro = row.Cells[6].Text;
                    int numRelacionesAjenas = bdMISC.mdfFobtenerCantidadRelacionesActivasOtrasFamiliasPorMiembro2(S, miembro, F);
                    if (numRelacionesAjenas > 0)
                    {
                        ingMhayMiembrosConOtrasRelaciones = 1;
                        ingMmiembrosAExcluir = ingMmiembrosAExcluir + miembro + ",";
                    }
                }
                if ((relacion.Equals("JEFE") || relacion.Equals("JEFM")) && (String.IsNullOrEmpty(razonInactivo)))
                {
                    numJefes++;
                }
                else if (String.IsNullOrEmpty(relacion))
                {
                    hayVacios = true;
                }
            }
            if (numJefes == 0)
            {
                return 1;
            }
            else if (numJefes > 1)
            {
                return 2;
            }
            if (hayVacios)
            {
                return 3;
            }
            return 0;
        }

        //MODIFICAR MIEMBRO
        static String mdfMmiembro;
        protected void mdfMCargarPagina()
        {
            mdfMllenarGdvMiembros();
            mdfMllenarPnlActualizar();
        }
        protected void mdfMllenarGdvMiembros()
        {
            mdfMgdvMiembros.Columns[0].HeaderText = dic.miembro;
            mdfMgdvMiembros.Columns[1].HeaderText = dic.nombre;
            mdfMgdvMiembros.Columns[2].HeaderText = dic.relacion;
            mdfMgdvMiembros.Columns[3].HeaderText = dic.inactivoRazon;
            mdfMgdvMiembros.Columns[4].HeaderText = dic.accion;
            mdfMgdvMiembros.DataSource = bdMISC.mdfMobtenerMiembros(S, F, L);
            mdfMgdvMiembros.DataBind();
        }
        protected void mdfMllenarPnlActualizar()
        {
            mdfMRevtxbDiaNacimiento.ErrorMessage = dic.msjCampoNecesario;
            mdfMRevddlMesNacimiento.ErrorMessage = dic.msjCampoNecesario;
            mdfMRevtxbAñoNacimiento.ErrorMessage = dic.msjCampoNecesario;
            mdfMRevTxbApellidos_ValidatorCalloutExtender.PopupPosition = ValidatorCalloutPosition.Left;
            mdfMRevTxbApellidos.ErrorMessage = dic.msjCampoNecesario;
            mdfMRevTxbNombres.ErrorMessage = dic.msjCampoNecesario;
            mdfMlblMiembros.Text = "&nbsp;" + dic.miembros;
            mdfMlblAñoUltimoGrado.Text = "&nbsp;" + dic.añoUltimoGrado + ":";
            mdfMlblApellidos.Text = "*" + dic.apellidos + ":";
            mdfMlblCUI.Text = "&nbsp;" + dic.DPI + ":";
            mdfMlblEstadoUltimoGrado.Text = "&nbsp;" + dic.estadoUltimoGrado + ":";
            mdfMlblFallecimiento.Text = "&nbsp;" + dic.fechaFallecimiento + ":";
            mdfMlblNombres.Text = "*" + dic.nombres + ":";
            mdfMlblNombreUsual.Text = "&nbsp;" + dic.nombreUsual + ":";
            mdfMlblOtraAfil.Text = "&nbsp;" + dic.otraAfil + ":";
            mdfMlblVivoOMuerto.Text = "&nbsp;" + dic.vivoOmuerto + ":";
            mdfMlblUltimoGrado.Text = "&nbsp;" + dic.ultimoGrado + ":";
            mdfMlblPuedeLeer.Text = "&nbsp;" + dic.lee + ":";
            mdfMlblNumeroCelular.Text = "&nbsp;" + dic.telefono + ":";
            mdfMlblNacimiento.Text = "*" + dic.fechaNacimiento + ":";
            mdfMlblNombreMadre.Text = "&nbsp;" + dic.nombreMadre + ":";
            mdfMlblNombrePadre.Text = "&nbsp;" + dic.nombrePadre + ":";
            mdfMlblNumeroMadre.Text = "&nbsp;" + dic.numeroMadre + ":";
            mdfMlblNumeroPadre.Text = "&nbsp;" + dic.numeroPadre + ":";
            mdfMbtnGuardar.Text = dic.actualizar;
            mdfMbtnRegresar.Text = dic.regresar;
            llenarAlfabetismo(mdfMddlPuedeLeer);
            llenarEstadoUltimoGrado(mdfMddlEstadoUltimoGrado);
            llenarGrados(mdfMddlUltimoGrado);
            llenarMeses(mdfMddlMesNacimiento);
            llenarMeses(mdfMddlMesFallecimiento);
            llenarOtrasAfiliaciones(mdfMddlOtraAfil);
            mdfMtxbNombres.MaxLength = 40;
            mdfMtxbApellidos.MaxLength = 40;
            mdfMtxbNombreUsual.MaxLength = 40;
            mdfMtxbCUI.MaxLength = 20;
        }

        protected void mdfMbtnGuardar_Click(object sender, EventArgs e)
        {
            String nombres = mdfMtxbNombres.Text;
            String apellidos = mdfMtxbApellidos.Text;
            String nombreUsual = mdfMtxbNombreUsual.Text;
            String diaNacimiento = mdfMtxbDiaNacimiento.Text;
            String mesNacimiento = mdfMddlMesNacimiento.SelectedValue;
            String añoNacimiento = mdfMtxbAñoNacimiento.Text;
            String strFechaNacimiento = "";
            Boolean fechaNacimientoCorrecta = true;
            DateTime fechaFallecimiento = DateTime.Now;
            DateTime fechaNacimiento = DateTime.Now;
            if (!String.IsNullOrEmpty(diaNacimiento) && !String.IsNullOrEmpty(mesNacimiento) && !String.IsNullOrEmpty(añoNacimiento))
            {
                if (validarFechas(añoNacimiento, mesNacimiento, diaNacimiento))
                {
                    fechaNacimiento = Convert.ToDateTime(añoNacimiento + "-" + mesNacimiento + "-" + diaNacimiento);
                    strFechaNacimiento = fechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    fechaNacimientoCorrecta = false;
                }
            }
            String diaFallecimiento = mdfMtxbDiaFallecimiento.Text;
            String mesFallecimiento = mdfMddlMesFallecimiento.SelectedValue;
            String añoFallecimiento = mdfMtxbAñoFallecimiento.Text;
            String strFechaFallecimiento = "";
            Boolean fechaFallecimientoCorrecta = true;
            Boolean hayFechaFallecimiento = false;
            if ((!String.IsNullOrEmpty(diaFallecimiento) && !String.IsNullOrEmpty(mesFallecimiento) && !String.IsNullOrEmpty(añoFallecimiento)) || (String.IsNullOrEmpty(diaFallecimiento) && String.IsNullOrEmpty(mesFallecimiento) && String.IsNullOrEmpty(añoFallecimiento)))
            {
                if (!String.IsNullOrEmpty(diaFallecimiento) && !String.IsNullOrEmpty(mesFallecimiento) && !String.IsNullOrEmpty(añoFallecimiento))
                {
                    if (validarFechas(añoFallecimiento, mesFallecimiento, diaFallecimiento))
                    {
                        fechaFallecimiento = Convert.ToDateTime(añoFallecimiento + "-" + mesFallecimiento + "-" + diaFallecimiento);
                        strFechaFallecimiento = fechaFallecimiento.ToString("yyyy-MM-dd HH:mm:ss");
                        hayFechaFallecimiento = true;
                    }
                    else
                    {
                        fechaFallecimientoCorrecta = false;
                    }
                }
                if (fechaFallecimientoCorrecta && fechaNacimientoCorrecta)
                {
                    if ((hayFechaFallecimiento && (fechaFallecimiento > fechaNacimiento)) || (!hayFechaFallecimiento))
                    {
                        if (fechaNacimiento <= DateTime.Now)
                        {
                            if ((hayFechaFallecimiento && (fechaFallecimiento <= DateTime.Now)) || (!hayFechaFallecimiento))
                            {
                                String CUI = mdfMtxbCUI.Text;
                                Boolean tenemosCUI = mdfMchkHayCUI.Checked;
                                String ultimoGrado = mdfMddlUltimoGrado.SelectedValue; ;
                                String añoUltimoGrado = mdfMtxbAñoUltimoGrado.Text;
                                String estadoUltimoGrado = mdfMddlEstadoUltimoGrado.SelectedValue;
                                String puedeLeer = mdfMddlPuedeLeer.SelectedValue;
                                String otraAfiliacion = mdfMddlOtraAfil.SelectedValue;
                                String tel1 = mdfMtxbNumeroCelular1.Text;
                                String tel2 = mdfMtxbNumeroCelular2.Text;
                                String numeroCelular = "";
                                Boolean numeroEsCorrecto = false;
                                if (!String.IsNullOrEmpty(tel1) && !String.IsNullOrEmpty(tel2))
                                {
                                    if (tel1.Length == 4 && tel2.Length == 4)
                                    {
                                        numeroCelular = tel1 + "-" + tel2;
                                        numeroEsCorrecto = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(tel1) && String.IsNullOrEmpty(tel2))
                                {
                                    numeroEsCorrecto = true;
                                }

                                if (numeroEsCorrecto)
                                {
                                    int resultadoMadre = 1;
                                    String madreBiologica = mdfMtxbNumeroMadre.Text;
                                    if (!String.IsNullOrEmpty(madreBiologica))
                                    {
                                        DataTable dtMadre = new BDMiembro().obtenerDatos(S, madreBiologica, L);
                                        if (dtMadre.Rows.Count != 0)
                                        {
                                            String generoMadre = dtMadre.Rows[0]["Gender"].ToString();
                                            if (generoMadre.Equals("Female") || generoMadre.Equals("Femenino"))
                                            {
                                                if (mdfMmiembro.Equals(madreBiologica))
                                                {
                                                    resultadoMadre = 4;
                                                }
                                                else
                                                {
                                                    resultadoMadre = 1;
                                                }
                                            }
                                            else
                                            {
                                                resultadoMadre = 2;
                                            }
                                        }
                                        else
                                        {
                                            resultadoMadre = 3;
                                        }
                                    }

                                    int resultadoPadre = 1;
                                    String padreBiologico = mdfMtxbNumeroPadre.Text;
                                    if (!String.IsNullOrEmpty(padreBiologico))
                                    {
                                        DataTable dtPadre = new BDMiembro().obtenerDatos(S, padreBiologico, L);
                                        if (dtPadre.Rows.Count != 0)
                                        {
                                            String generoPadre = dtPadre.Rows[0]["Gender"].ToString();
                                            if (generoPadre.Equals("Male") || generoPadre.Equals("Masculino"))
                                            {
                                                if (mdfMmiembro.Equals(padreBiologico))
                                                {
                                                    resultadoPadre = 4;
                                                }
                                                else
                                                {
                                                    resultadoPadre = 1;
                                                }
                                            }
                                            else
                                            {
                                                resultadoPadre = 2;
                                            }
                                        }
                                        else
                                        {
                                            resultadoPadre = 3;
                                        }
                                    }

                                    if (resultadoMadre == 1 && resultadoPadre == 1)
                                    {
                                        try
                                        {
                                            bdMISC.mdfMactualizarMiembro(S, mdfMmiembro, U, nombres, apellidos, nombreUsual, strFechaNacimiento, strFechaFallecimiento, madreBiologica, padreBiologico, otraAfiliacion, CUI, tenemosCUI, numeroCelular, puedeLeer, ultimoGrado, estadoUltimoGrado, añoUltimoGrado);
                                            mdfMllenarGdvMiembros();
                                            ingMllenarGdvMiembros(bdMISC.mdfFobtenerMiembros(S, F, L));
                                            mdfFllenarPnlGridViewMiembros();
                                            mdfMpnlFActualizarMiembro.Visible = false;
                                            mdfMpnlMiembros.Visible = true;
                                            mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                                        }
                                        catch (Exception ex)
                                        {
                                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                                        }
                                    }
                                    else
                                        if (resultadoMadre == 1 && resultadoPadre == 2)
                                        {
                                            if (L.Equals("es"))
                                            {
                                                mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Padre Biológico, no es de género masculino.");
                                            }
                                            else
                                            {
                                                mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Father, is not male.");
                                            }
                                        }
                                        else
                                            if (resultadoMadre == 1 && resultadoPadre == 3)
                                            {
                                                if (L.Equals("es"))
                                                {
                                                    mst.mostrarMsjAdvNtf("El número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                                }
                                                else
                                                {
                                                    mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Father, does not belong to any member.");
                                                }
                                            }
                                            else
                                                if (resultadoMadre == 2 && resultadoPadre == 1)
                                                {
                                                    if (L.Equals("es"))
                                                    {
                                                        mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino.");
                                                    }
                                                    else
                                                    {
                                                        mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female.");
                                                    }
                                                }
                                                else
                                                    if (resultadoMadre == 2 && resultadoPadre == 2)
                                                    {
                                                        if (L.Equals("es"))
                                                        {
                                                            mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino. Y el miembro que trata de asignar como Padre Biológico, no es de género masculino.");
                                                        }
                                                        else
                                                        {
                                                            mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female. And the member that you try to assign as Biologycal Father, is not male.");
                                                        }
                                                    }
                                                    else
                                                        if (resultadoMadre == 2 && resultadoPadre == 3)
                                                        {
                                                            if (L.Equals("es"))
                                                            {
                                                                mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Madre Biológica, no es de género femenino. Y el número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                                            }
                                                            else
                                                            {
                                                                mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Mother, is not female. And the Id that you entered for a Biologycal Father, does not belong to any member.");
                                                            }
                                                        }
                                                        else
                                                            if (resultadoMadre == 3 && resultadoPadre == 1)
                                                            {
                                                                if (L.Equals("es"))
                                                                {
                                                                    mst.mostrarMsjAdvNtf("El número que ingresó para la Madre Biológica, no pertenece a ningún miembro.");
                                                                }
                                                                else
                                                                {
                                                                    mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Mother, does not belong to any member.");
                                                                }
                                                            }
                                                            else
                                                                if (resultadoMadre == 3 && resultadoPadre == 2)
                                                                {
                                                                    if (L.Equals("es"))
                                                                    {
                                                                        mst.mostrarMsjAdvNtf("El miembro que trata de asignar como Padre Biológico, no es de género masculino. Y el número que ingresó para el Madre Biológica, no pertenece a ningún miembro.");
                                                                    }
                                                                    else
                                                                    {
                                                                        mst.mostrarMsjAdvNtf("The member that you try to assign as Biologycal Father, is not male. And the Id that you entered for a Biologycal Mother, does not belong to any member.");
                                                                    }
                                                                }
                                                                else
                                                                    if (resultadoMadre == 3 && resultadoPadre == 3)
                                                                    {
                                                                        if (L.Equals("es"))
                                                                        {
                                                                            mst.mostrarMsjAdvNtf("El número que ingresó para la Madre Biológica, no pertenece a ningún miembro. Y el número que ingresó para el Padre Biológico, no pertenece a ningún miembro.");
                                                                        }
                                                                        else
                                                                        {
                                                                            mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Mother, does not belong to any member. And the Id that you entered for a Biologycal Father, does not belong to any member.");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (resultadoMadre == 4)
                                                                        {
                                                                            if (L.Equals("es"))
                                                                            {
                                                                                mst.mostrarMsjAdvNtf("El número que ingresó para la Madre Biológica, es el del Miembro Seleccionado.");
                                                                            }
                                                                            else
                                                                            {
                                                                                mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Mother, is that of the Selected Member.");
                                                                            }
                                                                        }
                                                                        else
                                                                            if (resultadoPadre == 4)
                                                                            {
                                                                                if (L.Equals("es"))
                                                                                {
                                                                                    mst.mostrarMsjAdvNtf("El número que ingresó para la Padre Biológico, es el del Miembro Seleccionado.");
                                                                                }
                                                                                else
                                                                                {
                                                                                    mst.mostrarMsjAdvNtf("The Id that you entered for a Biologycal Father, is that of the Selected Member.");
                                                                                }
                                                                            }
                                                                    }
                                }
                                else
                                {
                                    String msj = L.Equals("es") ? "Por favor, asegurese de que el Número de Teléfono tenga 8 dígitos." : "Please, make sure that the Phone Number has 8 digits.";
                                    mst.mostrarMsjAdvNtf(msj);
                                }
                            }
                            else
                            {
                                if (L.Equals("es"))
                                {
                                    mst.mostrarMsjAdvNtf("La Fecha de Fallecimiento no puede ser después que la fecha actual.");
                                }
                                else
                                {
                                    mst.mostrarMsjAdvNtf("The Death Date cannot be before to the current date.");
                                }
                            }
                        }
                        else
                        {
                            if (L.Equals("es"))
                            {
                                mst.mostrarMsjAdvNtf("La Fecha de Nacimiento no puede ser después que la fecha actual.");
                            }
                            else
                            {
                                mst.mostrarMsjAdvNtf("The Birth Date cannot be before to the current date.");
                            }
                        }
                    }
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("La Fecha de Fallecimiento no puede ser antes que la Fecha de Nacimiento.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("The Death Date cannot be before to the Birth Date.");
                        }
                    }
                }
                else
                {
                    if (!fechaFallecimientoCorrecta && fechaNacimientoCorrecta)
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("La Fecha de Fallecimiento, no es correcta.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("The Death Date is not correct.");
                        }
                    }
                    else
                        if (fechaFallecimientoCorrecta && !fechaNacimientoCorrecta)
                        {
                            if (L.Equals("es"))
                            {
                                mst.mostrarMsjAdvNtf("La Fecha de Nacimiento, no es correcta.");
                            }
                            else
                            {
                                mst.mostrarMsjAdvNtf("The Birth Date is not correct.");
                            }
                        }
                        else
                            if (!fechaFallecimientoCorrecta && !fechaNacimientoCorrecta)
                            {
                                if (L.Equals("es"))
                                {
                                    mst.mostrarMsjAdvNtf("La Fecha de Nacimiento y Fallecimiento, no son correctas.");
                                }
                                else
                                {
                                    mst.mostrarMsjAdvNtf("The Birth and Death Date are not correct.");
                                }
                            }
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, complete o deje vacía la Fecha de Fallecimiento.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please complete or leave the Death Date empty.");
                }
            }
        }
        protected void mdfMbtnRegresar_Click(object sender, EventArgs e)
        {
            mdfMpnlFActualizarMiembro.Visible = false;
            mdfMpnlMiembros.Visible = true;
        }
        protected void mdfMgdvMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                try
                {
                    mdfMmiembro = mdfMgdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                    DataTable dtMiembro = bdMISC.mdfMobtenerMiembroEsp(S, mdfMmiembro, L);
                    DataRow rowMiembro = dtMiembro.Rows[0];
                    mdfMtxbNombres.Text = rowMiembro["Nombres"].ToString();
                    mdfMtxbApellidos.Text = rowMiembro["Apellidos"].ToString();
                    mdfMtxbNombreUsual.Text = rowMiembro["nombreUsual"].ToString();
                    mdfMtxbDiaNacimiento.Text = rowMiembro["diaNacimiento"].ToString();
                    mdfMddlMesNacimiento.SelectedValue = rowMiembro["mesNacimiento"].ToString();
                    mdfMtxbAñoNacimiento.Text = rowMiembro["añoNacimiento"].ToString();
                    mdfMlblVVivoOMuerto.Text = "&nbsp;&nbsp;" + rowMiembro["vivoMuerto"].ToString();
                    mdfMtxbDiaFallecimiento.Text = rowMiembro["diaFallecimiento"].ToString();
                    mdfMddlMesFallecimiento.Text = rowMiembro["mesFallecimiento"].ToString();
                    mdfMtxbAñoFallecimiento.Text = rowMiembro["añoFallecimiento"].ToString();
                    mdfMtxbCUI.Text = rowMiembro["CUI"].ToString();
                    mdfMchkHayCUI.Checked = (Boolean)rowMiembro["tenemosCUI"];
                    mdfMddlUltimoGrado.SelectedValue = rowMiembro["ultimoGrado"].ToString();
                    mdfMtxbAñoUltimoGrado.Text = rowMiembro["añoUltimoGrado"].ToString();
                    mdfMddlEstadoUltimoGrado.SelectedValue = rowMiembro["estadoUltimoGrado"].ToString();
                    mdfMddlPuedeLeer.SelectedValue = rowMiembro["puedeLeer"].ToString();
                    mdfMddlOtraAfil.SelectedValue = rowMiembro["otraAfiliacion"].ToString();
                    mdfMlblVNombreMadre.Text = "";
                    mdfMlblVNombrePadre.Text = "";
                    String numeroCelular = rowMiembro["telefono"].ToString();
                    if (!String.IsNullOrEmpty(numeroCelular))
                    {
                        String tel1 = "";
                        String tel2 = "";
                        tel1 = numeroCelular.Substring(0, numeroCelular.IndexOf("-"));
                        tel2 = numeroCelular.Substring(numeroCelular.IndexOf("-") + 1, numeroCelular.Length - numeroCelular.IndexOf("-") - 1);
                        mdfMtxbNumeroCelular1.Text = tel1;
                        mdfMtxbNumeroCelular2.Text = tel2;
                    }
                    else
                    {
                        mdfMtxbNumeroCelular1.Text = "";
                        mdfMtxbNumeroCelular2.Text = "";
                    }
                    String madreBio = rowMiembro["madreBiologica"].ToString();
                    if (!String.IsNullOrEmpty(madreBio) && !madreBio.Equals("0"))
                    {
                        mdfMtxbNumeroMadre.Text = madreBio;
                        DataTable dtMadre = new BDMiembro().obtenerDatos(S, madreBio, L);
                        if (dtMadre.Rows.Count != 0)
                        {
                            mdfMlblVNombreMadre.Text = "&nbsp;&nbsp;" + dtMadre.Rows[0]["FirstNames"].ToString() + " " + dtMadre.Rows[0]["LastNames"].ToString();
                        }
                    }
                    else
                    {
                        mdfMtxbNumeroMadre.Text = "";
                    }

                    String padreBio = rowMiembro["padreBiologico"].ToString();
                    if (!String.IsNullOrEmpty(padreBio) && !padreBio.Equals("0"))
                    {
                        mdfMtxbNumeroPadre.Text = padreBio;
                        DataTable dtPadre = new BDMiembro().obtenerDatos(S, padreBio, L);
                        if (dtPadre.Rows.Count != 0)
                        {
                            mdfMlblVNombrePadre.Text = "&nbsp;&nbsp;" + dtPadre.Rows[0]["FirstNames"].ToString() + " " + dtPadre.Rows[0]["LastNames"].ToString();
                        }
                    }
                    else
                    {
                        mdfMtxbNumeroPadre.Text = "";
                    }
                    mdfMpnlFActualizarMiembro.Visible = true;
                    mdfMpnlMiembros.Visible = false;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }

        //MODIFICAR FAMILIA
        DateTime mdfFfechaCreacion;
        DateTime mdfFfechaUltimaAct;
        String mdfFarea;
        String mdfFdireccion;
        String mdfFetnia;
        String mdfFpueblo;
        String mdfFtelefono;
        String mdfFmunicipio;
        String mdfFtiempoEnLugar;
        protected void mdfFCargarPagina()
        {
            mdfFllenarPnlActualizar();
        }
        protected void mdfFllenarNombres()
        {
            mdfFRevddlArea.ErrorMessage = dic.msjCampoNecesario;
            mdfFRevtxbDireccion.ErrorMessage = dic.msjCampoNecesario;
            mdfFlblArea.Text = "*" + dic.area + ":";
            mdfFlblDireccion.Text = "*" + dic.direccion + ":";
            mdfFlblEtnia.Text = "&nbsp;" + dic.etnia + ":";
            mdfFlblMunicipio.Text = "&nbsp;" + dic.municipio + ":";
            mdfFlblNumeroCelular.Text = "&nbsp;" + dic.telefono + ":";
            mdfFlblPueblo.Text = "&nbsp;" + dic.pueblo + ":";
            mdfFlblRelaciones.Text = "&nbsp;" + dic.relaciones;
            mdfFlblTiempo.Text = "&nbsp;" + dic.tiempoDeVivir + ":";
            mdfFlblUltimaAct.Text = "&nbsp;" + dic.ultimaActualizacion + ":";
            mdfFtxbDireccion.MaxLength = 80;
            mdfFtxbTiempo.MaxLength = 50;
            mdfFbtnGuardar.Text = dic.actualizar;
        }
        protected void mdfFllenarPnlActualizar()
        {
            mdfFllenarNombres();
            llenarAreas(mdfFddlArea);
            llenarEtnias(mdfFddlEtnia);
            llenarMunicipios(mdfFddlMunicipio);
            DataTable dtFamilia = bdMISC.mdfFobtenerInfoBasica(S, F);
            DataRow rowFamilia = dtFamilia.Rows[0];
            mdfFfechaCreacion = Convert.ToDateTime(rowFamilia["CreationDateTime"].ToString());
            String strFechaUltimaAct = rowFamilia["LastUpdateDate"].ToString();
            if (!String.IsNullOrEmpty(strFechaUltimaAct))
            {
                mdfFfechaUltimaAct = Convert.ToDateTime(strFechaUltimaAct);
                mdfFtxbFechaUltimaAct.Text = mdfFfechaUltimaAct.ToString("dd/MM/yyyy");
            }
            mdfFdireccion = rowFamilia["Address"].ToString();
            mdfFtelefono = rowFamilia["TelephoneNumber"].ToString();
            mdfFarea = rowFamilia["Area"].ToString();
            mdfFetnia = rowFamilia["Ethnicity"].ToString();
            mdfFmunicipio = rowFamilia["Municipality"].ToString();
            mdfFtiempoEnLugar = rowFamilia["TimeOnPlace"].ToString();
            mdfFpueblo = rowFamilia["Pueblo"].ToString();
            //estadoAfiliacion = rowFamilia["AffiliationStatus"].ToString();
            //fechaEstadoAfil = Convert.ToDateTime(rowFamilia["AffiliationStatusDate"].ToString());
            //nivelAfiliacion = rowFamilia["AffiliationLevel"].ToString();
            //clasificacion = rowFamilia["Classification"].ToString();
            //fechaClasif = Convert.ToDateTime(rowFamilia["ClassificationDate"].ToString());
            //proximaClasif = rowFamilia["NextClassification"].ToString();
            //numeroFaro = rowFamilia["RFaroNumber"].ToString();
            mdfFtxbDireccion.Text = mdfFdireccion;
            if (!String.IsNullOrEmpty(mdfFtelefono))
            {
                String tel1 = "";
                String tel2 = "";
                tel1 = mdfFtelefono.Substring(0, mdfFtelefono.IndexOf("-"));
                tel2 = mdfFtelefono.Substring(mdfFtelefono.IndexOf("-") + 1, mdfFtelefono.Length - mdfFtelefono.IndexOf("-") - 1);
                mdfFtxbNumeroCelular1.Text = tel1;
                mdfFtxbNumeroCelular2.Text = tel2;
            }
            mdfFddlArea.SelectedValue = mdfFarea;
            mdfFddlEtnia.SelectedValue = mdfFetnia;
            mdfFddlMunicipio.SelectedValue = mdfFmunicipio;
            mdfFtxbTiempo.Text = mdfFtiempoEnLugar;
            mdfFlblVPueblo.Text = "&nbsp;&nbsp;" + mdfFpueblo;
        }
        protected void mdfFbtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String strFechaUltimaAct = mdfFtxbFechaUltimaAct.Text;

                if (!String.IsNullOrEmpty(strFechaUltimaAct))
                {
                    mdfFfechaUltimaAct = Convert.ToDateTime(strFechaUltimaAct);
                    strFechaUltimaAct = mdfFfechaUltimaAct.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                mdfFarea = mdfFddlArea.SelectedValue;
                mdfFdireccion = mdfFtxbDireccion.Text;
                mdfFetnia = mdfFddlEtnia.SelectedValue;
                mdfFpueblo = mdfFlblVPueblo.Text;
                String tel1 = mdfFtxbNumeroCelular1.Text;
                String tel2 = mdfFtxbNumeroCelular2.Text;
                mdfFtelefono = "";
                Boolean numeroEsCorrecto = false;
                if (!String.IsNullOrEmpty(tel1) && !String.IsNullOrEmpty(tel2))
                {
                    if (tel1.Length == 4 && tel2.Length == 4)
                    {
                        mdfFtelefono = tel1 + "-" + tel2;
                        numeroEsCorrecto = true;
                    }
                }

                if (String.IsNullOrEmpty(tel1) && String.IsNullOrEmpty(tel2))
                {
                    numeroEsCorrecto = true;
                }

                if (numeroEsCorrecto)
                {
                    mdfFmunicipio = mdfFddlMunicipio.Text;
                    mdfFtiempoEnLugar = mdfFtxbTiempo.Text;
                    bdMISC.mdfFactualizarFamilia(S, F, mdfFfechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U, mdfFarea, mdfFdireccion, mdfFtelefono, mdfFetnia, strFechaUltimaAct, mdfFmunicipio, mdfFtiempoEnLugar);
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                }
                else
                {
                    String msj = L.Equals("es") ? "Por favor, asegurese de que el Número de Teléfono tenga 8 dígitos." : "Please, make sure that the Phone Number has 8 digits.";
                    mst.mostrarMsjAdvNtf(msj);
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }


        //REASIGNAR FAMILIA A MIEMBRO
        protected void rsgCargarPagina()
        {
            rsgllenarNombres();
            mdfFllenarPnlGridViewMiembros();
            rsgAutomataEstados(0);
        }
        protected void rsgllenarNombres()
        {
            llenarRazonesInactivo(rsgDdlRazonInactivo);
            rsgLblMiembroNuevo.Text = "*" + dic.nuevoMiembro + ":";
            rsgLblNombre.Text = dic.nombre;
            rsgLblFamilia.Text = dic.familia;
            rsgLblMiembro.Text = dic.miembro;
            rsgLblRelacion.Text = dic.relacion;
            rsgLblNuevaRelacion.Text = dic.TSnuevaRelacion;
            rsgLblRazonInactivo.Text = dic.inactivoRazon;
            mdfFbtnGuardarRelaciones.Text = dic.actualizar;
            rsgRevddlRazonInactivo.ErrorMessage = dic.msjCampoNecesario;
            rsgRevTxbMiembroNuevo.ErrorMessage = dic.msjParametroNecesario;
            rsgBtnNuevo.Text = dic.TSagregarMiembroOtraFam;
            rsgBtnBuscar.Text = dic.buscar;
            rsgBtnCancelar.Text = dic.cancelar;
            rsgBtnCancelarExportar.Text = dic.cancelar + " " + dic.TSagregarMiembroOtraFam;
            rsgBtnReestablecer.Text = dic.TSreestablecerRelaciones;
            mdfFbtnReestablecerConAjenas.Text = dic.TSreasignarRelaciones;
            mdfFbtnGuardarConAjenas.Text = dic.guardar;
            mdfFlblRelacionesajenas.Text = dic.TSinactivarRelacionOtraFamilia;
            rsgLblInactivar.Text = dic.TSasignarRelacionMiembroOtraFamilia;
        }
        protected void mdfFllenarPnlGridViewMiembros()
        {
            mdfFlblRelaciones.Text = dic.relacionesFamiliares;
            mdfFgdvMiembros.Columns[0].Visible = true;
            mdfFgdvMiembros.Columns[1].Visible = true;
            mdfFgdvMiembros.Columns[2].Visible = true;
            mdfFgdvMiembros.Columns[3].Visible = true;
            mdfFgdvMiembros.Columns[4].Visible = true;
            mdfFgdvMiembros.Columns[5].HeaderText = dic.miembro;
            mdfFgdvMiembros.Columns[6].HeaderText = dic.nombre;
            mdfFgdvMiembros.Columns[7].HeaderText = dic.edad;
            mdfFgdvMiembros.Columns[8].HeaderText = dic.relacion;
            mdfFgdvMiembros.Columns[9].HeaderText = dic.inactivoRazon;
            mdfFgdvMiembros.DataSource = bdMISC.mdfFobtenerMiembros(S, F, L);
            mdfFgdvMiembros.DataBind();
            mdfFgdvMiembros.Columns[0].Visible = false;
            mdfFgdvMiembros.Columns[1].Visible = false;
            mdfFgdvMiembros.Columns[2].Visible = false;
            mdfFgdvMiembros.Columns[3].Visible = false;
            mdfFgdvMiembros.Columns[4].Visible = false;
        }

        protected void mdfFbtnGuardarRelaciones_Click(object sender, EventArgs e)
        {
            int numVerificar = mdfFverificarRelaciones();
            if (numVerificar == 0)
            {
                if (mdfFhayMiembrosConOtrasRelaciones == 0)
                {
                    try
                    {
                        mdfFguardarRelaciones();
                        mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                        rsgAutomataEstados(0);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                    }
                }
                else if (mdfFhayMiembrosConOtrasRelaciones == 1)
                {
                    mdfFprepararLlenarRelacionesAjenas();
                }
            }
            else if (numVerificar == 1)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("No especificó ningún Jefe de Casa activo. Debe haber un Jefe de Casa activo.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("You did not specify any active Head of House. There must be an active Head of House.");
                }
            }
            else if (numVerificar == 2)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Especificó más de un Jefe de Casa activo. Debe haber un solo Jefe de Casa activo.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("You specified more than one active Head of House. There must be only one active Head of House.");
                }
            }
            else if (numVerificar == 3)
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, asegurese de que todos los miembros tengan asignada una relación.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, make sure all members are assigned a relation.");
                }
            }
        }

        protected void mdfFgdvMiembros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlNuevaRelacion = (DropDownList)e.Row.FindControl("ddlRelacionNueva");
                ddlNuevaRelacion.Items.Clear();
                ddlNuevaRelacion.Items.Add(new ListItem("", ""));
                String genero = e.Row.Cells[2].Text;
                String codigo = "";
                String empleado = "";
                ListItem item;
                foreach (DataRow row in bdGEN.obtenerRelaciones(L, genero).Rows)
                {
                    codigo = row["Code"].ToString();
                    empleado = row["Des"].ToString();
                    item = new ListItem(empleado, codigo);
                    ddlNuevaRelacion.Items.Add(item);
                }
                ddlNuevaRelacion.SelectedValue = e.Row.Cells[0].Text;
                DropDownList ddlRazonInactivo = (DropDownList)e.Row.FindControl("ddlRazonInactivo");
                ddlRazonInactivo.Items.Clear();
                ddlRazonInactivo.Items.Add(new ListItem("", ""));
                foreach (DataRow row in bdGEN.obtenerRazonesInactivo(L).Rows)
                {
                    codigo = row["Code"].ToString();
                    empleado = row["Des"].ToString();
                    item = new ListItem(empleado, codigo);
                    ddlRazonInactivo.Items.Add(item);
                }
                ddlRazonInactivo.SelectedValue = e.Row.Cells[1].Text;
            }
        }

        protected void mdfFddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            String area = mdfFddlArea.SelectedValue;
            mdfFlblVPueblo.Text = "&nbsp;&nbsp;" + bdMISC.mdfFobtenerPuebloDeArea(area);
        }


        protected void mdfFprepararLlenarRelacionesAjenas()
        {
            mdfFllenarRelacionesAjenas();
            if (rsgEstado == 2)
            {
                rsgAutomataEstados(3);
            }
            else
                if (rsgEstado == 0)
                {
                    rsgAutomataEstados(4);
                }
        }
        static int mdfFhayMiembrosConOtrasRelaciones = 0;
        static Boolean mdfFhayJefesEnRelacionesAjenas = false;
        static String mdfFmiembrosAExcluir;
        protected void mdfFbtnGuardarConAjenas_Click(object sender, EventArgs e)
        {
            if (mdfFverificarRelacionesAjenas())
            {
                try
                {
                    mdfFguardarRelacionesAjenas();
                    mdfFguardarRelaciones();
                    rsgAutomataEstados(0);
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                    mst.seleccionarMiembro(ingMmiembro);
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
                    mst.mostrarMsjAdvNtf("Por favor, asegurese de asignar como Inactivas, las relaciones en otras familias.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, make sure assign relations in other families as Inactive.");
                }
            }
        }

        protected Boolean mdfFverificarRelacionesAjenas()
        {
            Boolean noHayVacias = true;
            foreach (GridViewRow row in mdfFgdvRelacionesAjenas.Rows)
            {
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                if (String.IsNullOrEmpty(razonInactivo))
                {
                    noHayVacias = false;
                }
            }
            return noHayVacias;
        }

        protected void mdfFguardarRelacionesAjenas()
        {
            mdfFgdvRelacionesAjenas.Columns[5].Visible = true;
            foreach (GridViewRow row in mdfFgdvRelacionesAjenas.Rows)
            {
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                String miembro = row.Cells[0].Text;
                String familia = row.Cells[1].Text;
                String relacion = row.Cells[5].Text;
                bdMISC.mdfFActualizarRelacion(S, miembro, familia, relacion, U, razonInactivo);
                bdMISC.mdfFCambiarFamiliaMiembro(S, miembro, F, U);
            }
        }

        protected void mdfFbtnReestablecerConAjenas_Click(object sender, EventArgs e)
        {
            switch (rsgEstado)
            {
                case 3:
                    rsgAutomataEstados(2);
                    break;
                case 4:
                    rsgAutomataEstados(0);
                    break;
            }
        }
        protected void mdfFActivarDesactivarGdvRelaciones(Boolean activo)
        {
            foreach (GridViewRow row in mdfFgdvMiembros.Rows)
            {
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                ddlRelacion.Enabled = activo;
                ddlRazonInactivo.Enabled = activo;
            }
        }
        protected void mdfFllenarRelacionesAjenas()
        {
            mdfFpnlRelacionesAjenas.Visible = true;
            mdfFgdvRelacionesAjenas.Columns[0].HeaderText = dic.miembro;
            mdfFgdvRelacionesAjenas.Columns[1].HeaderText = dic.familia;
            mdfFgdvRelacionesAjenas.Columns[2].HeaderText = dic.nombre;
            mdfFgdvRelacionesAjenas.Columns[3].HeaderText = dic.relacion;
            mdfFgdvRelacionesAjenas.Columns[4].HeaderText = dic.inactivoRazon;
            mdfFgdvRelacionesAjenas.DataSource = bdMISC.mdfFobtenerRelacionesActivasOtrasFamilias(S, F, L, mdfFmiembrosAExcluir);
            mdfFgdvRelacionesAjenas.DataBind();
            mdfFgdvRelacionesAjenas.Columns[5].Visible = false;
            if (mdfFhayJefesEnRelacionesAjenas)
            {
                mdfFgdvRelacionesAjenas.Columns[4].Visible = false;
                mdfFpnlMiembros.Visible = false;
                mdfFbtnGuardarConAjenas.Visible = false;
            }
        }
        protected void mdfFguardarRelaciones()
        {
            mdfFgdvMiembros.Columns[0].Visible = true;
            mdfFgdvMiembros.Columns[1].Visible = true;
            mdfFgdvMiembros.Columns[2].Visible = true;
            mdfFgdvMiembros.Columns[3].Visible = true;
            mdfFgdvMiembros.Columns[4].Visible = true;
            foreach (GridViewRow row in mdfFgdvMiembros.Rows)
            {
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                String relacion = ddlRelacion.SelectedValue;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                String relacionSLCT = row.Cells[0].Text;
                String razonInactivoSLCT = row.Cells[1].Text;
                if (razonInactivoSLCT.Equals("&nbsp;"))
                {
                    razonInactivoSLCT = "";
                }
                String fechaCreacionSLCT = row.Cells[3].Text;
                String fechaActivoSLCT = row.Cells[4].Text;
                String miembro = row.Cells[5].Text;
                if (!relacionSLCT.Equals(relacion) || (!razonInactivoSLCT.Equals(razonInactivo)))
                {
                    bdMISC.mdfFActualizarRelacion(S, miembro, F, relacion, U, razonInactivo);
                    rsgAutomataEstados(0);
                }
            }
            if (rsgSeExportaMiembro == 1)
            {
                DateTime ahora = DateTime.Now;
                DateTime fechaCreacionNuevaExp = ahora;
                DateTime fechaActivoNuevaExp = ahora;
                DateTime fechaInactivoExp = ahora;
                String miembroExp = rsgLblVMiembro.Text;
                String relacionNuevaExp = rsgDdlNuevaRelacion.SelectedValue;
                String razonInactivoExp = rsgDdlRazonInactivo.SelectedValue;
                bdMISC.mdfFinsertarRelacionInactivar(S, F, miembroExp, relacionNuevaExp, fechaActivoNuevaExp.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacionNuevaExp.ToString("yyyy-MM-dd HH:mm:ss"), U, razonInactivoExp, fechaInactivoExp.ToString("yyyy-MM-dd HH:mm:ss"));
                bdMISC.mdfFCambiarFamiliaMiembro(S, miembroExp, F, U);
                rsgSeExportaMiembro = 0;
            }
            else
                if (rsgSeExportaMiembro == 2)
                {
                    DateTime ahora = DateTime.Now;
                    DateTime fechaCreacionNuevaExp = ahora;
                    DateTime fechaActivoNuevaExp = ahora;
                    String miembroExp = rsgLblVMiembro.Text;
                    String relacionNuevaExp = rsgDdlNuevaRelacion.SelectedValue;
                    bdMISC.mdfFActualizarRelacion(S, miembroExp, F, relacionNuevaExp, U, "");
                    bdMISC.mdfFCambiarFamiliaMiembro(S, miembroExp, F, U);
                    rsgSeExportaMiembro = 0;
                }
            mdfFllenarPnlGridViewMiembros();
            ingMllenarGdvMiembros(bdMISC.mdfFobtenerMiembros(S, F, L));
            mdfMllenarGdvMiembros();
            mdfFgdvMiembros.Columns[0].Visible = false;
            mdfFgdvMiembros.Columns[1].Visible = false;
            mdfFgdvMiembros.Columns[2].Visible = false;
            mdfFgdvMiembros.Columns[3].Visible = false;
            mdfFgdvMiembros.Columns[4].Visible = false;
        }

        protected int mdfFverificarRelaciones()
        {
            int numJefes = 0;
            Boolean hayVacios = false;
            mdfFhayMiembrosConOtrasRelaciones = 0;
            mdfFmiembrosAExcluir = "";
            foreach (GridViewRow row in mdfFgdvMiembros.Rows)
            {
                DropDownList ddlRelacion = row.FindControl("ddlRelacionNueva") as DropDownList;
                String relacion = ddlRelacion.SelectedValue;
                DropDownList ddlRazonInactivo = row.FindControl("ddlRazonInactivo") as DropDownList;
                String razonInactivo = ddlRazonInactivo.SelectedValue;
                String razonInactivoSLCT = row.Cells[1].Text;
                if (razonInactivoSLCT.Equals("&nbsp;"))
                {
                    razonInactivoSLCT = "";
                }
                if (razonInactivo.Equals("") && !razonInactivoSLCT.Equals(""))
                {
                    String miembro = row.Cells[5].Text;
                    mdfFbtnGuardarRelaciones.Visible = true;
                    int numRelacionesAjenas = bdMISC.mdfFobtenerCantidadRelacionesActivasOtrasFamiliasPorMiembro2(S, miembro, F);
                    if (numRelacionesAjenas > 0)
                    {
                        mdfFhayMiembrosConOtrasRelaciones = 1;
                        mdfFmiembrosAExcluir = mdfFmiembrosAExcluir + miembro + ",";
                    }
                }
                if ((relacion.Equals("JEFE") || relacion.Equals("JEFM")) && (String.IsNullOrEmpty(razonInactivo)))
                {
                    numJefes++;
                }
                else if (String.IsNullOrEmpty(relacion))
                {
                    hayVacios = true;
                }
            }
            if (rsgSeExportaMiembro != 0)
            {
                String relacionNuevo = rsgDdlNuevaRelacion.SelectedValue;
                String razonInactivoNuevo = rsgDdlRazonInactivo.SelectedValue;
                if ((relacionNuevo.Equals("JEFE") || relacionNuevo.Equals("JEFM")))
                {
                    numJefes++;
                }
                if (String.IsNullOrEmpty(relacionNuevo))
                {
                    hayVacios = true;
                }
            }

            if (numJefes == 0)
            {
                return 1;
            }
            else if (numJefes > 1)
            {
                return 2;
            }
            if (hayVacios)
            {
                return 3;
            }
            return 0;
        }
        protected void rsgBtnAsignar_Click(object sender, EventArgs e)
        {
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaActivo = fechaCreacion;
            DateTime fechaInactivoSLCT = fechaCreacion;
            bdMISC.mdfFinsertarRelacionInactivar(S, F, rsgTxbMiembroNuevo.Text, "AHIO", fechaActivo.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), U, "ABAN", fechaInactivoSLCT.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        protected void rsgBtnBuscar_Click(object sender, EventArgs e)
        {
            rsgDdlRazonInactivo.Enabled = true;
            String miembro = rsgTxbMiembroNuevo.Text;
            DataTable dtRelacionesOtrasFam = bdMISC.mdfFobtenerCantidadRelacionesActivasOtrasFamiliasPorMiembro(S, miembro, F, L);
            if (dtRelacionesOtrasFam.Rows.Count != 0)
            {
                String relacion = dtRelacionesOtrasFam.Rows[0]["Relacion"].ToString();
                if (!relacion.Equals("JEFE") && !relacion.Equals("JEFA"))
                {
                    rsgPnlExportarMiembro.Visible = true;
                    rsgLblVNombre.Text = dtRelacionesOtrasFam.Rows[0]["Nombre"].ToString();
                    rsgLblVMiembro.Text = dtRelacionesOtrasFam.Rows[0]["Miembro"].ToString();
                    rsgLblVFamilia.Text = dtRelacionesOtrasFam.Rows[0]["Familia"].ToString();
                    rsgLblVRelacion.Text = dtRelacionesOtrasFam.Rows[0]["RelacionDes"].ToString();
                    String genero = dtRelacionesOtrasFam.Rows[0]["Genero"].ToString();
                    String razonInactivo = dtRelacionesOtrasFam.Rows[0]["RazonInactivo"].ToString();
                    rsgDdlRazonInactivo.SelectedValue = razonInactivo;
                    if (!String.IsNullOrEmpty(razonInactivo))
                    {
                        rsgDdlRazonInactivo.Enabled = false;
                        rsgSeExportaMiembro = 2;
                    }
                    else
                    {
                        rsgSeExportaMiembro = 1;
                    }
                    llenarRelacionesFamiliares(rsgDdlNuevaRelacion, genero);
                    rsgBtnCancelarExportar.Visible = true;
                    rsgAutomataEstados(2);
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Este miembro, es Jefe de Casa, de la familia " + S + dtRelacionesOtrasFam.Rows[0]["Familia"].ToString() + ".");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("This member, is Head of House, from the family " + S + dtRelacionesOtrasFam.Rows[0]["Familia"].ToString() + ".");
                    }
                }
            }
            else
            {
                mst.mostrarMsjAdvNtf(dic.msjNoEncontroMiembro);
            }
        }
        protected void rsgBtnNuevo_Click(object sender, EventArgs e)
        {
            rsgAutomataEstados(1);
        }

        static int rsgSeExportaMiembro = 0;
        protected void rsgBtnCancelarExportar_Click(object sender, EventArgs e)
        {
            rsgAutomataEstados(0);
            rsgSeExportaMiembro = 0;
        }
        protected void rsgBtnCancelar_Click(object sender, EventArgs e)
        {
            rsgAutomataEstados(0);
        }
        static int rsgEstado = 0;

        protected void rsgBtnReestablecer_Click(object sender, EventArgs e)
        {
            mdfFllenarPnlGridViewMiembros();
        }
        public void rsgAutomataEstados(int estado)
        {
            rsgEstado = estado;
            switch (rsgEstado)
            {
                case 0:
                    mdfFActivarDesactivarGdvRelaciones(true);
                    mdfFbtnGuardarRelaciones.Visible = true;
                    rsgBtnNuevo.Visible = true;
                    rsgBtnReestablecer.Visible = true;
                    rsgLblMiembroNuevo.Visible = false;
                    rsgTxbMiembroNuevo.Visible = false;
                    rsgBtnBuscar.Visible = false;
                    rsgBtnCancelar.Visible = false;
                    rsgPnlExportarMiembro.Visible = false;
                    rsgBtnCancelarExportar.Visible = false;
                    rsgDdlNuevaRelacion.Enabled = false;
                    mdfFbtnReestablecerConAjenas.Visible = false;
                    mdfFbtnGuardarConAjenas.Visible = false;
                    mdfFpnlRelacionesAjenas.Visible = false;
                    break;
                case 1:
                    mdfFActivarDesactivarGdvRelaciones(true);
                    mdfFbtnGuardarRelaciones.Visible = false;
                    rsgBtnNuevo.Visible = false;
                    rsgBtnReestablecer.Visible = false;
                    rsgLblMiembroNuevo.Visible = true;
                    rsgTxbMiembroNuevo.Visible = true;
                    rsgBtnBuscar.Visible = true;
                    rsgBtnCancelar.Visible = true;
                    rsgPnlExportarMiembro.Visible = false;
                    rsgBtnCancelarExportar.Visible = false;
                    rsgDdlNuevaRelacion.Enabled = false;
                    mdfFbtnReestablecerConAjenas.Visible = false;
                    mdfFbtnGuardarConAjenas.Visible = false;
                    mdfFpnlRelacionesAjenas.Visible = false;
                    break;
                case 2:
                    mdfFActivarDesactivarGdvRelaciones(true);
                    mdfFbtnGuardarRelaciones.Visible = true;
                    rsgBtnNuevo.Visible = false;
                    rsgBtnReestablecer.Visible = true;
                    rsgLblMiembroNuevo.Visible = false;
                    rsgTxbMiembroNuevo.Visible = false;
                    rsgBtnBuscar.Visible = false;
                    rsgBtnCancelar.Visible = false;
                    rsgPnlExportarMiembro.Visible = true;
                    rsgBtnCancelarExportar.Visible = true;
                    rsgDdlNuevaRelacion.Enabled = true;
                    mdfFbtnReestablecerConAjenas.Visible = false;
                    mdfFbtnGuardarConAjenas.Visible = false;
                    mdfFpnlRelacionesAjenas.Visible = false;
                    break;
                case 3:
                    mdfFActivarDesactivarGdvRelaciones(false);
                    mdfFbtnGuardarRelaciones.Visible = false;
                    rsgBtnNuevo.Visible = false;
                    rsgBtnReestablecer.Visible = false;
                    rsgLblMiembroNuevo.Visible = false;
                    rsgTxbMiembroNuevo.Visible = false;
                    rsgBtnBuscar.Visible = false;
                    rsgBtnCancelar.Visible = false;
                    rsgPnlExportarMiembro.Visible = true;
                    rsgBtnCancelarExportar.Visible = false;
                    rsgDdlNuevaRelacion.Enabled = false;
                    rsgDdlRazonInactivo.Enabled = false;
                    mdfFbtnReestablecerConAjenas.Visible = true;
                    mdfFbtnGuardarConAjenas.Visible = true;
                    mdfFpnlRelacionesAjenas.Visible = true;
                    break;
                case 4:
                    mdfFActivarDesactivarGdvRelaciones(false);
                    mdfFbtnGuardarRelaciones.Visible = false;
                    rsgBtnNuevo.Visible = false;
                    rsgBtnReestablecer.Visible = false;
                    rsgLblMiembroNuevo.Visible = false;
                    rsgTxbMiembroNuevo.Visible = false;
                    rsgBtnBuscar.Visible = false;
                    rsgBtnCancelar.Visible = false;
                    rsgPnlExportarMiembro.Visible = false;
                    rsgBtnCancelarExportar.Visible = false;
                    rsgDdlNuevaRelacion.Enabled = false;
                    mdfFbtnReestablecerConAjenas.Visible = true;
                    mdfFbtnGuardarConAjenas.Visible = true;
                    mdfFpnlRelacionesAjenas.Visible = true;
                    break;
            }
        }

        //TELÉFONOS
        protected void tlfCargarPagina(){
            tlfLlenarNombres();
            tlfLlenarGdvTelefonos();
        }
        protected void tlfLlenarNombres()
        {
            
        }
        protected void tlfLlenarGdvTelefonos()
        {
            DataTable dtTelefonos = new DataTable();
            dtTelefonos.Columns.Add("CreationDateTime");
            dtTelefonos.Columns.Add("Pertenece");
            dtTelefonos.Columns.Add("Numero");
            dtTelefonos.Columns.Add("Compañia");
            dtTelefonos.Columns.Add("EsWhatsapp");
            dtTelefonos.Columns.Add("Estado");
            dtTelefonos.Columns.Add("UserId");
            dtTelefonos.Rows.Add("2020-23-01", "Josué Alberto Ramirez", "3456-8289", "Claro", "Sí", "Activo", "BrandonB");
            dtTelefonos.Rows.Add("2020-23-01", "Juan Antonio Ramirez", "4456-8289", "Rigo", "Sí", "Activo", "BrandonB");
            dtTelefonos.Rows.Add("2020-23-01", "Familiar", "3456-8289", "", "Sí", "Activo", "BrandonB");
            tlfGdvTelefonos.DataSource = dtTelefonos;
            tlfGdvTelefonos.DataBind();
        }
        protected void tlfBtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void tlfBtnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void tlfBtnInsertar_Click(object sender, EventArgs e)
        {

        }

        protected void tlfGdvTelefonos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void tlfGdvTelefonos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        //GENERAL
        protected void llenarAlfabetismo(DropDownList ddlArea)
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerAlfabetismo(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlArea.Items.Add(item);
            }
        }
        protected void llenarAreas(DropDownList ddlArea)
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerAreas(S, L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlArea.Items.Add(item);
            }
        }
        protected void llenarEtnias(DropDownList ddlEtnia)
        {
            ddlEtnia.Items.Clear();
            ddlEtnia.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerEtnias(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlEtnia.Items.Add(item);
            }
        }
        protected void llenarEstadoUltimoGrado(DropDownList ddlEstadoUltimoGrado)
        {
            ddlEstadoUltimoGrado.Items.Clear();
            ddlEstadoUltimoGrado.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerEstadosEducativos(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlEstadoUltimoGrado.Items.Add(item);
            }
        }
        protected void llenarGenero(DropDownList ddlGenero)
        {
            ddlGenero.Items.Clear();
            ddlGenero.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerGeneros(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlGenero.Items.Add(item);
            }
        }
        protected void llenarGrados(DropDownList ddlGrado)
        {
            ddlGrado.Items.Clear();
            ddlGrado.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerGrados(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlGrado.Items.Add(item);
            }
        }

        protected void llenarMeses(DropDownList ddlMes)
        {
            ddlMes.Items.Clear();
            ddlMes.Items.Add(new ListItem("", ""));
            DataTable tblMeses = new DataTable();
            tblMeses.Columns.Add("Code", typeof(String));
            tblMeses.Columns.Add("Month", typeof(String));
            if (L.Equals("es"))
            {
                tblMeses.Rows.Add("1", "Ene");
                tblMeses.Rows.Add("2", "Feb");
                tblMeses.Rows.Add("3", "Mar");
                tblMeses.Rows.Add("4", "Abr");
                tblMeses.Rows.Add("5", "May");
                tblMeses.Rows.Add("6", "Jun");
                tblMeses.Rows.Add("7", "Jul");
                tblMeses.Rows.Add("8", "Ago");
                tblMeses.Rows.Add("9", "Sep");
                tblMeses.Rows.Add("10", "Oct");
                tblMeses.Rows.Add("11", "Nov");
                tblMeses.Rows.Add("12", "Dic");
            }
            else
            {
                tblMeses.Rows.Add("1", "Jan");
                tblMeses.Rows.Add("2", "Feb");
                tblMeses.Rows.Add("3", "Mar");
                tblMeses.Rows.Add("4", "Apr");
                tblMeses.Rows.Add("5", "May");
                tblMeses.Rows.Add("6", "Jun");
                tblMeses.Rows.Add("7", "Jul");
                tblMeses.Rows.Add("8", "Agu");
                tblMeses.Rows.Add("9", "Sep");
                tblMeses.Rows.Add("10", "Oct");
                tblMeses.Rows.Add("11", "Nov");
                tblMeses.Rows.Add("12", "Dec");
            }
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblMeses.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Month"].ToString();
                item = new ListItem(Des, Code);
                ddlMes.Items.Add(item);
            }
        }
        protected void llenarMunicipios(DropDownList ddlMunicipio)
        {
            ddlMunicipio.Items.Clear();
            ddlMunicipio.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerMunicipios(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlMunicipio.Items.Add(item);

            }
        }
        protected void llenarRazonesInactivo(DropDownList ddlRazonInactivo)
        {
            ddlRazonInactivo.Items.Clear();
            ddlRazonInactivo.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerRazonesInactivo(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlRazonInactivo.Items.Add(item);
            }
        }
        protected void llenarRelacionesFamiliares(DropDownList ddlRelaciones, String genero)
        {
            ddlRelaciones.Items.Clear();
            ddlRelaciones.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerRelaciones(L, genero);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlRelaciones.Items.Add(item);
            }
        }

        protected void llenarOtrasAfiliaciones(DropDownList ddlMunicipio)
        {
            ddlMunicipio.Items.Clear();
            ddlMunicipio.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerOtrasAfiliaciones(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlMunicipio.Items.Add(item);

            }
        }
        protected Boolean validarFechas(String strYear, String strMonth, String strDay)
        {
            int year = Int32.Parse(strYear);
            int month = Int32.Parse(strMonth);
            int day = Int32.Parse(strDay);
            Boolean check = false;
            if (year <= DateTime.MaxValue.Year && year >= DateTime.MinValue.Year)
            {
                if (month >= 1 && month <= 12)
                {
                    if (DateTime.DaysInMonth(year, month) >= day && day >= 1)
                        check = true;
                }
            }
            return check;
        }

       

    }
}