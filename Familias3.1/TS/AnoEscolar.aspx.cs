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
    public partial class AñoEscolar : System.Web.UI.Page
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
        protected static DateTime fechaCreacionSLCT;
        protected static String añoSLCT;
        protected static String año;
        protected static String miembroSLCT;
        protected static int edadMiembro;
        protected static Boolean vista;
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
                mst = (mast)Master;
                año = (DateTime.Now.Year) + "";
                try
                {
                    llenarGdvMiembros();
                    llenarElementos();
                    edadMiembro = 20;
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + " " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    if (gdvMiembros.Rows.Count == 0)
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjStc("Esta familia no tiene miembros aptos para registrar Años Escolares.");
                        }
                        else
                        {
                            mst.mostrarMsjStc("This family has no members eligible to register School Years.");
                        }
                    }
                    else
                    {
                        if (vista)
                        {
                            cargarConSeguridad();
                        }
                    }
                }
                catch
                {

                }
            }
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarAñoEscolar);
        }

        protected void cargarConSeguridad()
        {

        }

        protected void cargarMiembroConSeguridad()
        {
            pnlIngresarAñoEscolar.Visible = false;
            gdvAñosEscolar.Columns[9].Visible = false;
        }

        protected void cargarAñoEscolarConSeguridad()
        {
            
        }

        protected void llenarElementos()
        {
            llenarFormIngresar();
            llenarFormActualizar();
            lblNoTiene.Text = dic.noTiene;
        }

        protected void llenarGdvMiembros()
        {
            lblMiembros.Text = dic.miembros;
            gdvMiembros.Columns[0].HeaderText = dic.miembro;
            gdvMiembros.Columns[1].HeaderText = dic.nombre;
            gdvMiembros.Columns[2].HeaderText = dic.edad;
            gdvMiembros.Columns[3].HeaderText = dic.accion;
            gdvMiembros.Columns[4].Visible = true;
            gdvMiembros.DataSource = bdTS.añoObtenerMiembros(S, F, L);
            gdvMiembros.DataBind();
            gdvMiembros.Columns[4].Visible = false;
        }

        protected void llenarFormIngresar()
        {
            txbSeccion.MaxLength = 1;
            txbAño.MaxLength = 4;
            txbNotas.MaxLength = 100;
            lblTS.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblDirec.Text = dic.direccion + ":";
            lblClasif.Text = dic.clasificacion + ":";
            //revDdlCentroEduc.ErrorMessage = dic.msjCampoNecesario;
            revDdlEstadoEduc.ErrorMessage = dic.msjCampoNecesario;
            revDdlProximoGrado.ErrorMessage = dic.msjCampoNecesario;
            revTxbAño.ErrorMessage = dic.msjCampoNecesario;
            lblMiembro.Text = "&nbsp;" + dic.miembro + ":";
            lblAñosEscolar.Text = dic.TShistorialAñoEscolar;
            lblAño.Text = "*" + dic.año + ":";
            lblProximoGrado.Text = "*" + dic.proximoGrado + ":";
            lblSeccion.Text = "&nbsp;" + dic.seccion + ":";
            //lblCentroEduc.Text = "*" + dic.centroEducativo + ":";
            lblEstadoEduc.Text = "*" + dic.estadoEducativo + ":";
            lblNotas.Text = "&nbsp;" + dic.nota + ":";
            lblCarreraEduc.Text = "&nbsp;" + dic.carrera + ":";
            btnIngresar.Text = dic.TSingresarAñoEscolar;
            btnRegresar.Text = dic.regresar;
            //llenarMiembros();
            llenarGrados(ddlProximoGrado);
            llenarEstadosEduc(ddlEstadoEduc);
            llenarCarrerasEduc(ddlCarreraEduc);
            txbAño.Text = año + "";
        }

        //protected void llenarMiembros()
        //{
        //    ddlMiembro.Items.Add(new ListItem("", ""));
        //    DataTable dtMiembros;
        //    dtMiembros = BDF.obtenerActivos(S, F, L);
        //    String numero = "";
        //    String nombre = "";
        //    ListItem item;
        //    foreach (DataRow row in dtMiembros.Rows)
        //    {
        //        numero = row["MemberId"].ToString();
        //        nombre = row["Nombre"].ToString();
        //        item = new ListItem(nombre + " (" + numero + ")", numero);
        //        ddlMiembro.Items.Add(item);
        //    }
        //}

        protected void llenarGrados(DropDownList ddlProximoGrado)
        {
            ddlProximoGrado.Items.Clear();
            ddlProximoGrado.Items.Add(new ListItem("", ""));
            DataTable tblGrados;
            tblGrados = bdGEN.obtenerGradosApad(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblGrados.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlProximoGrado.Items.Add(item);
            }
        }

        protected void llenarCarrerasEduc(DropDownList ddlCarreraEduc)
        {
            ddlCarreraEduc.Items.Clear();
            ddlCarreraEduc.Items.Add(new ListItem("", ""));
            DataTable tblCarrera;
            tblCarrera = bdGEN.obtenerCarrerasEduc(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblCarrera.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlCarreraEduc.Items.Add(item);
            }
        }

        protected void llenarCentrosEduc(DropDownList ddlCentroEduc, String grado)
        {
            ddlCentroEduc.Items.Clear();
            ddlCentroEduc.Items.Add(new ListItem("", ""));
            DataTable tblCentrosEduc;
            tblCentrosEduc = bdGEN.obtenerCentrosEduc(S, grado);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblCentrosEduc.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Name"].ToString();
                item = new ListItem(Des, Code);
                ddlCentroEduc.Items.Add(item);
            }
        }

        protected void llenarEstadosEduc(DropDownList ddlEstadoEduc)
        {
            ddlEstadoEduc.Items.Clear();
            ddlEstadoEduc.Items.Add(new ListItem("", ""));
            DataTable tblEstado;
            tblEstado = bdGEN.obtenerEstadosEducativos(L);
            String Code = "";
            String Des = "";
            ListItem item;
            foreach (DataRow row in tblEstado.Rows)
            {
                Code = row["Code"].ToString();
                Des = row["Des"].ToString();
                item = new ListItem(Des, Code);
                ddlEstadoEduc.Items.Add(item);
            }
        }

        protected void llenarFormActualizar()
        {
            txbActSeccion.MaxLength = 1;
            txbActNotas.MaxLength = 100;
            //revDdlActCentroEduc.ErrorMessage = dic.msjCampoNecesario;
            revDdlActProximoGrado.ErrorMessage = dic.msjCampoNecesario;
            revDdlActEstadoEduc.ErrorMessage = dic.msjCampoNecesario;
            lblActMiembro.Text = "&nbsp;" + dic.miembro + ":";
            lblActAño.Text = "&nbsp;" + dic.año + ":";
            lblActNotas.Text = "&nbsp;" + dic.nota + ":";
            lblActProximoGrado.Text = "*" + dic.proximoGrado + ":";
            lblActEstadoEduc.Text = "*" + dic.estadoEducativo + ":";
            lblActSeccion.Text = "&nbsp;" + dic.seccion + ":";
            lblActCarreraEduc.Text = "&nbsp;" + dic.carrera + ":";
            //lblActCentroEduc.Text = "*" + dic.centroEducativo + ":";
            btnActualizar.Text = dic.actualizar;
            btnNuevoAñoEscolar.Text = dic.TSnuevoAñoEscolar;
            llenarGrados(ddlActProximoGrado);
            llenarEstadosEduc(ddlActEstadoEduc);
            llenarCarrerasEduc(ddlActCarreraEduc);
        }

        protected void llenarGdvAñosEscolar()
        {
            DataTable dtAñosEscolar = bdTS.añoObtenerAñosEscolares(S, miembroSLCT, L);
            gdvAñosEscolar.Visible = false;
            lblNoTiene.Visible = false;
            if (dtAñosEscolar.Rows.Count > 0)
            {
                gdvAñosEscolar.Visible = true;
                gdvAñosEscolar.Columns[0].Visible = true;
                gdvAñosEscolar.Columns[1].Visible = true;
                gdvAñosEscolar.Columns[2].Visible = true;
                gdvAñosEscolar.Columns[3].HeaderText = dic.año;
                gdvAñosEscolar.Columns[4].HeaderText = dic.grado;
                gdvAñosEscolar.Columns[5].HeaderText = dic.carrera;
                gdvAñosEscolar.Columns[6].HeaderText = dic.estado;
                gdvAñosEscolar.Columns[7].HeaderText = dic.seccion;
                gdvAñosEscolar.Columns[8].HeaderText = dic.nota;
                gdvAñosEscolar.Columns[9].HeaderText = dic.acciones;
                gdvAñosEscolar.DataSource = dtAñosEscolar;
                gdvAñosEscolar.DataBind();
                gdvAñosEscolar.Columns[0].Visible = false;
                gdvAñosEscolar.Columns[1].Visible = false;
                gdvAñosEscolar.Columns[2].Visible = false;
            }
            else
            {
                lblNoTiene.Visible = true;
            }
        }

        protected void btnNuevoAñoEscolar_Click(object sender, EventArgs e)
        {
            limpiarElementosPnlInsertar();
            pnlActualizarAñoEscolar.Visible = false;
            pnlIngresarAñoEscolar.Visible = true;
        }

        protected void gdvAñosEscolar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gdvAñosEscolar.Columns[0].Visible = true;
            gdvAñosEscolar.Columns[1].Visible = true;
            gdvAñosEscolar.Columns[2].Visible = true;
            int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
            fechaCreacionSLCT = Convert.ToDateTime(gdvAñosEscolar.Rows[numFilaEsp].Cells[1].Text);
            miembroSLCT = gdvAñosEscolar.Rows[numFilaEsp].Cells[0].Text;
            añoSLCT = gdvAñosEscolar.Rows[numFilaEsp].Cells[2].Text;
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    DataRow dtRow = bdTS.añoObtenerAñoEscolarEsp(S, miembroSLCT, añoSLCT + "", fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss")).Rows[0];
                    String nombre = dtRow["Nombre"].ToString();
                    String año = dtRow["Año"].ToString();
                    String grado = dtRow["Grado"].ToString();
                    String estado = dtRow["Estado"].ToString();
                    String carrera = dtRow["Carrera"].ToString();
                    String seccion = dtRow["Seccion"].ToString();
                    //String centroEducProximoGrado = dtRow["GradeSchool"].ToString();
                    String notas = dtRow["Notas"].ToString();
                    //lblVActMiembro.Text = gdvAñosEscolar.Rows[numFilaEsp].Cells[3].Text;
                    //lblVActAño.Text = gdvAñosEscolar.Rows[numFilaEsp].Cells[4].Text;
                    //if (notas.Equals("&nbsp;"))
                    //{
                    //    notas = "";
                    //}
                    //llenarCentrosEduc(ddlActCentroEduc, proximoGrado);
                    //ddlActCentroEduc.SelectedValue = centroEducProximoGrado;
                    ddlActCarreraEduc.SelectedValue = carrera;
                    ddlActEstadoEduc.SelectedValue = estado;
                    ddlActProximoGrado.SelectedValue = grado;
                    txbActNotas.Text = notas;
                    txbActSeccion.Text = seccion;
                    lblVActMiembro.Text = "&nbsp;&nbsp;" + nombre;
                    lblVActAño.Text = "&nbsp;&nbsp;" + año;
                    pnlIngresarAñoEscolar.Visible = false;
                    pnlActualizarAñoEscolar.Visible = true;
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
            gdvAñosEscolar.Columns[0].Visible = false;
            gdvAñosEscolar.Columns[1].Visible = false;
            gdvAñosEscolar.Columns[2].Visible = false;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            DateTime fechaCreacion = DateTime.Now;
            String añoEscolar = txbAño.Text;
            //String miembro = ddlMiembro.SelectedValue;
            String miembro = miembroSLCT;
            String notas = txbNotas.Text;
            String proximoGrado = ddlProximoGrado.SelectedValue;
            //String centroEducProximoGrado = ddlCentroEduc.SelectedValue;
            String estadoEduc = ddlEstadoEduc.SelectedValue;
            String seccion = txbSeccion.Text;
            String carrera = ddlCarreraEduc.SelectedValue;
            int intAñoEscolar = Int32.Parse(añoEscolar);
            int añoActual = DateTime.Now.Year;
            int limiteInferior = añoActual - (edadMiembro - 1);
            int añoSiguiente = añoActual + 1;
            if ((intAñoEscolar <= añoSiguiente) && (intAñoEscolar >= limiteInferior))
            {
                try
                {
                    if (bdTS.añoVerificarIngreso(S, miembro, añoEscolar))
                    {
                        //DataTable dtAñoSuperior = bdTS.añoObtenerAñoSuperior(S, miembro, añoEscolar);
                        //DataTable dtAñoInferior = bdTS.añoObtenerAñoInferior(S, miembro, añoEscolar);
                        //DataRow drAñoSuperior = dtAñoSuperior.Rows[0];
                        //DataRow drAñoInferior = dtAñoInferior.Rows[0];
                        //int intAñoSuperior = Int32.Parse(drAñoSuperior["Año"].ToString());
                        //String gradoAñoSuperior = drAñoSuperior["Grado"].ToString();
                        //String estadoAñoSuperior = drAñoSuperior["Estado"].ToString();
                        //int intAñoInferior = Int32.Parse(drAñoInferior["Año"].ToString());
                        //String gradoAñoInferior = drAñoInferior["Grado"].ToString();
                        //String estadoAñoInferior = drAñoInferior["Estado"].ToString();
                        //int diferenciaSuperior = estadoEduc.Equals("PERD") ? (intAñoSuperior - intAñoEscolar) : (intAñoSuperior - intAñoEscolar);
                        //int diferenciaInferior = estadoAñoInferior.Equals("PERD") ? (intAñoEscolar - intAñoInferior) : (intAñoEscolar - intAñoInferior);
                        //Boolean esAptoSuperior = bdTS.añoVerificarAñoCronologicamente(diferenciaSuperior, proximoGrado, true, gradoAñoSuperior);
                        //Boolean esAptoInferior = bdTS.añoVerificarAñoCronologicamente(diferenciaInferior + 1, proximoGrado, false, gradoAñoInferior);
                        Boolean esAptoSuperior = true;
                        Boolean esAptoInferior = true;
                        if (esAptoSuperior && esAptoInferior)
                        {
                            bdTS.añoNuevoAño(S, miembro, añoEscolar, U, proximoGrado, estadoEduc, carrera, notas, seccion);
                            prepararPnlInsertar();
                            llenarGdvAñosEscolar();
                            mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                        }
                        else
                        {
                            //if (L.Equals("es"))
                            //{
                            //    mst.mostrarMsjAdv("No apto cronológicamente." + esAptoInferior + " " + diferenciaInferior + " " + gradoAñoInferior + " " + esAptoSuperior + " " + diferenciaSuperior + " " + gradoAñoSuperior);
                            //}
                            //else
                            //{
                            //    mst.mostrarMsjAdv("No apto cronológicamente.");
                            //}
                        }
                    }
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("Un miembro solo puede tener un Año Escolar por año.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("A member can only have one School Year per year.");
                        }
                    }
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
                    mst.mostrarMsjAdvNtf("El Año debe ser entre " + limiteInferior + " y " + añoSiguiente + ".");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("The Year must be between " + limiteInferior + " and " + añoSiguiente + ".");
                }
            }
        }

        protected void limpiarElementosPnlInsertar()
        {
            //ddlMiembro.SelectedValue = "";
            ddlProximoGrado.SelectedValue = "";
            ddlCarreraEduc.SelectedValue = "";
            ddlEstadoEduc.SelectedValue = "";
            //ddlCentroEduc.Items.Clear();
            txbSeccion.Text = "";
            txbNotas.Text = "";
        }

        protected void prepararPnlInsertar()
        {
            limpiarElementosPnlInsertar();
            pnlActualizarAñoEscolar.Visible = false;
            pnlIngresarAñoEscolar.Visible = true;
            gdvAñosEscolar.Columns[9].Visible = true;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String notas = txbActNotas.Text;
            String proximoGrado = ddlActProximoGrado.SelectedValue;
            String grado = ddlActProximoGrado.SelectedValue;
            String estado = ddlActEstadoEduc.SelectedValue;
            String carrera = ddlActCarreraEduc.SelectedValue;
            String seccion = txbActSeccion.Text;
            //String centroEducProximoGrado = ddlActCentroEduc.SelectedValue;
            try
            {
                if (bdTS.añoVerificarActualizacion(S, miembroSLCT, añoSLCT, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss")))
                {
                    bdTS.añoActualizarAñoEscolar(S, miembroSLCT, añoSLCT, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss"), U, grado, estado, carrera, notas, seccion);
                    mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                    prepararPnlInsertar();
                    llenarGdvAñosEscolar();
                }
                else
                {
                    if (L.Equals("es"))
                    {
                        mst.mostrarMsjAdvNtf("Un miembro solo puede tener un Año Escolar por año.");
                    }
                    else
                    {
                        mst.mostrarMsjAdvNtf("A member can only have one School Year per year.");
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }

        //protected void ddlProximoGrado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    String grado = ddlProximoGrado.SelectedValue;
        //    llenarCentrosEduc(ddlCentroEduc, grado);
        //}

        //protected void ddlActProximoGrado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    String grado = ddlActProximoGrado.SelectedValue;
        //    llenarCentrosEduc(ddlActCentroEduc, grado);
        //}

        protected void gdvMiembros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdMName")
            {
                try
                {
                    miembroSLCT = gdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                    String nombre = gdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                    gdvMiembros.Columns[4].Visible = true;
                    String strEdadMiembro = gdvMiembros.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                    gdvMiembros.Columns[4].Visible = false;
                    if (!String.IsNullOrEmpty(strEdadMiembro))
                    {
                        edadMiembro = Int32.Parse(strEdadMiembro);
                    }
                    lblVMiembro.Text = "&nbsp;&nbsp;" + nombre;
                    pnlMiembros.Visible = false;
                    pnlRegistroAñoEscolar.Visible = true;
                    llenarGdvAñosEscolar();
                    if (vista)
                    {
                        cargarMiembroConSeguridad();
                    }
                }
                catch(Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            prepararPnlInsertar();
            pnlRegistroAñoEscolar.Visible = false;
            pnlMiembros.Visible = true;
        }

        protected void eliminarAñoEscolar(object sender, EventArgs e)
        {
            try
            {
                bdTS.añoEliminarAñoEscolar(S, miembroSLCT, añoSLCT, U, fechaCreacionSLCT.ToString("MM/dd/yyyy HH:mm:ss"));
                llenarGdvAñosEscolar();
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
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
    }
}