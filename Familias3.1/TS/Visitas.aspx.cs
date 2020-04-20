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
using System.Drawing;
namespace Familias3._1.TS
{
    public partial class RegistrarVisitas : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected static Boolean vista;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bdTS = new BDTS();
                BDF = new BDFamilia();
                F = mast.F;
                S = mast.S;
                U = mast.U;
                L = mast.L;
                vista = mast.vista;
                try
                {
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    dic = new Diccionario(L, S);
                    cargarPgnVisitas();
                    cargarConSeguridad();
                }
                catch
                {
                }
            }
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarVisita);
        }
        public static DateTime fechaVisitaSLCT;
        public static DateTime fechaCreacionSLCT;
        public static String idVisitaSLCT;
        public static String tipo;
        public static Boolean actualizar;
        protected void cargarPgnVisitas()
        {
            idVisitaSLCT = "";
            lblDirec.Text = dic.direccion + ":";
            lblTelef.Text = dic.telefono + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblClasif.Text = dic.clasificacion + ":";
            llenarTiposVst();
            llenarGdv();
            llenarGdvObjetivos();
            txbFVisita.Text = DateTime.Now.ToString("dd/MM/yyyy");
            actualizar = false;
            llenarNombres();
        }

        protected void cargarConSeguridad()
        {
            if (vista)
            {
                pnlRegistro.Visible = false;
                pnlObjetivos.Visible = false;
                pnlFam.Visible = false;
                pnlEduc.Visible = false;
                pnlSalud.Visible = false;
                pnlLeg.Visible = false;
                pnlVnd.Visible = false;

                txbEduc.Enabled = false;
                txbFam.Enabled = false;
                txbLeg.Enabled = false;
                txbSalud.Enabled = false;
                txbVnd.Enabled = false;
                btnGuardar.Visible = false;
                btnEliminar.Visible = false;
                btnNuevaVst.Visible = false;
                chkAlcoh.Enabled = false;
                chkAnt.Enabled = false;
                chkCron.Enabled = false;
                chkDeser.Enabled = false;
                chkDivo.Enabled = false;
                chkDPI.Enabled = false;
                chkDroga.Enabled = false;
                chkEcon.Enabled = false;
                chkEmoc.Enabled = false;
                chkEnf.Enabled = false;
                chkEsc.Enabled = false;
                chkPens.Enabled = false;
                chkProb.Enabled = false;
                chkRect.Enabled = false;
                chkRend.Enabled = false;
                chkRepi.Enabled = false;
                chkVIF.Enabled = false;
            }
        }

        protected void seleccionarConSeguridad()
        {
            pnlRegistro.Visible = true;
            pnlObjetivos.Visible = true;
            pnlFam.Visible = true;
            pnlEduc.Visible = true;
            pnlSalud.Visible = true;
            pnlLeg.Visible = true;
            pnlVnd.Visible = true;
            btnEliminar.Visible = false;
            btnNuevaVst.Visible = false;
            desactivarObjetivos();
        }

        protected void desactivarObjetivos()
        {
            foreach (GridViewRow row in gdvObjetivos.Rows)
            {
                CheckBox chkAplica = row.FindControl("chkAplica") as CheckBox;
                chkAplica.Visible = false;
                Label lblAplica = row.FindControl("lblAplica") as Label;
                lblAplica.Visible = true;
            }
        }

        protected void llenarNombres()
        {
            revTxbFVisita.ErrorMessage = dic.msjCampoNecesario;
            revDdlVTipoV.ErrorMessage = dic.msjCampoNecesario;
            lblDirec.Text = dic.direccion + ":";
            lblTS.Text = dic.trabajadorS + ":";
            lblFVisita.Text = "*" + dic.TSfechaVisita + ":";
            lblTipoV.Text = "*" + dic.TStipoVisita + ":";
            lblVisitas.Text = dic.TShistorialVisitas;
            btnGuardar.Text = dic.ingresar;
            btnEliminar.Text = dic.eliminar;
            btnNuevaVst.Text = dic.TSnuevaVisita;
            lblEduca.Text = dic.educacion;
            lblFam.Text = dic.familia;
            lblProbl.Text = dic.problemasLegales;
            lblSalud.Text = dic.salud;
            lblVnd.Text = dic.vivienda;
            lblObs1.Text = dic.observaciones + ":";
            lblObs2.Text = dic.observaciones + ":";
            lblObs3.Text = dic.observaciones + ":";
            lblObs4.Text = dic.observaciones + ":";
            lblObs5.Text = dic.observaciones + ":";
            lblAlcoh.Text = dic.TSalcoholismo;
            lblAnt.Text = dic.TSantecedentesPyP;
            lblCron.Text = dic.TScronicas;
            lblDeser.Text = dic.TSdesercion;
            lblDivo.Text = dic.TSdivorcio;
            lblDPI.Text = dic.TSDPI;
            lblDroga.Text = dic.TSdrogadiccion;
            lblEcon.Text = dic.TSeconomia;
            lblEmoc.Text = dic.TSemocional;
            lblEnf.Text = dic.TSenfermedadesPrimarias;
            lblEsc.Text = dic.TSescritura;
            lblNoTiene.Text = dic.noTiene;
            lblPens.Text = dic.TSpensionAlimenticia;
            lblProb.Text = dic.TSproblemasAprendizaje;
            lblRect.Text = dic.TSrectificacionyPartida;
            lblRend.Text = dic.TSrendimientoAcademico;
            lblRepi.Text = dic.TSrepitencia;
            lblVIF.Text = dic.TSVIF;
            lblObjetivos.Text = dic.TSenfoquesDeVisita;
            txbFVisita.MaxLength = 10;
            txbFam.MaxLength = 510;
            txbEduc.MaxLength = 510;
            txbLeg.MaxLength = 510;
            txbSalud.MaxLength = 510;
            txbVnd.MaxLength = 510;
        }
        protected void llenarTiposVst()
        {
            ddlVTipoV.Items.Clear();
            ddlVTipoV.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdTS.vstObtenerTipos(S, L);
            String Code = "";
            String Tipo = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                Code = row["Code"].ToString();
                Tipo = row["Des"].ToString();
                item = new ListItem(Tipo, Code);
                ddlVTipoV.Items.Add(item);
            }
            ddlVTipoV.SelectedValue = "VICA";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //String Fa = txbFam.Text;
            //String Sa = txbSalud.Text;
            //String E = txbEduc.Text;
            //String V = txbVnd.Text;
            //String Le = txbLeg.Text;
            //if(){
                ingresarVisita();
            //}
        }
        protected void limpiarElementos()
        {
            txbFVisita.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlVTipoV.SelectedValue = "VICA";
            txbFam.Text = "";
            txbSalud.Text = "";
            txbEduc.Text = "";
            txbLeg.Text = "";
            txbVnd.Text = "";
            chkAlcoh.Checked = false;
            chkAnt.Checked = false;
            chkCron.Checked = false;
            chkDeser.Checked = false;
            chkDivo.Checked = false;
            chkDPI.Checked = false;
            chkDroga.Checked = false;
            chkEcon.Checked = false;
            chkEmoc.Checked = false;
            chkEnf.Checked = false;
            chkEsc.Checked = false;
            chkPens.Checked = false;
            chkProb.Checked = false;
            chkRect.Checked = false;
            chkRend.Checked = false;
            chkRepi.Checked = false;
            chkVIF.Checked = false;
        }
        protected void gdvVisitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            fechaVisitaSLCT = Convert.ToDateTime(gdvVisitas.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            fechaCreacionSLCT = Convert.ToDateTime(gdvVisitas.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[5].Text);
            tipo = gdvVisitas.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[4].Text;
            if (e.CommandName == "cmdSeleccionar")
            {
                try
                {
                    actualizar = true;
                    int numFilaEsp = 0;
                    DataTable vstEsp = bdTS.vstObtenerVisitaEspecifica(S, F, L, tipo, fechaVisitaSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    ddlVTipoV.SelectedValue = vstEsp.Rows[numFilaEsp]["VisitType"].ToString();
                    ddlVTipoV.Enabled = false;
                    ddlVTipoV.CssClass = "comboBoxBlueForm";
                    lblVTipoV.Visible = true;
                    lblVTipoV.Text = ddlVTipoV.SelectedItem + "";
                    ddlVTipoV.Visible = false;
                    lblVFVisita.Visible = true;
                    lblVFVisita.Text = vstEsp.Rows[numFilaEsp]["fechaVisita"].ToString();
                    txbFVisita.Text = fechaVisitaSLCT.ToString("dd/MM/yyyy");
                    txbFVisita.Enabled = false;
                    txbFVisita.CssClass = "textBoxBlueForm  date";
                    txbFVisita.Visible = false;
                    chkAlcoh.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["F1V"];
                    chkDroga.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["F2V"];
                    chkEcon.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["F3V"];
                    chkVIF.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["F4V"];
                    txbFam.Text = vstEsp.Rows[numFilaEsp]["F"].ToString();
                    chkCron.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["S1V"];
                    chkEmoc.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["S2V"];
                    chkEnf.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["S3V"];
                    txbSalud.Text = vstEsp.Rows[numFilaEsp]["S"].ToString();
                    chkDeser.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["E1V"];
                    chkProb.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["E2V"];
                    chkRend.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["E3V"];
                    chkRepi.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["E4V"];
                    txbEduc.Text = vstEsp.Rows[numFilaEsp]["E"].ToString();
                    chkAnt.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["L1V"];
                    chkDivo.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["L2V"];
                    chkDPI.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["L3V"];
                    chkEsc.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["L4V"];
                    chkPens.Checked = (Boolean)vstEsp.Rows[numFilaEsp]["L5V"];
                    txbLeg.Text = vstEsp.Rows[numFilaEsp]["L"].ToString();
                    txbVnd.Text = vstEsp.Rows[numFilaEsp]["V"].ToString();
                    idVisitaSLCT = vstEsp.Rows[numFilaEsp]["FamilyVisitId"].ToString();
                    btnGuardar.Text = dic.actualizar;
                    btnNuevaVst.Visible = true;
                    btnEliminar.Visible = true;
                    llenarGdvObjetivos();
                    lblFVisita.Text =  dic.TSfechaVisita + ":";
                    lblTipoV.Text = dic.TStipoVisita + ":";
                    if (vista)
                    {
                        seleccionarConSeguridad();
                    }
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
                //gdvVisitas.Rows[Int32.Parse(e.CommandArgument.ToString())].BackColor = Color.Red;
                //int index = Int32.Parse(e.CommandArgument.ToString());
                //GridViewRow deletedRow = gdvVisitas.Rows[index];
                //deletedRow.BackColor = Color.Red;
            }
            //else if (e.CommandName == "cmdBorrar")
            //{
            //    bdTS.vstEliminar(S, F, tipo, fechaVisita.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //    llenarGdv();
            //}
        }

        protected void ingresarVisita()
        {
            String tipo = ddlVTipoV.Text;
            DateTime nFechaVisita = Convert.ToDateTime(convertirAFechaAmericana(txbFVisita.Text));
            String strFechaVisita = nFechaVisita.ToString("yyyy-MM-dd HH:mm:ss.fff");
            String F1 = "ALCN";
            Boolean F1V = chkAlcoh.Checked;
            String F2 = "DROG";
            Boolean F2V = chkDroga.Checked;
            String F3 = "ECNN";
            Boolean F3V = chkEcon.Checked;
            String F4 = "VIF";
            Boolean F4V = chkVIF.Checked;
            String Fa = txbFam.Text;
            String S1 = "CROC";
            Boolean S1V = chkCron.Checked;
            String S2 = "EMOC";
            Boolean S2V = chkEmoc.Checked;
            String S3 = "EPRI";
            Boolean S3V = chkEnf.Checked;
            String Sa = txbSalud.Text;
            String E1 = "DESE";
            Boolean E1V = chkDeser.Checked;
            String E2 = "PAPN";
            Boolean E2V = chkProb.Checked;
            String E3 = "RACA";
            Boolean E3V = chkRend.Checked;
            String E4 = "REPI";
            Boolean E4V = chkRepi.Checked;
            String E = txbEduc.Text;
            String V = txbVnd.Text;
            String L1 = "ANPN";
            Boolean L1V = chkAnt.Checked;
            String L2 = "DIVN";
            Boolean L2V = chkDivo.Checked;
            String L3 = "DPI";
            Boolean L3V = chkDPI.Checked;
            String L4 = "ESCN";
            Boolean L4V = chkEsc.Checked;
            String L5 = "PALI";
            Boolean L5V = chkPens.Checked;
            String L6 = "RPDN";
            Boolean L6V = chkRect.Checked;
            String Le = txbLeg.Text;
            DateTime fechaVisita = Convert.ToDateTime(nFechaVisita);
            String añoVisita = fechaVisita.Year + "";
            añoVisita = añoVisita.Substring(2);
            String mesVisita = fechaVisita.Month + "";
            if (mesVisita.Length < 2)
            {
                mesVisita = "0" + mesVisita;
            }
            String diaVisita = fechaVisita.Day + "";
            if (diaVisita.Length < 2)
            {
                diaVisita = "0" + diaVisita;
            }
            String idVisita = S + F + tipo + añoVisita + "-" + fechaVisita.Month + "-" + fechaVisita.Day;
            if (((!Sa.Equals("") || !E.Equals("") || !V.Equals("") || !Fa.Equals("") || !Le.Equals("")) && !tipo.Equals("VICN")) || (tipo.Equals("VICN")))
            {
                if (!actualizar)
                {
                    if (nFechaVisita < DateTime.Now)
                    {
                        if (DateTime.Now.AddMonths(-1) < nFechaVisita)
                        {
                            if (!bdTS.vstIngresar(S, F, tipo, strFechaVisita, U, F1, F1V, F2, F2V, F3, F3V, F4, F4V, Fa, S1, S1V, S2, S2V, S3, S3V, Sa, E1, E1V, E2, E2V, E3, E3V, E4, E4V, E, V, L1, L1V, L2, L2V, L3, L3V, L4, L4V, L5, L5V, L6, L6V, Le, idVisita))
                            {
                                if (L.Equals("es"))
                                {
                                    mst.mostrarMsjAdvNtf("Una familia no puede tener más de una visita de un mismo tipo, el mismo día.");
                                }
                                else
                                {
                                    mst.mostrarMsjAdvNtf("A family cannot have more than one visit of the same type, the same day.");
                                }
                            }
                            else
                            {
                                try
                                {
                                    guardarObjetivos(idVisita);
                                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);
                                    nuevaVisitaPrepararElementos();
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
                                mst.mostrarMsjAdvNtf("No es posible registrar visitas, de hace más de un mes.");
                            }
                            else
                            {
                                mst.mostrarMsjAdvNtf("It is not possible to register visits, for more than one month ago.");
                            }
                        }
                    }
                    else
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("Esa fecha, no es válida.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("That date is not valid.");
                        }
                    }
                }
                else
                {
                    if (!bdTS.vstActualizar(S, F, tipo, fechaVisitaSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), strFechaVisita, U, F1, F1V, F2, F2V, F3, F3V, F4, F4V, Fa, S1, S1V, S2, S2V, S3, S3V, Sa, E1, E1V, E2, E2V, E3, E3V, E4, E4V, E, V, L1, L1V, L2, L2V, L3, L3V, L4, L4V, L5, L5V, L6, L6V, Le, idVisita))
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjAdvNtf("Una familia no puede tener más de una visita de un mismo tipo, el mismo día.");
                        }
                        else
                        {
                            mst.mostrarMsjAdvNtf("A family cannot have more than one visit of the same type, the same day.");
                        }
                    }
                    else
                    {
                        try
                        {
                            guardarObjetivos(idVisita);
                            mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                            nuevaVisitaPrepararElementos();
                        }
                        catch (Exception ex)
                        {
                            mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                        }
                    }
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, ingrese información en la visita.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, enter information for the visit.");
                }
            }
        }

        protected void nuevaVisitaPrepararElementos()
        {
            btnNuevaVst.Visible = false;
            btnEliminar.Visible = false;
            btnGuardar.Text = dic.ingresar;
            btnGuardar.Visible = true;
            lblFVisita.Text = "*" + dic.TSfechaVisita + ":";
            lblTipoV.Text = "*" + dic.TStipoVisita + ":";
            txbFVisita.Enabled = true;
            ddlVTipoV.Enabled = true;
            lblVTipoV.Visible = false;
            ddlVTipoV.Visible = true;
            lblVFVisita.Visible = false;
            txbFVisita.Visible = true;
            actualizar = false;
            limpiarElementos();
            idVisitaSLCT = "";
            llenarGdv();
            llenarGdvObjetivos();
        }
        protected void llenarGdv()
        {
            gdvVisitas.Visible = false;
            lblNoTiene.Visible = false;
            DataTable dtVisitas = bdTS.vstObtenerVisitas(S, F, L);
            if (dtVisitas.Rows.Count > 0)
            {
                gdvVisitas.Columns[1].Visible = true;
                gdvVisitas.Columns[4].Visible = true;
                gdvVisitas.Columns[5].Visible = true;
                gdvVisitas.Columns[0].HeaderText = dic.TStipoVisita;
                gdvVisitas.Columns[2].HeaderText = dic.TSfechaVisita;
                gdvVisitas.Columns[3].HeaderText = dic.usuario;
                //gdvVisitas.Columns[6].HeaderText = dic.TSenfoque;
                gdvVisitas.Columns[6].HeaderText = dic.accion;
                gdvVisitas.DataSource = dtVisitas;
                gdvVisitas.DataBind();
                gdvVisitas.Columns[1].Visible = false;
                gdvVisitas.Columns[4].Visible = false;
                gdvVisitas.Columns[5].Visible = false;
                gdvVisitas.Visible = true;
            }
            else
            {
                lblNoTiene.Visible = true;
            }
        }
        protected void llenarGdvObjetivos()
        {
            pnlObjetivos.Visible = false;
            DataTable dtObjetivos = bdTS.vstObjObtenerObjetivosdeVisita(S, L, idVisitaSLCT);
            if (dtObjetivos.Rows.Count > 0)
            {
                pnlObjetivos.Visible = true;
                gdvObjetivos.Columns[0].Visible = true;
                gdvObjetivos.Columns[1].HeaderText = dic.aplica;
                gdvObjetivos.Columns[2].HeaderText = dic.TSenfoque;
                gdvObjetivos.DataSource = dtObjetivos;
                gdvObjetivos.DataBind();
                gdvObjetivos.Columns[0].Visible = false;
                gdvObjetivos.Visible = true;
            }
        }

        protected void guardarObjetivos(String idVisita)
        {
            foreach (GridViewRow row in gdvObjetivos.Rows)
            {
                CheckBox check = row.FindControl("chkAplica") as CheckBox;
                Boolean aplica = check.Checked;
                String objetivo = row.Cells[0].Text;
                if (aplica)
                {
                    if (bdTS.vstVerificarSiExisteObjetivo(S, objetivo, idVisita) == 0)
                    {
                        bdTS.vstObjIngresar(S, idVisita, objetivo, U);
                    }
                }
                else
                {
                    if (bdTS.vstVerificarSiExisteObjetivo(S, objetivo, idVisita) > 0)
                    {
                        bdTS.vstObjEliminar(S, idVisita, objetivo, U);
                    }
                }
            }
        }

        protected void eliminarObjetivos()
        {
            foreach (GridViewRow row in gdvObjetivos.Rows)
            {
                String objetivo = row.Cells[0].Text;
                bdTS.vstObjEliminar(S, idVisitaSLCT, objetivo, U);
            }
        }

        protected void btnNuevaVst_Click(object sender, EventArgs e)
        {
            nuevaVisitaPrepararElementos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }
        protected void eliminarVisita(object sender, EventArgs e)
        {
            try
            {
                bdTS.vstEliminar(S, F, tipo, fechaVisitaSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss.fff"), U);
                llenarGdv();
                eliminarObjetivos();
                nuevaVisitaPrepararElementos();
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